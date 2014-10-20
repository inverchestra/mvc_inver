using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models
{
    public class MedicalInformation
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
        //AD Ends here


        private string allergies;
        public string Allergies
        {
            get { return allergies; }
            set { allergies = value; }
        }

        private string cancer;
        public string Cancer
        {
            get { return cancer; }
            set { cancer = value; }
        }

        private string asthma;
        public string Asthma
        {
            get { return asthma; }
            set { asthma = value; }
        }

        private string liverDisease;
        public string LiverDisease
        {
            get { return liverDisease; }
            set { liverDisease = value; }
        }

        private string heartProblems;
        public string HeartProblems
        {
            get { return heartProblems; }
            set { heartProblems = value; }
        }

        private string thyroidproblems;
        public string Thyroidproblems
        {
            get { return thyroidproblems; }
            set { thyroidproblems = value; }
        }

        private string chronicDiseases;
        public string ChronicDiseases
        {
            get { return chronicDiseases; }
            set { chronicDiseases = value; }
        }

        private string geneticDisoorder;
        public string GeneticDisoorder
        {
            get { return geneticDisoorder; }
            set { geneticDisoorder = value; }
        }

        private string vaccination;
        public string Vaccination
        {
            get { return vaccination; }
            set { vaccination = value; }
        }

        private string genitalInfections;
        public string GenitalInfections
        {
            get { return genitalInfections; }
            set { genitalInfections = value; }
        }

        private string arthrits;
        public string Arthrits
        {
            get { return arthrits; }
            set { arthrits = value; }
        }

        private string exposuretoTB;
        public string ExposuretoTB
        {
            get { return exposuretoTB; }
            set { exposuretoTB = value; }
        }

        private string operations;
        public string Operations
        {
            get { return operations; }
            set { operations = value; }
        }

        private string backProblems;
        public string BackProblems
        {
            get { return backProblems; }
            set { backProblems = value; }
        }

        private string adverseDrugReaction;
        public string AdverseDrugReaction
        {
            get { return adverseDrugReaction; }
            set { adverseDrugReaction = value; }
        }

        private string syndromes;
        public string Syndromes
        {
            get { return syndromes; }
            set { syndromes = value; }
        }

        private double? bMIHeight;
        public double? BMIHeight
        {
            get { return bMIHeight; }
            set { bMIHeight = value; }
        }

        private double? bMIWeight;
        public double? BMIWeight
        {
            get { return bMIWeight; }
            set { bMIWeight = value; }
        }

        private double? bMIValue; // sandeep added
        public double? BMIValue // sandeep added
        {
            get { return bMIValue; }
            set { bMIValue = value; }
        }

        private string skinColor;
        public string SkinColor
        {
            get { return skinColor; }
            set { skinColor = value; }
        }

        private string skinType;
        public string SkinType
        {
            get { return skinType; }
            set { skinType = value; }
        }

        private string hairColor;
        public string HairColor
        {
            get { return hairColor; }
            set { hairColor = value; }
        }

        private string hairType;
        public string HairType
        {
            get { return hairType; }
            set { hairType = value; }
        }

        private string eyeColor;
        public string EyeColor
        {
            get { return eyeColor; }
            set { eyeColor = value; }
        }

        private string eyeSight;
        public string EyeSight
        {
            get { return eyeSight; }
            set { eyeSight = value; }
        }
        private string leftEye;
        public string LeftEye
        {
            get { return leftEye; }
            set { leftEye = value; }
        }

        private string rightEye;
        public string RightEye
        {
            get { return rightEye; }
            set { rightEye = value; }
        }

        private string bloodPressure;
        public string BloodPressure
        {
            get { return bloodPressure; }
            set { bloodPressure = value; }
        }

        private string systole;
        public string Systole
        {
            get { return systole; }
            set { systole = value; }
        }

        private string diastole;
        public string Diastole
        {
            get { return diastole; }
            set { diastole = value; }
        }
        private float? bloodSugarLevel;
        public float? BloodSugarLevel
        {
            get { return bloodSugarLevel; }
            set { bloodSugarLevel = value; }
        }

        private string bloodGroup;
        public string BloodGroup
        {
            get { return bloodGroup; }
            set { bloodGroup = value; }
        }

        private string primaryContactPersonName;
        public string PrimaryContactPersonName
        {
            get { return primaryContactPersonName; }
            set { primaryContactPersonName = value; }
        }

        private long? primaryContactNumber;
        public long? PrimaryContactNumber
        {
            get { return primaryContactNumber; }
            set { primaryContactNumber = value; }
        }

        private string secondaryContactPersonName;
        public string SecondaryContactPersonName
        {
            get { return secondaryContactPersonName; }
            set { secondaryContactPersonName = value; }
        }

        private long? secondaryContactNumber;
        public long? SecondaryContactNumber
        {
            get { return secondaryContactNumber; }
            set { secondaryContactNumber = value; }
        }

        private string policyNumber;
        public string PolicyNumber
        {
            get { return policyNumber; }
            set { policyNumber = value; }
        }

        private string insuranceProviderName;
        public string InsuranceProviderName
        {
            get { return insuranceProviderName; }
            set { insuranceProviderName = value; }
        }

        private DateTime? updatedOn;
        public DateTime? UpdatedOn
        {
            get { return updatedOn; }
            set { updatedOn = value; }
        }

        private string updatedBy;
        public string UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        private string user;
        public string UserId
        {
            get { return user; }
            set { user = value; }
        }

        private string domainUser;
        public string DomainID
        {
            get { return domainUser; }
            set { domainUser = value; }
        }

        private string iD;
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private MedicalInformation mInfo;
        public MedicalInformation MInfo1
        {
            get { return mInfo; }
            set { mInfo = value; }
        }
        //adde by cs
        private string createdBy;

        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        //updated by ck

        //public List<SelectListItem> Alergies { get; set; }
        public int SelectedState { set; get; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        //end






    }


    public class Alergies
    {
        public static List<string> GetAlergiesList()
        {
            //@"C:\Users\Meera Devi\Desktop\iHealthDC(29-03-13)\countrylist.txt"; 
            string path = @"C:\Users\Meera Devi\Desktop\iHealthDC(29-03-13)\countrylist.txt"; //@"C:\InndocsiHealth\Medical Groups\Alergies.txt";
            List<string> aList = new List<string>();
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    aList.Add(line);
                }
            }
            return aList;
        }
    }


}