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
using TutoriasWeb.dsTutoriasTableAdapters;
using System.Data.SqlClient;
using System.Diagnostics;



namespace TutoriasWeb
{
    public partial class Grupos : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.GrupoTableAdapter ta = new dsTutoriasTableAdapters.GrupoTableAdapter();
        dsTutorias.GrupoDataTable dt;

        dsTutoriasTableAdapters.MaestroTableAdapter tta = new dsTutoriasTableAdapters.MaestroTableAdapter();
        dsTutorias.MaestroDataTable dtt;

        dsTutoriasTableAdapters.MateriaTableAdapter ttam = new dsTutoriasTableAdapters.MateriaTableAdapter();
        dsTutorias.MaestroDataTable dttm;



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

                    // Verificar que el ID_Carrera existe en la base de datos
                    var carreraAdapter = new dsTutoriasTableAdapters.CarreraTableAdapter();
                    var carreraData = carreraAdapter.GetDataByID(ID_Carrera); // Asegúrate de tener este método

                    if (carreraData.Rows.Count == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('ID de carrera no válido.'); window.location='Carrera.aspx';", true);
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('ID de carrera no válido.'); window.location='Carrera.aspx';", true);
                    return;
                }
                if (!IsPostBack)
                {
                    CargarMaestros(ID_Carrera);
                    CargarGrupos(); // Cargar los grupos existentes
                    ViewState["MaestrosMaterias"] = null;
                }



            }
        }

        private void CargarMaestros(int idCarrera)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
            SELECT m.ID, 
            m.Nombre + ' ' + m.A_Paterno + ' ' + m.A_Materno AS NombreCompleto,
            mat.ID AS MateriaID,
            mat.Nombre AS MateriaNombre
            FROM Maestro m
            INNER JOIN Materia mat ON m.ID = mat.ID_Maestro
            WHERE m.ID_Carrera = @ID_Carrera";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID_Carrera", idCarrera);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                ddlMaestro.DataSource = dt;
                ddlMaestro.DataTextField = "NombreCompleto";
                ddlMaestro.DataValueField = "ID";
                ddlMaestro.DataBind();
                ddlMaestro.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Selecciona un Maestro --", ""));
                Session["MaestrosMaterias"] = dt;
            }
        }

        protected void ddlMaestro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMaestro.SelectedValue != "")
            {
                int idMaestro = int.Parse(ddlMaestro.SelectedValue);
                DataTable dt = (DataTable)ViewState["MaestrosMaterias"];

                DataRow[] rows = dt.Select($"ID = {idMaestro}");
                if (rows.Length > 0)
                {
                    lblMateria.Text = rows[0]["MateriaNombre"].ToString();
                    ViewState["ID_Materia"] = rows[0]["MateriaID"];
                }
            }
            else
            {
                lblMateria.Text = "";
                ViewState["ID_Materia"] = null;
            }
        }

        private void CargarGrupos()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
            SELECT
            Grupo.ID AS GrupoID,
            Grupo.Nombre AS GrupoNombre,
            Maestro.Nombre + ' ' + A_Paterno + ' ' + A_Materno AS MaestroNombre,
            Materia.Nombre AS MateriaNombre
            FROM
            Grupo
            INNER JOIN
            Maestro ON Grupo.ID_Maestro = Maestro.ID
            INNER JOIN
            Materia ON Grupo.ID_Materia = Materia.ID
            WHERE
            Maestro.ID_Carrera = @ID_Carrera"; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Carrera", (int)ViewState["ID_Carrera"]);

                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
            }
        }




        protected void actualiza()
        {

        }


        protected void Btn_addGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Nombre.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe ingresar un nombre para el grupo');", true);
                    return;
                }
                if (string.IsNullOrEmpty(ddlMaestro.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe seleccionar un maestro válido');", true);
                    return;
                }
                int idMaestro = int.Parse(ddlMaestro.SelectedValue);
                if (ViewState["ID_Materia"] == null)
                {
                    DataTable dt = (DataTable)Session["MaestrosMaterias"];
                    if (dt == null)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('No se encontraron datos de maestros. Recargue la página.');", true);
                        return;
                    }
                    DataRow[] rows = dt.Select($"ID = {idMaestro}");
                    if (rows.Length == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('El maestro seleccionado no tiene materia asignada');", true);
                        return;
                    }
                    ViewState["ID_Materia"] = rows[0]["MateriaID"];
                    lblMateria.Text = rows[0]["MateriaNombre"].ToString();
                }
                string nombreGrupo = Nombre.Text.Trim();
                int idMateria = (int)ViewState["ID_Materia"];
                string estatus = "Activo";
                if (string.IsNullOrEmpty(lblGrupoID.Text))
                {
                    ta.Insert1(nombreGrupo, idMaestro, idMateria, estatus);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Grupo agregado correctamente.');", true);
                }
                else
                {
                    int grupoID = Convert.ToInt32(lblGrupoID.Text);
                    ta.Update1(nombreGrupo, idMaestro, idMateria, grupoID);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Grupo actualizado correctamente.');", true);
                }
                Nombre.Text = "";
                ddlMaestro.SelectedIndex = 0;
                lblMateria.Text = "";
                lblGrupoID.Text = "";
                CargarGrupos();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error: {ex.Message}');", true);
            }
        }



        protected void cleanModal()
        {

        }


        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                // Obtener el índice de la fila seleccionada
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                // Obtener el ID del grupo de la fila seleccionada
                int grupoID = Convert.ToInt32(GridView1.DataKeys[rowIndex].Value);
                // Llenar el modal con los datos del grupo
                LlenarModal(grupoID);
                // Mostrar el modal
                ClientScript.RegisterStartupScript(this.GetType(), "mostrarModal", "$('#mdl_maestro').modal('show');", true);
                // Cambiar el texto del botón "Agregar" a "Guardar"
                Btn_addGroup.Text = "Guardar";
            }
            else if (e.CommandName == "EliminarGrupo") // Cambiado aquí
            {
                // Obtener el ID del grupo de la fila seleccionada
                int grupoID = Convert.ToInt32(e.CommandArgument);

                // Eliminar el grupo de la base de datos
                EliminarGrupo(grupoID);

                // Recargar los grupos en el GridView
                CargarGrupos();
            }
            else if (e.CommandName == "MateriasAlumno")
            {
                // Obtener el ID del grupo de la fila seleccionada
                int grupoID = Convert.ToInt32(e.CommandArgument.ToString()); // Asegúrate de convertir a string primero
                                                                             // Llamar a la función para abrir el modal
                abrirModalMateriasAlumno(grupoID);
            }
            else if (e.CommandName == "Reportes")
            {
                // Obtener el ID del grupo de la fila seleccionada
                int grupoId = Convert.ToInt32(e.CommandArgument.ToString()); // Asegúrate de convertir a string primero
                                                                             // Llamar a la función para abrir el modal
                abrirModalReportes(grupoId);
            }

        }

        private void abrirModalMateriasAlumno(int grupoID)
        {
            // Aquí puedes implementar la lógica para abrir el modal y cargar los datos necesarios
            // Por ejemplo, puedes redirigir a la página de MateriasAlumno con el grupoID
            string url = "MateriasAlumno.aspx?grupoID=" + grupoID;
            ClientScript.RegisterStartupScript(this.GetType(), "abrirModal", "abrirModalMateriasAlumno('" + grupoID + "');", true);
        }

        private void abrirModalReportes(int grupoId)
        {
            // Aquí puedes implementar la lógica para abrir el modal y cargar los datos necesarios
            // Por ejemplo, puedes redirigir a la página de Reportes con el grupoID
            string url = "Reportes.aspx?grupoID=" + grupoId;
            ClientScript.RegisterStartupScript(this.GetType(), "abrirModal", "abrirModalReportes('" + grupoId + "');", true);
        }


        private void LlenarModal(int grupoID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
    SELECT g.ID, g.Nombre, g.ID_Maestro, m.ID AS MateriaID, m.Nombre AS MateriaNombre
    FROM Grupo g
    INNER JOIN Materia m ON g.ID_Materia = m.ID
    WHERE g.ID = @GrupoID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@GrupoID", grupoID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    lblGrupoID.Text = reader["ID"].ToString();
                    Nombre.Text = reader["Nombre"].ToString();
                    ddlMaestro.SelectedValue = reader["ID_Maestro"].ToString();
                    lblMateria.Text = reader["MateriaNombre"].ToString();
                    ViewState["ID_Materia"] = reader["MateriaID"];
                }
                reader.Close();
            }
        }




        private void EliminarGrupo(int ID)
        {
            try
            {
                ta.Delete1(ID);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Grupo eliminado correctamente.');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al eliminar el grupo: " + ex.Message + "');", true);
            }
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}