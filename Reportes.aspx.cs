using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Configuration;


namespace TutoriasWeb
{
    public partial class Reportes : System.Web.UI.Page
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

                    // YA EXISTÍA
                    CargarDatosReporte(grupoID);

                    // ===== NUEVO: CARGAR MATERIAS REPROBADAS =====
                    CargarMateriasReprobadas(grupoID);
                    // ============================================

                    CargarFechasSesiones(grupoID);

                    CargarObservaciones(grupoID);
                    
                    CargarSituaciones(grupoID);

                    CargarResultadosEspecificos(grupoID);

                    CargarDatosEncabezado(grupoID);

                }
                else
                {
                    // nada
                }
            }
        }


        private void CargarDatosReporte(int grupoID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;
            string query = @"
SELECT 
    a.No_control,
    a.Nombre + ' ' + a.A_Paterno + ' ' + a.A_Materno AS NombreCompleto,
    gc.Entrevista1,
    gc.Entrevista2,
    gc.Entrevista3,
    gc.A,
    gc.B,

    -- ACTIVIDADES (todas visibles para todos)
    gc.cal1,
    gc.cal2,
    gc.cal3,
    gc.cal4,
    gc.cal5,
    gc.cal6,

    gc.Promedio,
    gc.D,
    gc.N,
    gc.I,
    gc.R

FROM Grupo_Compuesto gc
INNER JOIN Alumno a ON gc.No_control = a.No_control
WHERE gc.ID_Grupo = @GrupoID
ORDER BY a.No_control";


            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@GrupoID", grupoID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                rptAlumnos.DataSource = dt;
                rptAlumnos.DataBind();
            }
        }



        //Editar

        protected void Calificacion_Changed(object sender, EventArgs e)
        {
            CheckBox radio = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)radio.NamingContainer;

            // 1. Obtener y validar el promedio
            Label lblPromedio = (Label)item.FindControl("lblPromedio");
            if (!decimal.TryParse(lblPromedio.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal promedio))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Formato de promedio inválido');", true);
                return;
            }

            // 2. Obtener valores de los RadioButtons
            bool cal1 = ((CheckBox)item.FindControl("rbCal1")).Checked;
            bool cal2 = ((CheckBox)item.FindControl("rbCal2")).Checked;
            bool cal3 = ((CheckBox)item.FindControl("rbCal3")).Checked;
            bool cal4 = ((CheckBox)item.FindControl("rbCal4")).Checked;
            bool cal5 = ((CheckBox)item.FindControl("rbCal5")).Checked;
            bool cal6 = ((CheckBox)item.FindControl("rbCal6")).Checked;

            // 3. Calcular situación final
            string situacionFinal = CalcularSituacionFinal(cal1, cal2, cal3, cal4, cal5, cal6, promedio);

            // 4. Actualizar visualización
            CheckBox rbD = (CheckBox)item.FindControl("rbD");
            CheckBox rbN = (CheckBox)item.FindControl("rbN");
            CheckBox rbI = (CheckBox)item.FindControl("rbI");
            CheckBox rbR = (CheckBox)item.FindControl("rbR");

            rbD.Checked = situacionFinal == "D";
            rbN.Checked = situacionFinal == "N";
            rbI.Checked = situacionFinal == "I";
            rbR.Checked = situacionFinal == "R";

            // 5. Obtener datos para actualización
            string noControl = ((Label)item.FindControl("lblNoControl")).Text;
            int grupoID = (int)ViewState["grupoID"];

            // 6. Llamar al método de actualización
            ActualizarCalificaciones(noControl, grupoID, cal1, cal2, cal3, cal4, cal5, cal6, situacionFinal, promedio);
        }


        private void CalcularYMostrarSituacion(RepeaterItem item)
        {
            bool cal1 = ((CheckBox)item.FindControl("rbCal1")).Checked;
            bool cal2 = ((CheckBox)item.FindControl("rbCal2")).Checked;
            bool cal3 = ((CheckBox)item.FindControl("rbCal3")).Checked;
            bool cal4 = ((CheckBox)item.FindControl("rbCal4")).Checked;
            bool cal5 = ((CheckBox)item.FindControl("rbCal5")).Checked;
            bool cal6 = ((CheckBox)item.FindControl("rbCal6")).Checked;
            decimal promedio = Convert.ToDecimal(((Label)item.FindControl("lblPromedio")).Text);
            int materiasReprobadas = 6 - (cal1 ? 1 : 0) - (cal2 ? 1 : 0) - (cal3 ? 1 : 0)
                                   - (cal4 ? 1 : 0) - (cal5 ? 1 : 0) - (cal6 ? 1 : 0);
            CheckBox rbD = (CheckBox)item.FindControl("rbD");
            CheckBox rbN = (CheckBox)item.FindControl("rbN");
            CheckBox rbI = (CheckBox)item.FindControl("rbI");
            CheckBox rbR = (CheckBox)item.FindControl("rbR");
            rbD.Checked = false;
            rbN.Checked = false;
            rbI.Checked = false;
            rbR.Checked = false;
            if (materiasReprobadas == 0 && promedio > 90)
                rbD.Checked = true;
            else if (materiasReprobadas == 0)
                rbN.Checked = true;
            else if (materiasReprobadas <= 2)
                rbI.Checked = true;
            else
                rbR.Checked = true;
        }

        protected void btnGuardarTodo_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptAlumnos.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblPromedio = (Label)item.FindControl("lblPromedio");
                    if (!decimal.TryParse(lblPromedio.Text,
                        NumberStyles.Any, CultureInfo.InvariantCulture,
                        out decimal promedio))
                    {
                        string nombreAlumno = ((Label)item.FindControl("lblNombre")).Text;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            $"alert('Error en el promedio del alumno: {nombreAlumno}');", true);
                        continue;
                    }

                    bool cal1 = ((CheckBox)item.FindControl("rbCal1")).Checked;
                    bool cal2 = ((CheckBox)item.FindControl("rbCal2")).Checked;
                    bool cal3 = ((CheckBox)item.FindControl("rbCal3")).Checked;
                    bool cal4 = ((CheckBox)item.FindControl("rbCal4")).Checked;
                    bool cal5 = ((CheckBox)item.FindControl("rbCal5")).Checked;
                    bool cal6 = ((CheckBox)item.FindControl("rbCal6")).Checked;

                    HiddenField hfCal1 = (HiddenField)item.FindControl("hfCal1");
                    HiddenField hfCal2 = (HiddenField)item.FindControl("hfCal2");
                    HiddenField hfCal3 = (HiddenField)item.FindControl("hfCal3");
                    HiddenField hfCal4 = (HiddenField)item.FindControl("hfCal4");
                    HiddenField hfCal5 = (HiddenField)item.FindControl("hfCal5");
                    HiddenField hfCal6 = (HiddenField)item.FindControl("hfCal6");

                    bool cal1Original = hfCal1.Value == "1";
                    bool cal2Original = hfCal2.Value == "1";
                    bool cal3Original = hfCal3.Value == "1";
                    bool cal4Original = hfCal4.Value == "1";
                    bool cal5Original = hfCal5.Value == "1";
                    bool cal6Original = hfCal6.Value == "1";

                    bool huboCambios =
                        cal1 != cal1Original || cal2 != cal2Original ||
                        cal3 != cal3Original || cal4 != cal4Original ||
                        cal5 != cal5Original || cal6 != cal6Original;

                    if (huboCambios)
                    {
                        string noControl = ((Label)item.FindControl("lblNoControl")).Text;
                        int grupoID = (int)ViewState["grupoID"];
                        string situacionFinal = CalcularSituacionFinal(
                            cal1, cal2, cal3, cal4, cal5, cal6, promedio);

                        ActualizarCalificaciones(
                            noControl, grupoID,
                            cal1, cal2, cal3, cal4, cal5, cal6,
                            situacionFinal, promedio);
                    }
                }
            }

            // ====== NUEVO: GUARDAR FECHAS DE SESIONES ======
            int grupoIDFechas = (int)ViewState["grupoID"];

            GuardarFechasSesiones(
                grupoIDFechas,
                txtFecha0.Text.Trim(),
                txtFecha1.Text.Trim(),
                txtFecha2.Text.Trim(),
                txtFecha3.Text.Trim(),
                txtFecha4.Text.Trim()
            );
            // ==============================================

            // ===== OBSERVACIONES =====
            int grupoIDObservaciones = (int)ViewState["grupoID"];

            GuardarObservaciones(
                grupoIDObservaciones,
                txtObservaciones.Text.Trim()
            );

            GuardarSituaciones(
    (int)ViewState["grupoID"],
    txtCtrl1.Text, txtNom1.Text, ddlSit1.SelectedItem.Text,
    txtCtrl2.Text, txtNom2.Text, ddlSit2.SelectedItem.Text,
    txtCtrl3.Text, txtNom3.Text, ddlSit3.SelectedItem.Text
);
            // ===== GUARDAR RESULTADOS ESPECÍFICOS =====
            int grupoIDResultados = (int)ViewState["grupoID"];

            GuardarResultadosEspecificos(
                grupoIDResultados,
                txtCirculos.Text.Trim(),
                txtMedica.Text.Trim(),
                txtPlaticas.Text.Trim(),
                txtPsicologica.Text.Trim(),
                txtExterno.Text.Trim()
            );
            // =========================================
            int grupoIDEnc = (int)ViewState["grupoID"];

            GuardarDatosEncabezado(
                grupoIDEnc,
                txtSemestreInforme.Text.Trim(),
                txtGrupo.Text.Trim(),
                txtCarrera.Text.Trim(),
                txtNombreTutor.Text.Trim(),
                txtFechaInforme.Text.Trim(),
                txtAsignados.Text.Trim(),
                txtAtendidos.Text.Trim(),
                txtSemestre.Text.Trim()
            );


            // ====== GUARDAR MATERIAS REPROBADAS ======
            int grupoIDMaterias = (int)ViewState["grupoID"];

            GuardarMateriasReprobadas(
                grupoIDMaterias,
                txtMateria1.Text.Trim(),
                txtMateria2.Text.Trim(),
                txtMateria3.Text.Trim(),
                txtMateria4.Text.Trim(),
                txtMateria5.Text.Trim(),
                txtMateria6.Text.Trim(),
                txtMateria7.Text.Trim()
            );
            // ========================================

            CargarDatosReporte(grupoIDMaterias);

            ClientScript.RegisterStartupScript(
                this.GetType(),
                "ok",
                "alert('Todos los cambios han sido guardados correctamente');",
                true
            );
        }


        private void GuardarMateriasReprobadas(
      int grupoID,
      string m1, string m2, string m3,
      string m4, string m5, string m6, string m7)
        {
            string connectionString =
                System.Configuration.ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
        IF EXISTS (SELECT 1 FROM MateriasReprobadas WHERE GrupoID = @GrupoID)
        BEGIN
            UPDATE MateriasReprobadas
            SET Materia1 = @M1,
                Materia2 = @M2,
                Materia3 = @M3,
                Materia4 = @M4,
                Materia5 = @M5,
                Materia6 = @M6,
                Materia7 = @M7
            WHERE GrupoID = @GrupoID
        END
        ELSE
        BEGIN
            INSERT INTO MateriasReprobadas
            (GrupoID, Materia1, Materia2, Materia3, Materia4, Materia5, Materia6, Materia7)
            VALUES
            (@GrupoID, @M1, @M2, @M3, @M4, @M5, @M6, @M7)
        END";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                    cmd.Parameters.AddWithValue("@M1", m1);
                    cmd.Parameters.AddWithValue("@M2", m2);
                    cmd.Parameters.AddWithValue("@M3", m3);
                    cmd.Parameters.AddWithValue("@M4", m4);
                    cmd.Parameters.AddWithValue("@M5", m5);
                    cmd.Parameters.AddWithValue("@M6", m6);
                    cmd.Parameters.AddWithValue("@M7", m7);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CargarMateriasReprobadas(int grupoID)
        {
            string connectionString =
                System.Configuration.ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
        SELECT Materia1, Materia2, Materia3, Materia4,
               Materia5, Materia6, Materia7
        FROM MateriasReprobadas
        WHERE GrupoID = @GrupoID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            // ===== TEXTBOX =====
                            txtMateria1.Text = dr["Materia1"].ToString();
                            txtMateria2.Text = dr["Materia2"].ToString();
                            txtMateria3.Text = dr["Materia3"].ToString();
                            txtMateria4.Text = dr["Materia4"].ToString();
                            txtMateria5.Text = dr["Materia5"].ToString();
                            txtMateria6.Text = dr["Materia6"].ToString();
                            txtMateria7.Text = dr["Materia7"].ToString();

                          
                        }
                    }
                }

            }

        }



        private void GuardarFechasSesiones(
    int grupoID,
    string f0, string f1, string f2, string f3, string f4)
        {
            string connectionString =
                System.Configuration.ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
IF EXISTS (SELECT 1 FROM FechasSesiones WHERE GrupoID = @GrupoID)
    UPDATE FechasSesiones SET
        Fecha0 = @F0,
        Fecha1 = @F1,
        Fecha2 = @F2,
        Fecha3 = @F3,
        Fecha4 = @F4
    WHERE GrupoID = @GrupoID
ELSE
    INSERT INTO FechasSesiones
    (GrupoID, Fecha0, Fecha1, Fecha2, Fecha3, Fecha4)
    VALUES
    (@GrupoID, @F0, @F1, @F2, @F3, @F4)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                    cmd.Parameters.AddWithValue("@F0", f0);
                    cmd.Parameters.AddWithValue("@F1", f1);
                    cmd.Parameters.AddWithValue("@F2", f2);
                    cmd.Parameters.AddWithValue("@F3", f3);
                    cmd.Parameters.AddWithValue("@F4", f4);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CargarFechasSesiones(int grupoID)
        {
            string connectionString =
                System.Configuration.ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
SELECT Fecha0, Fecha1, Fecha2, Fecha3, Fecha4
FROM FechasSesiones

WHERE GrupoID = @GrupoID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtFecha0.Text = dr["Fecha0"].ToString();
                            txtFecha1.Text = dr["Fecha1"].ToString();
                            txtFecha2.Text = dr["Fecha2"].ToString();
                            txtFecha3.Text = dr["Fecha3"].ToString();
                            txtFecha4.Text = dr["Fecha4"].ToString();
                        }
                    }
                }
            }
        }
        private void GuardarObservaciones(int grupoID, string observaciones)
        {
            string connectionString =
                System.Configuration.ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
IF EXISTS (SELECT 1 FROM ObservacionesGrupo WHERE GrupoID = @GrupoID)
    UPDATE ObservacionesGrupo
    SET Observaciones = @Obs
    WHERE GrupoID = @GrupoID
ELSE
    INSERT INTO ObservacionesGrupo (GrupoID, Observaciones)
    VALUES (@GrupoID, @Obs)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                    cmd.Parameters.AddWithValue("@Obs", observaciones);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void CargarObservaciones(int grupoID)
        {
            string connectionString =
                System.Configuration.ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
SELECT Observaciones
FROM ObservacionesGrupo
WHERE GrupoID = @GrupoID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtObservaciones.Text = dr["Observaciones"].ToString();
                        }
                    }
                }

            }
        }

        private void GuardarSituaciones(
    int grupoID,
    string c1, string n1, string s1,
    string c2, string n2, string s2,
    string c3, string n3, string s3)
        {
            string cs = ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"
IF EXISTS (SELECT 1 FROM SituacionesEspeciales WHERE GrupoID=@GrupoID)
UPDATE SituacionesEspeciales SET
Ctrl1=@C1, Nom1=@N1, Sit1=@S1,
Ctrl2=@C2, Nom2=@N2, Sit2=@S2,
Ctrl3=@C3, Nom3=@N3, Sit3=@S3
WHERE GrupoID=@GrupoID
ELSE
INSERT INTO SituacionesEspeciales
VALUES (@GrupoID,@C1,@N1,@S1,@C2,@N2,@S2,@C3,@N3,@S3)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                    cmd.Parameters.AddWithValue("@C1", c1);
                    cmd.Parameters.AddWithValue("@N1", n1);
                    cmd.Parameters.AddWithValue("@S1", s1);
                    cmd.Parameters.AddWithValue("@C2", c2);
                    cmd.Parameters.AddWithValue("@N2", n2);
                    cmd.Parameters.AddWithValue("@S2", s2);
                    cmd.Parameters.AddWithValue("@C3", c3);
                    cmd.Parameters.AddWithValue("@N3", n3);
                    cmd.Parameters.AddWithValue("@S3", s3);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void CargarSituaciones(int grupoID)
        {
            string connectionString =
                System.Configuration.ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
SELECT 
    Ctrl1, Nom1, Sit1,
    Ctrl2, Nom2, Sit2,
    Ctrl3, Nom3, Sit3
FROM SituacionesEspeciales
WHERE GrupoID = @GrupoID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtCtrl1.Text = dr["Ctrl1"].ToString();
                            txtNom1.Text = dr["Nom1"].ToString();
                            ddlSit1.SelectedValue = dr["Sit1"].ToString();

                            txtCtrl2.Text = dr["Ctrl2"].ToString();
                            txtNom2.Text = dr["Nom2"].ToString();
                            ddlSit2.SelectedValue = dr["Sit2"].ToString();

                            txtCtrl3.Text = dr["Ctrl3"].ToString();
                            txtNom3.Text = dr["Nom3"].ToString();
                            ddlSit3.SelectedValue = dr["Sit3"].ToString();
                        }
                    }
                }
            }
        }

        private void GuardarResultadosEspecificos(
    int grupoID,
    string c, string m, string p, string psi, string e)
        {
            string cs = ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"
IF EXISTS (SELECT 1 FROM ResultadosEspecificos WHERE GrupoID=@GrupoID)
    UPDATE ResultadosEspecificos SET
        Circulos=@C,
        Medica=@M,
        Platicas=@P,
        Psicologica=@PSI,
        Externo=@E
    WHERE GrupoID=@GrupoID
ELSE
    INSERT INTO ResultadosEspecificos
    (GrupoID, Circulos, Medica, Platicas, Psicologica, Externo)
    VALUES
    (@GrupoID, @C, @M, @P, @PSI, @E)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                cmd.Parameters.AddWithValue("@C", c);
                cmd.Parameters.AddWithValue("@M", m);
                cmd.Parameters.AddWithValue("@P", p);
                cmd.Parameters.AddWithValue("@PSI", psi);
                cmd.Parameters.AddWithValue("@E", e);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void CargarResultadosEspecificos(int grupoID)
        {
            string cs = ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"SELECT * FROM ResultadosEspecificos WHERE GrupoID=@GrupoID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@GrupoID", grupoID);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCirculos.Text = dr["Circulos"].ToString();
                    txtMedica.Text = dr["Medica"].ToString();
                    txtPlaticas.Text = dr["Platicas"].ToString();
                    txtPsicologica.Text = dr["Psicologica"].ToString();
                    txtExterno.Text = dr["Externo"].ToString();
                }
            }
        }

        private void GuardarDatosEncabezado(
    int grupoID,
    string semestreInf,
    string grupo,
    string carrera,
    string tutor,
    string fechaInf,
    string asignados,
    string atendidos,
    string semestre)
        {
            string cs = ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"
IF EXISTS (SELECT 1 FROM DatosEncabezadoReporte WHERE GrupoID=@GrupoID)
    UPDATE DatosEncabezadoReporte SET
        SemestreInforme=@SemestreInf,
        GrupoTexto=@Grupo,
        Carrera=@Carrera,
        NombreTutor=@Tutor,
        FechaInforme=@FechaInf,
        AlumnosAsignados=@Asignados,
        AlumnosAtendidos=@Atendidos,
        Semestre=@Semestre
    WHERE GrupoID=@GrupoID
ELSE
    INSERT INTO DatosEncabezadoReporte
    (GrupoID,SemestreInforme,GrupoTexto,Carrera,NombreTutor,FechaInforme,
     AlumnosAsignados,AlumnosAtendidos,Semestre)
    VALUES
    (@GrupoID,@SemestreInf,@Grupo,@Carrera,@Tutor,@FechaInf,
     @Asignados,@Atendidos,@Semestre)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@GrupoID", grupoID);
                cmd.Parameters.AddWithValue("@SemestreInf", semestreInf);
                cmd.Parameters.AddWithValue("@Grupo", grupo);
                cmd.Parameters.AddWithValue("@Carrera", carrera);
                cmd.Parameters.AddWithValue("@Tutor", tutor);
                cmd.Parameters.AddWithValue("@FechaInf", fechaInf);
                cmd.Parameters.AddWithValue("@Asignados", asignados);
                cmd.Parameters.AddWithValue("@Atendidos", atendidos);
                cmd.Parameters.AddWithValue("@Semestre", semestre);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }





        private void CargarDatosEncabezado(int grupoID)
        {
            string cs = ConfigurationManager
                .ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"SELECT * FROM DatosEncabezadoReporte WHERE GrupoID=@GrupoID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@GrupoID", grupoID);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtSemestreInforme.Text = dr["SemestreInforme"].ToString();
                    txtGrupo.Text = dr["GrupoTexto"].ToString();
                    txtCarrera.Text = dr["Carrera"].ToString();
                    txtNombreTutor.Text = dr["NombreTutor"].ToString();
                    txtFechaInforme.Text = dr["FechaInforme"].ToString();
                    txtAsignados.Text = dr["AlumnosAsignados"].ToString();
                    txtAtendidos.Text = dr["AlumnosAtendidos"].ToString();
                    txtSemestre.Text = dr["Semestre"].ToString();
                }
            }
        }






        private string CalcularSituacionFinal(bool cal1, bool cal2, bool cal3, bool cal4, bool cal5, bool cal6, decimal promedio)
        {
            int materiasReprobadas = 6 -
                (cal1 ? 1 : 0) - (cal2 ? 1 : 0) - (cal3 ? 1 : 0) -
                (cal4 ? 1 : 0) - (cal5 ? 1 : 0) - (cal6 ? 1 : 0);

            if (materiasReprobadas == 0 && promedio > 90)
                return "D";
            else if (materiasReprobadas == 0)
                return "N";
            else if (materiasReprobadas <= 2)
                return "I";
            else
                return "R";
        }


        private void ActualizarCalificaciones(string noControl, int grupoID, bool cal1, bool cal2,
        bool cal3, bool cal4, bool cal5, bool cal6, string situacionFinal, decimal promedio)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL completa con todos los parámetros
                string query = @"UPDATE Grupo_Compuesto 
                SET Cal1 = @Cal1,
                    Cal2 = @Cal2,
                    Cal3 = @Cal3,
                    Cal4 = @Cal4,
                    Cal5 = @Cal5,
                    Cal6 = @Cal6,
                    D = @D,
                    N = @N,
                    I = @I,
                    R = @R,
                    Promedio = @Promedio
                WHERE No_control = @NoControl AND ID_Grupo = @GrupoID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        // 1. Parámetros para calificaciones (decimal)
                        command.Parameters.Add("@Cal1", SqlDbType.Decimal).Value = cal1 ? 1.0m : 0.0m;
                        command.Parameters["@Cal1"].Precision = 5;
                        command.Parameters["@Cal1"].Scale = 2;

                        command.Parameters.Add("@Cal2", SqlDbType.Decimal).Value = cal2 ? 1.0m : 0.0m;
                        command.Parameters["@Cal2"].Precision = 5;
                        command.Parameters["@Cal2"].Scale = 2;

                        command.Parameters.Add("@Cal3", SqlDbType.Decimal).Value = cal3 ? 1.0m : 0.0m;
                        command.Parameters["@Cal3"].Precision = 5;
                        command.Parameters["@Cal3"].Scale = 2;

                        command.Parameters.Add("@Cal4", SqlDbType.Decimal).Value = cal4 ? 1.0m : 0.0m;
                        command.Parameters["@Cal4"].Precision = 5;
                        command.Parameters["@Cal4"].Scale = 2;

                        command.Parameters.Add("@Cal5", SqlDbType.Decimal).Value = cal5 ? 1.0m : 0.0m;
                        command.Parameters["@Cal5"].Precision = 5;
                        command.Parameters["@Cal5"].Scale = 2;

                        command.Parameters.Add("@Cal6", SqlDbType.Decimal).Value = cal6 ? 1.0m : 0.0m;
                        command.Parameters["@Cal6"].Precision = 5;
                        command.Parameters["@Cal6"].Scale = 2;

                        // 2. Parámetros para situación final (asegurando que nunca sean nulos)
                        command.Parameters.Add("@D", SqlDbType.VarChar, 10).Value = situacionFinal == "D" ? "X" : string.Empty;
                        command.Parameters.Add("@N", SqlDbType.VarChar, 10).Value = situacionFinal == "N" ? "X" : string.Empty;
                        command.Parameters.Add("@I", SqlDbType.VarChar, 10).Value = situacionFinal == "I" ? "X" : string.Empty;
                        command.Parameters.Add("@R", SqlDbType.VarChar, 10).Value = situacionFinal == "R" ? "X" : string.Empty;

                        // 3. Parámetro para el promedio
                        SqlParameter promedioParam = new SqlParameter("@Promedio", SqlDbType.Decimal);
                        promedioParam.Precision = 5;
                        promedioParam.Scale = 2;
                        promedioParam.Value = Math.Round(promedio, 2);
                        command.Parameters.Add(promedioParam);

                        // 4. Parámetros de identificación
                        command.Parameters.Add("@NoControl", SqlDbType.Int).Value = int.Parse(noControl);
                        command.Parameters.Add("@GrupoID", SqlDbType.Int).Value = grupoID;

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"No se actualizó ningún registro para NoControl: {noControl}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Registro detallado del error
                        StringBuilder errorDetails = new StringBuilder();
                        errorDetails.AppendLine("Error al ejecutar la consulta SQL:");
                        errorDetails.AppendLine($"Consulta: {query}");
                        errorDetails.AppendLine("Parámetros enviados:");

                        foreach (SqlParameter p in command.Parameters)
                        {
                            errorDetails.AppendLine($"{p.ParameterName}: {p.Value} (Tipo: {p.SqlDbType}, Tamaño: {p.Size})");
                        }

                        System.Diagnostics.Debug.WriteLine(errorDetails.ToString());

                        // Mostrar mensaje al usuario sin detalles técnicos
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Ocurrió un error al guardar los cambios. Por favor intente nuevamente.');", true);
                    }
                }
            }
        }


        protected void gvAlumnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Aquí puedes agregar lógica adicional si necesitas
                // manipular los controles después del data binding
            }
        }







        //Descargar PDF

        protected async void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            await GenerarReportePDF();
        }

        protected async Task GenerarReportePDF()
        {
            // 1. Cargar HTML base
            string ruta = Server.MapPath("~/PDF/Formato/FormatoReporte.html");
            string html = File.ReadAllText(ruta);

            // ====== REEMPLAZAR MATERIAS REPROBADAS (MANUALES) ======
            html = html.Replace("{{MAT1}}", txtMateria1.Text.Trim());
            html = html.Replace("{{MAT2}}", txtMateria2.Text.Trim());
            html = html.Replace("{{MAT3}}", txtMateria3.Text.Trim());
            html = html.Replace("{{MAT4}}", txtMateria4.Text.Trim());
            html = html.Replace("{{MAT5}}", txtMateria5.Text.Trim());
            html = html.Replace("{{MAT6}}", txtMateria6.Text.Trim());
            html = html.Replace("{{MAT7}}", txtMateria7.Text.Trim());
            // ======================================================

            html = html.Replace("{{F0}}", txtFecha0.Text);
            html = html.Replace("{{F1}}", txtFecha1.Text);
            html = html.Replace("{{F2}}", txtFecha2.Text);
            html = html.Replace("{{F3}}", txtFecha3.Text);
            html = html.Replace("{{F4}}", txtFecha4.Text);
            // ======================================================
            html = html.Replace("{{CTRL1}}", txtCtrl1.Text);
            html = html.Replace("{{NOM1}}", txtNom1.Text);
            html = html.Replace("{{SIT1}}", ddlSit1.SelectedItem.Text);

            html = html.Replace("{{CTRL1}}", txtCtrl1.Text);
            html = html.Replace("{{NOM1}}", txtNom1.Text);
            html = html.Replace("{{SIT1}}", ddlSit1.SelectedItem.Text);

            html = html.Replace("{{CTRL2}}", txtCtrl2.Text);
            html = html.Replace("{{NOM2}}", txtNom2.Text);
            html = html.Replace("{{SIT2}}", ddlSit2.SelectedItem.Text);

            html = html.Replace("{{CTRL3}}", txtCtrl3.Text);
            html = html.Replace("{{NOM3}}", txtNom3.Text);
            html = html.Replace("{{SIT3}}", ddlSit3.SelectedItem.Text);

            html = html.Replace("{{CIRC}}", txtCirculos.Text);
            html = html.Replace("{{MED}}", txtMedica.Text);
            html = html.Replace("{{PLAT}}", txtPlaticas.Text);
            html = html.Replace("{{PSI}}", txtPsicologica.Text);
            html = html.Replace("{{EXT}}", txtExterno.Text);

            html = html.Replace("{{RES_CIRCULOS}}", txtCirculos.Text);
            html = html.Replace("{{RES_MEDICA}}", txtMedica.Text);
            html = html.Replace("{{RES_PLATICAS}}", txtPlaticas.Text);
            html = html.Replace("{{RES_PSICO}}", txtPsicologica.Text);
            html = html.Replace("{{RES_EXTERNO}}", txtExterno.Text);

            html = html.Replace("{{SEM_INF}}", txtSemestreInforme.Text);
            html = html.Replace("{{GRUPO}}", txtGrupo.Text);
            html = html.Replace("{{CARRERA}}", txtCarrera.Text);
            html = html.Replace("{{TUTOR}}", txtNombreTutor.Text);
            html = html.Replace("{{FECHA_INF}}", txtFechaInforme.Text);
            html = html.Replace("{{ASIGNADOS}}", txtAsignados.Text);
            html = html.Replace("{{ATENDIDOS}}", txtAtendidos.Text);
            html = html.Replace("{{SEMESTRE}}", txtSemestre.Text);

            html = html.Replace("{{SEMESTRE_INFORME}}", txtSemestreInforme.Text);
            html = html.Replace("{{GRUPO}}", txtGrupo.Text);
            html = html.Replace("{{CARRERA}}", txtCarrera.Text);
            html = html.Replace("{{NOMBRE_TUTOR}}", txtNombreTutor.Text);
            html = html.Replace("{{FECHA_INFORME}}", txtFechaInforme.Text);
            html = html.Replace("{{ASIGNADOS}}", txtAsignados.Text);
            html = html.Replace("{{ATENDIDOS}}", txtAtendidos.Text);
            html = html.Replace("{{SEMESTRE}}", txtSemestre.Text);


            // ===== REPLACE DE OBSERVACIONES =====
            html = html.Replace(
                "{{OBS}}",
                txtObservaciones.Text.Replace("\r\n", "<br/>")
            );




            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            html = html.Replace("{{base}}", baseUrl);

            // ================= TABLA ALUMNOS (TU CÓDIGO) =================
            StringBuilder tabla = new StringBuilder();

            using (SqlConnection conn = new SqlConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString))
            {
                conn.Open();
                string sql = @"
        SELECT gc.No_control, a.Nombre + ' ' + a.A_Paterno + ' ' + a.A_Materno AS NombreCompleto,
               gc.Entrevista1, gc.Entrevista2, gc.Entrevista3,
               gc.A, gc.B, gc.Cal1, gc.Cal2, gc.Cal3, gc.Cal4, gc.Cal5, gc.Cal6,
               gc.Promedio, gc.D, gc.N, gc.I, gc.R, ISNULL(gc.Comentarios, '') AS Comentarios
        FROM Grupo_Compuesto gc
        INNER JOIN Alumno a ON gc.No_control = a.No_control
        WHERE gc.ID_Grupo = @GrupoID
        ORDER BY a.No_control";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", (int)ViewState["grupoID"]);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tabla.Append("<tr>");
                            tabla.Append($"<td>{reader["No_control"]}</td>");
                            tabla.Append($"<td>{reader["NombreCompleto"]}</td>");
                            tabla.Append($"<td>{((bool)reader["Entrevista1"] ? "x" : "")}</td>");
                            tabla.Append($"<td>{((bool)reader["Entrevista2"] ? "x" : "")}</td>");
                            tabla.Append($"<td>{((bool)reader["Entrevista3"] ? "x" : "")}</td>");
                            tabla.Append($"<td>{reader["A"]}</td>");
                            tabla.Append($"<td>{reader["B"]}</td>");
                            tabla.Append($"<td>{reader["Cal1"]}</td>");
                            tabla.Append($"<td>{reader["Cal2"]}</td>");
                            tabla.Append($"<td>{reader["Cal3"]}</td>");
                            tabla.Append($"<td>{reader["Cal4"]}</td>");
                            tabla.Append($"<td>{reader["Cal5"]}</td>");
                            tabla.Append($"<td>{reader["Cal6"]}</td>");
                            tabla.Append($"<td>{reader["Promedio"]}</td>");
                            tabla.Append($"<td>{reader["D"]}</td>");
                            tabla.Append($"<td>{reader["N"]}</td>");
                            tabla.Append($"<td>{reader["I"]}</td>");
                            tabla.Append($"<td>{reader["R"]}</td>");
                            tabla.Append("</tr>");
                        }
                    }
                }
            }

            html = html.Replace("{{TABLA_ALUMNOS}}", tabla.ToString());

            // ================== TABLA MATERIAS REPROBADAS (LO NUEVO) ==================
            StringBuilder tablaMaterias = new StringBuilder();

            using (SqlConnection conn = new SqlConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["TutoriasConnectionString"].ConnectionString))
            {
                conn.Open();
                string sqlMaterias = @"
        SELECT Materia1, Materia2, Materia3, Materia4,
               Materia5, Materia6, Materia7
        FROM MateriasReprobadas
        WHERE GrupoID = @GrupoID";

                using (SqlCommand cmd = new SqlCommand(sqlMaterias, conn))
                {
                    cmd.Parameters.AddWithValue("@GrupoID", (int)ViewState["grupoID"]);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            tablaMaterias.Append("<tr>");
                            tablaMaterias.Append($"<td>{dr["Materia1"]}</td>");
                            tablaMaterias.Append($"<td>{dr["Materia2"]}</td>");
                            tablaMaterias.Append($"<td>{dr["Materia3"]}</td>");
                            tablaMaterias.Append($"<td>{dr["Materia4"]}</td>");
                            tablaMaterias.Append($"<td>{dr["Materia5"]}</td>");
                            tablaMaterias.Append($"<td>{dr["Materia6"]}</td>");
                            tablaMaterias.Append($"<td>{dr["Materia7"]}</td>");
                            tablaMaterias.Append("</tr>");
                        }
                    }
                }
            }

            // ====== GUARDAR FECHAS DE SESIONES ======
            int grupoIDFechas = (int)ViewState["grupoID"];

            GuardarFechasSesiones(
                grupoIDFechas,
                txtFecha0.Text.Trim(),
                txtFecha1.Text.Trim(),
                txtFecha2.Text.Trim(),
                txtFecha3.Text.Trim(),
                txtFecha4.Text.Trim()
            );





            html = html.Replace("{{TABLA_MATERIAS_REPROBADAS}}", tablaMaterias.ToString());

            // ================= PDF (TU CÓDIGO) =================
            await new BrowserFetcher().DownloadAsync();
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();

            await page.SetContentAsync(html, new NavigationOptions
            {
                WaitUntil = new[] { WaitUntilNavigation.Load }
            });

            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                Landscape = true,
                PrintBackground = true
            });

            await browser.CloseAsync();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=InformeTutorias.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
        }
        




    }
}



