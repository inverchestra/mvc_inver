using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{

    public class UltraScans
    {
        public string ScanType { get; set; }
        public List<SelectListItem> ScanTypeList { get; set; }

        public List<SelectListItem> ScanTypes()
        {
            ScanTypeList = new List<SelectListItem>();
            ScanTypeList.Add(new SelectListItem() { Text = "PleaseSelect", Value = "" });
            ScanTypeList.Add(new SelectListItem() { Text = "EarlyStage", Value = "EarlyStage", Selected = false });
            ScanTypeList.Add(new SelectListItem() { Text = "NTScan", Value = "NTScan", Selected = false });
            ScanTypeList.Add(new SelectListItem() { Text = "AnamolyScan", Value = "AnamolyScan", Selected = false });
            ScanTypeList.Add(new SelectListItem() { Text = "Growth", Value = "Growth", Selected = false });
            return ScanTypeList;
        }

    }
    public class EarlyFindings
    {
        private string crl;

        public string CRL
        {
            get { return crl; }
            set { crl = value; }
        }
        private string fhs;

        public string FHS
        {
            get { return fhs; }
            set { fhs = value; }
        }
        private string yolkSac;

        public string YolkSac
        {
            get { return yolkSac; }
            set { yolkSac = value; }
        }
        private string adnexae;

        public string Adnexae
        {
            get { return adnexae; }
            set { adnexae = value; }
        }
        private string gestationalsac;

        public string Gestationalsac
        {
            get { return gestationalsac; }
            set { gestationalsac = value; }
        }
    }
    public class EarlyScan
    {
        private string earlyScanId;
        public string EarlyScanId
        {
            get { return earlyScanId; }
            set { earlyScanId = value; }
        }
        private string patientId;

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        private string patientName;

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }
        private DateTime? scanDate;

        public DateTime? ScanDate
        {
            get { return scanDate; }
            set { scanDate = value; }
        }
        private string reffDoctor;

        public string ReffDoctor
        {
            get { return reffDoctor; }
            set { reffDoctor = value; }
        }
        private string maternalHistory;

        public string MaternalHistory
        {
            get { return maternalHistory; }
            set { maternalHistory = value; }
        }
        private string gestation;

        public string Gestation
        {
            get { return gestation; }
            set { gestation = value; }
        }
        private string eDD;

        public string EDD
        {
            get { return eDD; }
            set { eDD = value; }
        }

        private Gestation gestinat;

        public Gestation Gestinat
        {
            get { return gestinat; }
            set { gestinat = value; }
        }

        private EDD eDDvalue;

        public EDD EDDvalue
        {
            get { return eDDvalue; }
            set { eDDvalue = value; }
        }
        private string indications;

        public string Indications
        {
            get { return indications; }
            set { indications = value; }
        }
        private string earlyFindings;

        public string EarlyFindings
        {
            get { return earlyFindings; }
            set { earlyFindings = value; }
        }

        private EarlyFindings findings;

        public EarlyFindings Findings
        {
            get { return findings; }
            set { findings = value; }
        }
        private string impression;

        public string Impression
        {
            get { return impression; }
            set { impression = value; }
        }
        private string radiologist;

        public string Radiologist
        {
            get { return radiologist; }
            set { radiologist = value; }
        }
        private string recomandations;

        public string Recomandations
        {
            get { return recomandations; }
            set { recomandations = value; }
        }
        private string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private DateTime? createdOn;

        public DateTime? CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
        private DateTime? scanOnDate;//ck added

        public DateTime? ScanOnDate
        {
            get { return scanOnDate; }
            set { scanOnDate = value; }

        }
        public string ScanDate1
        {
            get;
            set;
        }


    }

    public class NTScan
    {
        private string ntScanId;
        public string NtScanId
        {
            get { return ntScanId; }
            set { ntScanId = value; }
        }
        private string patientId;

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        private string patientName;

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }
        private DateTime? scanDate;

        public DateTime? ScanDate
        {
            get { return scanDate; }
            set { scanDate = value; }
        }
        private string reffDoctor;

        public string ReffDoctor
        {
            get { return reffDoctor; }
            set { reffDoctor = value; }
        }
        private MaternalHistory maternalHistory1;

        public MaternalHistory MaternalHistory1
        {
            get { return maternalHistory1; }
            set { maternalHistory1 = value; }
        }

        private string maternalHistory;

        public string MaternalHistory
        {
            get { return maternalHistory; }
            set { maternalHistory = value; }
        }
        private string gestation;

        public string Gestation
        {
            get { return gestation; }
            set { gestation = value; }
        }
        private string eDD;

        public string EDD
        {
            get { return eDD; }
            set { eDD = value; }
        }
        private string indications;

        public string Indications
        {
            get { return indications; }
            set { indications = value; }
        }

        private Gestation gestinat;

        public Gestation Gestinat
        {
            get { return gestinat; }
            set { gestinat = value; }
        }

        private EDD eDDvalue;

        public EDD EDDvalue
        {
            get { return eDDvalue; }
            set { eDDvalue = value; }
        }

        private string usFinds;

        public string UsFinds
        {
            get { return usFinds; }
            set { usFinds = value; }
        }

        private FirstTrimeterUSSFindings fTUSSFindings;

        public FirstTrimeterUSSFindings FTUSSFindings
        {
            get { return fTUSSFindings; }
            set { fTUSSFindings = value; }
        }

        private string chromosomalMark;

        public string ChromosomalMark
        {
            get { return chromosomalMark; }
            set { chromosomalMark = value; }
        }

        private Chromosomalmarkers cHSmarkers;

        public Chromosomalmarkers CHSmarkers
        {
            get { return cHSmarkers; }
            set { cHSmarkers = value; }
        }

        private string fetalAntomy;

        public string FetalAntomy
        {
            get { return fetalAntomy; }
            set { fetalAntomy = value; }
        }

        private FetalAnatomy fetalAnatomy1;

        public FetalAnatomy FetalAnatomy1
        {
            get { return fetalAnatomy1; }
            set { fetalAnatomy1 = value; }
        }
        private string maternalStructure;

        public string MaternalStructure
        {
            get { return maternalStructure; }
            set { maternalStructure = value; }
        }


        private MaternalStructures maternalStructures1;

        public MaternalStructures MaternalStructures1
        {
            get { return maternalStructures1; }
            set { maternalStructures1 = value; }
        }
        private string uterineDoppler;

        public string UterineDoppler
        {
            get { return uterineDoppler; }
            set { uterineDoppler = value; }
        }


        private UterineDoppler uterineDoppler1;

        public UterineDoppler UterineDoppler1
        {
            get { return uterineDoppler1; }
            set { uterineDoppler1 = value; }
        }
        private string impression;

        public string Impression
        {
            get { return impression; }
            set { impression = value; }
        }
        private string radiologist;

        public string Radiologist
        {
            get { return radiologist; }
            set { radiologist = value; }
        }
        private string recomandations;

        public string Recomandations
        {
            get { return recomandations; }
            set { recomandations = value; }
        }
        private string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private DateTime? createdOn;

        public DateTime? CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
        private DateTime? scanOnDate;//ck added

        public DateTime? ScanOnDate
        {
            get { return scanOnDate; }
            set { scanOnDate = value; }

        }
        public string ScanDate1
        {
            get;
            set;
        }


    }

    public class MaternalHistory
    {
        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        private float height;

        public float Height
        {
            get { return height; }
            set { height = value; }
        }
        private float weight;

        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        private float bmI;

        public float BmI
        {
            get { return bmI; }
            set { bmI = value; }
        }
        private string bloodGroup;

        public string BloodGroup
        {
            get { return bloodGroup; }
            set { bloodGroup = value; }
        }
        private DateTime? lastPeriod;

        public DateTime? LastPeriod
        {
            get { return lastPeriod; }
            set { lastPeriod = value; }
        }
    }

    public class Gestation
    {
        private string byUltra;

        public string ByUltra
        {
            get { return byUltra; }
            set { byUltra = value; }
        }
        private string byDates;

        public string ByDates
        {
            get { return byDates; }
            set { byDates = value; }
        }
    }
    public class EDD
    {
        private DateTime? byUltra;

        public DateTime? ByUltra
        {
            get { return byUltra; }
            set { byUltra = value; }
        }
        private DateTime? byDates;

        public DateTime? ByDates
        {
            get { return byDates; }
            set { byDates = value; }
        }
        // sandeep added start here on 3-4-2014
        public string ByUltra1
        {
            get;
            set;
        }

        public string ByDates1
        {
            get;
            set;
        }
        // sandeep added end here on 3-4-2014

    }
    public class FirstTrimeterUSSFindings
    {
        private string numberofFetuses;
        public string NumberofFetuses
        {
            get { return numberofFetuses; }
            set { numberofFetuses = value; }
        }
        private string ac;

        public string Ac
        {
            get { return ac; }
            set { ac = value; }
        }
        private string fl;

        public string Fl
        {
            get { return fl; }
            set { fl = value; }
        }


        private string efw;
        public string Efw
        {
            get { return efw; }
            set { efw = value; }
        }
        private string fhr;

        public string FHR
        {
            get { return fhr; }
            set { fhr = value; }
        }
        private string crl;

        public string CRL
        {
            get { return crl; }
            set { crl = value; }
        }
        private string nT;

        public string NT
        {
            get { return nT; }
            set { nT = value; }
        }
        private string bPD;

        public string BPD
        {
            get { return bPD; }
            set { bPD = value; }
        }
        private string hc;

        public string HC
        {
            get { return hc; }
            set { hc = value; }
        }
        private string placenta;

        public string Placenta
        {
            get { return placenta; }
            set { placenta = value; }
        }
        private string amnioticfluid;

        public string Amnioticfluid
        {
            get { return amnioticfluid; }
            set { amnioticfluid = value; }
        }
        private string cord;

        public string Cord
        {
            get { return cord; }
            set { cord = value; }
        }
        private string movenents;

        public string Movenents
        {
            get { return movenents; }
            set { movenents = value; }
        }

        private string presentation;

        public string Presentation
        {
            get { return presentation; }
            set { presentation = value; }
        }
        private string placentaGrade;

        public string PlacentaGrade
        {
            get { return placentaGrade; }
            set { placentaGrade = value; }
        }


    }
    public class FetalAnatomy
    {
        private string aVH;

        public string AVH
        {
            get { return aVH; }
            set { aVH = value; }
        }

        private string pVH;

        public string PVH
        {
            get { return pVH; }
            set { pVH = value; }
        }


        private string skull;

        public string Skull
        {
            get { return skull; }
            set { skull = value; }
        }
        private string spine;

        public string Spine
        {
            get { return spine; }
            set { spine = value; }
        }
        private string heart;

        public string Heart
        {
            get { return heart; }
            set { heart = value; }
        }
        private string abdomen;

        public string Abdomen
        {
            get { return abdomen; }
            set { abdomen = value; }
        }
        private string stomoch;

        public string Stomoch
        {
            get { return stomoch; }
            set { stomoch = value; }
        }
        private string bladder;

        public string Bladder
        {
            get { return bladder; }
            set { bladder = value; }
        }
        private string hands;

        public string Hands
        {
            get { return hands; }
            set { hands = value; }
        }
        private string feet;

        public string Feet
        {
            get { return feet; }
            set { feet = value; }
        }

        private string glt;

        public string Glt
        {
            get { return glt; }
            set { glt = value; }
        }
        private string urinTract;

        public string UrinTract
        {
            get { return urinTract; }
            set { urinTract = value; }
        }

        private string extremities;

        public string Extremities
        {
            get { return extremities; }
            set { extremities = value; }
        }

        private string genetilia;

        public string Genetilia
        {
            get { return genetilia; }
            set { genetilia = value; }
        }

        private string brain;

        public string Brain
        {
            get { return brain; }
            set { brain = value; }
        }
        private string thorax;

        public string Thorax
        {
            get { return thorax; }
            set { thorax = value; }
        }
    }

    public class Cervicalassessment
    {
        private string cervixlength;

        public string Cervixlength
        {
            get { return cervixlength; }
            set { cervixlength = value; }
        }
        private Boolean funnelling;

        public Boolean Funnelling
        {
            get { return funnelling; }
            set { funnelling = value; }
        }
        private string cervicalComment;

        public string CervicalComment
        {
            get { return cervicalComment; }
            set { cervicalComment = value; }
        }

    }
    public class Chromosomalmarkers
    {
        private string nasalbone;

        public string Nasalbone
        {
            get { return nasalbone; }
            set { nasalbone = value; }
        }
        private string facialangle;

        public string Facialangle
        {
            get { return facialangle; }
            set { facialangle = value; }
        }
        private string tricuspidDoppler;

        public string TricuspidDoppler
        {
            get { return tricuspidDoppler; }
            set { tricuspidDoppler = value; }
        }
        private string ductusvenosusDoppler;

        public string DuctusvenosusDoppler
        {
            get { return ductusvenosusDoppler; }
            set { ductusvenosusDoppler = value; }
        }
    }
    public class UterineDoppler
    {
        private Boolean patientmotherhadpet;

        public Boolean Patientmotherhadpet
        {
            get { return patientmotherhadpet; }
            set { patientmotherhadpet = value; }
        }
        private string piLeft;

        public string PiLeft
        {
            get { return piLeft; }
            set { piLeft = value; }
        }
        private string piright;

        public string Piright
        {
            get { return piright; }
            set { piright = value; }
        }
        private string lowestPI;

        public string LowestPI
        {
            get { return lowestPI; }
            set { lowestPI = value; }
        }

    }

    public class umbillicalartery
    {
        private string pI;

        public string PI
        {
            get { return pI; }
            set { pI = value; }
        }
        private string rI;

        public string RI
        {
            get { return rI; }
            set { rI = value; }
        }
        private string eDF;

        public string EDF
        {
            get { return eDF; }
            set { eDF = value; }
        }

    }
    public class middlecerbleartery
    {
        private string mPI;

        public string MPI
        {
            get { return mPI; }
            set { mPI = value; }
        }
        private string pSV;

        public string PSV
        {
            get { return pSV; }
            set { pSV = value; }
        }
    }
    public class Ductusvenosus
    {

        private string aWare;

        public string AWare
        {
            get { return aWare; }
            set { aWare = value; }
        }
        private string pIV;

        public string PIV
        {
            get { return pIV; }
            set { pIV = value; }
        }
    }

    public class MaternalStructures
    {
        private string rightovary;

        public string Rightovary
        {
            get { return rightovary; }
            set { rightovary = value; }
        }
        private string leftovary;

        public string Leftovary
        {
            get { return leftovary; }
            set { leftovary = value; }
        }
    }

    //public class NTScanFinds
    //{
    //    private int numberofFetuses;

    //    public int NumberofFetuses
    //    {
    //        get { return numberofFetuses; }
    //        set { numberofFetuses = value; }
    //    }
    //    private int efw;

    //    public int Efw
    //    {
    //        get { return efw; }
    //        set { efw = value; }
    //    }
    //    private float crl;

    //    public float CRL
    //    {
    //        get { return crl; }
    //        set { crl = value; }
    //    }
    //    private float nT;

    //    public float NT
    //    {
    //        get { return nT; }
    //        set { nT = value; }
    //    }
    //    private float bPD;

    //    public float BPD
    //    {
    //        get { return bPD; }
    //        set { bPD = value; }
    //    }
    //    private float hc;

    //    public float HC
    //    {
    //        get { return hc; }
    //        set { hc = value; }
    //    }
    //    private string placenta;

    //    public string Placenta
    //    {
    //        get { return placenta; }
    //        set { placenta = value; }
    //    }
    //    private string amnioticfluid;

    //    public string Amnioticfluid
    //    {
    //        get { return amnioticfluid; }
    //        set { amnioticfluid = value; }
    //    }
    //    private string movenents;

    //    public string Movenents
    //    {
    //        get { return movenents; }
    //        set { movenents = value; }
    //    }


    //}




    public class AnomalyScan
    {
        private string anomalyScanId;
        public string anomalyId
        {
            get { return anomalyScanId; }
            set { anomalyScanId = value; }
        }

        private string patientId;
        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        private string patientName;
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        private DateTime? scanDate;
        public DateTime? ScanDate
        {
            get { return scanDate; }
            set { scanDate = value; }
        }

        private string reffDoctor;
        public string ReffDoctor
        {
            get { return reffDoctor; }
            set { reffDoctor = value; }
        }

        private MaternalHistory maternalHistory1;
        public MaternalHistory MaternalHistory1
        {
            get { return maternalHistory1; }
            set { maternalHistory1 = value; }
        }

        private string maternalHistory;
        public string MaternalHistory
        {
            get { return maternalHistory; }
            set { maternalHistory = value; }
        }

        private string gestation;
        public string Gestation
        {
            get { return gestation; }
            set { gestation = value; }
        }

        private string eDD;
        public string EDD
        {
            get { return eDD; }
            set { eDD = value; }
        }

        private string indications;
        public string Indications
        {
            get { return indications; }
            set { indications = value; }
        }

        private Gestation gestinat;
        public Gestation Gestinat
        {
            get { return gestinat; }
            set { gestinat = value; }
        }

        private EDD eDDvalue;
        public EDD EDDvalue
        {
            get { return eDDvalue; }
            set { eDDvalue = value; }
        }

        private string usFinds;
        public string UsFinds
        {
            get { return usFinds; }
            set { usFinds = value; }
        }

        private FirstTrimeterUSSFindings nTScanFinds;
        public FirstTrimeterUSSFindings NTScanFinds
        {
            get { return nTScanFinds; }
            set { nTScanFinds = value; }
        }

        private Cervicalassessment cervicalassesment;
        public Cervicalassessment Cervicalassesment
        {
            get { return cervicalassesment; }
            set { cervicalassesment = value; }
        }

        private string cervicalAssessment;
        public string CervicalAssessment
        {
            get { return cervicalAssessment; }
            set { cervicalAssessment = value; }
        }

        private string fetalAntomy;
        public string FetalAntomy
        {
            get { return fetalAntomy; }
            set { fetalAntomy = value; }
        }

        private FetalAnatomy fetalAnatomy1;
        public FetalAnatomy FetalAnatomy1
        {
            get { return fetalAnatomy1; }
            set { fetalAnatomy1 = value; }
        }

        private string uterineDoppler;
        public string UterineDoppler
        {
            get { return uterineDoppler; }
            set { uterineDoppler = value; }
        }

        private UterineDoppler uterineDoppler1;
        public UterineDoppler UterineDoppler1
        {
            get { return uterineDoppler1; }
            set { uterineDoppler1 = value; }
        }

        private string impression;
        public string Impression
        {
            get { return impression; }
            set { impression = value; }
        }

        private string radiologist;
        public string Radiologist
        {
            get { return radiologist; }
            set { radiologist = value; }
        }

        private string recomandations;
        public string Recomandations
        {
            get { return recomandations; }
            set { recomandations = value; }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private DateTime? createdOn;
        public DateTime? CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
        private DateTime? scanOnDate;//ck added

        public DateTime? ScanOnDate
        {
            get { return scanOnDate; }
            set { scanOnDate = value; }
        }

        public string ScanDate1
        {
            get;
            set;
        }
    }

    public class GrowthScan
    {
        private string growthScanId;
        public string GrowthScanId
        {
            get { return growthScanId; }
            set { growthScanId = value; }
        }

        private string patientId;
        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        private string patientName;
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        private DateTime? scanDate;
        public DateTime? ScanOnDate
        {
            get { return scanDate; }
            set { scanDate = value; }
        }

        private string reffDoctor;
        public string ReffDoctor
        {
            get { return reffDoctor; }
            set { reffDoctor = value; }
        }

        private MaternalHistory maternalHistory1;
        public MaternalHistory MaternalHistory1
        {
            get { return maternalHistory1; }
            set { maternalHistory1 = value; }
        }

        private string maternalHistory;
        public string MaternalHistory
        {
            get { return maternalHistory; }
            set { maternalHistory = value; }
        }

        private string gestation;
        public string Gestation
        {
            get { return gestation; }
            set { gestation = value; }
        }

        private string eDD;
        public string EDD
        {
            get { return eDD; }
            set { eDD = value; }
        }

        private string indications;
        public string Indication
        {
            get { return indications; }
            set { indications = value; }
        }

        private Gestation gestinat;
        public Gestation Gestinat
        {
            get { return gestinat; }
            set { gestinat = value; }
        }

        private EDD eDDvalue;
        public EDD EDDvalue
        {
            get { return eDDvalue; }
            set { eDDvalue = value; }
        }

        private string usFinds;
        public string UsFinds
        {
            get { return usFinds; }
            set { usFinds = value; }
        }

        private FirstTrimeterUSSFindings nTScanFinds;
        public FirstTrimeterUSSFindings NTScanFinds
        {
            get { return nTScanFinds; }
            set { nTScanFinds = value; }
        }



        private string fetalAntomy;
        public string FetalAntomy
        {
            get { return fetalAntomy; }
            set { fetalAntomy = value; }
        }

        private FetalAnatomy fetalAnatomy1;
        public FetalAnatomy FetalAnatomy1
        {
            get { return fetalAnatomy1; }
            set { fetalAnatomy1 = value; }
        }

        private string middlecerebralartery;

        public string Middlecerebralartery
        {
            get { return middlecerebralartery; }
            set { middlecerebralartery = value; }
        }

        private middlecerbleartery middlecele;

        public middlecerbleartery Middlecele
        {
            get { return middlecele; }
            set { middlecele = value; }
        }

        private string ductusVenosus;

        public string DuctusVenosus
        {
            get { return ductusVenosus; }
            set { ductusVenosus = value; }
        }

        private Ductusvenosus ductusven;

        public Ductusvenosus Ductusven
        {
            get { return ductusven; }
            set { ductusven = value; }
        }

        private string umbilicalartery;

        public string Umbilicalartery
        {
            get { return umbilicalartery; }
            set { umbilicalartery = value; }
        }

        private umbillicalartery umbillical;

        public umbillicalartery Umbillical
        {
            get { return umbillical; }
            set { umbillical = value; }
        }



        private string impression;
        public string Impression
        {
            get { return impression; }
            set { impression = value; }
        }

        private string radiologist;
        public string Radiologist
        {
            get { return radiologist; }
            set { radiologist = value; }
        }

        private string recomandations;
        public string Recomandations
        {
            get { return recomandations; }
            set { recomandations = value; }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private DateTime? createdOn;
        public DateTime? CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
        // sandeep added start here on 3-4-2014
        public string ScanOnDate1
        {
            get;
            set;
        }
        // sandeep added end here on 3-4-2014

    }


    public class EarlyScanServicecalls
    {


        public static IList<EarlyScan> GetAllEarlyScansByUserId(string UserId)
        {
            IList<EarlyScan> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetAllEarlyScansByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<EarlyScan>));

            lstbranchs = OutPut.ReadObject(stream) as IList<EarlyScan>;
            return lstbranchs;
        }

    }





    public class GrowthScanServiceCalls
    {

        public static IList<GrowthScan> GetAllGrowthScansByUserId(string UserId)
        {
            IList<GrowthScan> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetAllGrowthScansByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<GrowthScan>));

            lstbranchs = OutPut.ReadObject(stream) as IList<GrowthScan>;
            return lstbranchs;
        }
    }

    public class NTScanServiceCalls
    {

        public static IList<NTScan> GetAllNTScansByUserId(string UserId)
        {
            IList<NTScan> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetAllNtScansByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<NTScan>));

            lstbranchs = OutPut.ReadObject(stream) as IList<NTScan>;
            return lstbranchs;
        }
    }

    public class AnomalyScanServiceCalls
    {


        public static IList<AnomalyScan> GetAllAlyScansByUserId(string UserId)
        {
            IList<AnomalyScan> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetAllAnomalyScansByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<AnomalyScan>));

            lstbranchs = OutPut.ReadObject(stream) as IList<AnomalyScan>;
            return lstbranchs;
        }

    }
}
