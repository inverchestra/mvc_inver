using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Collections.Generic;
using System.Collections;
using System.Web.Mvc;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class GetUserCases
    {
        public static IList<Cases> GetAllCases(string UserId,string GroupType)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllCasesbyUserID/" + UserId + "/" + GroupType +"";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Cases>));
            IList<Cases> lstEmp = OutPut.ReadObject(stream) as List<Cases>;
            lstEmp = lstEmp.OrderByDescending(x => x.CreatedOn).ToList();

            return lstEmp;
        }
        public static IList<Cases> GetAllCasesbyPageWise(string UserId,string Pageindex, string pagesize)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetPageWiseCases/" + UserId + "/" + Pageindex + "/" + pagesize + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Cases>));
            IList<Cases> lstEmp = OutPut.ReadObject(stream) as List<Cases>;
            lstEmp = lstEmp.OrderByDescending(x => x.CreatedOn).ToList();

            return lstEmp;
        }

        public Cases GetCaseByID(string CaseId)
        {
            Cases cs = new Cases();
            WebClient LogServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetCasebyID/" + CaseId;
            byte[] data = LogServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Cases));
            cs = OutPut.ReadObject(stream) as Cases;
            cs.HospitalInfos = XmlStringListSerializer.Deserialize<List<Hospital>>(cs.HospitalInfo); //for xml retrieving
            return cs;
        }

        public string deleteCaseByID(string id)
        {
           
            string Successcode;
            WebClient ServProxy = new WebClient();
            ServProxy.Headers["Content-Type"] = "application/json";
            ServProxy.Headers["Accept"] = "application/json";
            string ServiceURL = DomainServerPath.Service+"CaseManagement/DeleteCase/" + id.ToString().Trim();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;
        }

        public static IList<Cases> GetCasesByMonth(string UserId, int Year, int Month)
        {
            WebClient ServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllCasesbyMonth/" + UserId.ToString() + "/" + Year.ToString() + "/" + Month.ToString() + "";
            byte[] data = ServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer output = new DataContractJsonSerializer(typeof(List<Cases>));
            IList<Cases> lstCases = output.ReadObject(stream) as List<Cases>;
            return lstCases;
        }

        public static IList<Cases> GetAllSearchedCases(SearchFields sFields)
        {
            IList<Cases> lstEmp = null;
            lstEmp = new List<Cases>();
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Origin", "*");
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Methods", "POST");

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(SearchFields)));
            searialzeToInsert.WriteObject(ms, sFields);
            string ServiceUrl = DomainServerPath.Service+"Search/GetCasesSearchList";
            byte[] data = PerInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
            //   byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Cases>));
            lstEmp = OutPut.ReadObject(stream) as List<Cases>;

            return lstEmp;


        }

        //Dileep For Upload
        public List<Cases> GetAllUsersCasesByuserId(string UserId,string GroupType)
        {
            List<Cases> casesInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllCasesbyUserID/" + UserId + "/" + GroupType + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Cases>));
            casesInfo = OutPut.ReadObject(stream) as List<Cases>;
            return casesInfo;
        }
        public List<SelectListItem> UserCaseslist(string UserID,string GroupType)
        {
            List<SelectListItem> _caseslist = null;
            _caseslist = new List<SelectListItem>();
            List<Cases> _cases = null;
            _cases = new List<Cases>();
            _cases = GetAllUsersCasesByuserId(UserID,GroupType);
            for (int i = 0; i < _cases.Count; i++)
            {
                _caseslist.Add(new SelectListItem
                {
                    Text = _cases[i].CaseName,
                    Value = _cases[i].CaseId.ToString(),
                    //Selected = (_cases[i].CaseId == _cases[i].CaseId)
                    Selected = (_cases[0].CaseId == _cases[i].CaseId)
                });
            }
            return _caseslist;
        }
        //Dileep
    }
}