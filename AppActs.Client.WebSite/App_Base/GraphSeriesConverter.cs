using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using AppActs.Client.Model;
using System.Collections.ObjectModel;
using System.Collections;

namespace AppActs.Client.WebSite.App_Base
{
    public class GraphSeriesConverter : JavaScriptConverter
    {
        /// <summary>
        /// When overridden in a derived class, gets a collection of the supported types.
        /// </summary>
        /// <returns>An object that implements <see cref="T:System.Collections.Generic.IEnumerable`1"/> that represents the types supported by the converter.</returns>
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(List<GraphSeries>) })); }
        }

        /// <summary>
        /// When overridden in a derived class, builds a dictionary of name/value pairs.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="serializer">The object that is responsible for the serialization.</param>
        /// <returns>
        /// An object that contains key/value pairs that represent the object’s data.
        /// </returns>
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            List<GraphSeries> serieses = obj as List<GraphSeries>;

            if (serieses != null)
            {
                IDictionary<string, object> keyToValue = new Dictionary<string, object>();
                ArrayList arrayList = new ArrayList();

                int seriesesCount = serieses.Count;

                if(seriesesCount >= 1)
                {
                    int axisCount = serieses[0].Axis.Count;

                    //pre determine load type to save on speed
                    Action<Dictionary<string, object>, List<GraphSeries>, int> 
                        populateColumns = this.populateColumnsOne;

                    Action<IDictionary<string, object>, List<GraphSeries>>
                        populateHeader = this.populateHeaderOne;

                    if (seriesesCount == 2)
                    {
                        populateColumns = this.populateColumnsTwo;
                        populateHeader = this.populateHeaderTwo;
                    }

                    if (seriesesCount == 3)
                    {
                        populateColumns = this.populateColumnsThree;
                        populateHeader = this.populateHeaderThree;
                    }
 

                    for (int i = 0; i < axisCount; i++)
                    {
                        Dictionary<string, object> dicRow = new Dictionary<string, object>();
                        populateColumns(dicRow, serieses, i);
                        arrayList.Add(dicRow);
                    }

                    populateHeader(keyToValue, serieses);
                }
                
                keyToValue.Add("Data", arrayList);
                return keyToValue;
            }

            return new Dictionary<string, object>();
        }

        private void populateColumnsOne(Dictionary<string, object> dicRow, List<GraphSeries> serieses, int index)
        {
            dicRow.Add("Column0", serieses[0].Axis[index].X);
            dicRow.Add("Column1", serieses[0].Axis[index].Y);
        }

        private void populateColumnsTwo(Dictionary<string, object> dicRow, List<GraphSeries> serieses, int index)
        {
            dicRow.Add("Column0", serieses[0].Axis[index].X);
            dicRow.Add("Column1", serieses[0].Axis[index].Y);
            dicRow.Add("Column2", serieses[1].Axis[index].Y);
        }

        private void populateColumnsThree(Dictionary<string, object> dicRow, List<GraphSeries> serieses, int index)
        {
            dicRow.Add("Column0", serieses[0].Axis[index].X);
            dicRow.Add("Column1", serieses[0].Axis[index].Y);
            dicRow.Add("Column2", serieses[1].Axis[index].Y);
            dicRow.Add("Column3", serieses[2].Axis[index].Y);
        }

        private void populateHeaderOne(IDictionary<string, object> keyToValue, List<GraphSeries> serieses)
        {
            keyToValue.Add("Header0", serieses[0].Name);
        }

        private void populateHeaderTwo(IDictionary<string, object> keyToValue, List<GraphSeries> serieses)
        {
            keyToValue.Add("Header0", serieses[0].Name);
            keyToValue.Add("Header1", serieses[1].Name);
        }

        private void populateHeaderThree(IDictionary<string, object> keyToValue, List<GraphSeries> serieses)
        {
            keyToValue.Add("Header0", serieses[0].Name);
            keyToValue.Add("Header1", serieses[1].Name);
            keyToValue.Add("Header2", serieses[2].Name);
        }

        /// <summary>
        /// When overridden in a derived class, converts the provided dictionary into an object of the specified type.
        /// </summary>
        /// <param name="dictionary">An <see cref="T:System.Collections.Generic.IDictionary`2"/> instance of property data stored as name/value pairs.</param>
        /// <param name="type">The type of the resulting object.</param>
        /// <param name="serializer">The <see cref="T:System.Web.Script.Serialization.JavaScriptSerializer"/> instance.</param>
        /// <returns>
        /// The deserialized object.
        /// </returns>
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}