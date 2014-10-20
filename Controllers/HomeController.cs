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
  
    public class HomeController : Controller
    {

        private static ILog _log = LogManager.GetLogger("Web.UI");

        [OutputCache(Duration = 1800, VaryByParam = "*")]
        public ActionResult Home()
        {
            //if (Request.IsAuthenticated)
            //{
            //    return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
            //}
            return View("NewHome", new UserInformation());
        }

        public ActionResult Signup()
        {
            return PartialView();
        }

        public ActionResult Confirm_OTAcode(ota o)
        {
            Session["CRegUid"] = o.userid;

            ViewBag.ErrorMessage = "You are registered with BumpDocs, Please check your email to confirm your registration by confirmation code.";
            ViewBag.UserId = o.userid;
            return View("Confirm_OTAcode", masterName: "Common_Layout");

        }

        [HttpPost]
        public ActionResult Confirm_OTAcode1(ota o)
        {
            RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            SingleRegisterModel singleuserObj = reguser.GetSingleRegUserbyID(o.userid);
            DateTime expire = Convert.ToDateTime(singleuserObj.OTAExpire);
            DateTime presenttime = DateTime.Now;
            if (presenttime <= expire)
            {
                if (singleuserObj.OTACode == o.otacode)
                {
                    Session["RUserid"] = singleuserObj.RUserId;
                    return RedirectToAction("ConfirmAccount", "ConfirmAccount");
                }
                else if (o.otacode == "" || o.otacode == null)
                {
                    ViewBag.UserId = o.userid;
                    ViewBag.ErrorMessage = "Please enter confirmation code.";

                }
                else
                {
                    ViewBag.UserId = o.userid;
                    ViewBag.ErrorMessage = "Invalid confirmation code.";
                }
            }
            return View("Confirm_OTAcode", masterName: "Common_Layout");
        }

        public ActionResult Signup1()
        {
            return View("common", masterName: "Common_Layout");
        }

        [HttpPost]
        public ActionResult Signup(SingleRegisterModel singleuserObj)
        {
            ota o = new ota();
            string SuccessCode = "";
            string domainId = string.Empty;
            string[] insertedCode;
            string PhnSuccessCode = "";

            JsonResult ResultData = new JsonResult();

            singleuserObj.CreatedOn = System.DateTime.Now;
            singleuserObj.GroupType = "Family or Individual";
            string IsSendMail = string.Empty;
            string IsSendMessage = string.Empty;
            string ERUID = string.Empty;


            if (singleuserObj.EmailId.Contains('@'))
            {
                singleuserObj.PhoneNumber = null;
            }
            else
            {
                singleuserObj.PhoneNumber = Convert.ToInt64(singleuserObj.EmailId);
                singleuserObj.EmailId = null;
            }
            /*vvv*/
            //For local
            //string strHostName = "";
            //strHostName = System.Net.Dns.GetHostName();

            //IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

            //IPAddress[] addr = ipEntry.AddressList;
            //singleuserObj.IpAddress = addr[addr.Length - 2].ToString();
            //For DC
           singleuserObj.IpAddress= System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.UserHostAddress;
            /*vvv*/
            RegistrationServiceCalls RegSerCalls = null;
            RegSerCalls = new RegistrationServiceCalls();
            try
            {
                insertedCode = RegSerCalls.InsertRegisterUserInfo(singleuserObj).Split(new char[] { ',' }); ;
                o.userid = insertedCode[0];
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
                    if (insertedCode[3] != "")
                    {
                        IsSendMail = insertedCode[3];
                    }
                    if (insertedCode[4] != "")
                    {
                        IsSendMessage = insertedCode[4];
                    }
                    if (insertedCode[5] != "")
                    {
                        ERUID = insertedCode[5];
                    }
                }
            }
            catch (Exception ex)
            {
                SuccessCode = "1011";
                throw ex;
            }

            if (Convert.ToInt32(SuccessCode) == 1010)
            {

                if (Convert.ToInt32(IsSendMail) == 1080)
                {
                    ViewBag.ErrorMessage = "You are registered successfully with BumpDocs, Please check your email to confirm your registration by confirmation code.";
                    ViewBag.UserId = insertedCode[0];
                    return View("Confirm_OTAcode", masterName: "Common_Layout");
                }
                else if (Convert.ToInt32(IsSendMail) == 1081)
                {
                    ViewBag.ErrorMessage = "We are sorry, something's wrong, please contact our BumpDocs administrator.";
                    return View("common", masterName: "Common_Layout");

                }
                else
                {
                    ViewBag.ErrorMessage = "We are sorry ! Could not register you. Check the details you have entered and try again.";
                    return View("common", masterName: "Common_Layout");
                }
            }
            else if (Convert.ToInt32(SuccessCode) == 1041)
            {
                if (Convert.ToInt32(IsSendMail) == 1080)
                {
                    ViewBag.ErrorMessage = "You are already a member! Please check your email to confirm your registration.";
                    ViewBag.UserId = ERUID;
                    return View("Confirm_OTAcode", masterName: "Common_Layout");
                }

                ViewBag.ErrorMessage = "You are already a member! Please Login into your account.";
                return View("common", masterName: "Common_Layout");

            }
            else if (Convert.ToInt32(PhnSuccessCode) == 1051)
            {
                if (Convert.ToInt32(IsSendMessage) == 1090)
                {
                    ViewBag.ErrorMessage = "You are already a member! Please check your mobile to confirm your registration.";
                    ViewBag.UserId = ERUID;
                    return View("Confirm_OTAcode", masterName: "Common_Layout");
                }
                ViewBag.ErrorMessage = "You are already a member! Please Login into your account.";
                return View("common", masterName: "Common_Layout");
            }
            else if (Convert.ToInt32(PhnSuccessCode) == 1050)
            {
                if (Convert.ToInt32(IsSendMessage) == 1090)
                {
                    ViewBag.ErrorMessage = "Sucessfully registered with BumpDocs, Please confirm with confirmation code sent to your phone.";
                    ViewBag.UserId = insertedCode[0];
                    return View("Confirm_OTAcode", masterName: "Common_Layout");
                }
                else
                {
                    ViewBag.ErrorMessage = "Please check details your entered and try again.";
                    return View("common", masterName: "Common_Layout");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please check details your entered and try again.";
                return View("common", masterName: "Common_Layout");
            }
        }

        public ActionResult Select()
        {
            return PartialView(new UserInformation());
        }

        public ActionResult SingleRegister(string value1, string value2)
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

            return PartialView("SingleRegister", regUser);
        }

        [HttpPost]
        public ActionResult SingleRegister(SingleRegisterModel singleuserObj)
        {


            string SuccessCode = "";
            string domainId = string.Empty;
            string[] insertedCode;
            string PhnSuccessCode = "";

            JsonResult ResultData = new JsonResult();

            singleuserObj.CreatedOn = System.DateTime.Now;

            string IsSendMail = string.Empty;
            string IsSendMessage = string.Empty;
            singleuserObj.GroupType = "Family or Individual";

            if (singleuserObj.EmailId.Contains('@'))//ck added
            {
                singleuserObj.PhoneNumber = null;


            }
            else
            {
                singleuserObj.PhoneNumber = Convert.ToInt64(singleuserObj.EmailId);
                singleuserObj.EmailId = null;
            }
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
                    if (insertedCode[3] != "")
                    {
                        IsSendMail = insertedCode[3];
                    }
                    if (insertedCode[4] != "")
                    {
                        IsSendMessage = insertedCode[4];
                    }
                }
            }
            catch (Exception ex)
            {
                SuccessCode = "1011";
                throw ex;
            }

            if (Convert.ToInt32(SuccessCode) == 1010)
            {

                if (Convert.ToInt32(IsSendMail) == 1080)
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with BumpDocs, Please check your mail to confirm your registration.</h3>";

                }
                else if (Convert.ToInt32(IsSendMail) == 1081)
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, mail sending failed, please contact BumpDocs administrator.</h3>";


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
            else if (Convert.ToInt32(SuccessCode) == 1051)
            {
                ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Your phone number is already registered with us.</h3>";
            }
            else if (Convert.ToInt32(PhnSuccessCode) == 1050)
            {
                if (Convert.ToInt32(IsSendMessage) == 1090)
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with BumpDocs, Please check your mobile number to confirm your registration.</h3>";
                }
                else
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Please check details your entered and try again.</h3>";
                }
            }
            else
            {
                ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Please check details your entered and try again.</h3>";

            }
            return ResultData;

        }

        public ActionResult HospitalRegister(string value1, string value2)
        {
            ViewData["GroupType"] = value1;
            ViewData["IsGroupUser"] = value2;
            HospitalRegisterModel regUser = new HospitalRegisterModel();
            regUser.GroupType = value1;

            // regUser.IsGroupUser = value2;


            return PartialView("HospitalRegister", regUser);
        }

        [HttpPost]
        public ActionResult HospitalRegister(HospitalRegisterModel singleuserObj)
        {
            if (ModelState.IsValid)
            {

                string SuccessCode = "";
                string domainId = "";
                string PhnSuccessCode = "";
                string[] insertedCode;
                HospitalInformation1 hospInfo = new HospitalInformation1();
                JsonResult ResultData = new JsonResult();

                singleuserObj.CreatedOn = System.DateTime.Now;
                //bool _pCodeMatch = false;




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
                    //singleuserObj.OTAExpire = otaexpires;

                    string strDate = singleuserObj.CreatedOn.ToString();
                    DateTime date = DateTime.Parse(strDate);
                    DateTime expireDate = date.AddHours(24);
                    singleuserObj.OTAExpire = Convert.ToDateTime(expireDate);

                    singleuserObj.PhoneNumber = Convert.ToInt64(singleuserObj.EmailId);
                    singleuserObj.EmailId = null;

                }


                singleuserObj.Password = CryptorEngine.Encrypt(singleuserObj.Password, true);
                RegistrationServiceCalls RegSerCalls = null;
                RegSerCalls = new RegistrationServiceCalls();
                try
                {
                    insertedCode = RegSerCalls.InsertRegisterUserInfo(singleuserObj).Split(new char[] { ',' });

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
                        //if (Convert.ToInt32(SuccessCode) == 1010)
                        //{
                        //    string strDate = DateTime.Now.ToString("ddMMyyyyhhmm");
                        //    hospInfo.HospitalName = singleuserObj.Hospitalname;
                        //    hospInfo.MainBranch = singleuserObj.MainBranch;
                        //    hospInfo.Address = singleuserObj.Address;
                        //    hospInfo.HospitalID = "H" + strDate;
                        //    hospInfo.DomainId = domainId;



                        //    SuccessCode = HospitalServiceCals.InsertHospitalInfo(hospInfo);
                        //}
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
                        //                        MailMessage mail = new MailMessage();
                        //                        mail.To.Add(singleuserObj.EmailId);
                        //                        mail.From = new MailAddress("medlogx@gmail.com");
                        //                        mail.Subject = "Registration details";
                        //                        // string Body = "Dear " + singleuserObj.RUserName + ",\n\n You have been successfully registered with us.\n\n Please click on below link to verify your email ID. \n\n " + "107.20.220.222:6845/confirmaccount/confirmaccount?userid=" + domainId
                        //                        // + "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";
                        //                        string Body = "Dear " + singleuserObj.RUserName + ",\n\n You have been successfully registered with us. \n\n Please click on below link to verify your email ID. \n\n " + "http://www.bumpdocs.com:7814/confirmaccount/ConfirmHospRegAccount?userid=" + domainId
                        //+ "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";
                        //                        mail.Body = Body;
                        //                        mail.IsBodyHtml = false;
                        //                        SmtpClient smtp = new SmtpClient();
                        //                        smtp.Host = "smtp.gmail.com";
                        //                        smtp.Port = 587;
                        //                        smtp.UseDefaultCredentials = true;
                        //                        smtp.Credentials = new System.Net.NetworkCredential("medlogx@gmail.com", "medlogx2013");
                        //                        smtp.EnableSsl = true;
                        //                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                        //                        smtp.Send(mail);
                        //                        IsSendMail = true;

                        string Body = "Dear " + singleuserObj.RUserName + ",\n\n You have been successfully registered with us. \n\n Please click on below link to verify your email ID. \n\n " + "http://www.medlogx.com/confirmaccount/ConfirmHospRegAccount?userid=" + domainId
                        + "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";

                        SendMail _sendMail = new SendMail();

                        _sendMail.To = singleuserObj.EmailId;

                        _sendMail.Body = Body;
                        _sendMail.Subject = "Medlogx Registration details";
                        IsSendMail = RegSerCalls.SendMail(_sendMail);

                    }
                    catch (Exception ex)
                    {
                        IsSendMail = false;
                    }
                    #endregion

                    if (IsSendMail)
                    {
                        ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with MedLogx, Please check your mail to confirm your registration.</h3>";
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
                else
                {
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Please check details your entered and try again.</h3>";
                }
                return ResultData;
            }
            return PartialView(singleuserObj);

        }

        [HttpPost]
        [Analytics(Medium = "BumpDocs Login")]
        public ActionResult LoginNew(UserInformation usr)
        {
            usr.GroupType = "Patient";
            if (ModelState.IsValid)//Added by SMitra
            {
                Services1 UsrService = new Services1();
                Services3 PIfoservice = new Services3();
                if (usr.HospitalID == null)
                {
                    usr.HospitalID = "some";
                }
                HttpContext.Session["TUN"] = usr.EmailId;
                UserInformation resultUser = UsrService.ValidateUserNew(usr.EmailId, usr.Password, usr.GroupType, usr.HospitalID);
                SingleRegisterModel DomainUser = new SingleRegisterModel();
                RegistrationServiceCalls RegService = new RegistrationServiceCalls();
                if (resultUser.ErrorCode == "9150")
                {
                    if (resultUser.IsSendMail == 1080)
                    {

                        ViewBag.UserId = resultUser.DomainId;


                        ViewBag.ErrorMessage = "Your Confirmation code is successfully sent,please check your inbox/mobile";
                        return View("Confirm_OTAcode", masterName: "Common_Layout");
                    }
                    else if (resultUser.IsSendMail == 1081)
                    {
                        ViewBag.UserId = resultUser.DomainId;
                        ViewBag.ErrorMessage = "Problem in sending mail.please try again";
                        return View("Confirm_OTAcode", masterName: "Common_Layout");
                    }
                    else
                    {
                        // sandeep added new code start here on 4-3-2014

                        DoulaUser _doulauser = UsrService.AuthenticateDoulaUser(usr.EmailId, usr.Password);
                        if (_doulauser.ErrorCode == "0")
                        {
                            if (_doulauser != null)
                            {
                                Session["DoulaUser"] = _doulauser;
                                IList<DoulaiHealthUser> _getUsersbyDoula = UsrService.GetUsersByDoulaUser(_doulauser.DoulaUserId);

                                DoulaiHealthUser doula = new DoulaiHealthUser();
                                doula = _getUsersbyDoula.OrderBy(x => x.CreatedOn).FirstOrDefault();

                                // DoulaiHealthUser doula = UsrService.GetDoulaHealthUserbyDoulaId(_doulauser.DoulaUserId);
                                UserInformation userinfo = UsrService.GetUserbyId(doula.iHealthUserId);
                                Session["UsersList"] = UsrService.UserslistByDoula(_doulauser.DoulaUserId, userinfo.UserId);
                                Session["LoginUser"] = userinfo;
                                Session["DoulaLoginName"] = doula.FirstName;
                                Session["LoginName"] = userinfo.LoginName;
                                FormsAuthentication.SetAuthCookie(usr.EmailId, true);

                                return RedirectToAction("Index", "DoulaBump", new { Area = "DoulaBumpUser" });
                            }
                        }
                        else if (_doulauser.ErrorCode == "-1")
                        {
                            ModelState.AddModelError("EmailId", "Invalid credentials");
                            ViewBag.Password = "Invalid credentials.";
                            return View("NewHome", usr);
                        }
                        else if (_doulauser.ErrorCode == "-2")
                        {
                            ModelState.AddModelError("HospitalID", "Invalid HospitalId");
                            ViewBag.Password = "Invalid Credentials.";
                            return View("NewHome", usr);
                        }
                        // sandeep added new code end here on 4-3-2014
                        else
                        {
                            ModelState.AddModelError("EmailId", "Invalid user account");
                            ViewBag.Password = "Invalid user account.";
                            return View("NewHome", usr);
                        }
                        //ModelState.AddModelError("EmailId", "Invalid user account");
                        //ViewBag.Password = "Invalid user account.";
                        //return View("NewHome", usr);
                    }
                }
                else if (resultUser.ErrorCode == "-1")
                {
                    ModelState.AddModelError("EmailId", "Invalid credentials");
                    ViewBag.Password = "Invalid credentials.";
                    return View("NewHome", usr);
                }
                else if (resultUser.ErrorCode == "-2")
                {
                    ModelState.AddModelError("HospitalID", "Invalid HospitalId");
                    ViewBag.Password = "Invalid Credentials.";
                    return View("NewHome", usr);
                }
                else // here need to change code for admin login
                {
                    //PersonalInformation PInfo = PIfoservice.GetPersonalInfo(resultUser.UserId);
                    DomainUser.RUserId = resultUser.DomainId;
                    DomainUser.EmailId = resultUser.EmailId;
                    DomainUser.GroupType = resultUser.GroupType;
                    DomainUser.UserPlan = resultUser.UserPlan;
                    DomainUser.FirstName = resultUser.LoginName;
                    DomainUser.Password = resultUser.Password;
                    Session["RegUser"] = DomainUser;//added by usha for dropdown to check is group user or not

                    DateTime date1 = new DateTime();
                    date1 = DateTime.Now;
                    DateTime date2 = Convert.ToDateTime(DomainUser.ExpireOn);
                    int IsExpiry = DateTime.Compare(date1, date2);
                    //string loginname = PInfo.FirstName + " " + (PInfo.LastName != null ? PInfo.LastName : string.Empty);
                    string loginname = resultUser.LoginName;
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
                                FormsAuthentication.SetAuthCookie(usr.UserId, true);
                                ViewData["LoginSignUp"] = usr;
                                /* Hospital venkateswari */
                                if (DomainUser.GroupType != null)
                                {
                                    if (DomainUser.GroupType == "Hospital")
                                    {
                                        if (resultUser.Relationship == "Patient")
                                        {
                                            FormsAuthentication.SetAuthCookie(usr.UserId, true);

                                            return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                        }
                                        else if (resultUser.Relationship == "Doctor")
                                        {

                                        }
                                        else
                                        {
                                            FormsAuthentication.SetAuthCookie(usr.UserId, true);

                                            return RedirectToAction("HospitalInfo", "HospAdmin", new { Area = "HospitalUser" });
                                        }
                                    }
                                    else
                                    { // login with admin
                                        FormsAuthentication.SetAuthCookie(usr.UserId, true);
                                        return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                        //if (usr.EmailId == "admin@inndocs.com")
                                        //{
                                        //    FormsAuthentication.SetAuthCookie(usr.UserId, true);
                                        //    return RedirectToAction("UserAdministration", "Administration", new { Area = "iHealthUser" });
                                        //}
                                        //else
                                        //{
                                        //    FormsAuthentication.SetAuthCookie(usr.UserId, true);
                                        //    return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                        //}
                                    }
                                }
                                else
                                {
                                    FormsAuthentication.SetAuthCookie(usr.UserId, true);

                                    return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                }
                                /* Hospital venkateswari */
                            }
                            else
                            {
                                LoginAndSignUp lng = new LoginAndSignUp();
                                lng.IHealthUser = resultUser;
                                TempData["UsrObj"] = DomainUser;
                                ViewData["LoginSignUp"] = usr;
                                return RedirectToAction("PaymentNew", "Payment");
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
                                        FormsAuthentication.SetAuthCookie(usr.UserId, true);


                                        return RedirectToAction("DoctorInfo", "Doctor", new { Area = "iHealthUser" });
                                    }
                                    else if (DomainUser.GroupType == "Hospital" && resultUser.Relationship == "Patient")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.UserId, true);

                                        return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });

                                    }
                                    else if (DomainUser.GroupType == "Hospital")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.UserId, true);

                                        return RedirectToAction("HospitalInfo", "HospAdmin", new { Area = "HospitalUser" });
                                    }
                                    else
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.UserId, true);

                                        return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                    }
                                }
                                else
                                {
                                    if (DomainUser.GroupType == "Hospital" && resultUser.Relationship == "Doctor")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.UserId, true);


                                        return RedirectToAction("DoctorInfo", "Doctor", new { Area = "iHealthUser" });
                                    }
                                    else if (DomainUser.GroupType == "Hospital" && resultUser.Relationship == "Patient")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.UserId, true);

                                        return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });

                                    }
                                    else if (DomainUser.GroupType == "Hospital")
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.UserId, true);

                                        return RedirectToAction("HospitalInfo", "HospAdmin", new { Area = "HospitalUser" });
                                    }
                                    else
                                    {
                                        FormsAuthentication.SetAuthCookie(usr.UserId, true);
                                        return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                        //if (usr.EmailId == "admin@inndocs.com")
                                        //{
                                        //    FormsAuthentication.SetAuthCookie(usr.UserId, true);
                                        //    return RedirectToAction("UserAdministration", "Administration", new { Area = "iHealthUser" });
                                        //}
                                        //else
                                        //{
                                        //    FormsAuthentication.SetAuthCookie(usr.UserId, true);
                                        //    return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                                        //}
                                    }
                                }


                            }

                            /* Hospital venkateswari */
                        }
                    }
                    else if ((IsExpiry > 1 || IsExpiry == 1))
                    {

                        if (usr.EmailId == "admin@inndocs.com")
                        {
                            FormsAuthentication.SetAuthCookie(resultUser.UserId, true);
                            resultUser.IsLoggedIn = true;
                            
                            Services1 s1 = new Services1();
                            string SccessCode = s1.UpdateLoginStatus(resultUser);
                            return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                            //FormsAuthentication.SetAuthCookie(usr.EmailId, true);
                            //return RedirectToAction("UserAdministration", "Administration", new { Area = "iHealthUser" });
                        }
                        else
                        {

                            FormsAuthentication.SetAuthCookie(resultUser.UserId, true);
                            resultUser.IsLoggedIn = true;
                            // resultUser.UserSessionID = HttpContext.Request.Cookies["ASP.NET_SessionId"].Value.ToString();
                            Services1 s1 = new Services1();
                            string SccessCode = s1.UpdateLoginStatus(resultUser);

                            return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                        }

                    }
                    else if (resultUser.IsDeleted)
                    {
                        ViewBag.Password = "User deleted";
                        return View("NewHome", usr);
                    }
                    else if (resultUser.IsSuspend)
                    {
                        ViewBag.Password = "User Suspended";
                        return View("NewHome", usr);
                    }

                }


            }

            return View("NewHome", usr);

        }

        public ActionResult SendingMail(UserInformation userinfo)
        {
            bool IsSendMail = false;
            SendMail sendmail = new SendMail();

            sendmail.To = "bumpdocs@inndocs.com";
            sendmail.Body = "Hi bumpdocs \n \n" + "Email:" + userinfo.ContactEmail + "\n" + "Name:" + userinfo.ContactName + "\n" + "Subject:" + userinfo.Contactsubject + "\n" + "Message:" + userinfo.ContactMessage + "\n";
            sendmail.Subject = "Contact Us";
            RegistrationServiceCalls RegSerCalls = null;
            RegSerCalls = new RegistrationServiceCalls();
            IsSendMail = RegSerCalls.SendMail(sendmail);

            //int IsSendMail = 0; ;
            //String FROM = "Bumpdocsinfo@gmail.com";
            //String TO = "voggu.jagadeesh@gmail.com";


            //String SUBJECT = userinfo.Contactsubject;
            //String BODY = "Dear " + userinfo.ContactName + ",\n\n Thank you for registring with us.\n\n your confirmation  code for BumpDocs  is:" + "" + userinfo.ContactMessage + "\n\nThank you,\nBumpDocs";


            //// Supply your SMTP credentials below. Note that your SMTP credentials are different from your AWS credentials.
            //String SMTP_USERNAME = "AKIAJXCWFQFEFNVZ4KJA";
            //String SMTP_PASSWORD = "At3jgu5tqbckg5yXcOpWh5BpwlAMRRsJtcs0Kwjr2kv8";

            //// Amazon SES SMTP host name. This example uses the us-east-1 region.
            //String HOST = "email-smtp.us-east-1.amazonaws.com";

            //// Port we will connect to on the Amazon SES SMTP endpoint. We are choosing port 587 because we will use
            //// STARTTLS to encrypt the connection.
            //int PORT = 587;

            //_log.Error("Enter into Email sending1 ");
            //using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(HOST, PORT))
            //{

            //    client.Credentials = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

            //    client.EnableSsl = true;


            //    client.Send(FROM, TO, SUBJECT, BODY);

            //    IsSendMail = 1080;
            //}


            return RedirectToAction("Home");
        }



        public ActionResult Register()
        {
            return PartialView(new LoginAndSignUp());
        }

        [HttpPost]
        public ActionResult Register(LoginAndSignUp LogAdnSignUpObj)
        {
            int SuccessCode = 0;
            string InsertedID = string.Empty;
            string[] insertedCode;
            string iHealthinsertedId = string.Empty;
            RegisteredUser regUser = null;
            regUser = LogAdnSignUpObj.RegUser;

            JsonResult ResultData = new JsonResult();

            regUser.Password = CryptorEngine.Encrypt(regUser.Password, true);
            regUser.CreatedOn = System.DateTime.Now;
            regUser.IsVerified = false;
            bool _pCodeMatch = false;

            if (LogAdnSignUpObj.RegUser.TryIt == true)
            {
                regUser.UserPlan = "TrailUser";
            }
            if (LogAdnSignUpObj.RegUser.PromoCodeValue == null && LogAdnSignUpObj.RegUser.TryIt == false)
            {
                regUser.UserPlan = "PaidUser";
            }

            if ((LogAdnSignUpObj.RegUser.PromoCodeValue != null) && (LogAdnSignUpObj.RegUser.TryIt == false))
            {
                List<PromoCode> _lstPromoCodes = null;
                _lstPromoCodes = new List<PromoCode>();
                RegistrationServiceCalls RuserService = new RegistrationServiceCalls();
                _lstPromoCodes = RuserService.GetAllPromoCodes();
                for (int i = 0; i < _lstPromoCodes.Count; i++)
                {
                    if (LogAdnSignUpObj.RegUser.PromoCodeValue == _lstPromoCodes[i].PromoCodes)
                    {
                        _pCodeMatch = true;
                        regUser.ExpireOn = _lstPromoCodes[i].ExpireOn;
                        regUser.SubscriptionType = _lstPromoCodes[i].CodeUser;
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

            bool IsSendMail = false;
            if (InsertedID != null)
            {
                #region Mail Sending
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(regUser.EmailId);
                    mail.From = new MailAddress("medlogx@gmail.com");
                    mail.Subject = "Registration details";
                    string Body = "Dear " + regUser.FirstName + ",\n\n You have been successfully registered with us.\n\n Please click on below link to verify your email ID. \n\n " + "107.20.220.222:7814/confirmaccount/confirmaccount?userid=" + InsertedID + "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";

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
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with MedLogx, Please check your mail to confirm your registration.</h3>";
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

        [HttpPost]
        public ActionResult Login(LoginAndSignUp LogAdnSignUpObj)
        {
            if (ModelState.IsValid)//Added by SMitra
            {
                Services1 UsrService = new Services1();
                Services3 PIfoservice = new Services3();
                UserInformation usr = new UserInformation();
                usr = LogAdnSignUpObj.IHealthUser;

                UserInformation resultUser = UsrService.ValidateUser(usr.EmailId, usr.Password);
                if (resultUser.ErrorCode == "9150")
                {
                    ModelState.AddModelError("EmailId", "Not a User");
                    ViewBag.Password = "Invalid User.";
                    return View("Home", LogAdnSignUpObj);
                }
                else if (resultUser.ErrorCode == "-1")
                {
                    ModelState.AddModelError("EmailId", "InvalidEmail");
                    ViewBag.Password = "Invalid Credentials.";
                    return View("Home", LogAdnSignUpObj);
                }
                else
                {
                    PersonalInformation PInfo = PIfoservice.GetPersonalInfo(resultUser.UserId);
                    RegisteredUser DomainUser = new RegisteredUser();
                    RegistrationServiceCalls RegService = new RegistrationServiceCalls();
                    DomainUser = RegService.GetRegUserbyID(resultUser.DomainId.ToString());
                    Session["RegUser"] = DomainUser;//added by usha for dropdown to check is group user or not

                    DateTime date1 = new DateTime();
                    date1 = DateTime.Now;
                    DateTime date2 = Convert.ToDateTime(DomainUser.ExpireOn);
                    int IsExpiry = DateTime.Compare(date1, date2);
                    string loginname = PInfo.FirstName + " " + (PInfo.LastName != null ? PInfo.LastName : string.Empty);
                    Session["LoginName"] = loginname;
                    Session["LoginUser"] = resultUser;
                    //List<SelectListItem> Userslist = null;
                    //Userslist = new List<SelectListItem>();
                    //List<UserInformation> Users = null;
                    //Users = new List<UserInformation>();

                    if ((resultUser.EmailId != null) && (IsExpiry < 1) && (!resultUser.IsDeleted) && (!resultUser.IsSuspend))
                    {
                        if (DomainUser.UserPlan == "PaidUser")
                        {
                            if (resultUser.IsTransaction == true)
                            {
                                FormsAuthentication.SetAuthCookie(usr.EmailId, true);
                                ViewData["LoginSignUp"] = LogAdnSignUpObj;
                                return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
                            }
                            else
                            {
                                LoginAndSignUp lng = new LoginAndSignUp();
                                lng.IHealthUser = resultUser;
                                TempData["UsrObj"] = lng;
                                ViewData["LoginSignUp"] = LogAdnSignUpObj;
                                return RedirectToAction("Payment", "Payment");
                            }

                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(usr.EmailId, true);
                            ViewData["LoginSignUp"] = LogAdnSignUpObj;
                            return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });


                        }
                    }
                    else if ((IsExpiry > 1))
                    {
                        ViewBag.Password = "User Expired";
                        LoginAndSignUp lng = new LoginAndSignUp();
                        lng.IHealthUser = resultUser;
                        TempData["UsrObj"] = lng;
                        ViewData["LoginSignUp"] = LogAdnSignUpObj;
                        return RedirectToAction("Payment", "Payment");
                    }
                    else if (resultUser.IsDeleted)
                    {
                        ViewBag.Password = "User deleted";
                        return View("Home", LogAdnSignUpObj);
                    }
                    else if (resultUser.IsSuspend)
                    {
                        ViewBag.Password = "User Suspended";
                        return View("Home", LogAdnSignUpObj);
                    }
                    //else
                    //{//Changes done by SMitra
                    //    ModelState.AddModelError("EmailId", "InvalidEmail");
                    //    ViewBag.Password = "Invalid Credentials.";
                    //    return View("Home", LogAdnSignUpObj);
                    //}
                }

            }

            return View("Home", LogAdnSignUpObj);

        }

        public ActionResult ForgotPassword()
        {
            return PartialView(new ForgotPassword());
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword forgotpwd)
        {
            _log.Error("enter into forgotpassword btn click");
            JsonResult ResultData = new JsonResult();
            _log.Error("ModelState.IsValid :" + ModelState.IsValid);
            if (ModelState.IsValid)
            {
                Services1 s = new Services1();

                UserInformation resultUsr = s.GetUserByEmailForgotPaswd(forgotpwd.EmailId);
                HttpContext.Session["concode"] = resultUsr.OTACode;

                switch (resultUsr.ResultCode)
                {
                    case 1010:
                        {
                            ViewBag.ErrorMessage = "Please check your mail for confirmation code.";
                            break;
                        }
                    case 1011:
                        {
                            ViewBag.ErrorMessage = @"We cannot find you with us. Please check the details or <a href='/home/home'>join us.</a>";
                            break;
                        }
                    case 1050:
                        {
                            ViewBag.ErrorMessage = "Please check your phone for confirmation code.";
                            break;
                        }
                    case 1051:
                        {
                            ViewBag.ErrorMessage = @"We cannot find you with us. Please check the details or <a href='/home/home'>join us.</a>";
                            return PartialView("ForgotPassword");
                        }
                    case 1060:
                        {
                            ViewBag.ErrorMessage = @"We cannot find you with us. Please check the details or <a href='/home/home'>join us.</a>";
                            return PartialView("ForgotPassword");
                        }
                    case 1081:
                        {
                            ViewBag.ErrorMessage = "Problem in server mail sending failed, please try after some time";
                            break;
                        }
                    case 1091:
                        {
                            ViewBag.ErrorMessage = "Problem in server message sending failed, please try after some time";
                            break;
                        }
                    default:
                        {
                            ViewBag.ErrorMessage = "Please check whether you have valid details.";
                            break;
                        }
                }
                ViewBag.UserId = resultUsr.UserId;
                return PartialView("code");

            }
            return PartialView("ForgotPassword");


        }

        public ActionResult feedback()
        {
            return PartialView("feedback");
        }

        public ActionResult ContactUs(ContactUsViewModel cInfo)
        {
            string insertedCode;
            RegistrationServiceCalls RegSerCalls = null;
            RegSerCalls = new RegistrationServiceCalls();
            insertedCode = RegSerCalls.InsertContactInfo(cInfo);

            // return View("Home", "Home");
            JsonResult res = new JsonResult();
            if (insertedCode == "1010")
            {
                res.Data = "We will get back to you soon";
                return Json(res.Data, JsonRequestBehavior.AllowGet);// res;
                //  return res;
            }
            else
            {
                res.Data = "Error occured in Submitting";
                return res;
            }
        }

        public ActionResult Feedback1(ContactUsViewModel fInfo)
        {
            string insertedCode;
            RegistrationServiceCalls RegSerCalls = null;
            RegSerCalls = new RegistrationServiceCalls();
            insertedCode = RegSerCalls.InsertFeedbackInfo(fInfo);

            //return View("Home", "Home");
            JsonResult res = new JsonResult();
            if (insertedCode == "1010")
            {
                res.Data = "We will get back to you soon";
                return Json(res.Data, JsonRequestBehavior.AllowGet);// res;
                //  return res;
            }
            else
            {
                res.Data = "Error occured in Submitting";
                return res;
            }
        }

        public ActionResult Services(string detector)
        {
            return PartialView("Services", detector);
        }

        public ActionResult Pricing(string detector)
        {
            return PartialView("Pricing", detector);
        }

        public ActionResult Blog(string detector)
        {
            return PartialView("Blog", detector);
        }

        public ActionResult Careers(string detector)
        {
            return PartialView("Careers", detector);
        }

        public ActionResult FContactus(string detector)
        {
            return PartialView("Contactus", detector);
        }

        public ActionResult ResendOtaCode()
        {
            return PartialView(new ConfrimAccount());
        }

        [HttpPost]
        public ActionResult ResendOtaCode(string MobileNumber)
        {
            JsonResult ResulData = new JsonResult();
            RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            SingleRegisterModel singleuserObj = reguser.GetRegUserbyMobile(MobileNumber);

            if (!singleuserObj.IsPhnVerified)
            {
                if (singleuserObj.RUserId != null)
                {
                    DateTime date1 = new DateTime();
                    date1 = DateTime.Now;
                    DateTime date2 = Convert.ToDateTime(singleuserObj.OTAExpire);
                    int IsExpiry = DateTime.Compare(date1, date2);
                    if (IsExpiry == -1)
                    {
                        string phnno = Convert.ToString(singleuserObj.PhoneNumber);
                        string Uid = "7386041104";
                        string Password = "55312";
                        string Number = phnno;
                        string Message = singleuserObj.OTACode;
                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + Uid + "&pwd=" + Password +
                   "&phone=" + Number + "&msg=" + Message + "&provider=Fullonsms");

                        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                        string responseString = respStreamReader.ReadToEnd();

                        respStreamReader.Close();
                        myResp.Close();
                        if (responseString == "1")
                        {
                            ResulData.Data = "Check your mobile for the confirmation code.";

                        }


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

                        DateTime? otaexpire = DateTime.Now;
                        string strDate = singleuserObj.CreatedOn.ToString();
                        DateTime date = DateTime.Parse(strDate);
                        DateTime expireDate = date.AddHours(24);

                        RegistrationServiceCalls regservice = null;
                        regservice = new RegistrationServiceCalls();
                        SingleRegisterModel srm = new SingleRegisterModel();
                        srm = regservice.GetRegUserbyMobile(MobileNumber);
                        srm.OTACode = otaCode;
                        srm.OTAExpire = expireDate;
                        string successcode = regservice.UpdateUserOTACode(srm);
                        if (successcode == "1020")
                        {
                            string phnno = Convert.ToString(singleuserObj.PhoneNumber);
                            string Uid = "7386041104";
                            string Password = "55312";
                            string Number = phnno;
                            string Message = otaCode;
                            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + Uid + "&pwd=" + Password +
                       "&phone=" + Number + "&msg=" + Message + "&provider=Fullonsms");

                            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                            string responseString = respStreamReader.ReadToEnd();
                            // Close the Stream object.
                            respStreamReader.Close();
                            myResp.Close();
                            if (responseString == "1")
                            {
                                ResulData.Data = "Check your mobile for the confirmation code.";

                            }
                        }

                    }
                }
                else
                {
                    ResulData.Data = "Please provide us active phone number.";
                }
            }
            else
            {
                ResulData.Data = "You are already a member with us.";
            }


            return ResulData;
        }

        public ActionResult Resend(string UserId)
        {
            RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            string SuccessCode = reguser.ResendOTA(UserId);
            ota o = new ota();
            o.userid = UserId;
            if (SuccessCode == "1080")
            {
                ViewBag.UserId = UserId;
                ViewBag.ErrorMessage = "Please check your mail for confirmation code.";
            }
            else
            {
                ViewBag.UserId = UserId;
                ViewBag.ErrorMessage = "Something's wrong, please try again.";
            }
            return View("Confirm_OTAcode", masterName: "Common_Layout", model: o);

        }

        [HttpPost]
        public ActionResult ConfirmCode(ota code)
        {
            string ucode = HttpContext.Session["concode"] as string;

            if (!string.IsNullOrEmpty(ucode) && ucode == code.otacode)
            {
                ViewBag.ErrorMessage = "Please enter your new password.";
                ViewBag.UserId = code.userid;
                return View("NewPassword", masterName: "Common_Layout");
            }

            ViewBag.ErrorMessage = "Wrong code! Please try again.";
            ViewBag.UserId = code.userid;
            return View("code", masterName: "Common_Layout", model: code);
        }

        public ActionResult ResendCode(string UserId)
        {
            // RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            Services1 ser1 = new Services1();
            // string SuccessCode = reguser.ResendOTA(UserId);
            string SuccessCode = ser1.ResendOTAforForgotPaswd(UserId);
            ota o = new ota();
            o.userid = UserId;
            if (SuccessCode == "1080")
            {
                ViewBag.UserId = UserId;
                ViewBag.ErrorMessage = "Please check your mail/mobile for confirmation code.";
            }
            else
            {
                ViewBag.UserId = UserId;
                ViewBag.ErrorMessage = "Something's wrong, please try again.";
            }
            return View("code", masterName: "Common_Layout", model: o);

        }

        [HttpPost]
        public ActionResult NewPassword(FormCollection collections)
        {

            if (collections["password"] == collections["rpassword"])
            {
                var code = new Services1().UpdatePassword(collections["userid"], collections["password"]);
                ViewBag.ErrorMessage = code == "1020" ? "Your password has been set successfully! Please go to home page and login." : "Something went wrong! Please try again.";
                return View("Password", masterName: "Common_Layout");
            }
            ViewBag.ErrorMessage = "Something went wrong! Please try again.";
            ViewBag.UserId = collections["userid"];
            return View("NewPassword", masterName: "Common_Layout");
        }

        public ActionResult EmailVerification(string EmailId)
        {
            RegistrationServiceCalls reser = new RegistrationServiceCalls();

            bool val = Convert.ToBoolean(reser.EmailVerification(EmailId));
            return Json(val, JsonRequestBehavior.AllowGet);
        }

        public ActionResult terms()
        {
            return View();
        }


    }
}
