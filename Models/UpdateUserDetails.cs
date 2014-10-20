using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class UpdateUserDetails
    {
        private UserInformation ihealthuser;

        public UserInformation IHealthUser
        {
            get { return ihealthuser; }
            set { ihealthuser = value; }
        }

        private RegisteredUser rUser; //Added by AD

        public RegisteredUser RegUser
        {
            get { return rUser; }
            set { rUser = value; }
        }

        //usha
        private PersonalInformation _pInfo;

        public PersonalInformation PInfo
        {
            get { return _pInfo; }
            set { _pInfo = value; }
        }
        //end usha

        private string confirmpassword;

        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set { confirmpassword = value; }
        }

        private string newpassword;

        public string NewPassword
        {
            get { return newpassword; }
            set { newpassword = value; }
        }
    }
}