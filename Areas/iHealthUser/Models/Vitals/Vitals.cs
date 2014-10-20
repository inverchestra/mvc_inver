using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using InnDocs.iHealth.Web.UI.Models;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals
{
    public class Vitals
    {
        //--------------------------------------------added by chp
        private string _btname;

        public string BTName
        {
            get { return _btname; }
            set { _btname = value; }
        }
        //------------------------------------------added by chp
        private HeightandWeight _hnw;

        public HeightandWeight HnW
        {
            get { return _hnw; }
            set { _hnw = value; }
        }

        private IList<HeightandWeight> _lsthnw;

        public IList<HeightandWeight> LstHnW
        {
            get { return _lsthnw; }
            set { _lsthnw = value; }
        }

        private MedicalInformation medInfo;
        public MedicalInformation MedInfo
        {
            get { return medInfo; }
            set { medInfo = value; }
        }

        //--------------------------------------

        public string Systole { get; set; }
        public string Diastole { get; set; }
        public string BloodTestName { get; set; }
        public string FirstTrimester { get; set; }
        public string SecondTrimester { get; set; }
        public string ThirdTrimester { get; set; }

        private CompleteBloodPicture _completebp;
        public CompleteBloodPicture CompleteBP
        {
            get { return _completebp; }
            set { _completebp = value; }
        }

        private IList<CompleteBloodPicture> _lstcompletebp;
        public IList<CompleteBloodPicture> LstCompleteBP
        {
            get { return _lstcompletebp; }
            set { _lstcompletebp = value; }
        }



        //---------------------------------------------------
        private BloodTests _bt;

        public BloodTests BT
        {
            get { return _bt; }
            set { _bt = value; }
        }

        private IList<BloodTests> _lstbt;

        public IList<BloodTests> LstBT
        {
            get { return _lstbt; }
            set { _lstbt = value; }
        }

        //--------------------------------------------

        private SkinTest _st;

        public SkinTest ST
        {
            get { return _st; }
            set { _st = value; }
        }

        ////private IList<SkinTest> _lstst;

        //public IList<SkinTest> LstST
        //{
        //    get { return ; }
        //    set {  = value; }
        //}

        //----------------------------------------------
        public IList<SkinTest> lstSkinTest { get; set; }
        private UrineTest _ut;

        public UrineTest UT
        {
            get { return _ut; }
            set { _ut = value; }
        }

        //private IList<UrineTest> _lstut;

        //public IList<UrineTest> LstUT
        //{
        //    get { return _lstut; }
        //    set { _lstut = value; }
        //}

        //----------------------------------------------
        public IList<UrineTest> lstUrineTest { get; set; }
        private Fhr _fhr;
        public Fhr FHR
        {
            get { return _fhr; }
            set
            {
                _fhr = value;
            }
        }
        public IList<Fhr> lstFhr { get; set; }
        private STDs _stds;

        public STDs STDs
        {
            get { return _stds; }
            set { _stds = value; }
        }

        private IList<STDs> _lststds;

        public IList<STDs> LstSTDs
        {
            get { return _lststds; }
            set { _lststds = value; }
        }
        //---------------------------------------------------------
        private UST _uSTXMLFields;

        public UST USTXMLFields
        {
            get { return _uSTXMLFields; }
            set { _uSTXMLFields = value; }
        }


        private UST _ust;

        public UST UST
        {
            get { return _ust; }
            set { _ust = value; }
        }

        private IList<UST> _lstust;

        public IList<UST> LstUST
        {
            get { return _lstust; }
            set { _lstust = value; }
        }

        //----------------------------------------------------------

        //-------------------------------------------------------
        private string _choroidPlexusCyst;
        public string ChoroidPlexusCyst
        {
            get { return _choroidPlexusCyst; }
            set { _choroidPlexusCyst = value; }
        }
        //--------------------------------------------------
        private string _ventriculomegaly;
        public string Ventriculomegaly
        {
            get { return _ventriculomegaly; }
            set { _ventriculomegaly = value; }
        }
        //------------------------------------------------
        private string _echogenicBowel;
        public string EchogenicBowel
        {
            get { return _echogenicBowel; }
            set { _echogenicBowel = value; }
        }
        //------------------------------------------------
        private string _headShape;
        public string HeadShape
        {
            get { return _headShape; }
            set { _headShape = value; }
        }
        //----------------------------------------------
        private string _nuchalPad;
        public string NuchalPad
        {
            get { return _nuchalPad; }
            set { _nuchalPad = value; }
        }
        //---------------------------------------------
        private string _cysternaMagna;
        public string CysternaMagna
        {
            get { return _cysternaMagna; }
            set { _cysternaMagna = value; }
        }
        //-------------------------------------------------
        private string _cleftLip;
        public string CleftLip
        {
            get { return _cleftLip; }
            set { _cleftLip = value; }
        }
        //---------------------------------------------
        private string _echogenicFociInHeart;
        public string EchogenicFociInHeart
        {
            get { return _echogenicFociInHeart; }
            set { _echogenicFociInHeart = value; }
        }
        //-------------------------------------------------
        private string _dilatedRenalPelvis;
        public string DilatedRenalPelvis
        {
            get { return _dilatedRenalPelvis; }
            set { _dilatedRenalPelvis = value; }
        }
        //--------------------------------------------------
        private string _shortFemurOrHumerus;
        public string ShortFemurOrHumerus
        {
            get { return _shortFemurOrHumerus; }
            set { _shortFemurOrHumerus = value; }
        }
        //-------------------------------------------------
        private string _talipes;
        public string Talipes
        {
            get { return _talipes; }
            set { _talipes = value; }
        }
        //------------------------------------------------
        private string _sandalGap;
        public string SandalGap
        {
            get { return _sandalGap; }
            set { _sandalGap = value; }
        }
        //-----------------------------------------------
        private string _clinodactyly;
        public string Clinodactyly
        {
            get { return _clinodactyly; }
            set { _clinodactyly = value; }
        }
        //----------------------------------------------
        private string _clenchedHand;
        public string ClenchedHand
        {
            get { return _clenchedHand; }
            set { _clenchedHand = value; }
        }
        //---------------------------------------------
        private string _twoVesselCord;
        public string TwoVesselCord
        {
            get { return _twoVesselCord; }
            set { _twoVesselCord = value; }
        }
        //--------------------------------------------
        private string _maternalAge;
        public string MaternalAge
        {
            get { return _maternalAge; }
            set { _maternalAge = value; }
        }
        //-------------------------------------------
        private string _nuchalTranslucency;
        public string NuchalTranslucency
        {
            get { return _nuchalTranslucency; }
            set { _nuchalTranslucency = value; }
        }
        //------------------------------------------
        public string USTOXmlFields;
        public Vitals USTOXmlFieldsInfo { get; set; }

        private IList<AnomalyScan> _lstAnlyScans;//ck added

        public IList<AnomalyScan> LstAnlyScans
        {
            get { return _lstAnlyScans; }
            set { _lstAnlyScans = value; }
        }


        private IList<GrowthScan> _lstGrthScan;
        public IList<GrowthScan> LstGrthScan
        {
            get { return _lstGrthScan; }
            set { _lstGrthScan = value; }
        }

        private IList<NTScan> _lstNtScan;
        public IList<NTScan> LstNtScan
        {
            get { return _lstNtScan; }
            set { _lstNtScan = value; }
        }

        private IList<EarlyScan> _lstErlyScan;
        public IList<EarlyScan> LstErlyScan
        {
            get { return _lstErlyScan; }
            set { _lstErlyScan = value; }
        }
    }
}