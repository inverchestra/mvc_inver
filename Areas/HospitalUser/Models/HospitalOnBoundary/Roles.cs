using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class Roles
    {
        private string description;
        public string Descritpion
        {
            get { return description; }
            set { description=value; }
        }

        private int _roleId;
        public int RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }

        public IList<RolePermissions> lstRolePermissions { set; get; }

    }
}