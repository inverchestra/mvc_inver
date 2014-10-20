using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Collections.Generic;
using System.Collections;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class GetProcedures
    {
        public static IList<Procedure> GetAllProceduresByCaseId(string CaseId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetProcedureByCaseId/" + CaseId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Procedure>));
            IList<Procedure> lstprocs = OutPut.ReadObject(stream) as List<Procedure>;
            Getdocuments documents = new Getdocuments();
            foreach (var procedure in lstprocs)
            {
                procedure.lstDocuments = documents.GetDocumentsbyProcId(procedure.ProcedureId);
            }

            return lstprocs;
        }

        public static IList<Procedure> GetAllProceduresByUserId(string UserId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetProcedureByUserId/" + UserId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Procedure>));
            IList<Procedure> lstprocs = OutPut.ReadObject(stream) as List<Procedure>;
            Getdocuments documents = new Getdocuments();
            foreach (var procedure in lstprocs)
            {
                procedure.lstDocuments = documents.GetDocumentsbyProcId(procedure.ProcedureId);
            }

            return lstprocs;
        }

        public string DeleteProcedure(string id)
        {

            string Successcode;
            WebClient ServProxy = new WebClient();
            string ServiceURL = DomainServerPath.Service+"CaseManagement/DeleteProcedure/" + id.Trim();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;
        }

        public static Procedure GetProcedureById(string ProcId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetProcedure/" + ProcId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Procedure));
            Procedure procs = OutPut.ReadObject(stream) as Procedure;
            procs.dateofservice1 = procs.DateOfService.ToString();
            return procs;
        }
        ////Dileep
        public List<SelectListItem> UserCaseProcList(string CaseID)
        {
            List<SelectListItem> _procList = null;
            _procList = new List<SelectListItem>();
            List<Procedure> _procedures = null;
            _procedures = new List<Procedure>();
            _procedures = GetAllProceduresByCaseId(CaseID) as List<Procedure>;
            if (_procedures.Count > 0)
            {
                for (int i = 0; i < _procedures.Count; i++)
                {
                    _procList.Add(new SelectListItem
                    {
                        Text = _procedures[i].ProcedureName,
                        Value = _procedures[i].ProcedureId.ToString(),
                        //Selected = (_cases[i].CaseId == _cases[i].CaseId)
                        //Selected = (_procedures[0].CaseId == _procedures[i].CaseId)
                    });
                }
            }
            else
            {
                _procList.Add(new SelectListItem
                {
                    Text = "No Procedures",
                    Value = ""

                });
            }
            return _procList;
        }
        ////Dileep



    }
}