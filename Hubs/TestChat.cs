using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using System.Web.Security;

namespace InnDocs.iHealth.Web.UI.Hubs
{
    public class TestChat : Hub
    {
        public override Task OnConnected()
        {
            Clients.Caller.alertMessage(Context.ConnectionId);
            return base.OnConnected();
        }

        public void Authenticate(string id)
        {
            FormsAuthentication.SetAuthCookie(id, true);
            Clients.Caller.alertMessage(id);
        }

        public void test()
        {
            Clients.Caller.alertMessage("From test method: " + Context.User.Identity.Name);
        }

        public void other()
        {
            Clients.Caller.alertMessage("ID: " + Context.User.Identity.Name);
        }

    }
}