using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals
{
    public class HeightandWeight
    {
        public string BloodGroup { get; set; }
        public string BloodPressure { get; set; }

        public double? Height { get; set; }
        public int? feets { get; set; }
        public double? inches { get; set; }
        public double? Weight { get; set; }
        public DateTime? CreatedOn { get; set; }
        public float? Weight1 { get; set; }
        public string UserId { get; set; }
        public string DomainId { get; set; }
        public string Created { get; set; }
        public int systole {
            get {
                return String.IsNullOrEmpty(BloodPressure) ? 0 : Convert.ToInt32(BloodPressure.Split('/')[0]);

            }
        }
        public int diastole {
            get {
                return String.IsNullOrEmpty(BloodPressure) ? 0 : Convert.ToInt32(BloodPressure.Split('/')[1]);

            }
        }
        public string date {
            get {
                if (CreatedOn == null) {
                    return "";
                }
                return Convert.ToDateTime(CreatedOn).ToString("d");
            }
        }
    }

    public class HeightandWeightServiceCalls
    {
        public string InsertHeightandWeightInfo(HeightandWeight hw)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(HeightandWeight));
            serializerToUplaod.WriteObject(ms, hw);
            string ServiceURl = DomainServerPath.Service+"Vitals/InsertEssentialInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }

        public IList<HeightandWeight> GetHnWbyUserId(string userid)
        {
            try
            {
                IList<HeightandWeight> userInfo = new List<HeightandWeight>();
                //string UserID = Convert.ToString(userid);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetEssentialInfoByUserId/" + userid;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<HeightandWeight>));

                userInfo = OutPut.ReadObject(stream) as List<HeightandWeight>;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }
    }
}