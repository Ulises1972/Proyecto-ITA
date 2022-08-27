using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TutoriasWeb
{
    public partial class Login : System.Web.UI.Page
    {

        //ConexionBD cn = new ConexionBD();
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TutoriaConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            //pdf_Click(sender, e);
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("entra");
            try
            { 
                if (in_user.Value == "adminsu" && in_pass.Value == "041112!")
                {
                    Session["id"] = in_user.Value;                  
                    Response.Redirect("Home.aspx", false);
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Maestro Where RFC='" + in_user.Value + "' and Clave is null;", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (string.IsNullOrEmpty(in_pass.Value))
                    {
                        if (rd.Read())
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
                        }
                        else
                        {
                            rd.Close();
                            cmd = new SqlCommand("SELECT * FROM Administrador Where Usuario='" + in_user.Value + "' and Clave is null;", con);
                            rd = cmd.ExecuteReader();
                            if (rd.Read())
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
                            }
                            else
                            {
                                rd.Close();
                                cmd = new SqlCommand("SELECT * FROM Alumno Where No_control=" + in_user.Value + " and Clave is null;", con);
                                rd = cmd.ExecuteReader();
                                if (rd.Read())
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Usuario y contraseña incorrectos. Intente de nuevo')", true);
                                }
                            }
                        }
                        rd.Close();
                    }
                    else
                    {
                        rd.Close();
                        cmd = new SqlCommand("SELECT ID, Carrera FROM Maestro Where RFC='" + in_user.Value + "' and Clave='" + in_pass.Value + "';", con);
                        rd = cmd.ExecuteReader();
                        if (rd.Read())
                        {
                            Session["id"] = in_user.Value;
                            Session["id_m"] = rd[0].ToString();
                            Session["carrera"] = rd["Carrera"].ToString();
                            Response.Redirect("Home.aspx", false);
                        }
                        else
                        {
                            rd.Close();
                            cmd = new SqlCommand("SELECT Carrera FROM Administrador Where Usuario='" + in_user.Value + "' and Clave='" + in_pass.Value + "';", con);
                            rd = cmd.ExecuteReader();
                            if (rd.Read())
                            {
                                Session["id"] = in_user.Value;
                                Session["carrera"] = rd["Carrera"].ToString();
                                Response.Redirect("Home.aspx", false);
                            }
                            else
                            {
                                rd.Close();
                                cmd = new SqlCommand("SELECT Carrera FROM Alumno Where No_control='" + in_user.Value + "' and Clave='" + in_pass.Value + "';", con);
                                rd = cmd.ExecuteReader();
                                if (rd.Read())
                                {
                                    Session["id"] = in_user.Value;
                                    Session["carrera"] = rd["Carrera"].ToString();
                                    Response.Redirect("Home.aspx", false);
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Usuario y contraseña incorrectos. Intente de nuevo')", true);
                                }
                            }


                        }

                        rd.Close();
                    }

                    con.Close();
                }
            }
            catch (Exception ex ){
                MessageBox.Show(ex.Message);
            }
        }

        protected void Btn_addPass_Click(object sender, EventArgs e)
        {
            if(pass.Text == pass2.Text)
            {
                dsTutoriasTableAdapters.AdministradorTableAdapter taa = new dsTutoriasTableAdapters.AdministradorTableAdapter();
                dsTutorias.AdministradorDataTable dta = taa.GetDataByUser(in_user.Value);
                Metodos mt = new Metodos();
                if(dta.Rows.Count > 0)
                {
                    taa.UpdateClave(pass.Text, in_user.Value);
                }
                else
                {
                    dsTutoriasTableAdapters.MaestroTableAdapter ta = new dsTutoriasTableAdapters.MaestroTableAdapter();
                    dsTutorias.MaestroDataTable dt = ta.GetDataByRFC(in_user.Value);
                    if(dt.Rows.Count > 0)
                    {
                        ta.UpdateClave(pass.Text, in_user.Value);
                    }
                    else
                    {
                        if(mt.AlumnoPasswordUpdate(pass.Text, in_user.Value))
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Usuario y contraseña incorrectos. Intente de nuevo')", true);
                    }
                    
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Las Contraseñas no coinciden, intente de nuevo');", true);
            }
            pass.Text = "";
            pass2.Text = "";
        }

        protected void pdf_Click(object sender, EventArgs e)
        {
            //Metodos mt = new Metodos();
            //DataTable dt;
            //dt = mt.Reporte1GetInfo(17150409);
            //if (dt.Rows.Count > 0)
            //{
            //    // Create a new PDF document
            //    PdfDocument document = new PdfDocument();

            //    // Create an empty page
            //    PdfPage page = document.AddPage();

            //    // Get an XGraphics object for drawing
            //    XGraphics gfx = XGraphics.FromPdfPage(page);
            //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"img\Logo.png");
            //    XImage image = XImage.FromFile(path);

            //    // Create a font
            //    XFont txt = new XFont("Times New Roman", 10, XFontStyle.Regular);
            //    XFont title = new XFont("Times New Roman", 13, XFontStyle.Bold);
            //    XFont titleB = new XFont("Times New Roman", 15, XFontStyle.Bold);
            //    XFont titleC = new XFont("Times New Roman", 13, XFontStyle.BoldItalic);
            //    XFont subtitle = new XFont("Times New Roman", 13, XFontStyle.Italic);

            //    XStringFormat format = new XStringFormat();

            //    gfx.DrawImage(image, 460, 35, 105, 70);

            //    // Draw the text
            //    gfx.DrawString("ANEXO 4a", subtitle, XBrushes.Black, new XRect(1, 60, page.Width, page.Height), XStringFormats.TopCenter);
            //    gfx.DrawString("INSTITUTO TECNOLÓGICO DE AGUSCALIENTES", title, XBrushes.Black, new XRect(1, 75, page.Width, page.Height), XStringFormats.TopCenter);
            //    gfx.DrawString("DEPARTAMENTO DE DESARROLLO ACADEMICO", title, XBrushes.Black, new XRect(1, 90, page.Width, page.Height), XStringFormats.TopCenter);
            //    gfx.DrawString("CANALIZACION DE TUTORADOS", titleC, XBrushes.Black, new XRect(1, 130, page.Width, page.Height), XStringFormats.TopCenter);


            //    gfx.DrawString("Nombre del Tutor:", title, XBrushes.Black, new XRect(50, 175, page.Width, page.Height), format);
            //    gfx.DrawLine(XPens.Black, 155, 190, 540, 190);
            //    gfx.DrawString(dt.Rows[0]["NombreMaestro"].ToString(), txt, XBrushes.Black, new XRect(165, 175, page.Width, page.Height), format);
            //    gfx.DrawString("Nombre del Alumno:", title, XBrushes.Black, new XRect(50, 240, page.Width, page.Height), format);
            //    gfx.DrawLine(XPens.Black, 170, 255, 540, 255);
            //    gfx.DrawString(dt.Rows[0]["NombreAlumno"].ToString(), txt, XBrushes.Black, new XRect(180, 240, page.Width, page.Height), format);
            //    gfx.DrawString("Carrera:", title, XBrushes.Black, new XRect(50, 207, page.Width, page.Height), format);
            //    gfx.DrawLine(XPens.Black, 100, 222, 360, 222);
            //    gfx.DrawString(dt.Rows[0]["Carrera"].ToString(), txt, XBrushes.Black, new XRect(110, 207, page.Width, page.Height), format);
            //    gfx.DrawString("Semestre Actual:", title, XBrushes.Black, new XRect(380, 207, page.Width, page.Height), format);
            //    gfx.DrawLine(XPens.Black, 480, 222, 540, 222);
            //    gfx.DrawString(dt.Rows[0]["Semestre"].ToString(), txt, XBrushes.Black, new XRect(490, 207, page.Width, page.Height), format);

            //    gfx.DrawRectangle(XPens.Gray, XBrushes.LightGray, 110, 280, 380, 45);
            //    gfx.DrawString("Canalizacion de tutorados", titleB, XBrushes.Black, new XRect(1, 280, page.Width, page.Height), XStringFormats.TopCenter);

            //    gfx.DrawLine(XPens.Black, 50, 345, 540, 345);//horizontal1
            //    gfx.DrawLine(XPens.Black, 50, 385, 540, 385);//horizontal2
            //    gfx.DrawLine(XPens.Black, 50, 415, 540, 415);//horizontal3
            //    gfx.DrawLine(XPens.Black, 50, 445, 540, 445);//horizontal4
            //    gfx.DrawLine(XPens.Black, 50, 475, 540, 475);//horizontal5
            //    gfx.DrawLine(XPens.Black, 50, 505, 540, 505);//horizontal6
            //    gfx.DrawLine(XPens.Black, 50, 535, 540, 535);//horizontal7
            //    gfx.DrawLine(XPens.Black, 50, 585, 540, 585);//horizontal8
            //    gfx.DrawLine(XPens.Black, 50, 345, 50, 585);//vertical1 
            //    gfx.DrawLine(XPens.Black, 215, 345, 215, 585);//vertical2
            //    gfx.DrawLine(XPens.Black, 380, 345, 380, 585);//vertical3 
            //    gfx.DrawLine(XPens.Black, 540, 345, 540, 585);//vertical4

            //    gfx.DrawString("Área de Canalización", title, XBrushes.Black, new XRect(75, 360, page.Width, page.Height), format);
            //    gfx.DrawString("Alumno Canalizados", title, XBrushes.Black, new XRect(225, 360, page.Width, page.Height), format);
            //    gfx.DrawString("Observaciones", title, XBrushes.Black, new XRect(400, 360, page.Width, page.Height), format);

            //    gfx.DrawString("Atención Psicológica", txt, XBrushes.Black, new XRect(60, 395, page.Width, page.Height), format);
            //    gfx.DrawString("Atención Médica", txt, XBrushes.Black, new XRect(60, 425, page.Width, page.Height), format);
            //    gfx.DrawString("Circulos de estudio", txt, XBrushes.Black, new XRect(60, 455, page.Width, page.Height), format);
            //    gfx.DrawString("Platicas o conferencias", txt, XBrushes.Black, new XRect(60, 485, page.Width, page.Height), format);
            //    gfx.DrawString("servicios de apoyo externo", txt, XBrushes.Black, new XRect(60, 515, page.Width, page.Height), format);
            //    gfx.DrawString("Otros (especifique)", txt, XBrushes.Black, new XRect(60, 555, page.Width, page.Height), format);


            //    // Save the document...
            //    string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"PDF\") /*+NoControl */ + "17150409.pdf";
            //    document.Save(filename);
            //    // ...and start a viewer.
            //    Process.Start(filename);
            //}
            Metodos.Reporte5(1004);
            
        }

    }
}