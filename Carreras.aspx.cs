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
        dsTutoriasTableAdapters.MaestroTableAdapter taMaestros = new dsTutoriasTableAdapters.MaestroTableAdapter();
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
                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out int ID_Instituto))
                {
                    ViewState["ID_Instituto"] = ID_Instituto;
                    actualizar();
                }
                else
                {
                    Response.Redirect("Instituto.aspx");
                }
            }
            else
            {
                // En postbacks, recuperamos el ID desde ViewState
                int ID_Instituto = Convert.ToInt32(ViewState["ID_Instituto"]);
            }
        }

        protected void btnArmarGrupos_Click(object sender, EventArgs e)
        {
            ShowAlert("Hola Mudno");
        }


        protected void Btn_addCarrera_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(ca_nombre.Text) ||
                string.IsNullOrWhiteSpace(ca_logo.Text) ||
                string.IsNullOrWhiteSpace(ca_omo.Text))
            {
                ShowAlert("Debe llenar todos los campos");
                return;
            }

           

            try
            {
                int idInstituto = Convert.ToInt32(ViewState["ID_Instituto"]);

                if (Btn_addCarrera.Text == "Agregar")
                {
                    ta.Insert(
                        ca_nombre.Text.Trim().ToUpper(),
                        ca_logo.Text.Trim(),
                        "ACTIVO",
                        ca_omo.Text.Trim().ToUpper(),
                        idInstituto
                    );
                    ShowAlert("Carrera agregada correctamente");
                }
                else
                {
                    ta.Update(
                        ca_nombre.Text.Trim().ToUpper(),
                        ca_logo.Text.Trim(),
                        "ACTIVO",
                        ca_omo.Text.Trim().ToUpper(),
                        idInstituto,
                        Convert.ToInt32(ID.Text)
                    );
                    ShowAlert("Carrera actualizada correctamente");
                }

                cleanModal();
                actualizar();
            }
            catch (Exception ex)
            {
                ShowAlert($"Error: {ex.Message}");
            }
        }


        // Método auxiliar para mostrar alertas
        private void ShowAlert(string message)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", $"alert('{message}');", true);
        }


        protected void actualizar()
        {
            if (ViewState["ID_Instituto"] != null)
            {
                int idInstituto = Convert.ToInt32(ViewState["ID_Instituto"]);
                dt = ta.GetDataByInstituto(idInstituto);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }


        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            if (int.TryParse(GridView1.DataKeys[e.RowIndex].Value.ToString(), out int ID_Carrera))
            {
                ta.Delete1(ID_Carrera);
                actualizar(); 
            }
            else
            {
                ShowAlert("Eliminado correctamente");
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (int.TryParse(e.CommandArgument.ToString(), out int ID))
            {
                
                
                if (e.CommandName == "Editar")
                {
                    dt = ta.GetDataByID(ID);

                    if (dt.Rows.Count > 0)
                    {
                        ca_nombre.Text = dt.Rows[0]["Nombre"]?.ToString()?.Trim() ?? "";
                        ca_logo.Text = dt.Rows[0]["Logo"]?.ToString()?.Trim() ?? "";
                        ca_omo.Text = dt.Rows[0]["Omoclave"]?.ToString()?.Trim() ?? "";

                        Btn_cancel.Visible = true;
                        Btn_addCarrera.Text = "Actualizar";
                        this.ID.Text = ID.ToString(); // Asigna al campo oculto u otro lugar si necesitas el ID después
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal();", true);
                    }
                    else
                    {
                        ShowAlert("No se encontró la carrera.");
                    }
                }
                else if (e.CommandName == "Tutores")
                {
                    Response.Redirect($"Tutores.aspx?id={ID}");
                }
                else if (e.CommandName == "Materias")
                {
                    Response.Redirect($"Materias.aspx?idCarrera={ID}");
                }
                else if (e.CommandName == "Alumnos")
                {
                    Response.Redirect($"Alumnos.aspx?idCarrera={ID}");
                }
                else if (e.CommandName == "Grupos")
                {
                    Response.Redirect($"Grupos.aspx?idCarrera={ID}");
                }
            }
            else
            {
                ShowAlert("ID no válido.");
            }
        }


        private void RedirigirSiIDValido(string urlBase, object commandArg, string mensajeError = "ID de carrera no válido.")
        {
            if (int.TryParse(commandArg.ToString(), out int id))
            {
                Response.Redirect($"{urlBase}?idCarrera={id}");
            }
            else
            {
                ShowAlert(mensajeError);
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
            ca_omo.Text = "";
            Btn_addCarrera.Text = "Agregar";
            Btn_cancel.Visible = false;
        }

       


    }

}