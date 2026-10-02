using SpreadsheetLight;
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
        Metodos mt = new Metodos();
        DataTable t = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                string idCarreraStr = Request.QueryString["idCarrera"];
                if (!string.IsNullOrEmpty(idCarreraStr) && int.TryParse(idCarreraStr, out int ID_Carrera))
                {
                    ViewState["ID_Carrera"] = ID_Carrera;
                    actualizar(ID_Carrera);


                }

                else
                {
                    Response.Redirect("Carreras.aspx");
                }
            }
            if (!IsPostBack)
            {
                actualizar(Convert.ToInt32(ViewState["ID_Carrera"])); // o como sea tu lógica
            }
        }
     
        protected void Btn_addAlumno_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(al_nombre.Text) ||
                (string.IsNullOrWhiteSpace(al_aPaterno.Text) && string.IsNullOrWhiteSpace(al_aMaterno.Text)))
            {
                ShowAlert("Complete todos los campos obligatorios");
                return;
            }

            int ID_Carrera = Convert.ToInt32(ViewState["ID_Carrera"]);
            int No_Control = Convert.ToInt32(al_id.Text);

            try
            {
                if (Btn_addAlumno.Text == "Agregar")
                {
                    string nombre = al_nombre.Text.Trim().ToUpper();
                    string aPaterno = al_aPaterno.Text.Trim().ToUpper();
                    string aMaterno = al_aMaterno.Text.Trim().ToUpper();
                    int semestre = Convert.ToInt32(ddlSemestre.SelectedValue);
                     No_Control = Convert.ToInt32(al_id.Text.Trim());
                    string usuario = nombre; 
                    string clave = No_Control.ToString(); 
                    string estatus = "ACTIVO";
                    string tutoria = ddlTutoria.SelectedValue;
                    int idCarrera = Convert.ToInt32(ViewState["ID_Carrera"]);
                    ta.Insert1(
                        No_Control,
                        usuario,
                        nombre,
                        aPaterno,
                        aMaterno,
                        semestre,
                        estatus,
                        tutoria,
                        clave,
                        idCarrera
                    );

                    ShowAlert("Alumno agregado correctamente");
                }
                else
                {
                    var adapter = new dsTutoriasTableAdapters.AlumnoTableAdapter();
                    string tutoria = ddlTutoria.SelectedValue;
                    int filasAfectadas = adapter.Update1(
                        al_nombre.Text.Trim().ToUpper(),
                        al_aPaterno.Text.Trim().ToUpper(),
                        al_aMaterno.Text.Trim().ToUpper(),
                        Convert.ToInt32(ddlSemestre.SelectedValue),
                        tutoria,
                        No_Control
                    );
                    if (filasAfectadas > 0)
                        ShowAlert("Alumno actualizado correctamente");
                    else
                        ShowAlert("No se realizaron cambios");
                }
                cleanModal();
                ScriptManager.RegisterStartupScript(this, GetType(), "openModal", "closeModal();", true);
                actualizar(ID_Carrera);
            }
            catch (Exception ex)
            {
                ShowAlert($"Error al actualizar: {ex.Message}");
            }
        }



        private string GenerarUsuario(string nombre)
        {
            
            return nombre.Replace(" ", "").ToLower();
        }

        protected void actualizar(int ID_Carrera)
        {
            var adapter = new dsTutoriasTableAdapters.AlumnoTableAdapter();
            var dt = adapter.GetDataByCarrera(ID_Carrera);

            GridView1.DataSource = dt;
            GridView1.DataBind();
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
            Tb_buscar.Text = "";
            ddlTutoria.SelectedIndex = 0;

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (GridView1.DataKeys != null && e.RowIndex >= 0 && e.RowIndex < GridView1.DataKeys.Count)
            {
                object key = GridView1.DataKeys[e.RowIndex]?.Value;

                if (key != null && int.TryParse(key.ToString(), out int No_Control))
                {
                    try
                    {
                        ta.Delete(No_Control);
                        actualizar(Convert.ToInt32(ViewState["ID_Carrera"]));
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                            $"alert('Error al eliminar: {ex.Message}');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert('Error: índice fuera del rango del GridView.');", true);
            }
        }





        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int noControl = Convert.ToInt32(e.CommandArgument);
                dt = ta.GetDataByNo(noControl);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    al_id.Text = noControl.ToString();
                    al_nombre.Text = row["Nombre"].ToString();
                    al_aPaterno.Text = row["A_Paterno"].ToString();
                    al_aMaterno.Text = row["A_Materno"].ToString();
                    ddlSemestre.SelectedValue = row["Semestre"].ToString();
                    ddlTutoria.SelectedValue = row["Tutoria"].ToString();

                    Btn_addAlumno.Text = "Actualizar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "openModal", "openModal();", true);
                }
            }

            if (e.CommandName == "Imprimir")
            {
                int noControl = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("CertificadoAlumno.aspx?nc=" + noControl);
            }

        }



        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            
        }

        private void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                $"alert('{message}');", true);
        }

    }
}