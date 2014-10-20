using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Models;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers;
using System.ComponentModel.DataAnnotations;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET
{
    public class GetHospitalUserInfo
    {


        public static HospitalUserInfo GetUserbyId(string Id)
        {
            try
            {
                HospitalUserInfo userInfo = null;
                userInfo = new HospitalUserInfo();
                //string UserID = Convert.ToString(Id);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetUserByID/" + Id;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(HospitalUserInfo));

                userInfo = OutPut.ReadObject(stream) as HospitalUserInfo;
                return userInfo;
            }
            catch(Exception ex)
            {
                
                throw ex;
            }
        }

        public  static string UpdateUserPasswordDetails(UserInformation UserInfo, string ID,string gType)
        {
            try
            {
                string result = "false";
               string UserID = Convert.ToString(ID);
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"HospitalInfo/UpdateHospuserpasswordDetails/" + UserID +"/"+ gType ;

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public static List<HospitalUserInfo> GetAllDoctors(string DomaiID)
        {
            try
            {
                List<HospitalUserInfo> userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetAllDoctors/" + DomaiID;

                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<HospitalUserInfo>));
                userInfo = OutPut.ReadObject(stream) as List<HospitalUserInfo>;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        //added by sp
        public static IList<UserInformation> GetAllHospitaluserByObj(UserInformation usr)
        {
            try
            {
                IList<UserInformation> userInfo = null;
                WebClient PerInfoServiceProxy = new WebClient();

                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));
                serializerToUplaod.WriteObject(ms, usr);
                string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetAllHospitaluserByObj";
                byte[] data = Proxy1.UploadData(ServiceUrl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<UserInformation>));
                userInfo = OutPut.ReadObject(stream) as IList<UserInformation>;
                return userInfo;
            }
            catch (Exception e)
            {
                return (IList<UserInformation>)null;
            }
        }
        public static IList<Cases> GetAllIOpatients(UserInformation caseinfo)
        {
            try
            {

                IList<Cases> cases = new List<Cases>();
                WebClient PerInfoServiceProxy = new WebClient();

                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));
                serializerToUplaod.WriteObject(ms, caseinfo);
                string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllIOpatients";
                byte[] data = Proxy1.UploadData(ServiceUrl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<Cases>));
                cases = OutPut.ReadObject(stream) as IList<Cases>;
                return cases;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //ended by sp
    }
}