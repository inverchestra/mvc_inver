
using System;
using System.Globalization;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository;
using InnDocs.iHealth.Web.UI.Models;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Generator;
using PagedList;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get;
using Ionic.Zip;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    [SessionExpireFilter]
    public class HealthLogController : Controller
    {
        //
        // GET: /iHealthUser/HealthLog/
        public ActionResult MyLogs(int? page)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
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

            return View(GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType).ToPagedList(PageNumber, PageSize));
        }

        public ActionResult MedWall()
        {
            return View();
        }


        [HttpGet]
        public ActionResult MyAllLogs()
        {
            UserInformation LoginUser = Session["LoginUser"] as UserInformation;
            return PartialView("UpdatedTable", GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType).OrderByDescending(x => x.date));
        }

        public ActionResult GetSearchedLogs(int? page, string searchtext1)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
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
            SearchFields sFileds = new SearchFields();
            sFileds.Log = searchtext1;

            sFileds.UserId = LoginUser.UserId;
            ViewBag.FromSearch = searchtext1;
            return View("MyLogs", GetHealthLogs.GetAllSearchedLogs(sFileds).ToPagedList(PageNumber, PageSize));

        }
        public ActionResult CreateNewLog()
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
            UserHealthLog userhealthlog = new UserHealthLog();
            userhealthlog.lstLogs = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType);
            return PartialView("CreateNewLog", userhealthlog);
        }

        [HttpPost]
        public ActionResult CreateNewLog(UserHealthLog newLog)
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
                    // LoginUser = new UserInformation();
                    LoginUser = Session["LoginUser"] as UserInformation;
                }

                HealthLog log = UserLogToHealthLog.PopulateToHealthLog(newLog);
                if (Session["LoginUser"] != null)
                {
                    UserInformation CreatedByUser = null;
                    CreatedByUser = Session["LoginUser"] as UserInformation;
                    log.UserID = CreatedByUser.UserId;
                }
                log.OwnerId = LoginUser.UserId;
                log.domainId = LoginUser.DomainId;
                string i = CreateUserLog.InsertNewLog(log, LoginUser.GroupType);

                if (newLog.s3 != null)
                {
                    LogToLog ltl = null;
                    ltl = new LogToLog();
                    foreach (var v in newLog.s3)
                    {
                        ltl.LogId = i;
                        ltl.RelatedLogId = v;
                        string InsertCasetoLog = CreateLogToLog.InsertLogToLogInfo(ltl);
                    }
                }
                return RedirectToAction("MyLogs");
            }
            return PartialView("CreateNewLog");
        }
        public ActionResult ViewLog(string logId)
        {
            //UserHealthLog hl = GetHealthLogs.GetHealthLog(logId);
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

            UserHealthLog _h = new UserHealthLog();
            _h = GetHealthLogs.GetHealthLog(logId);
            _h.lstLogs = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType);
            _h.lstlogtolog = GetLogToLog.GetAllLogToLogByLogId(logId);
            _h.lstLogs.Where(a => a.logId == logId).ToList().ForEach(a => _h.lstLogs.Remove(a));//added by jagadeesh for duplication of logs
            _h.lstresultLogToLoglist = ResultLogList.GetResultLogList(_h.lstLogs, _h.lstlogtolog);
            //_cases.lstresultCaselist = ResultList.GetResulCaseList(_cases.lstCases, _cases.lstcasetocase);
            return PartialView("ViewLog", _h);
        }

        public ActionResult EditLog(string logId)
        {
            //UserHealthLog h = GetHealthLogs.GetHealthLog(logId);
            //UserHealthLog hl = GetHealthLogs.GetHealthLog(logId);
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
            UserHealthLog _h = new UserHealthLog();
            _h = GetHealthLogs.GetHealthLog(logId);
            _h.lstLogs = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType);
            _h.lstlogtolog = GetLogToLog.GetAllLogToLogByLogId(logId);
            _h.lstresultLogToLoglist = ResultLogList.GetResultLogList(_h.lstLogs, _h.lstlogtolog);
            return PartialView("EditLog", _h);
        }

        [HttpPost]
        public ActionResult EditLog(UserHealthLog uh)
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
                    // LoginUser = new UserInformation();
                    LoginUser = Session["LoginUser"] as UserInformation;
                }

                HealthLog log = UserLogToHealthLog.PopulateToHealthLog(uh);
                //log.ihealthuserID = LoginUser.UserId;
                if (Session["LoginUser"] != null)
                {
                    UserInformation CreatedByUser = null;
                    CreatedByUser = Session["LoginUser"] as UserInformation;
                    log.ModifiedBy = CreatedByUser.UserId;
                    log.MType = CreatedByUser.GroupType;
                }
                log.UserID = LoginUser.UserId;
                log.domainId = LoginUser.DomainId;
                //string i = EditUserLog.UpdateUserLog(log,LoginUser.GroupType).ToString();

                string successcode = EditUserLog.UpdateUserLog(log,LoginUser.GroupType,log.logId).ToString();
                if (successcode == "1020")
                {
                    if (uh.s3 != null)
                    {
                        LogToLog ltl = null;
                        ltl = new LogToLog();
                        foreach (var v in uh.s3)
                        {
                            ltl.LogId = uh.logId;
                            ltl.RelatedLogId = v;
                            string InsertCasetoLog = CreateLogToLog.InsertLogToLogInfo(ltl);

                        }
                    }
                }

                return RedirectToAction("MyLogs");
            }
            return PartialView("EditLog", uh);
        }



        public ActionResult AddSymptom(string id)
        {
            //UserHealthLog h = GetHealthLogs.GetHealthLog(id);
            return PartialView(new UserSymptom { LogId = id.ToString() });
        }

        [HttpPost]
        public ActionResult AddSymptom(UserSymptom symptom)
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
                symptom.DateTime = DateTime.Now;
                HealthLog hl = UserLogToHealthLog.PopulateToHealthLog(symptom);
                //hl.ihealthuserID = LoginUser.UserId;
                if (Session["LoginUser"] != null)
                {
                    UserInformation CreatedByUser = null;
                    CreatedByUser = Session["LoginUser"] as UserInformation;
                    hl.ModifiedBy = CreatedByUser.UserId;
                }
                hl.domainId = LoginUser.DomainId;
                int i = EditUserLog.UpdateUserLog(hl,LoginUser.GroupType,hl.logId);
                return RedirectToAction("MyLogs");
            }
            return PartialView(symptom);
        }


        public ActionResult ReturnList(string term)
        {
            System.Globalization.TextInfo searchtxt = new CultureInfo("en-US", false).TextInfo;
            List<string> items = System.IO.File
                .ReadAllLines(@"C:\InndocsiHealth\AutoCompleteReasons\ReasonList.txt")
                .ToList();
            if (term != null)
            {
                var sel_items = items.Where(x => x.StartsWith(searchtxt.ToTitleCase(term))).OrderBy(x => x).Select(x => x).Distinct();
                return Json(sel_items, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Null");
            }
        }

        public ActionResult DownloadFile(string LogID, string strRelatedlogs)
        {
            UserInformation LoginUser = new UserInformation();
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            UserHealthLog hl = GetHealthLogs.GetHealthLog(LogID);
            UserHealthLog h = new UserHealthLog();
            h.lstlogtolog = GetLogToLog.GetAllLogToLogByLogId(LogID);

            h.lstHealthlog = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType);

            h.lstresultLogToLoglist = ResultList.GetResultLogList(h.lstHealthlog, h.lstlogtolog);



            string s = Convert.ToDateTime(hl.CreatedOn).ToShortDateString();
            string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");


            //string retFileName = hl.logName + LoginUser.LoginName + strDate + ".pdf";
            //string zipFileName = hl.logName + LoginUser.LoginName + strDate + ".zip";

            //string tempDir = "C:\\InndocsiHealth\\PDFFiles\\temp\\" + LoginUser.LoginName + LoginUser.UserId;
            //string SendToDocInfo = tempDir + "\\" + hl.logName;//Sending this string to docinfo obj in docinfo.Name
            //string DestPdfFilePath = tempDir + "\\" + hl.logName + "\\" + retFileName;
            //string SourceDirectoryPath = tempDir;//@"C:\InndocsiHealth\PDFFiles\74";
            //string TargetZipFilePath = tempDir + hl.logName + LoginUser.LoginName + strDate + ".zip";//@"C:\InndocsiHealth\PDFFiles\" + objCase.CaseName + LoginUser.LoginName + strDate + ".zip";
            //ck modified
            string retFileName = hl.logName + LoginUser.UserId + strDate + ".pdf";
            string zipFileName = hl.logName + LoginUser.UserId + strDate + ".zip";
            //string tempDir = "C:\\InndocsiHealth\\PDFFiles\\temp\\" + LoginUser.LoginName + LoginUser.UserId;
            string tempDir = "C:\\InndocsiHealth\\PDFFiles\\temp\\" + LoginUser.EmailId.Substring(0, LoginUser.EmailId.IndexOf('@')) + LoginUser.UserId;
            //ck ended
            string SendToDocInfo = tempDir + "\\" + hl.logName;//Sending this string to docinfo obj in docinfo.Name
            string DestPdfFilePath = tempDir + "\\" + hl.logName + "\\" + retFileName;
            string SourceDirectoryPath = tempDir;//@"C:\InndocsiHealth\PDFFiles\74";
            string TargetZipFilePath = tempDir + hl.logName + LoginUser.EmailId.Substring(0, LoginUser.EmailId.IndexOf('@')) + strDate + ".zip";//@"C:\InndocsiHealth\PDFFiles\" + objCase.CaseName + LoginUser.LoginName + strDate + ".zip";


            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            if (!Directory.Exists(DestPdfFilePath))
            {
                Directory.CreateDirectory(SendToDocInfo);
            }



            License lic = new License();
            lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

            Pdf pdf1 = new Pdf();
            Section sec1 = pdf1.Sections.Add();


            sec1.PageInfo.PageWidth = PageSize.A3Width;
            sec1.PageInfo.PageHeight = PageSize.A3Height;

            MarginInfo marginInfo = new MarginInfo();
            marginInfo.Top = 72;
            marginInfo.Bottom = 72;
            marginInfo.Left = 90;
            marginInfo.Right = 90;

            sec1.PageInfo.Margin = marginInfo;


            Text pdfHeadText = new Text("Log");

            pdfHeadText.TextInfo.FontName = "Arial";
            pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfHeadText.TextInfo.FontSize = 16;
            pdfHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText);



            Table tab1 = new Table();
            sec1.Paragraphs.Add(tab1);
            tab1.ColumnWidths = "120 400";

            tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            tab1.Border = new BorderInfo((int)BorderSide.All, 1F);

            MarginInfo margin = new MarginInfo();
            margin.Top = 10;
            margin.Left = 10;
            margin.Right = 10;
            margin.Bottom = 10;


            tab1.DefaultCellPadding = margin;


            Aspose.Pdf.Generator.Row row1 = tab1.Rows.Add();
            row1.Cells.Add("Log name:");
            row1.Cells.Add(hl.logName);

            Aspose.Pdf.Generator.Row row2 = tab1.Rows.Add();
            row2.Cells.Add("Log description:");
            row2.Cells.Add(hl.logDescription);


            Aspose.Pdf.Generator.Row row3 = tab1.Rows.Add();
            row3.Cells.Add("First observed:");
            row3.Cells.Add(hl.date);

            Text pdfHeadText1 = new Text("Symptoms");

            pdfHeadText1.TextInfo.FontName = "Arial";
            pdfHeadText1.TextInfo.IsTrueTypeFontBold = true;

            pdfHeadText1.TextInfo.FontSize = 16;
            pdfHeadText1.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText1);


            Table tab2 = new Table();
            sec1.Paragraphs.Add(tab2);
            tab2.ColumnWidths = "120 400 ";

            MarginInfo margin1 = new MarginInfo();
            margin1.Top = 5;
            margin1.Left = 5;
            margin1.Right = 5;
            margin1.Bottom = 5;


            tab2.DefaultCellPadding = margin1;

            int i = 1;

            foreach (var symptom in hl.symptoms)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = true;


                Row srow5 = tab2.Rows.Add();
                srow5.Cells.Add("Symptom" + i, t);



                Row srow1 = tab2.Rows.Add();
                srow1.Cells.Add("Created on:");
                srow1.Cells.Add(symptom.DateTime.ToString());


                Row srow2 = tab2.Rows.Add();
                srow2.Cells.Add("Symptom name:");
                srow2.Cells.Add(symptom.Name);


                Row srow3 = tab2.Rows.Add();
                srow3.Cells.Add("Symptom description:");
                srow3.Cells.Add(symptom.Description);

                Row srow4 = tab2.Rows.Add();
                srow4.Cells.Add("Reason:");
                srow4.Cells.Add(symptom.Reasons);

                Row srow6 = tab2.Rows.Add();
                srow6.Cells.Add("Medicine:");
                srow6.Cells.Add(symptom.Medicines);
                i++;

                Aspose.Pdf.Generator.Row row4 = tab1.Rows.Add();
                row4.Cells.Add("Related logs:");
                string logtolog = string.Empty;
                if (h.lstresultLogToLoglist != null)
                {

                    foreach (var lstRelatedlogs in h.lstresultLogToLoglist)
                    {
                        if (lstRelatedlogs.IsChecked)
                        {
                            logtolog = logtolog + lstRelatedlogs.name + ", ";


                            if (strRelatedlogs == "YesRelatedLogs")
                            {
                                Relatedlogs(lstRelatedlogs.Id);
                            }
                        }

                    }
                }

                row4.Cells.Add(logtolog);
            }


            pdf1.Save(DestPdfFilePath);
            try
            {

                ZipFile zip = null;
                using (zip = new ZipFile())
                {
                    zip.AddDirectory(SourceDirectoryPath, System.IO.Path.GetFileName(SourceDirectoryPath));
                    zip.Save(TargetZipFilePath);
                    zip.Dispose();
                }
            }
            catch (System.Exception ex1)
            {
                throw ex1;
            }
            var bytes = System.IO.File.ReadAllBytes(TargetZipFilePath);
            System.IO.File.Delete(TargetZipFilePath);
            Directory.Delete(tempDir, true);

            return File(bytes, "application/x-zip-compressed", zipFileName);// DestPdfFilePath

        }





        private void Relatedlogs(string logId)
        {
            UserInformation LoginUser = new UserInformation();
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            UserHealthLog hl = GetHealthLogs.GetHealthLog(logId);
            string s = Convert.ToDateTime(hl.CreatedOn).ToShortDateString();
            string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
            string retFileName = hl.logName + LoginUser.LoginName + strDate + ".pdf";
            string zipFileName = hl.logName + LoginUser.LoginName + strDate + ".zip";

            string tempDir = "C:\\InndocsiHealth\\PDFFiles\\temp\\" + LoginUser.LoginName + LoginUser.UserId;
            string SendToDocInfo = tempDir + "\\" + hl.logName;//Sending this string to docinfo obj in docinfo.Name
            string DestPdfFilePath = tempDir + "\\" + hl.logName + "\\" + retFileName;
            string SourceDirectoryPath = tempDir;//@"C:\InndocsiHealth\PDFFiles\74";
            string TargetZipFilePath = tempDir + hl.logName + LoginUser.LoginName + strDate + ".zip";//@"C:\InndocsiHealth\PDFFiles\" + objCase.CaseName + LoginUser.LoginName + strDate + ".zip";
            //string[] files = Directory.GetFiles(tempDir);

            //foreach (string file in files)
            //{
            //    if (System.IO.File.Exists(file))
            //    {
            //        System.IO.File.Delete(file);
            //    }

            //}
            //Directory.Delete(tempDir, true);

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            if (!Directory.Exists(DestPdfFilePath))
            {
                Directory.CreateDirectory(SendToDocInfo);
            }

            License lic = new License();
            lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

            Pdf pdf1 = new Pdf();
            Section sec1 = pdf1.Sections.Add();


            sec1.PageInfo.PageWidth = Aspose.Pdf.Generator.PageSize.A3Width;
            sec1.PageInfo.PageHeight = Aspose.Pdf.Generator.PageSize.A3Height;

            MarginInfo marginInfo = new MarginInfo();
            marginInfo.Top = 72;
            marginInfo.Bottom = 72;
            marginInfo.Left = 90;
            marginInfo.Right = 90;

            sec1.PageInfo.Margin = marginInfo;


            Text pdfHeadText = new Text("Log");

            pdfHeadText.TextInfo.FontName = "Arial";
            pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfHeadText.TextInfo.FontSize = 16;
            pdfHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText);



            Table tab1 = new Table();
            sec1.Paragraphs.Add(tab1);
            tab1.ColumnWidths = "120 400";

            tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            tab1.Border = new BorderInfo((int)BorderSide.All, 1F);

            MarginInfo margin = new MarginInfo();
            margin.Top = 10;
            margin.Left = 10;
            margin.Right = 10;
            margin.Bottom = 10;


            tab1.DefaultCellPadding = margin;


            Aspose.Pdf.Generator.Row row1 = tab1.Rows.Add();
            row1.Cells.Add("Log name:");
            row1.Cells.Add(hl.logName);

            Aspose.Pdf.Generator.Row row2 = tab1.Rows.Add();
            row2.Cells.Add("Log description:");
            row2.Cells.Add(hl.logDescription);


            Aspose.Pdf.Generator.Row row3 = tab1.Rows.Add();
            row3.Cells.Add("First observed:");
            row3.Cells.Add(hl.date);

            Text pdfHeadText1 = new Text("Symptoms");

            pdfHeadText1.TextInfo.FontName = "Arial";
            pdfHeadText1.TextInfo.IsTrueTypeFontBold = true;

            pdfHeadText1.TextInfo.FontSize = 16;
            pdfHeadText1.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText1);


            Table tab2 = new Table();
            sec1.Paragraphs.Add(tab2);
            tab2.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo margin1 = new MarginInfo();
            margin1.Top = 5;
            margin1.Left = 5;
            margin1.Right = 5;
            margin1.Bottom = 5;


            tab2.DefaultCellPadding = margin1;

            int i = 1;

            foreach (var symptom in hl.symptoms)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = true;


                Row srow5 = tab2.Rows.Add();
                srow5.Cells.Add("Symptom" + i, t);



                Row srow1 = tab2.Rows.Add();
                srow1.Cells.Add("Created on:");
                srow1.Cells.Add(symptom.DateTime.ToString());


                Row srow2 = tab2.Rows.Add();
                srow2.Cells.Add("Symptom name:");
                srow2.Cells.Add(symptom.Name);


                Row srow3 = tab2.Rows.Add();
                srow3.Cells.Add("Symptom description:");
                srow3.Cells.Add(symptom.Description);

                Row srow4 = tab2.Rows.Add();
                srow4.Cells.Add("Reason:");
                srow4.Cells.Add(symptom.Reasons);

                Row srow6 = tab2.Rows.Add();
                srow6.Cells.Add("Medicine:");
                srow6.Cells.Add(symptom.Medicines);
                i++;
            }



            pdf1.Save(DestPdfFilePath);
            // return DestPdfFilePath;


        }




        //public ActionResult DownloadFile(string LogID)
        //{
        //    UserInformation LoginUser = new UserInformation();
        //    if (Session["CurrentUser"] != null)
        //    {
        //        LoginUser = Session["CurrentUser"] as UserInformation;
        //    }
        //    else
        //    {
        //        // LoginUser = new UserInformation();
        //        LoginUser = Session["LoginUser"] as UserInformation;
        //    }
        //    UserHealthLog hl = GetHealthLogs.GetHealthLog(LogID);

        //    string s = Convert.ToDateTime(hl.date).ToShortDateString();
        //    string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
        //    string retFileName = hl.logName + LoginUser.LoginName + strDate + ".pdf";

        //    string tempDir = "C:\\InndocsiHealth\\PDFFiles\\" + LoginUser.UserId;
        //    string DestPdfFilePath = tempDir + "\\" + retFileName;



        //    Directory.CreateDirectory(tempDir);


        //    License lic = new License();
        //    lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

        //    Pdf pdf1 = new Pdf();
        //    Section sec1 = pdf1.Sections.Add();


        //    sec1.PageInfo.PageWidth = PageSize.A3Width;
        //    sec1.PageInfo.PageHeight = PageSize.A3Height;

        //    MarginInfo marginInfo = new MarginInfo();
        //    marginInfo.Top = 72;
        //    marginInfo.Bottom = 72;
        //    marginInfo.Left = 90;
        //    marginInfo.Right = 90;

        //    sec1.PageInfo.Margin = marginInfo;


        //    Text pdfHeadText = new Text("Log");

        //    pdfHeadText.TextInfo.FontName = "Arial";
        //    pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
        //    pdfHeadText.TextInfo.FontSize = 16;
        //    pdfHeadText.TextInfo.LineSpacing = 5f;

        //    sec1.Paragraphs.Add(pdfHeadText);



        //    Table tab1 = new Table();
        //    sec1.Paragraphs.Add(tab1);
        //    tab1.ColumnWidths = "120 400";

        //    tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


        //    tab1.Border = new BorderInfo((int)BorderSide.All, 1F);

        //    MarginInfo margin = new MarginInfo();
        //    margin.Top = 10;
        //    margin.Left = 10;
        //    margin.Right = 10;
        //    margin.Bottom = 10;


        //    tab1.DefaultCellPadding = margin;


        //    Aspose.Pdf.Generator.Row row1 = tab1.Rows.Add();
        //    row1.Cells.Add("Log name:");
        //    row1.Cells.Add(hl.logName);

        //    Aspose.Pdf.Generator.Row row2 = tab1.Rows.Add();
        //    row2.Cells.Add("Log description:");
        //    row2.Cells.Add(hl.logDescription);


        //    Aspose.Pdf.Generator.Row row3 = tab1.Rows.Add();
        //    row3.Cells.Add("First observed:");
        //    row3.Cells.Add(hl.date);

        //    Text pdfHeadText1 = new Text("Symptoms");

        //    pdfHeadText1.TextInfo.FontName = "Arial";
        //    pdfHeadText1.TextInfo.IsTrueTypeFontBold = true;

        //    pdfHeadText1.TextInfo.FontSize = 16;
        //    pdfHeadText1.TextInfo.LineSpacing = 5f;

        //    sec1.Paragraphs.Add(pdfHeadText1);


        //    Table tab2 = new Table();
        //    sec1.Paragraphs.Add(tab2);
        //    tab2.ColumnWidths = "120 400 ";

        //    MarginInfo margin1 = new MarginInfo();
        //    margin1.Top = 5;
        //    margin1.Left = 5;
        //    margin1.Right = 5;
        //    margin1.Bottom = 5;


        //    tab2.DefaultCellPadding = margin1;

        //    int i = 1;

        //    foreach (var symptom in hl.symptoms)
        //    {
        //        Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
        //        t.FontSize = 14;
        //        t.IsTrueTypeFontBold = true;


        //        Row srow5 = tab2.Rows.Add();
        //        srow5.Cells.Add("Symptom" + i, t);



        //        Row srow1 = tab2.Rows.Add();
        //        srow1.Cells.Add("Created on:");
        //        srow1.Cells.Add(symptom.DateTime.ToString());


        //        Row srow2 = tab2.Rows.Add();
        //        srow2.Cells.Add("Symptom name:");
        //        srow2.Cells.Add(symptom.Name);


        //        Row srow3 = tab2.Rows.Add();
        //        srow3.Cells.Add("Symptom description:");
        //        srow3.Cells.Add(symptom.Description);

        //        Row srow4 = tab2.Rows.Add();
        //        srow4.Cells.Add("Reason:");
        //        srow4.Cells.Add(symptom.Reasons);

        //        Row srow6 = tab2.Rows.Add();
        //        srow6.Cells.Add("Medicine:");
        //        srow6.Cells.Add(symptom.Medicines);
        //        i++;
        //    }



        //    pdf1.Save(DestPdfFilePath);


        //    return File(DestPdfFilePath, "application/pdf", retFileName);
        //}


        public ActionResult ViewLogToLog(string logId)
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
            //string type = "Hospital";
            UserHealthLog hl = GetHealthLogs.GetHealthLog(logId);
            return PartialView("ViewLogToLog", hl);
        }

        //Added by jagadeesh
        //private string Relatedlogs(string logId)
        //{
        //    UserInformation LoginUser = new UserInformation();
        //    if (Session["CurrentUser"] != null)
        //    {
        //        LoginUser = Session["CurrentUser"] as UserInformation;
        //    }
        //    else
        //    {
        //        // LoginUser = new UserInformation();
        //        LoginUser = Session["LoginUser"] as UserInformation;
        //    }
        //    UserHealthLog hl = GetHealthLogs.GetHealthLog(logId);

        //    string s = Convert.ToDateTime(hl.date).ToShortDateString();
        //    string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
        //    string retFileName = hl.logName + LoginUser.UserId + strDate + ".pdf";

        //    string tempDir = "C:\\InndocsiHealth\\PDFFiles\\" + LoginUser.UserId;
        //    string DestPdfFilePath = tempDir + "\\" + retFileName;

        //    //string[] files = Directory.GetFiles(tempDir);

        //    //foreach (string file in files)
        //    //{
        //    //    if (System.IO.File.Exists(file))
        //    //    {
        //    //        System.IO.File.Delete(file);
        //    //    }

        //    //}
        //    //Directory.Delete(tempDir, true);

        //    Directory.CreateDirectory(tempDir);


        //    License lic = new License();
        //    lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

        //    Pdf pdf1 = new Pdf();
        //    Section sec1 = pdf1.Sections.Add();


        //    sec1.PageInfo.PageWidth = Aspose.Pdf.Generator.PageSize.A3Width;
        //    sec1.PageInfo.PageHeight = Aspose.Pdf.Generator.PageSize.A3Height;

        //    MarginInfo marginInfo = new MarginInfo();
        //    marginInfo.Top = 72;
        //    marginInfo.Bottom = 72;
        //    marginInfo.Left = 90;
        //    marginInfo.Right = 90;

        //    sec1.PageInfo.Margin = marginInfo;


        //    Text pdfHeadText = new Text("Log");

        //    pdfHeadText.TextInfo.FontName = "Arial";
        //    pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
        //    pdfHeadText.TextInfo.FontSize = 16;
        //    pdfHeadText.TextInfo.LineSpacing = 5f;

        //    sec1.Paragraphs.Add(pdfHeadText);



        //    Table tab1 = new Table();
        //    sec1.Paragraphs.Add(tab1);
        //    tab1.ColumnWidths = "120 400";

        //    tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


        //    tab1.Border = new BorderInfo((int)BorderSide.All, 1F);

        //    MarginInfo margin = new MarginInfo();
        //    margin.Top = 10;
        //    margin.Left = 10;
        //    margin.Right = 10;
        //    margin.Bottom = 10;


        //    tab1.DefaultCellPadding = margin;


        //    Aspose.Pdf.Generator.Row row1 = tab1.Rows.Add();
        //    row1.Cells.Add("Log name:");
        //    row1.Cells.Add(hl.logName);

        //    Aspose.Pdf.Generator.Row row2 = tab1.Rows.Add();
        //    row2.Cells.Add("Log description:");
        //    row2.Cells.Add(hl.logDescription);


        //    Aspose.Pdf.Generator.Row row3 = tab1.Rows.Add();
        //    row3.Cells.Add("First observed:");
        //    row3.Cells.Add(hl.date);

        //    Text pdfHeadText1 = new Text("Symptoms");

        //    pdfHeadText1.TextInfo.FontName = "Arial";
        //    pdfHeadText1.TextInfo.IsTrueTypeFontBold = true;

        //    pdfHeadText1.TextInfo.FontSize = 16;
        //    pdfHeadText1.TextInfo.LineSpacing = 5f;

        //    sec1.Paragraphs.Add(pdfHeadText1);


        //    Table tab2 = new Table();
        //    sec1.Paragraphs.Add(tab2);
        //    tab2.ColumnWidths = "120 400 ";

        //    // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


        //    // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

        //    MarginInfo margin1 = new MarginInfo();
        //    margin1.Top = 5;
        //    margin1.Left = 5;
        //    margin1.Right = 5;
        //    margin1.Bottom = 5;


        //    tab2.DefaultCellPadding = margin1;

        //    int i = 1;

        //    foreach (var symptom in hl.symptoms)
        //    {
        //        Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
        //        t.FontSize = 14;
        //        t.IsTrueTypeFontBold = true;


        //        Row srow5 = tab2.Rows.Add();
        //        srow5.Cells.Add("Symptom" + i, t);



        //        Row srow1 = tab2.Rows.Add();
        //        srow1.Cells.Add("Created on:");
        //        srow1.Cells.Add(symptom.DateTime.ToString());


        //        Row srow2 = tab2.Rows.Add();
        //        srow2.Cells.Add("Symptom name:");
        //        srow2.Cells.Add(symptom.Name);


        //        Row srow3 = tab2.Rows.Add();
        //        srow3.Cells.Add("Symptom description:");
        //        srow3.Cells.Add(symptom.Description);

        //        Row srow4 = tab2.Rows.Add();
        //        srow4.Cells.Add("Reason:");
        //        srow4.Cells.Add(symptom.Reasons);

        //        Row srow6 = tab2.Rows.Add();
        //        srow6.Cells.Add("Medicine:");
        //        srow6.Cells.Add(symptom.Medicines);
        //        i++;
        //    }



        //    pdf1.Save(DestPdfFilePath);
        //    return DestPdfFilePath;


        //}
        ////end
        ////Added by jagadeesh
        public ActionResult PrintDownloadFile(string LogID)
        {
            UserInformation LoginUser = new UserInformation();
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            UserHealthLog hl = GetHealthLogs.GetHealthLog(LogID);

            string s = Convert.ToDateTime(hl.date).ToShortDateString();
            string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
            string retFileName = hl.logName + LoginUser.UserId + strDate + ".pdf";

            string tempDir = "C:\\InndocsiHealth\\PDFFiles\\" + LoginUser.UserId;
            string DestPdfFilePath = tempDir + "\\" + retFileName;

            //string[] files = Directory.GetFiles(tempDir);

            //foreach (string file in files)
            //{
            //    if (System.IO.File.Exists(file))
            //    {
            //        System.IO.File.Delete(file);
            //    }

            //}
            //Directory.Delete(tempDir, true);

            Directory.CreateDirectory(tempDir);


            License lic = new License();
            lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

            Pdf pdf1 = new Pdf();
            Section sec1 = pdf1.Sections.Add();


            sec1.PageInfo.PageWidth = PageSize.A3Width;
            sec1.PageInfo.PageHeight = PageSize.A3Height;

            MarginInfo marginInfo = new MarginInfo();
            marginInfo.Top = 72;
            marginInfo.Bottom = 72;
            marginInfo.Left = 90;
            marginInfo.Right = 90;

            sec1.PageInfo.Margin = marginInfo;


            Text pdfHeadText = new Text("Log");

            pdfHeadText.TextInfo.FontName = "Arial";
            pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfHeadText.TextInfo.FontSize = 16;
            pdfHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText);



            Table tab1 = new Table();
            sec1.Paragraphs.Add(tab1);
            tab1.ColumnWidths = "120 400";

            tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            tab1.Border = new BorderInfo((int)BorderSide.All, 1F);

            MarginInfo margin = new MarginInfo();
            margin.Top = 10;
            margin.Left = 10;
            margin.Right = 10;
            margin.Bottom = 10;


            tab1.DefaultCellPadding = margin;


            Aspose.Pdf.Generator.Row row1 = tab1.Rows.Add();
            row1.Cells.Add("Log name:");
            row1.Cells.Add(hl.logName);

            Aspose.Pdf.Generator.Row row2 = tab1.Rows.Add();
            row2.Cells.Add("Log description:");
            row2.Cells.Add(hl.logDescription);


            Aspose.Pdf.Generator.Row row3 = tab1.Rows.Add();
            row3.Cells.Add("First observed:");
            row3.Cells.Add(hl.date);

            Text pdfHeadText1 = new Text("Symptoms");

            pdfHeadText1.TextInfo.FontName = "Arial";
            pdfHeadText1.TextInfo.IsTrueTypeFontBold = true;

            pdfHeadText1.TextInfo.FontSize = 16;
            pdfHeadText1.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText1);


            Table tab2 = new Table();
            sec1.Paragraphs.Add(tab2);
            tab2.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo margin1 = new MarginInfo();
            margin1.Top = 5;
            margin1.Left = 5;
            margin1.Right = 5;
            margin1.Bottom = 5;


            tab2.DefaultCellPadding = margin1;

            int i = 1;

            foreach (var symptom in hl.symptoms)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = true;


                Row srow5 = tab2.Rows.Add();
                srow5.Cells.Add("Symptom" + i, t);



                Row srow1 = tab2.Rows.Add();
                srow1.Cells.Add("Created on:");
                srow1.Cells.Add(symptom.DateTime.ToString());


                Row srow2 = tab2.Rows.Add();
                srow2.Cells.Add("Symptom name:");
                srow2.Cells.Add(symptom.Name);


                Row srow3 = tab2.Rows.Add();
                srow3.Cells.Add("Symptom description:");
                srow3.Cells.Add(symptom.Description);

                Row srow4 = tab2.Rows.Add();
                srow4.Cells.Add("Reason:");
                srow4.Cells.Add(symptom.Reasons);

                Row srow6 = tab2.Rows.Add();
                srow6.Cells.Add("Medicine:");
                srow6.Cells.Add(symptom.Medicines);
                i++;
            }



            pdf1.Save(DestPdfFilePath);


            return File(DestPdfFilePath, "application/pdf", retFileName);
        }
        //end



    }
}