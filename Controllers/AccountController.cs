using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using InnDocs.iHealth.Web.UI.Models;
using System.Text;
using InnDocs.iHealth.Web.UI.Utilities;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using System.Net;

namespace InnDocs.iHealth.Web.UI.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Facebook()
        {

            var fb = new Facebook.FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                //for BumpDocs Product
                //client_id = "315963101876035",
                //client_secret = "1598f42f534f9ceb2b0134fadc2eb850",


                ////for www.BumpDocs.com
                client_id = "559035264151323",
                client_secret = "4d6f18a553d19642742707bee42de85b",

                redirect_uri = UriRedirect.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });



            return Redirect(loginUrl.AbsoluteUri);
        }

        private Uri UriRedirect
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [Analytics(Medium="Facebook Login")]
        public ActionResult FacebookCallback(string code)
        {
            
            Services1 s1 = new Services1();
            RegistrationServiceCalls regser = new RegistrationServiceCalls();
            Services3 PIfoservice = new Services3();
            SingleRegisterModel reguser = null;
            UserInformation ruser = null;
            SingleRegisterModel singleuserObj = new SingleRegisterModel();
            if (code != null)
            {
                var fb = new Facebook.FacebookClient();
                string isemailfound = "";
                dynamic result = fb.Post("oauth/access_token", new
                {
                    //for BumpDocs Product
                    //client_id = "315963101876035",
                    //client_secret = "1598f42f534f9ceb2b0134fadc2eb850",


                    //for www.BumpDocs.com
                    client_id = "559035264151323",
                    client_secret = "4d6f18a553d19642742707bee42de85b",

                    redirect_uri = UriRedirect.AbsoluteUri,
                    code = code
                });

                var access_token = result.access_token;

                //Store the access key in the session
                Session["AccessToken"] = access_token;

                //update fbclient with accesstoken so we can make req on behalf of the client
                fb.AccessToken = access_token;

                //var client = new Facebook.FacebookClient(access_token);

                //get the user info
                dynamic me = fb.Get("me");//fb.Get("me?fields=first_name,last_name,id,email,gender,password");
                string email = me.email;
                string id = me.id;
                string firstname = me.first_name;
                string lastname = me.last_name;
                string gender = me.gender;

                HttpContext.Session["TUN"] = email;

                //set the auth cookie

                if (email == null)
                {
                    return RedirectToAction("Home", "Home");
                }
                if (gender == "male")
                {
                    ViewBag.Message = "This is only for female login";
                    return View("ErrorMessage", masterName: "Common_Layout");
                }
                if (email != null)
                {
                    //isemailfound = s1.Validateemail(email);
                    //reguser = regser.GetRegUserbyEmailId(email);
                    string strHostName = "";
                    strHostName = System.Net.Dns.GetHostName();

                    IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                    IPAddress[] addr = ipEntry.AddressList;
                    string Ip = addr[addr.Length - 2].ToString();
                    //ruser = s1.GetUserAndValidateByEmailNew(email, firstname,Ip);
                    ruser = s1.GetUserAndValidateByEmail(email, firstname);
                }
                if (ruser.IsEmailFound == "Found")
                {
                    // ruser = s1.GetUserByEmail(email);

                    if (ruser.UserId != null)
                    {
                        // PersonalInformation PInfo = PIfoservice.GetPersonalInfo(ruser.UserId);
                        ruser.GroupType = "Family or Individual";
                        //string loginname = PInfo.FirstName;
                        string loginname = ruser.LoginName;
                        Session["LoginName"] = loginname;

                        Session["LoginUser"] = ruser;
                        singleuserObj.EmailId = email;
                        singleuserObj.RUserName = firstname;
                        singleuserObj.UserPlan = ruser.UserPlan;
                        singleuserObj.RUserId = ruser.DomainId;
                    }
                    else
                    {
                        Session["RUserid"] = ruser.DomainId;
                        ViewBag.muserid = ruser.DomainId;
                        //  return RedirectToAction("ConfirmFacebookAccount", "ConfirmAccount");
                        return View("FBAccount", masterName: "Common_Layout");
                    }
                }
                else if (ruser.IsEmailFound == "Found" && reguser.IsFbVerified == false)
                {
                    Session["RUserid"] = ruser.DomainId;
                    ViewBag.muserid = ruser.DomainId;
                    return View("FBAccount", masterName: "Common_Layout");
                    //return RedirectToAction("ConfirmFacebookAccount", "ConfirmAccount");


                }
                else
                {


                    singleuserObj.CreatedOn = System.DateTime.Now;


                    string SuccessCode = "";
                    string domainId = string.Empty;
                    string[] insertedCode;
                    string PhnSuccessCode = "";
                    bool IsSendMail = false;

                    singleuserObj.PhoneNumber = null;
                    singleuserObj.GroupType = "Family or Individual";




                    RegistrationServiceCalls RegSerCalls = null;
                    RegSerCalls = new RegistrationServiceCalls();

                    singleuserObj.EmailId = email;
                    singleuserObj.RUserName = firstname;
                    singleuserObj.UserPlan = ruser.UserPlan;
                    singleuserObj.RUserId = ruser.DomainId;
                    //insertedCode = RegSerCalls.InsertRegisterUserInfo(singleuserObj).Split(new char[] { ',' }); ;
                    if (ruser.DomainId != null)
                    {
                        //string userid = insertedCode[0];
                        Session["RUserid"] = ruser.DomainId;


                        #region Mail Sending
                        //try
                        //{
                        //    string Body = "Dear " + singleuserObj.RUserName + ",\n\n You've successfully connected your Facebook account to BumpDocs, \n\n " + "Now you can sign into BumpDocs using your Facebook account" + "\n\nThank you,\nBumpDocs";


                        //    SendMail _sendMail = new SendMail();

                        //    _sendMail.To = singleuserObj.EmailId;

                        //    _sendMail.Body = Body;
                        //    _sendMail.Subject = "Medlogx Registration details";
                        //    IsSendMail = RegSerCalls.SendMail(_sendMail);

                        ViewBag.muserid = ruser.DomainId;
                        // return RedirectToAction("ConfirmFacebookAccount", "ConfirmAccount");
                        return View("FBAccount", masterName: "Common_Layout");
                        //}
                        //catch (Exception ex)
                        //{
                        //    IsSendMail = false;
                        //}
                        #endregion
                    }
                    else
                    {
                        return RedirectToAction("NewHome", "Home");
                    }
                }

            }
            else
            {
                return RedirectToAction("NewHome", "Home");

            }
            Session["RegUser"] = singleuserObj;
            FormsAuthentication.SetAuthCookie(ruser.UserId, true);
            return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });

        }


        public ActionResult FBAccount()
        {
            ViewBag.muserid = (Session["RUserid"] == null ? null : Session["RUserid"] as string);
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmFacebookAccount(ConfirmSingleRegModel cnfirmSingle, string UserId)
        {
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

            if (domainUser.ResultCode == 1010)
            {
                if (domainUser.UserPlan == "PaidUser")
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
            else if (domainUser.ResultCode == 1011)
            {
                // ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Your account is already confirmed</h3>";
                //ResultData.Data = "Your account is already confirmed";
                //return ResultData;
                ViewBag.message = "Your account is already confirmed";

            }
            else if (domainUser.ResultCode == 1012)
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

            //UserInformation ruser = s1.GetUserByEmail(email);

            //PersonalInformation PInfo = PIfoservice.GetPersonalInfo(ruser.UserId);
            domainUser.GroupType = "Family or Individual";
            //Session["LoginName"] = "Dharma";
            string loginname = domainUser.FirstName;
            Session["LoginName"] = loginname;
            user.UserId = domainUser.fbuserid;
            user.DomainId = domainUser.RUserId;
            user.GroupType = "Family or Individual";
            user.EmailId = domainUser.EmailId;
            if (cnfirmSingle.duemethod == "Date of conception")
            {
                DateTime d = Convert.ToDateTime(cnfirmSingle.duedate);
                user.StartDate = d.AddDays(-14);
            }
            else if (cnfirmSingle.duemethod == "I know my due date")
            {
                DateTime d = Convert.ToDateTime(cnfirmSingle.duedate);
                user.StartDate = d.AddDays(-266);
            }
            else
            {
                user.StartDate = cnfirmSingle.duedate;
            }

            Session["LoginUser"] = user;
            Session["RegUser"] = domainUser;
            return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });

        }

    }
}

