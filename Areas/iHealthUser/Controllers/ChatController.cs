using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get;
using InnDocs.iHealth.Web.UI.Models;
using System;
namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        Services1 servic1 = new Services1();
        UserInformation usr = null;
        public ActionResult Index()
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }
            var activeUsersCount = servic1.GetAllActiveUsersCount();

            var gestationUsersCount = servic1.GetUsersbyGestationCount(usr.UserId);

            var interesrUsersCount = servic1.GetUsersbyInterestsCount(usr.UserId);

            var countryUsersCount = servic1.GetUsersbyCountryCount(usr.UserId);

            var pinCodeUsersCount = servic1.GetUsersByPinCodeCount(usr.UserId);

            Filters filter = new Filters();
            filter = GetFilters.GetFiltersById(usr.UserId);

            ViewBag.FilterText = filter.Filtertext;
            ViewBag.Interests = filter.Interests;

            ViewBag.FilterText = filter.Filtertext;
            ViewBag.Interests = filter.Interests;

            ViewBag.PeersCount = activeUsersCount;
            ViewBag.GestationCount = gestationUsersCount;

            ViewBag.IntrestCount = interesrUsersCount;

            if (countryUsersCount > 0)
            {
                ViewBag.CountryCount = countryUsersCount - 1;
            }
            else
            {
                ViewBag.CountryCount = countryUsersCount;
            }
            if (pinCodeUsersCount > 0)
            {

                ViewBag.PinCount = pinCodeUsersCount - 1;
            }
            else
            {
                ViewBag.PinCount = pinCodeUsersCount;
            }


            return View();
        }

        public ActionResult GetAllUsers(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }


            var activeUsersCount = servic1.GetAllActiveUsersCount();

            var gestationUsersCount = servic1.GetUsersbyGestationCount(usr.UserId);

            var interesrUsersCount = servic1.GetUsersbyInterestsCount(usr.UserId);

            var countryUsersCount = servic1.GetUsersbyCountryCount(usr.UserId);

            var pinCodeUsersCount = servic1.GetUsersByPinCodeCount(usr.UserId);
            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;
            IList<UserInformation> _usrInfo = servic1.GetAlliHealthUsersPageWise(startlimit, pagesize).Where(item => item.UserId != usr.UserId && item.FirstName != null).OrderByDescending(x => x.IsLoggedIn).ToList();
            IList<User> _user = new List<User>();
            foreach (var item in _usrInfo)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId
                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;
            usrModel.gestCount = gestationUsersCount;
            usrModel.CountryCount = countryUsersCount - 1;
            usrModel.interestscount = interesrUsersCount;
            usrModel.pincodeCount = pinCodeUsersCount - 1;
            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = activeUsersCount - 1;
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllOnlineUsers(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }




            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;


            IList<UserInformation> _usrInfo = servic1.GetOnlineUsersPagewise(startlimit, pagesize).Where(item => (item.UserId != usr.UserId && item.FirstName != null)).ToList();
            IList<User> _user = new List<User>();
            foreach (var item in _usrInfo)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId



                });

            }
            var onlineUsersCount = servic1.GetOnlineActiveUsersCount();

            var gestationUsersCount = servic1.GetOnlineGestationCount(usr.UserId); ;

            var interesrUsersCount = servic1.GetOnlineInterestsCount(usr.UserId);

            var countryUsersCount = servic1.GetOnlineCountryCount(usr.UserId);

            var pinCodeUsersCount = servic1.GetOnlinePinCodeCount(usr.UserId);
            UserModel usrModel = new UserModel();
            usrModel.PageSize = pagesize;
            usrModel.Users = _user;
            usrModel.gestCount = gestationUsersCount;
            if (countryUsersCount > 0)
            {
                usrModel.CountryCount = countryUsersCount - 1;
            }
            else
            {
                usrModel.CountryCount = countryUsersCount;
            }
            if (pinCodeUsersCount > 0)
            {

                usrModel.pincodeCount = pinCodeUsersCount - 1;
            }
            else
            {
                usrModel.pincodeCount = pinCodeUsersCount;
            }
            usrModel.interestscount = interesrUsersCount;

            usrModel.PageNumber = PageNumber;
            if (onlineUsersCount > 0)
            {

                usrModel.TotalCount = onlineUsersCount - 1;
            }
            else
            {
                usrModel.TotalCount = onlineUsersCount;
            }

            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetAllUsersByCountry(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }
            var countryUsersCount = servic1.GetUsersbyCountryCount(usr.UserId);
            //IList<UserInformation> _usrInfo = servic1.GetUsersbyCountry(usr.UserId).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            //IList<User> _user = new List<User>();
            //foreach (var item in _usrInfo)
            //{

            //    _user.Add(new User()
            //    {
            //        Name = item.FirstName,
            //        Id = item.UserId,
            //        //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
            //        email = item.RoleName + " " + "Weeks",
            //        country = item.Country,
            //        img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename



            //    });

            //}

            //return Json(_user, JsonRequestBehavior.AllowGet);

            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;
            IList<UserInformation> _usrInfo = servic1.GetCountryUsersbyPagewise(usr.UserId, startlimit, pagesize).Where(item => (item.UserId != usr.UserId && item.FirstName != null)).ToList();
            IList<User> _user = new List<User>();
            foreach (var item in _usrInfo)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId



                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = countryUsersCount - 1;
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetOnlineUsersByCountry(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }

            //IList<UserInformation> _usrInfo = servic1.GetUsersbyCountry(usr.UserId).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            //IList<User> _user = new List<User>();
            //foreach (var item in _usrInfo)
            //{

            //    _user.Add(new User()
            //    {
            //        Name = item.FirstName,
            //        Id = item.UserId,
            //        //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
            //        email = item.RoleName + " " + "Weeks",
            //        country = item.Country,
            //        img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename



            //    });

            //}

            //return Json(_user, JsonRequestBehavior.AllowGet);

            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;
            IList<UserInformation> _usrInfo = servic1.GetOnlineUsersbyCountryPageWise(usr.UserId, startlimit, pagesize).Where(item => (item.UserId != usr.UserId && item.FirstName != null)).ToList();
            IList<User> _user = new List<User>();
            foreach (var item in _usrInfo)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId



                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = servic1.GetOnlineCountryCount(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetRecentActiveUsers(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }


            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;





            var users = GroupService.GetRecentActiveInvitationsByUser(usr.UserId).ToList();

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId




                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;
            usrModel.gestCount = GroupService.GetRecentCountByGestation(usr.UserId);
            usrModel.CountryCount = GroupService.GetRecentCountByCountry(usr.UserId);
            usrModel.interestscount = GroupService.GetRecentCountByInterests(usr.UserId);
            usrModel.pincodeCount = GroupService.GetRecentCountByPincode(usr.UserId);
            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = GroupService.GetAllRecentCountByUser(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetRecentUsersByCountry(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }


            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;





            var users = GroupService.GetRecentActiveInvitationsByUserCountry(usr.UserId).ToList();

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId




                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = GroupService.GetRecentCountByCountry(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRecentUsersByPincode(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }


            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;





            var users = GroupService.GetRecentActiveUsersByPincode(usr.UserId).ToList();

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId




                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = GroupService.GetRecentCountByPincode(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRecentUsersByGestation(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }


            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;





            var users = GroupService.GetRecentActiveUsersByGestation(usr.UserId).ToList();

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId




                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = GroupService.GetRecentCountByGestation(usr.UserId); ;
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRecentActiveUsersByFilters(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }


            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;





            var users = GroupService.GetRecentActiveUsersByFilters(usr.UserId).ToList();

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId




                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = GroupService.GetRecentCountByFilters(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRecentActiveUsersByInterests(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }


            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;





            var users = GroupService.GetRecentActiveUsersByInterests(usr.UserId).ToList();

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId




                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = GroupService.GetRecentCountByInterests(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllUsersByPinCode(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }
            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;

            var pinCodeUsersCount = servic1.GetUsersByPinCodeCount(usr.UserId);
            IList<User> _user = new List<User>();
            IList<UserInformation> _usrInfo = servic1.GetAllUsersbyPincode(usr.UserId, startlimit, pagesize).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            foreach (var item in _usrInfo)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
                    email = item.RoleName + " " + "Weeks",
                    country = item.Country,
                    img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename

                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = pinCodeUsersCount - 1;
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetOnlineUsersbyPincode(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }
            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;

            IList<User> _user = new List<User>();
            IList<UserInformation> _usrInfo = servic1.GetOnlineUsersbyPincode(usr.UserId, startlimit, pagesize).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            foreach (var item in _usrInfo)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
                    email = item.RoleName + " " + "Weeks",
                    country = item.Country,
                    img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename

                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = servic1.GetOnlinePinCodeCount(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllUsersByGestation(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }
            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;
            var gestUsersCount = servic1.GetUsersbyGestationCount(usr.UserId);
            IList<User> _user = new List<User>();
            IList<UserInformation> _usrInfo = servic1.GetAllUsersbyGestation(usr.UserId, startlimit, pagesize).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            foreach (var item in _usrInfo)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
                    email = item.RoleName + " " + "Weeks",
                    country = item.Country,
                    img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename

                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = gestUsersCount;
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetOnlineUsersbyGestation(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }
            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;

            IList<User> _user = new List<User>();
            IList<UserInformation> _usrInfo = servic1.GetOnlineUsersbyGestation(usr.UserId, startlimit, pagesize).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            foreach (var item in _usrInfo)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
                    email = item.RoleName + " " + "Weeks",
                    country = item.Country,
                    img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename

                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = servic1.GetOnlineGestationCount(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAllUsersByFilterText(string filters, int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }
            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;
            IList<UserInformation> _GetusersByFilter = servic1.GetAllUsersyFilters(filters, startlimit, pagesize, usr.UserId).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            IList<User> _user = new List<User>();
            foreach (var item in _GetusersByFilter)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
                    email = item.RoleName + " " + "Weeks",
                    country = item.Country,
                    img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename

                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = servic1.GetAllFiltersCount(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetonlineUsersyFilters(string filters, int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }
            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;
            IList<UserInformation> _GetusersByFilter = servic1.GetonlineUsersyFilters(filters, startlimit, pagesize, usr.UserId).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            IList<User> _user = new List<User>();
            foreach (var item in _GetusersByFilter)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
                    email = item.RoleName + " " + "Weeks",
                    country = item.Country,
                    img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename

                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = servic1.GetAllFiltersCount(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllUsersbyInterests(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;

            }
            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;
            var interesrUsersCount = servic1.GetUsersbyInterestsCount(usr.UserId);

            IList<UserInformation> _GetUsersbyInterests = servic1.GetAllUsersbyInterests(usr.UserId, startlimit, pagesize).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            IList<User> _user = new List<User>();
            foreach (var item in _GetUsersbyInterests)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
                    email = item.RoleName + " " + "Weeks",
                    country = item.Country,
                    img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename


                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = interesrUsersCount;
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetonlineUsersbyInterests(int? page)
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;

            }
            int PageNumber = (page ?? 1);
            int pagesize = 15;
            int startlimit = (PageNumber - 1) * pagesize;

            IList<UserInformation> _GetUsersbyInterests = servic1.GetonlineUsersbyInterests(usr.UserId, startlimit, pagesize).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            IList<User> _user = new List<User>();
            foreach (var item in _GetUsersbyInterests)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    //email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
                    email = item.RoleName + " " + "Weeks",
                    country = item.Country,
                    img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename


                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;

            usrModel.PageNumber = PageNumber;
            usrModel.TotalCount = servic1.GetOnlineInterestsCount(usr.UserId);
            usrModel.PageSize = 15;
            usrModel.TotalPages = (int)Math.Ceiling(usrModel.TotalCount / (double)usrModel.PageSize);
            return Json(usrModel, JsonRequestBehavior.AllowGet);

        }

        //added by jagadeesh
        public ActionResult GetAllGestationUsers()
        {
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                usr = Session["LoginUser"] as UserInformation;
            }
            IList<UserInformation> _GestationusrInfo = servic1.GetAllGestationUsers(usr.UserId).Where(item => (item.UserId != usr.UserId && item.FirstName != null) && (item.Privacy == false && item.LastName != "xyz")).ToList();
            IList<User> _user = new List<User>();
            foreach (var item in _GestationusrInfo)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    // email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId,
                    email = item.RoleName + " " + "Weeks",
                    country = item.Country,
                    img = string.IsNullOrEmpty(item.Imagename) ? "../../Images/default_female.png" : "https://www.bumpdocs.com/Uploads/" + item.Imagename

                });

            }

            return Json(_user, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddFilters()
        {

            return PartialView();

        }

        public ActionResult Savefilters(Filters cnfirmSingle)
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

            Filters filter1 = new Filters();
            if (cnfirmSingle.Gestination == true)
            {
                cnfirmSingle.Filtertext = "Gestination";
            }
            if (cnfirmSingle.Hypertension == true)
            {
                if (cnfirmSingle.Filtertext != null)
                {
                    cnfirmSingle.Filtertext = cnfirmSingle.Filtertext + ",Hypertension";
                }
                else
                {
                    cnfirmSingle.Filtertext = "Hypertension";
                }
            }
            if (cnfirmSingle.Latepregnancy == true)
            {
                if (cnfirmSingle.Filtertext != null)
                {
                    cnfirmSingle.Filtertext = cnfirmSingle.Filtertext + ",Latepregnancy";
                }
                else
                {
                    cnfirmSingle.Filtertext = "Latepregnancy";
                }
            }
            if (cnfirmSingle.Earlypregnancy == true)
            {
                if (cnfirmSingle.Filtertext != null)
                {
                    cnfirmSingle.Filtertext = cnfirmSingle.Filtertext + ",Earlypregnancy";
                }
                else
                {
                    cnfirmSingle.Filtertext = "Earlypregnancy";
                }
            }
            if (cnfirmSingle.Thyroid == true)
            {
                if (cnfirmSingle.Filtertext != null)
                {
                    cnfirmSingle.Filtertext = cnfirmSingle.Filtertext + ",Thyroid";
                }
                else
                {
                    cnfirmSingle.Filtertext = "Thyroid";
                }
            }
            if (cnfirmSingle.Diabetes == true)
            {
                if (cnfirmSingle.Filtertext != null)
                {
                    cnfirmSingle.Filtertext = cnfirmSingle.Filtertext + ",Diabetes";
                }
                else
                {
                    cnfirmSingle.Filtertext = "Diabetes";
                }
            }
            if (cnfirmSingle.Twins == true)
            {
                if (cnfirmSingle.Filtertext != null)
                {
                    cnfirmSingle.Filtertext = cnfirmSingle.Filtertext + ",Twins";
                }
                else
                {
                    cnfirmSingle.Filtertext = "Twins";
                }
            }
            if (cnfirmSingle.Tripplets == true)
            {
                if (cnfirmSingle.Filtertext != null)
                {
                    cnfirmSingle.Filtertext = cnfirmSingle.Filtertext + ",Tripplets";
                }
                else
                {
                    cnfirmSingle.Filtertext = "Tripplets";
                }
            }
            if (cnfirmSingle.Asthma == true)
            {
                if (cnfirmSingle.Filtertext != null)
                {
                    cnfirmSingle.Filtertext = cnfirmSingle.Filtertext + ",Asthma";
                }
                else
                {
                    cnfirmSingle.Filtertext = "Asthma";
                }
            }

            if (cnfirmSingle.Outdoorsports == true)
            {
                cnfirmSingle.Interests = "Outdoorsports";
            }
            if (cnfirmSingle.Swimming == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Swimming";
                }
                else
                {
                    cnfirmSingle.Interests = "Swimming";
                }
            }
            if (cnfirmSingle.Yoga == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Yoga";
                }
                else
                {
                    cnfirmSingle.Interests = "Yoga";
                }
            }
            if (cnfirmSingle.Adventuresports == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Adventuresports";
                }
                else
                {
                    cnfirmSingle.Interests = "Adventuresports";
                }
            }
            if (cnfirmSingle.Gardening == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Gardening";
                }
                else
                {
                    cnfirmSingle.Interests = "Gardening";
                }
            }
            if (cnfirmSingle.Animalcare == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Animalcare";
                }
                else
                {
                    cnfirmSingle.Interests = "Animalcare";
                }
            }
            if (cnfirmSingle.Pets == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Pets";
                }
                else
                {
                    cnfirmSingle.Interests = "Pets";
                }
            }
            if (cnfirmSingle.Arts == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Arts";
                }
                else
                {
                    cnfirmSingle.Interests = "Arts";
                }
            }
            if (cnfirmSingle.Modeling == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Modeling";
                }
                else
                {
                    cnfirmSingle.Interests = "Modeling";
                }
            }
            if (cnfirmSingle.Interiordesigning == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Interiordesigning";
                }
                else
                {
                    cnfirmSingle.Interests = "Interiordesigning";
                }
            }
            if (cnfirmSingle.Travelling == true)
            {
                if (cnfirmSingle.Interests != null)
                {
                    cnfirmSingle.Interests = cnfirmSingle.Interests + ",Travelling";
                }
                else
                {
                    cnfirmSingle.Interests = "Travelling";
                }
            }
            Filters filter = new Filters();
            filter.Filtertext = cnfirmSingle.Filtertext;
            filter.Interests = cnfirmSingle.Interests;
            filter.Privacy = cnfirmSingle.Privacy;
            filter.UserId = LoginUser.UserId;
            int errorcode = GetFilters.UpdateFiltertext(filter);


            return RedirectToAction("Index");

        }
    }
}
