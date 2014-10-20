using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Models;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository {
    public class OnlineUsers {
        public static List<User> users = new List<User>();
        public static List<GroupModel> groups = new List<GroupModel>();
        public static List<UserInformation> allUsers = new List<UserInformation>();
        public static List<Invitation> invitations = new List<Invitation>();

        public static void AddOnlineUsers(string userId, string userName) {
            User user = new User();
            user.Id = userId;
            user.Name = userName;
            if (users.Where(item => item.Id.Contains(userId)).ToList().Count == 0) {
                user.Connections = new List<Connection>();
                users.Add(user);
            }
        }

        public static void AddNewConnection(string userId, string connectionId, string userAgent) {
            users.SingleOrDefault(x => x.Id == userId).Connections.Add(new Connection() { Connected = true, ConnectionId = connectionId, UserAgent = userAgent });
            //save connection to server db
        }

        public static void RemoveConnection(string userId, string connectionId) {
            var user = users.SingleOrDefault(item => item.Id == userId);
            if (user != null) {
                var userConnection = user.Connections.SingleOrDefault(i => i.ConnectionId == connectionId);
                if (userConnection != null) {
                    users.SingleOrDefault(item => item.Id == userId).Connections.Remove(userConnection);
                    //remove connection from db
                }
            }
        }

        public static void AddNewGroup(string userId, string groupId) {
            var user = users.SingleOrDefault(item => item.Id == userId);
            if (user != null && !(user.Groups.Contains(groupId))) {
                users.SingleOrDefault(item => item.Id == userId).Groups.Add(groupId);
            }
        }

        public static bool GroupExists(string caller, string reciever) {
            var group = OnlineUsers.groups.SingleOrDefault(item => ((item.Caller == caller && item.Reciever == reciever) || (item.Caller == reciever && item.Reciever == caller)));
            if (group != null) {
                return true;
            }
            return false;
        }

        public static string GetGroupId(string caller, string reciever) {
            var group = OnlineUsers.groups.SingleOrDefault(item => ((item.Caller == caller && item.Reciever == reciever) || (item.Caller == reciever && item.Reciever == caller)));
            return group.GID;
        }

        public static void AddToGroups(string gid, string caller, string reciever, string name) {
            groups.Add(new GroupModel() { GID = gid, Reciever = reciever, Caller = caller, name = name });
        }

        public static bool InvitationExists(string to, string from) {
            var invitation = invitations.SingleOrDefault(item => (item.From == to && item.To == from) || (item.To == to && item.From == from));
            return (invitation != null) ? true : false;
        }

        public static string GetInvitationId(string user, string from) {
            var invitation = invitations.SingleOrDefault(item => item.From == user || item.To == user);
            return invitation.Id;
        }

        public static void AddInvitation(Invitation invitation) {
            invitation.Id = (invitations.Count() + 1).ToString();
            invitations.Add(invitation);
        }

    }
}