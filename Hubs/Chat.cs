using System.Linq;
using System.Threading.Tasks;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Models;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Web.Security;
using System.IO;
using System;

namespace InnDocs.iHealth.Web.UI.Hubs
{
    //[Authorize]
    public class Chat : Hub
    {

        public override Task OnConnected()
        {
            lock (Context.ConnectionId)
            {
                //OnlineUsers.allUsers = new Services1().GetAllActiveUsers();
                if (!HttpContext.Current.Request.Headers["User-Agent"].Contains("Android"))
                {
                    var user = User();
                    OnlineUsers.AddNewConnection(user, Context.ConnectionId, HttpContext.Current.Request.Headers["User-Agent"]);
                    Clients.AllExcept(Context.ConnectionId).joined(user, Context.ConnectionId);
                    setUserToGroup();
                }
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected()
        {

            try
            {
                lock (Context.ConnectionId)
                {
                    var user = User();

                    var obj = (from e in OnlineUsers.users
                               where e.Connections.Select(item => item.ConnectionId).Contains(Context.ConnectionId)
                               select e).FirstOrDefault().Connections.FirstOrDefault(item => item.ConnectionId == Context.ConnectionId);

                    OnlineUsers.RemoveConnection(user, Context.ConnectionId);

                    if (OnlineUsers.users.SingleOrDefault(item => item.Id == user).Connections.Count == 0)
                    {
                        Clients.AllExcept(Context.ConnectionId).userOffline(user);
                    }

                    if (obj != null && obj.UserAgent.Contains("Android"))
                    {
                        FormsAuthentication.SignOut();
                    }
                }
            }
            catch (Exception)
            {
                return base.OnDisconnected();
            }

            return base.OnDisconnected();
        }

        //public override Task OnDisconnected()
        //{
        //    lock (Context.ConnectionId)
        //    {
        //        var user = User();

        //        var obj = (from e in OnlineUsers.users
        //                   where e.Connections.Select(item => item.ConnectionId).Contains(Context.ConnectionId)
        //                   select e).FirstOrDefault().Connections.FirstOrDefault(item => item.ConnectionId == Context.ConnectionId);

        //        OnlineUsers.RemoveConnection(user, Context.ConnectionId);

        //        if (OnlineUsers.users.SingleOrDefault(item => item.Id == user).Connections.Count == 0)
        //        {
        //            Clients.AllExcept(Context.ConnectionId).userOffline(user);
        //        }

        //        if (obj != null && obj.UserAgent.Contains("Android"))
        //        {
        //            FormsAuthentication.SignOut();
        //        }
        //    }
        //    return base.OnDisconnected();
        //}

        public void AuthenticateUser(string id)
        {
            lock (Context.ConnectionId)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    FormsAuthentication.SetAuthCookie(id, true);
                    OnlineUsers.AddOnlineUsers(id, Name(id));
                    OnlineUsers.AddNewConnection(id, Context.ConnectionId, HttpContext.Current.Request.Headers["User-Agent"]);
                    Clients.AllExcept(Context.ConnectionId).joined(id, Context.ConnectionId);
                    setUserToGroup();
                }
            }
        }

        public void Test()
        {
            Clients.Caller.alertMessage("Server test: " + Context.User.Identity.Name);
        }

        public void SignoutOthers()
        {
            var user = User();
            var connections = OnlineUsers.users.SingleOrDefault(item => item.Id == user).Connections.Where(item => item.ConnectionId != Context.ConnectionId).ToList();
            if (connections != null)
            {
                foreach (var item in connections)
                {
                    Clients.Client(item.ConnectionId).signoutSession();
                }
            }
        }

        public void setUserToGroup()
        {
            lock (Context.ConnectionId)
            {
                var user = User();
                var groups = GroupService.GetActiveInvitations(User());
                if (groups.Count > 0)
                {
                    foreach (var item in groups)
                    {
                        Groups.Add(Context.ConnectionId, item.Id);
                    }
                }
            }
        }

        public void SetInvitationOff(string id)
        {
            lock (Context.ConnectionId)
            {
                var Iid = GroupService.GetInvitationId(id, User());
                if (!string.IsNullOrEmpty(Iid) && Iid != "0")
                {
                    Clients.Group(Iid).invitationOff(Iid);
                }
            }
        }

        public void SetInvitationOn(string id)
        {
            lock (Context.ConnectionId)
            {
                var Iid = GroupService.GetInvitationId(id, User());
                if (!string.IsNullOrEmpty(Iid) && Iid != "0")
                {
                    Clients.Group(Iid).invitationOn(Iid);
                }
            }
        }

        public void GetPendingInvitations()
        {
            lock (Context.ConnectionId)
            {
                var invitations = GroupService.GetPendindInvitations(User());
                if (invitations != null)
                {
                    invitations.ForEach(item => item.message = item.Name + " has invited you to a private chat.");
                    if (invitations != null)
                    {
                        Clients.Caller.displayPendingInvitations(invitations);
                    }
                }
            }
        }

        public void GetActiveUsers()
        {
            lock (Context.ConnectionId)
            {
                Clients.Caller.displayActiveUsers(ActiveUsers());
            }
        }

        public void OnlineUsersList()
        {
            lock (Context.ConnectionId)
            {
                Clients.Caller.onlineUsers(OnlineUsers.users.Where(item => item.Connections.Count > 0).Select(x => x.Id).ToList());
            }
        }

        public void SendInvitation(Invitation invitation)
        {
            lock (Context.ConnectionId)
            {
                if (GroupService.InvitationExists(invitation.To, User()))
                {
                    Clients.Caller.alertMessage("Invitation exists between " + Name(invitation.To) + " and you.");
                }
                else
                {
                    //var username = OnlineUsers.allUsers.SingleOrDefault(item => item.UserId == User()).FirstName;
                    var username = new Services1().GetUserFirstName(User());
                    var to = OnlineUsers.users.SingleOrDefault(item => item.Id == invitation.To);
                    invitation.Name = username;
                    invitation.message = username + " has invited you to a private chat.";
                    invitation.From = User();
                    invitation.Status = "Pending";
                    invitation.Id = GroupService.SaveInvitation(invitation);
                    if (invitation.Id != "0" && !string.IsNullOrEmpty(invitation.Id))
                    {
                        Clients.Caller.alertMessage("Invitation sent successfully.");
                        if (to != null)
                        {
                            foreach (var item in to.Connections)
                            {
                                Clients.Client(item.ConnectionId).invitation(invitation);
                            }
                        }
                    }
                }
            }
        }

        public void CreateActiveUser(Accept accept)
        {
            lock (Context.ConnectionId)
            {
                var reciever = OnlineUsers.users.SingleOrDefault(item => item.Id == accept.reciever);
                var caller = OnlineUsers.users.SingleOrDefault(item => item.Id == accept.caller);
                var list = new List<string>();

                if (accept.accepted)
                {

                    var id = GroupService.UpdateInvitation(accept.GID, "Yes");

                    var invitation = string.IsNullOrEmpty(id) ? new Invitation() { Id = "" } : GroupService.GetInvitationById(id);

                    if (!string.IsNullOrEmpty(id))
                    {

                        if (reciever.Connections.Count > 0)
                        {
                            var name = (reciever.Id == User()) ? Name(accept.caller) : Name(accept.reciever);
                            foreach (var item in reciever.Connections)
                            {
                                list.Add(item.ConnectionId);
                                Clients.Client(item.ConnectionId).addActiveUser(new ActiveUser()
                                {
                                    ID = id,
                                    Name = name
                                });
                            }
                        }
                        if (caller.Connections.Count > 0)
                        {
                            var name = (caller.Id == User()) ? Name(accept.caller) : Name(accept.reciever);
                            foreach (var item in caller.Connections)
                            {
                                list.Add(item.ConnectionId);
                                Clients.Client(item.ConnectionId).addActiveUser(new ActiveUser()
                                {
                                    ID = id,
                                    Name = name
                                });
                            }
                        }

                        foreach (var item in list)
                        {
                            Groups.Add(item, invitation.Id);
                        }

                    }

                }
                else
                {
                    var message = GroupService.UpdateInvitation(accept.GID, "No");
                }
            }
        }

        public void SendMessage(MessageModel message)
        {
            lock (Context.ConnectionId)
            {
                message.from = OnlineUsers.users.SingleOrDefault(item => item.Id == User()).Name;
                ChatFile.LogMessage(message);
                Clients.Group(message.gid).addMessage(message);
            }
        }

        public void GetHistory(string gid, string name)
        {
            lock (Context.ConnectionId)
            {

                IList<MessageModel> lines = ChatFile.ToMessageList(ChatFile.ReadAllLines(gid, name));
                Clients.Caller.showHistory(lines, gid);
            }
        }

        public void DeleteActiveUser(string li)
        {
            lock (Context.ConnectionId)
            {
                var id = GroupService.UpdateInvitation(li, "No");
                Clients.Group(li).deleteInvitation(li);
            }
        }

        private string User()
        {
            return Context.User.Identity.Name;
        }

        private string Name(string id)
        {
            //if (OnlineUsers.allUsers == null || OnlineUsers.allUsers.Count == 0)
            //{
            //    OnlineUsers.allUsers = new Services1().GetAllActiveUsers();
            //}
            string firstName = new Services1().GetUserFirstName(id);

            return firstName;//OnlineUsers.allUsers.SingleOrDefault(item => item.UserId == id).FirstName;
        }
        /*  new */
        public void AllOnlineUsersList()
        {
            lock (Context.ConnectionId)
            {
                Clients.Caller.displayOnlineUsers(GetAllOnlineUsers(OnlineUsers.users.Where(item => item.Connections.Count > 0 && item.Id != User()).Select(x => x.Id).ToList()));

            }
        }
        public void GetOnlineCountryUsers()
        {
            lock (Context.ConnectionId)
            {
                string currentId = User();
                Clients.Caller.displayOnlineCountryUsers(GetCountryUsersByIds(OnlineUsers.users.Where(item => item.Connections.Count > 0 && item.Id != User()).Select(x => x.Id).ToList(), currentId));

            }
        }
        public void GetOnlinePincodeUsers()
        {
            lock (Context.ConnectionId)
            {
                string currentId = User();
                Clients.Caller.displayOnlinePincodeUsers(GetPincodeUsersByIds(OnlineUsers.users.Where(item => item.Connections.Count > 0 && item.Id != User()).Select(x => x.Id).ToList(), currentId));

            }
        }
        public void GetOnlineGestationUsers()
        {
            lock (Context.ConnectionId)
            {
                string currentId = User();
                Clients.Caller.displayOnlineGestationUsers(GetGestationUsersByIds(OnlineUsers.users.Where(item => item.Connections.Count > 0 && item.Id != User()).Select(x => x.Id).ToList(), currentId));

            }
        }
        public void GetOnlineFiltersUsers()
        {
            lock (Context.ConnectionId)
            {
                string currentId = User();
                Clients.Caller.displayOnlineFiltersUsers(GetFiltersUsersByIds(OnlineUsers.users.Where(item => item.Connections.Count > 0 && item.Id != User()).Select(x => x.Id).ToList(), currentId));

            }
        }
        public void GetOnlineInterestUsers()
        {
            lock (Context.ConnectionId)
            {
                string currentId = User();
                Clients.Caller.displayOnlineInterestUsers(GetInterestsUsersByIds(OnlineUsers.users.Where(item => item.Connections.Count > 0 && item.Id != User()).Select(x => x.Id).ToList(), currentId));

            }
        }
        public void AllRecentActiveInvitationsByUser()
        {
            lock (Context.ConnectionId)
            {
                Clients.Caller.displayRecentActiveInvitationsByUser(GetRecentActiveInvitationsByUser());

            }
        }
        /*  new */
        private List<ActiveUser> ActiveUsers()
        {
            var invitations = GroupService.GetActiveInvitations(User());
            var userId = User();
            var users = new List<ActiveUser>();
            if (invitations != null)
            {
                foreach (var item in invitations)
                {
                    var user = new ActiveUser();
                    user.ID = item.Id;
                    user.Name = (item.To == userId) ? Name(item.From) : Name(item.To);
                    user.rm = ChatFile.LastMessage(item.Id);
                    user.offline = ChatFile.OfflineMessage(item.Id, user.Name); user.offline = user.offline == "0" ? "" : user.offline;
                    user.status = ((item.To == userId) ? IsOtherOnline(item.From) : IsOtherOnline(item.To)) ? "online" : "offline";
                    users.Add(user);
                }
            }
            return users;
        }
        /* new */
        private IList<User> GetRecentActiveInvitationsByUser()
        {
            var users = GroupService.GetRecentActiveInvitationsByUser(User()).ToList();

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId

                });

            }

            return _user;
        }
        private UserModel GetAllOnlineUsers(IList<string> user)
        {
            var users = GroupService.GetAllOnlineUsers(user);

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId
                });

            }

            var gestationUsersCount = GroupService.GetGestationUsersByIds(user, User()).Count;

            var interesrUsersCount = GroupService.GetInterestsUsersByIds(user, User()).Count;

            var countryUsersCount = GroupService.GetCountryUsersByIds(user, User()).Count;

            var pinCodeUsersCount = GroupService.GetInterestsUsersByIds(user, User()).Count;

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;
            usrModel.CountryCount = countryUsersCount;
            usrModel.gestCount = gestationUsersCount;
            usrModel.pincodeCount = pinCodeUsersCount;
            usrModel.interestscount = interesrUsersCount;
            return usrModel;
        }
        private UserModel GetCountryUsersByIds(IList<string> user, string currentId)
        {
            var users = GroupService.GetCountryUsersByIds(user, currentId);

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId
                });

            }
            UserModel usrModel = new UserModel();
            usrModel.Users = _user;


            return usrModel;
        }
        private UserModel GetPincodeUsersByIds(IList<string> user, string currentId)
        {
            var users = GroupService.GetPincodeUsersByIds(user, currentId);

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId
                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;


            return usrModel;
        }

        private UserModel GetGestationUsersByIds(IList<string> user, string currentId)
        {
            var users = GroupService.GetGestationUsersByIds(user, currentId);

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId
                });

            }


            UserModel usrModel = new UserModel();
            usrModel.Users = _user;


            return usrModel;
        }

        private UserModel GetFiltersUsersByIds(IList<string> user, string currentId)
        {
            var users = GroupService.GetFiltersUsersByIds(user, currentId);

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId
                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;


            return usrModel;
        }
        private UserModel GetInterestsUsersByIds(IList<string> user, string currentId)
        {
            var users = GroupService.GetInterestsUsersByIds(user, currentId);

            IList<User> _user = new List<User>();
            foreach (var item in users)
            {

                _user.Add(new User()
                {
                    Name = item.FirstName,
                    Id = item.UserId,
                    email = string.IsNullOrEmpty(item.EmailId) ? item.PhoneNo.ToString() : item.EmailId
                });

            }

            UserModel usrModel = new UserModel();
            usrModel.Users = _user;


            return usrModel;
        }
        /* new */
        private bool IsOtherOnline(string id)
        {
            var user = OnlineUsers.users.Where(item => item.Id == id);
            if (user.Count() > 0)
            {
                return (user.First().Connections.Count > 0) ? true : false;
            }
            return false;
        }

    }

}