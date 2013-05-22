using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppActs.API.WebService.Base;
using AppActs.Core.Exceptions;
using AppActs.Model;
using AppActs.API.Model;
using MongoDB.Bson;
using AppActs.API.Model.Exception;
using AppActs.API.Model.Enum;

namespace AppActs.API.WebService
{
    public class Event : HttpHandlerBase, IHttpHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            try
            {
                this.DeviceService.Log(new Model.Event.Event(context.Request.QueryString));
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
                this.Logger.Error("Event", ex);
            }

            base.ProcessRequest(context);
        }
    }
}