using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Models;
using System.IO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Configuration;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    [SessionExpireFilter]
    public class PersonalInfoController : Controller
    {
        //
        // GET: /iHealthUser/PersonalInfo/
        UserInformation usr = null;
        //[Authorize]
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize]
        public ActionResult PInfo2()
        {
            Services3 s3 = new Services3();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;

            }

            if (usr == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }
            MedicalAndPersonal m = new MedicalAndPersonal();
            m.MInfo1 = s3.GetMedicalInfo(usr.UserId);

            string _eyesight = m.MInfo1.EyeSight;

            if (!String.IsNullOrWhiteSpace(_eyesight))
            {

                if (_eyesight.Contains("/"))
                {
                    string[] Eyesight = _eyesight.Split('/');

                    m.MInfo1.LeftEye = Eyesight[0];
                    m.MInfo1.RightEye = Eyesight[1];

                }
                else
                {

                    m.MInfo1.LeftEye = "";
                    m.MInfo1.RightEye = "";
                }
            }
            else
            {

                m.MInfo1.LeftEye = "";
                m.MInfo1.RightEye = "";
            }

            List<string> templates = new List<string>();
            templates.Add(m.MInfo1.Thyroidproblems);
            templates.Add(m.MInfo1.ChronicDiseases);
            templates.Add(m.MInfo1.GeneticDisoorder);
            templates.Add(m.MInfo1.Vaccination);
            templates.Add(m.MInfo1.GenitalInfections);
            templates.Add(m.MInfo1.Arthrits);

            Diseases s = new Diseases();
            foreach (string t in templates)
            {
                XmlDocument xmldoc = new XmlDocument();
                if (!string.IsNullOrWhiteSpace(t))
                {
                    xmldoc.LoadXml(t);


                    for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
                    {
                        string node = xmldoc.DocumentElement.Name;


                        XmlElement name = xmldoc.DocumentElement.ChildNodes[i]["Name"];
                        if (name != null)
                        {
                            if (node == "ThyroidProblems")
                                s.ThyroidProblems.Add(name.InnerText);
                            else if (node == "ChronicDiseases")
                                s.ChronicDiseases.Add(name.InnerText);
                            else if (node == "GeneticDisorders")
                                s.GeneticDisorders.Add(name.InnerText);
                            else if (node == "Vaccinations")
                                s.Vaccination.Add(name.InnerText);
                            else if (node == "GenitalInfections")
                                s.GenitalInfections.Add(name.InnerText);
                            else if (node == "Arthritis")
                                s.Arthritis.Add(name.InnerText);
                        }

                    }
                    m.al.Add(s);
                }
            }

            return View("PInfo2", m);
        }

        //sandeep changes start here
        //public ActionResult PInfo3()
        //{
        //    return View("PInfo3");
        //}
        //[Authorize]
        public ActionResult PInfo3()
        {
            Services3 s3 = new Services3();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }

            if (usr == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }
            MedicalAndPersonal m = new MedicalAndPersonal();
            m.MInfo1 = s3.GetMedicalInfo(usr.UserId);

            //m.MInfo1 = s3.GetMedicalInfo(usr.UserId);

            List<string> templates = new List<string>();
            templates.Add(m.MInfo1.Asthma);
            templates.Add(m.MInfo1.LiverDisease);
            templates.Add(m.MInfo1.Cancer);
            templates.Add(m.MInfo1.HeartProblems);
            templates.Add(m.MInfo1.Thyroidproblems);
            templates.Add(m.MInfo1.ChronicDiseases);

            Diseases s = new Diseases();
            foreach (string t in templates)
            {

                XmlDocument xmldoc = new XmlDocument();
                if (!string.IsNullOrWhiteSpace(t))
                {
                    xmldoc.LoadXml(t);


                    for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
                    {
                        string node = xmldoc.DocumentElement.Name;


                        XmlElement name = xmldoc.DocumentElement.ChildNodes[i]["Name"];
                        if (name != null)
                        {
                            if (node == "Asthmas")
                                s.Asthamas.Add(name.InnerText);
                            else if (node == "LiverProblems")
                                s.Liverproblems.Add(name.InnerText);
                            else if (node == "HeartProblems")
                                s.Heartproblems.Add(name.InnerText);
                            else if (node == "Cancers")
                                s.Cancers.Add(name.InnerText);
                            else if (node == "ChronicDiseases")
                                s.ChronicDiseases.Add(name.InnerText);
                            else if (node == "ThyroidProblems")
                                s.ThyroidProblems.Add(name.InnerText);

                        }
                    }
                    m.al.Add(s);
                }
            }
            return View(m);
        }
        //sandeep changes end here
        // [Authorize]

        public ActionResult PInfo()
        {
            Services3 s3 = new Services3();
            string currentpage = "PersonalInfo";
            Session["CurrentPage"] = currentpage;
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            if (usr == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }
            MedicalAndPersonal m = new MedicalAndPersonal();

            m.PInfo1 = s3.GetPersonalInfo(usr.UserId);

            DateTime d = Convert.ToDateTime(usr.StartDate);
            m.PInfo1.duedate = usr.StartDate;
            m.PInfo1.EDDDate = d.AddDays(280);
            DateTime edd = Convert.ToDateTime(m.PInfo1.EDDDate);
            m.PInfo1.duedat = edd.ToString("dd MMM yyyy");
            DateTime d1 = Convert.ToDateTime(m.PInfo1.DateOfBirth);
            m.PInfo1.dateofbir = d1.ToString("dd MMM yyyy");


            m.MInfo1 = s3.GetMedicalInfo(usr.UserId);
            string _eyesight = m.MInfo1.EyeSight;

            if (!String.IsNullOrWhiteSpace(_eyesight))
            {

                if (_eyesight.Contains("/"))
                {
                    string[] Eyesight = _eyesight.Split('/');

                    m.MInfo1.LeftEye = Eyesight[0];
                    m.MInfo1.RightEye = Eyesight[1];

                }
                else
                {

                    m.MInfo1.LeftEye = "";
                    m.MInfo1.RightEye = "";
                }
            }
            else
            {

                m.MInfo1.LeftEye = "";
                m.MInfo1.RightEye = "";
            }
            string _bloodpresure = m.MInfo1.BloodPressure;


            if (!String.IsNullOrWhiteSpace(_bloodpresure))
            {

                if (_bloodpresure.Contains("/"))
                {
                    string[] Bloodpresure = _bloodpresure.Split('/');

                    m.MInfo1.Systole = Bloodpresure[0];
                    m.MInfo1.Diastole = Bloodpresure[1];

                }
                else
                {

                    m.MInfo1.Systole = "";
                    m.MInfo1.Diastole = "";
                }
            }
            else
            {

                m.MInfo1.Systole = "";
                m.MInfo1.Diastole = "";
            }


            List<string> templates = new List<string>();
            templates.Add(m.MInfo1.Allergies);
            templates.Add(m.MInfo1.Vaccination);
            templates.Add(m.MInfo1.Thyroidproblems);
            templates.Add(m.MInfo1.ChronicDiseases);
            templates.Add(m.MInfo1.GeneticDisoorder);
            //templates.Add(m.MInfo1.Vaccination);
            templates.Add(m.MInfo1.GenitalInfections);
            templates.Add(m.MInfo1.Arthrits);
            templates.Add(m.MInfo1.Cancer);
            templates.Add(m.MInfo1.Asthma);
            templates.Add(m.MInfo1.LiverDisease);
            templates.Add(m.MInfo1.HeartProblems);

            Diseases s = new Diseases();
            foreach (string t in templates)
            {

                XmlDocument xmldoc = new XmlDocument();
                if (!string.IsNullOrWhiteSpace(t))
                {
                    xmldoc.LoadXml(t);


                    for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
                    {
                        string node = xmldoc.DocumentElement.Name;


                        XmlElement name = xmldoc.DocumentElement.ChildNodes[i]["Name"];
                        if (name != null)
                        {
                            if (node == "Allergies")
                                s.Allergies.Add(name.InnerText);
                            else if (node == "Vaccinations")
                                s.Vaccination.Add(name.InnerText);
                            else if (node == "ThyroidProblems")
                                s.ThyroidProblems.Add(name.InnerText);
                            else if (node == "ChronicDiseases")
                                s.ChronicDiseases.Add(name.InnerText);
                            else if (node == "GeneticDisorders")
                                s.GeneticDisorders.Add(name.InnerText);
                            //else if (node == "Vaccinations")
                            // s.Vaccination.Add(name.InnerText);
                            else if (node == "GenitalInfections")
                                s.GenitalInfections.Add(name.InnerText);
                            else if (node == "Arthritis")
                                s.Arthritis.Add(name.InnerText);
                            else if (node == "Asthmas")
                                s.Asthamas.Add(name.InnerText);
                            else if (node == "LiverProblems")
                                s.Liverproblems.Add(name.InnerText);
                            else if (node == "HeartProblems")
                                s.Heartproblems.Add(name.InnerText);
                            else if (node == "Cancers")
                                s.Cancers.Add(name.InnerText);
                            else if (node == "ChronicDiseases")
                                s.ChronicDiseases.Add(name.InnerText);
                            else if (node == "ThyroidProblems")
                                s.ThyroidProblems.Add(name.InnerText);

                        }
                    }
                    m.al.Add(s);
                }
            }

            return View("PInfo", m);

        }


        public ActionResult EditPersonalInfo(MedicalAndPersonal m)
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
            //Services3 s3 = new Services3();
            //PersonalInformation pInfo = null;
            //pInfo = m.PInfo1;

            //PersonalInformation objpInfo = s3.GetPersonalInfo(pInfo.UserId);
            Services1 s1=new Services1();
            Services3 s3 = new Services3();
            PersonalInformation pInfo = null;
            PersonalInformation objpInfo = null;
            pInfo = m.PInfo1;
            if (pInfo != null)
            {
                objpInfo = s3.GetPersonalInfo(pInfo.CreatedBy);
            }
            else
            {
                objpInfo = s3.GetPersonalInfo(pInfo.CreatedBy);
                pInfo = objpInfo;
            }
            pInfo.ImageName = objpInfo.ImageName;
            pInfo.OriginalName = objpInfo.OriginalName;
            pInfo.DateOfBirth = Convert.ToDateTime(pInfo.dateofbir);
            int result = s3.UpdatePersonalInfo(pInfo, usr.UserId);

            if (result == 1010)
            {
                UserInformation usrinfo = s1.GetUserbyId(usr.UserId);
                Session["LoginUser"] = usrinfo;
            }
            Session["LoginName"] = pInfo.FirstName + " " + (pInfo.LastName != null ? pInfo.LastName : string.Empty);
            MedicalAndPersonal m2 = new MedicalAndPersonal();
            if (result == 1010)
            {
                m2.PInfo1 = s3.GetPersonalInfo(usr.UserId);
                m2.MInfo1 = s3.GetMedicalInfo(usr.UserId);

                List<string> templates = new List<string>();
                templates.Add(m2.MInfo1.Allergies);
                templates.Add(m2.MInfo1.Vaccination);
                templates.Add(m2.MInfo1.Cancer);
                templates.Add(m2.MInfo1.Asthma);
                templates.Add(m2.MInfo1.LiverDisease);
                templates.Add(m2.MInfo1.HeartProblems);

                Diseases s = new Diseases();
                foreach (string t in templates)
                {

                    XmlDocument xmldoc = new XmlDocument();
                    if (!string.IsNullOrWhiteSpace(t))
                    {
                        xmldoc.LoadXml(t);

                        for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
                        {
                            string node = xmldoc.DocumentElement.Name;

                            XmlElement name = xmldoc.DocumentElement.ChildNodes[i]["Name"];
                            if (name != null)
                            {
                                if (node == "Allergies")
                                    s.Allergies.Add(name.InnerText);
                                else if (node == "Cancers")
                                    s.Cancers.Add(name.InnerText);
                                else if (node == "Asthmas")
                                    s.Asthamas.Add(name.InnerText);
                                else if (node == "LiverProblems")
                                    s.Liverproblems.Add(name.InnerText);
                                else if (node == "HeartProblems")
                                    s.Heartproblems.Add(name.InnerText);
                                else if (node == "Vaccinations")
                                    s.Vaccination.Add(name.InnerText);

                            }
                        }
                        m2.al.Add(s);
                    }
                }

            }
            //return View("PInfo", m2);
            return RedirectToAction("PInfo");
        }

        [HttpPost]
        public ActionResult EditAlergiesInfo(string hdSelectedItems, string T)
        {
            string p = hdSelectedItems;
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            MedicalInformation mInfo = new MedicalInformation();
            string[] a = T.Split(',');
            T = a[0];
            string template = GenerateXMl(hdSelectedItems, T);

            if (T == "Allergy")
                mInfo.Allergies = template;
            else if (T == "Cancer")
                mInfo.Cancer = template;
            else if (T == "Asthma")
                mInfo.Asthma = template;
            else if (T == "Liver disease")
                mInfo.LiverDisease = template;
            else if (T == "Heart problem")
                mInfo.HeartProblems = template;
            else if (T == "Thyroid Problems")
                mInfo.Thyroidproblems = template;
            else if (T == "Chronic Diseases")
                mInfo.ChronicDiseases = template;
            else if (T == "Genetic disorder")
                mInfo.GeneticDisoorder = template;
            else if (T == "Vaccination and Immunization")
                mInfo.Vaccination = template;
            else if (T == "Genital infections")
                mInfo.GenitalInfections = template;
            else if (T == "Arthritis")
                mInfo.Arthrits = template;


            Services3 s3 = new Services3();
            int result = s3.UpdateMedicalInfo(mInfo, usr.UserId.ToString(), usr.DomainId.ToString(), T, usr.GroupType);
            MedicalAndPersonal m2 = new MedicalAndPersonal();
            if (result == 1010)
            {
                m2.PInfo1 = s3.GetPersonalInfo(usr.UserId);
                m2.MInfo1 = s3.GetMedicalInfo(usr.UserId);


                List<string> templates = new List<string>();
                templates.Add(m2.MInfo1.Allergies);
                templates.Add(m2.MInfo1.Cancer);
                templates.Add(m2.MInfo1.Asthma);
                templates.Add(m2.MInfo1.LiverDisease);
                templates.Add(m2.MInfo1.HeartProblems);
                templates.Add(m2.MInfo1.Thyroidproblems);
                templates.Add(m2.MInfo1.ChronicDiseases);
                templates.Add(m2.MInfo1.GeneticDisoorder);
                templates.Add(m2.MInfo1.Vaccination);
                templates.Add(m2.MInfo1.GenitalInfections);
                templates.Add(m2.MInfo1.Arthrits);

                Diseases s = new Diseases();
                foreach (string t in templates)
                {

                    XmlDocument xmldoc = new XmlDocument();
                    if (!string.IsNullOrWhiteSpace(t))
                    {
                        xmldoc.LoadXml(t);

                        for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
                        {
                            string node = xmldoc.DocumentElement.Name;


                            XmlElement name = xmldoc.DocumentElement.ChildNodes[i]["Name"];
                            if (name != null)
                            {
                                if (node == "Allergies")
                                    s.Allergies.Add(name.InnerText);
                                else if (node == "Cancers")
                                    s.Cancers.Add(name.InnerText);
                                else if (node == "Asthmas")
                                    s.Asthamas.Add(name.InnerText);
                                else if (node == "LiverProblems")
                                    s.Liverproblems.Add(name.InnerText);
                                else if (node == "HeartProblems")
                                    s.Heartproblems.Add(name.InnerText);
                                else if (node == "ThyroidProblems")
                                    s.ThyroidProblems.Add(name.InnerText);
                                else if (node == "ChronicDiseases")
                                    s.ChronicDiseases.Add(name.InnerText);
                                else if (node == "GeneticDisorders")
                                    s.GeneticDisorders.Add(name.InnerText);
                                else if (node == "Vaccinations")
                                    s.Vaccination.Add(name.InnerText);
                                else if (node == "GenitalInfections")
                                    s.GenitalInfections.Add(name.InnerText);
                                else if (node == "Arthritis")
                                    s.Arthritis.Add(name.InnerText);
                            }
                        }
                        m2.al.Add(s);
                    }
                }
            }
            if (a[1] == "PInfo")
            {
                return View("PInfo", m2);
            }
            else if (a[1] == "PInfo2")
            {
                return View("PInfo2", m2);
            }
            else
            {
                return View("PInfo3", m2);
            }

        }

        public ActionResult ChangePicture()
        {
            HttpPostedFileBase file = Request.Files["user_image"];
            if (file != null)
            {
                string fname = Path.GetFileName(file.FileName);
                //file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));
                Image image = Image.FromFile(fname);
                byte[] imageByte;
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Gif);
                    imageByte = ms.ToArray();
                }
            }
            return View("PInfo");
        }

        public string GenerateXMl(string value, string t)
        {
            string template = "";
            MemoryStream memoryStream = new MemoryStream();

            XmlTextWriter xmlWriter = new XmlTextWriter(memoryStream, Encoding.ASCII);

            xmlWriter.Formatting = Formatting.Indented;

            if (t == "Allergy")
                xmlWriter.WriteStartElement("Allergies");
            else if (t == "Cancer")
                xmlWriter.WriteStartElement("Cancers");
            else if (t == "Asthma")
                xmlWriter.WriteStartElement("Asthmas");
            else if (t == "Liver disease")
                xmlWriter.WriteStartElement("LiverProblems");
            else if (t == "Heart problem")
                xmlWriter.WriteStartElement("HeartProblems");
            else if (t == "Thyroid Problems")
                xmlWriter.WriteStartElement("ThyroidProblems");
            else if (t == "Chronic Diseases")
                xmlWriter.WriteStartElement("ChronicDiseases");
            else if (t == "Genetic disorder")
                xmlWriter.WriteStartElement("GeneticDisorders");
            else if (t == "Vaccination and Immunization")
                xmlWriter.WriteStartElement("Vaccinations");
            else if (t == "Genital infections")
                xmlWriter.WriteStartElement("GenitalInfections");
            else if (t == "Arthritis")
                xmlWriter.WriteStartElement("Arthritis");

            string[] a = value.Split(',');

            int arlength = 0;
            if (value.Contains(","))
                arlength = a.Length - 1;
            else
                arlength = a.Length;

            for (int i = 0; i < arlength; i++)
            {
                if (t == "Allergy")
                    xmlWriter.WriteStartElement("Allergy");
                else if (t == "Cancer")
                    xmlWriter.WriteStartElement("Cancer");
                else if (t == "Asthma")
                    xmlWriter.WriteStartElement("Asthma");
                else if (t == "Liver disease")
                    xmlWriter.WriteStartElement("LiverProblem");
                else if (t == "Heart problem")
                    xmlWriter.WriteStartElement("HeartHeartProblem");
                else if (t == "Thyroid Problems")
                    xmlWriter.WriteStartElement("ThyoroidProblem");
                else if (t == "Chronic Diseases")
                    xmlWriter.WriteStartElement("ChronicDisease");
                else if (t == "Genetic disorder")
                    xmlWriter.WriteStartElement("GeneticDisorder");
                else if (t == "Vaccination and Immunization")
                    xmlWriter.WriteStartElement("Vaccination");
                else if (t == "Genital infections")
                    xmlWriter.WriteStartElement("GenitalInfection");
                else if (t == "Arthritis")
                    xmlWriter.WriteStartElement("Arthriti");

                xmlWriter.WriteElementString("Name", a[i]);
                xmlWriter.WriteEndElement();

            }
            xmlWriter.WriteEndElement();
            xmlWriter.Flush();

            xmlWriter.Close();

            byte[] byteStream = memoryStream.ToArray();

            template = Encoding.ASCII.GetString(byteStream);

            return template;
        }

        public ActionResult UpdateAlergiesInfo(string tbVieInfoName, string reaction, string severity, string provider, string notes, string viewid, string button)
        {
            Services3 s3 = new Services3();
            UserInformation usr = Session["LoginUser"] as UserInformation;
            MedicalInformation mInfo = new MedicalInformation();
            MedicalAndPersonal m = new MedicalAndPersonal();
            int result = 0;
            string[] a = viewid.Split(',');
            viewid = a[0];
            if (button == "btnUpdate")
            {



                MemoryStream memoryStream = new MemoryStream();

                XmlTextWriter xmlWriter = new XmlTextWriter(memoryStream, Encoding.ASCII);

                xmlWriter.Formatting = Formatting.Indented;


                if (viewid == "Allergies")
                    xmlWriter.WriteStartElement("Allergies");
                else if (viewid == "Cancer")
                    xmlWriter.WriteStartElement("Cancers");
                else if (viewid == "Asthma")
                    xmlWriter.WriteStartElement("Asthmas");
                else if (viewid == "Liver")
                    xmlWriter.WriteStartElement("LiverProblems");
                else if (viewid == "Heart")
                    xmlWriter.WriteStartElement("HeartProblems");
                else if (viewid == "Thyroid")
                    xmlWriter.WriteStartElement("ThyroidProblems");
                else if (viewid == "ChronicDiseases")
                    xmlWriter.WriteStartElement("ChronicDiseases");
                else if (viewid == "GeneticDisorders")
                    xmlWriter.WriteStartElement("GeneticDisorders");
                else if (viewid == "VaccinationImmunization")
                    xmlWriter.WriteStartElement("Vaccinations");
                else if (viewid == "GenitalInfections")
                    xmlWriter.WriteStartElement("GenitalInfections");
                else if (viewid == "Arthritis")
                    xmlWriter.WriteStartElement("Arthritis");


                if (viewid == "Allergies")
                    xmlWriter.WriteStartElement("Allergy");
                else if (viewid == "Cancer")
                    xmlWriter.WriteStartElement("Cancer");
                else if (viewid == "Asthma")
                    xmlWriter.WriteStartElement("Asthma");
                else if (viewid == "Liver")
                    xmlWriter.WriteStartElement("LiverProblem");
                else if (viewid == "Heart")
                    xmlWriter.WriteStartElement("HeartProblem");
                else if (viewid == "Thyroid")
                    xmlWriter.WriteStartElement("ThyoroidProblem");
                else if (viewid == "ChronicDiseases")
                    xmlWriter.WriteStartElement("ChronicDisease");
                else if (viewid == "GeneticDisorders")
                    xmlWriter.WriteStartElement("GeneticDisorder");
                else if (viewid == "VaccinationImmunization")
                    xmlWriter.WriteStartElement("Vaccination");
                else if (viewid == "GenitalInfections")
                    xmlWriter.WriteStartElement("GenitalInfection");
                else if (viewid == "Arthritis")
                    xmlWriter.WriteStartElement("Arthriti");

                xmlWriter.WriteElementString("Name", tbVieInfoName);
                xmlWriter.WriteElementString("Reaction", reaction);
                xmlWriter.WriteElementString("Severity", severity);
                xmlWriter.WriteElementString("Provider", provider);
                xmlWriter.WriteElementString("Notes", notes);
                xmlWriter.WriteEndElement();


                xmlWriter.WriteEndElement();
                xmlWriter.Flush();

                xmlWriter.Close();

                byte[] byteStream = memoryStream.ToArray();
                string template = Encoding.ASCII.GetString(byteStream);


                if (viewid == "Allergies")
                    mInfo.Allergies = template;
                else if (viewid == "Cancer")
                    mInfo.Cancer = template;
                else if (viewid == "Asthma")
                    mInfo.Asthma = template;
                else if (viewid == "Liver")
                    mInfo.LiverDisease = template;
                else if (viewid == "Heart")
                    mInfo.HeartProblems = template;
                else if (viewid == "Thyroid")
                    mInfo.Thyroidproblems = template;
                else if (viewid == "ChronicDiseases")
                    mInfo.ChronicDiseases = template;
                else if (viewid == "GeneticDisorders")
                    mInfo.GeneticDisoorder = template;
                else if (viewid == "VaccinationImmunization")
                    mInfo.Vaccination = template;
                else if (viewid == "GenitalInfections")
                    mInfo.GenitalInfections = template;
                else if (viewid == "Arthritis")
                    mInfo.Arthrits = template;

                result = s3.UpdateMedicalInfo(mInfo, usr.UserId.ToString(), usr.DomainId.ToString(), viewid, usr.GroupType);

            }
            else if (button == "btnRemove")
            {


                m.MInfo1 = s3.GetMedicalInfo(usr.UserId);
                string template = string.Empty;

                if (viewid == "Allergies")
                    template = m.MInfo1.Allergies;
                else if (viewid == "Cancer")
                    template = m.MInfo1.Cancer;
                else if (viewid == "Asthma")
                    template = m.MInfo1.Asthma;
                else if (viewid == "Liver")
                    template = m.MInfo1.LiverDisease;
                else if (viewid == "Heart")
                    template = m.MInfo1.HeartProblems;
                else if (viewid == "Thyroid")
                    template = m.MInfo1.Thyroidproblems;
                else if (viewid == "ChronicDiseases")
                    template = m.MInfo1.ChronicDiseases;
                else if (viewid == "GeneticDisorders")
                    template = m.MInfo1.GeneticDisoorder;
                else if (viewid == "VaccinationImmunization")
                    template = m.MInfo1.Vaccination;
                else if (viewid == "GenitalInfections")
                    template = m.MInfo1.GenitalInfections;
                else if (viewid == "Arthritis")
                    template = m.MInfo1.Arthrits;



                XmlDocument xmldoc = new XmlDocument();
                if (!string.IsNullOrWhiteSpace(template))
                {
                    xmldoc.LoadXml(template);


                    for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
                    {
                        XmlElement value = xmldoc.DocumentElement.ChildNodes[i]["Name"];
                        string r = value.InnerText;
                        if (r.Trim() == tbVieInfoName.Trim())
                        {
                            xmldoc.DocumentElement.RemoveChild(value.ParentNode);

                        }
                    }
                }
                MemoryStream memoryStream = new MemoryStream();
                xmldoc.Save(memoryStream);
                byte[] byteStream = memoryStream.ToArray();

                string k = Encoding.ASCII.GetString(byteStream);

                if (viewid == "Allergies")
                    mInfo.Allergies = k;
                else if (viewid == "Cancer")
                    mInfo.Cancer = k;
                else if (viewid == "Asthma")
                    mInfo.Asthma = k;
                else if (viewid == "Liver")
                    mInfo.LiverDisease = k;
                else if (viewid == "Heart")
                    mInfo.HeartProblems = k;
                else if (viewid == "Thyroid")
                    mInfo.Thyroidproblems = k;
                else if (viewid == "ChronicDiseases")
                    mInfo.ChronicDiseases = k;
                else if (viewid == "GeneticDisorders")
                    mInfo.GeneticDisoorder = k;
                else if (viewid == "VaccinationImmunization")
                    mInfo.Vaccination = k;
                else if (viewid == "GenitalInfections")
                    mInfo.GenitalInfections = k;
                else if (viewid == "Arthritis")
                    mInfo.Arthrits = k;


                result = s3.UpdateMedicalInfoForDelete(mInfo, usr.UserId.ToString(), usr.DomainId.ToString());
            }

            m.PInfo1 = s3.GetPersonalInfo(usr.UserId);
            m.MInfo1 = s3.GetMedicalInfo(usr.UserId);

            List<string> templates = new List<string>();
            templates.Add(m.MInfo1.Allergies);
            templates.Add(m.MInfo1.Cancer);
            templates.Add(m.MInfo1.Asthma);
            templates.Add(m.MInfo1.LiverDisease);
            templates.Add(m.MInfo1.HeartProblems);
            templates.Add(m.MInfo1.Thyroidproblems);
            templates.Add(m.MInfo1.ChronicDiseases);
            templates.Add(m.MInfo1.GeneticDisoorder);
            templates.Add(m.MInfo1.Vaccination);
            templates.Add(m.MInfo1.GenitalInfections);
            templates.Add(m.MInfo1.Arthrits);


            Diseases s = new Diseases();
            foreach (string t in templates)
            {

                XmlDocument xmldoc = new XmlDocument();
                if (!string.IsNullOrWhiteSpace(t))
                {
                    xmldoc.LoadXml(t);


                    for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
                    {
                        string node = xmldoc.DocumentElement.Name;


                        XmlElement name = xmldoc.DocumentElement.ChildNodes[i]["Name"];
                        if (name != null)
                        {
                            if (node == "Allergies")
                                s.Allergies.Add(name.InnerText);
                            else if (node == "Cancers")
                                s.Cancers.Add(name.InnerText);
                            else if (node == "Asthmas")
                                s.Asthamas.Add(name.InnerText);
                            else if (node == "LiverProblems")
                                s.Liverproblems.Add(name.InnerText);
                            else if (node == "HeartProblems")
                                s.Heartproblems.Add(name.InnerText);
                            else if (node == "ThyroidProblems")
                                s.ThyroidProblems.Add(name.InnerText);
                            else if (node == "ChronicDiseases")
                                s.ChronicDiseases.Add(name.InnerText);
                            else if (node == "GeneticDisorders")
                                s.GeneticDisorders.Add(name.InnerText);
                            else if (node == "Vaccinations")
                                s.Vaccination.Add(name.InnerText);
                            else if (node == "GenitalInfections")
                                s.GenitalInfections.Add(name.InnerText);
                            else if (node == "Arthritis")
                                s.Arthritis.Add(name.InnerText);

                        }

                    }
                    m.al.Add(s);
                }
            }


            if (a[1] == "PInfo")
            {
                // return View("PInfo", m);
                return RedirectToAction("PInfo");
            }
            else if (a[1] == "PInfo2")
            {
                return View("PInfo2", m);
            }
            else
            {
                //return View("PInfo3", m);
                return RedirectToAction("PInfo3");
            }

        }

        public ActionResult DeleteAlergiesInfo()
        {
            return View("PInfo");
        }

        public ActionResult GetRelated(string data, string data1)
        {
            //string result=string.Empty;
            List<string> result = new List<string>();
            Services3 s3 = new Services3();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            MedicalAndPersonal m2 = new MedicalAndPersonal();
            Result Result = new Result();

            m2.PInfo1 = s3.GetPersonalInfo(usr.UserId);
            m2.MInfo1 = s3.GetMedicalInfo(usr.UserId);

            string template1 = string.Empty;

            if (data1 == "Allergies")
                template1 = m2.MInfo1.Allergies;
            else if (data1 == "Cancer")
                template1 = m2.MInfo1.Cancer;
            else if (data1 == "Asthma")
                template1 = m2.MInfo1.Asthma;
            else if (data1 == "Liver")
                template1 = m2.MInfo1.LiverDisease;
            else if (data1 == "Heart")
                template1 = m2.MInfo1.HeartProblems;
            else if (data1 == "Thyroid")
                template1 = m2.MInfo1.Thyroidproblems;
            else if (data1 == "ChronicDiseases")
                template1 = m2.MInfo1.ChronicDiseases;
            else if (data1 == "GeneticDisorders")
                template1 = m2.MInfo1.GeneticDisoorder;
            else if (data1 == "VaccinationImmunization")
                template1 = m2.MInfo1.Vaccination;
            else if (data1 == "GenitalInfections")
                template1 = m2.MInfo1.GenitalInfections;
            else if (data1 == "Arthritis")
                template1 = m2.MInfo1.Arthrits;


            XmlDocument xmldoc = new XmlDocument();
            if (!string.IsNullOrWhiteSpace(template1))
            {
                xmldoc.LoadXml(template1);
                for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
                {
                    XmlElement name = xmldoc.DocumentElement.ChildNodes[i]["Name"];
                    if (string.Compare(name.InnerText.Trim(), data.Trim(), true) == 0)
                    {
                        XmlElement Reaction = xmldoc.DocumentElement.ChildNodes[i]["Reaction"];
                        if (Reaction != null)
                            Result.Reaction = Reaction.InnerText;

                        XmlElement Severity = xmldoc.DocumentElement.ChildNodes[i]["Severity"];
                        if (Severity != null)
                            Result.Severity = Severity.InnerText;

                        XmlElement Provider = xmldoc.DocumentElement.ChildNodes[i]["Provider"];
                        if (Provider != null)
                            Result.Provider = Provider.InnerText;

                        XmlElement Notes = xmldoc.DocumentElement.ChildNodes[i]["Notes"];
                        if (Notes != null)
                            Result.Notes = Notes.InnerText;
                    }
                }
            }

            JsonResult jr = new JsonResult();
            jr.Data = Result;
            return jr;
        }

        public ActionResult GetListItems(string data)
        {
            Services3 s3 = new Services3();
            MedicalAndPersonal m2 = new MedicalAndPersonal();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            m2.PInfo1 = s3.GetPersonalInfo(usr.UserId);
            m2.MInfo1 = s3.GetMedicalInfo(usr.UserId);
            m2.Alergies = m2.FillList(data);

            JsonResult jr = new JsonResult();
            jr.Data = m2.Alergies;
            return jr;
        }

        //public ActionResult Upload()
        //{
        //    Services3 s3 = new Services3();
        //    if (Session["CurrentUser"] != null)
        //    {
        //        usr = Session["CurrentUser"] as UserInformation;
        //    }
        //    else
        //    {
        //        // LoginUser = new UserInformation();
        //        usr = Session["LoginUser"] as UserInformation;
        //    }
        //    MedicalAndPersonal m = new MedicalAndPersonal();

        //    m.PInfo1 = s3.GetPersonalInfo(usr.UserId);
        //    m.MInfo1 = s3.GetMedicalInfo(usr.UserId);
        //    //ViewBag.eInfo = EmergencyInfo.GetEmergencyInfo();
        //    //ViewBag.iInfo = InsuranceInfo.GetInsuranceInfo();
        //    //ViewBag.mInfo = MedicalInfo.GetMedicalInfo();

        //    foreach (string inputTagName in Request.Files)
        //    {
        //        HttpPostedFileBase file = Request.Files[inputTagName];

        //        if (file.ContentLength > 0)
        //        {                   
        //            string filePath = Path.Combine(HttpContext.Server.MapPath("~/Image")//("../Uploads")
        //            , Path.GetFileName(file.FileName));
        //            //ViewData["filePath"] = Path.GetFileName(file.FileName);
        //            string imagepath = Path.GetFileName(file.FileName);

        //            file.SaveAs(@"D:\Ihealth\iHealth @ 30-1-2013\Source\InnDocs.iHealth.Web.UI\InnDocs.iHealth.Web.UI\Image\" + usr.UserId.ToString() + ".jpg");
        //            m.PInfo1.OriginalName = imagepath;
        //            m.PInfo1.ImageName = usr.UserId.ToString() + ".jpg"; ;
        //        }
        //    }
        //    List<string> templates = new List<string>();
        //    templates.Add(m.MInfo1.Allergies);
        //    templates.Add(m.MInfo1.Cancer);
        //    templates.Add(m.MInfo1.Asthma);
        //    templates.Add(m.MInfo1.LiverDisease);
        //    templates.Add(m.MInfo1.HeartProblems);

        //    Diseases s = new Diseases();
        //    foreach (string t in templates)
        //    {

        //        XmlDocument xmldoc = new XmlDocument();
        //        if (!string.IsNullOrWhiteSpace(t))
        //        {
        //            xmldoc.LoadXml(t);

        //            for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
        //            {
        //                string node = xmldoc.DocumentElement.Name;

        //                XmlElement name = xmldoc.DocumentElement.ChildNodes[i]["Name"];
        //                if (name != null)
        //                {
        //                    if (node == "Allergies")
        //                        s.Allergies.Add(name.InnerText);
        //                    else if (node == "Cancers")
        //                        s.Cancers.Add(name.InnerText);
        //                    else if (node == "Asthmas")
        //                        s.Asthamas.Add(name.InnerText);
        //                    else if (node == "LiverProblems")
        //                        s.Liverproblems.Add(name.InnerText);
        //                    else if (node == "HeartProblems")
        //                        s.Heartproblems.Add(name.InnerText);
        //                }
        //            }
        //            m.al.Add(s);
        //        }
        //    }

        //    s3.UpdatePersonalInfo(m.PInfo1, usr.UserId);
        //    return View("PInfo", m);

        //    //RedirectToAction("MyInfo","PersonalInfo");//, "PersonalInfo");
        //}

        public ActionResult Upload()
        {
            Services3 s3 = new Services3();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            MedicalAndPersonal m = new MedicalAndPersonal();

            m.PInfo1 = s3.GetPersonalInfo(usr.UserId);
            m.MInfo1 = s3.GetMedicalInfo(usr.UserId);
            //ViewBag.eInfo = EmergencyInfo.GetEmergencyInfo();
            //ViewBag.iInfo = InsuranceInfo.GetInsuranceInfo();
            //ViewBag.mInfo = MedicalInfo.GetMedicalInfo();

            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];

                if (file.ContentLength > 0)
                {
                    string filePath = Path.Combine(HttpContext.Server.MapPath("~/Image")//("../Uploads")
                    , Path.GetFileName(file.FileName));
                    //ViewData["filePath"] = Path.GetFileName(file.FileName);
                    string imagepath = Path.GetFileName(file.FileName);
                    //for bumpdocs.com
                      file.SaveAs(@"C:\inetpub\wwwroot\BumpDocs\Uploads\" + usr.UserId.ToString() + ".jpg");

                    //for bumpdocs product
                  //  file.SaveAs(@"C:\inetpub\wwwroot\BumpDocsProduct\Uploads\" + usr.UserId.ToString() + ".jpg");
                    //file.SaveAs(@"C:\inetpub\wwwroot\iHealthMobile\Uploads\" + usr.UserId.ToString() + ".jpg");
                    m.PInfo1.OriginalName = imagepath;
                    m.PInfo1.ImageName = usr.UserId.ToString() + ".jpg";
                }
            }
            List<string> templates = new List<string>();
            templates.Add(m.MInfo1.Allergies);
            templates.Add(m.MInfo1.Cancer);
            templates.Add(m.MInfo1.Asthma);
            templates.Add(m.MInfo1.LiverDisease);
            templates.Add(m.MInfo1.HeartProblems);

            Diseases s = new Diseases();
            foreach (string t in templates)
            {

                XmlDocument xmldoc = new XmlDocument();
                if (!string.IsNullOrWhiteSpace(t))
                {
                    xmldoc.LoadXml(t);

                    for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
                    {
                        string node = xmldoc.DocumentElement.Name;

                        XmlElement name = xmldoc.DocumentElement.ChildNodes[i]["Name"];
                        if (name != null)
                        {
                            if (node == "Allergies")
                                s.Allergies.Add(name.InnerText);
                            else if (node == "Cancers")
                                s.Cancers.Add(name.InnerText);
                            else if (node == "Asthmas")
                                s.Asthamas.Add(name.InnerText);
                            else if (node == "LiverProblems")
                                s.Liverproblems.Add(name.InnerText);
                            else if (node == "HeartProblems")
                                s.Heartproblems.Add(name.InnerText);
                        }
                    }
                    m.al.Add(s);
                }
            }

            s3.UpdatePersonalInfo(m.PInfo1, usr.UserId);
            string userImage = new Services3().GetPersonalInfo(usr.UserId).ImageName;

            string userImage1 = "https://www.bumpdocs.com/Uploads/" + userImage;

            Session["userimage"] = userImage != "" ? userImage1 : "../../Images/default-user.png";

            return RedirectToAction("PInfo");
            //return View("PInfo", m);

            //RedirectToAction("MyInfo","PersonalInfo");//, "PersonalInfo");
        }

        public ActionResult GetBMIInfoItems(string data)
        {
            Services3 s3 = new Services3();
            //MedicalAndPersonal m2 = new MedicalAndPersonal();
            MedicalInformation m2 = new MedicalInformation();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            //m2.PInfo1 = s3.GetPersonalInfo(usr.UserId);
            //m2.MInfo1 = s3.GetMedicalInfo(usr.UserId);
            m2 = s3.GetMedicalInfo(usr.UserId);

            string FirstVal = null;
            string SecondVal = null;

            if (data == "BMI")
            {
                FirstVal = m2.BMIHeight.ToString();
                SecondVal = m2.BMIWeight.ToString();
            }
            else if (data == "Skin")
            {
                FirstVal = m2.SkinColor;
                SecondVal = m2.SkinType;
            }
            else if (data == "Hair")
            {
                FirstVal = m2.HairColor;
                SecondVal = m2.HairType;
            }
            else if (data == "Eye")
            {
                FirstVal = m2.EyeColor;
                SecondVal = m2.EyeSight;
            }
            else if (data == "Insurance")
            {
                FirstVal = m2.PolicyNumber;
                SecondVal = m2.InsuranceProviderName;
            }



            //m2.Alergies = m2.FillList(data);
            //m2.Height = m3.FillList(data);

            JsonResult jr = new JsonResult();
            //jr.Data = m2.Height ;
            //jr.Data = m2.Weight;
            var success = new { Value1 = FirstVal, Value2 = SecondVal };
            return Json(success);

        }

        int result;
        public ActionResult BtnAddBMI(string value1, string value2, string type)
        {
            Services3 s3 = new Services3();
            //MedicalAndPersonal m2 = new MedicalAndPersonal();
            MedicalInformation m2 = new MedicalInformation();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            //m2.PInfo1 = s3.GetPersonalInfo(usr.UserId);
            //m2.MInfo1 = s3.GetMedicalInfo(usr.UserId);
            m2 = s3.GetMedicalInfo(usr.UserId);
            //int result;

            if (type == "BMI Information")
            {
                if (value1 == "")
                    value1 = "0";
                if (value2 == "")
                    value2 = "0";
                m2.BMIHeight = Convert.ToSingle(value1);
                m2.BMIWeight = Convert.ToSingle(value2);
                if (value1 == "0" || value2 == "0" || value1 == "" || value2 == "")
                {
                    m2.BMIValue = 0;
                }
                else
                {
                    double? ptow = (m2.BMIWeight / 2.2);
                    float BMIValue = (Convert.ToSingle(ptow)) / ((Convert.ToSingle(value1) / 100) * (Convert.ToSingle(value1) / 100));
                    m2.BMIValue = Math.Round(BMIValue, 2);
                }
                result = s3.UpdateMedicalInfo(m2, usr.UserId.ToString(), usr.DomainId.ToString(), "PInfo3", usr.GroupType);
            }
            else if (type == "Skin Information")
            {
                m2.SkinColor = value1;
                m2.SkinType = value2;
                result = s3.UpdateMedicalInfo(m2, usr.UserId.ToString(), usr.DomainId.ToString(), "PInfo3", usr.GroupType);
            }
            else if (type == "Hair Information")
            {
                m2.HairColor = value1;
                m2.HairType = value2;
                result = s3.UpdateMedicalInfo(m2, usr.UserId.ToString(), usr.DomainId.ToString(), "PInfo3", usr.GroupType);
            }
            else if (type == "Irish Information")
            {
                m2.EyeColor = value1;
                m2.EyeSight = value2;
                result = s3.UpdateMedicalInfo(m2, usr.UserId.ToString(), usr.DomainId.ToString(), "PInfo3", usr.GroupType);
            }
            else if (type == "Add Insurance Info")
            {
                m2.PolicyNumber = value1; ;
                m2.InsuranceProviderName = value2;
                result = s3.UpdateMedicalInfo(m2, usr.UserId.ToString(), usr.DomainId.ToString(), "PInfo3", usr.GroupType);
            }

            JsonResult r = new JsonResult();

            if (result == 1010)
            {
                r.Data = result;
                return r;
            }
            else
            {
                r.Data = "Failed";
                return r;
            }

            ////m2.Alergies = m2.FillList(data);
            ////m2.Height = m3.FillList(data);

            //JsonResult jr = new JsonResult();
            ////jr.Data = m2.Height ;
            ////jr.Data = m2.Weight;
            //var success = new { Value1 = m2.Value1, Value2 = m2.Value2 };
            //return Json(success);

        }

        public ActionResult BtnAddBloodInfo(string BloodPressure, string BloodSugarLevel, string BloodGroup)
        {
            Services3 s3 = new Services3();
            //MedicalAndPersonal m2 = new MedicalAndPersonal();
            MedicalInformation m2 = new MedicalInformation();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            m2 = s3.GetMedicalInfo(usr.UserId);


            if (BloodSugarLevel == "")
                BloodSugarLevel = "0";


            m2.BloodPressure = BloodPressure;
            m2.BloodSugarLevel = Convert.ToSingle(BloodSugarLevel);
            m2.BloodGroup = BloodGroup;
            int result = s3.UpdateMedicalInfo(m2, usr.UserId.ToString(), usr.DomainId.ToString(), "PInfo3", usr.GroupType);

            JsonResult r = new JsonResult();

            if (result == 1010)
            {
                r.Data = result;
                return r;
            }
            else
            {
                r.Data = "Failed";
                return r;
            }
        }

        public ActionResult BtnAddEmergencyInfo(string PrimaryContactPersonName, string PrimaryContactNumber, string SecondaryContactPersonName, string SecondaryContactNumber)
        {
            Services3 s3 = new Services3();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            MedicalInformation m2 = new MedicalInformation();

            if (PrimaryContactNumber == "")
                PrimaryContactNumber = "0";
            if (SecondaryContactNumber == "")
                SecondaryContactNumber = "0";

            m2 = s3.GetMedicalInfo(usr.UserId);
            m2.PrimaryContactPersonName = PrimaryContactPersonName;
            m2.PrimaryContactNumber = Convert.ToInt64(PrimaryContactNumber);
            m2.SecondaryContactPersonName = SecondaryContactPersonName;
            m2.SecondaryContactNumber = Convert.ToInt64(SecondaryContactNumber);

            int result = s3.UpdateMedicalInfo(m2, usr.UserId.ToString(), usr.DomainId.ToString(), "PInfo3", usr.GroupType);

            JsonResult r = new JsonResult();

            if (result == 1010)
            {
                r.Data = result;
                return r;
            }
            else
            {
                r.Data = "Failed";
                return r;
            }
        }
        //AD Code for FamilyInfo
        //updated by ck on 24-5-13
        public ActionResult UpdateFamilyInfo(FamilyInfo objFamilyInfo)
        {
            Services3 s3 = new Services3();

            MedicalInformation m2 = new MedicalInformation();
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                usr = Session["LoginUser"] as UserInformation;
            }

            m2 = s3.GetMedicalInfo(usr.UserId);
            //int result;
            m2.FBloodPressure = objFamilyInfo.FBloodPressure;
            m2.FDiabetis = objFamilyInfo.FDiabetis;
            m2.FCancer = objFamilyInfo.FCancer;
            m2.FAcne = objFamilyInfo.FAcne;
            m2.FHeartDiseases = objFamilyInfo.FHeartDiseases;
            m2.FBladness = objFamilyInfo.FBladness;
            m2.FLungDiseases = objFamilyInfo.FLungDiseases;


            result = s3.UpdateMedicalInfo(m2, usr.UserId.ToString(), usr.DomainId.ToString(), "PInfo3", usr.GroupType);

            return RedirectToAction("PInfo");

        }

        public ActionResult LookUpReasons(string term, string name)
        {
            /* static path*/
            string path = ConfigurationManager.AppSettings["MedicalFiles"];
            //string path = HttpContext.Server.MapPath("~/App_Data/Medical Groups/");
            /* static path*/
            if (name == "Allergies")
                path = path+ "Alergies.txt";
            else if (name == "Cancer")
                   path = path+ "Cancertypes.txt";
               
            else if (name == "Asthma")
                 path = path+ "Astma types.txt";
                
            else if (name == "Liver")
                  path = path+ "Liver types.txt";
               
            else if (name == "Heart")
                  path = path+ "Heart types.txt";
             
            else if (name == "Thyroid")
                 path = path+ "Types of thyroid.txt";
               
            else if (name == "Chronic")
                 path = path+ "Chronic Diseases.txt";
               
            else if (name == "Genetic")
                    path = path+ "Genetic disorders.txt";
               
            else if (name == "Vaccination")
                    path = path+ "Imunizations.txt";
               
               
            else if (name == "Genital")
                  path = path+ "Genital infections.txt";
               
            else if (name == "Arthritis")
                path = path + "Arthritis.txt";
               

            TextInfo searchtxt = new CultureInfo("en-US", false).TextInfo;
            List<string> items = System.IO.File
            .ReadAllLines(path)
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
        public ActionResult EddChange2(MedicalAndPersonal m)
        {
            Services1 s1 = new Services1();
            Services3 s3 = new Services3();

            PersonalInformation pInfo = null;
            PersonalInformation objpInfo = null;
            UserInformation uinfo = null;
            string currentpage = "PersonalInfo";
            Session["CurrentPage"] = currentpage;
            if (Session["CurrentUser"] != null)
            {
                usr = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                usr = Session["LoginUser"] as UserInformation;
            }
            m.PInfo1.duedate = Convert.ToDateTime(m.PInfo1.duedat);
            pInfo = m.PInfo1;

          

            int result = s3.UpdateEddDate(pInfo, usr.UserId);
            if (result == 1020)
            {
                UserInformation usrinfo = s1.GetUserbyId(usr.UserId);
                Session["LoginUser"] = usrinfo;
            }

            return RedirectToAction("PInfo");
        }

    }
}
