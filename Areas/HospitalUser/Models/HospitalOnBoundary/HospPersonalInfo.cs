using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class HospPersonalInfo
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private DateTime? dateOfBirth;
        public DateTime? DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
        public string dateOfBirth1
        {
            get
            {
                if (DateOfBirth != null)
                    return DateOfBirth.Value.ToShortDateString();
                return null;
            }
            set { }
        }
        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private string State;
        public string state
        {
            get { return State; }
            set { State = value; }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        //private double contact;
        //public double Contact
        //{
        //    get { return contact; }
        //    set { contact = value; }
        //}

        //private double emergency;
        //public double Emergency
        //{
        //    get { return emergency; }
        //    set { emergency = value; }
        //}
       
        public long? Emergency { get; set; }

        private long? workTelNumber;
        public long? WorkTelNumber
        {
            get { return workTelNumber; }
            set { workTelNumber = value; }
        }

        private long? mobileNumber;
        public long? MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; }
        }

        private string religion;
        public string Religion
        {
            get { return religion; }
            set { religion = value; }
        }

        private string street;
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        private string town;
        public string Town
        {
            get { return town; }
            set { town = value; }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private int zipCode;
        public int ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        private double wristSize;
        public double WristSize
        {
            get { return wristSize; }
            set { wristSize = value; }
        }

        private double cholesterolValues;
        public double CholesterolValues
        {
            get { return cholesterolValues; }
            set { cholesterolValues = value; }
        }

        private string priorSurgery;
        public string PriorSurgery
        {
            get { return priorSurgery; }
            set { priorSurgery = value; }
        }

        private string adverseDrugReaction;
        public string AdverseDrugReaction
        {
            get { return adverseDrugReaction; }
            set { adverseDrugReaction = value; }
        }

        private bool migraine;
        public bool Migraine
        {
            get { return migraine; }
            set { migraine = value; }
        }

        private bool circumcision;
        public bool Circumcision
        {
            get { return circumcision; }
            set { circumcision = value; }
        }

        private bool smoking;
        public bool Smoking
        {
            get { return smoking; }
            set { smoking = value; }
        }

        private bool srugs;
        public bool Srugs
        {
            get { return srugs; }
            set { srugs = value; }
        }

        private bool fertilityProblems;
        public bool FertilityProblems
        {
            get { return fertilityProblems; }
            set { fertilityProblems = value; }
        }

        private bool bloodTransfusions;
        public bool BloodTransfusions
        {
            get { return bloodTransfusions; }
            set { bloodTransfusions = value; }
        }

        private bool isIndexed;
        public bool IsIndexed
        {
            get { return isIndexed; }
            set { isIndexed = value; }
        }

        private DateTime? UpdatedOn;
        public DateTime? UpdatedOn1
        {
            get { return UpdatedOn; }
            set { UpdatedOn = value; }
        }

        private int updatedBy;
        public int UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private int domainID;
        public int DomainID
        {
            get { return domainID; }
            set { domainID = value; }
        }

        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        //Added For HospitalInfo
        private int doctorId;
        public int DoctorId
        {
            get { return doctorId;}
            set { doctorId = value; }
        }
        //end Here

        private string originalName;
        public string OriginalName
        {
            get { return originalName; }
            set { originalName = value; }
        }

        private string imageName;
        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }
       
        public long? Contact { get; set; }
    }
}