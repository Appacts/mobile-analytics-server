using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppActs.API.WebService.Base;
using AppActs.API.Service.Interface;
using AppActs.Core.Exceptions;
using AppActs.Model;
using AppActs.Model.Enum;
using MongoDB.Bson;
using AppActs.API.Model.Exception;

namespace AppActs.API.WebService
{
    public class Crash : HttpHandlerBase, IHttpHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            try
            {
                this.DeviceService.Log(new Model.Crash.Crash(context.Request.QueryString));
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
                this.Logger.Error("Crash", ex);
            }

            base.ProcessRequest(context);
        }
    }
}