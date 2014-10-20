using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class RegisterUser
    {
        [DisplayName("UserName")]
        public string RegUserName { get; set; }
         [DisplayName("EmailId")]
        public string EmailId { get; set; }
         [DisplayName("Password")]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public long? PhoneNo { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
    }
    public class Services2
    {
        public string InsertRegisterUser(RegisterUser regUser)
        {
            try
            {
                string result = "false";
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(RegisterUser));

                serializerToUplaod.WriteObject(ms, regUser);
                string ServiceURl = DomainServerPath.Service+"Registration/InsertRegisterUser";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializerToUplaod = new DataContractJsonSerializer(typeof(RegisterUser));

                RegisterUser result1 = serializerToUplaod.ReadObject(stream) as RegisterUser;
                return result;
            }
            catch
            {
                throw;
            }
           
        }
       
    }
}