using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model;
using AppActs.API.Model.Error;

namespace AppActs.API.Repository.Interface
{
    public interface IErrorRepository
    {
        void Save(Error entity);

        void Save(ErrorSummary entity);
    }
}
