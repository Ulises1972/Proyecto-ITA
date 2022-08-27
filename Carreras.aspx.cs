using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class Carreras : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.CarreraTableAdapter ta = new dsTutoriasTableAdapters.CarreraTableAdapter();
        dsTutorias.CarreraDataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                actualizar();
            }
        }

        protected void Btn_addCarrera_Click(object sender, EventArgs e)
        {
            if(Btn_addCarrera.Text == "Agregar")
            {
                ta.Insert(ca_nombre.Text.ToUpper().Replace("  ", ""), ca_logo.Text.Replace(" ", ""), "ACTIVO", ca_omo.Text);
            }
            else
            {
                ta.Update(ca_nombre.Text.ToUpper().Replace("  ", ""), ca_logo.Text.Replace(" ", ""), "ACTIVO", ca_omo.Text, Convert.ToInt32(ID.Text));
            }
            ca_nombre.Text = "";
            ca_logo.Text = "";
            actualizar();
        }
        protected void actualizar()
        {
            dt = ta.GetData();

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ta.UpdateStatus("ELIMINADO", Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[0].Text));
            actualizar();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Editar")
            {
                dt = ta.GetDataByID(Convert.ToInt32(e.CommandArgument));
                ca_nombre.Text = dt[0][1].ToString().Replace("  ","");
                ca_logo.Text = dt[0][2].ToString().Replace("  ", "");
                ca_omo.Text = dt[0][4].ToString();
                Btn_cancel.Visible = true;
                Btn_addCarrera.Text = "Actualizar";
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
            ca_nombre.Text = "";
            ca_logo.Text = "";
            Btn_addCarrera.Text = "Agregar";
            Btn_cancel.Visible = false;
        }
    }
}