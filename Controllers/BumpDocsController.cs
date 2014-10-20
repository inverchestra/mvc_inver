using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Utilities;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using log4net;
using System.Reflection;
using CaptchaMvc.HtmlHelpers;
using System.Web.Security;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace InnDocs.iHealth.Web.UI.Controllers
{
    public class BumpDocsController : Controller
    {
        //
        // GET: /BumpDocs/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ConfirmAccount()
        {

            ViewBag.muserid = (Session["RUserid"] == null ? null : Session["RUserid"] as string);
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmAccount(ConfirmSingleRegModel cnfirmSingle, string UserId)
        {
            int errorCode = 0;
            int successcode = 0;
            string SuccessCode = "";
            string UID = UserId;
            JsonResult ResultData = new JsonResult();
            RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            SingleRegisterModel domainUser = reguser.GetSingleRegUserbyID(UID);
            int result = 0;
            int IsExpiry = 0;

            bool IsSendMail = false;
            if (domainUser.RUserId != null)
            {
                if (!domainUser.IsVerified)
                {
                    if (domainUser.EmailId == null)
                    {
                        domainUser.IsPhnVerified = true;
                    }
                    else
                    {
                        domainUser.IsVerified = true;
                    }
                    domainUser.Country = cnfirmSingle.Country;
                    domainUser.DOB = cnfirmSingle.DOB;
                    domainUser.Gender = cnfirmSingle.Gender;
                    domainUser.Pincode = cnfirmSingle.Pincode;
                    domainUser.UserPlan = cnfirmSingle.FreeUser;
                    // statr - chp
                    string charPoolCustId = "1234567890ZabcdABCDEFGOPQRSTUVWXYefghijklmHIJKLMNnopqrstuvwxyz";
                    StringBuilder rsCustId = new StringBuilder();
                    Random randomCustId = new Random();
                    for (int j = 0; j < 6; j++)
                    {
                        rsCustId.Append(charPoolCustId[(int)(randomCustId.NextDouble() * charPoolCustId.Length)]);
                    }
                    string custCode = Convert.ToString(rsCustId);

                    domainUser.CustomerId = custCode;
                    // end - chp
                    if (cnfirmSingle.FreeUser == "FreeUser")
                    {
                        string strDate = domainUser.CreatedOn.ToString();
                        DateTime date = DateTime.Parse(strDate);
                        string expireDate = date.AddDays(15).ToShortDateString();
                        domainUser.ExpireOn = Convert.ToDateTime(expireDate);
                    }
                    else if(cnfirmSingle.FreeUser=="Paiduser")
                    {
                    }
                    successcode = reguser.UpdateRUserDetails(domainUser, UID);
                    DateTime date1 = new DateTime();
                    date1 = DateTime.Now;
                    DateTime date2 = Convert.ToDateTime(domainUser.ExpireOn);
                    IsExpiry = DateTime.Compare(date1, date2);
                }
                else
                {
                    result = 1011;
                }
            }
            else
            {
                result = 1012;
            }
            if (successcode == 1020)
            {

                UserInformation user = new UserInformation();
                if (!String.IsNullOrWhiteSpace(domainUser.GroupType))
                {
                    if (domainUser.GroupType == "Family or Individual")
                    {
                        user.Relationship = "FamilyModerator";
                        user.RoleName = "FamilyModerator";
                    }
                }
                else
                {
                    user.RoleName = "SingleModerator";
                }

                user.EmailId = domainUser.EmailId;
                user.PhoneNo = domainUser.PhoneNumber;
                user.DomainId = UID;
                user.CreatedOn = System.DateTime.Now;
                user.Password = domainUser.Password;
                user.IsUsingModeratorCredentials = false;
                user.IsModerator = true;

                user.UserSessionID = "";
                //if (user.PhoneNo != 0)
                if (!string.IsNullOrWhiteSpace(user.PhoneNo.ToString()) && !string.IsNullOrEmpty(user.PhoneNo.ToString()))
                {
                    user.IsPhnVerified = true;
                    user.PreferredBy = 2;
                }
                else
                {
                    user.IsVerified = true;
                    user.PreferredBy = 1;
                }
                Services1 s = new Services1();
                string iHealthinsertedId = s.InsertiHealthUserNew(user, domainUser.GroupType);
                Session["Paymentuserid"] = iHealthinsertedId;
                if (iHealthinsertedId != null)
                {
                    PersonalInformation pinfo = new PersonalInformation();
                    pinfo.FirstName = domainUser.FirstName;
                    //pinfo.LastName = domainUser.LastName;
                    pinfo.DomainID = UID;
                    pinfo.UserId = iHealthinsertedId;
                    pinfo.CreatedBy = iHealthinsertedId;

                    pinfo.Country = domainUser.Country;
                    pinfo.DateOfBirth = domainUser.DOB;
                    pinfo.Gender = domainUser.Gender;
                    pinfo.Contact = domainUser.PhoneNumber;
                    pinfo.Address = domainUser.Address;
                    pinfo.ZipCode = domainUser.Pincode;
                    Services3 s3 = new Services3();
                    if (!String.IsNullOrWhiteSpace(domainUser.GroupType))
                    {
                        if (domainUser.GroupType == "Family or Individual")
                        {
                            pinfo.Type = "iHealthuser";
                            result = s3.InsertPersonalInfo(pinfo, domainUser.GroupType);
                        }
                    }

                }

            }

            if (result == 1010)
            {
                if (domainUser.FreeUser == "paidUser")
                {
                    ViewBag.result = "Confirmed. You can login now";
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Confirm account Failed. You can login now</h3>";
                    TempData["UsrObj"] = domainUser;
                    return RedirectToAction("PaymentNew", "Payment");
                }
                else if (IsExpiry > 1)
                {
                    ViewBag.result = "Confirmed. You can login now";
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Confirm account Failed. You can login now</h3>";
                    TempData["UsrObj"] = domainUser;
                    return RedirectToAction("PaymentNew", "Payment");
                }
                else
                {
                    // ViewBag.result = "Confirmed. You can login now";
                    // ResultData.Data = "Confirmed. You can login now";
                    //return ResultData;
                    ViewBag.message = "Confirmed. You can login now";
                    //return Content("<script language='javascript' type='text/javascript'>alert('Save Successfully');</script>");
                }

            }
            else if (result == 1011)
            {
                // ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Your account is already confirmed</h3>";
                //ResultData.Data = "Your account is already confirmed";
                //return ResultData;
                ViewBag.message = "Your account is already confirmed";

            }
            else if (result == 1012)
            {
                //ResultData.Data = "User Not Exist";
                //return ResultData;
                ViewBag.message = "User not exist";
            }
            else
            {
                //ResultData.Data = "Confirm account Failed. You can login now";
                //return ResultData;
                ViewBag.message = "Confirm account failed. You can login now";
            }
            return View("View1");

        }

        [HttpPost]
        public ActionResult Login(UserInformation usr)
        {
            usr.GroupType = "Patient";
            if (ModelState.IsValid)//Added by SMitra
            {
                if (usr.EmailId.Contains('@'))//ck added
                {
                    if (!Regex.IsMatch(usr.EmailId, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                    {
                        ModelState.AddModelError("EmailId", "Email format is invalid.");
                    }
                }
                else
                {
                    if (!Regex.IsMatch(usr.EmailId, @"^(?:[0-9]+(?:-[0-9])?)*$"))
                    {
                        ModelState.AddModelError("EmailId", "Phone number is invalid.");
                    }
                }
                if (!ModelState.IsValid)
                {
                    return View("Home", usr);
                }


                Services1 UsrService = new Services1();
                Services3 PIfoservice = new Services3();
                if (usr.HospitalID == null)
                {
                    usr.HospitalID = "some";
                }

                UserInformation resultUser = UsrService.ValidateUserNew(usr.EmailId, usr.Password, usr.GroupType, usr.HospitalID);

                //resultUser.GroupType = usr.GroupType;
                if (resultUser.ErrorCode == "9150")
                {
                    ModelState.AddModelError("EmailId", "Not a User");
                    ViewBag.Password = "Invalid User.";
                    return View("Home", usr);
                }
                else if (resultUser.ErrorCode == "-1")
                {
                    ModelState.AddModelError("EmailId", "InvalidEmail");
                    ViewBag.Password = "Invalid Credentials.";
                    return View("Home", usr);
                }
                else if (resultUser.ErrorCode == "-2")
                {
                    ModelState.AddModelError("HospitalID", "Invalid HospitalId");
                    //ViewBag.Password = "Invalid Credentials.";
                    return View("Home", usr);
                }
                else  // here need to change code for admin login
                {
                    PersonalInformation PInfo = PIfoservice.GetPersonalInfo(resultUser.UserId);
                    SingleRegisterModel DomainUser = new SingleRegisterModel();
                    RegistrationServiceCalls RegService = new RegistrationServiceCalls();
                    DomainUser = RegService.GetSingleRegUserbyID(resultUser.DomainId.ToString());
                    Session["RegUser"] = DomainUser;//added by usha for dropdown to check is group user or not

                    DateTime date1 = new DateTime();
                    date1 = DateTime.Now;
                    DateTime date2 = Convert.ToDateTime(DomainUser.ExpireOn);
                    int IsExpiry = DateTime.Compare(date1, date2);
                    //string loginname = PInfo.FirstName + " " + (PInfo.LastName != null ? PInfo.LastName : string.Empty);
                    string loginname = PInfo.FirstName;
                    Session["LoginName"] = loginname;
                    resultUser.GroupType = DomainUser.GroupType;
                    resultUser.Password = DomainUser.Password;
                    Session["LoginUser"] = resultUser;


                    if ((IsExpiry < 1) && (!resultUser.IsDeleted) && (!resultUser.IsSuspend))
                    {
                        if (DomainUser.UserPlan == "PaidUser")
                        {
                            resultUser.IsTransaction = true;
                            if (resultUser.IsTransaction == true)
                            {
                                FormsAuthentication.SetAuthCookie(usr.EmailId, true);
                                ViewData["LoginSignUp"] = usr;
                                /* Hospital venkateswari */
                                if (DomainUser.GroupType != null)
                                {
                                    if (DomainUser.GroupType == "Hospital")
                                    {
                                        if (resultUser.Relationship == "Patient")
                                        {
                                            FormsAuthentication.SetAuthCookie(usr.EmailId, true);

                                            return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                        }
                                        else if (resultUser.Relationship == "Doctor")
                                        {

                                        }
                                        else
                                        {
                                            FormsAuthentication.SetAuthCookie(usr.EmailId, true);

                                            return RedirectToAction("HospitalInfo", "HospAdmin", new { Area = "HospitalUser" });
                                        }
                                    }

                                    else
                                    { // login with admin
                                        if (usr.EmailId == "admin@inndocs.com")
                                        {
                                            FormsAuthentication.SetAuthCookie(usr.EmailId, true);
                                            return RedirectToAction("UserAdministration", "Administration", new { Area = "iHealthUser" });
                                        }
                                        else
                                        {
                                            FormsAuthentication.SetAuthCookie(usr.EmailId, true);
                                            return RedirectToAction("PInfo", "PersonalInfo", new { Area = "iHealthUser" });
                                        }
                                    }
                                }
                                else
                                {
                                    FormsAuthentication.SetAuthCookie(usr.EmailId, true);

                                    return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                }
                                /* Hospital venkateswari */
                            }
                            else
                            {
                                LoginAndSignUp lng = new LoginAndSignUp();
                                lng.IHealthUser = resultUser;
                                TempData["UsrObj"] = lng;
                                ViewData["LoginSignUp"] = usr;
                                return RedirectToAction("Payment", "Payment");
                            }

                        }
                        else
                        {

                            ViewData["LoginSignUp"] = resultUser;


                            /* Hospital venkateswari */
                            if (DomainUser.GroupType != null)
                            {
                                //if (DomainUser.PhoneNumber != 0)
                                if (!string.IsNullOrWhiteSpace(DomainUser.PhoneNumber.ToString()) && !string.IsNullOrEmpty(DomainUser.PhoneNumber.ToString()))//ck added and modified
                                {
                                    if (DomainUser.GroupType == "Hospital" && resultUser.Relationship == "Doctor")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.PhoneNo.ToString(), true);


                                        return RedirectToAction("DoctorInfo", "Doctor", new { Area = "iHealthUser" });
                                    }

                                    else if (DomainUser.GroupType == "Hospital" && resultUser.Relationship == "Patient")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.PhoneNo.ToString(), true);

                                        return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });

                                    }
                                    else if (DomainUser.GroupType == "Hospital")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.PhoneNo.ToString(), true);

                                        return RedirectToAction("HospitalInfo", "HospAdmin", new { Area = "HospitalUser" });
                                    }

                                    else
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.PhoneNo.ToString(), true);

                                        return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                    }
                                }
                                else
                                {
                                    if (DomainUser.GroupType == "Hospital" && resultUser.Relationship == "Doctor")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.EmailId, true);


                                        return RedirectToAction("DoctorInfo", "Doctor", new { Area = "iHealthUser" });
                                    }

                                    else if (DomainUser.GroupType == "Hospital" && resultUser.Relationship == "Patient")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.EmailId, true);

                                        return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });

                                    }
                                    else if (DomainUser.GroupType == "Hospital")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.EmailId, true);

                                        return RedirectToAction("HospitalInfo", "HospAdmin", new { Area = "HospitalUser" });
                                    }

                                    else
                                    {
                                        if (usr.EmailId == "admin@inndocs.com")
                                        {
                                            FormsAuthentication.SetAuthCookie(usr.EmailId, true);
                                            return RedirectToAction("UserAdministration", "Administration", new { Area = "iHealthUser" });
                                        }
                                        else
                                        {
                                            FormsAuthentication.SetAuthCookie(usr.EmailId, true);
                                            return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                        }
                                    }
                                }


                            }

                            /* Hospital venkateswari */
                        }
                    }
                    else if ((IsExpiry > 1))
                    {
                        ViewBag.Password = "User Expired";
                        LoginAndSignUp lng = new LoginAndSignUp();
                        lng.IHealthUser = resultUser;
                        TempData["UsrObj"] = lng;
                        // ViewData["LoginSignUp"] = LogAdnSignreUpObj;
                        return RedirectToAction("PaymentNew", "Payment");
                    }
                    else if (resultUser.IsDeleted)
                    {
                        ViewBag.Password = "User deleted";
                        return View("Home", usr);
                    }
                    else if (resultUser.IsSuspend)
                    {
                        ViewBag.Password = "User Suspended";
                        return View("Home", usr);
                    }
                    //else
                    //{//Changes done by SMitra
                    //    ModelState.AddModelError("EmailId", "InvalidEmail");
                    //    ViewBag.Password = "Invalid Credentials.";
                    //    return View("Home", LogAdnSignUpObj);
                    //}
                }

            }

            return View("Home", usr);

        }
    }
}
