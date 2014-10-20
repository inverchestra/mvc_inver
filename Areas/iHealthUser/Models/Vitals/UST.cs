using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals
{
    public class UST
    {

        private UST _uSTObservationsXmlFields;
        public UST USTObservationsXmlFields
        {
            get { return _uSTObservationsXmlFields; }
            set { _uSTObservationsXmlFields = value; }
        }
        //----------------------------------------------------
        private string _usObservations;
        public string USObservations
        {
            get { return _usObservations; }
            set { _usObservations = value; }
        }

        //-------------------------------------------------------
        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        //------------------------------------------------------
        private string _domainId;
        public string DomainId
        {
            get { return _domainId; }
            set { _domainId = value; }
        }

        //======================================================
        private DateTime? _createdOn;
        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
        public string dateUST
        {
            get
            {
                if (CreatedOn == null)
                {
                    return "";
                }
                return Convert.ToDateTime(CreatedOn).ToString("d");
            }
        }
        //=======================================================
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


    }

    public class USTServiceCalls
    {
        public string InsertUSTInfo(UST ust)
        {
            try
            {
                string InsertedID = string.Empty;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UST));

                serializerToUplaod.WriteObject(ms, ust);
                string ServiceURl = DomainServerPath.Service+"Vitals/InsertUSTInfo";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                InsertedID = serializerToDownLoad.ReadObject(stream) as string;
                return InsertedID;
            }
            catch
            {
                throw;
            }
        }

        //public List<UST> GetUSTInfoLstByUserId(string UserId)
        //{
        //    List<UST> userInfo = null;
        //    //userInfo=new List<OnlinePayments>();
        //    try
        //    {
        //        // IList<OnlinePayments> userInfo = null;               
        //        WebClient PerInfoServiceProxy = new WebClient();
        //        string ServiceUrl = DomainServerPath.Service+"Vitals/GetUSTByUserId" + UserId;
        //        byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
        //        Stream stream = new MemoryStream(data);
        //        DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UST>));
        //        userInfo = OutPut.ReadObject(stream) as List<UST>;

        //        for (int i = 0; i < userInfo.Count; i++)
        //        {
        //            string xml = userInfo[i].USObservations;
        //            userInfo[i].USObservationsInfo = XmlStringListSerializer.Deserialize<UST>(xml);
        //        }

        //        return userInfo;
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}


        public IList<UST> GetUSTInfoLstByUserId(string userid)
        {
            try
            {
                IList<UST> userInfo = new List<UST>();
                //string UserID = Convert.ToString(userid);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetUSTByUserId/" + userid;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UST>));
                userInfo = OutPut.ReadObject(stream) as List<UST>;

                for (int i = 0; i < userInfo.Count; i++)
                {
                    string xml = userInfo[i].USObservations;
                    userInfo[i].USTObservationsXmlFields = XmlStringListSerializer.Deserialize<UST>(xml);
                }

                return userInfo;
            }
            catch
            {
                throw;
            }
        }

    }

}