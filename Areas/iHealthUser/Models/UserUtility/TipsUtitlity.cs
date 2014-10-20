using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility
{

    public class Tip
    {
        public string Title { get; set; }
        public int Week { get; set; }
        public int Day { get; set; }
        public List<string> STip { get; set; }
        public bool Status { get; set; }
    }

    public static class TipService
    {
        public static string UpdateTip(string Id, string week)
        {
            try
            {
                WebClient client = new WebClient();
                string url = DomainServerPath.Service + "MedWall/uwt/" + week + "/" + Id;
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var success = new DataContractJsonSerializer(typeof(string)).ReadObject(stream) as string;
                return success;
            }
            catch
            {
                return "1021";
            }
        }

        public static Tip WeekTip(int week, int day)
        {
            try
            {
                WebClient client = new WebClient();
                string url = DomainServerPath.Service + "MedWall/gwt/" + week + "/" + day;
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var tip = new DataContractJsonSerializer(typeof(Tip)).ReadObject(stream) as Tip;
                return tip;
            }
            catch
            {
                return null;
            }
        }

        public static string UnreadTips(int week, string userId)
        {
            try
            {
                WebClient client = new WebClient();
                string url = DomainServerPath.Service + "MedWall/urt/" + week + "/" + userId;
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                var count = new DataContractJsonSerializer(typeof(string)).ReadObject(stream) as string;
                return count;
            }
            catch
            {
                return "0";
            }
        }

    }

}