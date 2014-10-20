using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class GetMedicalSchedules
    {
        public static IList<MedicalSchedule> GetAllMeicalschdulesbyCaseId(string CaseId)
        {
            IList<MedicalSchedule> lstmsc = null;
            lstmsc = new List<MedicalSchedule>();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetMedicalSchedulesByCase/" + CaseId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<MedicalSchedule>));
            lstmsc = OutPut.ReadObject(stream) as List<MedicalSchedule>;
            Getdocuments doc = new Getdocuments();
            foreach (MedicalSchedule s in lstmsc)
            {
                s.lstDocuments = doc.GetDocumentsbyMedicalScheduleId(s.MedicalScheduleId);
                s.lstSchedule = XmlStringListSerializer.Deserialize<List<Schedule>>(s.Schedule);
            }

            return lstmsc;
        }

        public string DeleteMedSchById(string id)
        {
            string Successcode;
            WebClient ServProxy = new WebClient();
            string ServiceURL = DomainServerPath.Service+"CaseManagement/DeleteMedicalSchedule/" + id.Trim();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;
        }

        public static MedicalSchedule GetMedicationScheduleById(string mSheduleId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetMedicalSchedule/" + mSheduleId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(MedicalSchedule));
            MedicalSchedule ms = OutPut.ReadObject(stream) as MedicalSchedule;
            Getdocuments doc = new Getdocuments();
            ms.lstDocuments = doc.GetDocumentsbyMedicalScheduleId(ms.MedicalScheduleId);
            ms.lstSchedule = XmlStringListSerializer.Deserialize<List<Schedule>>(ms.Schedule);

            return ms;

        }

        public List<SelectListItem> UserCaseMedicationsList(string caseID)
        {
            List<SelectListItem> _MedicationList = null;
            _MedicationList = new List<SelectListItem>();
            List<MedicalSchedule> _Medications = null;
            _Medications = new List<MedicalSchedule>();
            _Medications = GetAllMeicalschdulesbyCaseId(caseID) as List<MedicalSchedule>;
            if (_Medications.Count > 0)
            {
                for (int i = 0; i < _Medications.Count; i++)
                {
                    _MedicationList.Add(new SelectListItem
                    {
                        Text = _Medications[i].PharmacyName.ToString(), // sandeep modified
                        Value = _Medications[i].MedicalScheduleId.ToString(),
                        //Selected = (_cases[i].CaseId == _cases[i].CaseId)
                        //Selected = (_procedures[0].CaseId == _procedures[i].CaseId)
                    });
                }
            }
            else
            {
                _MedicationList.Add(new SelectListItem
                {
                    Text = "No Medications",
                    Value = ""

                });
            }
            return _MedicationList;
        }
    }
}