using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Tests
    {
        public string TestType { get; set; }
        public List<SelectListItem> TestTypeList { get; set; }

        public List<SelectListItem> TestTypes()
        {
            TestTypeList = new List<SelectListItem>();
            TestTypeList.Add(new SelectListItem() { Text = "PleaseSelect", Value = "" });
            TestTypeList.Add(new SelectListItem() { Text = "BloodTest", Value = "Blood", Selected = false });
            TestTypeList.Add(new SelectListItem() { Text = "UrineTest", Value = "UrineTest", Selected = false });
            TestTypeList.Add(new SelectListItem() { Text = "SeerumTest", Value = "Seerum", Selected = false });
           
            return TestTypeList;
        }
    }
}