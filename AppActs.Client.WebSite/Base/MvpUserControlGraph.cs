using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppActs.Client.Interface;

using AppActs.Client.Presenter.Interface;
using AppActs.Client.Data.Model.Instance;
using AppActs.Client.Presenter;
using System.Web.UI.DataVisualization.Charting;
using AppActs.Client.Data.Model;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Script.Serialization;
using AppActs.Client.Data.Model.Enum;

namespace AppActs.Client.WebSite.Base
{
    public abstract class MvpUserControlGraph<TypeOfInstance> : 
        MvpUserControl<IGraphWithInstanceView<TypeOfInstance>,
                        IGraphWithInstancePresenter<TypeOfInstance>,
                        GraphWithInstancePresenter<IGraphWithInstanceView<TypeOfInstance>, TypeOfInstance>>

        where TypeOfInstance : Instance
    {
        #region //Events
        /// <summary>
        /// Occurs when [on graph updated].
        /// </summary>
        public event EventHandler GraphUpdated;
        #endregion

        #region //Properties
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public Unit Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public Unit Height { get; set; }

        /// <summary>
        /// Gets the chart.
        /// </summary>
        /// <returns></returns>
        protected abstract System.Web.UI.DataVisualization.Charting.Chart Chart { get; }

        /// <summary>
        /// Gets the views.
        /// </summary>
        protected abstract MultiView Views { get; }

        /// <summary>
        /// Gets the json.
        /// </summary>
        protected string Json { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is time.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is time; otherwise, <c>false</c>.
        /// </value>
        protected Type XType { get; private set; }

        /// <summary>
        /// Gets the X label.
        /// </summary>
        protected string XLabel { get; private set; }

        /// <summary>
        /// Gets the Y label.
        /// </summary>
        protected string YLabel { get; private set; }

        /// <summary>
        /// Gets the YY label.
        /// </summary>
        protected string YYLabel { get; private set; }

        /// <summary>
        /// Gets the type of the chart.
        /// </summary>
        /// <value>
        /// The type of the chart.
        /// </value>
        protected ChartType ChartType { get; private set; }
        #endregion

        #region //Control Methods
        /// <summary>
        /// Raises the <see cref="E:Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Chart.Palette = ChartColorPalette.None;
            this.Chart.PaletteCustomColors = Lists.GraphColorPallet;
            this.Chart.PrePaint += new EventHandler<ChartPaintEventArgs>(Chart_PrePaint);
            this.Chart.Width = this.Width;
            this.Chart.Height = this.Height;
            this.Chart.BackColor = Color.Transparent;
            this.Chart.Visible = false;

            this.Chart.Series.Clear();
        }
        #endregion

        #region //Methods
        /// <summary>
        /// Binds the specified graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="chart">The chart.</param>
        /// <param name="multiView">The multi view.</param>
        protected virtual void DataBind(Data.Model.Graph graph)
        {
            if (graph.GraphType == Data.Model.Enum.GraphType.Static)
            {
                this.Chart.Visible = true;
                this.dataBindGraphStatic(graph);
            }
            else
            {
                this.dataBindGraphDynamic(graph);
            }

            if (this.GraphUpdated != null)
            {
                this.GraphUpdated(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the PrePaint event of the Chart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.DataVisualization.Charting.ChartPaintEventArgs"/> instance containing the event data.</param>
        protected void Chart_PrePaint(object sender, ChartPaintEventArgs e)
        {
            e.ChartGraphics.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }
        #endregion

        #region //Private Properties
        /// <summary>
        /// Datas the bind graph dynamic.
        /// </summary>
        /// <param name="graph">The graph.</param>
        private void dataBindGraphDynamic(Data.Model.Graph graph)
        {
            if (graph.ContainsData())
            {
                this.XType = graph.XType;
                this.XLabel = graph.XLabel;
                this.YLabel = graph.YLabel;
                this.YYLabel = graph.YYLabel;
                this.ChartType = graph.ChartType;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new JavaScriptConverter[] { new GraphConverter() });

                this.Json = serializer.Serialize(graph);
                this.Views.ActiveViewIndex = 1;
            }
            else
            {
                this.Views.ActiveViewIndex = 2;
            }
        }

        /// <summary>
        /// Datas the bind graph static.
        /// </summary>
        /// <param name="graph">The graph.</param>
        private void dataBindGraphStatic(Data.Model.Graph graph)
        {
            SeriesChartType seriesChartType =
                AppActs.Client.WebSite.Base.Dictionaries.chartTypeToSeriesChartType[graph.ChartType];

            Font fontTitle = new Font("Verdana", 10, FontStyle.Regular);
            Font FontAxes = new Font("Verdana", 8, FontStyle.Regular);

            this.Chart.ChartAreas["ChartAreaMain"].AxisX.LabelStyle.Font = FontAxes;
            this.Chart.ChartAreas["ChartAreaMain"].AxisX.TitleFont = fontTitle;
            this.Chart.ChartAreas["ChartAreaMain"].AxisX.Title = graph.XLabel;

            this.Chart.ChartAreas["ChartAreaMain"].AxisY.LabelStyle.Font = FontAxes;
            this.Chart.ChartAreas["ChartAreaMain"].AxisY.TitleFont = fontTitle;
            this.Chart.ChartAreas["ChartAreaMain"].AxisY.Title = graph.YLabel;

            if (graph.ContainsData())
            {
                foreach (GraphSeries graphSeries in graph.Series)
                {
                    Series series = new Series(graphSeries.Name);
                    series.ChartType = seriesChartType;

                    if (seriesChartType == SeriesChartType.Line)
                    {
                        series.MarkerStyle = MarkerStyle.Square;
                        series.BorderWidth = 4;
                        series.MarkerSize = 6;
                    }
                    else if (seriesChartType == SeriesChartType.Bar)
                    {
                        //series["DrawingStyle"] = "Cylinder";
                        series.BackImageWrapMode = ChartImageWrapMode.Scaled;
                    }
                    else if (seriesChartType == SeriesChartType.Pie)
                    {
                        this.Chart.Legends.Add(new Legend() { Enabled = true });
                        series["PieLabelStyle"] = "Disabled";
                        series["PieDrawingStyle"] = "Default";
                    }

                    if (String.IsNullOrEmpty(graph.YYLabel))
                    {
                        if (graph.Series.Count == 1)
                        {
                            series.ToolTip = String.Format("{0} #VALX\n{1} #VALY\n", graph.XLabel, graph.YLabel);
                        }
                        else
                        {
                            series.ToolTip = String.Format("{0}\n {1} #VALX\n{2} #VALY\n", graphSeries.Name, graph.XLabel, graph.YLabel);
                        }

                        for (int i = graphSeries.Axis.Count - 1; i >= 0; i--)
                        {
                            series.Points.AddXY(graphSeries.Axis[i].X, graphSeries.Axis[i].Y);
                        }
                    }
                    else
                    {
                        if (graph.Series.Count == 1)
                        {
                            series.ToolTip = String.Format("{0} #VALX\n{1} #VALY\n {2} [YY]", graph.XLabel, graph.YLabel, graph.YYLabel);
                        }
                        else
                        {
                            series.ToolTip = String.Format("{0} \n {1} #VALX\n{2} #VALY\n {3} [YY]", graphSeries.Name, graph.XLabel, graph.YLabel, graph.YYLabel);
                        }

                        for (int i = graphSeries.Axis.Count - 1; i >= 0; i--)
                        {
                            int index = series.Points.AddXY(graphSeries.Axis[i].X, graphSeries.Axis[i].Y.ToString("n2"));
                            series.Points[index].ToolTip = series.Points[index].ToolTip.Replace("[YY]", graphSeries.Axis[i].YY.ToString());
                        }
                    }

                    this.Chart.Series.Add(series);
                }

                this.Views.ActiveViewIndex = 0;
            }
            else
            {
                this.Views.ActiveViewIndex = 2;
            }

            //if there are more then 1 series we need to show legends
            if (this.Chart.Series.Count > 1)
            {
                this.Chart.Legends.Add(new Legend() { Enabled = true });
            }
        }
        #endregion
    }
}