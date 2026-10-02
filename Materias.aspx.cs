using DocumentFormat.OpenXml.Wordprocessing;
using TutoriasWeb.dsTutoriasTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TutoriasWeb;
using System.Data.SqlClient;
using System.Diagnostics;

namespace TutoriasWeb
{
    public partial class Materias : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.MateriaTableAdapter ta = new dsTutoriasTableAdapters.MateriaTableAdapter();
        dsTutorias.MateriaDataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["idCarrera"] != null)
                {
                    int idCarrera = Convert.ToInt32(Request.QueryString["idCarrera"]);
                    ViewState["ID_Carrera"] = idCarrera;

                    // Cargar datos iniciales
                    //CargarCarrera(idCarrera);
                    CargarMaestros(idCarrera);
                    CargarMaterias(idCarrera);
                }
                else
                {
                    Response.Redirect("Carreras.aspx");
                }
            }
        }

        private void CargarMaestros(int idCarrera)
        {
            var taMaestros = new dsTutoriasTableAdapters.MaestroTableAdapter();
            var maestros = taMaestros.GetDataByCarrera(idCarrera)
                            .Where(m => m.Estatus == "ACTIVO")
                            .ToList();

            ddlMaestros.DataSource = maestros;
            ddlMaestros.DataTextField = "Nombre";
            ddlMaestros.DataValueField = "ID";
            ddlMaestros.DataBind();

            //ddlMaestros.Items.Insert(0, new ListItem("-- Seleccione un maestro --", "0"));
        }

        private void CargarMaterias(int ID_Carrera)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
                string query = @"
            SELECT m.ID, m.Nombre, m.Nombre_Corto, m.Semestre, m.Estatus, m.ID_Carrera, m.ID_Maestro,
                   ma.Nombre + ' ' + ma.A_Paterno + ' ' + ma.A_Materno AS MaestroNombre
            FROM Materia m
            INNER JOIN Maestro ma ON m.ID_Maestro = ma.ID
            WHERE m.ID_Carrera = @ID_Carrera";
                var dt = new DataTable(); 
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Carrera", ID_Carrera);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = dt != null && dt.Rows.Count > 0; 
            }
            catch (Exception ex)
            {
                ShowAlert("Error al cargar materias: " + ex.Message);
                GridView1.Visible = false;
            }
        }


        //**private void CargarCarrera(int idCarrera)
        //{
            //var adapter = new dsTutoriasTableAdapters.AlumnoTableAdapter();
            //var dt = adapter.GetDataByCarrera(idCarrera); // Asegúrate de que este método existe en tu TableAdapter

            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            /// Opcional: Mostrar el nombre de la carrera en el título
            //var carreraAdapter = new dsTutoriasTableAdapters.CarreraTableAdapter();
            //var carrera = carreraAdapter.GetDataByID(idCarrera);
            //if (carrera.Rows.Count > 0)
            //{
                //lbl_name.Text = $"Materias de la carrera: {carrera.Rows[0]["Nombre"].ToString()}";
            //}
        //}

        protected void Btn_addMateria_Click(object sender, EventArgs e)
        {
            if (ViewState["ID_Carrera"] == null)
            {
                Response.Redirect("Carreras.aspx");
                return;
            }

            int idCarrera = (int)ViewState["ID_Carrera"];
            int idMaestro = Convert.ToInt32(ddlMaestros.SelectedValue);

            if (Btn_addMateria.Text == "Agregar")
            {
                // INSERT
                ta.Insert(
                    nombre.Text.Trim().ToUpper(),
                    nombre_corto.Text.Trim().ToUpper(),
                    Convert.ToByte(ddlSemestre.SelectedValue),
                    "ACTIVO",
                    idCarrera,
                    idMaestro
                );
                ShowAlert("Materia agregada correctamente");
            }
            else
            {
                // UPDATE
                int idMateria = (int)ViewState["EditID"];
                ta.Update(
                    nombre.Text.Trim().ToUpper(),
                    nombre_corto.Text.Trim().ToUpper(),
                    Convert.ToByte(ddlSemestre.SelectedValue),
                    "ACTIVO",
                    idCarrera,
                    idMaestro,
                    idMateria
                );
                ShowAlert("Materia actualizada correctamente");
            }

            cleanModal();
            CargarMaterias(idCarrera);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int idMateria = Convert.ToInt32(e.CommandArgument);
                ViewState["EditID"] = idMateria;

                var materia = ta.GetDataByID(idMateria).FirstOrDefault();
                if (materia != null)
                {
                    nombre.Text = materia.Nombre;
                    nombre_corto.Text = materia.Nombre_Corto;
                    ddlSemestre.SelectedValue = materia.Semestre.ToString();
                    ddlMaestros.SelectedValue = materia.ID_Maestro.ToString();
                    Btn_addMateria.Text = "Guardar Cambios";
                    Btn_cancel.Visible = true;

                    // Mostrar el modal con JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "openModal",
                        "$('#mdl').modal('show');", true);
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idMateria = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            ta.Delete(idMateria); 

            if (ViewState["ID_Carrera"] != null)
            {
                CargarMaterias((int)ViewState["ID_Carrera"]);
                
                ShowAlert("Materia eliminada correctamente");
            }
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        private void cleanModal()
        {
            nombre.Text = "";
            nombre_corto.Text = "";
            ddlSemestre.SelectedIndex = 0;
            ddlMaestros.SelectedIndex = 0;
            Btn_addMateria.Text = "Agregar";
            Btn_cancel.Visible = false;
            ViewState["EditID"] = null;
        }

        private void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                $"alert('{message}');", true);
        }
    }
}