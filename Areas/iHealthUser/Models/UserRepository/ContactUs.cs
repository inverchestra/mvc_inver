using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{

        public class ContactUs
        {

            //[Required(ErrorMessage = "Please enter subject")]
            //public string Subject { get; set; }

            //[Required(ErrorMessage = "Please enter email id")]
            //[DataType(DataType.EmailAddress, ErrorMessage = "Enter valid email id")]
            //[RegularExpression(@"\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Enter valid email id")]
            //public string EmailId { get; set; }

            //[Required(ErrorMessage = "Enter description")]
            //public string Description { get; set; }

            private string id;

            public string Id
            {
                get { return id; }
                set { id = value; }
            }
            private string type;

            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            private string emailId;

            public string EmailId
            {
                get { return emailId; }
                set { emailId = value; }
            }
            private string userFeeling;

            public string UserFeeling
            {
                get { return userFeeling; }
                set { userFeeling = value; }
            }
            private string description;

            public string Description
            {
                get { return description; }
                set { description = value; }
            }


        
    }
}