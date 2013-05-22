using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ReportTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                MongoClient client = new MongoClient(this.txtConnectionString.Text);
                MongoDatabase datbase = client.GetServer().GetDatabase(this.txtDatabase.Text);

                BsonValue value = datbase.Eval
                    (
                        new MongoDB.Bson.BsonJavaScript(this.txtJavaScript.Text),
                        new Guid(this.txtApplicationId.Text),
                        DateTime.Parse(this.txtStartDate.Text),
                        DateTime.Parse(this.txtEndDate.Text),
                        DateTime.Parse(this.txtStartDateCompare.Text),
                        DateTime.Parse(this.txtEndDateCompare.Text)
                    );

                this.txtResult.Text = value.ToJson();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, "Error");
            }
        }
    }
}
