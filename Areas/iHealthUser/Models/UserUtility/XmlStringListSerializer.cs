using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Xml.Serialization;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility
{
    public class XmlStringListSerializer
    {
        public static string ToXmlString<T>(T Object)
        {
            if (Object == null)
                return null;

            XmlSerializer xs = new XmlSerializer(Object.GetType());
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, Object);
                return sw.ToString();
            }
        }
        public static T Deserialize<T>(string xmlString)
        {
            if (xmlString == "" || xmlString == null)
            {
                IList<T> temp = null;
                return (T)temp;
            }
            else
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(new StringReader(xmlString));
            }
        }
    }
}