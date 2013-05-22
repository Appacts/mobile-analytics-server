using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace AppActs.Client.Model
{
    public class ReportDefinitionNestedValue : ConfigurationElement
    {
        protected override void DeserializeElement(XmlReader reader, bool s)
        {
            Value = reader.ReadElementContentAs(typeof(string), null) as string;
        }

        public string Value { get; private set; }
    }
}
