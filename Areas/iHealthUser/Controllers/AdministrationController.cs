using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using InnDocs.iHealth.Web.UI.Utilities;
using System.IO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using log4net;
//using System.Web.Security;
//using EnCryptDecrypt;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    [SessionExpireFilter]
    public class AdministrationController : Controller
    {

        private static ILog _log = LogManager.GetLogger("Web.UI");
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserManagement()
        {
            LoginAndSignUp _loginandSignUp = null; _loginandSignUp = new LoginAndSignUp();
            UserInformation usr = null; usr = new UserInformation();
            RegisteredUser RUsr = null; RUsr = new RegisteredUser();
            List<UserInformation> Users = null; Users = new List<UserInformation>();
            List<RegisteredUser> RegUsers = null; RegUsers = new List<RegisteredUser>();
            Services1 userService = new Services1();
            Services3 PinfoService = new Services3();
            RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
            //AD
            IList<RegisteredUser> RegUsrInfo = null; RegUsrInfo = new List<RegisteredUser>();
            Users = userService.GetAllActiveUsers();//.GetAlliHealthUsers();
            _loginandSignUp.LstPInfo = PinfoService.GetAllActivePinfoUsers();
            _loginandSignUp.StypeListfrAdmin = _loginandSignUp.StypesfrAdmin();
            _loginandSignUp.CountryList = _loginandSignUp.Countries();
            _loginandSignUp.IHealthUser = usr;
            _loginandSignUp.RegUser = RUsr;
            _loginandSignUp.Users = Users;

            return View("UserManagement", _loginandSignUp);
        }
        public ActionResult CreateUser()
        {
            return PartialView("CreateUser");
        }

        public ActionResult AdminSelect()
        {
            return PartialView(new UserInformation());
        }

        public ActionResult AdminSingleRegister(string value1, string value2)
        {

            SingleRegisterModel regUser = new SingleRegisterModel();
            if (!String.IsNullOrWhiteSpace(value1))
            {
                if (value1 == "Family")
                {
                    regUser.GroupType = "Family or Individual";
                }
                else if (value1 == "Hospital")
                {
                    regUser.GroupType = value1;
                }
            }

            else
            {
                regUser.GroupType = "Family or Individual";
            }


            ViewData["GroupType"] = regUser.GroupType;
            ViewData["IsGroupUser"] = value2;

            return PartialView("AdminSingleRegister", regUser);
        }

        [HttpPost]
        public ActionResult AdminSingleRegister(SingleRegisterModel singleuserObj)
        {
            string SuccessCode = "";
            string domainId = string.Empty;
            string[] insertedCode;
            string PhnSuccessCode = "";

            JsonResult ResultData = new JsonResult();

            singleuserObj.CreatedOn = System.DateTime.Now;




            if (singleuserObj.EmailId.Contains('@'))//ck added
            {
                singleuserObj.PhoneNumber = null;


            }
            else
            {
                string charPool = "ABCDEFGOPQRSTUVWXY1234567890ZabcdefghijklmHIJKLMNnopqrstuvwxyz";
                StringBuilder rs = new StringBuilder();
                Random random = new Random();

                for (int i = 0; i < 4; i++)
                {
                    rs.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
                }
                string otaCode = Convert.ToString(rs);
                DateTime? otaexpires = DateTime.Now;
                singleuserObj.OTACode = otaCode;
                // singleuserObj.OTAExpire = otaexpires;
                //
                string strDate = singleuserObj.CreatedOn.ToString();
                DateTime date = DateTime.Parse(strDate);
                DateTime expireDate = date.AddHours(24);
                singleuserObj.OTAExpire = Convert.ToDateTime(expireDate);
                //

                singleuserObj.PhoneNumber = Convert.ToInt64(singleuserObj.EmailId);
                singleuserObj.EmailId = null;
            }
            singleuserObj.Password = CryptorEngine.Encrypt(singleuserObj.Password, true);
            RegistrationServiceCalls RegSerCalls = null;
            RegSerCalls = new RegistrationServiceCalls();
            try
            {
                insertedCode = RegSerCalls.InsertRegisterUserInfo(singleuserObj).Split(new char[] { ',' }); ;

                if (insertedCode != null)
                {
                    if (insertedCode[0] != "")
                    {
                        domainId = insertedCode[0];
                    }
                    if (insertedCode[1] != "")
                    {
                        SuccessCode = insertedCode[1];
                    }
                    if (insertedCode[2] != "")
                    {
                        PhnSuccessCode = insertedCode[2];
                    }
                }
            }

            catch (Exception ex)
            {
                SuccessCode = "1011";
                throw ex;
            }

            /* Hospital venkateswari */

            bool IsSendMail = false;



            if (Convert.ToInt32(PhnSuccessCode) == 1050)
            {

                string phnno = Convert.ToString(singleuserObj.PhoneNumber);
                //     string Uid = "7386041104";
                //     string Password = "55312";
                //     string Number = phnno;
                string Message = "Your one time password for Medlox is..:" + "" + singleuserObj.OTACode;
                //     HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + Uid + "&pwd=" + Password +
                //"&phone=" + Number + "&msg=" + Message + "&provider=Fullonsms");
                //     // Get the response.
                //     HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                //     System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                //     string responseString = respStreamReader.ReadToEnd();
                //     // Close the Stream object.
                //     respStreamReader.Close();
                //     myResp.Close();

                SendMessage _sendmessage = new SendMessage();
                _sendmessage.TophNumber = phnno;
                _sendmessage.Message = Message;
                bool SendMessage = RegSerCalls.SendMessage(_sendmessage);
            }


            if (Convert.ToInt32(SuccessCode) == 1010)
            {
                #region Mail Sending
                try
                {
                    //MailMessage mail = new MailMessage();
                    //mail.To.Add(singleuserObj.EmailId);
                    //mail.From = new MailAddress("medlogx@gmail.com");
                    //mail.Subject = "Registration details";
                    // string Body = "Dear " + singleuserObj.FirstName + ",\n\n You have been successfully registered with us.\n\n Please click on below link to verify your email ID. \n\n " + "107.20.220.222:6845/confirmaccount/confirmaccount?userid=" + domainId + "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";
                    string Body = "Dear " + singleuserObj.RUserName + ",\n\n You have been successfully registered with us.\n\n Please click on below link to verify your email ID. \n\n " + "http://107.20.220.222:2313/confirmaccount/confirmaccount?userid=" + domainId + "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nBumpDocs";

                    //mail.Body = Body;
                    //mail.IsBodyHtml = false;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    //smtp.UseDefaultCredentials = true;
                    //smtp.Credentials = new System.Net.NetworkCredential("medlogx@gmail.com", "medlogx2013");
                    //smtp.EnableSsl = true;
                    //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    //smtp.Send(mail);
                    //IsSendMail = true;
                    SendMail _sendMail = new SendMail();

                    _sendMail.To = singleuserObj.EmailId;

                    _sendMail.Body = Body;
                    _sendMail.Subject = "BumpDocs Registration details";
                    IsSendMail = RegSerCalls.SendMail(_sendMail);


                }
                catch (Exception ex)
                {
                    IsSendMail = false;
                }
                #endregion

                if (IsSendMail)
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with BumpDocs, Please check your mail to confirm your registration.</h3>";
                }
                else
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Please check details your entered and try again.</h3>";
                }
            }
            else if (Convert.ToInt32(SuccessCode) == 1041)
            {
                ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Your email ID is already registered with us.</h3>";
            }

            else if (Convert.ToInt32(SuccessCode) == 1051)//ck modified,added
            {
                ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Your phone number is already registered with us.</h3>";
            }

            else if (Convert.ToInt32(PhnSuccessCode) == 1050)
            {
                ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with BumpDocs, Please check your mobile number to confirm your registration.</h3>";
            }
            else
            {
                ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Please check details your entered and try again.</h3>";
            }
            return ResultData;

        }


        //public ActionResult Register(LoginAndSignUp LogAdnSignUpObj)
        //{

        //    int SuccessCode = 0;
        //    int InsertedID = 0;
        //    string[] insertedCode;
        //    int iHealthinsertedId = 0;
        //    RegisteredUser regUser = null;
        //    regUser = LogAdnSignUpObj.RegUser;

        //    regUser.Password = InnDocs.iHealth.Web.UI.Utilities.CryptorEngine.Encrypt(regUser.Password, true);
        //    regUser.CreatedOn = System.DateTime.Now;
        //    //Added by AD Starts here
        //    bool _pCodeMatch = false;

        //    //Added by kumar code strat here
        //    if (LogAdnSignUpObj.RegUser.FreeUser == true)
        //    {
        //        regUser.UserPlan = "FreeUser";
        //    }
        //    if (LogAdnSignUpObj.RegUser.TryIt == true)
        //    {
        //        regUser.UserPlan = "TrailUser";
        //    }
        //    if (LogAdnSignUpObj.RegUser.FreeUser == false && LogAdnSignUpObj.RegUser.PromoCodeValue == null && LogAdnSignUpObj.RegUser.TryIt == false)
        //    {
        //        regUser.UserPlan = "PaidUser";

        //    }
        //    //Ends Here


        //    JsonResult ResultData = new JsonResult();
        //    if ((LogAdnSignUpObj.RegUser.PromoCodeValue != null) && (LogAdnSignUpObj.RegUser.TryIt == false))
        //    {
        //        List<PromoCode> _lstPromoCodes = null;
        //        _lstPromoCodes = new List<PromoCode>();
        //        RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
        //        _lstPromoCodes = RuserService.GetAllPromoCodes();
        //        for (int i = 0; i < _lstPromoCodes.Count; i++)
        //        {
        //            if (LogAdnSignUpObj.RegUser.PromoCodeValue == _lstPromoCodes[i].PromoCodes)
        //            {
        //                _pCodeMatch = true;
        //                regUser.ExpireOn = _lstPromoCodes[i].ExpireOn;
        //                regUser.PCodeUserGroup = _lstPromoCodes[i].CodeUser;
        //            }
        //        }
        //        //if (!_pCodeMatch)
        //        //{
        //        //    //JsonResult ResultData = new JsonResult();
        //        //    //ResultData.Data = "Promo Code Not Valid";//"Not a Valid Promo Code";           
        //        //    //return ResultData;
        //        //}
        //    }
        //    else
        //    {
        //        if ((LogAdnSignUpObj.RegUser.TryIt == true) && (LogAdnSignUpObj.RegUser.PromoCodeValue == null))
        //        {
        //            string strDate = regUser.CreatedOn.ToString();
        //            DateTime date = DateTime.Parse(strDate);
        //            string expireDate = date.AddDays(15).ToShortDateString();
        //            regUser.ExpireOn = Convert.ToDateTime(expireDate);
        //            regUser.SubscriptionType = LogAdnSignUpObj.RegUser.SubscriptionType;
        //        }
        //        else //if(LogAdnSignUpObj.RegUser.SubscriptionType == "FreeUser")
        //        {
        //            string strDate = regUser.CreatedOn.ToString();
        //            DateTime date = DateTime.Parse(strDate);
        //            string expireDate = date.AddYears(5).ToShortDateString();
        //            regUser.ExpireOn = Convert.ToDateTime(expireDate);
        //            regUser.SubscriptionType = LogAdnSignUpObj.RegUser.SubscriptionType;
        //        }


        //    }

        //    RegistrationServiceCalls RegSerCalls = null;
        //    RegSerCalls = new RegistrationServiceCalls();
        //    insertedCode = RegSerCalls.InsertRegisterUserInfo(regUser).Split(new char[] { ',' });
        //    if (insertedCode[0] != "")
        //        InsertedID = Convert.ToInt32(insertedCode[0]);
        //    SuccessCode = Convert.ToInt32(insertedCode[1]);
        //    //_log.Error("insertedid" + InsertedID);
        //    if (SuccessCode == 1010)
        //    {
        //        UserInformation usr = new UserInformation();
        //        usr.CreatedOn = System.DateTime.Now;
        //        usr.DomainId = InsertedID;
        //        usr.EmailId = regUser.EmailId;
        //        usr.FirstName = regUser.RUserName;
        //        usr.LastName = regUser.RUserName;
        //        usr.LoginName = regUser.RUserName;
        //        usr.Password = regUser.Password;
        //        usr.IsUsingModeratorCredentials = false;
        //        usr.IsModerator = true;
        //        usr.Relationship = "Moderator";
        //        usr.UserSessionID = "";
        //        Services1 s = new Services1();
        //        iHealthinsertedId = s.InsertiHealthUser(usr);

        //        PersonalInformation p = new PersonalInformation();
        //        p.FirstName = regUser.RUserName;
        //        p.DomainID = InsertedID;
        //        //_log.Error("p.UserId is : " + iHealthinsertedId);
        //        p.UserId = iHealthinsertedId;
        //        p.Country = regUser.Country;
        //        p.DateOfBirth = regUser.DOB;
        //        p.Gender = regUser.Gender;
        //        p.Contact = regUser.PhoneNumber;
        //        p.Address = regUser.Address;
        //        p.ZipCode = regUser.Pincode;
        //        // p.Updatedon = DateTime.UtcNow;

        //        Services3 s3 = new Services3();
        //        int result = s3.InsertPersonalInfo(p);
        //    }
        //    bool IsSendMail = false;
        //    //JsonResult ResultData = new JsonResult();
        //    if (iHealthinsertedId > 0)
        //    {
        //        #region Mail Sending
        //        try
        //        {
        //            MailMessage mail = new MailMessage();
        //            mail.To.Add(regUser.EmailId);
        //            mail.From = new MailAddress("medlogx@gmail.com");
        //            mail.Subject = "Registration details";

        //            string Body = "Dear " + regUser.RUserName + ",\n\n You have been successfully registered with us.\n\n Please click on below link to verify your email ID. \n\n " + "www.medlogx.com/confirmaccount/confirmaccount?userid=" + iHealthinsertedId + "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";

        //            mail.Body = Body;
        //            mail.IsBodyHtml = false;
        //            SmtpClient smtp = new SmtpClient();
        //            smtp.Host = "smtp.gmail.com";
        //            smtp.Port = 587;
        //            smtp.UseDefaultCredentials = true;
        //            smtp.Credentials = new System.Net.NetworkCredential("medlogx@gmail.com", "medlogx2013");
        //            smtp.EnableSsl = true;
        //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

        //            smtp.Send(mail);
        //            IsSendMail = true;

        //        }
        //        catch (Exception ex)
        //        {
        //            IsSendMail = false;
        //        }
        //        #endregion
        //        //    if (IsSendMail)
        //        //    {
        //        //        ResultData.Data = "Registered successfully. You will get login credentials to your mail soon..";
        //        //    }
        //        //    else
        //        //    {
        //        //        ResultData.Data = "Mail Send Failed. Try again..";
        //        //    }

        //    }
        //    //else if (SuccessCode == 1041)
        //    //{
        //    //    ResultData.Data = "This Mail ID already exists. Please create with another Mail ID";
        //    //}
        //    //else
        //    //{
        //    //    ResultData.Data = "Registration Failed. Try again..";
        //    //}
        //    //return ResultData;
        //    return RedirectToAction("UserManagement", "Administration");
        //}

        [HttpPost]
        public ActionResult Register(LoginAndSignUp LogAdnSignUpObj)
        {
            //if (ModelState.IsValid)
            //{
            int SuccessCode = 0;
            string InsertedID = string.Empty;
            string[] insertedCode;
            string iHealthinsertedId = string.Empty;
            RegisteredUser regUser = null;
            regUser = LogAdnSignUpObj.RegUser;

            JsonResult ResultData = new JsonResult();

            regUser.Password = CryptorEngine.Encrypt(regUser.Password, true);
            regUser.CreatedOn = System.DateTime.Now;

            bool _pCodeMatch = false;

            if (LogAdnSignUpObj.RegUser.TryIt == true)
            {
                regUser.UserPlan = "TrailUser";
            }
            if (LogAdnSignUpObj.RegUser.PromoCodeValue == null && LogAdnSignUpObj.RegUser.TryIt == false)
            {
                regUser.UserPlan = "PaidUser";
            }
            if ((LogAdnSignUpObj.RegUser.FreeUser == true) && (LogAdnSignUpObj.RegUser.TryIt == false))//Need to check with Promocode condition also
            {
                regUser.UserPlan = "FreeUser";
            }
            if ((LogAdnSignUpObj.RegUser.PromoCodeValue != null) && (LogAdnSignUpObj.RegUser.TryIt == false))
            {
                List<PromoCode> _lstPromoCodes = null;
                _lstPromoCodes = new List<PromoCode>();
                RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
                _lstPromoCodes = RuserService.GetAllPromoCodes();
                for (int i = 0; i < _lstPromoCodes.Count; i++)
                {
                    if ((LogAdnSignUpObj.RegUser.PromoCodeValue == _lstPromoCodes[i].PromoCodes) && (_lstPromoCodes[i].IsDeleted == false))
                    {
                        _pCodeMatch = true;
                        regUser.ExpireOn = _lstPromoCodes[i].ExpireOn;
                        regUser.SubscriptionType = _lstPromoCodes[i].CodeUser;
                        break;
                    }
                }
                if (!_pCodeMatch)
                {
                    ResultData.Data = "Promo Code Not Valid";
                    return ResultData;
                }
            }
            else
            {
                if ((LogAdnSignUpObj.RegUser.SubscriptionType == "MedLogxPro") && (LogAdnSignUpObj.RegUser.TryIt == true))
                {
                    string strDate = regUser.CreatedOn.ToString();
                    DateTime date = DateTime.Parse(strDate);
                    string expireDate = date.AddDays(15).ToShortDateString();
                    regUser.ExpireOn = Convert.ToDateTime(expireDate);
                }

            }

            _log.Error("regUser.IsGroupUser is @ UI is : " + regUser.IsGroupUser.ToString());
            RegistrationServiceCalls RegSerCalls = null;
            RegSerCalls = new RegistrationServiceCalls();
            insertedCode = RegSerCalls.InsertRegisterUserInfo(regUser).Split(new char[] { ',' });
            if (insertedCode[0] != "")
                InsertedID = insertedCode[0];
            SuccessCode = Convert.ToInt32(insertedCode[1]);
            _log.Error("insertedid" + InsertedID);
            #region insertuserinfo
            //if (SuccessCode == 1010)
            //{
            //    UserInformation usr = new UserInformation();
            //    usr.CreatedOn = System.DateTime.Now;
            //    usr.DomainId = InsertedID;
            //    usr.EmailId = regUser.EmailId; 
            //    usr.Password = regUser.Password;
            //    usr.IsUsingModeratorCredentials = false;
            //    usr.IsModerator = true;
            //    usr.Relationship = "Moderator";
            //    usr.UserSessionID = "";
            //    Services1 s = new Services1();
            //    iHealthinsertedId = s.InsertiHealthUser(usr);

            //    PersonalInformation p = new PersonalInformation();
            //    //p.FirstName = regUser.RUserName;
            //    p.DomainID = InsertedID;
            //    _log.Error("p.UserId is : " + iHealthinsertedId);
            //    p.UserId = iHealthinsertedId;
            //    p.Country = regUser.Country;
            //    p.DateOfBirth = regUser.DOB;
            //    p.Gender = regUser.Gender;
            //    p.Contact = regUser.PhoneNumber;
            //    p.Address = regUser.Address;
            //    p.ZipCode = regUser.Pincode;

            //    Services3 s3 = new Services3();
            //    int result = s3.InsertPersonalInfo(p);
            //}
            #endregion
            bool IsSendMail = false;
            if (InsertedID != null && InsertedID != " " && InsertedID != string.Empty)
            {
                #region Mail Sending
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(regUser.EmailId);
                    mail.From = new MailAddress("medlogx@gmail.com");
                    mail.Subject = "Registration details";
                    // string Body = "Dear " + regUser.FirstName + ",\n\n You have been successfully registered with us.\n\n Please click on below link to verify your email ID. \n\n " + "107.20.220.222:7814/confirmaccount/confirmaccount?userid=" +CryptorEngine.Encrypt(iHealthinsertedId,true) + "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";
                    string Body = "Dear " + regUser.FirstName + ",\n\n You have been successfully registered with us.\n\n Please click on below link to verify your email ID. \n\n " + "www.medlogx.com/confirmaccount/confirmaccount?userid=" + InsertedID + "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";        // Prashanth edited for create new user from administration


                    mail.Body = Body;
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential("medlogx@gmail.com", "medlogx2013");
                    smtp.EnableSsl = true;
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    smtp.Send(mail);
                    IsSendMail = true;

                }
                catch (Exception ex)
                {
                    IsSendMail = false;
                }
                #endregion

                if (IsSendMail)
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with MedLogx, you should have login credentials soon in your mailbox.</h3>";
                }
                else
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Please check details your entered and try again.</h3>";
                }
            }
            else if (SuccessCode == 1041)
            {
                ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Your email ID is already registered with us.</h3>";
            }
            else
            {
                ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Please check details your entered and try again.</h3>";
            }
            return ResultData;
            //}
            //return PartialView(LogAdnSignUpObj);
        }




        public ActionResult EditUser(string UserId)
        {
            if (ModelState.IsValid)
            {
                Services1 UserService = null;
                UserService = new Services1();
                string url = Request.UrlReferrer.ToString();
                TempData["PreviousPageUrl"] = url;
                UpdateUserDetails userinfo = new UpdateUserDetails();
                UserInformation usr = new UserInformation();
                usr = UserService.GetUserbyId(UserId);
                if (usr.Password != null)
                {
                    usr.Password = InnDocs.iHealth.Web.UI.Utilities.CryptorEngine.Decrypt(usr.Password, true);
                }
                RegistrationServiceCalls objServ = new RegistrationServiceCalls();
                RegisteredUser objRUser = new RegisteredUser();
                objRUser = objServ.GetRegUserbyID(usr.DomainId.ToString());
                userinfo.IHealthUser = usr;
                userinfo.RegUser = objRUser;

                return PartialView("EditUser", userinfo);
            }

            return PartialView("EditUser");
        }

        [HttpPost]
        public ActionResult EditUser(UpdateUserDetails userdet)
        {
            string ihlUsrSuccessCode = "";
            string rUsrSuccessCode = "";
            RegisteredUser regUser = new RegisteredUser();
            RegistrationServiceCalls RServ = new RegistrationServiceCalls();
            regUser = RServ.GetRegUserbyEmail(userdet.IHealthUser.EmailId);
            UserInformation usrInfo = new UserInformation();
            usrInfo = userdet.IHealthUser;
            regUser.IsGroupUser = userdet.RegUser.IsGroupUser;
            if (userdet.IHealthUser.Password != null)   // pwd change only
            {
                regUser.Password = InnDocs.iHealth.Web.UI.Utilities.CryptorEngine.Encrypt(userdet.IHealthUser.Password, true);
                ihlUsrSuccessCode = UpdateUserDetails(userdet);
            }
            else
            {
                Services1 UserService = null;
                UserService = new Services1();

                UserInformation usr = new UserInformation();
                usr = UserService.GetUserbyId(userdet.IHealthUser.UserId);
                if (usr.Password != null)
                {
                    regUser.Password = usr.Password;
                    // regUser.Password = InnDocs.iHealth.Web.UI.Utilities.CryptorEngine.Decrypt(usr.Password, true);
                    usr.Password = InnDocs.iHealth.Web.UI.Utilities.CryptorEngine.Decrypt(usr.Password, true);
                    userdet.IHealthUser.Password = usr.Password;
                    ihlUsrSuccessCode = UpdateUserDetails(userdet);
                }
            }

            Services3 s3 = new Services3();
            PersonalInformation _pinfo = new PersonalInformation();
            _pinfo = s3.GetPersonalInfo(usrInfo.UserId);



            rUsrSuccessCode = RServ.UpdateRUserDetails(regUser, regUser.RUserId);
            // return RedirectToAction("UserManagement", "Administration");
            LoginAndSignUp _loginandSignUp = null; _loginandSignUp = new LoginAndSignUp();
            UserInformation usrInfo1 = null; usrInfo1 = new UserInformation();
            RegisteredUser RUsr = null; RUsr = new RegisteredUser();
            List<UserInformation> Users = null; Users = new List<UserInformation>();
            List<RegisteredUser> RegUsers = null; RegUsers = new List<RegisteredUser>();
            Services1 UsrService = new Services1();
            RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
            //usha
            Services3 pinfoService = new Services3();
            _loginandSignUp.PInfo = pinfoService.GetPersonalInfo(userdet.IHealthUser.UserId);//usha
            //  _loginandSignUp.LstPInfo = pinfoService.GetAllActivePinfoUsers();//usha
            //end usha
            //AD         
            Users = UsrService.GetAllActiveUsers();//.GetAlliHealthUsers();

            _loginandSignUp.LstPInfo = new List<PersonalInformation>();   // chp
            Services3 PInfoService = new Services3();

            for (int i = 0; i < Users.Count; i++)
            {
                PersonalInformation value = PInfoService.GetPersonalInfoUserId(Users[i].UserId);
                _loginandSignUp.LstPInfo.Add(value);
            }

            //chp

            _loginandSignUp.IHealthUser = usrInfo1;
            _loginandSignUp.RegUser = RUsr;
            _loginandSignUp.Users = Users;
            //return PartialView("UserManagement", _loginandSignUp);

            bool IsSendMail = false;
            RegistrationServiceCalls RegSerCalls = null;
            RegSerCalls = new RegistrationServiceCalls();
            JsonResult ResultData = new JsonResult();

            if (Convert.ToInt32(ihlUsrSuccessCode) == 1020 && Convert.ToInt32(rUsrSuccessCode) == 1020)
            {
                #region Mail Sending
                try
                {
                    string Body = "Dear" + usrInfo.FirstName + ",\n\n Your account Password is edited by an administrator of Bump Docs.\n \n Please contact Bump Docs administrator. \n\n ";
                    SendMail _sendMail = new SendMail();

                    _sendMail.To = usrInfo.EmailId;

                    _sendMail.Body = Body;
                    _sendMail.Subject = "Bump Docs User account details";
                    IsSendMail = RegSerCalls.SendMail(_sendMail);
                }
                catch (Exception ex)
                {
                    IsSendMail = false;
                }
                #endregion
                if (IsSendMail)
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>User account Password is edited by an administrator, Please check your mail to know more information.</h3>";
                }
                else
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, mail sending fails</h3>";
                }
            }


            return ResultData;

        }

        [HttpPost]
        public ActionResult DelSusRevkUser(string id, string Res)
        {
            // string SuccessCode = "";
            UserInformation userinfo = null;
            userinfo = new UserInformation();
            Services1 UserService = null;
            UserService = new Services1();
            userinfo = UserService.GetUserbyId(id);
            UserInformation LoginUser = null;
            string currentpage = "Administration";
            Session["CurrentPage"] = currentpage;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            userinfo.ModifiedBy = LoginUser.UserId;
            userinfo.ModifiedOn = DateTime.Now;
            //userinfo.IsDeleted = true;
            if (Res == "Suspend")
            {
                userinfo.IsSuspend = true;
            }
            else if (Res == "Revoke")
            {
                userinfo.IsSuspend = false;
            }
            else
            {
                userinfo.IsDeleted = true;
            }
            Services1 usrServ = new Services1();

            string Sucesscode = usrServ.UpdateUserPasswordDetails(userinfo, id);

            bool IsSendMail = false;
            RegistrationServiceCalls RegSerCalls = null;
            RegSerCalls = new RegistrationServiceCalls();
            JsonResult ResultData = new JsonResult();

            if (Convert.ToInt32(Sucesscode) == 1020 && Res == "Suspend")
            {
                #region Mail Sending
                try
                {
                    string Body = "Dear" + userinfo.FirstName + ",\n\n Your account is Suspended by an administrator of Bump Docs.\n\n Please contact Bump Docs administrator. \n\n ";
                    SendMail _sendMail = new SendMail();

                    _sendMail.To = userinfo.EmailId;

                    _sendMail.Body = Body;
                    _sendMail.Subject = "Bump Docs User account details";
                    IsSendMail = RegSerCalls.SendMail(_sendMail);
                }
                catch (Exception ex)
                {
                    IsSendMail = false;
                }
                #endregion
                if (IsSendMail)
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>User account is suspented by an administrator, Please check your mail to know more information.</h3>";
                }
                else
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, mail sending fails</h3>";
                }
            }
            else if (Convert.ToInt32(Sucesscode) == 1020 && Res == "Revoke")
            {
                #region Mail Sending
                try
                {
                    string Body = "Dear" + userinfo.FirstName + ",\n\n Your account is Revoked by an administrator of Bump Docs.\n\n ";
                    SendMail _sendMail = new SendMail();

                    _sendMail.To = userinfo.EmailId;

                    _sendMail.Body = Body;
                    _sendMail.Subject = "Bump Docs User account details";
                    IsSendMail = RegSerCalls.SendMail(_sendMail);
                }
                catch (Exception ex)
                {
                    IsSendMail = false;
                }
                #endregion
                if (IsSendMail)
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>User account is revoked by an administrator, Please check your mail to know more information.</h3>";
                }
                else
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, mail sending fails</h3>";
                }
            }

            return ResultData;
        }
        //AD
        public ActionResult UserAdministration()
        {
            var list = new SelectList(new[]
                                          {
                                               new {ID="0",Name="Please Select"},
                                              new {ID="1",Name="Audit Reports"},
                                              new{ID="2",Name="Manage Users"},
                                              new{ID="3",Name="Manage PromoCodes"},
                                              
                                          },
                          "ID", "Name");
            ViewData["list"] = list;
            ViewData["list"] = list;
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            return View();//("AllAuditReports", _loginandsignup);
        }
        public ActionResult UserSelected(string selectedItem)
        {

            if (selectedItem == "Manage Users")
            {
                LoginAndSignUp _loginandSignUp = null;
                _loginandSignUp = new LoginAndSignUp();

                List<UserInformation> Users = null;
                Users = new List<UserInformation>();
                Services1 UsrService = new Services1();
                Services3 PInfoService = new Services3();
                Users = UsrService.GetAllActiveUsers();
                _loginandSignUp.LstPInfo = new List<PersonalInformation>();

                for (int i = 0; i < Users.Count; i++)        // added by prashanth
                {
                    PersonalInformation value = PInfoService.GetPersonalInfoUserId(Users[i].UserId);
                    _loginandSignUp.LstPInfo.Add(value);
                }

                //  _loginandSignUp.LstPInfo = PInfoService.GetAllActivePinfoUsers();
                // _loginandSignUp.LstPInfo = PInfoService.GetAllUsersbyDomainID(LoginUser.DomainId);
                _loginandSignUp.Users = Users;

                return PartialView("UserManagement", _loginandSignUp);
            }
            else if (selectedItem == "Manage PromoCodes")
            {
                LoginAndSignUp _loginandSignUp = null; _loginandSignUp = new LoginAndSignUp();
                PromoCode objPromoCodes = null; objPromoCodes = new PromoCode();
                List<PromoCode> _lstPromoCodes = null;
                _lstPromoCodes = new List<PromoCode>();
                Services1 UsrService = new Services1();
                RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
                _lstPromoCodes = RuserService.GetAllPromocodesByIsDeleted("0");
                _loginandSignUp.ListPromoCodes = _lstPromoCodes;
                return PartialView("PCode", _loginandSignUp);

            }
            else
            {
                UserInformation usr = null;
                usr = new UserInformation();
                List<UserInformation> Users = null;
                Users = new List<UserInformation>();
                Services1 UsrService = new Services1();
                LoginAndSignUp _loginandsignup = null;
                _loginandsignup = new LoginAndSignUp();
                GetAuditReports gar = new GetAuditReports();
                List<AuditHistory> adthistory = null;
                adthistory = new List<AuditHistory>();
                AuditHistory ah = new AuditHistory();
                ah.frmDate = DateTime.Today.AddDays(-1);
                ah.toDate = DateTime.Now;

                adthistory = GetAuditReports.GetAllAuditReportsByDate(ah);//.GetAllAuditReports();
                ViewBag.Userslist = UsrService.GetAllUserslist();
                _loginandsignup.Audits = adthistory;


                return PartialView("AllAuditReports", _loginandsignup);
            }

        }
        public ActionResult CreatePromoCode()
        {
            return PartialView("CreatePromoCode");
        }

        [HttpPost]
        public ActionResult SubmitPCode(PromoCode PCodeInfo)
        {
            if (ModelState.IsValid)
            {
                string insertedCode = "";
                PromoCode objPCodeInfo = new PromoCode();

                objPCodeInfo.PromoCodes = PCodeInfo.PromoCodes;
                objPCodeInfo.CodeUser = PCodeInfo.CodeUser;
                objPCodeInfo.CreatedOn = DateTime.Now;
                objPCodeInfo.ExpireOn = PCodeInfo.ExpireOn;//DateTime.Now.AddYears(1);
                RegistrationServiceCalls RegSerCalls = null;
                RegSerCalls = new RegistrationServiceCalls();
                LoginAndSignUp _loginandSignUp = null; _loginandSignUp = new LoginAndSignUp();
                PromoCode objPromoCodes = null; objPromoCodes = new PromoCode();
                List<PromoCode> _lstPromoCodes = null;
                _lstPromoCodes = new List<PromoCode>();
                Services1 UsrService = new Services1();
                RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
                _lstPromoCodes = RuserService.GetAllPromocodesByIsDeleted("0");
                bool flag = false;
                for (int i = 0; i < _lstPromoCodes.Count; i++)
                {
                    if (_lstPromoCodes[i].PromoCodes == objPCodeInfo.PromoCodes)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    insertedCode = RegSerCalls.InsertPCodeInfo(objPCodeInfo);//.Split(new char[] { ',' });  
                    _lstPromoCodes.Add(objPCodeInfo);
                }
                else
                {

                }

                _loginandSignUp.ListPromoCodes = _lstPromoCodes;
                return PartialView("PCode", _loginandSignUp);
            }
            return PartialView("CreatePromoCode");
        }
        public ActionResult EditPromoCode(string pCodeId)
        {
            RegistrationServiceCalls objServ = new RegistrationServiceCalls();
            PromoCode objPCodeInfo = new PromoCode();

            objPCodeInfo = objServ.GetPCodeInfoById(pCodeId);


            return PartialView("EditPromoCode", objPCodeInfo);
        }
        [HttpPost]
        public ActionResult SubmitEditedPCode(PromoCode PCodeInfo)
        {
            if (ModelState.IsValid)
            {
                RegistrationServiceCalls objServ = new RegistrationServiceCalls();
                PCodeInfo.CreatedOn = DateTime.Now;
                int Result = objServ.UpdatePromoCodeInfo(PCodeInfo);
                LoginAndSignUp _loginandSignUp = null; _loginandSignUp = new LoginAndSignUp();
                PromoCode objPromoCodes = null; objPromoCodes = new PromoCode();
                List<PromoCode> _lstPromoCodes = null;
                _lstPromoCodes = new List<PromoCode>();
                Services1 UsrService = new Services1();
                RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
                _lstPromoCodes = RuserService.GetAllPromocodesByIsDeleted("0");
                _loginandSignUp.ListPromoCodes = _lstPromoCodes;
                return PartialView("PCode", _loginandSignUp);
            }
            return PartialView("SubmitEditedPCode");
        }

        public ActionResult DeletePCode(string id)
        {
            RegistrationServiceCalls objServCalls = new RegistrationServiceCalls();
            string Result = objServCalls.DeletePromoCode(id);
            LoginAndSignUp _loginandSignUp = null; _loginandSignUp = new LoginAndSignUp();
            PromoCode objPromoCodes = null; objPromoCodes = new PromoCode();
            List<PromoCode> _lstPromoCodes = null;
            _lstPromoCodes = new List<PromoCode>();
            Services1 UsrService = new Services1();
            RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
            _lstPromoCodes = RuserService.GetAllPromocodesByIsDeleted("0");
            _loginandSignUp.ListPromoCodes = _lstPromoCodes;
            return PartialView("PCode", _loginandSignUp);
        }
        public ActionResult ArchivePromoCode(string id) // PRASHANTH
        {
            // RegistrationServiceCalls objServCalls = new RegistrationServiceCalls();
            //string Result = objServCalls.DeletePromoCode(id);
            LoginAndSignUp _loginandSignUp = null; _loginandSignUp = new LoginAndSignUp();
            PromoCode objPromoCodes = null; objPromoCodes = new PromoCode();
            List<PromoCode> _lstPromoCodes = null;
            _lstPromoCodes = new List<PromoCode>();
            Services1 UsrService = new Services1();
            RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
            _lstPromoCodes = RuserService.GetAllPromocodesByIsDeleted("1");
            _loginandSignUp.ListPromoCodes = _lstPromoCodes;
            return PartialView("PCode", _loginandSignUp);
        }

        public ActionResult GetUsersByEmail(string EmailId)
        {

            LoginAndSignUp _loginandSignUp = null;
            _loginandSignUp = new LoginAndSignUp();
            UserInformation usr = null;
            usr = new UserInformation();
            List<UserInformation> Users = null;
            Users = new List<UserInformation>();
            Services1 UsrService = new Services1();
            Services3 PinfoService = new Services3();
            PersonalInformation pinfo = null;
            IList<PersonalInformation> lstpifo = null;
            lstpifo = new List<PersonalInformation>();
            //AD 
            usr = UsrService.GetUserByEmail(EmailId);//.GetAlliHealthUsers();
            if (usr.UserId != null && usr.UserId != "" && usr.UserId != string.Empty)
            {
                pinfo = new PersonalInformation();
                pinfo = PinfoService.GetPersonalInfo(usr.UserId);
            }
            if (usr != null)
            {
                Users.Add(usr);
            }
            if (pinfo != null)
            {
                lstpifo.Add(pinfo);
            }

            _loginandSignUp.LstPInfo = lstpifo;
            _loginandSignUp.IHealthUser = usr;
            _loginandSignUp.Users = Users;
            return PartialView("UserManagement", _loginandSignUp);

        }
        public ActionResult UserReport(string hSelectedId)
        {
            LoginAndSignUp _loginandsignup = null;
            _loginandsignup = new LoginAndSignUp();
            Services1 UsrService = new Services1();
            GetAuditReports gar = new GetAuditReports();
            List<AuditHistory> adthistory = null;
            adthistory = new List<AuditHistory>();

            adthistory = GetAuditReports.GetAllAuditReportsByUserId(hSelectedId);
            ViewBag.Userslist = UsrService.GetAllUserslist();
            _loginandsignup.Audits = adthistory;
            return PartialView("AllAuditReports", _loginandsignup);
        }


        public ActionResult DateWiseReport(LoginAndSignUp las)
        {
            AuditHistory ah = null;
            ah = new AuditHistory();
            ah.frmDate = las.AHistory.frmDate;
            ah.toDate = las.AHistory.toDate;
            LoginAndSignUp _loginandsignup = null;
            _loginandsignup = new LoginAndSignUp();
            Services1 UsrService = new Services1();
            GetAuditReports gar = new GetAuditReports();
            List<AuditHistory> adthistory = null;
            adthistory = new List<AuditHistory>();
            adthistory = GetAuditReports.GetAllAuditReportsByDate(ah);
            ViewBag.Userslist = UsrService.GetAllUserslist();
            _loginandsignup.Audits = adthistory;
            return PartialView("AllAuditReports", _loginandsignup);

        }

        public ActionResult Ulist(string term, string name)
        {
            string path = string.Empty;
            Services1 UsrService = new Services1();
            Services3 pinfoservice = new Services3();
            List<UserInformation> lstUsers = UsrService.GetAllActiveUsers();
            //List<PersonalInformation> lstpusers = pinfoservice.GetAllActivePinfoUsers();

            // start-prashanth added for intelligence
            List<PersonalInformation> lstpusers = null;
            lstpusers = new List<PersonalInformation>();
            PersonalInformation perinfo = null;

            //pinfoservice.GetAllActivePinfoUsers();
            foreach (UserInformation p in lstUsers)
            {
                perinfo = new PersonalInformation();
                perinfo = pinfoservice.GetPersonalInfoByUserIdAndGroupType(p.UserId, "Family or Individual");
                lstpusers.Add(perinfo);
                perinfo = null;
            }

            // end-prashanth added for intelligence

            List<string> Uname = new List<string>();
            foreach (var user in lstUsers)
            {
                foreach (var puser in lstpusers)
                {
                    if (user.UserId == puser.UserId)
                    {
                        Uname.Add(puser.FirstName + "," + user.EmailId);
                    }
                }
            }
            if (term != null)
            {
                var sel_items = Uname.Where(x => x.StartsWith(term, true, CultureInfo.CurrentCulture)).OrderBy(x => x).Select(x => x).Distinct();
                return Json(sel_items, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Null");
            }
        }
        public ActionResult Audits(string namr)
        {
            string[] Una = namr.Split(',');
            string Uname = Una[0].ToString();
            string Emailid = Una[1];
            UserInformation lstuserinfo = new UserInformation();
            LoginAndSignUp _loginandsignup = null;
            _loginandsignup = new LoginAndSignUp();
            Services1 UsrService = new Services1();
            lstuserinfo = UsrService.GetUserByNamenEmail(Uname, Emailid);
            GetAuditReports gar = new GetAuditReports();
            List<AuditHistory> adthistory = null;
            adthistory = new List<AuditHistory>();
            //foreach (UserInformation userinfo in lstuserinfo)
            //{
            adthistory = GetAuditReports.GetAllAuditReportsByUserId(lstuserinfo.UserId);
            //}
            ViewBag.Userslist = UsrService.GetAllUserslist();
            _loginandsignup.Audits = adthistory;
            return PartialView("AllAuditReports", _loginandsignup);
        }

        public ActionResult UserAudits(string namr)
        {
            string[] Una = namr.Split(',');
            string Uname = Una[0].ToString();
            string Emailid = Una[1];
            UserInformation lstuserinfo = new UserInformation();
            LoginAndSignUp _loginandsignup = null;
            _loginandsignup = new LoginAndSignUp();
            Services1 UsrService = new Services1();
            lstuserinfo = UsrService.GetUserByNamenEmail(Uname, Emailid);
            GetAuditReports gar = new GetAuditReports();
            List<AuditHistory> adthistory = null;
            adthistory = new List<AuditHistory>();
            //foreach (UserInformation userinfo in lstuserinfo)
            //{
            adthistory = GetAuditReports.GetAllAuditReportsByUserId(lstuserinfo.UserId);
            //}
            ViewBag.Userslist = UsrService.GetAllUserslist();
            _loginandsignup.Audits = adthistory;
            return PartialView("AllAuditReports", _loginandsignup);
        }

        private string UpdateUserDetails(UpdateUserDetails NewUserDetails)
        {
            UserInformation userinfo = null;
            userinfo = new UserInformation();
            Services1 UserService = null;
            UserService = new Services1();
            userinfo = UserService.GetUserbyId(NewUserDetails.IHealthUser.UserId);
            RegisteredUser RegUser = new RegisteredUser();
            RegistrationServiceCalls regService = new RegistrationServiceCalls();
            RegUser = regService.GetRegUserbyID(userinfo.DomainId.ToString());
            userinfo.ModifiedOn = System.DateTime.Now;
            userinfo.Relationship = NewUserDetails.IHealthUser.Relationship;
            if (NewUserDetails.IHealthUser.IsUsingModeratorCredentials != true)
            {
                if (userinfo.IsUsingModeratorCredentials == true && NewUserDetails.IHealthUser.IsUsingModeratorCredentials == false)
                {
                    if (userinfo.EmailId != NewUserDetails.IHealthUser.EmailId)
                    {
                        userinfo.EmailId = NewUserDetails.IHealthUser.EmailId;
                        userinfo.Password = InnDocs.iHealth.Web.UI.Utilities.CryptorEngine.Encrypt(NewUserDetails.IHealthUser.Password, true);
                    }

                }
                else
                {
                    userinfo.EmailId = NewUserDetails.IHealthUser.EmailId;
                    userinfo.Password = InnDocs.iHealth.Web.UI.Utilities.CryptorEngine.Encrypt(NewUserDetails.IHealthUser.Password, true);
                }
            }
            else
            {
                userinfo.EmailId = RegUser.EmailId;
                userinfo.Password = RegUser.Password;
            }
            userinfo.IsUsingModeratorCredentials = NewUserDetails.IHealthUser.IsUsingModeratorCredentials;
            // string userupdate = UserService.UpdateUserDetails(userinfo, userinfo.UserId); 
            string userupdate = UserService.UpdateUserPasswordDetails(userinfo, userinfo.UserId);   // added by prashanth

            return userupdate;

        }

        public ActionResult PaymentForm(string id)
        {
            ViewBag.id = id;
            return PartialView("PaymentForm");
        }
        [HttpPost]
        public ActionResult submitPayment(Payments PaymentsInfo)
        {

            if (ModelState.IsValid)
            {


                string insertedCode = "";
                string updatedCode = "";
                string UserId = PaymentsInfo.UserId;

                RegistrationServiceCalls RuserService = new RegistrationServiceCalls();

                insertedCode = RuserService.InsertPaymentInfo(PaymentsInfo);
                RegisteredUser regInfo = new RegisteredUser();
                regInfo = RuserService.GetRegUserbyID(PaymentsInfo.UserId);
                regInfo.ExpireOn = PaymentsInfo.Validity;
                updatedCode = RuserService.UpdateRUserDetails(regInfo, UserId);

                JsonResult res = new JsonResult();
                if (insertedCode == "1010" && updatedCode == "1020")
                {
                    res.Data = "Payment details saved successfully";
                    return Json(res.Data, JsonRequestBehavior.AllowGet);// res;
                    //  return res;
                }

                else
                {
                    res.Data = "Error occured in Submitting";
                    return res;
                }

            }
            return PartialView("PaymentForm");

        }

        public ActionResult AuditHistory()
        {
            RegistrationServiceCalls RegService = new RegistrationServiceCalls();
            UserInformation usr = null;
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }

            IList<SingleRegisterModel> _regUsers = RegService.GetAllRegUsers().OrderByDescending(x => x.CreatedOn).ToList();
            
            return View(_regUsers);
        }

        public ActionResult GetActiveUsers()
        {
            IList<UserInformation> Users = null; Users = new List<UserInformation>();
          
            Services1 userService = new Services1();
         
            UserInformation usr = null;
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
           Users = userService.GetAllActiveUsers().OrderByDescending(x => x.CreatedOn).ToList(); ;//.GetAlliHealthUsers();
           // Users = userService.GetAllActiveUsersVV().OrderByDescending(x => x.CreatedOn).ToList(); ;//.GetAlliHealthUsers();
            return View(Users);
        }



    }
}
