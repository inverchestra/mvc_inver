using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Utilities;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Edit;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Create;
using System.Text;
using System.Net;

namespace InnDocs.iHealth.Web.UI.Controllers {
    public class ConfirmAccountController : Controller {
        public ActionResult ConfirmAccount() {

            ViewBag.muserid = (Session["RUserid"] == null ? null : Session["RUserid"] as string);
            return View("ConfirmAccount", masterName: "Common_Layout");
        }

        public ActionResult ConfirmSubUserAccount() {
            return View();
        }

        public ActionResult ConfirmHospSubUserAccount() {
            return View();

        }

        [HttpPost]
        public ActionResult ConfirmAccountold(string UserId) {
            string UID = UserId;
            Session["Paymentuserid"] = UID;
            LoginAndSignUp _loginSignup = new LoginAndSignUp();
            RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            _loginSignup.RegUser = reguser.GetRegUserbyID(UID);
            _loginSignup.RegUser.IsVerified = true;
            string successcode = reguser.UpdateRUserDetails(_loginSignup.RegUser, UID);
            if (successcode == "1020") {
                UserInformation user = new UserInformation();
                user.EmailId = _loginSignup.RegUser.EmailId;
                user.DomainId = UID;
                user.CreatedOn = System.DateTime.Now;
                user.Password = _loginSignup.RegUser.Password;
                user.IsUsingModeratorCredentials = false;
                user.IsModerator = true;
                user.Relationship = "Moderator";
                user.UserSessionID = "";
                user.IsVerified = true;

                Services1 s = new Services1();
                string iHealthinsertedId = s.InsertiHealthUser(user);
                if (iHealthinsertedId != null) {
                    PersonalInformation pinfo = new PersonalInformation();
                    pinfo.FirstName = _loginSignup.RegUser.FirstName;
                    pinfo.DomainID = UID;
                    pinfo.UserId = iHealthinsertedId;
                    pinfo.Country = _loginSignup.RegUser.Country;
                    pinfo.DateOfBirth = _loginSignup.RegUser.DOB;
                    pinfo.Gender = _loginSignup.RegUser.Gender;
                    pinfo.Contact = _loginSignup.RegUser.PhoneNumber;
                    pinfo.Address = _loginSignup.RegUser.Address;
                    pinfo.ZipCode = _loginSignup.RegUser.Pincode;
                    Services3 s3 = new Services3();
                    int result = s3.InsertPersonalInfo(pinfo, user.GroupType);
                }
            }
            ViewBag.result = "Confirmed. You can login now";
            TempData["UsrObj"] = _loginSignup;
            return RedirectToAction("Payment", "Payment");
        }

        [HttpPost]
        public ActionResult ConfirmSubUserAccountold(string UserId, string Domainid) {
            string UId = UserId;
            Session["Paymentuserid"] = UId;
            Services1 s = new Services1();
            LoginAndSignUp lng = new LoginAndSignUp();
            lng.IHealthUser = s.GetUserbyId(CryptorEngine.Decrypt(UserId, true));
            lng.IHealthUser.IsVerified = true;
            string result = s.UpdateUserDetails(lng.IHealthUser, UserId);
            ViewBag.result = "Confirmed. You can login now";
            TempData["UsrObj"] = lng;
            return RedirectToAction("Payment", "Payment");


        }

        [HttpPost]
        public ActionResult ConfirmSubUserAccount1(string UserId) {
            Session["Paymentuserid"] = UserId;


            Services1 s = new Services1();
            UserInformation userInfo = s.GetUserbyId(UserId);
            userInfo.IsVerified = true;

            string result = s.UpdateUserPasswordDetails(userInfo, UserId);
            ViewBag.result = "Confirmed. You can login now";
            TempData["UsrObj"] = userInfo;
            return RedirectToAction("Home", "Home");
        }


        public ActionResult ConfirmHospRegAccount() {
            ViewBag.muserid = (Session["RUserid"] == null ? null : Session["RUserid"] as string);
            return View();
        }

        public ActionResult ConfirmAccountByMobile() {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ConfirmAccountByMobile(ConfrimAccount cnf) {

            if (ModelState.IsValid) {

                JsonResult ResultData = new JsonResult();
                RegistrationServiceCalls reguser = new RegistrationServiceCalls();
                SingleRegisterModel singleuserObj = reguser.GetRegUserbyMobile(cnf.MobileNumber);
                DateTime expire = Convert.ToDateTime(singleuserObj.OTAExpire);
                DateTime presenttime = DateTime.Now;
                if (!singleuserObj.IsPhnVerified) {
                    if (presenttime <= expire) {
                        if (singleuserObj.OTACode == cnf.OtaCode) {
                            Session["RUserid"] = singleuserObj.RUserId;
                            if (singleuserObj.GroupType == "Hospital")//ck added
                            {
                                //return RedirectToAction("ConfirmHospRegAccount");
                                return RedirectToActionPermanent("ConfirmHospRegAccount");
                            } else {
                                return RedirectToAction("ConfirmAccount");
                            }
                        } else {
                            //ResultData.Data = "Your ota code is not valid";
                            ViewBag.message = "Please enter valid confirmation code.";
                        }
                    } else {
                        //ResultData.Data = "Your Mobile Number or OtaCode not valid";
                        ViewBag.message = "Please check the details or enter valid confirmation code.";
                    }
                } else {
                    //ResultData.Data = "Your Mobile Number is Already Confirmed";
                    ViewBag.message = "You are already a member with us.";
                }

                //return ResultData;
                return View("View1");
            }
            return PartialView("ConfirmAccountByMobile", cnf);
        }



        public ActionResult VerifyEmail() {
            return View();
        }
        [HttpPost]
        public ActionResult VerifyEmail(string UserId, string GroupType) {
            JsonResult ResultData = new JsonResult();
            Services1 s = new Services1();
            string result = "";
            UserInformation userInfo = null;

            // sandeep changes start

            if (GroupType == "1")//grouptype=1 means family or individual user
            {
                userInfo = s.GetUserbyIdNew(UserId, "Family or Individual");
            }
            if (GroupType == "2") {
                userInfo = s.GetUserbyIdNew(UserId, "Hospital");
            }


            if (!userInfo.IsVerified) {
                userInfo.IsVerified = true;

                if (GroupType == "2") {
                    userInfo.UserId = UserId;
                    result = s.UpdateHospitalUserAccountDetails(userInfo);
                    if (result == "1020") {
                        Services3 s3 = new Services3();
                        PersonalInformation pinfo = s3.GetPersonalInfo(UserId);

                        RegistrationServiceCalls res = new RegistrationServiceCalls();
                        SingleRegisterModel reguser = res.GetSingleRegUserbyID(userInfo.DomainId);
                        reguser.EmailId = userInfo.EmailId;
                        reguser.IsVerified = true;
                        int success = res.UpdateRUserDetails(reguser, userInfo.DomainId);
                    }
                }
                if (GroupType == "1") // sandeep added
                {
                    result = s.UpdateUserAccountDetails(userInfo, UserId);
                    if (result == "1020") {
                        Services3 s3 = new Services3();
                        PersonalInformation pinfo = s3.GetPersonalInfo(UserId);

                        RegistrationServiceCalls res = new RegistrationServiceCalls();
                        SingleRegisterModel reguser = res.GetSingleRegUserbyID(userInfo.DomainId);
                        reguser.EmailId = userInfo.EmailId;
                        reguser.IsVerified = true;
                        int success = res.UpdateRUserDetails(reguser, userInfo.DomainId);
                    }
                }

            } else {
                result = "1021";
            }

            if (result == "1020") {
                ResultData.Data = "Confirmed. You can login now";
            } else if (result == "1021") {
                ResultData.Data = "Your account is already confirmed";
            } else {
                ResultData.Data = "Confirm account Failed. ";
            }
            return ResultData;

        }
        
        [HttpPost]
        public ActionResult ConfirmAccount(ConfirmSingleRegModel cnfirmSingle, string UserId) {
            int errorCode = 0;
            int successcode = 0;
            string SuccessCode = "";
            string UID = UserId;
            JsonResult ResultData = new JsonResult();
            RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            if (cnfirmSingle.Gestination == true)
            {
                cnfirmSingle.filtertext = "Gestation";
            }
            if (cnfirmSingle.Hypertension == true)
            {
                if (cnfirmSingle.filtertext != null)
                {
                    cnfirmSingle.filtertext = cnfirmSingle.filtertext + ",Hypertension";
                }
                else
                {
                    cnfirmSingle.filtertext = "Hypertension";
                }
            }
            if (cnfirmSingle.Latepregnancy == true)
            {
                if (cnfirmSingle.filtertext != null)
                {
                    cnfirmSingle.filtertext = cnfirmSingle.filtertext + ",Latepregnancy";
                }
                else
                {
                    cnfirmSingle.filtertext = "Latepregnancy";
                }
            }
            if (cnfirmSingle.Earlypregnancy == true)
            {
                if (cnfirmSingle.filtertext != null)
                {
                    cnfirmSingle.filtertext = cnfirmSingle.filtertext + ",Earlypregnancy";
                }
                else
                {
                    cnfirmSingle.filtertext = "Earlypregnancy";
                }
            }
            if (cnfirmSingle.Thyroid == true)
            {
                if (cnfirmSingle.filtertext != null)
                {
                    cnfirmSingle.filtertext = cnfirmSingle.filtertext + ",Thyroid";
                }
                else
                {
                    cnfirmSingle.filtertext = "Thyroid";
                }
            }
            if (cnfirmSingle.Diabetes == true)
            {
                if (cnfirmSingle.filtertext != null)
                {
                    cnfirmSingle.filtertext = cnfirmSingle.filtertext + ",Diabetes";
                }
                else
                {
                    cnfirmSingle.filtertext = "Diabetes";
                }
            }
            if (cnfirmSingle.Twins == true)
            {
                if (cnfirmSingle.filtertext != null)
                {
                    cnfirmSingle.filtertext = cnfirmSingle.filtertext + ",Twins";
                }
                else
                {
                    cnfirmSingle.filtertext = "Twins";
                }
            }
            if (cnfirmSingle.Tripplets == true)
            {
                if (cnfirmSingle.filtertext != null)
                {
                    cnfirmSingle.filtertext = cnfirmSingle.filtertext + ",Tripplets";
                }
                else
                {
                    cnfirmSingle.filtertext = "Tripplets";
                }
            }
            if (cnfirmSingle.Asthma == true)
            {
                if (cnfirmSingle.filtertext != null)
                {
                    cnfirmSingle.filtertext = cnfirmSingle.filtertext + ",Asthma";
                }
                else
                {
                    cnfirmSingle.filtertext = "Asthma";
                }
            }
            if (cnfirmSingle.Outdoorsports == true)
            {
                cnfirmSingle.interests = "Outdoorsports";
            }
            if (cnfirmSingle.Swimming == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Swimming";
                }
                else
                {
                    cnfirmSingle.interests = "Swimming";
                }
            }
            if (cnfirmSingle.Yoga == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Yoga";
                }
                else
                {
                    cnfirmSingle.interests = "Yoga";
                }
            }
            if (cnfirmSingle.Adventuresports == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Adventuresports";
                }
                else
                {
                    cnfirmSingle.interests = "Adventuresports";
                }
            }
            if (cnfirmSingle.Gardening == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Gardening";
                }
                else
                {
                    cnfirmSingle.interests = "Gardening";
                }
            }
            if (cnfirmSingle.Animalcare == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Animalcare";
                }
                else
                {
                    cnfirmSingle.interests = "Animalcare";
                }
            }
            if (cnfirmSingle.Pets == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Pets";
                }
                else
                {
                    cnfirmSingle.interests = "Pets";
                }
            }
            if (cnfirmSingle.Arts == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Arts";
                }
                else
                {
                    cnfirmSingle.interests = "Arts";
                }
            }
            if (cnfirmSingle.Modeling == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Modeling";
                }
                else
                {
                    cnfirmSingle.interests = "Modeling";
                }
            }
            if (cnfirmSingle.Interiordesigning == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Interiordesigning";
                }
                else
                {
                    cnfirmSingle.interests = "Interiordesigning";
                }
            }
            if (cnfirmSingle.Travelling == true)
            {
                if (cnfirmSingle.interests != null)
                {
                    cnfirmSingle.interests = cnfirmSingle.interests + ",Travelling";
                }
                else
                {
                    cnfirmSingle.interests = "Travelling";
                }
            }
            if (cnfirmSingle.Privacy == false)
            {
                cnfirmSingle.Privacy = false;

            }
            else
            {
                cnfirmSingle.Privacy = true;
            }
            cnfirmSingle.Notificationtime = "8:00";
            SingleRegisterModel domainUser = reguser.ConfirmRegUserbyId(cnfirmSingle, UserId);
            int result = 0;
            int IsExpiry = 0;
            DateTime date1 = new DateTime();
            date1 = DateTime.Now;
            DateTime date2 = Convert.ToDateTime(domainUser.ExpireOn);
            IsExpiry = DateTime.Compare(date1, date2);
            bool IsSendMail = false;

            if (domainUser.ResultCode == 1010) {
                if (domainUser.UserPlan == "PaidUser") {
                    ViewBag.result = "Your registration process has been completed.You can start using BumpDocs";
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Confirm account Failed. You can login now</h3>";
                    TempData["UsrObj"] = domainUser;
                    return RedirectToAction("PaymentNew", "Payment");
                } else if (IsExpiry > 1) {
                    ViewBag.result = "Your registration process has been completed.You can start using BumpDocs";
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Confirm account Failed. You can login now</h3>";
                    TempData["UsrObj"] = domainUser;
                    return RedirectToAction("PaymentNew", "Payment");
                } else {
                    ViewBag.message = "Your registration process has been completed.You can start using BumpDocs";
                }

            } else if (domainUser.ResultCode == 1011) {
                ViewBag.message = "Your account is already confirmed";

            } else if (domainUser.ResultCode == 1012) {
                ViewBag.message = "User does not exist with us.Please register with BumDocs";
            } else {
                ViewBag.message = "Confirm account failed.Try again..";
            }
            return View("View1", masterName: "Common_Layout");

        }


        public ActionResult ConfirmFacebookAccount() {
            ViewBag.muserid = (Session["RUserid"] == null ? null : Session["RUserid"] as string);
            return View();
        }
        [HttpPost]
        public ActionResult FacebookConfirmAccount(ConfirmSingleRegModel cnfirmSingle, string UserId) {
            cnfirmSingle.IsFbVerified = true;
            int errorCode = 0;
            int successcode = 0;
            string SuccessCode = "";
            string UID = UserId;
            JsonResult ResultData = new JsonResult();
            RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            SingleRegisterModel domainUser = reguser.ConfirmRegUserbyId(cnfirmSingle, UserId);//.GetSingleRegUserbyID(UID);
            UserInformation user = new UserInformation();
            PersonalInformation pinfo = new PersonalInformation();
            int result = 0;
            int IsExpiry = 0;

            bool IsSendMail = false;

            if (domainUser.ResultCode == 1010) {
                if (domainUser.UserPlan == "PaidUser") {
                    ViewBag.result = "Confirmed. You can login now";
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Confirm account Failed. You can login now</h3>";
                    TempData["UsrObj"] = domainUser;
                    return RedirectToAction("PaymentNew", "Payment");
                } else if (IsExpiry > 1) {
                    ViewBag.result = "Confirmed. You can login now";
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Confirm account Failed. You can login now</h3>";
                    TempData["UsrObj"] = domainUser;
                    return RedirectToAction("PaymentNew", "Payment");
                } else {
                    ViewBag.message = "Confirmed. You can login now";
                }

            } else if (domainUser.ResultCode == 1011) {
                ViewBag.message = "Your account is already confirmed";

            } else if (domainUser.ResultCode == 1012) {
                ViewBag.message = "User not exist";
            } else {
                ViewBag.message = "Confirm account failed. You can login now";
            }

            domainUser.GroupType = "Family or Individual";

            string loginname = domainUser.FirstName;
            Session["LoginName"] = loginname;
            user.UserId = domainUser.fbuserid;
            user.DomainId = domainUser.RUserId;
            user.GroupType = "Family or Individual";
            user.EmailId = domainUser.EmailId;
            if (cnfirmSingle.duemethod == "Date of conception") {
                DateTime d = Convert.ToDateTime(cnfirmSingle.duedate);
                user.StartDate = d.AddDays(-14);
            } else if (cnfirmSingle.duemethod == "I know my due date") {
                DateTime d = Convert.ToDateTime(cnfirmSingle.duedate);
                user.StartDate = d.AddDays(-266);
            } else {
                user.StartDate = cnfirmSingle.duedate;
            }

            Session["LoginUser"] = user;
            Session["RegUser"] = domainUser;
            return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });

        }
    }
}
