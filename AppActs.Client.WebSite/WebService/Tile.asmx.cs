using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AppActs.Client.Model;
using AppActs.Client.Model.Enum;
using Castle.Windsor;
using AppActs.Mvp;
using AppActs.Client.Service.Interface;
using log4net;
using AppActs.Core.Exceptions;
using System.Web.Script.Serialization;
using AppActs.Client.WebSite.App_Base;
using System.Threading;
using AppActs.Core.Di;
using MongoDB.Bson;

namespace AppActs.Client.WebSite.WebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Tile : System.Web.Services.WebService
    {
        private static IServiceLocator serviceLocator;
        private static object handle = new object();

        private static IServiceLocator getsServiceLocator()
        {
            if (serviceLocator == null)
            {
                lock (handle)
                {
                    serviceLocator = (IServiceLocator)HttpContext.Current.Application[ContainerKeys.APPLICATION];
                }
            }

            return serviceLocator;
        }

        private static ITileService getTileService()
        {
            return getsServiceLocator().Resolve<ITileService>();
        }

        private static ILog getLog()
        {
            return getsServiceLocator().Resolve<ILog>();
        }

        [WebMethod]
        public Model.Tile GetTile(Guid tileGuid, Guid applicationId, DateTime dateStart, DateTime dateEnd, 
            DateTime? dateStartCompare, DateTime? dateEndCompare)
        {
            try
            {
                return (Model.Tile)getTileService().GetTile(tileGuid, applicationId, 
                    dateStart, dateEnd, dateStartCompare, dateEndCompare);
            }
            catch (Exception ex)
            {
                getLog().Error(ex);
            }

            return null;
        }
    }
}
