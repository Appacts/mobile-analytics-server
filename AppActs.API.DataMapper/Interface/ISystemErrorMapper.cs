using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.SystemError;

namespace AppActs.API.DataMapper.Interface
{
    public interface ISystemErrorMapper
    {
        void Save(SystemError entity);
    }
}
