using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using AppActs.Client.Model;

namespace AppActs.Client.WebSite.App_Base
{
    public class DatePickerConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(DatePicker) })); }
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DatePicker timelineBookmark = obj as DatePicker;

            if (timelineBookmark != null)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("Start", timelineBookmark.StartDate.ToString("yyyy/MM/dd"));
                dictionary.Add("End", timelineBookmark.EndDate.ToString("yyyy/MM/dd"));
                return dictionary;
            }

            return null;
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            //we don't deserialize
            return null;
        }
    }
}