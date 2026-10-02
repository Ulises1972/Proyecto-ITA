using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TutoriasWeb
{
    public partial class Administradores : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.AdministradorTableAdapter taa = new dsTutoriasTableAdapters.AdministradorTableAdapter();
        dsTutorias.AdministradorDataTable dta;

        dsTutoriasTableAdapters.CarreraTableAdapter tac = new dsTutoriasTableAdapters.CarreraTableAdapter();
        dsTutorias.CarreraDataTable dtc;
        Metodos mt = new Metodos();
        DataTable dt = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                string idStr = Request.QueryString["idInstituto"];
                if (!string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out int ID_Instituto))
                {
                    ViewState["ID_Instituto"] = ID_Instituto;
                    CargarCarreras(ID_Instituto); // Llama a CargarCarreras con el ID del instituto
                    actualizar();
                }
                else
                {
                    Response.Redirect("Instituto.aspx");
                }
            }
        }


        protected void CargarCarreras(int idInstituto)
        {
            // Cambia la consulta para obtener solo las carreras del instituto específico
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = "SELECT ID, Nombre FROM Carrera WHERE ID_Instituto = @ID_Instituto";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Instituto", idInstituto);
                    connection.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    Carrera.DataSource = dataTable;
                    Carrera.DataTextField = "Nombre";
                    Carrera.DataValueField = "ID";
                    Carrera.DataBind();
                }
            }
        }



        protected void actualizar()
        {
            if (ViewState["ID_Instituto"] != null)
            {
                int idInstituto = Convert.ToInt32(ViewState["ID_Instituto"]);

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;

                string query = @"
                SELECT 
                Administrador.ID, 
                Administrador.Usuario, 
                Administrador.Nombre, 
                Administrador.A_Paterno, 
                Administrador.A_Materno, 
                Carrera.Omoclave AS Carrera
                FROM 
                Administrador 
                INNER JOIN 
                Carrera ON Administrador.ID_Carrera = Carrera.ID
                WHERE 
                Carrera.ID_Instituto = @ID_Instituto";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Instituto", idInstituto);

                        connection.Open();
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            GridView1.DataSource = dataTable;
                            GridView1.DataBind();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('No se encontraron administradores para el instituto seleccionado.');", true);
                        }
                    }
                }
            }
        }







        protected void Btn_addAdmin_Click(object sender, EventArgs e)
        {
            if (Nombre.Text.Trim() == "" || A_Paterno.Text.Trim() == "" || A_Materno.Text.Trim() == "" || Carrera.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Todos los campos son obligatorios');", true);
                return;
            }
            string nombre = Nombre.Text.ToUpper();
            string a_paterno = A_Paterno.Text.ToUpper();
            string a_materno = A_Materno.Text.ToUpper();
            int idCarrera = Convert.ToInt32(Carrera.SelectedValue);
            string usuario = nombre; 
            string clave = nombre.Substring(0, 3) + "12345";
            if (ViewState["ID_Administrador"] != null)
            {
                int idAdministrador = Convert.ToInt32(ViewState["ID_Administrador"]);
                int filasAfectadas = taa.Update1(nombre, a_paterno, a_materno, idCarrera, idAdministrador);
                if (filasAfectadas > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Administrador actualizado correctamente');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Error al actualizar administrador');", true);
                }
            }
            else
            {
                int filasAfectadas = taa.Insert1(usuario, nombre, a_paterno, a_materno, clave, idCarrera); 
                if (filasAfectadas > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Administrador agregado correctamente');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Error al agregar administrador');", true);
                }
            }
            cleanModal();
            actualizar();
        }




        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int idAdministrador = Convert.ToInt32(e.CommandArgument);
                CargarAdministrador(idAdministrador); // Cargar los datos del administrador
                                                      // Almacenar el ID en ViewState para usarlo más tarde
                ViewState["ID_Administrador"] = idAdministrador;
                // Llama al método JavaScript para abrir el modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openModalScript", "openModal();", true);
            }
        }





        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idAdministrador = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            int filasAfectadas = taa.Delete1(idAdministrador); 

            if (filasAfectadas > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Administrador eliminado correctamente');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Error al eliminar administrador');", true);
            }
            actualizar();
        }


        private void CargarAdministrador(int idAdministrador)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            string query = @"
SELECT 
    ID, 
    Usuario, 
    Nombre, 
    A_Paterno, 
    A_Materno, 
    ID_Carrera 
FROM 
    Administrador 
WHERE 
    ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", idAdministrador);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Cargar los datos en los campos del modal
                            Nombre.Text = reader["Nombre"].ToString();
                            A_Paterno.Text = reader["A_Paterno"].ToString();
                            A_Materno.Text = reader["A_Materno"].ToString();
                            Carrera.SelectedValue = reader["ID_Carrera"].ToString(); // Asegúrate de que este campo exista

                            // Almacenar el ID en ViewState para usarlo más tarde
                            ViewState["ID_Administrador"] = reader["ID"].ToString(); // Guardar el ID en ViewState

                            Btn_addAdmin.Text = "Actualizar"; // Cambiar el texto del botón a "Actualizar"
                            Btn_cancel.Visible = true; // Mostrar el botón de cancelar
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('No se encontró el administrador.');", true);
                        }
                    }
                }
            }
        }






        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void cleanModal()
        {
            Nombre.Text = "";
            A_Paterno.Text = "";
            A_Materno.Text = "";
            Carrera.SelectedIndex = 0;
            Btn_addAdmin.Text = "Agregar";
            Btn_cancel.Visible = false;
        }
    }
}