using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using TutoriasWeb.dsTutoriasTableAdapters;

namespace TutoriasWeb
{
    public partial class CalificacionesAlumno1 : System.Web.UI.UserControl
    {
        public int GrupoID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GrupoID > 0)
            {

                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                string grupoIDStr = Request.QueryString["grupoID"];
                if (!string.IsNullOrEmpty(grupoIDStr) && int.TryParse(grupoIDStr, out int grupoID))
                {
                    ViewState["grupoID"] = grupoID;
                    CargarAlumnos(grupoID);
                    CargarDatosAlumnoMateria();

                }
            }

        }

        private void CargarAlumnos(int grupoID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
        SELECT a.No_control, a.Nombre
        FROM Alumno a
        INNER JOIN Grupo g ON g.ID = @GrupoID
        INNER JOIN Maestro m ON m.ID = g.ID_Maestro
        WHERE a.ID_Carrera = m.ID_Carrera
        ORDER BY a.No_control";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@GrupoID", grupoID);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                No_COntrol.DataSource = dt;
                No_COntrol.DataTextField = "No_control";
                No_COntrol.DataValueField = "No_control";
                No_COntrol.DataBind();
                No_COntrol.Items.Insert(0, new ListItem("-- Seleccione alumno --", ""));
            }
        }

        private void FiltrarAlumnos(string texto)
        {
            int grupoID = (int)ViewState["grupoID"];
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
        SELECT a.No_control, a.Nombre
        FROM Alumno a
        INNER JOIN Grupo g ON g.ID = @GrupoID
        INNER JOIN Maestro m ON m.ID = g.ID_Maestro
        WHERE a.ID_Carrera = m.ID_Carrera
          AND CAST(a.No_control AS VARCHAR) LIKE @Texto + '%'
        ORDER BY a.No_control";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@GrupoID", grupoID);
                command.Parameters.AddWithValue("@Texto", texto);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                No_COntrol.DataSource = dt;
                No_COntrol.DataTextField = "No_control";
                No_COntrol.DataValueField = "No_control";
                No_COntrol.DataBind();
                No_COntrol.Items.Insert(0, new ListItem(""));
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int grupoID = (int)ViewState["grupoID"];
            int noControl = int.Parse(No_COntrol.SelectedValue);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            int idMateria = 0;
            int semestre = 0;

            // Obtener ID_Materia desde Grupo
            string queryMateria = "SELECT ID_Materia FROM Grupo WHERE ID = @GrupoID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(queryMateria, connection))
            {
                command.Parameters.AddWithValue("@GrupoID", grupoID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    idMateria = Convert.ToInt32(result);
                else
                {
                    ShowAlert("No se encontró la materia para el grupo seleccionado.");
                    return;
                }
            }

            // Obtener Semestre desde Alumno
            string querySemestre = "SELECT Semestre FROM Alumno WHERE No_control = @NoControl";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querySemestre, connection))
            {
                command.Parameters.AddWithValue("@NoControl", noControl);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    semestre = Convert.ToInt32(result);
                else
                {
                    ShowAlert("No se encontró el semestre para el alumno seleccionado.");
                    return;
                }
            }

            // Leer calificaciones de campos textbox
            decimal?[] calificaciones = new decimal?[16];
            calificaciones[0] = ParseDecimalOrNull(TextBox.Text);
            calificaciones[1] = ParseDecimalOrNull(TextBox1.Text);
            calificaciones[2] = ParseDecimalOrNull(TextBox2.Text);
            calificaciones[3] = ParseDecimalOrNull(TextBox3.Text);
            calificaciones[4] = ParseDecimalOrNull(TextBox4.Text);
            calificaciones[5] = ParseDecimalOrNull(TextBox5.Text);
            calificaciones[6] = ParseDecimalOrNull(TextBox6.Text);
            calificaciones[7] = ParseDecimalOrNull(TextBox7.Text);
            calificaciones[8] = ParseDecimalOrNull(TextBox8.Text);
            calificaciones[9] = ParseDecimalOrNull(TextBox9.Text);
            calificaciones[10] = ParseDecimalOrNull(TextBox10.Text);
            calificaciones[11] = ParseDecimalOrNull(TextBox11.Text);
            calificaciones[12] = ParseDecimalOrNull(TextBox12.Text);
            calificaciones[13] = ParseDecimalOrNull(TextBox13.Text);
            calificaciones[14] = ParseDecimalOrNull(TextBox14.Text);
            calificaciones[15] = ParseDecimalOrNull(TextBox15.Text);

            // Calcular promedio
            decimal promedio = CalcularPromedio(calificaciones);

            // Insertar en la tabla Alumno_Materia usando TableAdapter
            var alumnoMateriaAdapter = new Alumno_MateriaTableAdapter();
            alumnoMateriaAdapter.Insert1(
                noControl, idMateria, grupoID,
                calificaciones[0], calificaciones[1], calificaciones[2], calificaciones[3],
                calificaciones[4], calificaciones[5], calificaciones[6], calificaciones[7],
                calificaciones[8], calificaciones[9], calificaciones[10], calificaciones[11],
                calificaciones[12], calificaciones[13], calificaciones[14], calificaciones[15],
                promedio, semestre
            );
            ShowAlert("Calificaciones registradas correctamente.");
            CargarDatosAlumnoMateria();

            Button2.Visible = true;
            btnActualizar.Visible = false;
        }

        private decimal? ParseDecimalOrNull(string text)
        {
            if (decimal.TryParse(text, out decimal val))
                return val;
            else
                return null;
        }

        private decimal CalcularPromedio(decimal?[] calificaciones)
        {
            decimal suma = 0;
            int count = 0;

            for (int i = 0; i < 8; i++)
            {
                decimal? c1 = calificaciones[i * 2];
                decimal? c2 = calificaciones[i * 2 + 1];

                if (c1.HasValue && c2.HasValue && c1.Value == c2.Value)
                {
                    suma += c1.Value;
                    count++;
                }
                else
                {
                    if (c1.HasValue)
                    {
                        suma += c1.Value;
                        count++;
                    }
                    if (c2.HasValue)
                    {
                        suma += c2.Value;
                        count++;
                    }
                }
            }
            return count > 0 ? Math.Round(suma / count, 2) : 0;
        }

        private void ShowAlert(string message)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", $"alert('{message}');", true);
        }

        private decimal? GetCalificacion(string calif1, string calif2)
        {
            if (decimal.TryParse(calif1, out decimal c1) && decimal.TryParse(calif2, out decimal c2))
            {
                return c1 == c2 ? c1 : (decimal?)null; // Retorna solo una calificación si son iguales
            }
            return null; // Si no se puede parsear, retorna null
        }

        private void InsertarDatos(int noControl, int idMateria, int idGrupo, int semestre, decimal?[] calificaciones, decimal promedio)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
    INSERT INTO Alumno_Materia (No_control, ID_Materia, ID_Grupo, u1_1, u1_2, u2_1, u2_2, u3_1, u3_2, u4_1, u4_2, u5_1, u5_2, u6_1, u6_2, u7_1, u7_2, u8_1, u8_2, Promedio, Semestre)
    VALUES (@NoControl, @IDMateria, @IDGrupo, @U1_1, @U1_2, @U2_1, @U2_2, @U3_1, @U3_2, @U4_1, @U4_2, @U5_1, @U5_2, @U6_1, @U6_2, @U7_1, @U7_2, @U8_1, @U8_2, @Promedio, @Semestre)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@NoControl", noControl);
                command.Parameters.AddWithValue("@IDMateria", idMateria);
                command.Parameters.AddWithValue("@IDGrupo", idGrupo);
                command.Parameters.AddWithValue("@Semestre", semestre);
                command.Parameters.AddWithValue("@Promedio", promedio);

                for (int i = 0; i < 8; i++)
                {
                    command.Parameters.AddWithValue($"@U{i + 1}_1", calificaciones[i * 2]);
                    command.Parameters.AddWithValue($"@U{i + 1}_2", calificaciones[i * 2 + 1]);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        protected void No_COntrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(No_COntrol.SelectedValue))
            {
                int noControl = int.Parse(No_COntrol.SelectedValue);
                CargarDetallesAlumno(noControl);
            }
        }


        private void CargarDetallesAlumno(int noControl)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            // Consulta para obtener los detalles del alumno
            string queryAlumno = @"
    SELECT a.No_control, a.Nombre, a.A_Paterno, a.A_Materno, a.Semestre
    FROM Alumno a
    WHERE a.No_control = @NoControl";

            // Consulta para obtener las calificaciones
            string queryCalificaciones = @"
    SELECT u1_1, u1_2, u2_1, u2_2, u3_1, u3_2, u4_1, u4_2, u5_1, u5_2, u6_1, u6_2, u7_1, u7_2, u8_1, u8_2
    FROM Alumno_Materia
    WHERE No_control = @NoControl";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Obtener detalles del alumno
                using (SqlCommand command = new SqlCommand(queryAlumno, connection))
                {
                    command.Parameters.AddWithValue("@NoControl", noControl);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Mostrar los detalles del alumno en la interfaz
                            string nombreCompleto = $"{reader["Nombre"]} {reader["A_Paterno"]} {reader["A_Materno"]}";
                            int semestre = (int)reader["Semestre"];

                            // Aquí puedes mostrar el nombre completo y semestre en controles como Label
                            // Por ejemplo:
                            // lblNombreCompleto.Text = nombreCompleto;
                            // lblSemestre.Text = semestre.ToString();
                        }
                    }
                }

                // Obtener calificaciones
                using (SqlCommand command = new SqlCommand(queryCalificaciones, connection))
                {
                    command.Parameters.AddWithValue("@NoControl", noControl);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Procesar y mostrar las calificaciones
                            decimal?[] calificaciones = new decimal?[16];
                            for (int i = 0; i < 8; i++)
                            {
                                calificaciones[i * 2] = reader[$"u{i + 1}_1"] as decimal?;
                                calificaciones[i * 2 + 1] = reader[$"u{i + 1}_2"] as decimal?;
                            }

                            // Aquí puedes aplicar la lógica para mostrar solo una calificación por unidad
                            // y mostrar las calificaciones en la interfaz
                        }
                    }
                }
            }
        }

        private void CargarDatosAlumnoMateria()
        {
            int grupoID = (int)ViewState["grupoID"];
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
            SELECT 
                am.No_control,
                a.Nombre + ' ' + a.A_Paterno + ' ' + a.A_Materno AS NombreCompleto,
                a.Semestre,
                am.u1_1, am.u1_2, am.u2_1, am.u2_2,
                am.u3_1, am.u3_2, am.u4_1, am.u4_2,
                am.u5_1, am.u5_2, am.u6_1, am.u6_2,
                am.u7_1, am.u7_2, am.u8_1, am.u8_2,
                am.Promedio
            FROM Alumno_Materia am
            INNER JOIN Alumno a ON am.No_control = a.No_control
            WHERE am.ID_Grupo = @GrupoID
            ORDER BY am.No_control";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))

            {
                command.Parameters.AddWithValue("@GrupoID", grupoID);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridViewAlumnos.DataSource = dt;
                GridViewAlumnos.DataBind();
            }
        }

        protected void GridViewAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                string noControl = e.CommandArgument.ToString();

                // 1. Cargar datos del alumno en el modal
                CargarDatosParaEditar(noControl);

                // 2. Ocultar "Agregar" y mostrar "Actualizar"
                Button2.Visible = false;
                btnActualizar.Visible = true;

                // 3. Guardar el No_control en ViewState para usarlo al actualizar
                ViewState["NoControlEditar"] = noControl;

                // 2. Mostrar el modal (ya lo hace tu código)
                ScriptManager.RegisterStartupScript(this, GetType(), "openModal", "$('#mdl').modal('show');", true);
            }
            else if (e.CommandName == "Eliminar")
            {
                string noControl = e.CommandArgument.ToString();
                int grupoID = (int)ViewState["grupoID"];

                // Llamar al método para eliminar
                EliminarAlumnoMateria(noControl, grupoID);
            }
        }


        private void EliminarAlumnoMateria(string noControl, int grupoID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
        DELETE FROM Alumno_Materia 
        WHERE No_control = @NoControl AND ID_Grupo = @GrupoID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@NoControl", noControl);
                command.Parameters.AddWithValue("@GrupoID", grupoID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ShowAlert("Alumno eliminado correctamente.");
                    CargarDatosAlumnoMateria(); // Recargar el GridView
                }
                else
                {
                    ShowAlert("No se encontró el registro para eliminar.");
                }
            }
        }

        private void CargarDatosParaEditar(string noControl)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
        SELECT 
            u1_1, u1_2, u2_1, u2_2, u3_1, u3_2, 
            u4_1, u4_2, u5_1, u5_2, u6_1, u6_2, 
            u7_1, u7_2, u8_1, u8_2
        FROM Alumno_Materia
        WHERE No_control = @NoControl AND ID_Grupo = @GrupoID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@NoControl", noControl);
                command.Parameters.AddWithValue("@GrupoID", ViewState["grupoID"]);  // ¡Agrega este parámetro!
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                // ¡Agrega este parámetro!

                if (reader.Read())
                {
                    // Asignar valores a los TextBox del modal
                    TextBox.Text = reader["u1_1"].ToString();
                    TextBox1.Text = reader["u1_2"].ToString();
                    TextBox2.Text = reader["u2_1"].ToString();
                    TextBox3.Text = reader["u2_2"].ToString();
                    TextBox4.Text = reader["u3_1"].ToString();
                    TextBox5.Text = reader["u3_2"].ToString();
                    TextBox6.Text = reader["u4_1"].ToString();
                    TextBox7.Text = reader["u4_2"].ToString();
                    TextBox8.Text = reader["u5_1"].ToString();
                    TextBox9.Text = reader["u5_2"].ToString();
                    TextBox10.Text = reader["u6_1"].ToString();
                    TextBox11.Text = reader["u6_2"].ToString();
                    TextBox12.Text = reader["u7_1"].ToString();
                    TextBox13.Text = reader["u7_2"].ToString();
                    TextBox14.Text = reader["u8_1"].ToString();
                    TextBox15.Text = reader["u8_2"].ToString();
                }
                reader.Close();
            }

            // Opcional: Seleccionar el alumno en el DropDownList
            No_COntrol.SelectedValue = noControl;
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (ViewState["NoControlEditar"] != null && ViewState["grupoID"] != null)
            {
                int noControl = Convert.ToInt32(ViewState["NoControlEditar"].ToString());
                int grupoID = (int)ViewState["grupoID"];

                // 1. Obtener TODAS las calificaciones de los TextBox (16 campos)
                decimal?[] calificaciones = new decimal?[16];
                calificaciones[0] = ParseDecimalOrNull(TextBox.Text);
                calificaciones[1] = ParseDecimalOrNull(TextBox1.Text);
                calificaciones[2] = ParseDecimalOrNull(TextBox2.Text);
                calificaciones[3] = ParseDecimalOrNull(TextBox3.Text);
                calificaciones[4] = ParseDecimalOrNull(TextBox4.Text);
                calificaciones[5] = ParseDecimalOrNull(TextBox5.Text);
                calificaciones[6] = ParseDecimalOrNull(TextBox6.Text);
                calificaciones[7] = ParseDecimalOrNull(TextBox7.Text);
                calificaciones[8] = ParseDecimalOrNull(TextBox8.Text);
                calificaciones[9] = ParseDecimalOrNull(TextBox9.Text);
                calificaciones[10] = ParseDecimalOrNull(TextBox10.Text);
                calificaciones[11] = ParseDecimalOrNull(TextBox11.Text);
                calificaciones[12] = ParseDecimalOrNull(TextBox12.Text);
                calificaciones[13] = ParseDecimalOrNull(TextBox13.Text);
                calificaciones[14] = ParseDecimalOrNull(TextBox14.Text);
                calificaciones[15] = ParseDecimalOrNull(TextBox15.Text);

                // 2. Calcular promedio
                decimal promedio = CalcularPromedio(calificaciones);

                // 3. Actualizar en BD (VERIFICA EL ORDEN DE PARÁMETROS!)
                var adapter = new Alumno_MateriaTableAdapter();
                adapter.Update1(
                    // Unidades 1-8 (16 parámetros)
                    calificaciones[0], calificaciones[1],  // u1_1, u1_2
                    calificaciones[2], calificaciones[3],  // u2_1, u2_2
                    calificaciones[4], calificaciones[5],  // u3_1, u3_2
                    calificaciones[6], calificaciones[7],  // u4_1, u4_2
                    calificaciones[8], calificaciones[9],  // u5_1, u5_2
                    calificaciones[10], calificaciones[11], // u6_1, u6_2
                    calificaciones[12], calificaciones[13], // u7_1, u7_2
                    calificaciones[14], calificaciones[15], // u8_1, u8_2
                                                            // Promedio y condiciones WHERE
                    promedio,
                    noControl,  // @NoControl (WHERE)
                    grupoID     // @IDGrupo (WHERE)
                );

                // 4. Actualizar GridView y cerrar modal
                CargarDatosAlumnoMateria();
                ShowAlert("¡Datos actualizados correctamente!");
                ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "$('#mdl').modal('hide');", true);
            }
        }



    }
}