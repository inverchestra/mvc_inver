using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get
{
    public class GetTests
    {
        public static BloodPicture GetBloodPictureByUserIdandId(string UserId, string Id)
        {
            BloodPicture lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetBloodPictureByUserIdandId/" + UserId + "/" + Id + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(BloodPicture));
            lstposts = OutPut.ReadObject(stream) as BloodPicture;

            return lstposts;


        }
        public static IList<BloodPicture> GetAllBloodPicturesByUserId(string UserId)
        {
            IList<BloodPicture> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetAllBloodPictureByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<BloodPicture>));

            lstbranchs = OutPut.ReadObject(stream) as IList<BloodPicture>;
            return lstbranchs;
        }

        public static UrinecompPicture GetUrinecompPictureByUserIdandId(string UserId, string Id)
        {
            UrinecompPicture lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetUrinePictureUserIdandId/" + UserId + "/" + Id + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UrinecompPicture));
            lstposts = OutPut.ReadObject(stream) as UrinecompPicture;

            return lstposts;


        }
        public static IList<UrinecompPicture> GetAllUrinecompPictureByUserId(string UserId)
        {
            IList<UrinecompPicture> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetAllUrinePictureByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<UrinecompPicture>));

            lstbranchs = OutPut.ReadObject(stream) as IList<UrinecompPicture>;
            return lstbranchs;
        }

        public static SerumTest GetSerumTestByUserIdandId(string UserId, string Id)
        {
            SerumTest lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetSerumTestByUserIdandId/" + UserId + "/" + Id + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(SerumTest));
            lstposts = OutPut.ReadObject(stream) as SerumTest;

            return lstposts;


        }
        public static IList<SerumTest> GetAllSerumTestByUserId(string UserId)
        {
            IList<SerumTest> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Vitals/GetAllSerumTestByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<SerumTest>));

            lstbranchs = OutPut.ReadObject(stream) as IList<SerumTest>;
            return lstbranchs;
        }
    }
}