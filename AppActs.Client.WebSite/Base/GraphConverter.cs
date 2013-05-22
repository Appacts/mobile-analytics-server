using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using AppActs.Client.Data.Model;
using System.Collections;

namespace AppActs.Client.WebSite.Base
{
    public class GraphConverter : JavaScriptConverter
    {
        /// <summary>
        /// When overridden in a derived class, gets a collection of the supported types.
        /// </summary>
        /// <returns>An object that implements <see cref="T:System.Collections.Generic.IEnumerable`1"/> that represents the types supported by the converter.</returns>
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(Graph) })); }
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
            Graph graph = obj as Graph;

            if (graph != null)
            {
                if (graph.ChartType != Data.Model.Enum.ChartType.Pie)
                {
                    return serializeChart(graph);
                }
                else
                {
                    return serializeChartPie(graph);
                }
            }

            return new Dictionary<string, object>();
        }

        /// <summary>
        /// Serializes the chart.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <returns></returns>
        private static IDictionary<string, object> serializeChart(Graph graph)
        {
            Type typeDateTime = typeof(DateTime);

            Dictionary<string, int> dicXtype = new Dictionary<string, int>();

            //cache x values if we are working with strings, we will need ticker tape to display values
            if (graph.XType != typeDateTime && graph.Series.Count > 0 &&
                graph.Series[0].Axis.Count > 0)
            {
                for (int i = 0; i < graph.Series[0].Axis.Count; i++)
                {
                    dicXtype.Add(graph.Series[0].Axis[i].X, i);
                }
            }

            bool yyIsUsed = graph.YYLabel != null && graph.YYLabel.Length > 0;

            Dictionary<string, object> dictValueToObject = new Dictionary<string, object>();
            ArrayList arrayList = new ArrayList();

            Dictionary<string, object> dictYYToValues = new Dictionary<string, object>();

            foreach (GraphSeries series in graph.Series)
            {
                ArrayList arrayListYY = new ArrayList();

                Dictionary<string, object> dictList = new Dictionary<string, object>();
                dictList.Add("label", series.Name);

                ArrayList arrayListAxis = new ArrayList();

                foreach (GraphAxis axis in series.Axis)
                {
                    ArrayList arrayListXY = new ArrayList();

                    if (graph.XType == typeDateTime)
                    {
                        arrayListXY.Add((DateTime.Parse(axis.X) - new DateTime(1970, 1, 1)).TotalMilliseconds);
                    }
                    else
                    {
                        arrayListXY.Add(dicXtype[axis.X]);
                    }

                    arrayListXY.Add(axis.Y);

                    if(yyIsUsed)
                    {
                        
                        arrayListYY.Add(axis.YY);
                    }

                    arrayListAxis.Add(arrayListXY);
                }

                if (yyIsUsed)
                {
                    dictYYToValues.Add(series.Name, arrayListYY);
                }

                dictList.Add("data", arrayListAxis);
                arrayList.Add(dictList);
            }

            dictValueToObject.Add("Series", arrayList);

            if (graph.XType != typeDateTime)
            {
                ArrayList arrayListTicker = new ArrayList();
                foreach (KeyValuePair<string, int> ticker in dicXtype)
                {
                    ArrayList arrayListTickerValues = new ArrayList();
                    arrayListTickerValues.Add(ticker.Value);
                    arrayListTickerValues.Add(ticker.Key);
                    arrayListTicker.Add(arrayListTickerValues);
                }

                dictValueToObject.Add("Ticker", arrayListTicker);
            }

            if (yyIsUsed)
            {
                dictValueToObject.Add("YY", dictYYToValues);
            }

            return dictValueToObject;
        }

        /// <summary>
        /// Serializes the chart pie.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <returns></returns>
        private static IDictionary<string, object> serializeChartPie(Graph graph)
        {
            ArrayList arrayList = new ArrayList();
            GraphSeries series = graph.Series[0];

            Dictionary<string, object> dictValueToObject = new Dictionary<string, object>();
            foreach (GraphAxis graphAxis in series.Axis)
            { 
                Dictionary<string, object> dictList = new Dictionary<string, object>();
                dictList.Add("label", graphAxis.X);
                dictList.Add("data", graphAxis.Y);
                arrayList.Add(dictList);
            }

            dictValueToObject.Add("Series", arrayList);
            return dictValueToObject;
        }

        /// <summary>
        /// Converts the provided dictionary into an object of the specified type. 
        /// </summary>
        /// <param name="dictionary">An <see cref="IDictionary{TKey,TValue}"/> instance of property data stored as name/value pairs. </param>
        /// <param name="type">The type of the resulting object.</param>
        /// <param name="serializer">The <see cref="JavaScriptSerializer"/> instance. </param>
        /// <returns>The deserialized object. </returns>
        /// <exception cref="InvalidOperationException">We only serialize</exception>
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new InvalidOperationException("We only serialize");
        }
    }
}