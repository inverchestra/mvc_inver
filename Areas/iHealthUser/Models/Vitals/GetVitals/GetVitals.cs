using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals.GetVitals
{
    public class GetAllVitals
    {
        public static IList<STDs> GetRecentStdsByUserId(string Userid)
        {
            IList<STDs> lststds = new List<STDs>();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetSTDsByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<STDs>));
                lststds = OutPut.ReadObject(stream) as List<STDs>;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lststds;
        }
        public static IList<SkinTest> GetRecentSkinTestByUserId(string Userid)
        {
            IList<SkinTest> lstst = new List<SkinTest>();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetSkinTestByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<SkinTest>));
                lstst = OutPut.ReadObject(stream) as List<SkinTest>;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lstst;
        }
        public static IList<UrineTest> GetRecentUrineTestByUserId(string Userid)
        {
            IList<UrineTest> lstut = new List<UrineTest>();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetUrineTestByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UrineTest>));
                lstut = OutPut.ReadObject(stream) as List<UrineTest>;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lstut;
        }
        public static IList<Fhr> GetRecentFHRByUserId(string Userid)
        {
            IList<Fhr> lstfhr = new List<Fhr>();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetFHRByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Fhr>));
                lstfhr = OutPut.ReadObject(stream) as List<Fhr>;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lstfhr;
        }


        //get recent updated values methods
        public static STDs GetRecentStdsObjByUserId(string Userid)
        {
            STDs lststds = new STDs();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetSTDsRecentByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(STDs));
                lststds = OutPut.ReadObject(stream) as STDs;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lststds;
        }

        public static SkinTest GetRecentSkinTestObjByUserId(string Userid)
        {
            SkinTest lstskintest = new SkinTest();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetSkinTestRecentByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(SkinTest));
                lstskintest = OutPut.ReadObject(stream) as SkinTest;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lstskintest;
        }

        public static UrineTest GetRecentUrineTestObjByUserId(string Userid)
        {
            UrineTest lsturinetest = new UrineTest();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetUrineTestRecentByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UrineTest));
                lsturinetest = OutPut.ReadObject(stream) as UrineTest;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lsturinetest;
        }

        public static Fhr GetRecentFhrObjByUserId(string Userid)
        {
            Fhr lstfhr = new Fhr();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetFHRRecentByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Fhr));
                lstfhr = OutPut.ReadObject(stream) as Fhr;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lstfhr;
        }

        public static HeightandWeight GetRecentHWObjByUserId(string Userid)
        {
            HeightandWeight lsthw = new HeightandWeight();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetEssentialInfoRecentByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(HeightandWeight));
                lsthw = OutPut.ReadObject(stream) as HeightandWeight;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lsthw;
        }

        public static CompleteBloodPicture GetRecentCBPObjByUserId(string Userid)
        {
            CompleteBloodPicture lstcbp = new CompleteBloodPicture();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetCBPRecentByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(CompleteBloodPicture));
                lstcbp = OutPut.ReadObject(stream) as CompleteBloodPicture;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lstcbp;
        }

        public static UST GetRecentUSTObjByUserId(string Userid)
        {
            UST lstust = new UST();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetUSTRecentByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UST));
                lstust = OutPut.ReadObject(stream) as UST;
                string xml = lstust.USObservations;
                lstust.USTObservationsXmlFields = XmlStringListSerializer.Deserialize<UST>(xml);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lstust;
        }

        public static CompleteBloodPicture GetRecentBInfoByUserId(string Userid)
        {
            CompleteBloodPicture lsthw = new CompleteBloodPicture();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetBIRecentByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(CompleteBloodPicture));
                lsthw = OutPut.ReadObject(stream) as CompleteBloodPicture;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lsthw;
        }

        public static IList<BloodTests> GetAllBTsInfoByUserIdandBTName(string Userid, string BTname)
        {
            IList<BloodTests> lstBTs = new List<BloodTests>();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetBTsByUserIdandName/" + Userid + "/" + BTname + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<BloodTests>));
                lstBTs = OutPut.ReadObject(stream) as List<BloodTests>;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lstBTs;
        }

        public static IList<BloodTests> GetAllBTsInfoByUserId(string Userid)
        {
            IList<BloodTests> lstBTs = new List<BloodTests>();
            try
            {

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetBTsByUserId/" + Userid.ToString() + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<BloodTests>));
                lstBTs = OutPut.ReadObject(stream) as List<BloodTests>;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return lstBTs;
        }
    }
}