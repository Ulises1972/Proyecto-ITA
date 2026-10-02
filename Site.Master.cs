using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TutoriasWeb
{
    public partial class Site : System.Web.UI.MasterPage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Load')", true);
            System.Diagnostics.Debug.WriteLine("Page_Load ejecutado.");
            if (Session["id"] == null)
            {
               
            }
            else
            {
                if (!IsPostBack)
                {
                    System.Diagnostics.Debug.WriteLine("Current URL: " + Request.Url.AbsolutePath);

                    // Verifica si la página actual es MateriasAlumno.aspx
                    if (Request.Url.AbsolutePath.Contains("MateriasAlumno.aspx"))
                    {
                        // Oculta el menú o las barras de navegación usando CSS
                        //menu.Attributes.Add("class", "hidden"); // Agrega la clase CSS para ocultar el menú
                        //btn_logout.Attributes.Add("class", "hidden"); // Agrega la clase CSS para ocultar el botón de logout
                    }
                    else
                    {
                        dsTutoriasTableAdapters.CarreraTableAdapter tac = new dsTutoriasTableAdapters.CarreraTableAdapter();
                        //dsTutorias.CarreraDataTable dtc = tac.GetDataByNombre(Session["carrera"].ToString());

                        //if (dtc.Rows.Count > 0)
                        //{
                        //    img_carrera.ImageUrl = dtc[0][2].ToString();
                        //    img_carrera.Visible = true;
                        //}


                        dsTutoriasTableAdapters.InstitutoTableAdapter ta = new dsTutoriasTableAdapters.InstitutoTableAdapter();
                        //dsTutorias.InstitutoDataTable dt = ta.GetData();

                        //if (dt.Rows.Count > 0)
                        //{
                        //    img_instituto.ImageUrl = dt[0][2].ToString();
                        //    img_instituto.Visible = true;
                        //}

                        if (Session["id"].ToString() == "adminsu")
                        {
                            su.Visible = true;
                        }
                        else if (Metodos.toInt(Session["ID"].ToString()) > 0)
                        {
                            alumno.Visible = true;
                        }
                        else
                        {
                            if (Session["ID"].ToString().Substring(3, 5).ToUpper() == "ADMIN")
                            {
                                admin.Visible = true;
                                reportes.Visible = true;
                            }
                            else
                            {
                                tutor.Visible = true;
                                reportes.Visible = true;
                            }
                        }
                        menu.Visible = true;
                        btn_logout.Visible = true;
                        // Asegúrate de que el menú y el botón de logout sean visibles en otras páginas
                        //menu.Attributes.Remove("class"); // Elimina la clase CSS para mostrar el menú
                        //btn_logout.Attributes.Remove("class"); // Elimina la clase CSS para mostrar el botón de logout

                    }
                }
            }

        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Remove("id");
            Session.Remove("carrera");
            Response.Redirect("Login.aspx");           
        }

        protected void al_add_Click(object sender, EventArgs e)
        {

        }

        protected void al_import_Click(object sender, EventArgs e)
        {

        }
    }
}