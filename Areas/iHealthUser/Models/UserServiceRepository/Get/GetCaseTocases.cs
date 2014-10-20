using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;



namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class GetCaseTocases
    {
        public static IList<CaseToCase> GetAllCaseToCasesByCaseId(string CaseId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllCaseToCases/" + CaseId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<CaseToCase>));
            IList<CaseToCase> lstcases = OutPut.ReadObject(stream) as List<CaseToCase>;
            return lstcases;
        }
    }
}