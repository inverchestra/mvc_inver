using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.Web.Optimization;
using System.ComponentModel.DataAnnotations;
using InnDocs.iHealth.Web.UI.Controllers;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class HospitalServiceCals
    {
        public static string InsertHospitalInfo(HospitalInformation1 hosp)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            try
            {
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(HospitalInformation1));
                serializerToUplaod.WriteObject(ms, hosp);
                string ServiceURl = DomainServerPath.Service+"HospitalInfo/InsertHospitalInfo";
                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
                string successCode = serializerToResult.ReadObject(stream) as string;

                return successCode;
            }
            catch
            {
                throw;
            }
        }

        //public string UpdateHospitalInfoDetails(HospitalInformation hospinfo, int UserId)
        //{
        //    string result = "false";
        //    string UserID = Convert.ToString(UserId);
        //    WebClient Proxy1 = new WebClient();
        //    Proxy1.Headers["Content-type"] = "application/json";
        //    Proxy1.Headers["Accept"] = "application/json";
        //    MemoryStream ms = new MemoryStream();
        //    DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(HospitalInformation));

        //    serializerToUplaod.WriteObject(ms, hospinfo);
        //    string ServiceURl = DomainServerPath.Service+"Registration/UpdateRegisterUser/" + UserID + "";

        //    byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
        //    Stream stream = new MemoryStream(data);
        //    DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
        //    result = serializerToDownLoad.ReadObject(stream) as string;
        //    return result;
        //}



      

    }
}