using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class UploadFiles
    {
        public static string UploadFile(Documents p)
        {
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(Documents));
            serializerToUplaod.WriteObject(ms, p);
            string ServiceURl = DomainServerPath.Service+"DocumentLoader/UploadFile";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string message = serializerToResult.ReadObject(stream) as string;
            return message;
        }
    }
}