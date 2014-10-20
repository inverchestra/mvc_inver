using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals
{
    public class UrineTest
    {
        public bool BloodCells { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string DomainId { get; set; }
        public bool GlucoseTolerantTest { get; set; }
        public bool Ketones { get; set; }
        public bool Protein { get; set; }
        public bool UrinaryTtractInFection { get; set; }
        public string UserId { get; set; }
        public string dateUT
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

    }

    public class UrineTestServiceCalls
    {
        public string InsertUrineTestInfo(UrineTest ut)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UrineTest));
            serializerToUplaod.WriteObject(ms, ut);
            string ServiceURl = DomainServerPath.Service+"Vitals/InsertUrineTestInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }


        public IList<UrineTest> GeturintestbyUserId(string userid)
        {
            try
            {
                IList<UrineTest> userInfo = new List<UrineTest>();
                //string UserID = Convert.ToString(userid);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetUrineTestByUserId/" + userid;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UrineTest>));

                userInfo = OutPut.ReadObject(stream) as List<UrineTest>;
                return userInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}