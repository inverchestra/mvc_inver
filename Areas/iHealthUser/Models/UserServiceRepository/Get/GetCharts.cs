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

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class GetCharts
    {
        public static IList<Charts> GetAllChartsByCaseId(string CaseId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetChartsbyCaseID/" + CaseId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Charts>));
            IList<Charts> lstCts = OutPut.ReadObject(stream) as List<Charts>;

            Getdocuments documents = new Getdocuments();
            foreach (var chrts in lstCts)
            {
                //chrts.lstDocuments = documents.GetDocumentsbyMedicalTestId(chrts.ChartId);
                chrts.lstDocuments = documents.GetDocumentsbyChartId(chrts.ChartId);
            }
            return lstCts;
        }

        public string DeleteChartByChartId(string id)
        {
            string Successcode;
            WebClient ServProxy = new WebClient();
            string ServiceURL = DomainServerPath.Service+"CaseManagement/DeleteChart/" + id.Trim();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;

        }

        public static Charts GetChartById(string chartId)
        {
            WebClient PerInfoServiceProxy = new WebClient();

            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetChartsById/" + chartId.ToString();

            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Charts));
            Charts cts = OutPut.ReadObject(stream) as Charts;
            // procs.dateoftest1 = procs.DateOfTest.ToString();
            return cts;

        }
        //Dileep
        public List<SelectListItem> UserCaseChartsList(string caseID)
        {
            List<SelectListItem> _chartsList = null;
            _chartsList = new List<SelectListItem>();
            List<Charts> _charts = null;
            _charts = new List<Charts>();
            _charts = GetAllChartsByCaseId(caseID) as List<Charts>;
            if (_charts.Count > 0)
            {
                for (int i = 0; i < _charts.Count; i++)
                {
                    _chartsList.Add(new SelectListItem
                    {
                        Text = _charts[i].ChartName,
                        Value = _charts[i].ChartId.ToString(),
                        //Selected = (_cases[i].CaseId == _cases[i].CaseId)
                        //Selected = (_procedures[0].CaseId == _procedures[i].CaseId)
                    });
                }
            }
            else
            {
                _chartsList.Add(new SelectListItem
                {
                    Text = "No Charts",
                    Value = ""

                });
            }
            return _chartsList;
        }
        //Dileep
    }
}