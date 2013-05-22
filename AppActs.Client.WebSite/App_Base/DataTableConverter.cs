using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections;

namespace AppActs.Client.WebSite.App_Base
{
    public class DataTableConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(DataTable) })); }
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DataTable dataTable = obj as DataTable;

            if (dataTable != null)
            {
                Dictionary<string, object> dicRows = new Dictionary<string, object>();

                ArrayList list = new ArrayList();

                Action<Dictionary<string, object>, DataTable, int> populateColumns = this.populateColumnsOne;

                if (dataTable.Columns.Count == 2)
                    populateColumns = this.populateColumnsTwo;

                if (dataTable.Columns.Count == 3)
                    populateColumns = this.populateColumnsThree;

                if (dataTable.Columns.Count == 4)
                    populateColumns = this.populateColumnsFour;

                if (dataTable.Columns.Count == 5)
                    populateColumns = this.populateColumnsFive;

                if (dataTable.Columns.Count == 6)
                    populateColumns = this.populateColumnsSix;

                if (dataTable.Columns.Count == 7)
                    populateColumns = this.populateColumnsSeven;

                if (dataTable.Columns.Count == 8)
                    populateColumns = this.populateColumnsEight;

                if (dataTable.Columns.Count == 9)
                    populateColumns = this.populateColumnsNine;

                if (dataTable.Columns.Count == 10)
                    populateColumns = this.populateColumnsTen;

                if (dataTable.Columns.Count == 11)
                    populateColumns = this.populateColumnsEleven;

                if (dataTable.Columns.Count == 12)
                    populateColumns = this.populateColumnsTwelve;

                if (dataTable.Columns.Count == 13)
                    populateColumns = this.populateColumnsThirteen;

                if (dataTable.Columns.Count == 14)
                    populateColumns = this.populateColumnsFourteen;

                if (dataTable.Columns.Count == 15)
                    populateColumns = this.populateColumnsFithteen;

                for (int i = 0; i < dataTable.Rows.Count; i ++)
                {
                    Dictionary<string, object> dicRow = new Dictionary<string, object>();
                    populateColumns(dicRow, dataTable, i);
                    list.Add(dicRow);
                }

                dicRows.Add("Data", list);

                return dicRows;
            }

            return null;
        }

        private void populateColumnsOne(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            dicRow.Add(table.Columns[0].ColumnName, table.Rows[index][0]);
        }

        private void populateColumnsTwo(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsOne(dicRow, table, index);
            dicRow.Add(table.Columns[1].ColumnName, table.Rows[index][1]);
        }

        private void populateColumnsThree(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsTwo(dicRow, table, index);
            dicRow.Add(table.Columns[2].ColumnName, table.Rows[index][2]);
        }

        private void populateColumnsFour(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsThree(dicRow, table, index);
            dicRow.Add(table.Columns[3].ColumnName, table.Rows[index][3]);
        }

        private void populateColumnsFive(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsFour(dicRow, table, index);
            dicRow.Add(table.Columns[4].ColumnName, table.Rows[index][4]);
        }

        private void populateColumnsSix(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsFive(dicRow, table, index);
            dicRow.Add(table.Columns[5].ColumnName, table.Rows[index][5]);
        }

        private void populateColumnsSeven(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsSix(dicRow, table, index);
            dicRow.Add(table.Columns[6].ColumnName, table.Rows[index][6]);
        }

        private void populateColumnsEight(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsSeven(dicRow, table, index);
            dicRow.Add(table.Columns[7].ColumnName, table.Rows[index][7]);
        }

        private void populateColumnsNine(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsEight(dicRow, table, index);
            dicRow.Add(table.Columns[8].ColumnName, table.Rows[index][8]);
        }

        private void populateColumnsTen(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsNine(dicRow, table, index);
            dicRow.Add(table.Columns[9].ColumnName, table.Rows[index][9]);
        }

        private void populateColumnsEleven(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsTen(dicRow, table, index);
            dicRow.Add(table.Columns[10].ColumnName, table.Rows[index][10]);
        }

        private void populateColumnsTwelve(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsEleven(dicRow, table, index);
            dicRow.Add(table.Columns[11].ColumnName, table.Rows[index][11]);
        }

        private void populateColumnsThirteen(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsTwelve(dicRow, table, index);
            dicRow.Add(table.Columns[12].ColumnName, table.Rows[index][12]);
        }

        private void populateColumnsFourteen(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsThirteen(dicRow, table, index);
            dicRow.Add(table.Columns[13].ColumnName, table.Rows[index][13]);
        }

        private void populateColumnsFithteen(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsFourteen(dicRow, table, index);
            dicRow.Add(table.Columns[14].ColumnName, table.Rows[index][14]);
        }

        private void populateColumnsSixteen(Dictionary<string, object> dicRow, DataTable table, int index)
        {
            this.populateColumnsFithteen(dicRow, table, index);
            dicRow.Add(table.Columns[15].ColumnName, table.Rows[index][15]);
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            //we don't deserialize
            return null;
        }
    }
}