using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get {
    public class GetPostComments {
        public static PostComments GetPostcommentsByDayandUserId(string day, string UserId) {
            PostComments lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"MedWall/GetPostcommentsByDayandUserId/" + day + "/" + UserId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(PostComments));
            lstposts = OutPut.ReadObject(stream) as PostComments;

            return lstposts;


        }
        public static IList<PostComments> GetPostcommentsByweekandUserId(string week, string UserId) {
            IList<PostComments> lstposts = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetPostcommentsByWeekandUserId/" + week + "/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<PostComments>));
                lstposts = OutPut.ReadObject(stream) as List<PostComments>;


            } catch {
            }

            return lstposts;
        }


        //added by kumar
        public static IList<Tips> GetAllTipsByUseridandDay(string Day, string UserId) {
            IList<Tips> lstposts = null;
            try {
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetAllTipsByUseridandDay/" + Day + "/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Tips>));
                lstposts = OutPut.ReadObject(stream) as List<Tips>;

            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }

        public static IList<Tips> GetAllTipsByUseridandWeek(string Week, string UserId) {
            IList<Tips> lstposts = null;
            try {
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetAllTipsByUseridandWeek/" + Week + "/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Tips>));
                lstposts = OutPut.ReadObject(stream) as List<Tips>;

            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }


        public static IList<Questions> GetAllQuesByUseridandWeek(string Week, string UserId) {
            IList<Questions> lstposts = null;
            try {
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetAllQuesByUseridandWeek/" + Week + "/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Questions>));
                lstposts = OutPut.ReadObject(stream) as List<Questions>;

            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }

        public static IList<Questions> GetAllQuestionsbyUserIdandDay(string Day, string UserId) {
            IList<Questions> lstposts = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetAllQuestionsbyUserIdandDay/" + Day + "/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Questions>));
                lstposts = OutPut.ReadObject(stream) as List<Questions>;


            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }

        public static TipsandResponse GetTipsandResponseStatusCount(string UserId, string status) {
            TipsandResponse lstTips = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetTipandResponseByUserIdandStatus/" + UserId + "/" + status + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(TipsandResponse));
                lstTips = OutPut.ReadObject(stream) as TipsandResponse;

            } catch (Exception ex) {
                throw (ex);
            }

            return lstTips;
        }


        public static IList<Tips> GetTipsByDay(string Day, string UserId) {
            IList<Tips> lstposts = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetTipsByDay/" + Day + "/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Tips>));
                lstposts = OutPut.ReadObject(stream) as List<Tips>;


            } catch (Exception ex) {
            }

            return lstposts;
        }

        public static IList<Questions> GetQuestionsByDay(string Day, string UserId) {
            IList<Questions> lstposts = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetQuestionsByDay/" + Day + "/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Questions>));
                lstposts = OutPut.ReadObject(stream) as List<Questions>;


            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }

        public static Questions GetQuestionByQidandUserId(string qid, string UserId) {
            Questions lstTips = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetQuestionByQidandUserId/" + qid + "/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Questions));
                lstTips = OutPut.ReadObject(stream) as Questions;

            } catch (Exception ex) {
                throw (ex);
            }

            return lstTips;
        }


        public static IList<PostComments> GetPostcommentsByUserId(string UserId) {
            IList<PostComments> lstposts = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetPostcommentsByUserId/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<PostComments>));
                lstposts = OutPut.ReadObject(stream) as List<PostComments>;


            } catch (Exception e) {
                throw e;
            }

            return lstposts;
        }

        public static IList<Tips> GetAllTipsByUserid(string UserId) {
            IList<Tips> lstposts = null;
            try {
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetAllTipsByUserid/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Tips>));
                lstposts = OutPut.ReadObject(stream) as List<Tips>;

            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }

        public static IList<Tips> GetAllUnreadTipsByUserid(string UserId, string status) {
            IList<Tips> lstposts = null;
            try {
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetAllTipsByUseridandStatus/" + UserId + "/" + status + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Tips>));
                lstposts = OutPut.ReadObject(stream) as List<Tips>;

            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }

        public static IList<Questions> GetAllQuestionsbyUserId(string UserId) {
            IList<Questions> lstposts = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetAllQuestionsbyUserId/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Questions>));
                lstposts = OutPut.ReadObject(stream) as List<Questions>;


            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }

        public static IList<Questions> GetAllUnreadQuestionsbyUserId(string UserId, string status) {
            IList<Questions> lstposts = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetAllQuestionsbyUserIdandStatus/" + UserId + "/" + status + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Questions>));
                lstposts = OutPut.ReadObject(stream) as List<Questions>;


            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }

        public static IList<Questions> GetAllQuestionsbyUserIdandWeek(string UserId, string week)
        {
            IList<Questions> lstposts = null;
            try
            {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"/MedWall/GetAllQuestionsbyUserIdandWeek/" + UserId + "/" + week + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Questions>));
                lstposts = OutPut.ReadObject(stream) as List<Questions>;


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstposts;
        }

        public static IList<PostComments> GetPostcommentsByUserIdFilterPath(string UserId) {
            IList<PostComments> lstposts = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetPostcommentsByUserIdFilterPath/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<PostComments>));
                lstposts = OutPut.ReadObject(stream) as List<PostComments>;


            } catch (Exception e) {
                throw e;
            }

            return lstposts;
        }
        public static IList<PostComments> GetPostcommentsByweekandUserIdFilterImagePath(string week, string UserId) {
            IList<PostComments> lstposts = null;
            try {


                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"MedWall/GetPostcommentsByweekandUserIdFilterImagePath/" + week + "/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<PostComments>));
                lstposts = OutPut.ReadObject(stream) as List<PostComments>;


            } catch (Exception ex) {
                throw ex;
            }

            return lstposts;
        }

    }
}