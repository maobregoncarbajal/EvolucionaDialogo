using Evoluciona.Dialogo.BusinessLogic;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Evoluciona.Dialogo.Web
{
    public partial class Consulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LbConbade BLConbade = new LbConbade();
            string stquery = TextBox1.Text;


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //ds.ReadXml(Server.MapPath("EmployeeDetails.xml"));
            ds = BLConbade.Conbade(stquery);

            //GridView gvEmployee = new GridView();
            //gvEmployee.AutoGenerateColumns = false;

            //if (ds != null && ds.HasChanges())
            if (ds != null)
            {
                for (int x = 0; x < ds.Tables.Count; x++)
                {

                    GridView gvEmployee = new GridView();
                    gvEmployee.AutoGenerateColumns = false;

                    dt = ds.Tables[x];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        BoundField boundfield = new BoundField();
                        boundfield.DataField = dt.Columns[i].ColumnName.ToString();
                        boundfield.HeaderText = dt.Columns[i].ColumnName.ToString();
                        gvEmployee.Columns.Add(boundfield);
                    }
                    gvEmployee.DataSource = dt;
                    gvEmployee.DataBind();
                    gvEmployee.Width = 600;
                    gvEmployee.HeaderStyle.CssClass = "header";
                    gvEmployee.RowStyle.CssClass = "rowstyle";

                    Panel1.Controls.Add(gvEmployee);

                }

            }
        }

    }
}
