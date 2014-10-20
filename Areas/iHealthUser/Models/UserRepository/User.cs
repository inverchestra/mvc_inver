using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Configuration;
using InnDocs.iHealth.Web.UI.Models;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class User
    {
        public User()
        {
            Connections = new List<Connection>();
            Groups = new List<string>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Connection> Connections { get; set; }
        public List<string> Groups { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string img { get; set; }
    }
    public class UserModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int gestCount { get; set; }
        public int CountryCount { get; set; }
        public int interestscount { get; set; }
        public int pincodeCount { get; set; }
       
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 0);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber + 1 < TotalPages);
            }
        }


        public IList<User> Users { get; set; }

    }
    public class Connection
    {
        public string ConnectionId { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
    }
    public class Group
    {
        public Group()
        {
            Users = new List<string>();
            Messages = new List<MessageModel>();
        }
        public string GroupId { get; set; }
        public string Type { get; set; }
        public string Topic { get; set; }
        public bool Privacy { get; set; }
        public string Initiater { get; set; }
        public string to { get; set; }
        public List<string> Users { get; set; }
        public List<MessageModel> Messages { get; set; }
    }
    public class UserGroup
    {

        public string UserId { get; set; }
        public string RID { get; set; }
        public string GID { get; set; }
        public bool IsAccepted { get; set; }
    }
    public class GroupModel
    {
        public string GID { get; set; }
        public string Caller { get; set; }
        public string Reciever { get; set; }
        public string name { get; set; }
    }
    public class MessageModel
    {
        public string text { get; set; }
        public string from { get; set; }
        public string gid { get; set; }
        public string groupname { get; set; }
        public DateTime time { get; set; }
        public string des { get; set; }
        public string classa { get; set; }
    }
    public class Invitation
    {
        public string From { get; set; }
        public string Name { get; set; }
        public string message { get; set; }
        public string Id { get; set; }
        public string To { get; set; }
        public string Status { get; set; }
        public DateTime? Time { get; set; }
    }
    public class Accept
    {
        public string reciever { get; set; }
        public bool accepted { get; set; }
        public string GID { get; set; }
        public string caller { get; set; }
        public string name { get; set; }
    }
    public class ActiveUser
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string other { get; set; }
        public string rm { get; set; }
        public string status { get; set; }
        public string offline { get; set; }

    }
    public static class ChatFile
    {
        /* static path*/
       // static string ChatHistoryPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/BumpdocsChat/");
        static string ChatHistoryPath = ConfigurationManager.AppSettings["ChatHistoryPath"];
        /* static path*/
        public static void LogMessage(MessageModel message)
        {
            
            string path = ChatHistoryPath + message.gid + ".txt";
            string line = "";
            if (message.groupname.Contains("online"))
            {
                line = message.from + ": " + message.text + " @" + message.time.TimeOfDay + "" + "online$" + "\r\n";
            }
           else
            {
                line = message.from + ": " + message.text + " @" + message.time.TimeOfDay + "!" + "offline$" + "\r\n";
            }
            File.AppendAllText(path, line);
        }
        public static List<string> ReadAllLines(string gid, string name)
        {
            int counter = 0;
            string line;
         
            gid = ChatHistoryPath + gid + ".txt";
            List<string> list = new List<string>();
            if (File.Exists(gid))
            {
                list = File.ReadAllLines(gid).ToList();

                var str = File.ReadAllText(gid);
                var arr = str.Split(new[] { "$" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i].Contains(name))
                    {
                        if (arr[i].Contains("!offline"))
                        {
                            arr[i] = arr[i].Replace("!offline", "online");
                        }
                    }
                }

                var res = string.Join("$", arr);
                File.WriteAllText(gid, res);
            }
            return list;
        }

        //public static List<string> ReadAllLines(string gid, string name)
        //{
        //    int counter = 0;
        //    string line;
        //    gid = @"C://BumpdocsChat/" + gid + ".txt";
        //    var list = File.ReadAllLines(gid).ToList();

        //    var str = File.ReadAllText(gid);
        //    var arr = str.Split(new[] { "$" }, StringSplitOptions.RemoveEmptyEntries).ToList();

        //    for (int i = 0; i < arr.Count; i++)
        //    {
        //        if (arr[i].Contains(name))
        //        {
        //            if (arr[i].Contains("!offline"))
        //            {
        //                arr[i] = arr[i].Replace("!offline", "online");
        //            }
        //        }
        //    }

        //    var res = string.Join("$", arr);
        //    File.WriteAllText(gid, res);

        //    return list;
        //}
        public static string LastMessage(string id)
        {
            id = ChatHistoryPath + id + ".txt";
          //  id = @"C://BumpdocsChat/" + id + ".txt";
            if (File.Exists(id))
            {
                if (File.ReadAllLines(id).Count() > 0)
                {
                    var line = File.ReadAllLines(id).ToList().Last();
                    var resultant = line.Remove(line.LastIndexOf("@")).Substring(line.IndexOf(":") + 1);
                    return resultant;
                }
            }
            return "";
        }

        public static string OfflineMessage(string id, string name)
        {
            id = ChatHistoryPath + id + ".txt";
            var count = 0;
            if (File.Exists(id))
            {

                var arr = File.ReadAllLines(id).ToList();

                for (int i = 0; i < arr.Count; i++)
                {

                    if (arr[i].Contains(name))
                    {

                        if (arr[i].Contains("!offline"))
                        {
                            count++;
                        }
                    }
                }
                return count.ToString();

            }
            return "";
        }
        public static List<MessageModel> ToMessageList(List<string> lines)
        {
            List<MessageModel> messages = new List<MessageModel>();
            foreach (var item in lines)
            {
                MessageModel message = new MessageModel();
                message.from = item.Split(':')[0];
                message.text = item.Remove(item.LastIndexOf("@")).Substring(item.IndexOf(":") + 1);
                messages.Add(message);
            }
            return messages;
        }
    }
    public static class GroupService
    {
        /* new */
        public static IList<UserInformation> GetAllOnlineUsers(IList<string> lstIds)
        {
            try
            {
                WebClient groupProxy1 = new WebClient();
                groupProxy1.Headers["Content-Type"] = "application/json";
                groupProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(List<string>)));
                searialzeToInsert.WriteObject(ms, lstIds);
                string ServiceURL = DomainServerPath.Service + "Peers/GetUsersByIds";
                byte[] data = groupProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(IList<UserInformation>)).ReadObject(strm) as IList<UserInformation>;
                return list;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static IList<UserInformation> GetCountryUsersByIds(IList<string> lstIds, string currentID)
        {
            try
            {
                WebClient groupProxy1 = new WebClient();
                groupProxy1.Headers["Content-Type"] = "application/json";
                groupProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(List<string>)));
                searialzeToInsert.WriteObject(ms, lstIds);
                string ServiceURL = DomainServerPath.Service + "Peers/GetCountryUsersByIds/" + currentID;
                byte[] data = groupProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(IList<UserInformation>)).ReadObject(strm) as IList<UserInformation>;
                return list;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static IList<UserInformation> GetPincodeUsersByIds(IList<string> lstIds, string currentID)
        {
            try
            {
                WebClient groupProxy1 = new WebClient();
                groupProxy1.Headers["Content-Type"] = "application/json";
                groupProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(List<string>)));
                searialzeToInsert.WriteObject(ms, lstIds);
                string ServiceURL = DomainServerPath.Service + "Peers/GetPincodeUsersByIds/" + currentID;
                byte[] data = groupProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(IList<UserInformation>)).ReadObject(strm) as IList<UserInformation>;
                return list;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static IList<UserInformation> GetGestationUsersByIds(IList<string> lstIds, string currentID)
        {
            try
            {
                WebClient groupProxy1 = new WebClient();
                groupProxy1.Headers["Content-Type"] = "application/json";
                groupProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(List<string>)));
                searialzeToInsert.WriteObject(ms, lstIds);
                string ServiceURL = DomainServerPath.Service + "Peers/GetGestationUsersByIds/" + currentID;
                byte[] data = groupProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(IList<UserInformation>)).ReadObject(strm) as IList<UserInformation>;
                return list;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static IList<UserInformation> GetFiltersUsersByIds(IList<string> lstIds, string currentID)
        {
            try
            {
                WebClient groupProxy1 = new WebClient();
                groupProxy1.Headers["Content-Type"] = "application/json";
                groupProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(List<string>)));
                searialzeToInsert.WriteObject(ms, lstIds);
                string ServiceURL = DomainServerPath.Service + "Peers/GetFiltersUsersByIds/" + currentID;
                byte[] data = groupProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(IList<UserInformation>)).ReadObject(strm) as IList<UserInformation>;
                return list;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static IList<UserInformation> GetInterestsUsersByIds(IList<string> lstIds, string currentID)
        {
            try
            {
                WebClient groupProxy1 = new WebClient();
                groupProxy1.Headers["Content-Type"] = "application/json";
                groupProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(List<string>)));
                searialzeToInsert.WriteObject(ms, lstIds);
                string ServiceURL = DomainServerPath.Service + "Peers/GetInterestsUsersByIds/" + currentID;
                byte[] data = groupProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(IList<UserInformation>)).ReadObject(strm) as IList<UserInformation>;
                return list;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        /* new */
        public static string CreateGroup(Group grp)
        {
            try
            {
                WebClient groupProxy1 = new WebClient();
                groupProxy1.Headers["Content-Type"] = "application/json";
                groupProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(Group)));
                searialzeToInsert.WriteObject(ms, grp);
                string ServiceURL = DomainServerPath.Service + "Peers/CreateGroup";
                byte[] data = groupProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(strm) as string;
                return insertId;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static string CreateUserGroups(UserGroup ugp)
        {
            try
            {
                WebClient groupProxy1 = new WebClient();
                groupProxy1.Headers["Content-Type"] = "application/json";
                groupProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(UserGroup)));
                searialzeToInsert.WriteObject(ms, ugp);
                string ServiceURL = DomainServerPath.Service + "Peers/AddUserstoGroup";
                byte[] data = groupProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string Gid = searializeToResult.ReadObject(strm) as string;
                return Gid;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static string GetGroupId(string caller, string reciever, bool isaccepted)
        {
            Group grp = new Group();
            grp.Initiater = caller;
            grp.Privacy = true;
            grp.Topic = "Chating";
            grp.Type = "Single";
            string Gid = CreateGroup(grp);
            if (!string.IsNullOrEmpty(Gid))
            {
                UserGroup ugp = new UserGroup();
                ugp.RID = reciever;
                ugp.GID = Gid;
                ugp.UserId = caller;
                ugp.IsAccepted = isaccepted;
                string Scode = CreateUserGroups(ugp);
                return Gid;
            }
            else
            {
                var r = OnlineUsers.users.SingleOrDefault(item => item.Id == reciever);
                var c = OnlineUsers.users.SingleOrDefault(item => item.Id == caller);
                Gid = c.Name + " - " + r.Name;
                return Gid;
            }

        }

        public static bool InvitationExists(string to, string from)
        {
            var url = DomainServerPath.Service + "Peers/InvitationExists/" + to + "/" + from;
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = "application/json";
            client.Headers["Accept"] = "application/json";
            byte[] data = client.DownloadData(url);
            Stream stream = new MemoryStream(data);
            bool exists = Convert.ToBoolean(new DataContractJsonSerializer(typeof(string)).ReadObject(stream) as string);
            return exists;
        }

        public static string SaveInvitation(Invitation invitiation)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/SaveInvitation";
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Invitation));
                serializer.WriteObject(stream, invitiation);
                byte[] data = client.UploadData(url, "POST", stream.ToArray());
                stream = new MemoryStream(data);
                var id = new DataContractJsonSerializer(typeof(string)).ReadObject(stream) as string;
                return id;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string UpdateInvitation(string id, string status)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/UpdateInvitation/" + id + "/" + status;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                id = new DataContractJsonSerializer(typeof(string)).ReadObject(stream) as string;
                return id;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static List<Invitation> GetPendindInvitations(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetPendingInvitationsByReciverId/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(List<Invitation>)).ReadObject(stream) as List<Invitation>;
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Invitation> GetActiveInvitations(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetActiveInvitationsByUser/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(List<Invitation>)).ReadObject(stream) as List<Invitation>;
                list = list.Where(item => item.Status == "Yes").ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Invitation GetInvitationById(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GII/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var invitation = new DataContractJsonSerializer(typeof(Invitation)).ReadObject(stream) as Invitation;
                return invitation;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetInvitationId(string to, string from)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetInvitationById/" + to + "/" + from;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var id = new DataContractJsonSerializer(typeof(string)).ReadObject(stream) as string;
                return id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<UserInformation> GetRecentActiveInvitationsByUser(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentActiveInvitationsByUser/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(List<UserInformation>)).ReadObject(stream) as List<UserInformation>;

                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<UserInformation> GetRecentActiveInvitationsByUserCountry(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentActiveUsersByCountry/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(List<UserInformation>)).ReadObject(stream) as List<UserInformation>;

                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<UserInformation> GetRecentActiveUsersByPincode(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentActiveUsersByPincode/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(List<UserInformation>)).ReadObject(stream) as List<UserInformation>;

                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<UserInformation> GetRecentActiveUsersByGestation(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentActiveUsersByGestation/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(List<UserInformation>)).ReadObject(stream) as List<UserInformation>;

                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<UserInformation> GetRecentActiveUsersByFilters(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentActiveUsersByFilters/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(List<UserInformation>)).ReadObject(stream) as List<UserInformation>;

                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<UserInformation> GetRecentActiveUsersByInterests(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentActiveUsersByInterests/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var list = new DataContractJsonSerializer(typeof(List<UserInformation>)).ReadObject(stream) as List<UserInformation>;

                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static int GetAllRecentCountByUser(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetAllRecentCountByUser/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
                int count = (int)OutPut.ReadObject(stream);
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int GetRecentCountByFilters(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentCountByFilters/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
                int count = (int)OutPut.ReadObject(stream);
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int GetRecentCountByCountry(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentCountByCountry/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
                int count = (int)OutPut.ReadObject(stream);
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int GetRecentCountByPincode(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentCountByPincode/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
                int count = (int)OutPut.ReadObject(stream);
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int GetRecentCountByGestation(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentCountByGestation/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
                int count = (int)OutPut.ReadObject(stream);
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int GetRecentCountByInterests(string Id)
        {
            try
            {
                string url = DomainServerPath.Service + "Peers/GetRecentCountByInterests/" + Id;
                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
                int count = (int)OutPut.ReadObject(stream);
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}