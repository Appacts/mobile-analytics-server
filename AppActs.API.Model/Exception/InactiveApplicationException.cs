using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Core.Exceptions;
using MongoDB.Bson;

namespace AppActs.API.Model.Exception
{
    public class InactiveApplicationException : BaseException
    {
        public InactiveApplicationException(Guid applicationId)
            : base(string.Format("Application {0} is inactive", applicationId))
        {

        }
    }
}
