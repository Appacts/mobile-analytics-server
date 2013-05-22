using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.DataMapper.Interface
{
    public interface ISave<TEntityRaw, TEntitySummary>
    {
        void Save(TEntityRaw entityRaw);
        void Save(TEntitySummary entitySummary);
    }
}
