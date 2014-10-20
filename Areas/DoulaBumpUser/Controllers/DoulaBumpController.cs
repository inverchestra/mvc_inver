using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Models;
using System.IO;
using System.Web.Security;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;

namespace InnDocs.iHealth.Web.UI.Areas.DoulaBumpUser.Controllers
{
     [SessionExpireFilter]
    public class DoulaBumpController : Controller
    {
        //
        // GET: /DoulaUser/Doula/
        UserInformation LoginUser = null;
        public ActionResult Index()
        {
            Services1 UsrService = new Services1();
            string currentpage = "DashBoard";
            Session["CurrentPage"] = currentpage;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            LoginUser.GroupType = "Family or Individual";

            PersonalInformation pinfo = new PersonalInformation();
            Services3 s3 = new Services3();
            pinfo = s3.GetPersonalInfo(LoginUser.UserId);


            SingleRegisterModel DomainUser = new SingleRegisterModel();
            RegistrationServiceCalls RegService = new RegistrationServiceCalls();
            DomainUser = RegService.GetSingleRegUserbyID(LoginUser.DomainId.ToString());


            LoginAndSignUp _loginandSignUp = null;
            _loginandSignUp = new LoginAndSignUp();
            UserInformation usr = null;
            usr = new UserInformation();
            usr = UsrService.GetUserbyIdNew(LoginUser.UserId, LoginUser.GroupType);
            _loginandSignUp.IHealthUser = usr;
            if (DomainUser.GroupType == "Family or Individual" && DomainUser.IsGroupUser == true)
            {

                Session["UsesrList"] = UsrService.Userslist(pinfo.DomainID, pinfo.UserId);

            }
            if (pinfo.LastName != null)
            {
                ViewBag.UserName = pinfo.FirstName + " " + pinfo.LastName;
            }
            else
            {
                ViewBag.UserName = pinfo.FirstName;
            }
            string userImage = new Services3().GetPersonalInfo(LoginUser.UserId).ImageName;
            //string path = Path.GetFullPath(userImage);
            string userImage1 = "https://www.bumpdocs.com/Uploads/" + userImage;

            Session["userimage"] = userImage != "" ? userImage1 : "../../Images/default-user.png";
            return RedirectToAction("Index", "DoulaMedwall");
        }

        public ActionResult LogOut()
        {
            if (Session["LoginUser"] != null)
            {
                UserInformation user = (UserInformation)Session["LoginUser"];
                user.IsLoggedIn = false;
                Services1 s1 = new Services1();
                string result = s1.UpdateLoginStatus(user);
                string tempDir = "C:\\InndocsiHealth\\PDFFiles\\" + user.UserId;
                string[] files = null;
                if (Directory.Exists(tempDir))
                {
                    files = Directory.GetFiles(tempDir);
                    foreach (string file in files)
                    {
                        if (System.IO.File.Exists(file))
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                    Directory.Delete(tempDir, true);
                }
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
            }

            FormsAuthentication.SignOut();
            return Redirect("~/home/home");
        }

        public ActionResult ChangeUser(string hdSelectedID)
        {
            DoulaUser _doula = null;
            LoginUser = new UserInformation();
            LoginUser = Session["LoginUser"] as UserInformation;
            Services1 UsrService = new Services1();
            LoginAndSignUp _loginandSignUp = null;
            _loginandSignUp = new LoginAndSignUp();
            UserInformation usr = null;
            usr = new UserInformation();
            usr = UsrService.GetUserbyId(hdSelectedID);
            _loginandSignUp.IHealthUser = usr;
            //ViewBag.ChangedUser1 = _loginandSignUp.IHealthUser.LoginName;

            _doula = Session["DoulaUser"] as DoulaUser;
            Session["CurrentUser"] = usr;
            Session["LoginName"] = usr.LoginName;
            Session["UsersList"] = UsrService.UserslistByDoula(_doula.DoulaUserId,usr.UserId);
            string userImage = new Services3().GetPersonalInfo(usr.UserId).ImageName;
            //string path = Path.GetFullPath(userImage);
            string userImage1 = "https://www.bumpdocs.com/Uploads/" + userImage;

            Session["userimage"] = userImage != "" ? userImage1 : "../../Images/default-user.png";
            string currentpage = Session["CurrentPage"] as string;

            if (currentpage == "HealthLog")
            {
                return RedirectToAction("MyLogs", "HealthLog");
            }
            else if (currentpage == "PersonalInfo")
            {
                return RedirectToAction("DoulaPInfo", "DoulaPersonal");
            }
            else
            {
                //return View("Index", _loginandSignUp);
                return RedirectToAction("Index", "DoulaMedwall");
            }

        }

        public ActionResult ChangePassword()
        {

            return PartialView("_ChangePassword");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changepwd)
        {
            if (ModelState.IsValid)
            {
                DoulaUser LoginDoulaUser = null;

                LoginDoulaUser = Session["DoulaUser"] as DoulaUser;

                Services1 UserService = null;
                UserService = new Services1();
                DoulaUser usr = new DoulaUser();
                usr = UserService.ChangePaswdByDoulaUserId(LoginDoulaUser.DoulaUserId, changepwd.oldPassword, changepwd.newPassword);//GetUserbyId(LoginUser.UserId);

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



            }

            return PartialView("_ChangePassword");
        }
    }
}
