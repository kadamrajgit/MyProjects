using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WinFormMobile
{
    public partial class Mobile : Form
    {
        public Mobile()
        {
            InitializeComponent();
        }

        private void Mobile_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            string apiUrl = "https://apimobiledetails.azurewebsites.net/api/mobile";
            object input = new
            {
                CompanyName = ""//OnePlus
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            System.Net.WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            
            string json = client.UploadString(apiUrl  + "/GetListMobile/", inputJson);

            dataGridView1.DataSource = (new JavaScriptSerializer()).Deserialize<List<MobileModel>>(json);
        }
    }
}
