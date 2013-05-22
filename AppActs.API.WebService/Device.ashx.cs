using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppActs.API.WebService.Base;
using AppActs.API.Service.Interface;
using AppActs.Core.Exceptions;
using AppActs.Model;
using AppActs.Model.Enum;
using System.Xml.Serialization;
using MongoDB.Bson;
using AppActs.API.Model.Exception;
using AppActs.API.Model.Device;
using AppActs.API.Model;

namespace AppActs.API.WebService
{

    public class Device : HttpHandlerBase<String>, IHttpHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            try
            {
                DeviceInfo deviceInfo = new DeviceInfo(context.Request.QueryString);
                ApplicationInfo applicationInfo = new ApplicationInfo(context.Request.QueryString);
                this.DeviceService.Log(deviceInfo, applicationInfo);
                this.Object = deviceInfo.Guid.ToString();
            }
            catch (InactiveApplicationException)
            {
                this.ResponseCodeType = WebServiceResponseCodeType.InactiveApplicationException;
            }
            catch (NoDeviceException)
            {
                this.ResponseCodeType = WebServiceResponseCodeType.NoDeviceException;
            }
            catch (Exception ex)
            {
                this.ResponseCodeType = WebServiceResponseCodeType.GeneralError;
                this.Logger.Error("Device", ex);
            }

            base.ProcessRequest(context);
        }

    }
}