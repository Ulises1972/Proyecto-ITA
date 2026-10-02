using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TutoriasWeb;
using System.Configuration;


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
                return;
            }

            if (!IsPostBack)
            {
                // 3. Obtener ID de carrera
                if (int.TryParse(Request.QueryString["id"], out int ID_Carrera))
                {
                    // Opcional: Debuggear
                    

                    // 4. Guardar ID para uso posterior
                    ViewState["ID_Carrera"] = ID_Carrera;

                    // 5. Cargar datos filtrados
                    CargarDatos(ID_Carrera);
                }
                else
                {
                    Response.Redirect("Carreras.aspx");
                }
            }
        }



        private void CargarDatos(int ID_Carrera)
        {
            dt = ta.GetDataByCarrera(ID_Carrera);
            GridView1.DataSource = dt;
            GridView1.DataBind(); 

            GridView1.Visible = dt.Rows.Count > 0;

      
        }






        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var keyValue = GridView1.DataKeys[e.RowIndex].Value?.ToString();

            if (int.TryParse(keyValue, out int id))
            {
                try
                {
                    ta.Delete(id);
                    if (ViewState["ID_Carrera"] != null)
                    {
                        int ID_Carrera = Convert.ToInt32(ViewState["ID_Carrera"]);
                        CargarDatos(ID_Carrera); 
                    }
                    ShowAlert("Tutor eliminado correctamente");
                }
                catch (Exception ex)
                {
                    ShowAlert($"Error al eliminar: {ex.Message}");
                }
            }
            else
            {
                ShowAlert("Error: ID del tutor no es válido.");
            }
        }

        private void ShowAlert(string message)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", $"alert('{message}');", true);
        }

        protected void Btn_addTutor_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tu_rfc.Text) || string.IsNullOrWhiteSpace(tu_nombre.Text) ||
                    string.IsNullOrWhiteSpace(tu_aPaterno.Text))
                {
                    ShowAlert("Complete los campos obligatorios");
                    return;
                }
                if (ViewState["EditID"] != null)
                {
                    int id = (int)ViewState["EditID"];
                    ta.Update1(
                        tu_rfc.Text.Trim().ToUpper(),
                        tu_nombre.Text.Trim().ToUpper(),
                        tu_aPaterno.Text.Trim().ToUpper(),
                        tu_aMaterno.Text.Trim().ToUpper(),
                        id
                    );
                    ShowAlert("Maestro actualizado correctamente");
                }
                else
                {
                    string usuario = tu_rfc.Text.Trim().ToUpper();
                    ta.Insert1(
                        tu_rfc.Text.Trim().ToUpper(),
                        usuario,
                        tu_nombre.Text.Trim().ToUpper(),
                        tu_aPaterno.Text.Trim().ToUpper(),
                        tu_aMaterno.Text.Trim().ToUpper(),
                        usuario + "123",
                        "ACTIVO",
                        (int)ViewState["ID_Carrera"]
                    );
                    ShowAlert("Maestro agregado correctamente");
                }
                cleanModal();
                actualizar();
                ViewState["EditID"] = null;
            }
            catch (Exception ex)
            {
                ShowAlert($"Error: {(ex.Message.Contains("duplicate") ? "RFC ya existe" : ex.Message)}");
            }
        }




        protected void actualizar()
        {
            if (ViewState["ID_Carrera"] == null)
            {
                GridView1.Visible = false;
                return;
            }

            int ID_Carrera = Convert.ToInt32(ViewState["ID_Carrera"]);

            // Aquí solo llamamos método que filtra maestros por carrera
            dt = ta.GetDataByCarrera(ID_Carrera);

            if (dt != null && dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.Visible = false;
            }

            GridView1.DataBind();
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
            ViewState["EditID"] = null;
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                // Obtener el ID del maestro a editar
                int id = Convert.ToInt32(e.CommandArgument);

                // Cargar datos del maestro
                dt = ta.GetDataByID(id);

                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    tu_rfc.Text = row["RFC"].ToString();
                    tu_nombre.Text = row["Nombre"].ToString();
                    tu_aPaterno.Text = row["A_Paterno"].ToString();
                    tu_aMaterno.Text = row["A_Materno"].ToString();

                    // Guardar ID en ViewState para usarlo al guardar
                    ViewState["EditID"] = id;

                    // Configurar botones
                    Btn_addTutor.Text = "Guardar Cambios";
                    tu_rfc.Enabled = false; // No permitir editar RFC
                    Btn_cancel.Visible = true;

                    // Mostrar modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "openModal", "$('#mdl_maestro').modal('show');", true);
                }
            }
            // ... (mantén el resto de tu código existente)
        
            if (e.CommandName == "Tutores")
            {
                if (int.TryParse(e.CommandArgument.ToString(), out int ID_Carrera))
                {
                    // Redirige a la página de Maestros con el ID de la carrera
                    Response.Redirect($"Tutores.aspx?id={ID_Carrera}");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('ID de carrera no válido para Maestros.');", true);

                }
            }

        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            actualizar();
        }
    }
}