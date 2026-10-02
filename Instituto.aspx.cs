using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TutoriasWeb;

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
                add.Visible = true;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int ID_Instituto = Convert.ToInt32(e.CommandArgument);
                dt = ta.GetData();
                nombre.Text = dt[0]["Nombre"].ToString();
                logo.Text = dt[0]["Logo"].ToString();
                sitio.Text = dt[0]["Sitio"].ToString();
                Btn_cancel.Visible = true;
                Btn_addInst.Text = "Actualizar";
                ID.Text = ID_Instituto.ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal();", true);
            }
            else if (e.CommandName == "Eliminar")
            {
                int idInstituto = Convert.ToInt32(e.CommandArgument);
                try
                {
                    ta.Delete(idInstituto); 
                    actualiza();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Instituto eliminado correctamente');", true);
                }
                catch (SqlException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", $"alert('Error al eliminar: {ex.Message}');", true);
                }
            }
            else if (e.CommandName == "Carreras")
            {
                string ID_Instiuto = e.CommandArgument.ToString();
                Response.Redirect("Carreras.aspx?id=" + ID_Instiuto);
            }
            else if (e.CommandName == "Administradores")
            {
                string ID_Instituto = e.CommandArgument.ToString();
                Response.Redirect("Administradores.aspx?idInstituto=" + ID_Instituto);
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
            if (string.IsNullOrWhiteSpace(nombre.Text) || string.IsNullOrWhiteSpace(logo.Text) || string.IsNullOrWhiteSpace(sitio.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Todos los campos son obligatorios');", true);
                return;
            }

            try
            {
                if (Btn_addInst.Text == "Agregar")
                {
                    
                    // 👉 INSERTAR (NO SE USA ID)
                    ta.Insert(nombre.Text.Trim().ToUpper(), logo.Text.Trim(), sitio.Text.Trim());
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Instituto agregado');", true);
                }
                else
                {
                    // 👉 ACTUALIZAR (SE USA ID)
                    ta.Update(nombre.Text.Trim().ToUpper(), logo.Text.Trim(), sitio.Text.Trim(), Convert.ToInt32(ID.Text));
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Instituto actualizado');", true);
                }

                cleanModal();
                actualiza();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }

    }
}

