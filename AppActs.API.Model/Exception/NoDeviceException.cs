using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Core.Exceptions;
using MongoDB.Bson;

namespace AppActs.API.Model.Exception
{
    public class NoDeviceException : BaseException
    {
        public NoDeviceException(Guid deviceGuid)
            : base(String.Format("No {0} Device", deviceGuid))
        {
            
        }

    }
}
