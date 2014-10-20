using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals
{
    public class Fhr
    {

        public string Bpm { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string DomainId { get; set; }
        public string dateFHR {
            get {
                if (CreatedOn == null) {
                    return "";
                }
                return Convert.ToDateTime(CreatedOn).ToString("d");
            }
        }

        public int bp {
            get {
                return String.IsNullOrEmpty(Bpm) ? 0 : Convert.ToInt32(Bpm);
            }
        }
        public string created { get; set; }

    }
    public class FHRServiceCalls
    {
        public string InsertFHRInfo(Fhr fhr)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(Fhr));
            serializerToUplaod.WriteObject(ms, fhr);
            string ServiceURl = DomainServerPath.Service+"Vitals/InsertFHRInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }

        public IList<Fhr> GetFhrbyUserId(string userid)
        {
            try
            {
                IList<Fhr> userInfo = new List<Fhr>();
                //string UserID = Convert.ToString(userid);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetFHRByUserId/" + userid;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Fhr>));

                userInfo = OutPut.ReadObject(stream) as List<Fhr>;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }
    }

}
