using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.WebService
{
    public enum WebServiceResponseCodeType
    {
        Ok = 100,
        InactiveApplicationException = 102,
        NoDeviceException = 103,
        GeneralError = 104
    }
}
