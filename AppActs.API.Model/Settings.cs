using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model
{
    public class Settings : AppActs.Model.Settings
    {
        public bool DataLoggingRecordRaw { get; set; }
        public bool DataLoggingRecordSystemErrors { get; set; }
    }
}
