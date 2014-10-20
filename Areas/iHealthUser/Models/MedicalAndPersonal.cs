using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models
{
    public class MedicalAndPersonal
    {
        //updated familyinfo by ck
        //private bool fbloodPressure;

        //public bool FBloodPressure
        //{
        //    get { return fbloodPressure; }
        //    set { fbloodPressure = value; }
        //}
        //private bool fdiabetis;

        //public bool FDiabetis
        //{
        //    get { return fdiabetis; }
        //    set { fdiabetis = value; }
        //}
        //private bool fheartDiseases;

        //public bool FHeartDiseases
        //{
        //    get { return fheartDiseases; }
        //    set { fheartDiseases = value; }
        //}
        //private bool flungDiseases;

        //public bool FLungDiseases
        //{
        //    get { return flungDiseases; }
        //    set { flungDiseases = value; }
        //}
        //private bool fbladness;

        //public bool FBladness
        //{
        //    get { return fbladness; }
        //    set { fbladness = value; }
        //}
        //private bool facne;

        //public bool FAcne
        //{
        //    get { return facne; }
        //    set { facne = value; }
        //}
        //private bool fcancer;

        //public bool FCancer
        //{
        //    get { return fcancer; }
        //    set { fcancer = value; }
        //}

        //end of family info

        //updated by ck for blood info
        //private string bloodPressure;
        //public string BloodPressure
        //{
        //    get { return bloodPressure; }
        //    set { bloodPressure = value; }
        //}

        //private float bloodSugarLevel;
        //public float BloodSugarLevel
        //{
        //    get { return bloodSugarLevel; }
        //    set { bloodSugarLevel = value; }
        //}

        //private string bloodGroup;
        //public string BloodGroup
        //{
        //    get { return bloodGroup; }
        //    set { bloodGroup = value; }
        //}
        //end of blood info

        private PersonalInformation pInfo;
        public PersonalInformation PInfo1
        {
            get { return pInfo; }
            set { pInfo = value; }
        }

        private MedicalInformation mInfo;
        public MedicalInformation MInfo1
        {
            get { return mInfo; }
            set { mInfo = value; }
        }
        private Result result;
        public Result ResultInfo
        {
            get { return result; }
            set { result = value; }
        }
        public List<Diseases> al { get; set; }
        public List<SelectListItem> Alergies { get; set; }
        public int SelectedState { set; get; }
        public List<SelectListItem> States { get; set; }


        public List<SelectListItem> CountryList { get; set; }

        //added by ck 
        //private string skinColor;
        //public string SkinColor
        //{
        //    get { return skinColor; }
        //    set { skinColor = value; }
        //}

        //private string skinType;
        //public string SkinType
        //{
        //    get { return skinType; }
        //    set { skinType = value; }
        //}

        //private string hairColor;
        //public string HairColor
        //{
        //    get { return hairColor; }
        //    set { hairColor = value; }
        //}

        //private string hairType;
        //public string HairType
        //{
        //    get { return hairType; }
        //    set { hairType = value; }
        //}

        //private string eyeColor;
        //public string EyeColor
        //{
        //    get { return eyeColor; }
        //    set { eyeColor = value; }
        //}

        //private string eyeSight;
        //public string EyeSight
        //{
        //    get { return eyeSight; }
        //    set { eyeSight = value; }
        //}

        ////end by ck

        ////updated bmi by ck
        //private int bMIHeight;
        //public int BMIHeight
        //{
        //    get { return bMIHeight; }
        //    set { bMIHeight = value; }
        //}

        //private int bMIWeight;
        //public int BMIWeight
        //{
        //    get { return bMIWeight; }
        //    set { bMIWeight = value; }
        //}

        //private double bMIValue; // sandeep added
        //public double BMIValue // sandeep added
        //{
        //    get { return bMIValue; }
        //    set { bMIValue = value; }
        //}


        public MedicalAndPersonal()
        {
            Alergies = new List<SelectListItem>();

            al = new List<Diseases>();

            CountryList = new List<SelectListItem>();
            string path = ConfigurationManager.AppSettings["CountryList"];
            /* static path*/
            //string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Countrylist/countrylist.txt");
           // string path = @"C:\InndocsiHealth\Countrylist\countrylist.txt";
            /* static path*/
            List<string> aList = new List<string>();
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    aList.Add(line);
                }

                CountryList = new List<SelectListItem>();
                foreach (string a in aList)
                {
                    CountryList.Add(new SelectListItem() { Text = a, Value = a, Selected = false });

                }
            }



        }
        public List<SelectListItem> FillList(string s)
        {
            /* static path*/
            string path = ConfigurationManager.AppSettings["MedicalFiles"];
           // string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Medical Groups/");

            /* static path*/
            //////////// Datacenter Machine /////////////
            if (s == "Allergies")
                 path = path + "Alergies.txt";
            
            else if (s == "Cancer")
                path = path + "Cancertypes.txt";

            else if (s == "Asthma")
                  path = path + "Astma types.txt";
            else if (s == "Liver")
                path = path + "Liver types.txt";
            else if (s == "Heart")
                 path = path + "Heart types.txt";
            else if (s == "Thyroid")
                path = path + "Types of thyroid.txt";
            else if (s == "Chronic")
                 path = path + "Chronic Diseases.txt";
            else if (s == "Genetic")
                 path = path + "Genetic disorders.txt";
            else if (s == "Vaccination")
               path = path + "Imunizations.txt";

            else if (s == "Genital")
                  path = path + "Genital infections.txt";
                
            else if (s == "Arthritis")
                 path = path + "Arthritis.txt";

            List<string> aList = new List<string>();
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    aList.Add(line);
                }

            }
            Alergies = new List<SelectListItem>();
            foreach (string a in aList)
            {

                Alergies.Add(new SelectListItem() { Text = a, Value = a, Selected = false });
            }

            return Alergies;
        }

    }

    public class Result
    {
        private string reaction;

        public string Reaction
        {
            get { return reaction; }
            set { reaction = value; }
        }
        private string severity;

        public string Severity
        {
            get { return severity; }
            set { severity = value; }
        }
        private string provider;

        public string Provider
        {
            get { return provider; }
            set { provider = value; }
        }
        private string notes;

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
    }

    public class Diseases
    {
        private List<string> allergies = new List<string>();

        public List<string> Allergies
        {
            get { return allergies; }
            set { allergies = value; }
        }
        private List<string> cancers = new List<string>();

        public List<string> Cancers
        {
            get { return cancers; }
            set { cancers = value; }
        }
        private List<string> asthamas = new List<string>();

        public List<string> Asthamas
        {
            get { return asthamas; }
            set { asthamas = value; }
        }
        private List<string> liverproblems = new List<string>();

        public List<string> Liverproblems
        {
            get { return liverproblems; }
            set { liverproblems = value; }
        }
        private List<string> heartproblems = new List<string>();

        public List<string> Heartproblems
        {
            get { return heartproblems; }
            set { heartproblems = value; }
        }
        private List<string> thyroidproblems = new List<string>();
        public List<string> ThyroidProblems
        {
            get { return thyroidproblems; }
            set { thyroidproblems = value; }
        }
        private List<string> chronicDiseases = new List<string>();

        public List<string> ChronicDiseases
        {
            get { return chronicDiseases; }
            set { chronicDiseases = value; }
        }
        private List<string> geneticDisorders = new List<string>();

        public List<string> GeneticDisorders
        {
            get { return geneticDisorders; }
            set { geneticDisorders = value; }
        }
        private List<string> vaccination = new List<string>();

        public List<string> Vaccination
        {
            get { return vaccination; }
            set { vaccination = value; }
        }
        private List<string> genitalInfections = new List<string>();

        public List<string> GenitalInfections
        {
            get { return genitalInfections; }
            set { genitalInfections = value; }
        }
        private List<string> arthritis = new List<string>();

        public List<string> Arthritis
        {
            get { return arthritis; }
            set { arthritis = value; }
        }
    }

    public class FamilyInfo
    {

        //AD Code for Family Info
        private bool fbloodPressure;

        public bool FBloodPressure
        {
            get { return fbloodPressure; }
            set { fbloodPressure = value; }
        }
        private bool fdiabetis;

        public bool FDiabetis
        {
            get { return fdiabetis; }
            set { fdiabetis = value; }
        }
        private bool fheartDiseases;

        public bool FHeartDiseases
        {
            get { return fheartDiseases; }
            set { fheartDiseases = value; }
        }
        private bool flungDiseases;

        public bool FLungDiseases
        {
            get { return flungDiseases; }
            set { flungDiseases = value; }
        }
        private bool fbladness;

        public bool FBladness
        {
            get { return fbladness; }
            set { fbladness = value; }
        }
        private bool facne;

        public bool FAcne
        {
            get { return facne; }
            set { facne = value; }
        }
        private bool fcancer;

        public bool FCancer
        {
            get { return fcancer; }
            set { fcancer = value; }
        }
        //AD End here
    }


}