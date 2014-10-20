using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Collections.Generic;
using System.Collections;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class GetMedicalTests
    {
        public static IList<MedicalTests> GetAllMedicalTestsByCaseId(string CaseId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetMedicalTestsbyCaseID/" + CaseId.Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<MedicalTests>));
            IList<MedicalTests> lstMts = OutPut.ReadObject(stream) as List<MedicalTests>;

            Getdocuments documents = new Getdocuments();
            foreach (var medicaltest in lstMts)
            {
                medicaltest.lstDocuments = documents.GetDocumentsbyMedicalTestId(medicaltest.MedicalTestId);
            }

            return lstMts;
        }

        public static IList<MedicalTests> GetAllMedicalTestsByUserId(string UserId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetMedicalTestsbyUserId/" + UserId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<MedicalTests>));
            IList<MedicalTests> lstMts = OutPut.ReadObject(stream) as List<MedicalTests>;

            Getdocuments documents = new Getdocuments();
            foreach (var medicaltest in lstMts)
            {
                medicaltest.lstDocuments = documents.GetDocumentsbyMedicalTestId(medicaltest.MedicalTestId);
            }

            return lstMts;
        }

        public string DeleteMTestbyMtestId(string id)
        {

            string Successcode;
            WebClient ServProxy = new WebClient();
            string ServiceURL = DomainServerPath.Service+"CaseManagement/DeleteMedicalTest/" + id.Trim();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;
        }

        public static MedicalTests GetMedicalTestById(string mtID)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetMedicalTestById/" + mtID.ToString();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(MedicalTests));
            MedicalTests procs = OutPut.ReadObject(stream) as MedicalTests;
           // procs.dateoftest1 = procs.DateOfTest.ToString();
            return procs;
        }
        //Dileep
        public List<SelectListItem> UserCaseMedTestsList(string caseID)
        {
            List<SelectListItem> _medTestsList = null;
            _medTestsList = new List<SelectListItem>();
            List<MedicalTests> _medTests = null;
            _medTests = new List<MedicalTests>();
            _medTests = GetAllMedicalTestsByCaseId(caseID) as List<MedicalTests>;
            if (_medTests.Count > 0)
            {
                for (int i = 0; i < _medTests.Count; i++)
                {
                    _medTestsList.Add(new SelectListItem
                    {
                        Text = _medTests[i].TestName,
                        Value = _medTests[i].MedicalTestId.ToString(),
                        //Selected = (_cases[i].CaseId == _cases[i].CaseId)
                        //Selected = (_procedures[0].CaseId == _procedures[i].CaseId)
                    });
                }
            }
            else
            {
                _medTestsList.Add(new SelectListItem
                {
                    Text = "No MedicalTests",
                    Value = ""

                });
            }
            return _medTestsList;
        }
        //Dileep
    }
}