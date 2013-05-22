using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Error;

namespace AppActs.API.DataMapper.Interface
{
    public interface IErrorMapper : ISave<Error, ErrorSummary>
    {

    }
}
