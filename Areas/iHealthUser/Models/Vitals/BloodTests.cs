using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals
{
    public class BloodTests
    {
        public string DomainId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string BloodTestName { get; set; }
        public string BloodTestValue { get; set; }

        public string dateBloodTests
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

    public class BloodTestsServiceCalls
    {
        public string InsertBloodTestsInfo(BloodTests cbp)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(BloodTests));
            serializerToUplaod.WriteObject(ms, cbp);
            string ServiceURl = DomainServerPath.Service+"Vitals/InsertBloodTests";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }
    }
}