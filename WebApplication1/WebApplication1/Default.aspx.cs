using KPI_Dashboard.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        public string val1 = "90deg";

        public string val2 = "90deg";

        public string colorCode = "#d5dae2";

        public string val3 = "90deg";

        public string val4 = "90deg";

        public string val5 = "90deg";

        public string val6 = "90deg";
        public string colorCode2 = "#d5dae2";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               //DashboardPresenter db = new DashboardPresenter();

               // Session["Database"] = db;
              // PopulateProductComboBox();
                if (DropDownList2.SelectedItem != null)
                {

              ///  PopulateVersionComboBox();
                }

            }
        }

        private void CalculateActiveUsersAngle(int TotalUser, out string v1, out string v2, out string coll)
        {
            //int TotalUser = 50;  
            v1 = ""; v2 = ""; coll = "";
           // circularProgess.Style.Add("visibility", "visible");
            if (TotalUser == 0)
            {
                v2 = "90deg";
                v1 = "90deg";
                coll = "#d5dae2";
            }
            else if (TotalUser < 50 && TotalUser > 0)
            {
                double percentageOfWholeAngle = 360 * (Convert.ToDouble(TotalUser) / 100);
                v2 = (90 + percentageOfWholeAngle).ToString() + "deg";
                v1 = "90deg";
                coll = "#d5dae2";
            }
            else if (TotalUser > 50 && TotalUser < 100)
            {
                double percentage = 360 * (Convert.ToDouble(TotalUser) / 100);
                v1 = (percentage - 270).ToString() + "deg";
                v2 = "270deg";
                coll = "#AC2D36";
            }
            else if (TotalUser == 50)
            {
                v1 = "-90deg";
                v2 = "270deg";
                coll = "#AC2D36";
            }
            else if (TotalUser >= 100)
            {
                v1 = "90deg";
                v2 = "270deg";
                coll = "#AC2D36";
            }

            //ProgressText.InnerText = TotalUser + "%";

        }

        public void PopulateProductComboBox()
        {
            foreach (string product in ((DashboardPresenter)Session["Database"]).GetAllProduct())

            { DropDownList2.Items.Add(product); }

        }

        public void PopulateVersionComboBox()
        {
            DropDownList1.Items.Clear();
            string temp = DropDownList2.SelectedItem.ToString();
            foreach (string version in ((DashboardPresenter)Session["Database"]).GetAllVersion(temp))
            { DropDownList1.Items.Add(version); }
        }






        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedItem != null)
            {

                PopulateVersionComboBox();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //  string v = DropDownList1.SelectedItem.ToString();
            //  int s = ((DashboardPresenter)Session["Database"]).GetPercent(v);
            //  string t = s.ToString();
            //  Label4.Text = t;

            // ProgressText.InnerText =TextBox1.Text + "%";
            // Div2.InnerText = TextBox1.Text + "%";
            ////  CalculateActiveUsersAngle(16, out val1, out val2, out colorCode);
            //  CalculateActiveUsersAngle(Convert.ToInt32(TextBox1.Text), out val3, out val4, out colorCode2);

            ft.Attributes["data-percent"] = TextBox1.Text;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ft.Attributes["data-percent"] = TextBox1.Text;
        }
    }
}