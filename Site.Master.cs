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

            if(Session["id"] == null)
            {

            }
            else
            {
                if (!IsPostBack)
                {


                    dsTutoriasTableAdapters.InstitutoTableAdapter ta = new dsTutoriasTableAdapters.InstitutoTableAdapter();
                    dsTutorias.InstitutoDataTable dt = ta.GetData();

                    if (dt.Rows.Count > 0)
                    {
                        img_instituto.ImageUrl = dt[0][2].ToString();
                        img_instituto.Visible = true;
                    }

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
                        dsTutoriasTableAdapters.CarreraTableAdapter tac = new dsTutoriasTableAdapters.CarreraTableAdapter();
                        dsTutorias.CarreraDataTable dtc = tac.GetDataByNombre(Session["carrera"].ToString());

                        if (dtc.Rows.Count > 0)
                        {
                            img_carrera.ImageUrl = dtc[0][2].ToString();
                        }
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
                        img_carrera.Visible = true;
                    }
                    menu.Visible = true;
                    btn_logout.Visible = true;
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