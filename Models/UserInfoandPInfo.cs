using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class UserInfoandPInfo
    {

        private UserInformation _IHealthUser;
        public UserInformation IHealthUser
        {
            get { return _IHealthUser; }
            set { _IHealthUser = value; }
        }

        private PersonalInformation _pInfo;

        public PersonalInformation PInfo
        {
            get { return _pInfo; }
            set { _pInfo = value; }
        }
    }
}