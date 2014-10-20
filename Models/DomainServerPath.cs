using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace InnDocs.iHealth.Web.UI
{
    public static class DomainServerPath
    {
        public static readonly string Service = ConfigurationManager.AppSettings["DomainService"];
    }
}