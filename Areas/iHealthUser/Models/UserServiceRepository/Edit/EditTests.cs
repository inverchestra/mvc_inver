using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Edit
{
    public class EditTests
    {
        public static string UpdateBloodPicture(BloodPicture es1)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(es1.GetType());
                serializer.WriteObject(ms, es1);
                string url = DomainServerPath.Service+"Vitals/UpdateBloodPictureDetails/" + es1.UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static string UpdateUrienePicture(UrinecompPicture es1)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(es1.GetType());
                serializer.WriteObject(ms, es1);
                string url = DomainServerPath.Service+"Vitals/UpdateUrinePictureDetails/" + es1.UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
        public static string UpdateSerumTest(SerumTest es1)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(es1.GetType());
                serializer.WriteObject(ms, es1);
                string url = DomainServerPath.Service+"Vitals/UpdateSerumTest/" + es1.UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}
