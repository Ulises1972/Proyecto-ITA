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
        Metodos mt = new Metodos();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            pdf_Click(sender, e);
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            dt = mt.login(in_user.Value.Replace(" ", ""), in_pass.Value.Replace(" ", ""));
            switch (dt.Rows[0]["result"])
            {
                case ("adminsu"):
                    Session["id"] = in_user.Value;
                    Session["carrera"] = "";
                    Response.Redirect("Home.aspx", false);
                    break;

                case ("modal"):
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
                    break;

                case ("alumno"):
                    Session["id"] = in_user.Value;
                    Session["carrera"] = dt.Rows[0]["carrera"];
                    Response.Redirect("Home.aspx", false);
                    break;

                case ("tutor"):
                    Session["id"] = in_user.Value;
                    Session["carrera"] = dt.Rows[0]["carrera"];
                    Session["id_m"] = dt.Rows[0]["id_m"];
                    Response.Redirect("Home.aspx", false);
                    break;

                case ("admon"):
                    Session["id"] = in_user.Value;
                    Session["carrera"] = dt.Rows[0]["carrera"];
                    Response.Redirect("Home.aspx", false);
                    break;

                case ("error"):
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Usuario y contraseña incorrectos. Intente de nuevo')", true);
                    break;
            }
        }

        protected void Btn_addPass_Click(object sender, EventArgs e)
        {
            if(mt.updatePassword(in_user.Value.Replace(" ",""), pass.Text, pass2.Text))
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
            //Metodos.Reporte5(3);
            
        }

    }
}