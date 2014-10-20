using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Runtime.Serialization.Json;
namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Create
{
    public class CreatePostComment
    {
         public static string InsertPostComment(PostComments PostInfo)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                wc.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PostComments));
                serializer.WriteObject(ms, PostInfo);
                string url = DomainServerPath.Service+"MedWall/InsertPostComments/";
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

         public string InsertComment(PostComments PostInfo)
         {
             string code = string.Empty;
             try
             {
                 WebClient wc = new WebClient();
                 wc.Headers["Content-type"] = "application/json";
                 wc.Headers["Accept"] = "application/json";
                 MemoryStream ms = new MemoryStream();
                 DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PostComments));
                 serializer.WriteObject(ms, PostInfo);
                 string url = DomainServerPath.Service+"MedWall/Insertcomments/";
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
