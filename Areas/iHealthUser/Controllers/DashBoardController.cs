using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Models;
using System.IO;
using System.Web.Security;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers {
    [SessionExpireFilter]
    public class DashBoardController : Controller {
        UserInformation LoginUser = null;
        //[Authorize]
        public ActionResult DashBoard(LoginAndSignUp _logSignUp) {
            Services1 UsrService = new Services1();
            string currentpage = "DashBoard";
            Session["CurrentPage"] = currentpage;
            if (Session["CurrentUser"] != null) {
                LoginUser = Session["CurrentUser"] as UserInformation;
            } else {
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
            if (DomainUser.GroupType == "Family or Individual" && DomainUser.IsGroupUser == true) {

                Session["UsesrList"] = UsrService.Userslist(pinfo.DomainID, pinfo.UserId);

            }
            if (pinfo.LastName != null) {
                ViewBag.UserName = pinfo.FirstName + " " + pinfo.LastName;
            } else {
                ViewBag.UserName = pinfo.FirstName;
            }
            string userImage = new Services3().GetPersonalInfo(LoginUser.UserId).ImageName;
            //string path = Path.GetFullPath(userImage);
            string userImage1 = "https://www.bumpdocs.com/Uploads/" + userImage;

            Session["userimage"] = userImage != "" ? userImage1 : "../../Images/default_female.png";
            var username = HttpContext.User.Identity.Name;
            OnlineUsers.AddOnlineUsers(LoginUser.UserId, LoginUser.LoginName);
            return RedirectToAction("Index", "Medwall");
        }

        public ActionResult ChangeUser(string hdSelectedID) {
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


            Session["CurrentUser"] = usr;
            Session["UsesrList"] = UsrService.Userslist(usr.DomainId, usr.UserId);
            string currentpage = Session["CurrentPage"] as string;

            if (currentpage == "HealthLog") {
                return RedirectToAction("MyLogs", "HealthLog");
            } else if (currentpage == "PersonalInfo") {
                return RedirectToAction("PInfo", "PersonalInfo");
            } else {
                return View("DashBoard", _loginandSignUp);
            }
        }

        public ActionResult LogOut() {
            if (Session["LoginUser"] != null) {
                UserInformation user = (UserInformation)Session["LoginUser"];
                user.IsLoggedIn = false;
                user.UserSessionID = "";
                Services1 s1 = new Services1();
                string result = s1.UpdateLoginStatus(user);
                string tempDir = "C:\\InndocsiHealth\\PDFFiles\\" + user.UserId;
                string[] files = null;
                if (Directory.Exists(tempDir)) {
                    files = Directory.GetFiles(tempDir);
                    foreach (string file in files) {
                        if (System.IO.File.Exists(file)) {
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
            return Redirect("/home/home");
        }
    }
}
