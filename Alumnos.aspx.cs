using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TutoriasWeb
{
    public partial class Home : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.AlumnoTableAdapter ta = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dt;
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

        protected void Btn_addAlumno_Click(object sender, EventArgs e)
        {
            if (Btn_addAlumno.Text == "Agregar")
            {
                ta.Insert(Convert.ToInt32(al_id.Text), al_nombre.Text.ToUpper(), al_aPaterno.Text.ToUpper(), al_aMaterno.Text.ToUpper(), 1, Session["carrera"].ToString(), "NO ASIGNADO", 0);
            }
            else
            {
                dt = ta.GetDataByNo(Convert.ToInt32(al_id.Text));
                ta.Update(Convert.ToInt32(al_id.Text), al_nombre.Text.ToUpper(), al_aPaterno.Text.ToUpper(), al_aMaterno.Text.ToUpper(), Convert.ToInt32(dt[0][4].ToString()), dt[0][5].ToString(), dt[0][6].ToString(), Convert.ToByte(dt[0][7].ToString()), Convert.ToInt32(al_id.Text));                
            }
            cleanModal();
            actualiza();
        }

        protected void actualiza()
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
            al_id.Text = "";
            al_nombre.Text = "";
            al_aPaterno.Text = "";
            al_aMaterno.Text = "";
            Btn_addAlumno.Text = "Agregar";
            al_id.Enabled = true;
            Btn_cancel.Visible = false;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ta.UpdateStatus("ELIMINADO", Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[0].Text));
            actualiza();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "Editar")
            {
                dt = ta.GetDataByNo(Convert.ToInt32(e.CommandArgument.ToString()));
                al_id.Text = e.CommandArgument.ToString();
                al_id.Enabled = false;
                al_nombre.Text = dt[0][1].ToString();
                al_aPaterno.Text = dt[0][2].ToString();
                al_aMaterno.Text = dt[0][3].ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
                Btn_addAlumno.Text = "Actualizar";
                Btn_cancel.Visible = true;
            }
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            dt = ta.GetDataByNo(Convert.ToInt32(Tb_buscar.Text));

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('No existen registros con dicho No. Control. Intente de nuevo')", true);
            }
        }
    }
}