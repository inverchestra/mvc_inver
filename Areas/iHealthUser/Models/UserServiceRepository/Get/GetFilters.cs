using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get
{
    public class GetFilters
    {
        public static Filters GetFiltersById(string UserId)
        {
            try
            {
                WebClient PerInfoServiceProxy = new WebClient();

                string ServiceUrl = DomainServerPath.Service+"Peers/GetFiltersbyUserId/" + UserId;

                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Filters));
                Filters cts = OutPut.ReadObject(stream) as Filters;
                // procs.dateoftest1 = procs.DateOfTest.ToString();
                return cts;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public static int UpdateFiltertext(Filters filters)
        {
            try
            {
                WebClient wc = new WebClient();
                MemoryStream ms = new MemoryStream();
                wc.Headers["Content-Type"] = "application/json";
                wc.Headers["Accept"] = "application/json";
                //DataContractJsonSerializer serializer = new DataContractJsonSerializer(log.GetType());
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Filters));
                serializer.WriteObject(ms, filters);
                string url = DomainServerPath.Service+"Peers/UpdateFilters";
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
        public static string InsertFilters(Filters vt)
        {
            try
            {

                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(Filters)));
                searialzeToInsert.WriteObject(ms, vt);
                string ServiceURL = DomainServerPath.Service+"Peers/InsertFilters";

                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                ms = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(ms) as string;
                return insertId;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static string InsertInvitations(Invitation vt)
        {
            try
            {
                
                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(Invitation)));
                searialzeToInsert.WriteObject(ms, vt);
                string ServiceURL = DomainServerPath.Service+"Peers/SaveInvitation";

                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                ms = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(ms) as string;
                return insertId;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}