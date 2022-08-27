using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TutoriasWeb
{
    public partial class Maestros : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.MaestroTableAdapter ta = new dsTutoriasTableAdapters.MaestroTableAdapter();
        dsTutorias.MaestroDataTable dt;
        
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ta.UpdateStatus("ELIMINADO", Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[0].Text));
            actualizar();
        }


        protected void Btn_addTutor_Click(object sender, EventArgs e)
        {
            if(Btn_addTutor.Text == "Agregar")
            {
                ta.Insert(tu_rfc.Text.ToUpper().Replace("  ", ""), tu_nombre.Text.ToUpper().Replace("  ", ""), tu_aPaterno.Text.ToUpper().Replace("  ", ""), tu_aMaterno.Text.ToUpper().Replace("  ", ""), "", Session["carrera"].ToString(), "ACTIVO");
            }
            else
            {
                dt = ta.GetDataByID(Convert.ToInt32(ID.Text));
                ta.Update(dt[0][1].ToString(), tu_nombre.Text.ToUpper().Replace("  ", ""), tu_aPaterno.Text.ToUpper().Replace("  ", ""), tu_aMaterno.Text.ToUpper().Replace("  ", ""), dt[0][5].ToString(), dt[0][6].ToString(), "ACTIVO", Convert.ToInt32(ID.Text));
            }
            cleanModal();
            actualizar();
        }

        protected void actualizar()
        {
            dt = ta.GetData(Session["carrera"].ToString());

            if(dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            
        }
        protected void cleanModal()
        {
            tu_rfc.Text = "";
            tu_nombre.Text = "";
            tu_aPaterno.Text = "";
            tu_aMaterno.Text = "";
            Btn_addTutor.Text = "Agregar";
            tu_rfc.Enabled = true;
            Btn_cancel.Visible = false;
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "Editar")
            {
                ID.Text = e.CommandArgument.ToString();
                dt = ta.GetDataByID(Convert.ToInt32(ID.Text));
                tu_rfc.Text = dt[0][1].ToString();
                tu_rfc.Enabled = false;
                tu_nombre.Text = dt[0][2].ToString();
                tu_aPaterno.Text = dt[0][3].ToString();
                tu_aMaterno.Text = dt[0][4].ToString();
                Btn_cancel.Visible = true;
                Btn_addTutor.Text = "Actualizar";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal();", true);
            }
            
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void Btn_uscar_Click(object sender, EventArgs e)
        {
            dt = ta.GetDataByRFC(Tb_buscar.Text);

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                GridView1.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No existen registros con dicho RFC. Intente de nuevo')", true);
            }
        }
    }
}