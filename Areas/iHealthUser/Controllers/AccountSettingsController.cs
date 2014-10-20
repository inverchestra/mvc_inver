using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net;
using InnDocs.iHealth.Web.UI.Utilities;
using InnDocs.iHealth.Web.UI.Models;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET;
using System.Text;
using System.Linq;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Create;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    //public class ContactUs
    //{
    //    private string _subject;

    //    public string Subject
    //    {
    //        get { return _subject; }
    //        set { _subject = value; }
    //    }
    //    private string _emailId;
    //    public string EmailId
    //    {
    //        get { return _emailId; }
    //        set { _emailId = value; }
    //    }

    //    private string _description;

    //    public string Description
    //    {
    //        get { return _description; }
    //        set { _description = value; }
    //    }

    //    //}
    //    // public class Feedback
    //    //{
    //    private string _fsubject;

    //    public string FSubject
    //    {
    //        get { return _fsubject; }
    //        set { _fsubject = value; }
    //    }
    //    private string _email;
    //    public string Email
    //    {
    //        get { return _email; }
    //        set { _email = value; }
    //    }

    //    private string _fdescription;

    //    public string FDescription
    //    {
    //        get { return _fdescription; }
    //        set { _fdescription = value; }
    //    }

    //}
    [SessionExpireFilter]
    public class AccountSettingsController : Controller
    {
        //
        // GET: /iHealthUser/AccountSettings/

        public ActionResult PaymentManagement()  // added by prashanth
        {
            string currentpage = "PaymentManagement";
            Session["CurrentPage"] = currentpage;
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            //List<OnlinePayments> op = new List<OnlinePayments>();
            LoginAndSignUp _loginandSignUp = null; _loginandSignUp = new LoginAndSignUp();

            RegistrationServiceCalls _regSerCalls = null;
            _regSerCalls = new RegistrationServiceCalls();

            SingleRegisterModel snglRegModel = null;
            snglRegModel = new SingleRegisterModel();

            Services1 UserService1 = new Services1();

            _loginandSignUp.SingleRegUser = _regSerCalls.GetSingleRegUserbyID(LoginUser.DomainId);
            _loginandSignUp.LstOP = UserService1.GetOnlinePaymentsByDomainId(LoginUser.DomainId).OrderByDescending(x => x.OnlinePaymentDate).ToList();
            _loginandSignUp.OP = _loginandSignUp.LstOP.FirstOrDefault();
            //Services1 UserService1 = new Services1();
            //var op = UserService1.GetOnlinePaymentsByDomainId(LoginUser.DomainId);
            //op = op.OrderByDescending(x => x.OnlinePaymentDate).ToList();
            //var item = _loginandSignUp.LstOP.FirstOrDefault();
            return View(_loginandSignUp);
        }


        public ActionResult Index()
        {
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }


            LoginAndSignUp _loginandSignUp = null;
            _loginandSignUp = new LoginAndSignUp();
            UserInformation usr = null;
            SingleRegisterModel SingleRegUsr = null; // sandeep added

            List<UserInformation> Users = null;
            Services1 UsrService = new Services1();
            RegistrationServiceCalls RuserService = new RegistrationServiceCalls();

            //usr = UsrService.GetUserbyId(iHealthusr.UserId);
            usr = UsrService.GetUserbyIdNew(LoginUser.UserId, LoginUser.GroupType);
            Session["LoginUser"] = usr;
            if (usr.Password != null)
            {
                usr.Password = CryptorEngine.Decrypt(usr.Password, true);
            }

            // sandeep added new code start here on 17-09-2013

            if (usr.IsModerator)
            {
                if (usr.EmailId != null && usr.IsVerified)
                {
                    SingleRegUsr = RuserService.GetRegUserbyEmailId(usr.EmailId);
                }
                else
                {
                    SingleRegUsr = RuserService.GetRegUserbyMobile(usr.PhoneNo.ToString());
                }
            }
            _loginandSignUp.Timezone = usr.Timezone;
            _loginandSignUp.Notificationtime = usr.Notificationtime;
            Filters filter = new Filters();
            filter = GetFilters.GetFiltersById(LoginUser.UserId);
            _loginandSignUp.Privacy = filter.Privacy;
            if (filter != null)
            {
                if (filter.Filtertext != null)
                {
                    if (filter.Filtertext.Contains("Gestination"))
                    {
                        _loginandSignUp.Gestination = true;
                    }
                    else
                    {
                        _loginandSignUp.Gestination = false;
                    }
                    if (filter.Filtertext.Contains("Hypertension"))
                    {
                        _loginandSignUp.Hypertension = true;
                    }
                    else
                    {
                        _loginandSignUp.Hypertension = false;
                    }
                    if (filter.Filtertext.Contains("Latepregnancy"))
                    {
                        _loginandSignUp.Latepregnancy = true;
                    }
                    else
                    {
                        _loginandSignUp.Latepregnancy = false;
                    }
                    if (filter.Filtertext.Contains("Gestination"))
                    {
                        _loginandSignUp.Gestination = true;
                    }
                    else
                    {
                        _loginandSignUp.Gestination = false;
                    }
                    if (filter.Filtertext.Contains("Earlypregnancy"))
                    {
                        _loginandSignUp.Earlypregnancy = true;
                    }
                    else
                    {
                        _loginandSignUp.Earlypregnancy = false;
                    }
                    if (filter.Filtertext.Contains("Thyroid"))
                    {
                        _loginandSignUp.Thyroid = true;
                    }
                    else
                    {
                        _loginandSignUp.Thyroid = false;
                    }
                    if (filter.Filtertext.Contains("Diabetes"))
                    {
                        _loginandSignUp.Diabetes = true;
                    }
                    else
                    {
                        _loginandSignUp.Diabetes = false;
                    }
                    if (filter.Filtertext.Contains("Twins"))
                    {
                        _loginandSignUp.Twins = true;
                    }
                    else
                    {
                        _loginandSignUp.Twins = false;
                    }
                    if (filter.Filtertext.Contains("Tripplets"))
                    {
                        _loginandSignUp.Tripplets = true;
                    }
                    else
                    {
                        _loginandSignUp.Tripplets = false;
                    }
                    if (filter.Filtertext.Contains("Asthma"))
                    {
                        _loginandSignUp.Asthma = true;
                    }
                    else
                    {
                        _loginandSignUp.Asthma = false;
                    }
                }
                if (filter.Interests != null)
                {
                    if (filter.Interests.Contains("Outdoorsports"))
                    {
                        _loginandSignUp.Outdoorsports = true;
                    }
                    else
                    {
                        _loginandSignUp.Outdoorsports = false;
                    }
                    if (filter.Interests.Contains("Swimming"))
                    {
                        _loginandSignUp.Swimming = true;
                    }
                    else
                    {
                        _loginandSignUp.Swimming = false;
                    }
                    if (filter.Interests.Contains("Yoga"))
                    {
                        _loginandSignUp.Yoga = true;
                    }
                    else
                    {
                        _loginandSignUp.Yoga = false;
                    }
                    if (filter.Interests.Contains("Adventuresports"))
                    {
                        _loginandSignUp.Adventuresports = true;
                    }
                    else
                    {
                        _loginandSignUp.Adventuresports = false;
                    }
                    if (filter.Interests.Contains("Gardening"))
                    {
                        _loginandSignUp.Gardening = true;
                    }
                    else
                    {
                        _loginandSignUp.Gardening = false;
                    }
                    if (filter.Interests.Contains("Animalcare"))
                    {
                        _loginandSignUp.Animalcare = true;
                    }
                    else
                    {
                        _loginandSignUp.Animalcare = false;
                    }
                    if (filter.Interests.Contains("Pets"))
                    {
                        _loginandSignUp.Pets = true;
                    }
                    else
                    {
                        _loginandSignUp.Pets = false;
                    }
                    if (filter.Interests.Contains("Arts"))
                    {
                        _loginandSignUp.Arts = true;
                    }
                    else
                    {
                        _loginandSignUp.Arts = false;
                    }
                    if (filter.Interests.Contains("Modeling"))
                    {
                        _loginandSignUp.Modeling = true;
                    }
                    else
                    {
                        _loginandSignUp.Modeling = false;
                    }
                    if (filter.Interests.Contains("Interiordesigning"))
                    {
                        _loginandSignUp.Interiordesigning = true;
                    }
                    else
                    {
                        _loginandSignUp.Interiordesigning = false;
                    }
                    if (filter.Interests.Contains("Travelling"))
                    {
                        _loginandSignUp.Travelling = true;
                    }
                    else
                    {
                        _loginandSignUp.Travelling = false;
                    }
                }
            }
            // sandeep added new code end here on 17-09-2013

            //RUsr = RuserService.GetRegUserbyEmail(usr.EmailId);
            Services3 Pinfoservice = new Services3();
            _loginandSignUp.PInfo = Pinfoservice.GetPersonalInfo(LoginUser.UserId);//usha
            _loginandSignUp.LstPInfo = Pinfoservice.GetAllUsersbyDomainID(LoginUser.DomainId);
            _loginandSignUp.DoulaUser = UsrService.GetDoulaUser(LoginUser.UserId); // sandeep added on 4-3-2014
            if (_loginandSignUp.DoulaUser.Count > 0)
            {
                Session["DoulaUser"] = _loginandSignUp;
            }
            else
            {
                Session["DoulaUser"] = "";
            }

            //if (RUsr != null) // sandeep commented
            if (SingleRegUsr != null)
            {
                SingleRegUsr.ExpireOn1 = Convert.ToDateTime(SingleRegUsr.ExpireOn).ToShortDateString(); // sandeep added
                Users = UsrService.GetAllUsersbyDomainID(usr.DomainId);
                UserInformation UserInfo = null;
                UserInfo = Users.Find(delegate(UserInformation UInfo) { return UInfo.IsModerator == true; });
                Users.Remove(UserInfo);
            }
            Session["UsesrList"] = UsrService.Userslist(usr.DomainId, usr.UserId);
            _loginandSignUp.IHealthUser = usr;
            //_loginandSignUp.RegUser = RUsr; // sandeep commented
            _loginandSignUp.SingleRegUser = SingleRegUsr; // sandeep added on 28-09-2013
            _loginandSignUp.Users = Users;
            String ViewName = string.Empty;
            if (SingleRegUsr != null)
            {
                if (SingleRegUsr.IsGroupUser == true)
                {
                    ViewName = "ModeratorAccountSettings";
                }
                else
                {
                    ViewName = "UserAccountSettings";
                }
            }
            else
            {
                ViewName = "UserAccountSettings";
            }
            return View(ViewName, _loginandSignUp);
        }

        [HttpPost]
        public ActionResult SubmitDetails(LoginAndSignUp _loginandsignupObj)
        {
            UserInformation userinfo = null;
            userinfo = new UserInformation();
            Services1 UserService = null;
            UserService = new Services1();
            userinfo = UserService.GetUserbyId(_loginandsignupObj.IHealthUser.UserId);
            userinfo.ModifiedOn = System.DateTime.Now;
            //userinfo.FirstName = _loginandsignupObj.IHealthUser.FirstName;
            //userinfo.LastName = _loginandsignupObj.IHealthUser.LastName;
            //userinfo.LoginName = _loginandsignupObj.IHealthUser.LoginName;
            //userinfo.Relationship = _loginandsignupObj.IHealthUser.Relationship;
            //userinfo.Password = CryptorEngine.Encrypt(_loginandsignupObj.IHealthUser.Password, true); 
            UserService.UpdateUserDetails(userinfo, userinfo.UserId);
            return RedirectToAction("Index", "AccountSettings");
        }

        public ActionResult TimeZone()
        {

            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            LoginAndSignUp _loginandSignUp = null;
            _loginandSignUp = new LoginAndSignUp();
            UserInformation usr = null;
            SingleRegisterModel SingleRegUsr = null; // sandeep added

            List<UserInformation> Users = null;
            Services1 UsrService = new Services1();

            usr = UsrService.GetUserbyIdNew(LoginUser.UserId, LoginUser.GroupType);
            _loginandSignUp.Timezone = usr.Timezone;
            return PartialView(_loginandSignUp);
        }

        [HttpPost]
        public ActionResult UpdatetimeZone(string Timezone)
        {
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            UserInformation usr = null;
            List<UserInformation> Users = null;
            Services1 s1 = new Services1();
            usr = s1.GetUserbyIdNew(LoginUser.UserId, LoginUser.GroupType);
            usr.Timezone = Timezone;

            string SccessCode = s1.UpdateTimeZonestatus(usr);

            return RedirectToAction("Index");
        }

        public ActionResult NotiFicationTime()
        {
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            UserInformation usr = null;
            List<UserInformation> Users = null;
            Services1 s1 = new Services1();
            usr = s1.GetUserbyIdNew(LoginUser.UserId, LoginUser.GroupType);
            return PartialView(usr);
        }

        public ActionResult UpdateTime(string uId)
        {
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            UserInformation usr = null;
            List<UserInformation> Users = null;
            Services1 s1 = new Services1();
            usr = s1.GetUserbyIdNew(LoginUser.UserId, LoginUser.GroupType);
            usr.Notificationtime = uId;

            string SccessCode = s1.UpdateTimeZonestatus(usr);
            return RedirectToAction("Index");
        }

        private bool UpdateUserDetails(UpdateUserDetails NewUserDetails)
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
            //userinfo.FirstName = NewUserDetails.IHealthUser.FirstName;
            //userinfo.LastName = NewUserDetails.IHealthUser.LastName;
            // userinfo.LoginName = NewUserDetails.IHealthUser.LoginName;
            userinfo.Relationship = NewUserDetails.IHealthUser.Relationship;
            if (NewUserDetails.IHealthUser.IsUsingModeratorCredentials != true)
            {
                if (userinfo.IsUsingModeratorCredentials == true && NewUserDetails.IHealthUser.IsUsingModeratorCredentials == false)
                {
                    if (userinfo.EmailId != NewUserDetails.IHealthUser.EmailId)
                    {
                        userinfo.EmailId = NewUserDetails.IHealthUser.EmailId;
                        userinfo.Password = CryptorEngine.Encrypt(NewUserDetails.IHealthUser.Password, true);
                    }

                }
                else
                {
                    userinfo.EmailId = NewUserDetails.IHealthUser.EmailId;
                    userinfo.Password = CryptorEngine.Encrypt(NewUserDetails.IHealthUser.Password, true);
                }
            }
            else
            {
                userinfo.EmailId = RegUser.EmailId;
                userinfo.Password = RegUser.Password;
            }
            userinfo.IsUsingModeratorCredentials = NewUserDetails.IHealthUser.IsUsingModeratorCredentials;
            //added sangamithra
            string userupdate = UserService.UpdateUserPasswordDetails(userinfo, userinfo.UserId);
            //added sangamithra
            // string userupdate = UserService.UpdateUserDetails(userinfo, userinfo.UserId);/
            if (userupdate == "1020")
            {
                Services3 s3 = new Services3();

                PersonalInformation pinfo = new PersonalInformation();
                pinfo = s3.GetPersonalInfo(NewUserDetails.IHealthUser.UserId);
                // pinfo.Gender = NewUserDetails.IHealthUser.Gender;
                int result = s3.UpdatePersonalInfo(pinfo, NewUserDetails.IHealthUser.UserId);
            }
            bool IsSendMail = false;
            if (userupdate == "1020")
            {
                #region Mail Sending
                try
                {
                    //MailMessage mail = new MailMessage();
                    //mail.To.Add(userinfo.EmailId);
                    //mail.From = new MailAddress("medlogx@gmail.com");
                    //mail.Subject = "iHealth User Credentials";
                    //string Body = "Hi " + userinfo.EmailId + System.Environment.NewLine + " your Password changed successful " + System.Environment.NewLine + " Your iHealth User account details are: Userid:" + userinfo.EmailId + "Passowrd:" + CryptorEngine.Decrypt(userinfo.Password, true);// isusing Gmail in ASP.NET";
                    //mail.Body = Body;
                    //mail.IsBodyHtml = true;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    //smtp.Credentials = new System.Net.NetworkCredential("medlogx@gmail.com", "medlogx2013");
                    //smtp.EnableSsl = true;
                    //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    //smtp.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    return IsSendMail;
                }
                #endregion
            }
            else
            {
                return IsSendMail;
            }
        }

        private bool UpdateUsrDetails(AddUser NewUserDetails)
        {
            AddUser userinfo = null;
            userinfo = new AddUser();
            Services1 UserService = null;
            UserService = new Services1();
            userinfo = UserService.GetUsrbyId(NewUserDetails.UserId);
            RegisteredUser RegUser = new RegisteredUser();
            RegistrationServiceCalls regService = new RegistrationServiceCalls();
            RegUser = regService.GetRegUserbyID(userinfo.DomainId.ToString());
            userinfo.ModifiedOn = System.DateTime.Now;
            userinfo.FirstName = NewUserDetails.FirstName;
            userinfo.LastName = NewUserDetails.LastName;
            userinfo.LoginName = NewUserDetails.LoginName;
            userinfo.Relationship = NewUserDetails.Relationship;
            if (NewUserDetails.IsUsingModeratorCredentials != true)
            {
                if (userinfo.IsUsingModeratorCredentials == true && NewUserDetails.IsUsingModeratorCredentials == false)
                {
                    if (userinfo.EmailId != NewUserDetails.EmailId)
                    {
                        userinfo.EmailId = NewUserDetails.EmailId;
                        userinfo.Password = CryptorEngine.Encrypt(NewUserDetails.Password, true);
                    }

                }
                else
                {
                    userinfo.EmailId = NewUserDetails.EmailId;
                    userinfo.Password = CryptorEngine.Encrypt(NewUserDetails.Password, true);
                }
            }
            else
            {
                userinfo.EmailId = RegUser.EmailId;
                userinfo.Password = RegUser.Password;
            }
            userinfo.IsUsingModeratorCredentials = NewUserDetails.IsUsingModeratorCredentials;
            string userupdate = UserService.UpdateUsrDetails(userinfo, userinfo.UserId);
            if (userupdate == "1020")
            {
                Services3 s3 = new Services3();
                // string type="Hospital";
                PersonalInformation pinfo = new PersonalInformation();
                pinfo = s3.GetPersonalInfo(NewUserDetails.UserId);
                pinfo.Gender = NewUserDetails.Gender;
                int result = s3.UpdatePersonalInfo(pinfo, NewUserDetails.UserId);
            }
            bool IsSendMail = false;
            if (userupdate == "1020")
            {
                #region Mail Sending
                try
                {
                    //MailMessage mail = new MailMessage();
                    //mail.To.Add(userinfo.EmailId);
                    //mail.From = new MailAddress("medlogx@gmail.com");
                    //mail.Subject = "iHealth User Credentials";
                    //string Body = "Hi " + userinfo.EmailId + System.Environment.NewLine + " your Password changed successful " + System.Environment.NewLine + " Your iHealth User account details are: Userid:" + userinfo.EmailId + "Passowrd:" + CryptorEngine.Decrypt(userinfo.Password, true);// isusing Gmail in ASP.NET";
                    //mail.Body = Body;
                    //mail.IsBodyHtml = true;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    //smtp.Credentials = new System.Net.NetworkCredential("medlogx@gmail.com", "medlogx2013");
                    //smtp.EnableSsl = true;
                    //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    //smtp.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    return IsSendMail;
                }
                #endregion
            }
            else
            {
                return IsSendMail;
            }
        }

        [HttpPost]
        public ActionResult UpdateFilterText(LoginAndSignUp cnfirmSingle)
        {
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
                cnfirmSingle.Interest = "Outdoorsports";
            }
            if (cnfirmSingle.Swimming == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Swimming";
                }
                else
                {
                    cnfirmSingle.Interest = "Swimming";
                }
            }
            if (cnfirmSingle.Yoga == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Yoga";
                }
                else
                {
                    cnfirmSingle.Interest = "Yoga";
                }
            }
            if (cnfirmSingle.Adventuresports == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Adventuresports";
                }
                else
                {
                    cnfirmSingle.Interest = "Adventuresports";
                }
            }
            if (cnfirmSingle.Gardening == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Gardening";
                }
                else
                {
                    cnfirmSingle.Interest = "Gardening";
                }
            }
            if (cnfirmSingle.Animalcare == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Animalcare";
                }
                else
                {
                    cnfirmSingle.Interest = "Animalcare";
                }
            }
            if (cnfirmSingle.Pets == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Pets";
                }
                else
                {
                    cnfirmSingle.Interest = "Pets";
                }
            }
            if (cnfirmSingle.Arts == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Arts";
                }
                else
                {
                    cnfirmSingle.Interest = "Arts";
                }
            }
            if (cnfirmSingle.Modeling == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Modeling";
                }
                else
                {
                    cnfirmSingle.Interest = "Modeling";
                }
            }
            if (cnfirmSingle.Interiordesigning == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Interiordesigning";
                }
                else
                {
                    cnfirmSingle.Interest = "Interiordesigning";
                }
            }
            if (cnfirmSingle.Travelling == true)
            {
                if (cnfirmSingle.Interest != null)
                {
                    cnfirmSingle.Interest = cnfirmSingle.Interest + ",Travelling";
                }
                else
                {
                    cnfirmSingle.Interest = "Travelling";
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
            Filters filter = new Filters();
            filter.Filtertext = cnfirmSingle.filtertext;
            filter.Interests = cnfirmSingle.Interest;
            filter.Privacy = cnfirmSingle.Privacy;
            filter.UserId = cnfirmSingle.IHealthUser.UserId;
            int errorcode = GetFilters.UpdateFiltertext(filter);
            JsonResult jr = new JsonResult();
            jr.Data = errorcode;
            return jr;
            //return RedirectToAction("Index", "AccountSettings");
        }
        [HttpPost]
        //public ActionResult CreateUser(AddUser NewUser) // sandeep commented
        public ActionResult SaveSubUser(AddUser NewUser) // sandeep changed action name
        {
            JsonResult res = new JsonResult();
            string insertedID = string.Empty;
            AddUser userinfo = null;
            userinfo = new AddUser();
            //UserInformation LoginUser = new UserInformation();
            UserInformation LoginUser = null;
            LoginUser = Session["LoginUser"] as UserInformation;
            Services1 UserService = null;
            UserService = new Services1();
            SingleRegisterModel RegUser = new SingleRegisterModel();
            RegistrationServiceCalls regService = new RegistrationServiceCalls();
            userinfo.CreatedOn = System.DateTime.Now;
            userinfo.FirstName = NewUser.FirstName;
            userinfo.LastName = NewUser.LastName;
            userinfo.Relationship = NewUser.Relationship;
            userinfo.IsVerified = false;//added by usha for verfication
            RegUser = regService.GetSingleRegUserbyID(LoginUser.DomainId.ToString());
            userinfo.DomainId = RegUser.RUserId;
            userinfo.GroupType = LoginUser.GroupType;
            userinfo.Gender = NewUser.Gender;
            //userinfo.IsModerator = false;

            //if (RegUser.SubscriptionType == "PaidUser")
            //{
            //    userinfo.IsTransaction = true;
            //}
            userinfo.IsUsingModeratorCredentials = NewUser.IsUsingModeratorCredentials;
            if (userinfo.IsUsingModeratorCredentials != true)
            {
                if (NewUser.EmailId.Contains("@"))
                {
                    userinfo.PhoneNo = 0;
                    userinfo.EmailId = NewUser.EmailId;
                    userinfo.PreferredBy = 1;
                }
                else
                {
                    userinfo.PhoneNo = Convert.ToInt64(NewUser.EmailId);
                    userinfo.EmailId = null;
                    userinfo.PreferredBy = 2;
                    string charPool = "ABCDEFGOPQRSTUVWXY1234567890ZabcdefghijklmHIJKLMNnopqrstuvwxyz";
                    StringBuilder rs = new StringBuilder();
                    Random random = new Random();

                    for (int i = 0; i < 4; i++)
                    {
                        rs.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
                    }

                    string otaCode = Convert.ToString(rs);
                    DateTime? otaexpire = DateTime.Now;
                    string strDate = DateTime.Now.ToString();
                    DateTime date = DateTime.Parse(strDate);
                    DateTime expireDate = date.AddHours(24);
                    userinfo.OTACode = otaCode;
                    userinfo.OTAExpire = expireDate;
                }

                userinfo.Password = CryptorEngine.Encrypt(NewUser.Password, true);
            }
            else
            {
                userinfo.EmailId = LoginUser.EmailId;
                userinfo.Password = CryptorEngine.Encrypt(LoginUser.Password, true);
                userinfo.PhoneNo = LoginUser.PhoneNo;
                userinfo.PreferredBy = LoginUser.PreferredBy;
            }
            //if (!String.IsNullOrWhiteSpace(RegUser.GroupType))
            //{
            //    if (RegUser.GroupType == "Family or Individual")
            //    {
            //        userinfo.Relationship = "user";
            //        userinfo.RoleName = "user";
            //    }

            //}

            //string groupType = RegUser.GroupType;
            insertedID = UserService.AddUser(userinfo, LoginUser.GroupType);
            if (insertedID == "1041")//added by ck
            {
                //res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Your email id already exists please register with another mail id</h3>";
                res.Data = "1041";
                return res;
            }

            if (insertedID == "1051")
            {
                //res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Your contact number already exists exists please register with another phone number</h3>";
                res.Data = "1051";
                return res;
            }

            if (insertedID != "1041" && insertedID != "1051")
            {
                PersonalInformation p = new PersonalInformation();
                Services3 s3 = new Services3(); // sandeep added on 24-09-2013
                PersonalInformation pinfo = null;
                pinfo = s3.GetPersonalInfoByUserIdAndGroupType(LoginUser.UserId, LoginUser.GroupType);

                p.FirstName = userinfo.FirstName;
                p.LastName = userinfo.LastName;
                p.DomainID = userinfo.DomainId;
                p.UserId = insertedID;

                //p.Country = RegUser.Country;  // sandeep commented on 24-09-2013
                p.Country = pinfo.Country; // sandeep added on 24-09-2013

                p.DateOfBirth = NewUser.DOB;
                p.Gender = NewUser.Gender;

                //p.Contact = RegUser.PhoneNumber; // sandeep commented on 24-09-2013
                if (userinfo.IsUsingModeratorCredentials)
                {
                    p.Contact = pinfo.Contact; // sandeep added on 24-09-2013
                }
                else
                {
                    p.Contact = userinfo.PhoneNo;
                }

                //p.Address = RegUser.Address; // sandeep commented on 24-09-2013
                p.Address = pinfo.Address; // sandeep added on 24-09-2013

                //p.ZipCode = RegUser.Pincode; // sandeep commented on 24-09-2013
                p.ZipCode = pinfo.ZipCode; // sandeep added on 24-09-2013

                p.CreatedBy = LoginUser.UserId;
                int result = 0;
                //if (!String.IsNullOrWhiteSpace(RegUser.GroupType)) // sandeep commented on 24-09-2013
                if (!String.IsNullOrWhiteSpace(LoginUser.GroupType)) // sandeep added on 24-09-2013
                {
                    if (LoginUser.GroupType == "Family or Individual")
                    {
                        //Services3 s3 = new Services3(); // sandeep commented on 24-09-2013
                        result = s3.InsertPersonalInfo(p, LoginUser.GroupType);
                    }
                }
            }

            //code added by kumar
            bool IsSendMail = false;
            // JsonResult ResultData = new JsonResult();
            // LoginAndSignUp user = new LoginAndSignUp();
            // RegisteredUser RegUser = new RegisteredUser();
            if (insertedID != null && insertedID != "1041" && insertedID != "1051" && userinfo.PhoneNo == 0)
            {
                if (userinfo.EmailId != null)
                {
                    #region Mail Sending
                    try
                    {
                        MailMessage mail = new MailMessage();
                        mail.To.Add(userinfo.EmailId);
                        mail.From = new MailAddress("medlogx@gmail.com");
                        mail.Subject = "Registration details";
                        //string Body = "Hi " + regUser.EmailId + System.Environment.NewLine + " your Registration successful " + System.Environment.NewLine + " Your iHealth User account details are: Userid:" + regUser.EmailId + "Passowrd:" + CryptorEngine.Decrypt(regUser.Password, true) + "\n" + "www.medlogx.com/confirmaccount/confirmaccount?userid=" + iHealthinsertedId + " verify here";// isusing Gmail in ASP.NET";
                        //string Body = "Hi please click here to confirm yor account " + System.Environment.NewLine + "\n" + "www.medlogx.com/confirmaccount/confirmaccount?userid=" + iHealthinsertedId + " verify here";// isusing Gmail in ASP.NET";
                        //string Body = "Dear " + userinfo.FirstName + "You have been successfully registered with us. \n Please click on below link to verify your email ID. " + System.Environment.NewLine + "\n" + "107.20.220.222:7814/confirmaccount/confirmaccount?userid=" + insertedID + "You can start using your credentials to login after verfication is successful. \n Thank you, \nMedLogx";// isusing Gmail in ASP.NET";
                        //updated by ck
                        //string Body = "Dear  " + userinfo.FirstName + " ,\nYou have been successfully registered with us. \n Please click on below link to verify your email ID.  " + System.Environment.NewLine + "\n" + "107.20.220.222:6845/confirmaccount/ConfirmSubUserAccount?userid=" + insertedID + "  \n You can start using your credentials to login after verfication is successful. \n Thank you, \nMedLogx";// isusing Gmail in ASP.NET";
                        string Body = "Dear " + userinfo.FirstName + " ,\nYou have been successfully registered with us. \n Please click on below link to verify your email ID. " + System.Environment.NewLine + "\n" + "http:107.20.220.222:7814/confirmaccount/ConfirmSubUserAccount?userid=" + insertedID + "&Type=" + userinfo.GroupType + " \n You can start using your credentials to login after verfication is successful. \n Thank you, \nMedLogx";// isusing Gmail in ASP.NET"; // sandeep changed on 28-09-2013
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
                }
                else
                {
                    string phnno = Convert.ToString(userinfo.PhoneNo);
                    string Uid = "7386041104";
                    string Password = "55312";
                    string Number = phnno;
                    string Message = userinfo.OTACode;
                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + Uid + "&pwd=" + Password +
               "&phone=" + Number + "&msg=" + Message + "&provider=Fullonsms");
                    // Get the response.
                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    // Close the Stream object.
                    respStreamReader.Close();
                    myResp.Close();
                    if (responseString == "1")
                    {
                        res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with MedLogx, you should have login credentials soon in your mobile phone..</h3>";
                    }
                }
                    #endregion
                if (IsSendMail)
                {
                    //ViewBag.ErrorMessage = string.Format("You are registered successfully with MedLogx, you should have login credentials soon in your mailbox..");
                    res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with MedLogx, you should have login credentials soon in your mailbox..</h3>";
                }
                else
                {
                    res.Data = "We are sorry, could not register you successfully. Please check details your entered and try again";
                    //ViewBag.ErrorMessage = string.Format("We are sorry, could not register you successfully. Please check details your entered and try again");
                }
                return res;
            }

            //ends here

            return RedirectToAction("Index", "AccountSettings");
        }

        [HttpPost]
        public ActionResult CreateUserNew(AddUser NewUser)
        {
            string insertedID = string.Empty;
            AddUser userinfo = null;
            userinfo = new AddUser();
            UserInformation LoginUser = new UserInformation();
            LoginUser = Session["LoginUser"] as UserInformation;
            Services1 UserService = null;
            UserService = new Services1();
            SingleRegisterModel RegUser = new SingleRegisterModel();
            RegistrationServiceCalls regService = new RegistrationServiceCalls();
            userinfo.CreatedOn = System.DateTime.Now;
            userinfo.FirstName = NewUser.FirstName;
            userinfo.LastName = NewUser.LastName;
            //userinfo.LoginName = NewUser.LoginName;
            userinfo.Relationship = NewUser.Relationship;
            userinfo.IsVerified = false;//added by usha for verfication
            RegUser = regService.GetSingleRegUserbyID(LoginUser.DomainId.ToString());
            userinfo.DomainId = RegUser.RUserId;
            JsonResult res = new JsonResult();
            if (RegUser.SubscriptionType == "PaidUser")
            {
                userinfo.IsTransaction = true;
            }
            userinfo.IsUsingModeratorCredentials = NewUser.IsUsingModeratorCredentials;
            if (userinfo.IsUsingModeratorCredentials != true)
            {
                userinfo.EmailId = NewUser.EmailId;
                userinfo.Password = CryptorEngine.Encrypt(NewUser.Password, true);
            }
            else
            {
                userinfo.EmailId = RegUser.EmailId;
                userinfo.Password = CryptorEngine.Encrypt(RegUser.Password, true);
            }
            if (!String.IsNullOrWhiteSpace(RegUser.GroupType))
            {
                if (RegUser.GroupType == "Family or Individual")
                {
                    userinfo.Relationship = "user";
                    userinfo.RoleName = "user";
                }

            }

            string groupType = RegUser.GroupType;
            insertedID = UserService.AddUser(userinfo, RegUser.GroupType);
            if (insertedID == "")//added by ck
            {
                res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Your email id exists please sign up with another mail id</h3>";
                return res;
            }


            if (insertedID != null)
            {
                PersonalInformation p = new PersonalInformation();
                p.FirstName = userinfo.FirstName;
                p.LastName = userinfo.LastName;
                p.DomainID = userinfo.DomainId;
                p.UserId = insertedID;
                p.Country = RegUser.Country;
                p.DateOfBirth = RegUser.DOB;
                p.Gender = NewUser.Gender;
                p.Contact = RegUser.PhoneNumber;
                p.Address = RegUser.Address;
                p.ZipCode = RegUser.Pincode;
                p.CreatedBy = insertedID;
                int result = 0;
                if (!String.IsNullOrWhiteSpace(RegUser.GroupType))
                {
                    if (RegUser.GroupType == "Family or Individual")
                    {
                        Services3 s3 = new Services3();
                        result = s3.InsertPersonalInfo(p, RegUser.GroupType);
                    }

                }



            }

            //code added by kumar
            bool IsSendMail = false;
            // JsonResult ResultData = new JsonResult();
            // LoginAndSignUp user = new LoginAndSignUp();
            // RegisteredUser RegUser = new RegisteredUser();
            if (insertedID != null)
            {

                #region Mail Sending
                try
                {
                    //MailMessage mail = new MailMessage();
                    //mail.To.Add(userinfo.EmailId);
                    //mail.From = new MailAddress("medlogx@gmail.com");
                    //mail.Subject = "Registration details";
                    //string Body = "Hi " + regUser.EmailId + System.Environment.NewLine + " your Registration successful " + System.Environment.NewLine + " Your iHealth User account details are: Userid:" + regUser.EmailId + "Passowrd:" + CryptorEngine.Decrypt(regUser.Password, true) + "\n" + "www.medlogx.com/confirmaccount/confirmaccount?userid=" + iHealthinsertedId + " verify here";// isusing Gmail in ASP.NET";
                    //string Body = "Hi please click here to confirm yor account " + System.Environment.NewLine + "\n" + "www.medlogx.com/confirmaccount/confirmaccount?userid=" + iHealthinsertedId + " verify here";// isusing Gmail in ASP.NET";
                    //string Body = "Dear " + userinfo.FirstName + "You have been successfully registered with us. \n Please click on below link to verify your email ID. " + System.Environment.NewLine + "\n" + "107.20.220.222:7814/confirmaccount/confirmaccount?userid=" + insertedID + "You can start using your credentials to login after verfication is successful. \n Thank you, \nMedLogx";// isusing Gmail in ASP.NET";
                    //updated by ck
                    //string Body = "Dear  " + userinfo.FirstName + " ,\nYou have been successfully registered with us. \n Please click on below link to verify your email ID.  " + System.Environment.NewLine + "\n" + "107.20.220.222:6845/confirmaccount/ConfirmSubUserAccount?userid=" + insertedID + "  \n You can start using your credentials to login after verfication is successful. \n Thank you, \nMedLogx";// isusing Gmail in ASP.NET";
                    string Body = "Dear " + userinfo.FirstName + " ,\nYou have been successfully registered with us. \n Please click on below link to verify your email ID. " + System.Environment.NewLine + "\n" + "http://107.20.220.222:7814/confirmaccount/ConfirmSubUserAccount?userid=" + insertedID + " \n You can start using your credentials to login after verfication is successful. \n Thank you, \nMedLogx";// isusing Gmail in ASP.NET";
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


                    RegistrationServiceCalls RegSerCalls = null;
                    RegSerCalls = new RegistrationServiceCalls();
                    SendMail _sendMail = new SendMail();

                    _sendMail.To = userinfo.EmailId;

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
                    //ViewBag.ErrorMessage = string.Format("You are registered successfully with MedLogx, you should have login credentials soon in your mailbox..");
                    res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with MedLogx, you should have login credentials soon in your mailbox..</h3>";
                }
                else
                {
                    res.Data = "We are sorry, could not register you successfully. Please check details your entered and try again";
                    //ViewBag.ErrorMessage = string.Format("We are sorry, could not register you successfully. Please check details your entered and try again");
                }
                return res;
            }

            //ends here

            return RedirectToAction("Index", "AccountSettings");

        }

        //public ActionResult CreateUser(string id) // sandeep commented on 28-09-2013
        public ActionResult CreateSubUser(string id) // sandeep added on 28-09-2013
        {
            AddUser uInfo = new AddUser();
            uInfo.UserId = id;
            return PartialView("AddUser", uInfo);
        }

        public ActionResult EditUser(string UserId)
        {
            Services3 s3 = new Services3();
            Services1 UserService = null;
            UserService = new Services1();
            string url = Request.UrlReferrer.ToString();
            TempData["PreviousPageUrl"] = url;
            AddUser userinfo = new AddUser();
            AddUser usr = new AddUser();
            //UpdateUserDetails userinfo = new UpdateUserDetails();
            //UserInformation usr = new UserInformation();
            PersonalInformation pinfo = new PersonalInformation();
            // string type = "Hospital";
            pinfo = s3.GetPersonalInfo(UserId);
            //usr = UserService.GetUserbyId(Convert.ToInt32(UserId));
            usr = UserService.GetUsrbyId(UserId);
            //usr.Password = CryptorEngine.Decrypt(usr.Password, true);
            userinfo = usr;
            userinfo.FirstName = pinfo.FirstName; //added cs
            userinfo.LastName = pinfo.LastName; //added cs
            userinfo.Gender = pinfo.Gender;
            return PartialView("EditUser", userinfo);
        }

        [HttpPost]
        public ActionResult EditUser(AddUser NewUserDetails)
        {
            // bool successmsg = UpdateUsrDetails(userdet);
            // JsonResult res = new JsonResult();
            //if (successmsg)
            //{
            //    res.Data = "Your details updated successfully";
            //    return res;
            //}
            //else
            //{
            //    res.Data = "We are sorry, we could not update your details";
            //    return res;
            //}


            //added by ck
            AddUser userinfo = null;
            userinfo = new AddUser();
            Services1 UserService = null;
            UserService = new Services1();
            userinfo = UserService.GetUsrbyId(NewUserDetails.UserId);
            RegisteredUser RegUser = new RegisteredUser();
            RegistrationServiceCalls regService = new RegistrationServiceCalls();
            RegUser = regService.GetRegUserbyID(userinfo.DomainId.ToString());
            userinfo.ModifiedOn = System.DateTime.Now;
            userinfo.FirstName = NewUserDetails.FirstName;
            userinfo.LastName = NewUserDetails.LastName;
            userinfo.LoginName = NewUserDetails.LoginName;
            userinfo.Relationship = NewUserDetails.Relationship;
            if (NewUserDetails.IsUsingModeratorCredentials != true)
            {
                if (userinfo.IsUsingModeratorCredentials == true && NewUserDetails.IsUsingModeratorCredentials == false)
                {
                    if (userinfo.EmailId != NewUserDetails.EmailId)
                    {
                        userinfo.EmailId = NewUserDetails.EmailId;
                        userinfo.Password = CryptorEngine.Encrypt(NewUserDetails.Password, true);
                    }

                }
                else
                {
                    userinfo.EmailId = NewUserDetails.EmailId;
                    userinfo.Password = CryptorEngine.Encrypt(NewUserDetails.Password, true);
                }
            }
            else
            {
                userinfo.EmailId = RegUser.EmailId;
                userinfo.Password = RegUser.Password;
            }
            userinfo.IsUsingModeratorCredentials = NewUserDetails.IsUsingModeratorCredentials;
            string userupdate = UserService.UpdateUsrDetails(userinfo, userinfo.UserId);
            JsonResult res = new JsonResult();
            if (userupdate == "1020")
            {
                Services3 s3 = new Services3();
                // string type = "Hospital";
                PersonalInformation pinfo = new PersonalInformation();
                pinfo = s3.GetPersonalInfo(NewUserDetails.UserId);
                pinfo.Gender = NewUserDetails.Gender;
                pinfo.LastName = NewUserDetails.LastName;//added by ck for last name
                int result = s3.UpdatePersonalInfo(pinfo, NewUserDetails.UserId);
            }
            bool IsSendMail = false;
            //if (userupdate == "1020")
            //{
            //    #region Mail Sending
            //    try
            //    {
            //        //MailMessage mail = new MailMessage();
            //        //mail.To.Add(userinfo.EmailId);
            //        //mail.From = new MailAddress("medlogx@gmail.com");
            //        //mail.Subject = "iHealth User Credentials";
            //        //string Body = "Hi " + userinfo.EmailId + System.Environment.NewLine + " your Password changed successful " + System.Environment.NewLine + " Your iHealth User account details are: Userid:" + userinfo.EmailId + "Passowrd:" + CryptorEngine.Decrypt(userinfo.Password, true);// isusing Gmail in ASP.NET";
            //        //mail.Body = Body;
            //        //mail.IsBodyHtml = true;
            //        //SmtpClient smtp = new SmtpClient();
            //        //smtp.Host = "smtp.gmail.com";
            //        //smtp.Port = 587;
            //        //smtp.Credentials = new System.Net.NetworkCredential("medlogx@gmail.com", "medlogx2013");
            //        //smtp.EnableSsl = true;
            //        //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            //        //smtp.Send(mail);
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        return IsSendMail;
            //    }
            //    #endregion
            //}
            //else
            //{
            //    return IsSendMail;
            //}
            //added by ck
            if (userupdate == "1041")
            {
                res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, could not register you successfully. Your email ID is already registered with us.</h3>";
                return res;
            }
            //added by ck
            if (userupdate == "1020")
            {
                res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Your details updated successfully</h3>";
                return res;

            }
            return RedirectToAction("Index", "AccountSettings");
        }

        //public ActionResult EditUser(string UserId)
        //{
        //    Services3 s3 = new Services3();
        //    Services1 UserService = null;
        //    UserService = new Services1();
        //    string url = Request.UrlReferrer.ToString();
        //    TempData["PreviousPageUrl"] = url;
        //    UpdateUserDetails userinfo = new UpdateUserDetails();
        //    UserInformation usr = new UserInformation();
        //    PersonalInformation pinfo = new PersonalInformation();
        //    pinfo = s3.GetPersonalInfo(Convert.ToInt32(UserId));
        //    usr = UserService.GetUserbyId(Convert.ToInt32(UserId));
        //    usr.Password = CryptorEngine.Decrypt(usr.Password, true);
        //    userinfo.IHealthUser = usr;
        //    userinfo.IHealthUser.Gender = pinfo.Gender;
        //    return PartialView("EditUser", userinfo);
        //}

        //[HttpPost]
        //public ActionResult EditUser(UpdateUserDetails userdet)
        //{
        //    bool successmsg = UpdateUserDetails(userdet);
        //    JsonResult res = new JsonResult();
        //    if (successmsg)
        //    {
        //        res.Data = "Your details updated successfully";
        //        return res;
        //    }
        //    else
        //    {
        //        res.Data = "We are sorry, we could not update your details";
        //        return res;
        //    }
        //}

        public ActionResult ChangePassword()
        {
            //string url = Request.UrlReferrer.ToString();
            //TempData["PreviousPageUrl"] = url;
            return PartialView("_ChangePassword");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changepwd)
        {
            if (ModelState.IsValid)
            {
                UserInformation LoginUser = null;
                if (Session["CurrentUser"] != null)
                {
                    LoginUser = Session["CurrentUser"] as UserInformation;
                }
                else
                {

                    LoginUser = Session["LoginUser"] as UserInformation;
                }
                Services1 UserService = null;
                UserService = new Services1();
                UserInformation usr = new UserInformation();
                usr = UserService.ChangeAcPaswdByUserId(LoginUser.UserId, changepwd.oldPassword, changepwd.newPassword);//GetUserbyId(LoginUser.UserId);

                JsonResult res = new JsonResult();

                if (usr.ResultCode == 1020)
                {
                    res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Your password has been changed successfully.</h3>";
                    return res;
                }
                else if (usr.ResultCode == 1021)
                {
                    res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, your password change attempt failed, Please try again.</h3>";
                    return res;
                }
                else if (usr.ResultCode == 1022)
                {
                    res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Choose a password you haven't previously used with this account.</h3>";
                    return res;
                }
                else if (usr.ResultCode == 1023)
                {
                    res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, please ensure your have entered the right old password.</h3>";
                    return res;
                }
                else
                {
                    res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, your password change attempt failed, Please try again.</h3>";
                    return res;
                }

                #region Dileep Commented
                //if (usr.Password == passowrd)
                //{
                //    if (usr.Password != nwpassword)
                //    {
                //        usr.Password = changepwd.newPassword;
                //        _objUserDetails.IHealthUser = usr;
                //        TempData["NewUserDetails"] = _objUserDetails;
                //        TempData["changepassword"] = "true";
                //        bool successmsg = UpdateUserDetails(_objUserDetails);

                //        if (successmsg)
                //        {
                //            //res.Data = "Your password has been changed successfully";
                //            //return res;
                //            // return RedirectToAction("PersonalInfo", "PInfo");
                //            res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Your password has been changed successfully.</h3>";
                //            //return RedirectToAction("PInfo", "PersonalInfo");
                //            return res;
                //        }

                //        else
                //        {
                //            //res.Data = "We are sorry, your password change attempt failed, Please try again";
                //            // return res;
                //            // return RedirectToAction("PersonalInfo", "PInfo");   
                //            res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, your password change attempt failed, Please try again.</h3>";
                //            return res;
                //        }
                //    }
                //    else
                //    {
                //        res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Choose a password you haven't previously used with this account.</h3>";
                //        return res;
                //    }

                //}
                //else
                //{
                //    // JsonResult res = new JsonResult();
                //    // res.Data = "We are sorry, please ensure your have entered the right password";
                //    //return res;
                //    // return RedirectToAction("PersonalInfo", "PInfo");   
                //    res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>We are sorry, please ensure your have entered the right password.</h3>";
                //    return res;
                //}

                #endregion

            }

            return PartialView("_ChangePassword");
        }
        public ActionResult DownloadScan()
        {
            string TargetZipFilePath = @"C:\inetpub\wwwroot\BumpDocs\DownLoads\BumpDocsScanApplication.zip";
            var bytes = System.IO.File.ReadAllBytes(TargetZipFilePath);


            return File(bytes, "application/x-zip-compressed", "BumpDocsScanApplication.zip");//, zipFileName);// DestPdfFilePath
        }

        //public ActionResult feedback()
        //{
        //    return PartialView("feedback");
        //}
        //[HttpPost]
        //public JsonResult ContactUs(ContactUsViewModel cInfo)
        //{
        //    string insertedCode;
        //    Services1 UserService = null;
        //    UserService = new Services1();
        //    insertedCode = UserService.InsertContactInfo(cInfo);

        //    // return RedirectToAction("Home", "Home");
        //    // return View();
        //    JsonResult res = new JsonResult();
        //    if (insertedCode == "1010")
        //    {
        //        res.Data = "We will get back to you soon";
        //        // return Json(res, JsonRequestBehavior.AllowGet);// res;
        //        // return PartialView("feedback",res.D
        //        //return Json(new { res.Data });
        //       return Json(res.Data, JsonRequestBehavior.AllowGet);
        //      // return res;
        //        // return Json(new { Success = true, Status = res.Data });
        //    }

        //    else
        //    {
        //        res.Data = "Error occured in Submitting";
        //        return res;
        //    }
        //}
        //public ActionResult Feedback1(ContactUsViewModel fInfo)
        //{
        //    string insertedCode;
        //    Services1 UserService = null;
        //    UserService = new Services1();
        //    insertedCode = UserService.InsertFeedbackInfo(fInfo);

        //    JsonResult res = new JsonResult();
        //    if (insertedCode == "1010")
        //    {
        //        res.Data = "We will get back to you soon";
        //        return Json(res.Data, JsonRequestBehavior.AllowGet);// res;
        //      //  return res;
        //    }

        //    else
        //    {
        //        res.Data = "Error occured in Submitting";
        //        return res;
        //    }
        //}

        public ActionResult feedback()
        {
            return PartialView("feedback");
        }
        [HttpPost]
        public JsonResult ContactUs(ContactUsViewModel cInfo)
        {
            string insertedCode;
            Services1 UserService = null;
            UserService = new Services1();
            insertedCode = UserService.InsertContactInfo(cInfo);

            // return RedirectToAction("Home", "Home");
            // return View();
            JsonResult res = new JsonResult();
            if (insertedCode == "1010")
            {
                res.Data = "We will get back to you soon";
                // return Json(res, JsonRequestBehavior.AllowGet);// res;
                // return PartialView("feedback",res.D
                //return Json(new { res.Data });
                return Json(res.Data, JsonRequestBehavior.AllowGet);
                // return res;
                // return Json(new { Success = true, Status = res.Data });
            }

            else
            {
                res.Data = "Error occured in Submitting";
                return res;
            }
        }
        [HttpPost]
        public ActionResult Feedback1(ContactUsViewModel fInfo)
        {

            string insertedCode;
            Services1 UserService = null;
            UserService = new Services1();
            insertedCode = UserService.InsertFeedbackInfo(fInfo);

            JsonResult res = new JsonResult();
            if (insertedCode == "1010")
            {
                res.Data = "We will get back to you soon";
                return Json(res.Data, JsonRequestBehavior.AllowGet);// res;
                // return res;
            }

            else
            {
                res.Data = "Error occured in Submitting";
                return res;
            }

        }

        // iHealthUser footer links
        public ActionResult MFHome(string detector)
        {
            return PartialView("MFHome", detector);
        }
        public ActionResult MFServices(string detector)
        {
            return PartialView("MFServices", detector);
        }
        public ActionResult MFSupport(string detector)
        {
            return PartialView("MFSupport", detector);
        }
        public ActionResult MFContact(string detector)
        {
            return PartialView("MFContact", detector);
        }
        public ActionResult HospitalInfo()
        {
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            //HospitalInformation1 hosp = HospitalInformation1.GetHospitalInfoById(LoginUser.UserId);
            HospitalInformation1 hosp = HospitalInformation1.GetHospitalInfoDomainId(LoginUser.DomainId);

            return View("HospitalInfo", hosp);

        }
        public ActionResult DoctorInfo()
        {
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            Services3 s = new Services3();
            //HospitalInformation1 hosp = HospitalInformation1.GetHospitalInfoById(LoginUser.UserId);
            PersonalInformation hosp = s.GetDoctorinfo(LoginUser.UserId);


            return View("DoctorInfo", hosp);

        }

        // sandeep added new code start here on 18-09-2013

        public ActionResult EditUserInfo(string UserId)
        {
            UserInformation userinfo = null;
            userinfo = new UserInformation();

            Services1 UserService = new Services1();

            userinfo = UserService.GetUserbyId(UserId);

            int preferrredby = userinfo.PreferredBy;
            if (preferrredby == 1)
            {
                userinfo.IsEmail = true;
            }
            else if (preferrredby == 2)
            {
                userinfo.IsPhone = true;
            }
            else if (preferrredby == 3)
            {
                userinfo.IsEmail = true;
                userinfo.IsPhone = true;
            }

            return PartialView(userinfo);
        }

        //[HttpPost]
        public ActionResult SaveEditUserInfo(UserInformation user)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation userinfo = null;
            Services1 UserService = new Services1();
            string successcode;

            userinfo = UserService.GetUserbyId(user.UserId);
            userinfo.ModifiedBy = user.UserId;
            //userinfo.PreferredBy = user.PreferredBy;
            if (user.IsEmail && user.IsPhone)
            {
                userinfo.PreferredBy = 3;
            }
            else if (user.IsEmail)
            {
                userinfo.PreferredBy = 1;
            }
            else if (user.IsPhone)
            {
                userinfo.PreferredBy = 2;
            }


            if (user.EmailId == null)
            {
                userinfo.EmailId = null;
                userinfo.IsVerified = false;
            }
            //else if(user.EmailId == userinfo.EmailId)
            //{

            //}
            else if (user.EmailId != userinfo.EmailId)
            {
                userinfo.IsVerified = false;
            }

            if (user.PhoneNo == null) // sandeep changed on 28-09-2013
            {
                userinfo.PhoneNo = null; // sandeep changed on 28-09-2013
                userinfo.IsPhnVerified = false;
                userinfo.OTACode = null;
                userinfo.OTAExpire = null;
            }
            //else if (user.PhoneNo == userinfo.PhoneNo)
            //{

            //}
            else if (user.PhoneNo != userinfo.PhoneNo)
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
                string strDate = userinfo.CreatedOn.ToString();
                DateTime date = DateTime.Parse(strDate);
                DateTime expireDate = date.AddHours(24);
                userinfo.PhoneNo = user.PhoneNo;
                userinfo.IsPhnVerified = false;
                userinfo.OTACode = otaCode;
                userinfo.OTAExpire = expireDate;
            }

            //if (userinfo.IsVerified)
            //{
            //    userinfo.PreferredBy = "1";
            //}
            //if (userinfo.IsPhnVerified)
            //{
            //    userinfo.PreferredBy = "2";
            //}
            //if (userinfo.IsVerified && userinfo.IsPhnVerified)
            //{
            //    userinfo.PreferredBy = "3";
            //}
            //if (!userinfo.IsVerified && !userinfo.IsPhnVerified)
            //{
            //    userinfo.PreferredBy = null;
            //}


            successcode = UserService.UpdateUserAccountDetails(userinfo, userinfo.UserId);

            if (successcode == "1020")
            {
                if (user.EmailId != null)
                {
                    if (userinfo.EmailId != user.EmailId)
                    {
                        bool IsSendMail = false;
                        try
                        {
                            MailMessage mail = new MailMessage();
                            mail.To.Add(userinfo.EmailId);
                            mail.From = new MailAddress("medlogx@gmail.com");
                            mail.Subject = "Registration details";
                            string Body = "Dear " + userinfo.FirstName + " ,\nYou have been successfully registered with us. \n Please click on below link to verify your email ID. " + System.Environment.NewLine + "\n" + "http:107.20.220.222:7814/confirmaccount/ConfirmSubUserAccount?userid=" + userinfo.UserId + "&Type=" + userinfo.GroupType + " \n You can start using your credentials to login after verfication is successful. \n Thank you, \nMedLogx";// isusing Gmail in ASP.NET";
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
                        if (IsSendMail)
                        {
                            ResultData.Data = "Message has been sent to your registered mailid";
                        }
                        else
                        {
                            ResultData.Data = "Message sending failed";
                        }
                    }
                }

                if (user.PhoneNo != 0)
                {
                    if (userinfo.PhoneNo != user.PhoneNo)
                    {
                        string phnno = Convert.ToString(userinfo.PhoneNo);
                        string Uid = "7386041104";
                        string Password = "55312";
                        string Number = phnno;
                        string Message = userinfo.OTACode;
                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + Uid + "&pwd=" + Password +
                   "&phone=" + Number + "&msg=" + Message + "&provider=Fullonsms");
                        // Get the response.
                        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                        string responseString = respStreamReader.ReadToEnd();
                        // Close the Stream object.
                        respStreamReader.Close();
                        myResp.Close();
                        if (responseString == "1")
                        {
                            ResultData.Data = "Message Has been sent to your mobile";
                        }
                    }
                }
            }

            if (successcode == "1041")
            {
                ResultData.Data = "Email already exist";
            }
            else if (successcode == "1051")
            {
                ResultData.Data = "Phone number already exist";
            }
            else if (successcode == "1020")
            {
                ResultData.Data = "successfully added";
            }
            else
            {
                ResultData.Data = "Insertion failed";
            }
            return ResultData;

            //_loginAndSignUp.IHealthUser = UserService.GetUserbyId(UserId);
            //return View("Index");
        }

        public ActionResult VerifyPhone(string UserId)
        {
            UserInformation userinfo = null;
            userinfo = new UserInformation();

            Services1 UserService = new Services1();

            userinfo = UserService.GetUserbyId(UserId);

            return PartialView(userinfo);
        }

        //[HttpPost]
        public ActionResult SaveVerifyPhone(UserInformation user)
        {
            JsonResult ResultData = new JsonResult();
            Services1 userservice = new Services1();
            string scode = userservice.VerifyMobile(user);

            if (scode == "1020")
            {
                ResultData.Data = "Your Confirmation Code Verified Successfully";
            }
            else if (scode == "1069")
            {
                ResultData.Data = "Your Confirmation Code is Invalid";

            }
            return ResultData;

        }

        //[HttpPost]
        public ActionResult ResendOtaCode(string UserId)
        {
            JsonResult ResultData = new JsonResult();
            Services1 userservice = new Services1();


            string successcode = userservice.ResendConfirmationCode(UserId);

            if (successcode == "1080")
            {
                ResultData.Data = "Your Confirmations code is successfully sent to your mail id";

            }
            else if (successcode == "1081")
            {
                ResultData.Data = "Mail Sending Failed";
            }
            else if (successcode == "1090")
            {
                ResultData.Data = "Your Confirmation code is successfully sent to your mobile number";
            }
            else if (successcode == "1091")
            {
                ResultData.Data = "Message sent Failed";
            }




            return ResultData;
        }

        public ActionResult ResendMail(string UserId)
        {
            JsonResult ResultData = new JsonResult();
            Services1 userservice = new Services1();


            string successcode = userservice.ResendConfirmationCode(UserId);

            if (successcode == "1080")
            {
                ResultData.Data = "Your Confirmations code is successfully sent to your mail id";

            }
            else if (successcode == "1081")
            {
                ResultData.Data = "Mail Sending Failed";
            }
            else if (successcode == "1090")
            {
                ResultData.Data = "Your Confirmation code is successfully sent to your mobile number";
            }
            else if (successcode == "1091")
            {
                ResultData.Data = "Message sent Failed";
            }


            return ResultData;


        }

        // sandeep added new code end here on 18-09-2013

        public ActionResult AddEmail()
        {
            UserInformation userinfo = null;

            if (Session["CurrentUser"] != null)
            {
                userinfo = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                userinfo = Session["LoginUser"] as UserInformation;
            }
            return PartialView(userinfo);
        }

        [HttpPost]
        public ActionResult AddEmail(UserInformation usr)
        {
            JsonResult ResultData = new JsonResult();


            Services1 UserService = new Services1();
            string successcode = UserService.AddEmail(usr);
            if (successcode == "1080")
            {
                ResultData.Data = "Your Confirmation code is sent to your email ";
            }
            else if (successcode == "1081")
            {
                ResultData.Data = "Mail Sending Failed";
            }
            else if (successcode == "1041")
            {
                ResultData.Data = "Your mail id is already existed";
            }
            else
            {
                ResultData.Data = "Failed";
            }

            return ResultData;
        }

        public ActionResult AddMobileNumber()
        {
            UserInformation userinfo = null;

            if (Session["CurrentUser"] != null)
            {
                userinfo = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                userinfo = Session["LoginUser"] as UserInformation;
            }
            return PartialView(userinfo);
        }
        [HttpPost]
        public ActionResult AddMobileNumber(UserInformation usr)
        {
            JsonResult ResultData = new JsonResult();


            string successcode = Services1.AddMobile(usr);
            if (successcode == "true")
            {
                ResultData.Data = "Message has been sent to your mobile number";

            }
            else if (successcode == "false")
            {
                ResultData.Data = "Message sending failed";
            }
            else if (successcode == "1051")
            {
                ResultData.Data = "Phone Number Already Existed";
            }
            else
            {
                ResultData.Data = "Failed";

            }
            return ResultData;
        }

        public ActionResult DeleteMobileNumber()
        {
            UserInformation userinfo = null;

            if (Session["CurrentUser"] != null)
            {
                userinfo = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                userinfo = Session["LoginUser"] as UserInformation;
            }
            JsonResult ResultData = new JsonResult();


            string successcode = Services1.DeleteMobile(userinfo);
            if (successcode == "1020")
            {
                ResultData.Data = "Mobile number deleted successfully";

            }
            else
            {
                ResultData.Data = "Deletion failed";
            }
            
          
            return ResultData;
        }
        public ActionResult VerifyEmail(string UserId)
        {
            UserInformation userinfo = null;
            userinfo = new UserInformation();

            Services1 UserService = new Services1();

            userinfo = UserService.GetUserbyId(UserId);

            return PartialView(userinfo);
        }

        public ActionResult SaveVerifyEmail(UserInformation user)
        {
            JsonResult ResultData = new JsonResult();
            Services1 userservice = new Services1();

            string scode = userservice.VerifyEmail(user);

            if (scode == "1020")
            {
                ResultData.Data = "Your Confirmation Code Verified Successfully";
            }
            else if (scode == "1069")
            {
                ResultData.Data = "Your Confirmation Code is Invalid";

            }
            return ResultData;
        }

        public ActionResult ChangePreferedby(string UserId, string cvalue, string type)
        {
            JsonResult ResultData = new JsonResult();
            Services1 ser = new Services1();

            string result = ser.ChangePreferedby(UserId, cvalue, type);
            if (result == "1020")
            {
                ResultData.Data = "Your Changes updted successfully";
            }
            return ResultData;
        }


        public ActionResult TipSendinGmail(string UserId, string cvalue)
        {
            Services1 service = new Services1();
            string result = service.TipSendinByGmail(UserId, cvalue);
            if (result == "1020")
            {
                return Json("Your Changes updted successfully", JsonRequestBehavior.AllowGet);
            }
            return Json("Your changes not updted successfully", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailySendinGmail(string UserId, string cvalue)
        {

            Services1 service = new Services1();

            string result = service.DailySendinByGmail(UserId, cvalue);
            if (result == "1020")
            {
                return Json("Your Changes updted successfully", JsonRequestBehavior.AllowGet);
            }
            return Json("Your changes not updted successfully", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailySendinSms(string UserId, string cvalue)
        {

            Services1 service = new Services1();

            string result = service.DailySendinBySms(UserId, cvalue);
            if (result == "1020")
            {
                return Json("Your Changes updted successfully", JsonRequestBehavior.AllowGet);
            }
            return Json("Your changes not updted successfully", JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertContact(ContactUs c)
        {
            UserInformation userinfo = null;

            if (Session["CurrentUser"] != null)
            {
                userinfo = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                userinfo = Session["LoginUser"] as UserInformation;
            }
            c.EmailId = userinfo.EmailId;
            string insertedId = CreateContactInfo.InsertContactInfo(c);
            JsonResult res = new JsonResult();
            string[] resarr = new string[2];
            resarr[0] = insertedId;
            if (insertedId != "0")
            {
                if (c.Type != null)
                {
                    if (c.Type == "Praise")
                    {
                        resarr[1] = "Praise";
                        res.Data = resarr;
                    }
                    else if (c.Type == "Support Request" || c.Type == "Bug Report")
                    {
                        resarr[1] = "Support Request";
                        res.Data = resarr;
                    }

                    else if (c.Type == "Sales Request")
                    {
                        resarr[1] = "Sales Request";
                        res.Data = resarr;
                    }
                    else if (c.Type == "Feedback")
                    {
                        resarr[1] = "Feedback";
                        res.Data = resarr;
                    }
                }
                else
                {
                    resarr[1] = "Praise";
                    res.Data = resarr;
                }

            }
            return res;

        }


        public ActionResult CreateDoulaUser(string id) // sandeep added on 28-02-2014
        {
            DoulaUser doulaUserInfo = new DoulaUser();

            doulaUserInfo.iHealthUserId = id;
            return PartialView("AddDoulaUser", doulaUserInfo);
        }

        [HttpPost]
        public ActionResult SaveDoulaUser(DoulaUser newDoulaUser) // sandeep added on 28-02-2014
        {
            Services1 service = new Services1();

            string insertedID = string.Empty;
            DoulaUser userinfo = null;
            userinfo = new DoulaUser();
            userinfo.Email = newDoulaUser.Email;
            userinfo.PhoneNo = newDoulaUser.PhoneNo;
            userinfo.DoulaType = newDoulaUser.DoulaType;
            userinfo.LastName = newDoulaUser.LastName;
            userinfo.DoulaType = newDoulaUser.DoulaType;
            userinfo.FirstName = newDoulaUser.FirstName;
            userinfo.iHealthUserId = newDoulaUser.iHealthUserId;
            userinfo.IsVerified = true;




            insertedID = service.AddDoulaUser(userinfo);



            return RedirectToAction("Index", "AccountSettings");

        }

        public ActionResult ViewDoulaUser(string UserId, string iHealthUser)
        {
            DoulaUser doula = new DoulaUser();

            Services1 userService = new Services1();
            doula = userService.GetDoulaUserById(UserId);
            List<DoulaiHealthUser> _lstdoulaihealthuser = doula.DoulaiHealthUser;

            foreach (DoulaiHealthUser doulaihealthuser in _lstdoulaihealthuser)
            {
                if (doulaihealthuser.iHealthUserId == iHealthUser)
                {
                    doula.FirstName = doulaihealthuser.FirstName;
                    doula.LastName = doulaihealthuser.LastName;
                    doula.DoulaType = doulaihealthuser.DoulaType;
                }
            }

            return PartialView("AddDoulaUser", doula);
        }

        public ActionResult DeleteDoulaUser(string UserId, string iHealthUserId)
        {
            //string result = string.Empty;

            //Services1 userService = new Services1();
            //result = userService.DeleteDoulaByUserIdAndDoulaId(UserId, iHealthUserId);

            //JsonResult json = new JsonResult();

            //if (result == "1030")
            //{
            //    json.Data = "1030";
            //}
            //else
            //{
            //    json.Data = "1031";
            //}

            //return json;

            string result = string.Empty;
            int count = 1;

            string[] _lstUserId = UserId.Split(',');
            int totalcount = _lstUserId.Length;

            foreach (string user in _lstUserId)
            {
                if (user != "")
                {
                    Services1 userService = new Services1();
                    result = userService.DeleteDoulaByUserIdAndDoulaId(user, iHealthUserId);
                    if (result == "1030")
                    {
                        count += 1;
                    }
                }
            }

            JsonResult json = new JsonResult();

            if (count == totalcount)
            {
                json.Data = "1030";
            }
            else if (count > 1)
            {
                json.Data = "1032";
            }
            else
            {
                json.Data = "1031";
            }

            return json;


        }

        public ActionResult EmailVerification(string email)
        {
            Services1 service = new Services1();
            bool status = false;

            string scode = service.Validateemail(email);

            if (scode == "Found")
            {
                status = false;
            }
            else
            {
                if (Session["DoulaUser"] != "")
                {
                    LoginAndSignUp _loginSignup = new LoginAndSignUp();
                    _loginSignup = Session["DoulaUser"] as LoginAndSignUp;

                    IList<DoulaUser> _lstdoulauser = _loginSignup.DoulaUser;

                    foreach (DoulaUser doula in _lstdoulauser)
                    {
                        if (doula.Email == email)
                        {
                            status = false;
                        }
                        else
                        {
                            status = true;
                        }
                    }
                }
                else
                {
                    status = true;
                }
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

    }
}
