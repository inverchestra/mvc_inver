using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class RolePermissions
    {
        private int roleId;
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        private int _rolePermisionId;
        public int RolePermisionId
        {
            get {return _rolePermisionId; }
            set { _rolePermisionId = value;}
        }
        private int hospuserId;
        public int HospUserId
        {
            get { return hospuserId; }
            set { hospuserId = value; }
        }

        private int doctorId;
        public int DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; }
        }
    
    
    }
}