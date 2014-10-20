using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class Getdocuments
    {
        public IList<Documents> GetDocumentsbyProcId(string procId)
        {
            IList<Documents> lstDocuments = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetDocumentsbyProcedureId/" + procId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstDocuments = OutPut.ReadObject(stream) as List<Documents>;
            return lstDocuments;
        }
        public IList<Documents> GetDocumentsbyMedicalTestId(string medTestId)
        {
            IList<Documents> lstDocuments = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetDocumentsbyMedicalTestId/" + medTestId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstDocuments = OutPut.ReadObject(stream) as List<Documents>;
            return lstDocuments;
        }
        public IList<Documents> GetDocumentsbyMedicalScheduleId(string medScheduleId)
        {
            IList<Documents> lstDocuments = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetDocumentsbyMedicalScheduleId/" + medScheduleId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstDocuments = OutPut.ReadObject(stream) as List<Documents>;
            return lstDocuments;
        }
        public  Documents GetDocument(string DocumentId)
        {
             Documents document = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetDocumentbyId/" + DocumentId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Documents));
            document = OutPut.ReadObject(stream) as Documents;
            return document;
        }
        //added by jagadeesh
        public Documents GetDocumentDownload(string DocumentId)
        {
            Documents document = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = "https://www.bumpdocs.com//InnDocs.BumpDocs.CoreDocumentProcess/DocumentLoader/Downloadfile/" + DocumentId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Documents));
            document = OutPut.ReadObject(stream) as Documents;
            return document;
        }

        //end of jagadeesh.
        
        
        public IList<Documents> GetDocumentsbyChartId(string chartId)
        {
            IList<Documents> lstDocuments = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetDocumentsbyChartId/" + chartId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstDocuments = OutPut.ReadObject(stream) as List<Documents>;
            return lstDocuments;
        }
        public static IList<Documents> GetAllSearchedDocs(SearchFields sFields)
        {
            IList<Documents> lstEmp = null;
            lstEmp = new List<Documents>();
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Origin", "*");
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Methods", "POST");

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(SearchFields)));
            searialzeToInsert.WriteObject(ms, sFields);
            string ServiceUrl = DomainServerPath.Service+"Search/GetDocsSearchList";
            byte[] data = PerInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
            //   byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstEmp = OutPut.ReadObject(stream) as List<Documents>;

            //lstEmp = lstEmp.OrderByDescending(x => x.modifiedOn).ToList();//build it


            return lstEmp;

        }

        //Dileep
        public IList<Documents> GetAllProcedureDocumentsbyUserId(string UserId,string GroupType)
        {
            IList<Documents> lstDocuments = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllProcedureDocumentsbyUserId/" + UserId +"/" + GroupType + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstDocuments = OutPut.ReadObject(stream) as List<Documents>;

            lstDocuments = lstDocuments.OrderByDescending(x => x.CreatedOn).ToList();
            return lstDocuments;
        }

        public IList<Documents> GetAllMedicalTestDocumentsbyUserId(string UserId,string GroupType)
        {
            IList<Documents> lstDocuments = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllMedicalTestDocumentsbyUserId/" + UserId + "/" + GroupType + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstDocuments = OutPut.ReadObject(stream) as List<Documents>;

            lstDocuments = lstDocuments.OrderByDescending(x => x.CreatedOn).ToList();
            return lstDocuments;

        }

        public IList<Documents> GetAllChartDocumentsbyUserId(string UserId,string GroupType)
        {
            IList<Documents> lstDocuments = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllChartDocumentsbyUserId/" + UserId + "/" + GroupType + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstDocuments = OutPut.ReadObject(stream) as List<Documents>;
            lstDocuments = lstDocuments.OrderByDescending(x => x.CreatedOn).ToList();
            return lstDocuments;
        }

        public IList<Documents> GetAllmedicationDocumentsbyUserId(string UserId,string GroupType)
        {
            IList<Documents> lstDocuments = null;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllMedicationDocumentsbyUserId/" + UserId + "/" + GroupType + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstDocuments = OutPut.ReadObject(stream) as List<Documents>;
            lstDocuments = lstDocuments.OrderByDescending(x => x.CreatedOn).ToList();
            return lstDocuments;
        }

        public static string UpdateDocument(Documents Docs)
        {
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            Proxy1.Headers["Accept"] = "application/json";


            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Documents));
            serializer.WriteObject(ms, Docs);
            string ServiceURl = DomainServerPath.Service+"CaseManagement/UpdateDocument/" + Docs.Id.ToString();
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            ms = new MemoryStream(data);
            serializer = new DataContractJsonSerializer(typeof(string));
            string succode = serializer.ReadObject(ms) as string;

            return succode;
        }
        //Dileep
        public static IList<Documents> GetAllSearchedProcDocs(SearchFields sFields)
        {
            IList<Documents> lstEmp = null;
            lstEmp = new List<Documents>();
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Origin", "*");
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Methods", "POST");

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(SearchFields)));
            searialzeToInsert.WriteObject(ms, sFields);
            string ServiceUrl = DomainServerPath.Service+"Search/GetProcedureDocs";
            byte[] data = PerInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
            //   byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstEmp = OutPut.ReadObject(stream) as List<Documents>;

            //lstEmp = lstEmp.OrderByDescending(x => x.modifiedOn).ToList();//build it


            return lstEmp;

        }
        public static IList<Documents> GetAllSearchedMtestDocs(SearchFields sFields)
        {
            IList<Documents> lstEmp = null;
            lstEmp = new List<Documents>();
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Origin", "*");
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Methods", "POST");

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(SearchFields)));
            searialzeToInsert.WriteObject(ms, sFields);
            string ServiceUrl = DomainServerPath.Service+"Search/GetMTestDocs";
            byte[] data = PerInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
            //   byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstEmp = OutPut.ReadObject(stream) as List<Documents>;

            //lstEmp = lstEmp.OrderByDescending(x => x.modifiedOn).ToList();//build it


            return lstEmp;

        }
        public static IList<Documents> GetAllSearchedMScehduleDocs(SearchFields sFields)
        {
            IList<Documents> lstEmp = null;
            lstEmp = new List<Documents>();
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Origin", "*");
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Methods", "POST");

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(SearchFields)));
            searialzeToInsert.WriteObject(ms, sFields);
            string ServiceUrl = DomainServerPath.Service+"Search/GetMSchedulesDocs";
            byte[] data = PerInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
            //   byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstEmp = OutPut.ReadObject(stream) as List<Documents>;

            //lstEmp = lstEmp.OrderByDescending(x => x.modifiedOn).ToList();//build it


            return lstEmp;

        }
        public static IList<Documents> GetAllSearchedChartDocs(SearchFields sFields)
        {
            IList<Documents> lstEmp = null;
            lstEmp = new List<Documents>();
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Origin", "*");
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Methods", "POST");

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(SearchFields)));
            searialzeToInsert.WriteObject(ms, sFields);
            string ServiceUrl = DomainServerPath.Service+"Search/GetChartDocs";
            byte[] data = PerInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
            //   byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Documents>));
            lstEmp = OutPut.ReadObject(stream) as List<Documents>;

            //lstEmp = lstEmp.OrderByDescending(x => x.modifiedOn).ToList();//build it


            return lstEmp;

        }
    }
}