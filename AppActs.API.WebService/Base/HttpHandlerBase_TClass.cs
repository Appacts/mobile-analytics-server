using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Text;

namespace AppActs.API.WebService.Base
{
    [XmlInclude(typeof(AppActs.API.WebService.Device))]
    public class HttpHandlerBase<TClass> : HttpHandlerBase
    {
        [XmlElement]
        public TClass Object { get; set; }

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write(AppActs.Core.Xml.Serialization.Serialize<HttpHandlerBase<TClass>>(this, new System.Xml.XmlWriterSettings() { Encoding = Encoding.UTF8 }));
        }
    }
}