using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Optimization;
using System.ComponentModel.DataAnnotations;
using InnDocs.iHealth.Web.UI.Controllers;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Create
{
    public class CreateHospMedicalInformation
    {

        public static int InsertHospitalMedicalInfo(HospMedicalInformation hm)
        {
            try
            {
                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(HospMedicalInformation)));
                searialzeToInsert.WriteObject(ms, hm);
                string ServiceURL = DomainServerPath.Service+"CaseManagement/InsertChart";
                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                int insertId = Convert.ToInt32(searializeToResult.ReadObject(strm) as string);
                return insertId;
            }
            catch (Exception e)
            {
                return 0;
            }
        }


    }
}