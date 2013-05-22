using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

using Castle.Windsor;
using AppActs.Mvp;
using log4net;
using AppActs.API.Service.Interface;
using System.Text;
using AppActs.Core.Di;

namespace AppActs.API.WebService.Base
{
    [XmlInclude(typeof(AppActs.API.WebService.Crash))]
    [XmlInclude(typeof(AppActs.API.WebService.Error))]
    [XmlInclude(typeof(AppActs.API.WebService.Event))]
    [XmlInclude(typeof(AppActs.API.WebService.Feedback))]
    [XmlInclude(typeof(AppActs.API.WebService.User))]
    [XmlInclude(typeof(AppActs.API.WebService.SystemError))]
    [XmlInclude(typeof(AppActs.API.WebService.Location))]
    [XmlInclude(typeof(AppActs.API.WebService.Upgrade))]
    public class HttpHandlerBase
    {
        [XmlIgnore]
        public IServiceLocator ServiceLocator
        {
            get
            {
                return (IServiceLocator)HttpContext.Current.Application[ContainerKeys.APPLICATION];
            }
        }

        [XmlIgnore]
        public IDeviceService DeviceService
        {
            get
            {
                return this.ServiceLocator.Resolve<IDeviceService>();
            }
        }


        [XmlIgnore]
        public ILog Logger
        {
            get
            {
                return ServiceLocator.Resolve<ILog>();
            }
        }

        [XmlIgnore]
        protected WebServiceResponseCodeType ResponseCodeType = WebServiceResponseCodeType.Ok;


        [XmlElement]
        public int ResponseCode
        {
            get
            {
                return (int)this.ResponseCodeType;
            }
            set
            {
                this.ResponseCodeType = (WebServiceResponseCodeType)value;
            }
        }

        [XmlIgnore]
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write(AppActs.Core.Xml.Serialization.Serialize<HttpHandlerBase>(this, new System.Xml.XmlWriterSettings() { Encoding = Encoding.UTF8 }));
        }
    }
}