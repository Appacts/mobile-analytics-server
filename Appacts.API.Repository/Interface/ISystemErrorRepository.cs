using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.SystemError;

namespace AppActs.API.Repository.Interface
{
    public interface ISystemErrorRepository
    {
        void Save(SystemError systemError);
    }
}
