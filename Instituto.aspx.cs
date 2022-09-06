using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class Instituto : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.InstitutoTableAdapter ta = new dsTutoriasTableAdapters.InstitutoTableAdapter();
        dsTutorias.InstitutoDataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                actualiza();
            }
        }

        protected void actualiza()
        {
            dt = ta.GetData();

            if(dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                add.Visible = false;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "Editar")
            {
                dt = ta.GetData();
                nombre.Text = dt[0][1].ToString().Replace("  ", "");
                logo.Text = dt[0][2].ToString().Replace("  ", "");
                sitio.Text = dt[0][3].ToString().Replace("  ", "");
                Btn_cancel.Visible = true;
                Btn_addInst.Text = "Actualizar";
                ID.Text = e.CommandArgument.ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
            }
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }
        protected void cleanModal()
        {
            nombre.Text = "";
            logo.Text = "";
            sitio.Text = "";
            Btn_cancel.Visible = false;
            Btn_addInst.Text = "Agregar";
        }

        protected void Btn_addInst_Click(object sender, EventArgs e)
        {
            if(nombre.Text.Replace(" ","") == "" || logo.Text.Replace(" ","") == "" || sitio.Text.Replace(" ","") == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Debe llenar todos los campos'); ", true);
                return;
            }
            if(Btn_addInst.Text == "Agregar")
            {
                ta.Insert(nombre.Text.ToUpper().Replace("  ", ""), logo.Text, sitio.Text);
            }
            else
            {
                ta.Update(nombre.Text.ToUpper().Replace("  ", ""), logo.Text.Replace("  ", ""), sitio.Text.Replace("  ",""), Convert.ToInt32(ID.Text));
            }
            cleanModal();
            actualiza();
        }
    }
}