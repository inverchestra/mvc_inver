using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models
{
    public class ResultDeptList
    {
        public string Id;
        public string name;
        public bool IsChecked;
        public static IList<ResultDeptList> GetResultDeptList(IList<Departments> lstdeptes, IList<DepartToBranch> lstdtob)
        {
            IList<ResultDeptList> lstResult = null;
            lstResult = new List<ResultDeptList>();
            ResultDeptList result = null;

            foreach (Departments depts in lstdeptes)
            {
                result = new ResultDeptList();
                result.Id = depts.DeptId;
                result.name = depts.DepartmentName;
                if (lstdtob != null)
                {
                    foreach (DepartToBranch dtob in lstdtob)
                    {

                        if (depts.DeptId == dtob.DepartmentID)
                        {
                            result.IsChecked = true;
                        }
                    }
                    lstResult.Add(result);
                }
            }
            return lstResult;
        }
    }
}
