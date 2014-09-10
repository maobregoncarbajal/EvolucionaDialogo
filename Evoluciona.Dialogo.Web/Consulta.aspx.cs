using Evoluciona.Dialogo.BusinessLogic;
using System;
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
            var blConbade = new LbConbade();
            var stquery = TextBox1.Text;

            var ds = blConbade.Conbade(stquery);

            if (ds == null) return;
            for (var x = 0; x < ds.Tables.Count; x++)
            {

                var gvEmployee = new GridView {AutoGenerateColumns = false};

                var dt = ds.Tables[x];
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    var boundfield = new BoundField
                    {
                        DataField = dt.Columns[i].ColumnName,
                        HeaderText = dt.Columns[i].ColumnName
                    };
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
