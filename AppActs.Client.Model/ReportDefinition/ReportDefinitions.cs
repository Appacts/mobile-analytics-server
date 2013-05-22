using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AppActs.Client.Model
{
    public class ReportDefinitions : ConfigurationElementCollection
    {
        internal const string PropertyName = "ReportDefinition";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ReportDefinition();
        }

        public ReportDefinition this[int idx]
        {
            get { return (ReportDefinition)BaseGet(idx); }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ReportDefinition)element).Name;
        }
    }
}
