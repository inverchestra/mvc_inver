using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Edit
{
    public class EditPostComment
    {
        public static int UpdatePostComment(PostComments postcomment, string UserId)
        {
            try
            {
                WebClient wc = new WebClient();
                MemoryStream ms = new MemoryStream();
                wc.Headers["Content-Type"] = "application/json";
                wc.Headers["Accept"] = "application/json";
                //DataContractJsonSerializer serializer = new DataContractJsonSerializer(log.GetType());
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PostComments));
                serializer.WriteObject(ms, postcomment);
                string url = DomainServerPath.Service+"MedWall/UpdatePostComments/" + UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);
            }
            catch (Exception e)
            {
                return 0;
            }
        }




        public static string UpdateTip(string TipId, string UserId)
        {
            try
            {
                WebClient wc = new WebClient();
                MemoryStream ms = new MemoryStream();
                wc.Headers["Content-Type"] = "application/json";
                wc.Headers["Accept"] = "application/json";
                string url = DomainServerPath.Service+"Medwall/UpdateTipStatus/" + TipId + "/" + UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

        public static string UpdateQuestion(string QuestionId, string UserId, string response)
        {
            try
            {
                WebClient wc = new WebClient();
                MemoryStream ms = new MemoryStream();
                wc.Headers["Content-Type"] = "application/json";
                wc.Headers["Accept"] = "application/json";
                string url = DomainServerPath.Service+"Medwall/UpdateQuestionStatus/" + QuestionId + "/" + UserId + "/" + response + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

        public static string UpdateTipStatusByTipId(string TipId, string UserId)
        {
            try
            {
                WebClient wc = new WebClient();
                MemoryStream ms = new MemoryStream();
                wc.Headers["Content-Type"] = "application/json";
                wc.Headers["Accept"] = "application/json";
                string url = DomainServerPath.Service+"Medwall/UpdateTipStatusByTipId/" + TipId + "/" + UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string UpdateQuestion(Questions PostInfo)
        {
            string code = string.Empty;
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                wc.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Questions));
                serializer.WriteObject(ms, PostInfo);
                string url = DomainServerPath.Service+"/MedWall/UpdateQuestion/";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                code = serializer.ReadObject(stream) as string;

            }
            catch (Exception e)
            {
                return string.Empty;
            }
            return code;
        }


    }
}