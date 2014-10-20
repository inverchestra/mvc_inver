using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Edit;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Create;
using InnDocs.iHealth.Web.UI.Utilities;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals.GetVitals;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using Facebook;
using System.Dynamic;
using Ionic.Zip;
using Aspose.Pdf.Generator;
using System.ComponentModel;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Generator;
using System.Drawing.Imaging;



namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    [SessionExpireFilter]
    public class MedwallController : Controller
    {

        public ActionResult Index()
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
            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }


            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays);
                if (currentDay >= 280)
                {
                    currentDay = 280;
                }
                else
                {
                    currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
                }

                string tr = "false";
                TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);

                int week = currentDay / 7;
                int day = currentDay % 7;
                if (day > 0)
                {
                    week += 1;
                }
                else if (day == 0)
                {
                    day = 7;
                }

                ViewBag.week = week;
                ViewBag.Currentday = currentDay;
                ViewBag.currentweek = week;
                ViewBag.weekcurrentday = day;
                ViewBag.weekday = day;
                ViewBag.statusresponce = i.ResponseCount;
                ViewBag.statustip = i.TipCount;

                PostComments postcomments = new PostComments();
                postcomments.Week = week;
                postcomments.CurrentDay = currentDay;
                postcomments.CurrentWeek = week;
                postcomments.UserId = LoginUser.UserId;
                postcomments.weekcurrentday = day;
                postcomments.weekday = day;

                MedicalAndPersonal m = new MedicalAndPersonal();
                Services3 s3 = new Services3();
                m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);

                ViewBag.ImageName = m.PInfo1.ImageName;
                ViewBag.currentday = currentDay;
                DateTime d = Convert.ToDateTime(LoginUser.StartDate);
                ViewBag.EDDdate = d.AddDays(280);
                Questions ques = null;
                ques = GetPostComments.GetAllQuestionsbyUserIdandDay(postcomments.CurrentDay.ToString(), LoginUser.UserId).FirstOrDefault();
                if (ques != null&& ques.ResponseStatus==null)
                {

                    ViewBag.Question = "Not";

                }

                TempData["postcomments"] = postcomments;
                return View("Medwall");
            }
            else
            {
                return View("Medwall");
            }

        }
        public ActionResult Tableposts()
        {

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
                PostComments postcomments = null;
                if (TempData["postcomments"] != null)
                {
                    postcomments = (PostComments)TempData["postcomments"];

                    Comments comment = new Comments();

                    postcomments.UserId = LoginUser.UserId;

                    PostComments postcomment = GetPostComments.GetPostcommentsByDayandUserId(postcomments.CurrentDay.ToString(), postcomments.UserId);
                    //string encryptedComment = postcomment.PostComment;

                    //List<Comments> tempcomments = null;
                    //if (postcomment.PostComment != null)
                    //{
                    //    tempcomments = XmlStringListSerializer.Deserialize<List<Comments>>(CryptorEngine.Decrypt(postcomment.PostComment, true));
                    //    postcomments.lstComments = tempcomments;
                    //}
                    //else
                    //{
                    //    postcomments.lstComments = tempcomments;
                    //}

                    postcomments.lstComments = postcomment.lstComments;
                    ViewBag.Currentday = postcomments.CurrentDay;
                    return PartialView(postcomments);
                }
                else
                {
                    return PartialView(new PostComments());
                }




            }


        }
        public ActionResult ShowPosts(string data)
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
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now.Date;
            string[] day1 = data.Split(' ');

            string d = day1.Last();

            int week = Convert.ToInt32(d);
            int currentDay = (week - 1) * 7 + 1;
            PostComments postcomments = new PostComments();
            postcomments.UserId = LoginUser.UserId;

            postcomments.CurrentDay = currentDay;
            postcomments.Week = week;
            postcomments.CurrentWeek = week;
            PostComments postcomment = GetPostComments.GetPostcommentsByDayandUserId(postcomments.CurrentDay.ToString(), postcomments.UserId);
            //string encryptedComment = postcomment.PostComment;

            //List<Comments> tempcomments = null;
            //if (postcomment.PostComment != null)
            //{
            //    tempcomments = XmlStringListSerializer.Deserialize<List<Comments>>(CryptorEngine.Decrypt(postcomment.PostComment, true));
            //    postcomments.lstComments = tempcomments;
            //}
            //else
            //{
            //    postcomments.lstComments = tempcomments;
            //}

            postcomments.lstComments = postcomment.lstComments;

            ViewBag.week = postcomments.Week;
            ViewBag.Currentday = postcomments.CurrentDay;
            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/TablePosts.cshtml"), postcomments);

        }

        public ActionResult ShowPosts1(string previous)
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
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now;
            string[] words = previous.Split(',');
            string[] data = words[0].Split(' ');

            int week = Convert.ToInt32(data.Last());

            string[] day = words[1].Split(' ');

            string day2 = day.Last();

            int totalday = Convert.ToInt32(((week - 1) * 7)) + Convert.ToInt32(day2);



            PostComments postcomments = new PostComments();


            Comments comment = new Comments();

            postcomments.UserId = LoginUser.UserId;

            postcomments.CurrentDay = totalday;
            postcomments.Week = week;
            postcomments.CurrentWeek = week;


            PostComments postcomment = GetPostComments.GetPostcommentsByDayandUserId(postcomments.CurrentDay.ToString(), postcomments.UserId);
            //string encryptedComment = postcomment.PostComment;

            //List<Comments> tempcomments = null;
            //if (postcomment.PostComment != null)
            //{
            //    tempcomments = XmlStringListSerializer.Deserialize<List<Comments>>(CryptorEngine.Decrypt(postcomment.PostComment, true));
            //    postcomments.lstComments = tempcomments;
            //}
            //else
            //{
            //    postcomments.lstComments = tempcomments;
            //}

            postcomments.lstComments = postcomment.lstComments;


            ViewBag.week = postcomments.Week;
            ViewBag.Currentday = postcomments.CurrentDay;
            ViewBag.weekcurrentday = day2;
            ViewBag.weekday = day2;
            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/TablePosts.cshtml"), postcomments);

        }


        public ActionResult PostNote(PostComments content, HttpPostedFileBase file)
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

            PostComments postcomments = null;
            if (TempData["postcomments"] != null)
            {
                postcomments = (PostComments)TempData["postcomments"];
            }
            else
            {
                postcomments = new PostComments();
            }

            Comments comment = new Comments();

            postcomments.UserId = LoginUser.UserId;
            string pic;
            string path = string.Empty;
            string filename = string.Empty;
            if (file != null)
            {
                string strDate = DateTime.Now.ToString("ddMMyyyyhhmmss");
                string Imagename = strDate;
                pic = System.IO.Path.GetFileName(file.FileName);
                filename = Imagename + pic;
                path = System.IO.Path.Combine(
                Server.MapPath("~/CommentImages"), filename);

                file.SaveAs(path);


            }
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now.Date;

            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            int week = currentDay / 7;
            int day = currentDay % 7;

            if (day > 0)
            {
                week += 1;
            }

            postcomments.Week = week;
            postcomments.CurrentDay = currentDay;
            postcomments.CurrentDate = DateTime.Now;
            postcomments.CurrentWeek = week;
            postcomments.weekcurrentday = day;
            postcomments.weekday = day;

            comment.Comment = content.PostComment;
            if (file != null)
            {
                comment.path = "../CommentImages/" + filename;
            }
            else
            {
                comment.path = path;
            }


            comment.Postdate = Convert.ToString(System.DateTime.Now);
            postcomments.UserId = LoginUser.UserId;
            postcomments.PostComment = content.PostComment;
            postcomments.Comment = comment;

            CreatePostComment CreatePostComment = new CreatePostComment();

            string i = CreatePostComment.InsertComment(postcomments);
            if (!string.IsNullOrEmpty(content.accesstoken))
            {
                FacebookClient fb = new FacebookClient(content.accesstoken);
                if (!string.IsNullOrWhiteSpace(comment.path) && !string.IsNullOrEmpty(comment.path))
                {
                    string imgpath = @"C:/inetpub/wwwroot/BumpDocs/CommentImages/" + filename;
                    var imgstream = System.IO.File.OpenRead(imgpath);
                    dynamic res = fb.Post("/me/photos", new
                    {

                        message = content.PostComment,
                        file = new FacebookMediaStream
                        {
                            ContentType = file.ContentType,
                            FileName = Path.GetFileName(file.FileName)

                        }.SetValue(imgstream)

                    });
                }
                else
                {

                    dynamic parameters = new ExpandoObject();
                    parameters.message = content.PostComment;
                    parameters.name = "BumpDocs";
                    parameters.link = "https://www.bumpdocs.com";
                    parameters.picture = "https://www.bumpdocs.com/Images/small.png"; 
                    parameters.caption = "BumpDocs 'Wisdom to deliver a healthy baby'";
                    parameters.description = "";
                    dynamic postresult = fb.Post("me/feed", parameters);
                }
            }

            PostComments postcomment = GetPostComments.GetPostcommentsByDayandUserId(postcomments.CurrentDay.ToString(), postcomments.UserId);
            //string encryptedComment1 = postcomment.PostComment;

            //List<Comments> tempcomments = null;
            //if (postcomment.PostComment != null)
            //{
            //    tempcomments = XmlStringListSerializer.Deserialize<List<Comments>>(CryptorEngine.Decrypt(encryptedComment1, true));
            //    postcomments.lstComments = tempcomments;
            //}
            //else
            //{
            //    postcomments.lstComments = tempcomments;
            //}

            postcomments.lstComments = postcomment.lstComments;

            postcomments.Week = week;
            postcomments.CurrentDay = currentDay;
            ViewBag.week = postcomments.Week;
            ViewBag.Currentday = postcomments.CurrentDay;
            ViewBag.weekcurrentday = day;
            ViewBag.weekday = day;
            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/TablePosts.cshtml"), postcomments);

        }

        public ActionResult GetMonths()
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

            PostComments _pstcmnts = new PostComments();

            DateTime startDate = (DateTime)LoginUser.StartDate;
            DateTime CurrentDate = DateTime.Now.Date;
            int months = ((CurrentDate.Year - startDate.Year) * 12) + CurrentDate.Month - startDate.Month;
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);//need to integreate
            ViewBag.Months = months + 1;
            ViewBag.statustip = i.TipCount;
            ViewBag.statusresponce = i.ResponseCount;
            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);

            ViewBag.ImageName = m.PInfo1.ImageName;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);

            return View();

        }

        public ActionResult GetWeeks()
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

            PostComments _pstcmnts = new PostComments();
            DateTime startDate = (DateTime)LoginUser.StartDate;
            DateTime CurrentDate = DateTime.Now.Date;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            int week = currentDay / 7;
            int day = currentDay % 7;
            if (day > 0)
            {

                week += 1;
            }
            _pstcmnts.CurrentWeek = week;
            _pstcmnts.CurrentDay = currentDay;
            ViewBag.CurrentWeek = week;
            ViewBag.startdate = startDate;

            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);//need to integreate

            IList<PostComments> pstcmnts = GetPostComments.GetPostcommentsByUserIdFilterPath(LoginUser.UserId);
            IList<string> path = new List<string>();
            string p = null;

            //if (pstcmnts != null)
            //{
            //    pstcmnts = pstcmnts.OrderBy(x => x.CurrentDay).Where(c => c.lstComments.Any(x=>x.path!="")).ToList();
            //    var child = (from n in pstcmnts
            //                 where n.lstComments.Any(x => x.path == "")
            //                 select n into c
            //                 from e in c.lstComments
            //                 where e.path == ""
            //                 select e).ToList();
            //    pstcmnts.ToList().ForEach(x => x.lstComments.RemoveAll(k => child.Contains(k)));


            //}


            ViewBag.statustip = i.TipCount;
            ViewBag.statusresponce = i.ResponseCount;

            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);

            ViewBag.ImageName = m.PInfo1.ImageName;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);

            return View(pstcmnts);

        }

        public ActionResult GetDays(int id)
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

            ViewBag.Week = id;
            PostComments _pstcmnts = new PostComments();

            DateTime startDate = (DateTime)LoginUser.StartDate;
            ViewBag.startdate = startDate;
            DateTime CurrentDate = DateTime.Now.Date;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            int week = currentDay / 7;
            int day = currentDay % 7;
            if (day > 0)
            {

                week += 1;
            }
            _pstcmnts.CurrentWeek = week;
            _pstcmnts.CurrentDay = currentDay;

            ViewBag.Day = id;
            if (id == 1)
            {
                ViewBag.Days1 = 1;
                ViewBag.Days2 = 2;
                ViewBag.Days3 = 3;
                ViewBag.Days4 = 4;
                ViewBag.Days5 = 5;
                ViewBag.Days6 = 6;
                ViewBag.Days7 = 7;
                ViewBag.lastday = 7;
            }
            else
            {
                ViewBag.Days1 = 1 + (7 * (id - 1));
                ViewBag.Days2 = 2 + (7 * (id - 1));
                ViewBag.Days3 = 3 + (7 * (id - 1));
                ViewBag.Days4 = 4 + (7 * (id - 1));
                ViewBag.Days5 = 5 + (7 * (id - 1));
                ViewBag.Days6 = 6 + (7 * (id - 1));
                ViewBag.Days7 = 7 + (7 * (id - 1));
                if (id == week && ViewBag.Days7 == currentDay)
                {
                    ViewBag.lastday = id * 7;
                }
                else if (id == week)
                {

                    ViewBag.lastday = ViewBag.Days1 + (day - 1);
                }
                else
                {
                    ViewBag.lastday = id * 7;
                }
            }
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);

            ViewBag.statustip = i.TipCount;
            ViewBag.statusresponce = i.ResponseCount;

            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);

            ViewBag.ImageName = m.PInfo1.ImageName;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);


            IList<PostComments> Postcom = GetPostComments.GetPostcommentsByweekandUserIdFilterImagePath(id.ToString(), LoginUser.UserId);


            //if (Postcom != null)
            //{
            //    Postcom = Postcom.OrderBy(x => x.CurrentDay).Where(c => c.lstComments.Any(x => x.path != "")).ToList();
            //    var child = (from n in Postcom
            //                 where n.lstComments.Any(x => x.path == "")
            //                 select n into c
            //                 from e in c.lstComments
            //                 where e.path == ""
            //                 select e).ToList();
            //    Postcom.ToList().ForEach(x => x.lstComments.RemoveAll(k => child.Contains(k)));


            //}

            return View(Postcom);
        }

        public ActionResult GetImagesWeeks(int id, int purl)
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
            ViewBag.Week = purl;
            PostComments postcomments = new PostComments();
            Comments comment = new Comments();
            postcomments.CurrentDay = id;
            postcomments.UserId = LoginUser.UserId;
            PostComments postcomment = GetPostComments.GetPostcommentsByDayandUserId(postcomments.CurrentDay.ToString(), postcomments.UserId);
            //string encryptedComment1 = postcomment.PostComment;

            //List<Comments> tempcomments = null;
            //if (postcomment.PostComment != null)
            //{
            //    tempcomments = XmlStringListSerializer.Deserialize<List<Comments>>(CryptorEngine.Decrypt(encryptedComment1, true));
            //    postcomments.lstComments = tempcomments;
            //}
            //else
            //{
            //    postcomments.lstComments = tempcomments;
            //}
            postcomments.lstComments = postcomment.lstComments;

            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
            DateTime CurrentDate = DateTime.Now.Date;
            ViewBag.ImageName = m.PInfo1.ImageName;
            DateTime startDate = (DateTime)LoginUser.StartDate;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);

            return View(postcomments);
        }

        public ActionResult ShowAllTips()
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

            Services1 s = new Services1();
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now.Date;

            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);

            int week = currentDay / 7;
            int day = currentDay % 7;
            if (day > 0)
            {
                week += 1;
            }
            ViewBag.week = week;
            ViewBag.Currentday = currentDay;
            ViewBag.currentweek = week;
            ViewBag.weekcurrentday = day;
            ViewBag.weekday = day;
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;

            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
            ViewBag.ImageName = m.PInfo1.ImageName;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);
            return View();
        }

        public ActionResult TableTips()
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
            IList<Tips> tips = null;

            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;

                int week = currentDay / 7;
                int day = currentDay % 7;

                tips = GetPostComments.GetAllTipsByUseridandDay(Convert.ToString(currentDay), LoginUser.UserId);
            }
            return PartialView(tips);
        }

        public ActionResult TableTipsBYUserId()
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
            IList<Tips> tips = null;

            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;

                int week = currentDay / 7;
                int day = currentDay % 7;

                //tips = GetPostComments.GetAllTipsByUseridandDay(Convert.ToString(currentDay), LoginUser.UserId);
                tips = GetPostComments.GetAllTipsByUserid(LoginUser.UserId).OrderBy(x => x.Day.ToString()).ToList();
            }
            return PartialView(tips);
        }

        public ActionResult TableQuestionsByUserId()
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

            IList<Questions> ques = null;

            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;

                int week = currentDay / 7 ;
                int day = currentDay % 7;
                if (day > 0)
                {
                    week += 1;
                }

                ques = GetPostComments.GetAllQuestionsbyUserIdandWeek(LoginUser.UserId, week.ToString()).OrderBy(x => x.Day.ToString()).ToList();
                //ques = GetPostComments.GetAllQuestionsbyUserId(LoginUser.UserId).OrderBy(x => x.Day.ToString()).ToList();
            }
            return PartialView(ques);
        }

        public ActionResult UnReadTips()//added by jagadeesh
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

            Services1 s = new Services1();
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now.Date;

            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);

            int week = currentDay / 7;
            int day = currentDay % 7;
            if (day > 0)
            {
                week += 1;
            }
            ViewBag.week = week;
            ViewBag.Currentday = currentDay;
            ViewBag.currentweek = week;
            ViewBag.weekcurrentday = day;
            ViewBag.weekday = day;
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;

            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
            ViewBag.ImageName = m.PInfo1.ImageName;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);
            return View();
        }

        public ActionResult UnreadTipsBYUserId()
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

            IList<Tips> tips = null;

            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;

                int week = currentDay / 7 + 1;
                int day = currentDay % 7;

                string tr = "false";

                tips = GetPostComments.GetAllUnreadTipsByUserid(LoginUser.UserId, tr).OrderBy(x => x.Day.ToString()).ToList(); ;
            }
            return PartialView(tips);
        }

        public ActionResult UnreadQuestions()
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

            Services1 s = new Services1();
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now.Date;

            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);

            int week = currentDay / 7;
            int day = currentDay % 7;
            if (day > 0)
            {
                week += 1;
            }
            ViewBag.week = week;
            ViewBag.Currentday = currentDay;
            ViewBag.currentweek = week;
            ViewBag.weekcurrentday = day;
            ViewBag.weekday = day;
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;

            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
            ViewBag.ImageName = m.PInfo1.ImageName;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);
            return View();
        }

        public ActionResult UnreadQuestByuserId()
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

            IList<Questions> tips = null;

            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;

                int week = currentDay / 7 + 1;
                int day = currentDay % 7;

                string tr = "false";

                tips = GetPostComments.GetAllUnreadQuestionsbyUserId(LoginUser.UserId, tr);
            }
            return PartialView(tips);
        }

        public ActionResult ShowWeekTips(string data)
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
            Services1 s = new Services1();
            UserInformation userInfo = s.GetUserbyId(LoginUser.UserId);
            IList<Tips> tips = null;
            int currentDay = 0;
            int week = 0;
            if (userInfo.StartDate != null)
            {
                DateTime startDate = (DateTime)userInfo.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays);

                week = Convert.ToInt32(data);

                int currentDay1 = (week - 1) * 7 + 1;
                int day = currentDay % 7;

                tips = GetPostComments.GetAllTipsByUseridandDay(Convert.ToString(currentDay1), LoginUser.UserId);
            }
            ViewBag.week = week;
            ViewBag.Currentday = currentDay;
            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/TableTips.cshtml"), tips);

        }

        public ActionResult ShowWeekQuestions(string data)
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

            IList<Questions> tips = null;
            int currentDay = 0;
            int week = 0;
            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now;

                currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays);


                week = Convert.ToInt32(data);
                int currentDay1 = (week - 1) * 7 + 1;
                int day = currentDay % 7;

                tips = GetPostComments.GetAllQuestionsbyUserIdandDay(Convert.ToString(currentDay1), LoginUser.UserId);
            }
            ViewBag.week = week;
            ViewBag.Currentday = currentDay;
            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/TableQuestions.cshtml"), tips);

        }

        public ActionResult ShowDayTips(string previous)
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
            Services1 s = new Services1();
            IList<Tips> tips = null;
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now;
            string[] words = previous.Split(',');
            string data = words[0];

            int week = Convert.ToInt32(data);

            string day = words[1];
            string[] day1 = day.Split(' ');

            string day2 = day1[0];

            int totalday = Convert.ToInt32(((week - 1) * 7)) + Convert.ToInt32(day2);

            tips = GetPostComments.GetAllTipsByUseridandDay(Convert.ToString(totalday), LoginUser.UserId);

            ViewBag.weekcurrentday = day;
            ViewBag.weekday = day;
            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/TableTips.cshtml"), tips);

        }

        public ActionResult ShowDayQuestions(string previous)
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


            IList<Questions> tips = null;
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now;
            string[] words = previous.Split(',');
            string data = words[0];

            int week = Convert.ToInt32(data);

            string day = words[1];
            string[] day1 = day.Split(' ');

            string day2 = day1[0];

            int totalday = Convert.ToInt32(((week - 1) * 7)) + Convert.ToInt32(day2);

            tips = GetPostComments.GetAllQuestionsbyUserIdandDay(Convert.ToString(totalday), LoginUser.UserId);


            ViewBag.weekcurrentday = day;
            ViewBag.weekday = day;
            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/TableQuestions.cshtml"), tips);

        }


        public ActionResult ViewTips()
        {
            TipsAndQA tipsandQA = new TipsAndQA();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            IList<Tips> lstTips = new List<Tips>();
            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
                lstTips = GetPostComments.GetTipsByDay(currentDay.ToString(), LoginUser.UserId);
                tipsandQA.lstTips = lstTips;

                IList<Questions> lstq = new List<Questions>();
                lstq = GetPostComments.GetQuestionsByDay(currentDay.ToString(), LoginUser.UserId);
                tipsandQA.lstQues = lstq;


            }

            if (lstTips.Count == 0)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            return PartialView(tipsandQA);


        }
        public ActionResult GetAlert()
        {
            TipsAndQA tipsandQA = new TipsAndQA();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            IList<Tips> lstTips = new List<Tips>();
            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
                IList<Questions> lstq = new List<Questions>();
                lstq = GetPostComments.GetQuestionsByDay(currentDay.ToString(), LoginUser.UserId);
                tipsandQA.lstQues = lstq;


            }

            return Json(tipsandQA, JsonRequestBehavior.AllowGet);

        }

        public ActionResult UpdateTip(string tipId)
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
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now.Date;

            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            string errorCode = EditPostComment.UpdateTip(Convert.ToString(currentDay), LoginUser.UserId);
            return new EmptyResult();

        }

        public ActionResult ShowAllQuestions()
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

            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
                string tr = "false";
                TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);

                int week = currentDay / 7;
                int day = currentDay % 7;
                if (day > 0)
                {

                    week += 1;
                }
                ViewBag.week = week;
                ViewBag.Currentday = currentDay;
                ViewBag.currentweek = week;
                ViewBag.weekcurrentday = day;
                ViewBag.weekday = day;
                ViewBag.statusresponce = i.ResponseCount;
                ViewBag.statustip = i.TipCount;

                MedicalAndPersonal m = new MedicalAndPersonal();
                Services3 s3 = new Services3();
                m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
                ViewBag.ImageName = m.PInfo1.ImageName;
                ViewBag.currentday = currentDay;
                DateTime d = Convert.ToDateTime(LoginUser.StartDate);
                ViewBag.EDDdate = d.AddDays(280);

                return View();
            }
            else
            {
                return View();
            }


        }

        public ActionResult UpdateQuestion(string QuestionId, string result)
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
            string errorCode = EditPostComment.UpdateQuestion(QuestionId, LoginUser.UserId, result);
            return new EmptyResult();

        }


        public ActionResult TableQuestions()
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

            IList<Questions> ques = null;

            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;

                int week = currentDay / 7 + 1;
                int day = currentDay % 7;



                ques = GetPostComments.GetAllQuestionsbyUserIdandDay(Convert.ToString(currentDay), LoginUser.UserId);
            }
            return PartialView(ques);
        }

        public ActionResult TableQuestions1()
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

            IList<Questions> ques = null;

            if (LoginUser.StartDate != null)
            {
                DateTime startDate = (DateTime)LoginUser.StartDate;

                DateTime CurrentDate = DateTime.Now.Date;

                int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;

                int week = currentDay / 7 + 1;
                int day = currentDay % 7;
                /* vvv */
                PostComments postcomments = new PostComments();
                postcomments.Week = week;
                postcomments.CurrentDay = currentDay;
                postcomments.CurrentWeek = week;
                postcomments.UserId = LoginUser.UserId;
                postcomments.weekcurrentday = day;
                postcomments.weekday = day;
                TempData["postcomments"] = postcomments;
                /* vvv */
                ques = GetPostComments.GetAllQuestionsbyUserIdandDay(Convert.ToString(currentDay), LoginUser.UserId);
            }
           
            return PartialView(ques);

        }

        public ActionResult QResponses1(string qid)
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
            Questions ques = GetPostComments.GetQuestionByQidandUserId(qid, LoginUser.UserId);
            ques.QuestionId = qid;
            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/QResponses1.cshtml"), ques);
        }

        public ActionResult QResponses(string qid)
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
            Questions ques = GetPostComments.GetQuestionByQidandUserId(qid, LoginUser.UserId);
            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/QResponses.cshtml"), ques);
        }


        [HttpPost]
        public ActionResult QResponses(string ans, string id)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            string errorCode = EditPostComment.UpdateQuestion(id, LoginUser.UserId, ans);
            ResultData.Data = errorCode;
            return ResultData;

        }

        public ActionResult Qrespo(string ans, string id, string res)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }

            Questions ques = null;
            ques = new Questions();

            ques.Response = ans;
            ques.QuestionId = id;
            ques.UserId = LoginUser.UserId;
            ques.ResponseStatus = res;
            string errorCode = EditPostComment.UpdateQuestion(ques);
            ResultData.Data = errorCode;
            return ResultData;

        }

        public ActionResult UpdateTipStatus(string tipId)
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
            string errorCode = EditPostComment.UpdateTipStatusByTipId(tipId, LoginUser.UserId);

            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now;

            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays);
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);

            int week = currentDay / 7;
            int day = currentDay % 7;

            if (day > 0)
            {
                week += 1;
            }
            ViewBag.week = week;
            ViewBag.Currentday = currentDay;
            ViewBag.currentweek = week;
            ViewBag.weekcurrentday = day;
            ViewBag.weekday = day;
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;
            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
            ViewBag.ImageName = m.PInfo1.ImageName;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);
            return View("ShowAllTips");
        }


        public ActionResult DownloadMedwall()
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
            JsonResult ResultData = new JsonResult();

            PostComments postcomments = new PostComments();

            //    postcomments = (PostComments)TempData["postcomments"];
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime CurrentDate = DateTime.Now.Date;

            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;

            Comments comment = new Comments();

            postcomments.UserId = LoginUser.UserId;

            // PostComments postcomment = GetPostComments.GetPostcommentsByDayandUserId(currentDay.ToString(), postcomments.UserId);
            IList<PostComments> lstpost = GetPostComments.GetPostcommentsByUserId(postcomments.UserId);
            postcomments.lstComments = new List<Comments>();
            foreach (var i in lstpost)
            {
                string encryptedComment = i.PostComment;

                List<Comments> tempcomments = null;
                if (i.PostComment != null)
                {
                    tempcomments = i.lstComments;
                    //  tempcomments = XmlStringListSerializer.Deserialize<List<Comments>>(CryptorEngine.Decrypt(i.PostComment, true));
                    // postcomments.lstComments = tempcomments;

                    postcomments.lstComments.AddRange(tempcomments);
                }
                else
                {
                    postcomments.lstComments = tempcomments;
                }

            }




            //PdfFileEditor pdfEditor = new PdfFileEditor();
            //string tempdir = "C:\\Medwall\\";
            //string date = Convert.ToDateTime(LoginUser.StartDate).ToShortDateString();
            //string startdate = Convert.ToDateTime(date).ToString("ddMMyyyy");
            //string retFileName = LoginUser.UserId + startdate + ".pdf";
            //string zipFileName = "C:\\"+LoginUser.UserId + startdate + ".zip";
            //string DestinationFolder = tempdir + "\\" + retFileName;
            //string SendToDocInfo = tempdir + LoginUser.UserId + "kumar";
            //string SourceDirectoryPath = tempdir;//@"C:\InndocsiHealth\PDFFiles\74";
            ////string TargetZipFilePath = tempdir + LoginUser.FirstName + startdate + ".zip";//@"C:\InndocsiHealth\PDFFiles\" + objCase.CaseName + LoginUser.LoginName + strDate + ".zip";
            //string TargetZipFilePath = "C:\\" + LoginUser.FirstName + startdate + ".zip";






            string s = Convert.ToDateTime(LoginUser.StartDate).ToShortDateString();
            string startdate = Convert.ToDateTime(s).ToString("ddMMyyyy");
            string retFileName = LoginUser.FirstName + LoginUser.UserId + startdate + ".pdf";
            string zipFileName = LoginUser.FirstName + LoginUser.UserId + startdate + ".zip";


            //for Bumpdocs Product
            // string tempdir = "C:\\BumpDocsProduct\\PDFFiles\\temp\\" + LoginUser.UserId;

            //for www.BumpDocs.com
            string tempdir = "C:\\BumpDocs\\PDFFiles\\temp\\" + LoginUser.UserId;

            string SendToDocInfo = tempdir + "\\" + LoginUser.FirstName;//Sending this string to docinfo obj in docinfo.Name
            string DestinationFolder = tempdir + "\\" + LoginUser.FirstName + "\\" + retFileName;
            string ImagesDestination = tempdir + "\\" + LoginUser.FirstName + "Images";
            string SourceDirectoryPath = tempdir;//@"C:\InndocsiHealth\PDFFiles\74";
            string TargetZipFilePath = tempdir + LoginUser.FirstName + startdate + ".zip";//@"C:\InndocsiHealth\PDFFiles\" + objCase.CaseName + LoginUser.LoginName + strDate + ".zip";





            if (!Directory.Exists(tempdir))
            {
                Directory.CreateDirectory(tempdir);
            }
            if (!Directory.Exists(DestinationFolder))
            {
                Directory.CreateDirectory(SendToDocInfo);
            }
            if (!Directory.Exists(ImagesDestination))
            {
                Directory.CreateDirectory(ImagesDestination);
            }

            Aspose.Pdf.License lic = new Aspose.Pdf.License();
            //lic.SetLicense(@"C\\InndocsiHealth\\Aspose.Total.lic");
            lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

            Pdf pdf = new Pdf();

            Section sec1 = pdf.Sections.Add();
            sec1.PageInfo.PageHeight = Aspose.Pdf.Generator.PageSize.A3Height;
            sec1.PageInfo.PageWidth = Aspose.Pdf.Generator.PageSize.A3Width;

            MarginInfo margin = new MarginInfo();
            margin.Top = 72;
            margin.Bottom = 72;
            margin.Left = 90;
            margin.Right = 90;
            sec1.PageInfo.Margin = margin;








            Text pdfHeadText = new Text("Medwall Information");

            pdfHeadText.TextInfo.FontName = "Arial";
            pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfHeadText.TextInfo.FontSize = 16;
            pdfHeadText.TextInfo.LineSpacing = 5f;


            MarginInfo tabmargin = new MarginInfo();
            tabmargin.Top = 10;
            tabmargin.Bottom = 10;
            tabmargin.Right = 10;
            tabmargin.Left = 10;

            sec1.Paragraphs.Add(pdfHeadText);
            if (postcomments.lstComments.Count > 0)
            {

                Table tab1 = new Table();
                sec1.Paragraphs.Add(tab1);
                tab1.ColumnWidths = "400 100";

                tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);
                tab1.Border = new BorderInfo((int)BorderSide.All, 1F);



                tab1.DefaultCellPadding = tabmargin;
                Row srow0 = tab1.Rows.Add();
                srow0.Cells.Add("Post");
                srow0.Cells.Add("PostTime");


                int j = 0;
                foreach (var i in postcomments.lstComments)
                {
                    Comments c = (Comments)postcomments.lstComments[j];
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;

                    //  Row srow5 = tab1.Rows.Add();
                    //    srow5.Cells.Add("Commments"+j,t);

                    Row srow1 = tab1.Rows.Add();
                    srow1.Cells.Add(c.Comment);
                    srow1.Cells.Add(c.Postdate);
                    //  Row srow2 = tab1.Rows.Add();
                    //  srow2.Cells.Add(c.Postdate);
                    j++;

                }
            }
            else
            {

                Text postelseText = new Text("You have no postcomments");

                postelseText.TextInfo.FontName = "Arial";
                postelseText.TextInfo.IsTrueTypeFontBold = false;
                postelseText.TextInfo.FontSize = 10;
                postelseText.TextInfo.LineSpacing = 5f;
                sec1.Paragraphs.Add(postelseText);


            }

            HeightandWeightServiceCalls hwservice = new HeightandWeightServiceCalls();


            IList<HeightandWeight> lsthw = hwservice.GetHnWbyUserId(LoginUser.UserId);

            Text pdfMTHeadText = new Text("Height and weight");

            pdfMTHeadText.TextInfo.FontName = "Arial";
            pdfMTHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfMTHeadText.TextInfo.FontSize = 16;
            pdfMTHeadText.TextInfo.LineSpacing = 5f;

            MarginInfo table2mgr = new MarginInfo();
            table2mgr.Top = 10;
            table2mgr.Bottom = 10;
            table2mgr.Left = 10;
            table2mgr.Right = 10;

            sec1.Paragraphs.Add(pdfMTHeadText);
            if (lsthw.Count > 0)
            {

                Table table2 = new Table();
                sec1.Paragraphs.Add(table2);
                table2.ColumnWidths = "100 100 100 100";
                table2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);
                table2.Border = new BorderInfo((int)BorderSide.All, 1F);



                table2.Margin = table2mgr;
                Row srow8 = table2.Rows.Add();
                srow8.Cells.Add("Height");
                srow8.Cells.Add("Weight");
                srow8.Cells.Add("BloodGroup");
                srow8.Cells.Add("BloodPressure");

                int k = 0;
                foreach (var i in lsthw)
                {
                    Row srow6 = table2.Rows.Add();
                    srow6.Cells.Add(i.Height.ToString());
                    srow6.Cells.Add(i.Weight.ToString());
                    srow6.Cells.Add(i.BloodGroup);
                    srow6.Cells.Add(i.BloodPressure);

                }
            }
            else
            {
                Text hwelseText = new Text("You have no Height and Weight records");

                hwelseText.TextInfo.FontName = "Arial";
                hwelseText.TextInfo.IsTrueTypeFontBold = false;
                hwelseText.TextInfo.FontSize = 10;
                hwelseText.TextInfo.LineSpacing = 5f;
                sec1.Paragraphs.Add(hwelseText);
            }

            FHRServiceCalls fhrservice = new FHRServiceCalls();
            IList<Fhr> lstfhr = fhrservice.GetFhrbyUserId(LoginUser.UserId);

            Text pdfhrHeadText = new Text("Fhr");

            pdfhrHeadText.TextInfo.FontName = "Arial";
            pdfhrHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfhrHeadText.TextInfo.FontSize = 16;
            pdfhrHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfhrHeadText);

            if (lstfhr.Count > 0)
            {
                Table fhrtable = new Table();
                sec1.Paragraphs.Add(fhrtable);
                fhrtable.ColumnWidths = "200 200";
                fhrtable.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);
                fhrtable.Border = new BorderInfo((int)BorderSide.All, 1F);

                fhrtable.Margin = table2mgr;

                Row srow9 = fhrtable.Rows.Add();
                srow9.Cells.Add("Beats per minute");
                srow9.Cells.Add("");
                foreach (var i in lstfhr)
                {
                    Row srow7 = fhrtable.Rows.Add();
                    srow7.Cells.Add(i.Bpm);
                    srow7.Cells.Add();
                }

            }
            else
            {
                Text fhrelseText = new Text("You have no Fhr  records");

                fhrelseText.TextInfo.FontName = "Arial";
                fhrelseText.TextInfo.IsTrueTypeFontBold = false;
                fhrelseText.TextInfo.FontSize = 10;
                fhrelseText.TextInfo.LineSpacing = 5f;
                sec1.Paragraphs.Add(fhrelseText);

            }

            Text skintest = new Text("Skin Test");
            skintest.TextInfo.FontName = "Arial";
            skintest.TextInfo.IsTrueTypeFontBold = true;
            skintest.TextInfo.FontSize = 16;
            skintest.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(skintest);


            SkinTestServiceCalls skintestservice = new SkinTestServiceCalls();
            IList<SkinTest> lstskintest = skintestservice.GetSkintestbyUserId(LoginUser.UserId);
            if (lstskintest.Count > 0)
            {

                Table skintesttable = new Table();
                sec1.Paragraphs.Add(skintesttable);
                skintesttable.ColumnWidths = "100 100 100 100 100";
                skintesttable.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);
                skintesttable.Border = new BorderInfo((int)BorderSide.All, 1F);

                skintesttable.Margin = table2mgr;

                Row srow10 = skintesttable.Rows.Add();
                srow10.Cells.Add("ColourOfNipples");
                srow10.Cells.Add("PigmentationSpotsOnSkin");
                srow10.Cells.Add("ComplexionOfSkin");
                srow10.Cells.Add("StretchMarks");
                srow10.Cells.Add("Acne");



                foreach (var i in lstskintest)
                {
                    Row srow11 = skintesttable.Rows.Add();
                    srow11.Cells.Add(i.ColourOfNipples.ToString());
                    srow11.Cells.Add(i.PigmentationSpotsOnSkin.ToString());
                    srow11.Cells.Add(i.ComplexionOfSkin.ToString());
                    srow11.Cells.Add(i.StretchMarks.ToString());
                    srow11.Cells.Add(i.Acne.ToString());
                }
            }
            else
            {
                Text skinelseText = new Text("You have no SkinTest  records");

                skinelseText.TextInfo.FontName = "Arial";
                skinelseText.TextInfo.IsTrueTypeFontBold = false;
                skinelseText.TextInfo.FontSize = 10;
                skinelseText.TextInfo.LineSpacing = 5f;
                sec1.Paragraphs.Add(skinelseText);

            }

            Text Stdinfo = new Text("STD");
            Stdinfo.TextInfo.FontName = "Arial";
            Stdinfo.TextInfo.IsTrueTypeFontBold = true;
            Stdinfo.TextInfo.FontSize = 16;
            Stdinfo.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(Stdinfo);

            STDsServiceCalls stdserv = new STDsServiceCalls();
            IList<STDs> lststds = stdserv.GetStdsbyUserId(LoginUser.UserId);
            if (lststds.Count > 0)
            {
                Table stdtable = new Table();
                sec1.Paragraphs.Add(stdtable);
                stdtable.ColumnWidths = "100 100 100 100 100";
                stdtable.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);
                stdtable.Border = new BorderInfo((int)BorderSide.All, 1F);

                stdtable.Margin = table2mgr;

                Row srow12 = stdtable.Rows.Add();
                srow12.Cells.Add("Syphilis");
                srow12.Cells.Add("Herpes");
                srow12.Cells.Add("Hepatitis B & C ");
                srow12.Cells.Add("Gonorrhea");
                srow12.Cells.Add("Chlamydia");



                foreach (var i in lststds)
                {
                    Row srow13 = stdtable.Rows.Add();
                    srow13.Cells.Add(i.Syphilis.ToString());
                    srow13.Cells.Add(i.Herpes.ToString());
                    srow13.Cells.Add(i.HepatitisBnC.ToString());
                    srow13.Cells.Add(i.Gonorrhea.ToString());
                    srow13.Cells.Add(i.Chlamydia.ToString());
                }
            }
            else
            {
                Text stdelseText = new Text("You have no STDs  records");

                stdelseText.TextInfo.FontName = "Arial";
                stdelseText.TextInfo.IsTrueTypeFontBold = false;
                stdelseText.TextInfo.FontSize = 10;
                stdelseText.TextInfo.LineSpacing = 5f;
                sec1.Paragraphs.Add(stdelseText);
            }

            Text urintestinfo = new Text("UrinTest");
            urintestinfo.TextInfo.FontName = "Arial";
            urintestinfo.TextInfo.IsTrueTypeFontBold = true;
            urintestinfo.TextInfo.FontSize = 16;
            urintestinfo.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(urintestinfo);
            UrineTestServiceCalls urintestserv = new UrineTestServiceCalls();
            IList<UrineTest> lsturintest = urintestserv.GeturintestbyUserId(LoginUser.UserId);
            if (lsturintest.Count > 0)
            {

                Table urintesttable = new Table();
                sec1.Paragraphs.Add(urintesttable);
                urintesttable.ColumnWidths = "100 100 100 100 100";
                urintesttable.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);
                urintesttable.Border = new BorderInfo((int)BorderSide.All, 1F);

                urintesttable.Margin = table2mgr;

                Row srow14 = urintesttable.Rows.Add();

                srow14.Cells.Add("Glucose tolerant test");
                srow14.Cells.Add("UrinaryTtractInFection");
                srow14.Cells.Add("Protein");
                srow14.Cells.Add("BloodCells");
                srow14.Cells.Add("Ketones");



                foreach (var i in lsturintest)
                {
                    Row srow15 = urintesttable.Rows.Add();
                    srow15.Cells.Add(i.GlucoseTolerantTest.ToString());
                    srow15.Cells.Add(i.UrinaryTtractInFection.ToString());
                    srow15.Cells.Add(i.Protein.ToString());
                    srow15.Cells.Add(i.BloodCells.ToString());
                    srow15.Cells.Add(i.Ketones.ToString());
                }
            }
            else
            {
                Text urintstelseText = new Text("You have no UrinTest  records");

                urintstelseText.TextInfo.FontName = "Arial";
                urintstelseText.TextInfo.IsTrueTypeFontBold = false;
                urintstelseText.TextInfo.FontSize = 10;
                urintstelseText.TextInfo.LineSpacing = 5f;
                sec1.Paragraphs.Add(urintstelseText);
            }

            string destinationPath = DestinationFolder;



            //  string sourcePath = @"C:\Program Files\Common Files\microsoft shared\DevServer\CommentImages";

            string sourcePath = "C:\\inetpub\\wwwroot\\BumpDocs\\CommentImages";

            // string targetPath = @"C:\Users\Public\TestFolder\SubDir";

            // Use Path class to manipulate file and directory paths. 

            int l = 0;
            if (postcomments.lstComments.Count > 0)
            {
                foreach (var i in postcomments.lstComments)
                {
                    Comments c = (Comments)postcomments.lstComments[l];
                    string path = c.path;

                    if (!string.IsNullOrEmpty(path) || !string.IsNullOrWhiteSpace(path))
                    {
                        var op = Path.GetFileName(path);
                        var fulpath = Path.GetFullPath(path);

                        string sourceFile = System.IO.Path.Combine(sourcePath, op);
                        string destFile = System.IO.Path.Combine(ImagesDestination, op);

                        System.IO.File.Copy(sourceFile, destFile, true);


                    }

                    l++;
                }
            }







            //PostComments postcomments = new PostComments();

            //   // postcomments = (PostComments)TempData["postcomments"];

            //    Comments comment = new Comments();

            //    postcomments.UserId = LoginUser.UserId;
            //    DateTime startDate = (DateTime)LoginUser.StartDate;

            //    DateTime CurrentDate = DateTime.Now.Date;

            //    int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;

            //    PostComments postcomment = GetPostComments.GetPostcommentsByDayandUserId(currentDay.ToString(), postcomments.UserId);
            //    string encryptedComment = postcomment.PostComment;
            //    Table t1 = new Table();
            //    sec1.Paragraphs.Add(t1);
            //    List<Comments> tempcomments = null;
            //    if (postcomment.PostComment != null)
            //    {
            //        tempcomments = XmlStringListSerializer.Deserialize<List<Comments>>(CryptorEngine.Decrypt(postcomment.PostComment, true));
            //        postcomments.lstComments = tempcomments;
            //        int j = 0;
            //        foreach(var i in postcomments.lstComments)
            //        {

            //        Comments cm = (Comments)postcomments.lstComments[j];
            //        string text = cm.Comment;
            //        string d = cm.Postdate;
            //        Aspose.Pdf.Generator.Row r = t1.Rows.Add();
            //        r.Cells.Add(text+d);
            //      // Text pdfheader = new Text(text+d);

            //      //  pdfheader.TextInfo.FontSize = 11;
            //      //  pdfheader.TextInfo.FontName = "Arial";
            //      //  pdfheader.TextInfo.WordSpace = 5f;
            //       // sec1.Paragraphs.Add(pdfheader);
            //        j++;
            //        }
            //    }
            //    else
            //    {
            //        postcomments.lstComments = tempcomments;
            //    }







            //  Text pdfheader = new Text("Hi this medwall example");
            //  Text pdfheader = new Text();
            //pdfheader.TextInfo.FontSize = 11;
            //pdfheader.TextInfo.FontName = "Arial";
            //pdfheader.TextInfo.WordSpace = 5f;
            //sec1.Paragraphs.Add(pdfheader);
            pdf.Save(DestinationFolder);
            //string TargetZipFilePath=@

            try
            {

                ZipFile zip = null;
                using (zip = new ZipFile())
                {
                    zip.AddDirectory(SourceDirectoryPath, System.IO.Path.GetFileName(DestinationFolder));
                    zip.Save(TargetZipFilePath);
                    zip.Dispose();
                }
            }
            catch (System.Exception ex1)
            {
                throw ex1;
            }
            var bytes = System.IO.File.ReadAllBytes(TargetZipFilePath);
            // System.IO.File.Delete(TargetZipFilePath);
            //  Directory.Delete(tempdir, true);

            return File(bytes, "application/x-zip-compressed", "medwallinfo.zip");

            // return ResultData;
        }

        #region Vitals

        public ActionResult VitalsNew()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }
            Vitals _vitals = null; _vitals = new Vitals();
            CompleteBPServiceCalls cbpservicecalls = null;
            cbpservicecalls = new CompleteBPServiceCalls();
            USTServiceCalls ustSerCalls = new USTServiceCalls();
            HeightandWeightServiceCalls HnWServiceCalls = new HeightandWeightServiceCalls();
            Services3 s3 = new Services3();
            _vitals.STDs = GetAllVitals.GetRecentStdsObjByUserId(LoginUser.UserId);
            _vitals.HnW = GetAllVitals.GetRecentHWObjByUserId(LoginUser.UserId);
            _vitals.LstHnW = HnWServiceCalls.GetHnWbyUserId(LoginUser.UserId);
            _vitals.MedInfo = s3.GetMedicalInfo(LoginUser.UserId);

            _vitals.ST = GetAllVitals.GetRecentSkinTestObjByUserId(LoginUser.UserId);
            _vitals.UT = GetAllVitals.GetRecentUrineTestObjByUserId(LoginUser.UserId);
            _vitals.FHR = GetAllVitals.GetRecentFhrObjByUserId(LoginUser.UserId);
            _vitals.UST = GetAllVitals.GetRecentUSTObjByUserId(LoginUser.UserId);
            _vitals.CompleteBP = GetAllVitals.GetRecentBInfoByUserId(LoginUser.UserId);
            _vitals.LstBT = GetAllVitals.GetAllBTsInfoByUserId(LoginUser.UserId).ToList();
            _vitals.LstBT = _vitals.LstBT.GroupBy(x => x.BloodTestName).Select(y => y.First()).ToList();
            _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
            _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
            _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
            _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);//need to integreate
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;

            return View(_vitals);

        }



        public ActionResult VitalsEssentialInfo()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }
            Vitals _vitals = null; _vitals = new Vitals();
            CompleteBPServiceCalls cbpservicecalls = null;
            cbpservicecalls = new CompleteBPServiceCalls();
            USTServiceCalls ustSerCalls = new USTServiceCalls();
            HeightandWeightServiceCalls HnWServiceCalls = new HeightandWeightServiceCalls();
            Services3 s3 = new Services3();
            // _vitals.STDs = GetAllVitals.GetRecentStdsObjByUserId(LoginUser.UserId);
            _vitals.HnW = GetAllVitals.GetRecentHWObjByUserId(LoginUser.UserId);
            _vitals.LstHnW = HnWServiceCalls.GetHnWbyUserId(LoginUser.UserId);
            DateTime time = Convert.ToDateTime(_vitals.HnW.CreatedOn);
            _vitals.HnW.Created = time.ToString("dd MMMM yyyy");

            // _vitals.MedInfo = s3.GetMedicalInfo(LoginUser.UserId);



            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);//need to integreate
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;

            return View(_vitals);

        }
        public ActionResult HnWEdit()
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

            HeightandWeightServiceCalls HnWServiceCalls = new HeightandWeightServiceCalls();
            Vitals _vitals = null; _vitals = new Vitals();

            _vitals.HnW = GetAllVitals.GetRecentHWObjByUserId(LoginUser.UserId);
            Services3 s3 = new Services3();
            _vitals.MedInfo = s3.GetMedicalInfo(LoginUser.UserId);
            return PartialView(_vitals);
        }
        [HttpPost]
        //public ActionResult HeightandWeight(Vitals hw)
        //{
        //    JsonResult ResultData = new JsonResult();
        //    UserInformation LoginUser = null;
        //    if (Session["CurrentUser"] != null)
        //    {
        //        LoginUser = Session["CurrentUser"] as UserInformation;
        //    }
        //    else
        //    {
        //        LoginUser = Session["LoginUser"] as UserInformation;
        //    }

        //    string SuccessCode = "";
        //    HeightandWeightServiceCalls hwservicecalls = null;
        //    hwservicecalls = new HeightandWeightServiceCalls();


        //    hw.HnW.CreatedOn = DateTime.Now;
        //    hw.HnW.DomainId = LoginUser.DomainId;
        //    hw.HnW.UserId = LoginUser.UserId;

        //    if (hw.Systole != null && hw.Diastole != null)
        //    {
        //        hw.HnW.BloodPressure = hw.Systole + "/" + hw.Diastole;
        //    }

        //    if (hw.HnW.Weight1 != null)
        //    {
        //        float val2 = (float)hw.HnW.Weight1;
        //        double weight = (float)val2 * 2.2;
        //        hw.HnW.Weight = Math.Round(weight, 2);
        //    }
        //    if (hw.HnW.feets != 0 && hw.HnW.inches != null)
        //    {
        //        if (hw.HnW.inches == 0)
        //        {
        //            hw.HnW.inches = hw.HnW.inches;
        //        }
        //        else if (hw.HnW.inches <= 9)
        //        {
        //            hw.HnW.inches = ((hw.HnW.inches) / 10);
        //        }
        //        else
        //        {
        //            hw.HnW.inches = ((hw.HnW.inches) / 100);
        //        }
        //        var ht = hw.HnW.feets + hw.HnW.inches;
        //        hw.HnW.Height = (ht * 30.48);
        //    }

        //    SuccessCode = hwservicecalls.InsertHeightandWeightInfo(hw.HnW);
        //    if (SuccessCode == "1010")
        //    {
        //        Vitals _vitals = new Vitals();
        //        _vitals.HnW = GetAllVitals.GetRecentHWObjByUserId(LoginUser.UserId);
        //        return PartialView("VitalsEssentialInfo", _vitals);
        //    }
        //    // else
        //    // {
        //    // ResultData.Data = "Fail";
        //    // return ResultData;
        //    // }
        //    Services3 s3 = new Services3();
        //    hw.MedInfo = s3.GetMedicalInfo(LoginUser.UserId);

        //    if (hw.MedInfo.BMIHeight == null)
        //    {
        //        hw.MedInfo.BMIHeight = hw.HnW.Height;
        //    }

        //    if (hw.MedInfo.BloodGroup == null)
        //    {
        //        hw.MedInfo.BloodGroup = hw.HnW.BloodGroup;
        //    }


        //    if (hw.HnW.Weight != null)
        //    {
        //        hw.MedInfo.BMIWeight = hw.HnW.Weight;
        //    }
        //    else
        //    {

        //    }

        //    if (hw.HnW.BloodPressure != null)
        //    {
        //        hw.MedInfo.BloodPressure = hw.HnW.BloodPressure;
        //    }
        //    else
        //    {
        //    }

        //    int result = s3.UpdateMedicalInfo(hw.MedInfo, LoginUser.UserId, LoginUser.DomainId, "PInfo3", "Family or Individual");


        //    if (SuccessCode == "1010")
        //    {
        //        ResultData.Data = "Record successfully inserted ";
        //    }
        //    else
        //    {
        //        ResultData.Data = "Fail";
        //    }

        //    return ResultData;

        //    // return ResultData;
        //}

        public ActionResult HeightandWeight(Vitals hw)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            string SuccessCode = "";
            HeightandWeightServiceCalls hwservicecalls = null;
            hwservicecalls = new HeightandWeightServiceCalls();


            hw.HnW.CreatedOn = DateTime.Now;
            hw.HnW.DomainId = LoginUser.DomainId;
            hw.HnW.UserId = LoginUser.UserId;

            if (hw.Systole == null && hw.Diastole == null)
            {
                hw.HnW.BloodPressure = hw.HnW.BloodPressure;
            }
            if (hw.Systole != null && hw.Diastole != null)
            {
                hw.HnW.BloodPressure = hw.Systole + "/" + hw.Diastole;
            }


            if (hw.HnW.Weight1 != null)
            {
                float val2 = (float)hw.HnW.Weight1;
                double weight = (float)val2 * 2.2;
                hw.HnW.Weight = Math.Round(weight, 2);
            }
            if (hw.HnW.feets != 0 && hw.HnW.inches != null)
            {
                if (hw.HnW.inches == 0)
                {
                    hw.HnW.inches = hw.HnW.inches;
                }
                else if (hw.HnW.inches <= 9)
                {
                    hw.HnW.inches = ((hw.HnW.inches) / 10);
                }
                else
                {
                    hw.HnW.inches = ((hw.HnW.inches) / 100);
                }
                var ht = hw.HnW.feets + hw.HnW.inches;
                hw.HnW.Height = (ht * 30.48);
            }

            SuccessCode = hwservicecalls.InsertHeightandWeightInfo(hw.HnW);
            if (SuccessCode == "1010")
            {
                Vitals _vitals = new Vitals();
                _vitals.HnW = GetAllVitals.GetRecentHWObjByUserId(LoginUser.UserId);
                return PartialView("VitalsEssentialInfo", _vitals);
            }
            // else
            // {
            // ResultData.Data = "Fail";
            // return ResultData;
            // }
            Services3 s3 = new Services3();
            hw.MedInfo = s3.GetMedicalInfo(LoginUser.UserId);

            if (hw.MedInfo.BMIHeight == null)
            {
                hw.MedInfo.BMIHeight = hw.HnW.Height;
            }

            if (hw.MedInfo.BloodGroup == null)
            {
                hw.MedInfo.BloodGroup = hw.HnW.BloodGroup;
            }


            if (hw.HnW.Weight != null)
            {
                hw.MedInfo.BMIWeight = hw.HnW.Weight;
            }
            else
            {

            }

            if (hw.HnW.BloodPressure != null)
            {
                hw.MedInfo.BloodPressure = hw.HnW.BloodPressure;
            }
            else
            {
            }

            int result = s3.UpdateMedicalInfo(hw.MedInfo, LoginUser.UserId, LoginUser.DomainId, "PInfo3", "Family or Individual");


            if (SuccessCode == "1010")
            {
                ResultData.Data = "Record successfully inserted ";
            }
            else
            {
                ResultData.Data = "Fail";
            }

            return ResultData;

            // return ResultData;
        }

        /* venkateswari */
        public ActionResult VitalsNew2()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            return View();

        }



        [HttpPost]
        public ActionResult UrineTest(UrineTest ut)
        {
            JsonResult Resultdata = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            string SuccessCode = "";
            UrineTestServiceCalls utservicecalls = null;
            utservicecalls = new UrineTestServiceCalls();

            ut.CreatedOn = System.DateTime.Now;
            ut.UserId = LoginUser.UserId;
            ut.DomainId = LoginUser.DomainId;

            SuccessCode = utservicecalls.InsertUrineTestInfo(ut);
            if (SuccessCode == "1010")
            {
                Vitals _vitals = new Vitals();
                _vitals.UT = GetAllVitals.GetRecentUrineTestObjByUserId(LoginUser.UserId);
                return PartialView("_urineTest", _vitals);
            }
            else
            {
                Resultdata.Data = "Fail";
                return Resultdata;
            }

        }

        public ActionResult UrineTestEdit()
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

            return PartialView("UrineTestEdit");
        }

        public ActionResult VitalsSkinTest()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }
            Vitals _vitals = null; _vitals = new Vitals();
            // CompleteBPServiceCalls cbpservicecalls = null;
            // cbpservicecalls = new CompleteBPServiceCalls();
            // USTServiceCalls ustSerCalls = new USTServiceCalls();
            // HeightandWeightServiceCalls HnWServiceCalls = new HeightandWeightServiceCalls();
            // Services3 s3 = new Services3();
            // _vitals.STDs = GetAllVitals.GetRecentStdsObjByUserId(LoginUser.UserId);
            // _vitals.HnW = GetAllVitals.GetRecentHWObjByUserId(LoginUser.UserId);
            // _vitals.LstHnW = HnWServiceCalls.GetHnWbyUserId(LoginUser.UserId);
            // _vitals.MedInfo = s3.GetMedicalInfo(LoginUser.UserId);

            _vitals.ST = GetAllVitals.GetRecentSkinTestObjByUserId(LoginUser.UserId);
            DateTime time = Convert.ToDateTime(_vitals.ST.CreatedOn);
            _vitals.ST.created = time.ToString("dd MMMM yyyy");
            // _vitals.UT = GetAllVitals.GetRecentUrineTestObjByUserId(LoginUser.UserId);
            // _vitals.FHR = GetAllVitals.GetRecentFhrObjByUserId(LoginUser.UserId);
            // _vitals.UST = GetAllVitals.GetRecentUSTObjByUserId(LoginUser.UserId);
            // _vitals.CompleteBP = GetAllVitals.GetRecentBInfoByUserId(LoginUser.UserId);
            // _vitals.LstBT = GetAllVitals.GetAllBTsInfoByUserId(LoginUser.UserId).ToList();
            // _vitals.LstBT = _vitals.LstBT.GroupBy(x => x.BloodTestName).Select(y => y.First()).ToList();
            // _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
            // _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
            // _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
            // _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);//need to integreate
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;

            return View(_vitals);

        }

        public ActionResult SkinTestEdit()
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

            Vitals _vitals = new Vitals();
            _vitals.ST = GetAllVitals.GetRecentSkinTestObjByUserId(LoginUser.UserId);
            return PartialView(_vitals);
        }
        [HttpPost]
        public ActionResult SkinTest(SkinTest st)
        {
            JsonResult Resultdata = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            string SuccessCode = "";
            SkinTestServiceCalls stservicecalls = null;
            stservicecalls = new SkinTestServiceCalls();



            st.CreatedOn = System.DateTime.Now;
            st.UserId = LoginUser.UserId;
            st.DomainId = LoginUser.DomainId;



            SuccessCode = stservicecalls.InsertSkinTestInfo(st);

            if (SuccessCode == "1010")
            {
                Vitals _vitals = new Vitals();
                _vitals.lstSkinTest = GetAllVitals.GetRecentSkinTestByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList();
                _vitals.ST = _vitals.lstSkinTest.FirstOrDefault();
                return PartialView("VitalsSkinTest", _vitals);

            }
            else
            {
                Resultdata.Data = "Fail";
                return Resultdata;
            }

        }




        public ActionResult VitalsStds()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }
            Vitals _vitals = null; _vitals = new Vitals();
            // CompleteBPServiceCalls cbpservicecalls = null;
            // cbpservicecalls = new CompleteBPServiceCalls();
            // USTServiceCalls ustSerCalls = new USTServiceCalls();
            // HeightandWeightServiceCalls HnWServiceCalls = new HeightandWeightServiceCalls();
            // Services3 s3 = new Services3();
            _vitals.STDs = GetAllVitals.GetRecentStdsObjByUserId(LoginUser.UserId);
            DateTime time = Convert.ToDateTime(_vitals.STDs.CreatedOn);
            _vitals.STDs.created = time.ToString("dd MMMM yyyy");
            // _vitals.HnW = GetAllVitals.GetRecentHWObjByUserId(LoginUser.UserId);
            // _vitals.LstHnW = HnWServiceCalls.GetHnWbyUserId(LoginUser.UserId);
            // _vitals.MedInfo = s3.GetMedicalInfo(LoginUser.UserId);

            // _vitals.ST = GetAllVitals.GetRecentSkinTestObjByUserId(LoginUser.UserId);
            // _vitals.UT = GetAllVitals.GetRecentUrineTestObjByUserId(LoginUser.UserId);
            // _vitals.FHR = GetAllVitals.GetRecentFhrObjByUserId(LoginUser.UserId);
            // _vitals.UST = GetAllVitals.GetRecentUSTObjByUserId(LoginUser.UserId);
            // _vitals.CompleteBP = GetAllVitals.GetRecentBInfoByUserId(LoginUser.UserId);
            // _vitals.LstBT = GetAllVitals.GetAllBTsInfoByUserId(LoginUser.UserId).ToList();
            // _vitals.LstBT = _vitals.LstBT.GroupBy(x => x.BloodTestName).Select(y => y.First()).ToList();
            // _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
            // _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
            // _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
            // _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);//need to integreate
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;

            return View(_vitals);

        }
        [HttpPost]
        public ActionResult STDs(STDs stds)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            string SuccessCode = "";
            STDsServiceCalls stdsservicecalls = null;
            stdsservicecalls = new STDsServiceCalls();

            stds.CreatedOn = System.DateTime.Now;
            stds.UserId = LoginUser.UserId;
            stds.DomainId = LoginUser.DomainId;

            SuccessCode = stdsservicecalls.InsertSTDsInfo(stds);


            if (SuccessCode == "1010")
            {

                Vitals _vitals = new Vitals();
                _vitals.STDs = GetAllVitals.GetRecentStdsObjByUserId(LoginUser.UserId);
                return PartialView("VitalsStds", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        public ActionResult STDsEdit()
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
            Vitals _vitals = new Vitals();
            _vitals.STDs = GetAllVitals.GetRecentStdsObjByUserId(LoginUser.UserId);
            return PartialView(_vitals);
        }


        public ActionResult VitalsFhr()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }
            Vitals _vitals = null; _vitals = new Vitals();
            // CompleteBPServiceCalls cbpservicecalls = null;
            // cbpservicecalls = new CompleteBPServiceCalls();
            // USTServiceCalls ustSerCalls = new USTServiceCalls();
            // HeightandWeightServiceCalls HnWServiceCalls = new HeightandWeightServiceCalls();
            // Services3 s3 = new Services3();
            // _vitals.STDs = GetAllVitals.GetRecentStdsObjByUserId(LoginUser.UserId);
            // _vitals.HnW = GetAllVitals.GetRecentHWObjByUserId(LoginUser.UserId);
            // _vitals.LstHnW = HnWServiceCalls.GetHnWbyUserId(LoginUser.UserId);
            // _vitals.MedInfo = s3.GetMedicalInfo(LoginUser.UserId);

            // _vitals.ST = GetAllVitals.GetRecentSkinTestObjByUserId(LoginUser.UserId);v
            // _vitals.UT = GetAllVitals.GetRecentUrineTestObjByUserId(LoginUser.UserId);
            _vitals.FHR = GetAllVitals.GetRecentFhrObjByUserId(LoginUser.UserId);
             DateTime time = Convert.ToDateTime(_vitals.FHR.CreatedOn);
             _vitals.FHR.created = time.ToString("dd MMMM yyyy");
            // _vitals.UST = GetAllVitals.GetRecentUSTObjByUserId(LoginUser.UserId);
            // _vitals.CompleteBP = GetAllVitals.GetRecentBInfoByUserId(LoginUser.UserId);
            // _vitals.LstBT = GetAllVitals.GetAllBTsInfoByUserId(LoginUser.UserId).ToList();
            // _vitals.LstBT = _vitals.LstBT.GroupBy(x => x.BloodTestName).Select(y => y.First()).ToList();
            // _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
            // _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
            // _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
            // _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);//need to integreate
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;

            return View(_vitals);

        }
        [HttpPost]
        public ActionResult FHRInfo(Vitals _vc)
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
            JsonResult ResultData = new JsonResult();

            string SuccessCode = "";




            FHRServiceCalls fhrservicecalls = new FHRServiceCalls();
            _vc.FHR.CreatedOn = System.DateTime.Now;
            _vc.FHR.UserId = LoginUser.UserId;
            _vc.FHR.DomainId = LoginUser.DomainId;



            SuccessCode = fhrservicecalls.InsertFHRInfo(_vc.FHR);

            if (SuccessCode == "1010")
            {
                Vitals _vitals = new Vitals();
                _vitals.FHR = GetAllVitals.GetRecentFhrObjByUserId(LoginUser.UserId);
                return PartialView("VitalsFhr", _vitals);
            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        public ActionResult FhrEdit()
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
            Vitals _vitals = null; _vitals = new Vitals();

            _vitals.FHR = GetAllVitals.GetRecentFhrObjByUserId(LoginUser.UserId);

            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);//need to integreate
            ViewBag.statusresponce = i.ResponseCount;
            ViewBag.statustip = i.TipCount;
            return PartialView(_vitals);
        }


        public ActionResult CompleteBPEdit()
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

            return PartialView("CompleteBPEdit");
        }

        [HttpPost]
        public ActionResult CompleteBPEdit(Vitals vtls)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            string SuccessCode = "";
            CompleteBPServiceCalls completeBPSvrCalls = null;
            completeBPSvrCalls = new CompleteBPServiceCalls();

            vtls.CompleteBP.UserId = LoginUser.UserId;
            vtls.CompleteBP.DomainId = LoginUser.DomainId;
            vtls.CompleteBP.CreatedOn = DateTime.Now;
            SuccessCode = completeBPSvrCalls.InsertBloodInfo(vtls.CompleteBP);

            if (SuccessCode == "1010")
            {

                vtls.CompleteBP = GetAllVitals.GetRecentBInfoByUserId(LoginUser.UserId);
                vtls.LstBT = GetAllVitals.GetAllBTsInfoByUserId(LoginUser.UserId).ToList();
                vtls.LstBT = vtls.LstBT.GroupBy(x => x.BloodTestName).Select(y => y.First()).ToList();
                return PartialView("_completeBP", vtls);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        [HttpPost]
        public ActionResult CompleteBP(Vitals cbp)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            string SuccessCode = "";
            CompleteBPServiceCalls cbpservicecalls = null;
            cbpservicecalls = new CompleteBPServiceCalls();
            cbp.CompleteBP.CreatedOn = DateTime.Now;
            cbp.CompleteBP.UserId = LoginUser.UserId;
            cbp.CompleteBP.DomainId = LoginUser.DomainId;
            SuccessCode = cbpservicecalls.InsertCompleteBPInfo(cbp.CompleteBP);

            if (SuccessCode == "1010")
            {
                Vitals _vitals = new Vitals();

                _vitals.LstCompleteBP = cbpservicecalls.GetCompleteBPbyUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList();
                _vitals.CompleteBP = _vitals.LstCompleteBP.FirstOrDefault();
                return PartialView("_completeBP", _vitals);
            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        public ActionResult AddBloodTestPopup(string bloodTestName, string bloodTestValue)
        {
            ViewBag.BloodTestName = bloodTestName;
            ViewBag.BloodTestValue = bloodTestValue;
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddBloodTest(Vitals vtls)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            string SuccessCode = "";
            BloodTestsServiceCalls btServCalls = new BloodTestsServiceCalls();

            vtls.BT.UserId = LoginUser.UserId;
            vtls.BT.DomainId = LoginUser.DomainId;
            vtls.BT.CreatedOn = DateTime.Now;
            vtls.BT.BloodTestName = vtls.BTName;
            SuccessCode = btServCalls.InsertBloodTestsInfo(vtls.BT);
            if (SuccessCode == "1010")
            {
                vtls.CompleteBP = GetAllVitals.GetRecentBInfoByUserId(LoginUser.UserId);
                vtls.LstBT = GetAllVitals.GetAllBTsInfoByUserId(LoginUser.UserId).ToList();
                vtls.LstBT = vtls.LstBT.GroupBy(x => x.BloodTestName).Select(y => y.First()).ToList();
                return PartialView("_completeBP", vtls);
            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }

        }

        public ActionResult WeightTrend()
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

            Vitals _vitals = null; _vitals = new Vitals();
            USTServiceCalls ustSerCalls = new USTServiceCalls();
            HeightandWeightServiceCalls HnWServiceCalls = new HeightandWeightServiceCalls();
            _vitals.LstHnW = HnWServiceCalls.GetHnWbyUserId(LoginUser.UserId);

            JsonResult ResultData = new JsonResult();
            ResultData.Data = _vitals.LstHnW;


            return Json(_vitals.LstHnW.OrderBy(e => e.CreatedOn), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BloodPressureTrend()
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

            Vitals _vitals = null; _vitals = new Vitals();
            CompleteBPServiceCalls cbpservicecalls = null;
            cbpservicecalls = new CompleteBPServiceCalls();

            _vitals.LstCompleteBP = cbpservicecalls.GetCompleteBPbyUserId(LoginUser.UserId);
            _vitals.CompleteBP = _vitals.LstCompleteBP.FirstOrDefault();
            JsonResult ResultData = new JsonResult();
            ResultData.Data = _vitals.LstCompleteBP;

            return Json(_vitals.LstCompleteBP.OrderByDescending(e => e.CreatedOn), JsonRequestBehavior.AllowGet);

        }
        public ActionResult STDsTrend()
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


            STDsServiceCalls _StdServiceCalls = new STDsServiceCalls();
            Vitals _vitals = new Vitals();

            _vitals.LstSTDs = GetAllVitals.GetRecentStdsByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList();
            return PartialView(_vitals);
        }

        public ActionResult SkinTestTrend()
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

            SkinTestServiceCalls _STServiceCalls = new SkinTestServiceCalls();
            Vitals _vitals = new Vitals();

            _vitals.lstSkinTest = GetAllVitals.GetRecentSkinTestByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList();
            return PartialView(_vitals);
        }

        public ActionResult UstTrend()
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

            USTServiceCalls ustSvrCalls = null;
            ustSvrCalls = new USTServiceCalls();
            Vitals _vitals = new Vitals();
            _vitals.LstUST = ustSvrCalls.GetUSTInfoLstByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList();
            _vitals.UST = _vitals.LstUST.FirstOrDefault();
            return PartialView(_vitals);
        }

        public ActionResult UrineTestTrend()
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

            UrineTestServiceCalls _UTServiceCalls = new UrineTestServiceCalls();
            Vitals _vitals = new Vitals();

            _vitals.lstUrineTest = GetAllVitals.GetRecentUrineTestByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList();


            return PartialView(_vitals);
        }

        public ActionResult UstAdd()
        {
            return PartialView("SelectScan");
        }
        public ActionResult EarlyScan()
        {
            return PartialView();
        }
        public ActionResult NTScan()
        {
            return PartialView();
        }
        public ActionResult AnamolyScan()
        {
            return PartialView();
        }

        public ActionResult GrowthScan()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult USTObservations(UST ust)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            string SuccessCode = "";
            USTServiceCalls ustSvrCalls = null;
            ustSvrCalls = new USTServiceCalls();

            ust.CreatedOn = System.DateTime.Now;
            ust.UserId = LoginUser.UserId;
            ust.DomainId = LoginUser.DomainId;

            ust.USObservations = XmlStringListSerializer.ToXmlString(ust);

            SuccessCode = ustSvrCalls.InsertUSTInfo(ust);

            if (SuccessCode == "1010")
            {
                USTServiceCalls ustSerCalls = new USTServiceCalls();
                Vitals _vitals = new Vitals();
                _vitals.UST = GetAllVitals.GetRecentUSTObjByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);
            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }

        }

        public ActionResult FHRTrend()
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

            Vitals _vc = new Vitals();
            _vc.lstFhr = GetAllVitals.GetRecentFHRByUserId(LoginUser.UserId).OrderBy(x => x.CreatedOn).ToList();
            JsonResult ResultData = new JsonResult();
            ResultData.Data = _vc.lstFhr;


            return Json(_vc.lstFhr.OrderBy(e => e.CreatedOn), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BloodTestsTrend(string BTsName)
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

            USTServiceCalls ustSvrCalls = null;
            ustSvrCalls = new USTServiceCalls();
            Vitals _vitals = new Vitals();
            ViewBag.BTname = BTsName;
            _vitals.LstBT = GetAllVitals.GetAllBTsInfoByUserIdandBTName(LoginUser.UserId, BTsName);
            return PartialView(_vitals);
        }

        #endregion

        [HttpPost]
        public ActionResult SaveEarlyScan(EarlyScan es)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            EarlyScan esinfo = new EarlyScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();
            EarlyFindings ef = new EarlyFindings();
            esinfo.EarlyScanId = es.EarlyScanId;
            esinfo.Impression = es.Impression;
            esinfo.Indications = es.Indications;
            esinfo.PatientId = es.PatientId;
            esinfo.PatientName = es.PatientName;
            esinfo.Radiologist = es.Radiologist;
            esinfo.Recomandations = es.Recomandations;
            esinfo.ReffDoctor = es.ReffDoctor;
            esinfo.ScanOnDate = es.ScanDate;
            esinfo.UserId = LoginUser.UserId;

            ed.ByDates = es.EDDvalue.ByDates;
            ed.ByUltra = es.EDDvalue.ByUltra;
            esinfo.EDD = XmlStringListSerializer.ToXmlString<EDD>(ed);
            ges.ByDates = es.Gestinat.ByDates;
            ges.ByUltra = es.Gestinat.ByUltra;
            esinfo.Gestation = XmlStringListSerializer.ToXmlString<Gestation>(ges);


            ef.Adnexae = es.Findings.Adnexae;
            ef.CRL = es.Findings.CRL;
            ef.FHS = es.Findings.FHS;
            ef.YolkSac = es.Findings.YolkSac;
            ef.Gestationalsac = es.Findings.Gestationalsac;
            esinfo.EarlyFindings = XmlStringListSerializer.ToXmlString<EarlyFindings>(ef);
            CreateUltraScans CreateUltraScans = new CreateUltraScans();

            string i = CreateUltraScans.InsertEarlyScanInfo(esinfo);


            if (i == "1010")
            {
                Vitals _vitals = new Vitals();
                _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
                _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
                _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
                _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }



        }
        [HttpPost]
        public ActionResult EditEarlyScan(EarlyScan es)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            EarlyScan esinfo = new EarlyScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();
            EarlyFindings ef = new EarlyFindings();
            esinfo.EarlyScanId = es.EarlyScanId;
            esinfo.Impression = es.Impression;
            esinfo.Indications = es.Indications;
            esinfo.PatientId = es.PatientId;
            esinfo.PatientName = es.PatientName;
            esinfo.Radiologist = es.Radiologist;
            esinfo.Recomandations = es.Recomandations;
            esinfo.ReffDoctor = es.ReffDoctor;
            //  esinfo.ScanOnDate = es.ScanDate;
            esinfo.ScanOnDate = Convert.ToDateTime(es.ScanDate1);
            esinfo.UserId = LoginUser.UserId;

            //ed.ByDates = es.EDDvalue.ByDates;
            //ed.ByUltra = es.EDDvalue.ByUltra;
            if (es.EDDvalue.ByDates1 != null)
            {
                ed.ByDates = Convert.ToDateTime(es.EDDvalue.ByDates1);
            }
            else
            {
                ed.ByDates = null;
            }
            if (es.EDDvalue.ByUltra1 != null)
            {
                ed.ByUltra = Convert.ToDateTime(es.EDDvalue.ByUltra1);
            }
            else
            {
                ed.ByUltra = null;
            }
            esinfo.EDD = XmlStringListSerializer.ToXmlString<EDD>(ed);
            ges.ByDates = es.Gestinat.ByDates;
            ges.ByUltra = es.Gestinat.ByUltra;
            esinfo.Gestation = XmlStringListSerializer.ToXmlString<Gestation>(ges);


            ef.Adnexae = es.Findings.Adnexae;
            ef.CRL = es.Findings.CRL;
            ef.FHS = es.Findings.FHS;
            ef.YolkSac = es.Findings.YolkSac;
            ef.Gestationalsac = es.Findings.Gestationalsac;
            esinfo.EarlyFindings = XmlStringListSerializer.ToXmlString<EarlyFindings>(ef);
            EDitUltraScan ediScanObj = new EDitUltraScan();
            string i = ediScanObj.UpdateEarlyscan(esinfo);

            if (i == "1020")
            {
                Vitals _vitals = new Vitals();
                _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
                _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
                _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
                _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }






        }
        public ActionResult EarlyScanView(string esid)
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


            EarlyScan esinfo = GetUltraScan.GetEarlyScanByUserIdandId(LoginUser.UserId, esid);

            esinfo.Findings = XmlStringListSerializer.Deserialize<EarlyFindings>(esinfo.EarlyFindings);
            esinfo.Gestinat = XmlStringListSerializer.Deserialize<Gestation>(esinfo.Gestation);
            esinfo.EDDvalue = XmlStringListSerializer.Deserialize<EDD>(esinfo.EDD);
            esinfo.ScanDate = esinfo.ScanOnDate;
            DateTime time = Convert.ToDateTime(esinfo.ScanOnDate);
            esinfo.ScanDate1 = time.ToString("dd MMMM yyyy");
            //if (esinfo.ScanOnDate != null)
            //{
            //    esinfo.ScanDate1 = esinfo.ScanOnDate.Value.ToShortDateString();
            //}
            //else
            //{
            //    esinfo.ScanDate1 = string.Empty;
            //}
            if (esinfo.EDDvalue.ByDates != null)
            {
                DateTime time1 = Convert.ToDateTime(esinfo.EDDvalue.ByDates);
                //esinfo.ScanDate1 = time1.ToString("dd MM yyyy");
                //esinfo.EDDvalue.ByDates1 = esinfo.EDDvalue.ByDates.Value.ToShortDateString(); // sandeep added on 4-4-2014
                esinfo.EDDvalue.ByDates1 = time1.ToString("dd MMMM yyyy");
            }
            else
            {
                esinfo.EDDvalue.ByDates1 = string.Empty; // sandeep added on 4-4-2014
            }
            if (esinfo.EDDvalue.ByUltra != null)
            {
                DateTime time1 = Convert.ToDateTime(esinfo.EDDvalue.ByUltra);
                //esinfo.ScanDate1 = time1.ToString("dd MM yyyy");
                //esinfo.EDDvalue.ByDates1 = esinfo.EDDvalue.ByDates.Value.ToShortDateString(); // sandeep added on 4-4-2014
                esinfo.EDDvalue.ByUltra1 = time1.ToString("dd MMMM yyyy");
                // esinfo.EDDvalue.ByUltra1 = esinfo.EDDvalue.ByUltra.Value.ToShortDateString(); // sandeep added on 4-4-2014
            }
            else
            {
                esinfo.EDDvalue.ByUltra1 = string.Empty; // sandeep added on 4-4-2014
            }
            return PartialView(esinfo);

        }
        [HttpPost]
        public ActionResult SaveNTScan(NTScan nt)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            NTScan ntinfo = new NTScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();
            Chromosomalmarkers csm = new Chromosomalmarkers();
            UterineDoppler ud = new UterineDoppler();
            FetalAnatomy fa = new FetalAnatomy();
            MaternalStructures ms = new MaternalStructures();
            MaternalHistory mh = new MaternalHistory();

            FirstTrimeterUSSFindings ff = new FirstTrimeterUSSFindings();
            ntinfo.NtScanId = nt.NtScanId;
            ntinfo.Impression = nt.Impression;
            ntinfo.Indications = nt.Indications;
            ntinfo.PatientId = nt.PatientId;
            ntinfo.PatientName = nt.PatientName;
            ntinfo.Radiologist = nt.Radiologist;
            ntinfo.Recomandations = nt.Recomandations;
            ntinfo.ReffDoctor = nt.ReffDoctor;
            ntinfo.ScanOnDate = nt.ScanDate;
            ntinfo.UserId = LoginUser.UserId;

            ed.ByDates = nt.EDDvalue.ByDates;
            ed.ByUltra = nt.EDDvalue.ByUltra;
            ntinfo.EDD = XmlStringListSerializer.ToXmlString<EDD>(ed);
            ges.ByDates = nt.Gestinat.ByDates;
            ges.ByUltra = nt.Gestinat.ByUltra;
            ntinfo.Gestation = XmlStringListSerializer.ToXmlString<Gestation>(ges);
            ff.NumberofFetuses = nt.FTUSSFindings.NumberofFetuses;
            ff.Amnioticfluid = nt.FTUSSFindings.Amnioticfluid;
            ff.BPD = nt.FTUSSFindings.BPD;
            ff.Cord = nt.FTUSSFindings.Cord;
            ff.CRL = nt.FTUSSFindings.CRL;
            ff.FHR = nt.FTUSSFindings.FHR;
            ff.HC = nt.FTUSSFindings.HC;
            ff.NT = nt.FTUSSFindings.NT;
            ff.Placenta = nt.FTUSSFindings.Placenta;
            ntinfo.UsFinds = XmlStringListSerializer.ToXmlString<FirstTrimeterUSSFindings>(ff);
            csm.DuctusvenosusDoppler = nt.CHSmarkers.DuctusvenosusDoppler;
            csm.Facialangle = nt.CHSmarkers.Facialangle;
            csm.Nasalbone = nt.CHSmarkers.Nasalbone;
            csm.TricuspidDoppler = nt.CHSmarkers.TricuspidDoppler;

            ntinfo.ChromosomalMark = XmlStringListSerializer.ToXmlString<Chromosomalmarkers>(csm);
            ud.LowestPI = nt.UterineDoppler1.LowestPI;
            ud.Patientmotherhadpet = nt.UterineDoppler1.Patientmotherhadpet;
            ud.PiLeft = nt.UterineDoppler1.PiLeft;
            ud.Piright = nt.UterineDoppler1.Piright;
            ntinfo.UterineDoppler = XmlStringListSerializer.ToXmlString<UterineDoppler>(ud);

            fa.Abdomen = nt.FetalAnatomy1.Abdomen;
            fa.Bladder = nt.FetalAnatomy1.Bladder;
            fa.Feet = nt.FetalAnatomy1.Feet;
            fa.Hands = nt.FetalAnatomy1.Hands;
            fa.Heart = nt.FetalAnatomy1.Heart;
            fa.Skull = nt.FetalAnatomy1.Skull;
            fa.Spine = nt.FetalAnatomy1.Spine;
            fa.Stomoch = nt.FetalAnatomy1.Stomoch;
            ntinfo.FetalAntomy = XmlStringListSerializer.ToXmlString<FetalAnatomy>(fa);

            ms.Leftovary = nt.MaternalStructures1.Leftovary;
            ms.Rightovary = nt.MaternalStructures1.Rightovary;
            ntinfo.MaternalStructure = XmlStringListSerializer.ToXmlString<MaternalStructures>(ms);
            mh.Age = nt.MaternalHistory1.Age;
            mh.BloodGroup = nt.MaternalHistory1.BloodGroup;
            mh.BmI = nt.MaternalHistory1.BmI;
            mh.Height = nt.MaternalHistory1.Height;
            mh.LastPeriod = nt.MaternalHistory1.LastPeriod;
            mh.Weight = nt.MaternalHistory1.Weight;
            ntinfo.MaternalHistory = XmlStringListSerializer.ToXmlString<MaternalHistory>(mh);
            CreateUltraScans CreateUltraScans = new CreateUltraScans();
            string i = CreateUltraScans.InsertNTScanInfo(ntinfo);


            if (i == "1010")
            {
                Vitals _vitals = new Vitals();
                _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
                _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
                _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
                _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        [HttpPost]
        public ActionResult EditNTScan(NTScan nt)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            NTScan ntinfo = new NTScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();
            Chromosomalmarkers csm = new Chromosomalmarkers();
            UterineDoppler ud = new UterineDoppler();
            FetalAnatomy fa = new FetalAnatomy();
            MaternalStructures ms = new MaternalStructures();
            MaternalHistory mh = new MaternalHistory();

            FirstTrimeterUSSFindings ff = new FirstTrimeterUSSFindings();
            ntinfo.NtScanId = nt.NtScanId;
            ntinfo.Impression = nt.Impression;
            ntinfo.Indications = nt.Indications;
            ntinfo.PatientId = nt.PatientId;
            ntinfo.PatientName = nt.PatientName;
            ntinfo.Radiologist = nt.Radiologist;
            ntinfo.Recomandations = nt.Recomandations;
            ntinfo.ReffDoctor = nt.ReffDoctor;
            // ntinfo.ScanOnDate = nt.ScanDate;
            ntinfo.ScanOnDate = Convert.ToDateTime(nt.ScanDate1);
            ntinfo.UserId = LoginUser.UserId;

            //ed.ByDates = nt.EDDvalue.ByDates;
            //ed.ByUltra = nt.EDDvalue.ByUltra;
            // ed.ByDates = Convert.ToDateTime(nt.EDDvalue.ByDates1);
            // ed.ByUltra = Convert.ToDateTime(nt.EDDvalue.ByUltra1);
            if (nt.EDDvalue.ByDates1 != null)
            {
                ed.ByDates = Convert.ToDateTime(nt.EDDvalue.ByDates1);
            }
            else
            {
                ed.ByDates = null;
            }
            if (nt.EDDvalue.ByUltra1 != null)
            {
                ed.ByUltra = Convert.ToDateTime(nt.EDDvalue.ByUltra1);
            }
            else
            {
                ed.ByUltra = null;
            }

            ntinfo.EDD = XmlStringListSerializer.ToXmlString<EDD>(ed);
            ges.ByDates = nt.Gestinat.ByDates;
            ges.ByUltra = nt.Gestinat.ByUltra;
            ntinfo.Gestation = XmlStringListSerializer.ToXmlString<Gestation>(ges);
            ff.NumberofFetuses = nt.FTUSSFindings.NumberofFetuses;
            ff.Amnioticfluid = nt.FTUSSFindings.Amnioticfluid;
            ff.BPD = nt.FTUSSFindings.BPD;
            ff.Cord = nt.FTUSSFindings.Cord;
            ff.CRL = nt.FTUSSFindings.CRL;
            ff.FHR = nt.FTUSSFindings.FHR;
            ff.HC = nt.FTUSSFindings.HC;
            ff.NT = nt.FTUSSFindings.NT;
            ff.Placenta = nt.FTUSSFindings.Placenta;
            ntinfo.UsFinds = XmlStringListSerializer.ToXmlString<FirstTrimeterUSSFindings>(ff);
            csm.DuctusvenosusDoppler = nt.CHSmarkers.DuctusvenosusDoppler;
            csm.Facialangle = nt.CHSmarkers.Facialangle;
            csm.Nasalbone = nt.CHSmarkers.Nasalbone;
            csm.TricuspidDoppler = nt.CHSmarkers.TricuspidDoppler;

            ntinfo.ChromosomalMark = XmlStringListSerializer.ToXmlString<Chromosomalmarkers>(csm);
            ud.LowestPI = nt.UterineDoppler1.LowestPI;
            ud.Patientmotherhadpet = nt.UterineDoppler1.Patientmotherhadpet;
            ud.PiLeft = nt.UterineDoppler1.PiLeft;
            ud.Piright = nt.UterineDoppler1.Piright;
            ntinfo.UterineDoppler = XmlStringListSerializer.ToXmlString<UterineDoppler>(ud);

            fa.Abdomen = nt.FetalAnatomy1.Abdomen;
            fa.Bladder = nt.FetalAnatomy1.Bladder;
            fa.Feet = nt.FetalAnatomy1.Feet;
            fa.Hands = nt.FetalAnatomy1.Hands;
            fa.Heart = nt.FetalAnatomy1.Heart;
            fa.Skull = nt.FetalAnatomy1.Skull;
            fa.Spine = nt.FetalAnatomy1.Spine;
            fa.Stomoch = nt.FetalAnatomy1.Stomoch;
            ntinfo.FetalAntomy = XmlStringListSerializer.ToXmlString<FetalAnatomy>(fa);

            ms.Leftovary = nt.MaternalStructures1.Leftovary;
            ms.Rightovary = nt.MaternalStructures1.Rightovary;
            ntinfo.MaternalStructure = XmlStringListSerializer.ToXmlString<MaternalStructures>(ms);
            mh.Age = nt.MaternalHistory1.Age;
            mh.BloodGroup = nt.MaternalHistory1.BloodGroup;
            mh.BmI = nt.MaternalHistory1.BmI;
            mh.Height = nt.MaternalHistory1.Height;
            mh.LastPeriod = nt.MaternalHistory1.LastPeriod;
            mh.Weight = nt.MaternalHistory1.Weight;
            ntinfo.MaternalHistory = XmlStringListSerializer.ToXmlString<MaternalHistory>(mh);
            EDitUltraScan ediScanObj = new EDitUltraScan();
            string i = ediScanObj.UpdateNTScan(ntinfo);


            if (i == "1020")
            {
                Vitals _vitals = new Vitals();
                _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
                _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
                _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
                _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }
        public ActionResult NTScanview(string ntid)
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
            NTScan ntinfo = new NTScan();
            NTScan ntin = new NTScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();
            Chromosomalmarkers csm = new Chromosomalmarkers();
            UterineDoppler ud = new UterineDoppler();
            FetalAnatomy fa = new FetalAnatomy();
            MaternalStructures ms = new MaternalStructures();
            MaternalHistory mh = new MaternalHistory();

            ntinfo = GetUltraScan.GetNTScanByUserIdandId(LoginUser.UserId, ntid);
            ntin.Impression = ntinfo.Impression;
            ntin.NtScanId = ntinfo.NtScanId;
            ntin.PatientId = ntinfo.PatientId;
            ntin.Indications = ntinfo.Indications;
            ntin.PatientName = ntinfo.PatientName;
            ntin.Radiologist = ntinfo.Radiologist;
            ntin.Recomandations = ntinfo.Recomandations;
            ntin.ReffDoctor = ntinfo.ReffDoctor;
            //ntin.ScanDate = ntinfo.ScanOnDate;
            DateTime time = Convert.ToDateTime(ntinfo.ScanOnDate);
            ntin.ScanDate1 = time.ToString("dd MMMM yyyy");
            //if (ntinfo.ScanOnDate != null)
            //{
            //    ntin.ScanDate1 = ntinfo.ScanOnDate.Value.ToShortDateString();
            //}
            //else
            //{
            //    ntin.ScanDate1 = string.Empty;
            //}
            ntin.FTUSSFindings = XmlStringListSerializer.Deserialize<FirstTrimeterUSSFindings>(ntinfo.UsFinds);
            ntin.Gestinat = XmlStringListSerializer.Deserialize<Gestation>(ntinfo.Gestation);
            ntin.FetalAnatomy1 = XmlStringListSerializer.Deserialize<FetalAnatomy>(ntinfo.FetalAntomy);
            ntin.EDDvalue = XmlStringListSerializer.Deserialize<EDD>(ntinfo.EDD);
            if (ntin.EDDvalue.ByDates != null)
            {
                DateTime time1 = Convert.ToDateTime(ntin.EDDvalue.ByDates);
                // ntin.EDDvalue.ByDates1 = ntin.EDDvalue.ByDates.Value.ToShortDateString(); // sandeep added on 4-4-2014
                ntin.EDDvalue.ByDates1 = time1.ToString("dd MMMM yyyy");
            }
            else
            {
                ntin.EDDvalue.ByDates1 = string.Empty; // sandeep added on 4-4-2014
            }
            if (ntin.EDDvalue.ByUltra != null)
            {
                DateTime time1 = Convert.ToDateTime(ntin.EDDvalue.ByUltra);
                //ntin.EDDvalue.ByUltra1 = ntin.EDDvalue.ByUltra.Value.ToShortDateString(); // sandeep added on 4-4-2014
                ntin.EDDvalue.ByUltra1 = time1.ToString("dd MMMM yyyy");
            }
            else
            {
                ntin.EDDvalue.ByUltra1 = string.Empty; // sandeep added on 4-4-2014
            }
            ntin.MaternalStructures1 = XmlStringListSerializer.Deserialize<MaternalStructures>(ntinfo.MaternalStructure);
            ntin.MaternalHistory1 = XmlStringListSerializer.Deserialize<MaternalHistory>(ntinfo.MaternalHistory);
            ntin.UterineDoppler1 = XmlStringListSerializer.Deserialize<UterineDoppler>(ntinfo.UterineDoppler);
            ntin.CHSmarkers = XmlStringListSerializer.Deserialize<Chromosomalmarkers>(ntinfo.ChromosomalMark);

            return PartialView(ntin);
        }
        [HttpPost]
        public ActionResult SaveAnomalyScan(AnomalyScan nt)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            AnomalyScan asinfo = new AnomalyScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();

            UterineDoppler ud = new UterineDoppler();
            FetalAnatomy fa = new FetalAnatomy();
            Cervicalassessment ca = new Cervicalassessment();
            MaternalHistory mh = new MaternalHistory();

            FirstTrimeterUSSFindings ff = new FirstTrimeterUSSFindings();
            asinfo.anomalyId = nt.anomalyId;
            asinfo.Impression = nt.Impression;
            asinfo.Indications = nt.Indications;
            asinfo.PatientId = nt.PatientId;
            asinfo.PatientName = nt.PatientName;
            asinfo.Radiologist = nt.Radiologist;
            asinfo.Recomandations = nt.Recomandations;
            asinfo.ReffDoctor = nt.ReffDoctor;
            asinfo.ScanOnDate = nt.ScanDate;
            asinfo.UserId = LoginUser.UserId;

            ed.ByDates = nt.EDDvalue.ByDates;
            ed.ByUltra = nt.EDDvalue.ByUltra;
            asinfo.EDD = XmlStringListSerializer.ToXmlString<EDD>(ed);
            ges.ByDates = nt.Gestinat.ByDates;
            ges.ByUltra = nt.Gestinat.ByUltra;
            asinfo.Gestation = XmlStringListSerializer.ToXmlString<Gestation>(ges);
            ff.Amnioticfluid = nt.NTScanFinds.Amnioticfluid;
            ff.BPD = nt.NTScanFinds.BPD;
            ff.Efw = nt.NTScanFinds.Efw;
            ff.CRL = nt.NTScanFinds.CRL;
            ff.FHR = nt.NTScanFinds.FHR;
            ff.HC = nt.NTScanFinds.HC;
            ff.NT = nt.NTScanFinds.NT;
            ff.Movenents = nt.NTScanFinds.Movenents;
            ff.NumberofFetuses = nt.NTScanFinds.NumberofFetuses;
            ff.Placenta = nt.NTScanFinds.Placenta;
            ff.Ac = nt.NTScanFinds.Ac;
            ff.Fl = nt.NTScanFinds.Fl;
            ff.PlacentaGrade = nt.NTScanFinds.PlacentaGrade;
            asinfo.UsFinds = XmlStringListSerializer.ToXmlString<FirstTrimeterUSSFindings>(ff);


            ud.PiLeft = nt.UterineDoppler1.PiLeft;
            ud.Piright = nt.UterineDoppler1.Piright;
            asinfo.UterineDoppler = XmlStringListSerializer.ToXmlString<UterineDoppler>(ud);


            fa.Abdomen = nt.FetalAnatomy1.Abdomen;
            fa.Bladder = nt.FetalAnatomy1.Bladder;
            fa.Feet = nt.FetalAnatomy1.Feet;
            fa.Hands = nt.FetalAnatomy1.Hands;
            fa.Heart = nt.FetalAnatomy1.Heart;
            fa.Skull = nt.FetalAnatomy1.Skull;
            fa.Spine = nt.FetalAnatomy1.Spine;
            fa.Stomoch = nt.FetalAnatomy1.Stomoch;
            fa.AVH = nt.FetalAnatomy1.AVH;
            fa.Brain = nt.FetalAnatomy1.Brain;
            fa.Extremities = nt.FetalAnatomy1.Extremities;
            fa.Genetilia = nt.FetalAnatomy1.Genetilia;
            fa.Glt = nt.FetalAnatomy1.Glt;
            fa.PVH = nt.FetalAnatomy1.PVH;
            fa.Thorax = nt.FetalAnatomy1.Thorax;
            fa.UrinTract = nt.FetalAnatomy1.UrinTract;
            asinfo.FetalAntomy = XmlStringListSerializer.ToXmlString<FetalAnatomy>(fa);

            ca.CervicalComment = nt.Cervicalassesment.CervicalComment;
            ca.Cervixlength = nt.Cervicalassesment.Cervixlength;
            ca.Funnelling = nt.Cervicalassesment.Funnelling;
            asinfo.CervicalAssessment = XmlStringListSerializer.ToXmlString<Cervicalassessment>(ca);

            mh.Age = nt.MaternalHistory1.Age;
            mh.BloodGroup = nt.MaternalHistory1.BloodGroup;
            mh.BmI = nt.MaternalHistory1.BmI;
            mh.Height = nt.MaternalHistory1.Height;
            mh.LastPeriod = nt.MaternalHistory1.LastPeriod;
            mh.Weight = nt.MaternalHistory1.Weight;
            asinfo.MaternalHistory = XmlStringListSerializer.ToXmlString<MaternalHistory>(mh);
            CreateUltraScans CreateUltraScans = new CreateUltraScans();
            string i = CreateUltraScans.InsertAnomalyScanInfo(asinfo);


            if (i == "1010")
            {
                Vitals _vitals = new Vitals();
                _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
                _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
                _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
                _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        [HttpPost]
        public ActionResult EditAnomalyScan(AnomalyScan nt)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            AnomalyScan asinfo = new AnomalyScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();

            UterineDoppler ud = new UterineDoppler();
            FetalAnatomy fa = new FetalAnatomy();
            Cervicalassessment ca = new Cervicalassessment();
            MaternalHistory mh = new MaternalHistory();

            FirstTrimeterUSSFindings ff = new FirstTrimeterUSSFindings();
            asinfo.anomalyId = nt.anomalyId;
            asinfo.Impression = nt.Impression;
            asinfo.Indications = nt.Indications;
            asinfo.PatientId = nt.PatientId;
            asinfo.PatientName = nt.PatientName;
            asinfo.Radiologist = nt.Radiologist;
            asinfo.Recomandations = nt.Recomandations;
            asinfo.ReffDoctor = nt.ReffDoctor;
            //  asinfo.ScanOnDate = nt.ScanDate;
            asinfo.ScanOnDate = Convert.ToDateTime(nt.ScanDate1); // sandeep added on 4-4-2014

            asinfo.UserId = LoginUser.UserId;

            //ed.ByDates = nt.EDDvalue.ByDates;
            //ed.ByUltra = nt.EDDvalue.ByUltra;
            if (nt.EDDvalue.ByDates1 != null)
            {
                ed.ByDates = Convert.ToDateTime(nt.EDDvalue.ByDates1);
            }
            else
            {
                ed.ByDates = null;
            }
            if (nt.EDDvalue.ByUltra1 != null)
            {
                ed.ByUltra = Convert.ToDateTime(nt.EDDvalue.ByUltra1);
            }
            else
            {
                ed.ByUltra = null;
            }
            asinfo.EDD = XmlStringListSerializer.ToXmlString<EDD>(ed);
            ges.ByDates = nt.Gestinat.ByDates;
            ges.ByUltra = nt.Gestinat.ByUltra;
            asinfo.Gestation = XmlStringListSerializer.ToXmlString<Gestation>(ges);
            ff.Amnioticfluid = nt.NTScanFinds.Amnioticfluid;
            ff.BPD = nt.NTScanFinds.BPD;
            ff.Efw = nt.NTScanFinds.Efw;
            ff.CRL = nt.NTScanFinds.CRL;
            ff.FHR = nt.NTScanFinds.FHR;
            ff.HC = nt.NTScanFinds.HC;
            ff.NT = nt.NTScanFinds.NT;
            ff.Movenents = nt.NTScanFinds.Movenents;
            ff.NumberofFetuses = nt.NTScanFinds.NumberofFetuses;
            ff.Placenta = nt.NTScanFinds.Placenta;
            ff.Ac = nt.NTScanFinds.Ac;
            ff.Fl = nt.NTScanFinds.Fl;
            ff.PlacentaGrade = nt.NTScanFinds.PlacentaGrade;
            asinfo.UsFinds = XmlStringListSerializer.ToXmlString<FirstTrimeterUSSFindings>(ff);


            ud.PiLeft = nt.UterineDoppler1.PiLeft;
            ud.Piright = nt.UterineDoppler1.Piright;
            asinfo.UterineDoppler = XmlStringListSerializer.ToXmlString<UterineDoppler>(ud);


            fa.Abdomen = nt.FetalAnatomy1.Abdomen;
            fa.Bladder = nt.FetalAnatomy1.Bladder;
            fa.Feet = nt.FetalAnatomy1.Feet;
            fa.Hands = nt.FetalAnatomy1.Hands;
            fa.Heart = nt.FetalAnatomy1.Heart;
            fa.Skull = nt.FetalAnatomy1.Skull;
            fa.Spine = nt.FetalAnatomy1.Spine;
            fa.Stomoch = nt.FetalAnatomy1.Stomoch;
            fa.AVH = nt.FetalAnatomy1.AVH;
            fa.Brain = nt.FetalAnatomy1.Brain;
            fa.Extremities = nt.FetalAnatomy1.Extremities;
            fa.Genetilia = nt.FetalAnatomy1.Genetilia;
            fa.Glt = nt.FetalAnatomy1.Glt;
            fa.PVH = nt.FetalAnatomy1.PVH;
            fa.Thorax = nt.FetalAnatomy1.Thorax;
            fa.UrinTract = nt.FetalAnatomy1.UrinTract;
            asinfo.FetalAntomy = XmlStringListSerializer.ToXmlString<FetalAnatomy>(fa);

            ca.CervicalComment = nt.Cervicalassesment.CervicalComment;
            ca.Cervixlength = nt.Cervicalassesment.Cervixlength;
            ca.Funnelling = nt.Cervicalassesment.Funnelling;
            asinfo.CervicalAssessment = XmlStringListSerializer.ToXmlString<Cervicalassessment>(ca);

            mh.Age = nt.MaternalHistory1.Age;
            mh.BloodGroup = nt.MaternalHistory1.BloodGroup;
            mh.BmI = nt.MaternalHistory1.BmI;
            mh.Height = nt.MaternalHistory1.Height;
            mh.LastPeriod = nt.MaternalHistory1.LastPeriod;
            mh.Weight = nt.MaternalHistory1.Weight;
            asinfo.MaternalHistory = XmlStringListSerializer.ToXmlString<MaternalHistory>(mh);
            EDitUltraScan ediScanObj = new EDitUltraScan();
            string i = ediScanObj.UpdateAnomalyScan(asinfo);



            if (i == "1020")
            {
                Vitals _vitals = new Vitals();
                _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
                _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
                _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
                _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        public ActionResult AnomalyScanview(string anomalyid)
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
            AnomalyScan asinfo = new AnomalyScan();
            AnomalyScan asin = new AnomalyScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();
            Chromosomalmarkers csm = new Chromosomalmarkers();
            UterineDoppler ud = new UterineDoppler();
            FetalAnatomy fa = new FetalAnatomy();
            MaternalStructures ms = new MaternalStructures();
            MaternalHistory mh = new MaternalHistory();

            asinfo = GetUltraScan.GetAnomalyScanByUserIdandId(LoginUser.UserId, anomalyid);
            asin.Impression = asinfo.Impression;
            asin.PatientId = asinfo.PatientId;
            asin.anomalyId = asinfo.anomalyId;
            asin.Indications = asinfo.Indications;
            asin.PatientName = asinfo.PatientName;
            asin.Radiologist = asinfo.Radiologist;
            asin.Recomandations = asinfo.Recomandations;
            asin.ReffDoctor = asinfo.ReffDoctor;
            //asin.ScanDate = asinfo.ScanOnDate;
            DateTime time = Convert.ToDateTime(asinfo.ScanOnDate);
            asin.ScanDate1 = time.ToString("dd MMMM yyyy");
            //if (asinfo.ScanOnDate != null)
            //{
            //    asin.ScanDate1 = asinfo.ScanOnDate.Value.ToShortDateString();
            //}
            //else
            //{
            //    asin.ScanDate1 = string.Empty;
            //}
            asin.NTScanFinds = XmlStringListSerializer.Deserialize<FirstTrimeterUSSFindings>(asinfo.UsFinds);
            asin.Gestinat = XmlStringListSerializer.Deserialize<Gestation>(asinfo.Gestation);
            asin.EDDvalue = XmlStringListSerializer.Deserialize<EDD>(asinfo.EDD);
            if (asin.EDDvalue.ByDates != null)
            {
                DateTime time1 = Convert.ToDateTime(asin.EDDvalue.ByDates);
                //asin.EDDvalue.ByDates1 = asin.EDDvalue.ByDates.Value.ToShortDateString(); // sandeep added on 4-4-2014
                asin.EDDvalue.ByDates1 = time1.ToString("dd MMMM yyyy");

            }
            else
            {
                asin.EDDvalue.ByDates1 = string.Empty; // sandeep added on 4-4-2014
            }
            if (asin.EDDvalue.ByUltra != null)
            {
                DateTime time1 = Convert.ToDateTime(asin.EDDvalue.ByUltra);
                // asin.EDDvalue.ByUltra1 = asin.EDDvalue.ByUltra.Value.ToShortDateString(); // sandeep added on 4-4-2014
                asin.EDDvalue.ByUltra1 = time1.ToString("dd MMMM yyyy");
            }
            else
            {
                asin.EDDvalue.ByUltra1 = string.Empty; // sandeep added on 4-4-2014
            }
            asin.FetalAnatomy1 = XmlStringListSerializer.Deserialize<FetalAnatomy>(asinfo.FetalAntomy);
            asin.Cervicalassesment = XmlStringListSerializer.Deserialize<Cervicalassessment>(asinfo.CervicalAssessment);
            asin.MaternalHistory1 = XmlStringListSerializer.Deserialize<MaternalHistory>(asinfo.MaternalHistory);
            asin.UterineDoppler1 = XmlStringListSerializer.Deserialize<UterineDoppler>(asinfo.UterineDoppler);


            return PartialView(asin);
        }

        [HttpPost]
        public ActionResult SaveGrowthScan(GrowthScan nt)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            GrowthScan gsinfo = new GrowthScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();

            umbillicalartery ud = new umbillicalartery();
            FetalAnatomy fa = new FetalAnatomy();
            Ductusvenosus dv = new Ductusvenosus();
            MaternalHistory mc = new MaternalHistory();
            middlecerbleartery mb = new middlecerbleartery();

            FirstTrimeterUSSFindings ff = new FirstTrimeterUSSFindings();
            gsinfo.GrowthScanId = nt.GrowthScanId;
            gsinfo.Impression = nt.Impression;
            gsinfo.Indication = nt.Indication;
            gsinfo.PatientId = nt.PatientId;
            gsinfo.PatientName = nt.PatientName;
            gsinfo.Radiologist = nt.Radiologist;
            gsinfo.Recomandations = nt.Recomandations;
            gsinfo.ReffDoctor = nt.ReffDoctor;
            gsinfo.ScanOnDate = nt.ScanOnDate;
            gsinfo.UserId = LoginUser.UserId;

            ed.ByDates = nt.EDDvalue.ByDates;
            ed.ByUltra = nt.EDDvalue.ByUltra;
            gsinfo.EDD = XmlStringListSerializer.ToXmlString<EDD>(ed);
            ges.ByDates = nt.Gestinat.ByDates;
            ges.ByUltra = nt.Gestinat.ByUltra;
            gsinfo.Gestation = XmlStringListSerializer.ToXmlString<Gestation>(ges);
            ff.Amnioticfluid = nt.NTScanFinds.Amnioticfluid;
            ff.BPD = nt.NTScanFinds.BPD;
            ff.Efw = nt.NTScanFinds.Efw;
            ff.CRL = nt.NTScanFinds.CRL;
            ff.Ac = nt.NTScanFinds.Ac;
            ff.Fl = nt.NTScanFinds.Fl;
            ff.HC = nt.NTScanFinds.HC;
            ff.Movenents = nt.NTScanFinds.Movenents;
            ff.NumberofFetuses = nt.NTScanFinds.NumberofFetuses;
            ff.Placenta = nt.NTScanFinds.Placenta;
            ff.PlacentaGrade = nt.NTScanFinds.PlacentaGrade;
            ff.Presentation = nt.NTScanFinds.Presentation;
            gsinfo.UsFinds = XmlStringListSerializer.ToXmlString<FirstTrimeterUSSFindings>(ff);


            ud.PI = nt.Umbillical.PI;
            ud.RI = nt.Umbillical.RI;
            ud.EDF = nt.Umbillical.EDF;
            gsinfo.Umbilicalartery = XmlStringListSerializer.ToXmlString<umbillicalartery>(ud);


            fa.Abdomen = nt.FetalAnatomy1.Abdomen;
            fa.Bladder = nt.FetalAnatomy1.Bladder;
            fa.Feet = nt.FetalAnatomy1.Feet;
            fa.Hands = nt.FetalAnatomy1.Hands;
            fa.Heart = nt.FetalAnatomy1.Heart;
            fa.Skull = nt.FetalAnatomy1.Skull;
            fa.Spine = nt.FetalAnatomy1.Spine;
            fa.Stomoch = nt.FetalAnatomy1.Stomoch;
            fa.AVH = nt.FetalAnatomy1.AVH;
            fa.Brain = nt.FetalAnatomy1.Brain;
            fa.Extremities = nt.FetalAnatomy1.Extremities;
            fa.Genetilia = nt.FetalAnatomy1.Genetilia;
            fa.Glt = nt.FetalAnatomy1.Glt;
            fa.PVH = nt.FetalAnatomy1.PVH;
            gsinfo.FetalAntomy = XmlStringListSerializer.ToXmlString<FetalAnatomy>(fa);

            dv.AWare = nt.Ductusven.AWare;
            dv.PIV = nt.Ductusven.PIV;
            gsinfo.DuctusVenosus = XmlStringListSerializer.ToXmlString<Ductusvenosus>(dv);

            mb.MPI = nt.Middlecele.MPI;
            mb.PSV = nt.Middlecele.PSV;
            gsinfo.Middlecerebralartery = XmlStringListSerializer.ToXmlString<middlecerbleartery>(mb);
            mc.Age = nt.MaternalHistory1.Age;
            mc.BloodGroup = nt.MaternalHistory1.BloodGroup;
            mc.BmI = nt.MaternalHistory1.BmI;
            mc.Height = nt.MaternalHistory1.Height;
            mc.LastPeriod = nt.MaternalHistory1.LastPeriod;
            mc.Weight = nt.MaternalHistory1.Weight;
            gsinfo.MaternalHistory = XmlStringListSerializer.ToXmlString<MaternalHistory>(mc);
            CreateUltraScans CreateUltraScans = new CreateUltraScans();
            string i = CreateUltraScans.InsertGrowthScanInfo(gsinfo);


            if (i == "1010")
            {
                Vitals _vitals = new Vitals();
                _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
                _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
                _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
                _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        [HttpPost]
        public ActionResult EditGrowthScan(GrowthScan nt)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            GrowthScan gsinfo = new GrowthScan();
            EDD ed = new EDD();
            Gestation ges = new Gestation();

            umbillicalartery ud = new umbillicalartery();
            FetalAnatomy fa = new FetalAnatomy();
            Ductusvenosus dv = new Ductusvenosus();
            MaternalHistory mc = new MaternalHistory();
            middlecerbleartery mb = new middlecerbleartery();

            FirstTrimeterUSSFindings ff = new FirstTrimeterUSSFindings();
            gsinfo.GrowthScanId = nt.GrowthScanId;
            gsinfo.Impression = nt.Impression;
            gsinfo.Indication = nt.Indication;
            gsinfo.PatientId = nt.PatientId;
            gsinfo.PatientName = nt.PatientName;
            gsinfo.Radiologist = nt.Radiologist;
            gsinfo.Recomandations = nt.Recomandations;
            gsinfo.ReffDoctor = nt.ReffDoctor;
            //  gsinfo.ScanOnDate = nt.ScanOnDate;
            gsinfo.ScanOnDate = Convert.ToDateTime(nt.ScanOnDate1); // sandeep added on 4-4-2014

            gsinfo.UserId = LoginUser.UserId;

            //ed.ByDates = nt.EDDvalue.ByDates;
            //ed.ByUltra = nt.EDDvalue.ByUltra;
            if (nt.EDDvalue.ByDates1 != null)
            {
                ed.ByDates = Convert.ToDateTime(nt.EDDvalue.ByDates1);
            }
            else
            {
                ed.ByDates = null;
            }
            if (nt.EDDvalue.ByUltra1 != null)
            {
                ed.ByUltra = Convert.ToDateTime(nt.EDDvalue.ByUltra1);
            }
            else
            {
                ed.ByUltra = null;
            }
            gsinfo.EDD = XmlStringListSerializer.ToXmlString<EDD>(ed);
            ges.ByDates = nt.Gestinat.ByDates;
            ges.ByUltra = nt.Gestinat.ByUltra;
            gsinfo.Gestation = XmlStringListSerializer.ToXmlString<Gestation>(ges);
            ff.Amnioticfluid = nt.NTScanFinds.Amnioticfluid;
            ff.BPD = nt.NTScanFinds.BPD;
            ff.Efw = nt.NTScanFinds.Efw;
            ff.CRL = nt.NTScanFinds.CRL;
            ff.Ac = nt.NTScanFinds.Ac;
            ff.Fl = nt.NTScanFinds.Fl;
            ff.HC = nt.NTScanFinds.HC;
            ff.Movenents = nt.NTScanFinds.Movenents;
            ff.NumberofFetuses = nt.NTScanFinds.NumberofFetuses;
            ff.Placenta = nt.NTScanFinds.Placenta;
            ff.PlacentaGrade = nt.NTScanFinds.PlacentaGrade;
            ff.Presentation = nt.NTScanFinds.Presentation;
            gsinfo.UsFinds = XmlStringListSerializer.ToXmlString<FirstTrimeterUSSFindings>(ff);


            ud.PI = nt.Umbillical.PI;
            ud.RI = nt.Umbillical.RI;
            ud.EDF = nt.Umbillical.EDF;
            gsinfo.Umbilicalartery = XmlStringListSerializer.ToXmlString<umbillicalartery>(ud);


            fa.Abdomen = nt.FetalAnatomy1.Abdomen;
            fa.Bladder = nt.FetalAnatomy1.Bladder;
            fa.Feet = nt.FetalAnatomy1.Feet;
            fa.Hands = nt.FetalAnatomy1.Hands;
            fa.Heart = nt.FetalAnatomy1.Heart;
            fa.Skull = nt.FetalAnatomy1.Skull;
            fa.Spine = nt.FetalAnatomy1.Spine;
            fa.Stomoch = nt.FetalAnatomy1.Stomoch;
            fa.AVH = nt.FetalAnatomy1.AVH;
            fa.Brain = nt.FetalAnatomy1.Brain;
            fa.Extremities = nt.FetalAnatomy1.Extremities;
            fa.Genetilia = nt.FetalAnatomy1.Genetilia;
            fa.Glt = nt.FetalAnatomy1.Glt;
            fa.PVH = nt.FetalAnatomy1.PVH;
            gsinfo.FetalAntomy = XmlStringListSerializer.ToXmlString<FetalAnatomy>(fa);

            dv.AWare = nt.Ductusven.AWare;
            dv.PIV = nt.Ductusven.PIV;
            gsinfo.DuctusVenosus = XmlStringListSerializer.ToXmlString<Ductusvenosus>(dv);

            mb.MPI = nt.Middlecele.MPI;
            mb.PSV = nt.Middlecele.PSV;
            gsinfo.Middlecerebralartery = XmlStringListSerializer.ToXmlString<middlecerbleartery>(mb);
            mc.Age = nt.MaternalHistory1.Age;
            mc.BloodGroup = nt.MaternalHistory1.BloodGroup;
            mc.BmI = nt.MaternalHistory1.BmI;
            mc.Height = nt.MaternalHistory1.Height;
            mc.LastPeriod = nt.MaternalHistory1.LastPeriod;
            mc.Weight = nt.MaternalHistory1.Weight;
            gsinfo.MaternalHistory = XmlStringListSerializer.ToXmlString<MaternalHistory>(mc);
            EDitUltraScan ediScanObj = new EDitUltraScan();
            string i = ediScanObj.UpdateGrowthScan(gsinfo);

            if (i == "1020")
            {
                Vitals _vitals = new Vitals();
                _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
                _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
                _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
                _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        public ActionResult GrowthScanview(string growthid)
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
            GrowthScan asinfo = new GrowthScan();
            GrowthScan gsin = new GrowthScan();


            asinfo = GetUltraScan.GetGrowthScanByUserIdandId(LoginUser.UserId, growthid);
            gsin.PatientId = asinfo.PatientId;
            gsin.Impression = asinfo.Impression;
            gsin.GrowthScanId = asinfo.GrowthScanId;
            gsin.Indication = asinfo.Indication;
            gsin.PatientName = asinfo.PatientName;
            gsin.Radiologist = asinfo.Radiologist;
            gsin.Recomandations = asinfo.Recomandations;
            gsin.ReffDoctor = asinfo.ReffDoctor;
            //gsin.ScanOnDate = asinfo.ScanOnDate;
            DateTime time = Convert.ToDateTime(asinfo.ScanOnDate);
            gsin.ScanOnDate1 = time.ToString("dd MMMM yyyy");
            //if (asinfo.ScanOnDate != null)
            //{
            //    gsin.ScanOnDate1 = asinfo.ScanOnDate.Value.ToShortDateString();
            //}
            //else
            //{
            //    gsin.ScanOnDate1 = string.Empty;
            //}
            gsin.NTScanFinds = XmlStringListSerializer.Deserialize<FirstTrimeterUSSFindings>(asinfo.UsFinds);
            gsin.Gestinat = XmlStringListSerializer.Deserialize<Gestation>(asinfo.Gestation);
            gsin.FetalAnatomy1 = XmlStringListSerializer.Deserialize<FetalAnatomy>(asinfo.FetalAntomy);
            gsin.Umbillical = XmlStringListSerializer.Deserialize<umbillicalartery>(asinfo.Umbilicalartery);
            gsin.EDDvalue = XmlStringListSerializer.Deserialize<EDD>(asinfo.EDD);
            if (gsin.EDDvalue.ByDates != null)
            {
                DateTime time1 = Convert.ToDateTime(gsin.EDDvalue.ByDates);
                //gsin.EDDvalue.ByDates1 = gsin.EDDvalue.ByDates.Value.ToShortDateString(); // sandeep added on 4-4-2014
                gsin.EDDvalue.ByDates1 = time1.ToString("dd MMMM yyyy");
            }
            else
            {
                gsin.EDDvalue.ByDates1 = string.Empty; // sandeep added on 4-4-2014
            }
            if (gsin.EDDvalue.ByUltra != null)
            {
                DateTime time1 = Convert.ToDateTime(gsin.EDDvalue.ByUltra);
                // gsin.EDDvalue.ByUltra1 = gsin.EDDvalue.ByUltra.Value.ToShortDateString(); // sandeep added on 4-4-2014
                gsin.EDDvalue.ByUltra1 = time1.ToString("dd MMMM yyyy");
            }
            else
            {
                gsin.EDDvalue.ByUltra1 = string.Empty; // sandeep added on 4-4-2014
            }
            gsin.MaternalHistory1 = XmlStringListSerializer.Deserialize<MaternalHistory>(asinfo.MaternalHistory);
            gsin.Middlecele = XmlStringListSerializer.Deserialize<middlecerbleartery>(asinfo.Middlecerebralartery);
            gsin.Ductusven = XmlStringListSerializer.Deserialize<Ductusvenosus>(asinfo.DuctusVenosus);
            return PartialView(gsin);
        }

        /* venkateswari */
        public ActionResult CBPView()
        {
            return PartialView();
        }
        public ActionResult AddComplUrine()
        {
            return PartialView();
        }
        public ActionResult TestAdd()
        {
            return PartialView("SelectTest");
        }
        public ActionResult GetBloodTests()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            IList<BloodPicture> bloodtests = GetTests.GetAllBloodPicturesByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList(); ;
            return PartialView(bloodtests);
        }

        public ActionResult GetUrineTests()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            IList<UrinecompPicture> urinetests = GetTests.GetAllUrinecompPictureByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList(); ;
            return PartialView(urinetests);
        }

        public ActionResult GetSeerumTests()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            IList<SerumTest> urinetests = GetTests.GetAllSerumTestByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList(); ;
            return PartialView(urinetests);
        }

        public ActionResult EarlyScans()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            IList<EarlyScan> lstEarlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList(); ;
            return PartialView(lstEarlyScan);
        }
        public ActionResult NtScans()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            IList<NTScan> lstNTScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList(); ;
            return PartialView(lstNTScan);

        }
        public ActionResult AnomalyScans()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            IList<AnomalyScan> lstAnomalyScan = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList(); ;
            return PartialView(lstAnomalyScan);
        }
        public ActionResult GrowthScans()
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

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            IList<GrowthScan> lstGrowthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId).OrderByDescending(x => x.CreatedOn).ToList(); ;
            return PartialView(lstGrowthScan);

        }

        [HttpPost]
        public ActionResult SaveComplUrine(UrinecompPicture up)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }

            up.UserId = LoginUser.UserId;
            up.UrinePicture = XmlStringListSerializer.ToXmlString<UrienePicture>(up.urienepicture1);

            up.UrieneCulture = XmlStringListSerializer.ToXmlString<UrieneCulture>(up.urineculture1);

            string i = CreateTests.InsertUrienePicture(up);

            if (i == "1010")
            {

                return PartialView("testsnew");

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }



        }
        public ActionResult SaveBloodPicture(BloodPicture bloodpics)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            BloodPicture esinfo = new BloodPicture();
            Cbp ed = new Cbp();
            Antihiv ah = new Antihiv();
            Vdrl vd = new Vdrl();
            Hepatitisb hb = new Hepatitisb();
            Rbs rb = new Rbs();


            // esinfo.EarlyScanId = es.EarlyScanId;

            esinfo.PatientName = bloodpics.PatientName;
            esinfo.TestOnDate = bloodpics.TestOnDate;
            esinfo.ReffDoctor = bloodpics.ReffDoctor;

            esinfo.UserId = LoginUser.UserId;

            ed.Bands = bloodpics.cbp1.Bands;
            ed.BloodGroup = bloodpics.cbp1.BloodGroup;
            ed.Eosinophils = bloodpics.cbp1.Eosinophils;
            ed.Hemoglobin = bloodpics.cbp1.Hemoglobin;
            ed.Lymphocytes = bloodpics.cbp1.Lymphocytes;
            ed.MCH = bloodpics.cbp1.MCH;
            ed.MCHC = bloodpics.cbp1.MCHC;
            ed.MCV = bloodpics.cbp1.MCV;
            ed.Monocytes = bloodpics.cbp1.Monocytes;
            ed.Neutrophils = bloodpics.cbp1.Neutrophils;
            ed.PCV = bloodpics.cbp1.PCV;
            ed.PlateletCount = bloodpics.cbp1.PlateletCount;
            ed.RDW = bloodpics.cbp1.RDW;
            ed.RedBloodCells = bloodpics.cbp1.RedBloodCells;
            ed.Rhtype = bloodpics.cbp1.Rhtype;
            ed.WbcCount = bloodpics.cbp1.WbcCount;
            ed.Patholgist = bloodpics.cbp1.Patholgist;

            esinfo.Cbp = XmlStringListSerializer.ToXmlString<Cbp>(ed);
            ah.antiresult = bloodpics.Antihiv1.antiresult;
            ah.Methodology = bloodpics.Antihiv1.Methodology;
            ah.Microbiologist = bloodpics.Antihiv1.Microbiologist;
            esinfo.Antihiv = XmlStringListSerializer.ToXmlString<Antihiv>(ah);

            vd.Reportresult = bloodpics.Vdrl1.Reportresult;
            vd.Methodology = bloodpics.Vdrl1.Methodology;
            vd.Microbiologist = bloodpics.Vdrl1.Microbiologist;
            esinfo.Vdrl = XmlStringListSerializer.ToXmlString<Vdrl>(vd);

            hb.Result = bloodpics.Hepatitisb.Result;
            hb.Methodlogy = bloodpics.Hepatitisb.Methodlogy;
            hb.Microbiologist = bloodpics.Hepatitisb.Microbiologist;
            esinfo.Hepatitiesb = XmlStringListSerializer.ToXmlString<Hepatitisb>(hb);

            rb.Rbsresult = bloodpics.rbs1.Rbsresult;
            rb.patholgist = bloodpics.rbs1.patholgist;

            esinfo.Rbs = XmlStringListSerializer.ToXmlString<Rbs>(rb);




            string i = CreateTests.InsertBloodPicture(esinfo);


            if (i == "1010")
            {
                Vitals _vitals = new Vitals();
                _vitals.LstErlyScan = EarlyScanServicecalls.GetAllEarlyScansByUserId(LoginUser.UserId);
                _vitals.LstAnlyScans = AnomalyScanServiceCalls.GetAllAlyScansByUserId(LoginUser.UserId);
                _vitals.LstNtScan = NTScanServiceCalls.GetAllNTScansByUserId(LoginUser.UserId);
                _vitals.LstGrthScan = GrowthScanServiceCalls.GetAllGrowthScansByUserId(LoginUser.UserId);
                return PartialView("_uST", _vitals);

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        public ActionResult BloodTestView(string bdid)
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


            BloodPicture esinfo = GetTests.GetBloodPictureByUserIdandId(LoginUser.UserId, bdid);

            esinfo.Antihiv1 = XmlStringListSerializer.Deserialize<Antihiv>(esinfo.Antihiv);
            esinfo.cbp1 = XmlStringListSerializer.Deserialize<Cbp>(esinfo.Cbp);
            esinfo.Hepatitisb = XmlStringListSerializer.Deserialize<Hepatitisb>(esinfo.Hepatitiesb);
            esinfo.rbs1 = XmlStringListSerializer.Deserialize<Rbs>(esinfo.Rbs);
            esinfo.Vdrl1 = XmlStringListSerializer.Deserialize<Vdrl>(esinfo.Vdrl);
            // esinfo.TestOnDate1 = esinfo.TestOnDate.Value.ToShortDateString(); // sandeep added on 4-4-2014
            DateTime time = Convert.ToDateTime(esinfo.TestOnDate);
            esinfo.TestOnDate1 = time.ToString("dd MMMM yyyy");
            return PartialView(esinfo);
        }

        public ActionResult UrienePictureView(string urid)
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


            UrinecompPicture esinfo = GetTests.GetUrinecompPictureByUserIdandId(LoginUser.UserId, urid);

            esinfo.urineculture1 = XmlStringListSerializer.Deserialize<UrieneCulture>(esinfo.UrieneCulture);
            esinfo.urienepicture1 = XmlStringListSerializer.Deserialize<UrienePicture>(esinfo.UrinePicture);
            //  esinfo.TestOnDate1 = esinfo.TestOnDate.Value.ToShortDateString(); // sandeep added on 4-4-2014
            DateTime time = Convert.ToDateTime(esinfo.TestOnDate);
            esinfo.TestOnDate1 = time.ToString("dd MMMM yyyy");

            return PartialView(esinfo);
        }

        public ActionResult EditUrieneTest(UrinecompPicture up)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }

            up.UserId = LoginUser.UserId;
            up.UrinePicture = XmlStringListSerializer.ToXmlString<UrienePicture>(up.urienepicture1);

            up.UrieneCulture = XmlStringListSerializer.ToXmlString<UrieneCulture>(up.urineculture1);
            up.TestOnDate = Convert.ToDateTime(up.TestOnDate1); // sandeep added on 4-4-2014


            string i = EditTests.UpdateUrienePicture(up);

            if (i == "1020")
            {

                return PartialView("testsnew");

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        public ActionResult EditBloodTest(BloodPicture bloodpics)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            BloodPicture esinfo = new BloodPicture();
            Cbp ed = new Cbp();
            Antihiv ah = new Antihiv();
            Vdrl vd = new Vdrl();
            Hepatitisb hb = new Hepatitisb();
            Rbs rb = new Rbs();


            // esinfo.EarlyScanId = es.EarlyScanId;

            esinfo.PatientName = bloodpics.PatientName;
            // esinfo.TestOnDate = bloodpics.TestOnDate;
            esinfo.TestOnDate = Convert.ToDateTime(bloodpics.TestOnDate1); // sandeep added on 4-4-2014


            esinfo.ReffDoctor = bloodpics.ReffDoctor;
            esinfo.BloodPictureId = bloodpics.BloodPictureId;

            esinfo.UserId = LoginUser.UserId;

            ed.Bands = bloodpics.cbp1.Bands;
            ed.BloodGroup = bloodpics.cbp1.BloodGroup;
            ed.Eosinophils = bloodpics.cbp1.Eosinophils;
            ed.Hemoglobin = bloodpics.cbp1.Hemoglobin;
            ed.Lymphocytes = bloodpics.cbp1.Lymphocytes;
            ed.MCH = bloodpics.cbp1.MCH;
            ed.MCHC = bloodpics.cbp1.MCHC;
            ed.MCV = bloodpics.cbp1.MCV;
            ed.Monocytes = bloodpics.cbp1.Monocytes;
            ed.Neutrophils = bloodpics.cbp1.Neutrophils;
            ed.PCV = bloodpics.cbp1.PCV;
            ed.PlateletCount = bloodpics.cbp1.PlateletCount;
            ed.RDW = bloodpics.cbp1.RDW;
            ed.RedBloodCells = bloodpics.cbp1.RedBloodCells;
            ed.Rhtype = bloodpics.cbp1.Rhtype;
            ed.WbcCount = bloodpics.cbp1.WbcCount;
            ed.Patholgist = bloodpics.cbp1.Patholgist;
            esinfo.Cbp = XmlStringListSerializer.ToXmlString<Cbp>(ed);
            ah.antiresult = bloodpics.Antihiv1.antiresult;
            ah.Methodology = bloodpics.Antihiv1.Methodology;
            ah.Microbiologist = bloodpics.Antihiv1.Microbiologist;
            esinfo.Antihiv = XmlStringListSerializer.ToXmlString<Antihiv>(ah);

            vd.Reportresult = bloodpics.Vdrl1.Reportresult;
            vd.Methodology = bloodpics.Vdrl1.Methodology;
            vd.Microbiologist = bloodpics.Vdrl1.Microbiologist;
            esinfo.Vdrl = XmlStringListSerializer.ToXmlString<Vdrl>(vd);

            hb.Result = bloodpics.Hepatitisb.Result;
            hb.Methodlogy = bloodpics.Hepatitisb.Methodlogy;
            hb.Microbiologist = bloodpics.Hepatitisb.Microbiologist;
            esinfo.Hepatitiesb = XmlStringListSerializer.ToXmlString<Hepatitisb>(hb);

            rb.Rbsresult = bloodpics.rbs1.Rbsresult;
            rb.patholgist = bloodpics.rbs1.patholgist;

            esinfo.Rbs = XmlStringListSerializer.ToXmlString<Rbs>(rb);
            string i = EditTests.UpdateBloodPicture(esinfo);

            if (i == "1020")
            {

                return PartialView("testsnew");

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }

        }

        public ActionResult SerumTest()
        {
            return PartialView();
        }

        public ActionResult SaveSerumTest(SerumTest st)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }

            st.UserId = LoginUser.UserId;


            string i = CreateTests.InsertSerumTest(st
                );

            if (i == "1010")
            {

                return PartialView("testsnew");

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        public ActionResult SerumTestView(string srid)
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


            SerumTest esinfo = GetTests.GetSerumTestByUserIdandId(LoginUser.UserId, srid);
            //esinfo.TestOnDate1 = esinfo.TestOnDate.Value.ToShortDateString(); // sandeep added on 4-4-201

            DateTime time = Convert.ToDateTime(esinfo.TestOnDate);
            esinfo.TestOnDate1 = time.ToString("dd MMMM yyyy");


            return PartialView(esinfo);

        }

        public ActionResult EditSerumTest(SerumTest st)
        {
            JsonResult ResultData = new JsonResult();
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }

            st.UserId = LoginUser.UserId;
            st.TestOnDate = Convert.ToDateTime(st.TestOnDate1); // sandeep added on 4-4-2014
            string i = EditTests.UpdateSerumTest(st);

            if (i == "1020")
            {

                return PartialView("testsnew");

            }
            else
            {
                ResultData.Data = "Fail";
                return ResultData;
            }
        }

        public ActionResult WeekUpload()
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

            PostComments _pstcmnts = new PostComments();
            DateTime startDate = (DateTime)LoginUser.StartDate;
            DateTime CurrentDate = DateTime.Now.Date;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            int week = currentDay / 7;
            int day = currentDay % 7;
            if (day > 0)
            {

                week += 1;
            }
            _pstcmnts.CurrentWeek = week;
            _pstcmnts.CurrentDay = currentDay;
            ViewBag.CurrentWeek = week;
            ViewBag.startdate = startDate;


            // IList<PostComments> pstcmnts = GetPostComments.GetPostcommentsByUserIdFilterPath(LoginUser.UserId);
            // IList<string> path = new List<string>();
            //   string p = null;

            return PartialView();
        }


        [HttpPost]
        public ActionResult ImagePostByWeek(AlbumsUpload content, HttpPostedFileBase file)
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

            PostComments postcomments = null;
            if (TempData["postcomments"] != null)
            {
                postcomments = (PostComments)TempData["postcomments"];
            }
            else
            {
                postcomments = new PostComments();
            }

            Comments comment = new Comments();

            postcomments.UserId = LoginUser.UserId;
            string pic;
            string path = string.Empty;
            string filename = string.Empty;
            if (file != null)
            {
                string strDate = DateTime.Now.ToString("ddMMyyyyhhmmss");
                string Imagename = strDate;
                pic = System.IO.Path.GetFileName(file.FileName);
                filename = Imagename + pic;
                path = System.IO.Path.Combine(
                Server.MapPath("~/CommentImages"), filename);

                file.SaveAs(path);


            }
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime cdate = startDate.AddDays(Convert.ToInt32(content.CurrentDay) - 1);

            DateTime CurrentDate = cdate;

            int currentDay = content.CurrentDay;
            int week = content.CurrentWeek;
            //int day = currentDay % 7;

            //if (day > 0)
            //{
            //    week += 1;
            //}

            postcomments.Week = week;
            postcomments.CurrentDay = currentDay;
            postcomments.CurrentDate = CurrentDate;
            postcomments.CurrentWeek = week;
            //postcomments.weekcurrentday = day;
            //postcomments.weekday = day;

            comment.Comment = "This is image u post from albums";
            //  comment.Comment = content.PostComment;
            if (file != null)
            {
                comment.path = "../CommentImages/" + filename;
            }
            else
            {
                comment.path = path;
            }


            comment.Postdate = Convert.ToString(System.DateTime.Now);
            postcomments.UserId = LoginUser.UserId;
            postcomments.PostComment = content.PostComment;
            postcomments.Comment = comment;

            CreatePostComment CreatePostComment = new CreatePostComment();

            string i = CreatePostComment.InsertComment(postcomments);
            //if (!string.IsNullOrEmpty(content.accesstoken))
            //{
            //    FacebookClient fb = new FacebookClient(content.accesstoken);
            //    if (!string.IsNullOrWhiteSpace(comment.path) && !string.IsNullOrEmpty(comment.path))
            //    {
            //        string imgpath = @"C:/inetpub/wwwroot/BumpDocs/CommentImages/" + filename;
            //        var imgstream = System.IO.File.OpenRead(imgpath);
            //        dynamic res = fb.Post("/me/photos", new
            //        {

            //            message = content.PostComment,
            //            file = new FacebookMediaStream
            //            {
            //                ContentType = file.ContentType,
            //                FileName = Path.GetFileName(file.FileName)

            //            }.SetValue(imgstream)

            //        });
            //    }
            //    else
            //    {

            //        dynamic parameters = new ExpandoObject();
            //        parameters.message = content.PostComment;
            //        parameters.name = "BumpDocs";
            //        parameters.link = "https://www.bumpdocs.com";
            //        parameters.picture = "https://107.20.220.222/BumpDocs/Images/small.png";
            //        parameters.caption = "BumpDocs 'Wisdom to deliver a healthy baby'";
            //        parameters.description = "";
            //        dynamic postresult = fb.Post("me/feed", parameters);
            //    }
            //}

            //  PostComments postcomment = GetPostComments.GetPostcommentsByDayandUserId(postcomments.CurrentDay.ToString(), postcomments.UserId);
            //string encryptedComment1 = postcomment.PostComment;

            //List<Comments> tempcomments = null;
            //if (postcomment.PostComment != null)
            //{
            //    tempcomments = XmlStringListSerializer.Deserialize<List<Comments>>(CryptorEngine.Decrypt(encryptedComment1, true));
            //    postcomments.lstComments = tempcomments;
            //}
            //else
            //{
            //    postcomments.lstComments = tempcomments;
            //}

            //   postcomments.lstComments = postcomment.lstComments;

            postcomments.Week = week;
            postcomments.CurrentDay = currentDay;
            ViewBag.week = postcomments.Week;
            ViewBag.Currentday = postcomments.CurrentDay;
            //ViewBag.weekcurrentday = day;
            //ViewBag.weekday = day;
            //return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/TablePosts.cshtml"), postcomments);
            return RedirectToAction("GetWeeks", "Medwall");

        }

        public ActionResult DayUpload(int Id)
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

            ViewBag.Week = Id;
            PostComments _pstcmnts = new PostComments();

            DateTime startDate = (DateTime)LoginUser.StartDate;
            ViewBag.startdate = startDate;
            DateTime CurrentDate = DateTime.Now.Date;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            int week = currentDay / 7;
            int day = currentDay % 7;
            if (day > 0)
            {

                week += 1;
            }
            _pstcmnts.CurrentWeek = week;
            _pstcmnts.CurrentDay = currentDay;

            ViewBag.Day = Id;
            if (Id == 1)
            {
                ViewBag.Days1 = 1;
                ViewBag.Days2 = 2;
                ViewBag.Days3 = 3;
                ViewBag.Days4 = 4;
                ViewBag.Days5 = 5;
                ViewBag.Days6 = 6;
                ViewBag.Days7 = 7;
                ViewBag.lastday = 7;
            }
            else
            {
                ViewBag.Days1 = 1 + (7 * (Id - 1));
                ViewBag.Days2 = 2 + (7 * (Id - 1));
                ViewBag.Days3 = 3 + (7 * (Id - 1));
                ViewBag.Days4 = 4 + (7 * (Id - 1));
                ViewBag.Days5 = 5 + (7 * (Id - 1));
                ViewBag.Days6 = 6 + (7 * (Id - 1));
                ViewBag.Days7 = 7 + (7 * (Id - 1));
                if (Id == week && ViewBag.Days7 == currentDay)
                {
                    ViewBag.lastday = Id * 7;
                }
                else if (Id == week)
                {

                    ViewBag.lastday = ViewBag.Days1 + (day - 1);
                }
                else
                {
                    ViewBag.lastday = Id * 7;
                }
            }
            string tr = "false";
            TipsandResponse i = GetPostComments.GetTipsandResponseStatusCount(LoginUser.UserId, tr);

            ViewBag.statustip = i.TipCount;
            ViewBag.statusresponce = i.ResponseCount;

            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);

            ViewBag.ImageName = m.PInfo1.ImageName;
            ViewBag.currentday = currentDay;
            ViewBag.Currentweek = Id;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);


            //IList<PostComments> Postcom = GetPostComments.GetPostcommentsByweekandUserIdFilterImagePath(Id.ToString(), LoginUser.UserId);


            //if (Postcom != null)
            //{
            //    Postcom = Postcom.OrderBy(x => x.CurrentDay).Where(c => c.lstComments.Any(x => x.path != "")).ToList();
            //    var child = (from n in Postcom
            //                 where n.lstComments.Any(x => x.path == "")
            //                 select n into c
            //                 from e in c.lstComments
            //                 where e.path == ""
            //                 select e).ToList();
            //    Postcom.ToList().ForEach(x => x.lstComments.RemoveAll(k => child.Contains(k)));


            //}

            return PartialView();
        }
        [HttpPost]
        public ActionResult ImagePostByDay(AlbumsUpload content, HttpPostedFileBase file)
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

            PostComments postcomments = null;
            if (TempData["postcomments"] != null)
            {
                postcomments = (PostComments)TempData["postcomments"];
            }
            else
            {
                postcomments = new PostComments();
            }

            Comments comment = new Comments();

            postcomments.UserId = LoginUser.UserId;
            string pic;
            string path = string.Empty;
            string filename = string.Empty;
            if (file != null)
            {
                string strDate = DateTime.Now.ToString("ddMMyyyyhhmmss");
                string Imagename = strDate;
                pic = System.IO.Path.GetFileName(file.FileName);
                filename = Imagename + pic;
                path = System.IO.Path.Combine(
                Server.MapPath("~/CommentImages"), filename);

                file.SaveAs(path);


            }
            DateTime startDate = (DateTime)LoginUser.StartDate;

            DateTime cdate = startDate.AddDays(Convert.ToInt32(content.CurrentDay) - 1);

            DateTime CurrentDate = cdate;

            int currentDay = content.CurrentDay;
            int week = content.CurrentWeek;
            int day = currentDay % 7;

            //if (day > 0)
            //{
            //    week += 1;
            //}

            postcomments.Week = week;
            postcomments.CurrentDay = currentDay;
            postcomments.CurrentDate = CurrentDate;
            postcomments.CurrentWeek = week;
            //postcomments.weekcurrentday = day;
            //postcomments.weekday = day;
            comment.Comment = "This is image u post from albums";
            //  comment.Comment = content.PostComment;
            if (file != null)
            {
                comment.path = "../CommentImages/" + filename;
            }
            else
            {
                comment.path = path;
            }


            comment.Postdate = Convert.ToString(System.DateTime.Now);
            postcomments.UserId = LoginUser.UserId;
            postcomments.PostComment = content.PostComment;
            postcomments.Comment = comment;

            CreatePostComment CreatePostComment = new CreatePostComment();

            string i = CreatePostComment.InsertComment(postcomments);
            //if (!string.IsNullOrEmpty(content.accesstoken))
            //{
            //    FacebookClient fb = new FacebookClient(content.accesstoken);
            //    if (!string.IsNullOrWhiteSpace(comment.path) && !string.IsNullOrEmpty(comment.path))
            //    {
            //        string imgpath = @"C:/inetpub/wwwroot/BumpDocs/CommentImages/" + filename;
            //        var imgstream = System.IO.File.OpenRead(imgpath);
            //        dynamic res = fb.Post("/me/photos", new
            //        {

            //            message = content.PostComment,
            //            file = new FacebookMediaStream
            //            {
            //                ContentType = file.ContentType,
            //                FileName = Path.GetFileName(file.FileName)

            //            }.SetValue(imgstream)

            //        });
            //    }
            //    else
            //    {

            //        dynamic parameters = new ExpandoObject();
            //        parameters.message = content.PostComment;
            //        parameters.name = "BumpDocs";
            //        parameters.link = "https://www.bumpdocs.com";
            //        parameters.picture = "https://107.20.220.222/BumpDocs/Images/small.png";
            //        parameters.caption = "BumpDocs 'Wisdom to deliver a healthy baby'";
            //        parameters.description = "";
            //        dynamic postresult = fb.Post("me/feed", parameters);
            //    }
            //}

            //  PostComments postcomment = GetPostComments.GetPostcommentsByDayandUserId(postcomments.CurrentDay.ToString(), postcomments.UserId);
            //string encryptedComment1 = postcomment.PostComment;

            //List<Comments> tempcomments = null;
            //if (postcomment.PostComment != null)
            //{
            //    tempcomments = XmlStringListSerializer.Deserialize<List<Comments>>(CryptorEngine.Decrypt(encryptedComment1, true));
            //    postcomments.lstComments = tempcomments;
            //}
            //else
            //{
            //    postcomments.lstComments = tempcomments;
            //}

            //   postcomments.lstComments = postcomment.lstComments;

            postcomments.Week = week;
            postcomments.CurrentDay = currentDay;
            ViewBag.week = postcomments.Week;
            ViewBag.Currentday = postcomments.CurrentDay;
            //ViewBag.weekcurrentday = day;
            //ViewBag.weekday = day;
            //return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/iHealthUser/Views/Medwall/TablePosts.cshtml"), postcomments);
            // return RedirectToAction("GetDays", "Medwall");
            return RedirectToAction("GetDays", new { Id = postcomments.Week });

        }

        //public ActionResult WeekUpload()
        //{
        //    return ();
        //}

        /* venkateswari */

    }
}
