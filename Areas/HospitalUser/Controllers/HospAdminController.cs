using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Create;
using InnDocs.iHealth.Web.UI.Models;
using System.IO;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Edit;
using InnDocs.iHealth.Web.UI.Utilities;
//using System.Web.Mail;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using System.Text;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models;
using System.Text.RegularExpressions;
using PagedList;


namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Controllers
{
    public class HospAdminController : Controller
    {

        public ActionResult HospitalInfo()
        {
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            else
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;


            HospitalInformation hospInfo = GetHospitalinfo.GetHospitalInfoDomainId(reguser.RUserId);
            if (reguser.FirstName != null)
            {
                // ViewBag.UserName = reguser.FirstName;
                Session["LoginName"] = reguser.FirstName;
            }


            return View(hospInfo);
        }
        public ActionResult LogOut()
        {
            UserInformation user = null;
            if (Session["LoginUser"] != null)
            {
                user = (UserInformation)Session["LoginUser"];
                user.IsLoggedIn = false;
                user.UserSessionID = "";
                Services1 s1 = new Services1();
                string result = s1.UpdateLoginStatus(user);
            }
            //Deleting user directory in server
            string tempDir = "C:\\InndocsiHealth\\PDFFiles\\" + user.UserId;
            string[] files = null;
            if (Directory.Exists(tempDir))
            {
                files = Directory.GetFiles(tempDir);
                foreach (string file in files)
                {
                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                    }

                }
                Directory.Delete(tempDir, true);
            }


            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToActionPermanent("Home", "Home", new { area = "" });
        }

        public ActionResult ShowAllPatient()
        {
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            else
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;



            IList<UserInformation> userInfo = Services1.GetAllPatientsbyDomainIDNew(reguser.RUserId, reguser.GroupType);

            return View(userInfo);

        }

        //ck added
        public ActionResult GetSearchedUsers(string searchtext1)
        {
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            else
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;

            SearchFields sFields = new SearchFields();
            sFields.iHealthUserId = searchtext1;
            sFields.UserId = reguser.RUserId;
            ViewBag.FromSearch = searchtext1;
            return View("SearchPatient", Services1.GetAllSearchedUsers(sFields));
        }
        //ck ended
        //public ActionResult CreateUser()
        //{
        //    return PartialView(new AddUser());
        //}

        public ActionResult CreatePatient()
        {
            return PartialView("CreateUser", new AddUser());
        }

        [HttpPost]
        public ActionResult SavePatient(AddUser NewUser)
        {

            JsonResult res = new JsonResult();
            string insertedID = string.Empty;
            AddUser userinfo = null;
            userinfo = new AddUser();
            UserInformation LoginUser = null;
            LoginUser = Session["LoginUser"] as UserInformation;
            Services1 UserService = null;
            UserService = new Services1();
            SingleRegisterModel RegUser = new SingleRegisterModel();
            RegistrationServiceCalls regService = new RegistrationServiceCalls();
            userinfo.CreatedOn = System.DateTime.Now;
            userinfo.FirstName = NewUser.FirstName;
            userinfo.LastName = NewUser.LastName;
            userinfo.Relationship = NewUser.Relationship;
            userinfo.IsVerified = false;//added by usha for verfication
            RegUser = regService.GetSingleRegUserbyID(LoginUser.DomainId.ToString());
            userinfo.DomainId = RegUser.RUserId;
            userinfo.GroupType = LoginUser.GroupType;
            userinfo.Gender = NewUser.Gender;
            userinfo.Relationship = "Patient";

            userinfo.IsUsingModeratorCredentials = NewUser.IsUsingModeratorCredentials;
            if (userinfo.IsUsingModeratorCredentials != true)
            {
                if (NewUser.EmailId.Contains("@"))
                {
                    userinfo.PhoneNo = null;
                    userinfo.EmailId = NewUser.EmailId;
                    // userinfo.PreferredBy = "1";
                }
                else
                {
                    userinfo.PhoneNo = Convert.ToInt64(NewUser.EmailId);
                    userinfo.EmailId = null;
                    // userinfo.PreferredBy = "2";
                    string charPool = "ABCDEFGOPQRSTUVWXY1234567890ZabcdefghijklmHIJKLMNnopqrstuvwxyz";
                    StringBuilder rs = new StringBuilder();
                    Random random = new Random();

                    for (int i = 0; i < 4; i++)
                    {
                        rs.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
                    }

                    string otaCode = Convert.ToString(rs);
                    DateTime? otaexpire = DateTime.Now;
                    string strDate = DateTime.Now.ToString();
                    DateTime date = DateTime.Parse(strDate);
                    DateTime expireDate = date.AddHours(24);
                    userinfo.OTACode = otaCode;
                    userinfo.OTAExpire = expireDate;
                }

                userinfo.Password = CryptorEngine.Encrypt(NewUser.Password, true);
            }
            else
            {
                userinfo.EmailId = LoginUser.EmailId;
                userinfo.Password = LoginUser.Password;
                userinfo.PhoneNo = LoginUser.PhoneNo;
                // userinfo.PreferredBy = LoginUser.PreferredBy;
            }

            insertedID = UserService.AddUser(userinfo, LoginUser.GroupType);
            if (insertedID == "1041")//added by ck
            {
                res.Data = "1041";
                return res;
            }

            if (insertedID == "1051")
            {
                res.Data = "1051";
                return res;
            }

            if (insertedID != "1041" && insertedID != "1051")
            {
                PersonalInformation p = new PersonalInformation();
                Services3 s3 = new Services3(); // sandeep added on 24-09-2013
                PersonalInformation pinfo = null;
                pinfo = s3.GetPersonalInfoByUserIdAndGroupType(LoginUser.UserId, LoginUser.GroupType);

                p.FirstName = userinfo.FirstName;
                p.LastName = userinfo.LastName;
                p.DomainID = userinfo.DomainId;
                p.UserId = insertedID;
                p.OwnerId = insertedID;

                p.Country = pinfo.Country; // sandeep added on 24-09-2013

                p.DateOfBirth = NewUser.DOB;
                p.Gender = NewUser.Gender;

                if (userinfo.IsUsingModeratorCredentials)
                {
                    p.Contact = pinfo.Contact; // sandeep added on 24-09-2013
                }
                else
                {
                    p.Contact = userinfo.PhoneNo;
                }

                p.Address = pinfo.Address; // sandeep added on 24-09-2013

                p.ZipCode = pinfo.ZipCode; // sandeep added on 24-09-2013

                p.CreatedBy = LoginUser.UserId;
                int result = 0;
                if (!String.IsNullOrWhiteSpace(LoginUser.GroupType)) // sandeep added on 24-09-2013
                {
                    if (LoginUser.GroupType == "Family or Individual")
                    {
                        result = s3.InsertPersonalInfo(p, LoginUser.GroupType);
                    }
                    else
                    {
                        result = s3.InsertPersonalInfo(p, LoginUser.GroupType);
                    }
                }
            }

            //code added by kumar
            bool IsSendMail = false;

            if (insertedID != null && insertedID != "1041" && insertedID != "1051" && userinfo.PhoneNo == 0)
            {
                if (userinfo.EmailId != null)
                {
                    #region Mail Sending
                    try
                    {

                        string Body = "Dear " + userinfo.FirstName + " ,\nYou have been successfully registered with us. \n Please click on below link to verify your email ID. " + System.Environment.NewLine + "\n" + "http://www.medlogx.com/confirmaccount/ConfirmSubUserAccount?userid=" + insertedID + " and Type=" + userinfo.GroupType + " \n You can start using your credentials to login after verfication is successful. \n Thank you, \nMedLogx";// isusing Gmail in ASP.NET";

                        SendMail _sendMail = new SendMail();

                        _sendMail.To = userinfo.EmailId;

                        _sendMail.Body = Body;
                        _sendMail.Subject = "Medlogx Registration details";
                        IsSendMail = regService.SendMail(_sendMail);

                    }
                    catch (Exception ex)
                    {
                        IsSendMail = false;
                    }
                }
                else
                {
                    string phnno = Convert.ToString(userinfo.PhoneNo);
                    //string Uid = "7386041104";
                    //string Password = "55312";
                    //string Number = phnno;
                    string Message = "Your one time password for Medlox is..:" + "" + userinfo.OTACode;
                    //     HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + Uid + "&pwd=" + Password +
                    //"&phone=" + Number + "&msg=" + Message + "&provider=Fullonsms");
                    //     // Get the response.
                    //     HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    //     System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    //     string responseString = respStreamReader.ReadToEnd();
                    //     // Close the Stream object.
                    //     respStreamReader.Close();
                    //     myResp.Close();
                    //if (responseString == "1")
                    //{
                    //    res.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You are registered successfully with MedLogx, you should have login credentials soon in your mobile phone..</h3>";
                    //}

                    SendMessage _sendmessage = new SendMessage();
                    _sendmessage.TophNumber = phnno;
                    _sendmessage.Message = Message;
                    bool SendMessage = regService.SendMessage(_sendmessage);
                }
                    #endregion
                if (IsSendMail)
                {
                    res.Data = "You are registered successfully with MedLogx, you should have login credentials soon in your mailbox..";
                }
                else
                {
                    res.Data = "We are sorry, could not register you successfully. Please check details your entered and try again";
                }
                return res;
            }
            return RedirectToAction("ShowAllPatient", "HospAdmin");

        }

        public ActionResult ShowAllBranches(int? page)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            // branchlist = null;
            IList<Branch> branchlist = GetBranch.GetAllBranchInfo(reguser.RUserId);
            Branch branchinfo = new Branch(); ;
            //branchlist=new IList<Branch>();
            branchinfo.BranchList = branchlist;
            return View(branchlist.ToPagedList(PageNumber, PageSize));

        }

        public ActionResult CreateBranch(Branch branchinfo)
        {
            return PartialView(branchinfo);
        }

        [HttpPost]
        public ActionResult CreateBranchInfo(Branch branchinfo)
        {

            int insertId = 0;
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            else
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;


            JsonResult ResultData = new JsonResult();
            Branch _branchobj = new Branch();
            _branchobj.BranchName = branchinfo.BranchName;
            _branchobj.BranchAddress = branchinfo.BranchAddress;
            _branchobj.BranchDetails = branchinfo.BranchDetails;
            _branchobj.CreatedOn = DateTime.Now;
            string strDate = DateTime.Now.ToString("ddMMyyyyhhmm");
            _branchobj.BranchID = branchinfo.BranchName.Substring(0, 2) + strDate;

            HospitalInformation hospInfo = GetHospitalinfo.GetHospitalInfoDomainId(reguser.RUserId);

            _branchobj.DomainId = hospInfo.DomainId;
            _branchobj.HospitalInfoId = hospInfo.HospitalInfoId;
            CreateBranch _createbranch = null;
            insertId = InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Create.CreateBranch.InsertBranhInfo(_branchobj);
            List<Branch> _lstbranchs = new List<Branch>();
            _lstbranchs.Add(_branchobj);

            if (insertId == 1010)
            {
                ResultData.Data = "Branch Created Successfully";
            }
            else if (insertId == 1041)
            {
                ResultData.Data = "Branch name already exist";
            }
            else
            {
                ResultData.Data = "Branch is not Created .";
            }
            return ResultData;
        }


        public ActionResult UpdateBranch(string branchid)
        {

            Branch getbranch = null;
            getbranch = new Branch();
            getbranch = GetBranch.GetBranchById(branchid);

            return PartialView(getbranch);
        }

        [HttpPost]
        public ActionResult SaveUpdateBranch(Branch branchinfo)
        {
            int updateid = 0;
            Branch updatebranc = null;
            JsonResult js = new JsonResult();
            updatebranc = new Branch();
            updateid = EditBranch.UpdateBranch(branchinfo);
            if (updateid == 1020)
            {
                js.Data = "Updated Successfully";
            }
            else
            {
                js.Data = "update fail";
            }
            return js;
        }


        public ActionResult DeleteBranch(string branchid)
        {
            EditBranch geteditbranch;
            geteditbranch = new EditBranch();
            int successcode = geteditbranch.DeleteBranch(branchid);
            JsonResult r = new JsonResult();
            if (successcode == 1020)
            {
                r.Data = "Branch Deleted Successfully";
              
            }
            else if (successcode == 1036)
            {
                r.Data = "MainBranch Cannot be Deleted";
            }
            else
            {
                r.Data = "Failed please select one branch";
           
            }

            return r;

        }


        public ActionResult ShowAllDepartments(int? page)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);

            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            else
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;



            HospitalInformation hospInfo = GetHospitalinfo.GetHospitalInfoDomainId(reguser.RUserId);
            IList<Departments> departments = GetDepartments.GetDepartmentsByHospitalId(hospInfo.HospitalInfoId);



            return View(departments.ToPagedList(PageNumber, PageSize));
        }


        public ActionResult CreateDepartments()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDepartments(Departments cs)
        {
            JsonResult ResultData = new JsonResult();


            int insertId = 0;
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            else
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            HospitalInformation hospInfo = GetHospitalinfo.GetHospitalInfoDomainId(reguser.RUserId);

            Departments dept = new Departments();
            dept.DepartmentName = cs.DepartmentName;
            dept.HospitalInfoId = hospInfo.HospitalInfoId;
            dept.DomainId = hospInfo.DomainId;
            string strDate = DateTime.Now.ToString("ddMMyyyyhhmm");
            dept.DeptId = cs.DepartmentName.Substring(0, 2) + strDate;
            dept.DeptSpecialization = cs.DeptSpecialization;
            dept.DeptDetails = cs.DeptDetails;
            insertId = InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Create.CreateDepartments.CreateDepartment(dept);


            if (insertId == 1010)
            {
                ResultData.Data = "Department  Created Successfully.";
            }
            else if (insertId == 1041)
            {

                ResultData.Data = "Departmen is already exists.";

            }
            else
            {
                ResultData.Data = "Departmen is not created.";
            }

            return ResultData;
        }


        public ActionResult UpdateDepartment(string deptid)
        {
            Departments dept = new Departments();
            dept = GetDepartments.GetDepartmentById(deptid);
            return PartialView(dept);
        }

        [HttpPost]
        public ActionResult UpdateDepartment(Departments dept)
        {
            JsonResult ResultData = new JsonResult();
            int suucesscode = 0;
            suucesscode = EditDepartments.UpdateDepartments(dept);
            if (suucesscode == 1020)
            {
                ResultData.Data = "Department Updated SuccessFully";
            }
            else if (suucesscode == 1041)
            {
                ResultData.Data = "Department name already Exists";
            }
            else
            {
                ResultData.Data = "Department not Updated ";
            }
            return ResultData;
        }

        [HttpPost]
        public ActionResult DeleteDepartment(string deptid)
        {
            EditDepartments getdepartment;
            getdepartment = new EditDepartments();
            int successcode = getdepartment.DeleteDepatment(deptid);
            JsonResult r = new JsonResult();
            if (successcode == 1020)
            {
                r.Data = "Department Deleted Successfully";

            }
            else
            {
                r.Data = "Department Not Deleted";

            }
            return r;
        }

        public ActionResult ShowAllDoctors()
        {
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            else
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;



            IList<UserInformation> userInfo = Services1.GetAllDoctorsbyDomainIDNew(reguser.RUserId, reguser.GroupType);

            return View(userInfo);


        }
        public ActionResult CreateDoctor()
        {
            return PartialView(new AddUser());
        }
        [HttpPost]
        public ActionResult CreateDoctor(AddUser NewUser)
        {
            //if (NewUser.EmailId.Contains('@'))
            //{
            //    if (!Regex.IsMatch(NewUser.EmailId, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            //    {
            //        ModelState.AddModelError("EmailId", "Email format is invalid.");
            //    }
            //}
            //else
            //{
            //    if (!Regex.IsMatch(NewUser.EmailId, @"^(?:[0-9]+(?:-[0-9])?)*$"))
            //    {
            //        ModelState.AddModelError("EmailId", "Phone number is invalid.");
            //    }
            //}
            //if (!ModelState.IsValid)
            //{
            //    return PartialView(NewUser);
            //}


            JsonResult res = new JsonResult();
            string insertedID = string.Empty;
            AddUser userinfo = null;
            userinfo = new AddUser();
            UserInformation LoginUser = null;
            LoginUser = Session["LoginUser"] as UserInformation;
            Services1 UserService = null;
            UserService = new Services1();
            SingleRegisterModel RegUser = new SingleRegisterModel();
            RegistrationServiceCalls regService = new RegistrationServiceCalls();
            userinfo.CreatedOn = System.DateTime.Now;
            userinfo.FirstName = NewUser.FirstName;
            userinfo.LastName = NewUser.LastName;
            userinfo.Relationship = NewUser.Relationship;
            userinfo.IsVerified = false;//added by usha for verfication
            RegUser = regService.GetSingleRegUserbyID(LoginUser.DomainId.ToString());
            userinfo.DomainId = RegUser.RUserId;
            userinfo.GroupType = LoginUser.GroupType;
            userinfo.Gender = NewUser.Gender;
            userinfo.Relationship = "Doctor";


            userinfo.IsUsingModeratorCredentials = NewUser.IsUsingModeratorCredentials;
            if (userinfo.IsUsingModeratorCredentials != true)
            {
                if (NewUser.EmailId.Contains("@"))
                {
                    userinfo.PhoneNo = null;
                    userinfo.EmailId = NewUser.EmailId;
                    // userinfo.PreferredBy = "1";
                }
                else
                {
                    userinfo.PhoneNo = Convert.ToInt64(NewUser.EmailId);
                    userinfo.EmailId = null;
                    // userinfo.PreferredBy = "2";
                    string charPool = "ABCDEFGOPQRSTUVWXY1234567890ZabcdefghijklmHIJKLMNnopqrstuvwxyz";
                    StringBuilder rs = new StringBuilder();
                    Random random = new Random();

                    for (int i = 0; i < 4; i++)
                    {
                        rs.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
                    }

                    string otaCode = Convert.ToString(rs);
                    DateTime? otaexpire = DateTime.Now;
                    string strDate = DateTime.Now.ToString();
                    DateTime date = DateTime.Parse(strDate);
                    DateTime expireDate = date.AddHours(24);
                    userinfo.OTACode = otaCode;
                    userinfo.OTAExpire = expireDate;
                }

                userinfo.Password = CryptorEngine.Encrypt(NewUser.Password, true);
            }
            else
            {
                userinfo.EmailId = LoginUser.EmailId;
                userinfo.Password = LoginUser.Password;
                userinfo.PhoneNo = LoginUser.PhoneNo;
            }

            insertedID = UserService.AddUser(userinfo, LoginUser.GroupType);
            if (insertedID == "1041")//added by ck
            {
                res.Data = "1041";
                return res;
            }

            if (insertedID == "1051")
            {
                res.Data = "1051";
                return res;
            }

            if (insertedID != "1041" && insertedID != "1051")
            {
                PersonalInformation p = new PersonalInformation();
                Services3 s3 = new Services3(); // sandeep added on 24-09-2013
                PersonalInformation pinfo = null;
                pinfo = s3.GetPersonalInfoByUserIdAndGroupType(LoginUser.UserId, LoginUser.GroupType);

                p.FirstName = userinfo.FirstName;
                p.LastName = userinfo.LastName;
                p.DomainID = userinfo.DomainId;
                p.UserId = insertedID;
                p.OwnerId = insertedID;

                p.Country = pinfo.Country; // sandeep added on 24-09-2013

                p.DateOfBirth = NewUser.DOB;
                p.Gender = NewUser.Gender;

                if (userinfo.IsUsingModeratorCredentials)
                {
                    p.Contact = pinfo.Contact; // sandeep added on 24-09-2013
                }
                else
                {
                    p.Contact = userinfo.PhoneNo;
                }

                p.Address = pinfo.Address; // sandeep added on 24-09-2013

                p.ZipCode = pinfo.ZipCode; // sandeep added on 24-09-2013

                p.CreatedBy = LoginUser.UserId;
                int result = 0;
                if (!String.IsNullOrWhiteSpace(LoginUser.GroupType)) // sandeep added on 24-09-2013
                {
                    if (LoginUser.GroupType == "Family or Individual")
                    {
                        result = s3.InsertPersonalInfo(p, LoginUser.GroupType);
                    }
                    else
                    {
                        result = s3.InsertPersonalInfo(p, LoginUser.GroupType);
                    }
                }
            }

            //code added by kumar
            bool IsSendMail = false;

            if (insertedID != null && insertedID != "1041" && insertedID != "1051" && userinfo.PhoneNo == 0)
            {
                if (userinfo.EmailId != null)
                {
                    #region Mail Sending
                    try
                    {

                        string Body = "Dear " + userinfo.FirstName + " ,\nYou have been successfully registered with us. \n Please click on below link to verify your email ID. " + System.Environment.NewLine + "\n" + "http://www.medlogx.com/confirmaccount/ConfirmSubUserAccount?userid=" + insertedID + " and Type=" + userinfo.GroupType + " \n You can start using your credentials to login after verfication is successful. \n Thank you, \nMedLogx";// isusing Gmail in ASP.NET";

                        SendMail _sendMail = new SendMail();

                        _sendMail.To = userinfo.EmailId;

                        _sendMail.Body = Body;
                        _sendMail.Subject = "Medlogx Registration details";
                        IsSendMail = regService.SendMail(_sendMail);

                    }
                    catch (Exception ex)
                    {
                        IsSendMail = false;
                    }
                }
                else
                {
                    string phnno = Convert.ToString(userinfo.PhoneNo);

                    string Message = "Your one time password for Medlox is..:" + "" + userinfo.OTACode;

                    SendMessage _sendmessage = new SendMessage();
                    _sendmessage.TophNumber = phnno;
                    _sendmessage.Message = Message;
                    bool SendMessage = regService.SendMessage(_sendmessage);
                }
                    #endregion
                if (IsSendMail)
                {
                    res.Data = "You are registered successfully with MedLogx, you should have login credentials soon in your mailbox..";
                }
                else
                {
                    res.Data = "We are sorry, could not register you successfully. Please check details your entered and try again";
                }
                return res;
            }
            return RedirectToAction("ShowAllDoctors", "HospAdmin");
        }

        public ActionResult SearchPatient()
        {
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            else
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;



            IList<UserInformation> userInfo = Services1.GetAllPatientsbyDomainIDNew(reguser.RUserId, reguser.GroupType);

            return View(userInfo);
        }

        [HttpPost]
        public ActionResult SendOTACode(string userid, string GroupType, bool val)
        {

            string charPool = "ABCDEFGOPQRSTUVWXY1234567890ZabcdefghijklmHIJKLMNnopqrstuvwxyz";
            StringBuilder rs = new StringBuilder();
            Random random = new Random();
            RegistrationServiceCalls regService = new RegistrationServiceCalls();
            bool IsSendMail = false;

            for (int i = 0; i < 4; i++)
            {
                rs.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
            }
            string otaCode = Convert.ToString(rs);
            DateTime? otaexpires = DateTime.Now;
            Services1 UsrService = new Services1();
            UserInformation usr = null;
            usr = new UserInformation();
            usr = UsrService.GetUserbyIdNew(userid, GroupType);
            usr.OTACode = otaCode;
            usr.OTAExpire = otaexpires;


            string successcode = UsrService.UpdateUserOTACode(usr);
            if (val == true)
            {
                if (successcode == "1020")
                {

                    Services3 s3 = new Services3();
                    PersonalInformation hosppinfo = s3.GetPersonalInfo(userid);
                    string phnno = Convert.ToString(usr.PhoneNo);
                    //string Uid = "7386041104";
                    //string Password = "55312";
                    string Number = phnno;
                    // string Message = otaCode;
                    //     HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + Uid + "&pwd=" + Password +
                    //"&phone=" + Number + "&msg=" + Message + "&provider=Fullonsms");
                    //     // Get the response.
                    //     HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    //     System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    //     string responseString = respStreamReader.ReadToEnd();
                    //     // Close the Stream object.
                    //     respStreamReader.Close();
                    //     myResp.Close();




                    string Message = "Your one time password for Medlox is..:" + "" + otaCode;

                    SendMessage _sendmessage = new SendMessage();
                    _sendmessage.TophNumber = phnno;
                    _sendmessage.Message = Message;
                    bool SendMessage = regService.SendMessage(_sendmessage);
                }
                else
                {

                }
            }
            else
            {
                if (successcode == "1020")
                {

                    //MailMessage mail = new MailMessage();
                    //mail.To.Add(usr.EmailId);
                    //mail.From = new MailAddress("medlogx@gmail.com");
                    //mail.Subject = "Registration details";
                    // string Body = "Dear " + singleuserObj.RUserName + ",\n\n You have been successfully registered with us.\n\n Please click on below link to verify your email ID. \n\n " + "107.20.220.222:6845/confirmaccount/confirmaccount?userid=" + domainId
                    // + "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";
                    //string Body = "Dear " + usr.FirstName + ",\n\n You have been successfully registered with us.\n\n Please click on below link to verify your email ID. \n\n " + "107.20.220.222:120/confirmaccount/confirmaccount?userid=" + domainId
                    //+ "\n\n You can start using your credentials to login after verfication is successful. \n\nThank you,\nMedLogx";
                    string Body = "Dear" + usr.FirstName + ",\n\n your ota ocde:" + usr.OTACode;

                    //mail.Body = Body;
                    //mail.IsBodyHtml = false;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    //smtp.UseDefaultCredentials = true;
                    //smtp.Credentials = new System.Net.NetworkCredential("medlogx@gmail.com", "medlogx2013");
                    //smtp.EnableSsl = true;
                    //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };


                    SendMail _sendMail = new SendMail();

                    _sendMail.To = usr.EmailId;

                    _sendMail.Body = Body;
                    _sendMail.Subject = "Medlogx Registration details";
                    IsSendMail = regService.SendMail(_sendMail);
                    //smtp.Send(mail);
                    //IsSendMail = true;
                }
            }

            //   int successcode=CreateOTACode.


            return RedirectToAction("ShowAllPatient");


        }

        [HttpPost]
        public ActionResult VerifyOTACODE(string userid, string GroupType, string otacode)
        {

            Services1 UsrService = new Services1();
            UserInformation usr = null;
            usr = new UserInformation();
            usr = UsrService.GetUserbyIdNew(userid, GroupType);
            DateTime presenttime = DateTime.Now;
            DateTime exprie = Convert.ToDateTime(usr.OTAExpire);
            TimeSpan span = exprie - presenttime;

            //if (span.Hours <= 4)
            //{
            //    if (usr.OTACode == otacode)
            //    {

            //    }
            //}
            if (exprie.AddHours(4) >= presenttime)
            {

                if (otacode == usr.OTACode)
                {
                    HospitalInformation hospInfo = GetHospitalinfo.GetHospitalInfoDomainId(usr.DomainId);
                    Relationships relObj = new Relationships();
                    relObj.HospitalInfoID = hospInfo.HospitalID;
                    relObj.UserID = userid;
                    relObj.Type = GroupType;
                    // relObj.DomainId = domainUser.RUserId;
                    int res = EditRelationships.InsertRelationships(relObj);
                }
                else
                {
                    JsonResult ResultData = new JsonResult();
                    ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>You have entered invalid ota code please check it again and enter.</h3>";
                    return ResultData;
                }

            }
            else
            {

                JsonResult ResultData = new JsonResult();
                ResultData.Data = "<h3 style='padding: 10px;width:400px;word-wrap:break-word'>Your ota code has expired please use sendota option to get new ota code.</h3>";
                return ResultData;

                //     string charPool = "ABCDEFGOPQRSTUVWXY1234567890ZabcdefghijklmHIJKLMNnopqrstuvwxyz";
                //     StringBuilder rs = new StringBuilder();
                //     Random random = new Random();

                //     for (int i = 0; i < 4; i++)
                //     {
                //         rs.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
                //     }
                //     string otaCode = Convert.ToString(rs);
                //     Services3 s3 = new Services3();
                //     PersonalInformation hosppinfo = s3.GetPersonalInfo(userid);
                //     string phnno = Convert.ToString(hosppinfo.Contact);
                //     string Uid = "7386041104";
                //     string Password = "55312";
                //     string Number = phnno;
                //     string Message = otaCode;
                //     HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + Uid + "&pwd=" + Password +
                //"&phone=" + Number + "&msg=" + Message + "&provider=Fullonsms");
                //     // Get the response.
                //     HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                //     System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                //     string responseString = respStreamReader.ReadToEnd();
                //     // Close the Stream object.
                //     respStreamReader.Close();
                //     myResp.Close();

            }
            return RedirectToAction("ShowAllPatient");

        }


        public ActionResult AssignDepertment(string branchid)
        {
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            GetBranch getb = new GetBranch();
            List<SelectListItem> lstcases = getb.UserBranchlist(reguser.RUserId, ((branchid == null) ? "" : branchid));
            ViewBag.UserCaseslist = lstcases;
            //lstcases.Where(x => x.Selected == true).FirstOrDefault().Value.ToString();
            DepartToBranch branchinfo = new DepartToBranch();
            IList<Departments> _lstdept = null;
            EditDepartments editdept = new EditDepartments();

            branchinfo.lstDept = GetDepartments.GetDepartmentsByDomainId(reguser.RUserId);
            branchid = lstcases.Where(x => x.Selected == true).FirstOrDefault().Value.ToString();
            List<SelectListItem> lstdepts = editdept.UserDepartmentList(branchid);
            IList<DepartToBranch> lsts = EditDepartments.GetAllBranchInfo(branchid);

            branchinfo.lstDept = GetDepartments.GetDepartmentsByDomainId(reguser.RUserId);
            branchinfo.rdlist = ResultDeptList.GetResultDeptList(branchinfo.lstDept, lsts);
            return PartialView("AssignDepertment", branchinfo);
        }


        [HttpPost]
        public ActionResult AssignDepertment(DepartToBranch deptobranch)
        {
            JsonResult resultdata = new JsonResult();
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
            {
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            }
            List<DepartToBranch> branchlist = EditDepartments.GetAllBranchInfo(deptobranch.BranchID) as List<DepartToBranch>;
            DepartToBranch db = null;
            int s = EditDepartments.DeleteDepartToBranch(deptobranch.BranchID);
            if (deptobranch.dep != null)
            {
                foreach (var i in deptobranch.dep)
                {
                    db = new DepartToBranch();
                    db.BranchID = deptobranch.BranchID;
                    db.DepartmentID = i;

                    int errorcode = EditDepartments.InsertDepartToBranch(db);
                    if (errorcode == 1020)
                    {
                        resultdata.Data = "Sucess";
                    }
                    else
                    {
                        resultdata.Data = "Failed";
                    }

                }
            }
            if (deptobranch.BranchID == deptobranch.CurrentID)
            {
                return Json("1010", JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("BranchCheckedChanged", new { branchid = deptobranch.CurrentID });

        }


        public ActionResult BranchCheckedChanged(string BranchId)
        {
            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            GetBranch getb = new GetBranch();
            EditDepartments editdept = new EditDepartments();
            List<DepartToBranch> branchlist = EditDepartments.GetAllBranchInfo(BranchId) as List<DepartToBranch>;
            List<SelectListItem> lstdepts = editdept.UserDepartmentList(BranchId);
            IList<DepartToBranch> lsts = EditDepartments.GetAllBranchInfo(BranchId);
            DepartToBranch branchinfo = new DepartToBranch();

            branchinfo.lstDept = GetDepartments.GetDepartmentsByDomainId(reguser.RUserId);

            IList<ResultDeptList> rdlist = ResultDeptList.GetResultDeptList(branchinfo.lstDept, lsts);

            return Json(rdlist, JsonRequestBehavior.AllowGet);

        }


    }
}
