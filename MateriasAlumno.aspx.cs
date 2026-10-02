using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using TutoriasWeb.dsTutoriasTableAdapters;
using DocumentFormat.OpenXml.Presentation;
using System.Web.UI.HtmlControls;
using System.IO;

namespace TutoriasWeb
{
    public partial class MateriasAlumno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["id"] == null)
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


        private void abrirModalMateriasAlumno(int noControl)
        {
            // Aquí puedes implementar la lógica para abrir el modal y cargar los datos necesarios
            // Por ejemplo, puedes redirigir a la página de MateriasAlumno con el grupoID
            string url = "CertificadoAlumno.aspx?No_control=" + noControl;
            ClientScript.RegisterStartupScript(this.GetType(), "abrirModal", "abrirModalMateriasAlumno('" + noControl + "');", true);
        }

        // Método para cargar alumnos filtrados por la carrera asociada al grupo
        private void CargarAlumnos(int grupoID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
            SELECT a.No_control, a.Nombre
            FROM Alumno a
            INNER JOIN Grupo g ON g.ID = @GrupoID
            INNER JOIN Maestro m ON m.ID = g.ID_Maestro
            WHERE a.ID_Carrera = m.ID_Carrera
            AND a.Tutoria = 'EN PROCESO' OR a.Tutoria = 'NO REALIZADAS'
            ORDER BY a.No_control";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@GrupoID", grupoID);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                No_Control.DataSource = dt;
                No_Control.DataTextField = "No_control";
                No_Control.DataValueField = "No_control";
                No_Control.DataBind();
                No_Control.Items.Insert(0, new ListItem("-- Seleccione alumno --", ""));
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
                No_Control.DataSource = dt;
                No_Control.DataTextField = "No_control";
                No_Control.DataValueField = "No_control";
                No_Control.DataBind();
                No_Control.Items.Insert(0, new ListItem(""));
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int grupoID = (int)ViewState["grupoID"];
           
          
            if (!int.TryParse(No_Control.SelectedValue, out int noControl))
            {
                ShowAlert("El número de control no tiene un formato válido");
                return;
            }
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            int idMateria = 0;
            int semestre = 0;
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
            decimal?[] calificaciones = new decimal?[16];
            decimal?[] porcentajes = new decimal?[16];
            calificaciones[0] = ParseDecimalOrNull(TextBox.Text);
            porcentajes[1] = ParseDecimalOrNull(TextBox1.Text);
            calificaciones[2] = ParseDecimalOrNull(TextBox2.Text);
            porcentajes[3] = ParseDecimalOrNull(TextBox3.Text);
            calificaciones[4] = ParseDecimalOrNull(TextBox4.Text);
            porcentajes[5] = ParseDecimalOrNull(TextBox5.Text);
            calificaciones[6] = ParseDecimalOrNull(TextBox6.Text);
            porcentajes[7] = ParseDecimalOrNull(TextBox7.Text);
            calificaciones[8] = ParseDecimalOrNull(TextBox8.Text);
            porcentajes[9] = ParseDecimalOrNull(TextBox9.Text);
            calificaciones[10] = ParseDecimalOrNull(TextBox10.Text);
            porcentajes[11] = ParseDecimalOrNull(TextBox11.Text);
            calificaciones[12] = ParseDecimalOrNull(TextBox12.Text);
            porcentajes[13] = ParseDecimalOrNull(TextBox13.Text);
            calificaciones[14] = ParseDecimalOrNull(TextBox14.Text);
            porcentajes[15] = ParseDecimalOrNull(TextBox15.Text);
            if (!calificaciones[0].HasValue || !porcentajes[1].HasValue)
            {
                ShowAlert("Debe ingresar calificación y porcentaje para al menos la Unidad 1");
                return;
            }
            decimal sumaPorcentajes = porcentajes.Where((p, i) => i % 2 == 1 && p.HasValue).Sum(p => p.Value);
            if (sumaPorcentajes > 100)
            {
                ShowAlert("¡La suma de porcentajes no puede exceder 100%!");
                return;
            }
            decimal promedio = CalcularPromedio(calificaciones, porcentajes);
            var alumnoMateriaAdapter = new Alumno_MateriaTableAdapter();
            alumnoMateriaAdapter.Insert1(
                noControl, idMateria, grupoID,
                calificaciones[0], porcentajes[1], calificaciones[2], porcentajes[3],
                calificaciones[4], porcentajes[5], calificaciones[6], porcentajes[7],
                calificaciones[8], porcentajes[9], calificaciones[10], porcentajes[11],
                calificaciones[12], porcentajes[13], calificaciones[14], porcentajes[15],
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

        private decimal CalcularPromedio(decimal?[] calificaciones, decimal?[] porcentajes)
        {
            decimal sumaPonderada = 0;
            decimal sumaPorcentajes = 0;
            for (int i = 0; i < 8; i++)
            {
                int indexCalif = i * 2;     
                int indexPorc = indexCalif + 1;
                if (calificaciones[indexCalif].HasValue && porcentajes[indexPorc].HasValue)
                {
                    if (porcentajes[indexPorc].Value < 0 || porcentajes[indexPorc].Value > 100)
                    {
                        ShowAlert($"¡Error! El porcentaje para U{i + 1} debe estar entre 0 y 100.");
                        return 0;
                    }
                    sumaPonderada += calificaciones[indexCalif].Value * porcentajes[indexPorc].Value;
                    sumaPorcentajes += porcentajes[indexPorc].Value;
                }
            }
            if (sumaPorcentajes > 100)
            {
                ShowAlert("¡Error! La suma de porcentajes no puede exceder 100%.");
                return 0;
            }
            return sumaPorcentajes > 0 ? Math.Round(sumaPonderada / sumaPorcentajes, 2) : 0;
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
            if (!string.IsNullOrEmpty(No_Control.SelectedValue))
            {
                int noControl = int.Parse(No_Control.SelectedValue);
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
        am.Promedio,
        gc.Entrevista1, gc.Entrevista2, gc.Entrevista3,
        gc.A, gc.B
    FROM Alumno_Materia am
    INNER JOIN Alumno a ON am.No_control = a.No_control
    LEFT JOIN Grupo_Compuesto gc ON am.No_control = gc.No_control AND am.ID_Grupo = gc.ID_Grupo
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

                // Verifica que los datos se están cargando correctamente
                System.Diagnostics.Debug.WriteLine($"Registros cargados: {dt.Rows.Count}");
                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Primera fila - Entrevista1: {dt.Rows[0]["Entrevista1"]}");
                }

                // 🔹 Evita DBNull para los campos booleanos antes de enlazar al GridView
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Entrevista1"] == DBNull.Value) row["Entrevista1"] = false;
                    if (row["Entrevista2"] == DBNull.Value) row["Entrevista2"] = false;
                    if (row["Entrevista3"] == DBNull.Value) row["Entrevista3"] = false;
                }

                GridViewAlumnos.DataSource = dt;
                GridViewAlumnos.DataBind();
            }

        }

        protected void GridViewAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                ViewState["NoControlEditar"] = e.CommandArgument.ToString();

                Button2.Visible = false;
                btnActualizar.Visible = true;

                string noControl = e.CommandArgument.ToString();
                int grupoID = (int)ViewState["grupoID"];
                bool esPrimero = EsPrimerRegistro(int.Parse(noControl), grupoID);

                CargarDatosParaEditar(noControl);

                TextBox1.Enabled = esPrimero;
                TextBox3.Enabled = esPrimero;
                TextBox5.Enabled = esPrimero;
                TextBox7.Enabled = esPrimero;
                TextBox9.Enabled = esPrimero;
                TextBox11.Enabled = esPrimero;
                TextBox13.Enabled = esPrimero;
                TextBox15.Enabled = esPrimero;

                ScriptManager.RegisterStartupScript(
                    this, GetType(), "openModal", "$('#mdl').modal('show');", true);
            }
            else if (e.CommandName == "Eliminar")
            {
                string noControl = e.CommandArgument.ToString();
                int grupoID = (int)ViewState["grupoID"];

                EliminarAlumnoMateria(noControl, grupoID);
            }
           else if (e.CommandName == "Imprimir")
{
    string noControl = e.CommandArgument.ToString();
    string rutaPdf = Server.MapPath("~/PDF/Formato/FormatoReporteAlumno.pdf");

    if (File.Exists(rutaPdf))
    {
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader(
            "Content-Disposition",
            $"inline; filename=Reporte_{noControl}.pdf");

        Response.WriteFile(rutaPdf);
        Response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}
            else if (e.CommandName == "Imprimir")
            {
                string noControl = e.CommandArgument.ToString();
                string rutaPdf = Server.MapPath("~/PDF/Formato/FormatoReporteAlumno.pdf");

                if (File.Exists(rutaPdf))
                {
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader(
                        "Content-Disposition",
                        $"inline; filename=Reporte_{noControl}.pdf");

                    Response.WriteFile(rutaPdf);
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }

        }



        private bool EsPrimerRegistro(int noControl, int grupoID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = "SELECT TOP 1 No_control FROM Alumno_Materia WHERE ID_Grupo = @GrupoID ORDER BY No_control";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@GrupoID", grupoID);
                connection.Open();
                object result = command.ExecuteScalar();
                return result != null && Convert.ToInt32(result) == noControl;
            }
        }

        private void EliminarAlumnoMateria(string noControl, int grupoID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string deleteGrupoCompuesto = @"
                DELETE FROM Grupo_Compuesto 
                WHERE No_control = @NoControl AND ID_Grupo = @GrupoID";
                    using (SqlCommand command = new SqlCommand(deleteGrupoCompuesto, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@NoControl", noControl);
                        command.Parameters.AddWithValue("@GrupoID", grupoID);
                        command.ExecuteNonQuery();
                    }
                    string deleteAlumnoMateria = @"
                DELETE FROM Alumno_Materia 
                WHERE No_control = @NoControl AND ID_Grupo = @GrupoID";

                    using (SqlCommand command = new SqlCommand(deleteAlumnoMateria, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@NoControl", noControl);
                        command.Parameters.AddWithValue("@GrupoID", grupoID);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            ShowAlert("Alumno eliminado correctamente.");
                            CargarDatosAlumnoMateria(); 
                        }
                        else
                        {
                            transaction.Rollback();
                            ShowAlert("No se encontró el registro para eliminar.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); 
                    ShowAlert($"Error al eliminar: {ex.Message}");
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
            No_Control.SelectedValue = noControl;
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (ViewState["NoControlEditar"] == null || ViewState["grupoID"] == null)
            {
                ShowAlert("Error: No se identificó el alumno o grupo.");
                return;
            }
            int noControl = Convert.ToInt32(ViewState["NoControlEditar"]);
            int grupoID = (int)ViewState["grupoID"];
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            decimal?[] porcentajes = new decimal?[8];
            porcentajes[0] = ParseDecimalOrNull(TextBox1.Text);
            porcentajes[1] = ParseDecimalOrNull(TextBox3.Text);
            porcentajes[2] = ParseDecimalOrNull(TextBox5.Text);
            porcentajes[3] = ParseDecimalOrNull(TextBox7.Text);
            porcentajes[4] = ParseDecimalOrNull(TextBox9.Text);
            porcentajes[5] = ParseDecimalOrNull(TextBox11.Text);
            porcentajes[6] = ParseDecimalOrNull(TextBox13.Text);
            porcentajes[7] = ParseDecimalOrNull(TextBox15.Text);
            decimal sumaPorcentajes = porcentajes.Sum(p => p ?? 0);
            if (sumaPorcentajes > 100)
            {
                ShowAlert("¡La suma de porcentajes no puede exceder 100%!");
                return;
            }
            decimal?[] calificaciones = new decimal?[16];
            calificaciones[0] = ParseDecimalOrNull(TextBox.Text);
            calificaciones[2] = ParseDecimalOrNull(TextBox2.Text);
            calificaciones[4] = ParseDecimalOrNull(TextBox4.Text);
            calificaciones[6] = ParseDecimalOrNull(TextBox6.Text);
            calificaciones[8] = ParseDecimalOrNull(TextBox8.Text);
            calificaciones[10] = ParseDecimalOrNull(TextBox10.Text);
            calificaciones[12] = ParseDecimalOrNull(TextBox12.Text);
            calificaciones[14] = ParseDecimalOrNull(TextBox14.Text);
            decimal promedio = CalcularPromedio(calificaciones, porcentajes);
            var adapter = new Alumno_MateriaTableAdapter();
            adapter.Update1(
                calificaciones[0], porcentajes[0],
                calificaciones[2], porcentajes[1],
                calificaciones[4], porcentajes[2],
                calificaciones[6], porcentajes[3],
                calificaciones[8], porcentajes[4],
                calificaciones[10], porcentajes[5],
                calificaciones[12], porcentajes[6],
                calificaciones[14], porcentajes[7],
                promedio,
                noControl,
                grupoID
            );
            string updateQuery = @"UPDATE Alumno_Materia 
                         SET u1_2 = @u1_2, u2_2 = @u2_2, u3_2 = @u3_2, u4_2 = @u4_2,
                             u5_2 = @u5_2, u6_2 = @u6_2, u7_2 = @u7_2, u8_2 = @u8_2
                         WHERE ID_Grupo = @grupoID AND No_control != @noControl";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@u1_2", (object)porcentajes[0] ?? DBNull.Value);
                command.Parameters.AddWithValue("@u2_2", (object)porcentajes[1] ?? DBNull.Value);
                command.Parameters.AddWithValue("@u3_2", (object)porcentajes[2] ?? DBNull.Value);
                command.Parameters.AddWithValue("@u4_2", (object)porcentajes[3] ?? DBNull.Value);
                command.Parameters.AddWithValue("@u5_2", (object)porcentajes[4] ?? DBNull.Value);
                command.Parameters.AddWithValue("@u6_2", (object)porcentajes[5] ?? DBNull.Value);
                command.Parameters.AddWithValue("@u7_2", (object)porcentajes[6] ?? DBNull.Value);
                command.Parameters.AddWithValue("@u8_2", (object)porcentajes[7] ?? DBNull.Value);
                command.Parameters.AddWithValue("@grupoID", grupoID);
                command.Parameters.AddWithValue("@noControl", noControl);

                connection.Open();
                command.ExecuteNonQuery();
            }
            CargarDatosAlumnoMateria();
            ShowAlert("¡Datos actualizados correctamente!");
            ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "$('#mdl').modal('hide');", true);
        }




        //ENTREVISTAS
        protected void btnGuardarEntrevistas_Click(object sender, EventArgs e)
        {
            if (ViewState["grupoID"] == null)
            {
                ShowAlert("No se ha identificado el grupo.");
                return;
            }
            Button btnGuardar = (Button)sender;
            GridViewRow row = (GridViewRow)btnGuardar.NamingContainer;
            HiddenField hfNoControl = (HiddenField)row.FindControl("hfNoControl");
            if (hfNoControl == null || string.IsNullOrEmpty(hfNoControl.Value))
            {
                ShowAlert("No se pudo identificar al alumno.");
                return;
            }
            int grupoID = (int)ViewState["grupoID"];
            int noControl = int.Parse(hfNoControl.Value);
            RadioButton RadioButton1 = (RadioButton)row.FindControl("RadioButton1");
            RadioButton RadioButton2 = (RadioButton)row.FindControl("RadioButton2");
            RadioButton RadioButton3 = (RadioButton)row.FindControl("RadioButton3");
            bool entrevista1 = RadioButton1?.Checked ?? false;
            bool entrevista2 = RadioButton2?.Checked ?? false;
            bool entrevista3 = RadioButton3?.Checked ?? false;
            int totalEntrevistas = (entrevista1 ? 1 : 0) + (entrevista2 ? 1 : 0) + (entrevista3 ? 1 : 0);
            string A = totalEntrevistas >= 2 ? "X" : null;
            string B = totalEntrevistas < 2 ? "X" : null;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int idAlumMat = 0;
                decimal promedio = 0;
                string queryAlumno = @"SELECT ID, Promedio FROM Alumno_Materia 
                             WHERE No_control = @NoControl AND ID_Grupo = @GrupoID";
                using (SqlCommand command = new SqlCommand(queryAlumno, connection))
                {
                    command.Parameters.AddWithValue("@NoControl", noControl);
                    command.Parameters.AddWithValue("@GrupoID", grupoID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idAlumMat = reader.GetInt32(0);
                            promedio = reader.GetDecimal(1);
                        }
                        else
                        {
                            ShowAlert("El alumno no tiene registros en esta materia.");
                            return;
                        }
                    }
                }
                string checkQuery = @"SELECT COUNT(*) FROM Grupo_Compuesto 
                            WHERE No_control = @NoControl AND ID_Grupo = @GrupoID";
                int existe = 0;
                using (SqlCommand command = new SqlCommand(checkQuery, connection))
                {
                    command.Parameters.AddWithValue("@NoControl", noControl);
                    command.Parameters.AddWithValue("@GrupoID", grupoID);
                    existe = (int)command.ExecuteScalar();
                }
                if (existe > 0)
                {
                    string updateQuery = @"UPDATE Grupo_Compuesto 
                                SET Entrevista1 = @Ent1, Entrevista2 = @Ent2, Entrevista3 = @Ent3,
                                    A = @A, B = @B, Promedio = @Promedio
                                WHERE No_control = @NoControl AND ID_Grupo = @GrupoID";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Ent1", entrevista1);
                        command.Parameters.AddWithValue("@Ent2", entrevista2);
                        command.Parameters.AddWithValue("@Ent3", entrevista3);
                        command.Parameters.AddWithValue("@A", (object)A ?? DBNull.Value);
                        command.Parameters.AddWithValue("@B", (object)B ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Promedio", promedio);
                        command.Parameters.AddWithValue("@NoControl", noControl);
                        command.Parameters.AddWithValue("@GrupoID", grupoID);

                        command.ExecuteNonQuery();
                        ShowAlert("¡Entrevistas actualizadas!");
                    }
                }
                else
                {
                    string insertQuery = @"INSERT INTO Grupo_Compuesto 
                                (ID_Grupo, No_control, ID_AlumMat, Entrevista1, Entrevista2, Entrevista3, 
                                 A, B, Promedio, Estatus, Semestre)
                                VALUES 
                                (@GrupoID, @NoControl, @IDAlumMat, @Ent1, @Ent2, @Ent3, 
                                 @A, @B, @Promedio, 'EN PROCESO', (SELECT Semestre FROM Alumno WHERE No_control = @NoControl))";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@GrupoID", grupoID);
                        command.Parameters.AddWithValue("@NoControl", noControl);
                        command.Parameters.AddWithValue("@IDAlumMat", idAlumMat);
                        command.Parameters.AddWithValue("@Ent1", entrevista1);
                        command.Parameters.AddWithValue("@Ent2", entrevista2);
                        command.Parameters.AddWithValue("@Ent3", entrevista3);
                        command.Parameters.AddWithValue("@A", (object)A ?? DBNull.Value);
                        command.Parameters.AddWithValue("@B", (object)B ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Promedio", promedio);

                        command.ExecuteNonQuery();
                        ShowAlert("¡Entrevistas guardadas!");
                    }
                }
            }
            CargarDatosAlumnoMat();
        }

        private void CargarDatosAlumnoMat()
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
            am.Promedio,
            gc.Entrevista1, gc.Entrevista2, gc.Entrevista3,
            gc.A, gc.B
            FROM Alumno_Materia am
            INNER JOIN Alumno a ON am.No_control = a.No_control
            LEFT JOIN Grupo_Compuesto gc ON am.No_control = gc.No_control AND am.ID_Grupo = gc.ID_Grupo
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





        protected void btn_addEntrevistas(object sender, EventArgs e)
        {
            var Grupo_CompuestoAdapter = new Grupo_CompuestoTableAdapter();

        }


        public int GrupoID
        {
            get { return ViewState["grupoID"] != null ? (int)ViewState["grupoID"] : 0; }
        }
    }
}