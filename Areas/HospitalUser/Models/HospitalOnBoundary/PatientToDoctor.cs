using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class PatientToDoctor
    {
        public string RelatedDoctorID { get; set; }
    }
}