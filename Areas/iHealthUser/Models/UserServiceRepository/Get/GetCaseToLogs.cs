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
    public class GetCaseToLogs
    {
        public static IList<CaseToLog> GetAllCaseTologsByCaseId(string CaseId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetCaseToLogs/" + CaseId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<CaseToLog>));
            IList<CaseToLog> lstlogs = OutPut.ReadObject(stream) as List<CaseToLog>;
            return lstlogs;
        }

    }
}