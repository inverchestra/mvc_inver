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

namespace InnDocs.iHealth.Web.UI.Areas.DoulaBumpUser.Controllers
{
    [SessionExpireFilter]
    public class DoulaPersonalController : Controller
    {
        //
        // GET: /DoulaBumpUser/DoulaPersonal/
        UserInformation usr = null;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DoulaPInfo()
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
            m.PInfo1.duedat = d.ToString("dd MMM yyyy"); // sandeep added on 14-4-2014
            DateTime date = Convert.ToDateTime(m.PInfo1.DateOfBirth); // sandeep added on 14-4-2014
            m.PInfo1.dateofbir = date.ToString("dd MMM yyyy"); // sandeep added on 14-4-2014

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
            return View("DoulaPInfo", m);
        }
    }
}
