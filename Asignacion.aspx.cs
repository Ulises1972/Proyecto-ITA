using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class Asignacion : Page
    {
        dsTutoriasTableAdapters.GrupoTableAdapter tag = new dsTutoriasTableAdapters.GrupoTableAdapter();
        dsTutorias.GrupoDataTable dtg;

        dsTutoriasTableAdapters.AlumnoTableAdapter ta = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dt;

        dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter tagc = new dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            
        }

        protected void actualiza()
        {                                    
            if (g_grado.SelectedIndex != 0)
            {
                dt = ta.GetDataByGrade(Convert.ToInt32(g_grado.SelectedValue), Session["carrera"].ToString());
                GridView1.Visible = true;

                switch (Convert.ToInt32(g_grado.SelectedValue))
                {
                    case 0:
                        dtg = tag.GetDataByGrade1(Session["carrera"].ToString());
                        break;
                    case 1:
                        dtg = tag.GetDataByGrade2(Session["carrera"].ToString());
                        break;
                    case 2:
                        dtg = tag.GetDataByGrade3(Session["carrera"].ToString());
                        break;
                }

                g_grupo.Items.Clear();
                g_grupo.Items.Add("Selecciona grupo");
                for (int i = 0; i < dtg.Rows.Count; i++)
                {
                    g_grupo.Items.Add(dtg[i][1].ToString());
                    g_grupo.Items[i + 1].Value = dtg[i][0].ToString();
                }

                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView1.UseAccessibleHeader = true;
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;                      
                }
                else
                {
                    GridView1.Visible = false;
                }
            }
            else
            {
                GridView1.Visible = false;
            }
            
        }

        protected void Btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox chkRow = (GridView1.Rows[i].Cells[7].Controls[1].FindControl("checkBox") as CheckBox);
                    if (chkRow.Checked)
                    {
                        tagc.Insert1(Convert.ToInt32(g_grupo.SelectedValue.ToString()), Convert.ToInt32(GridView1.Rows[i].Cells[0].Text));
                        ta.UpdateStatus("En Curso", Convert.ToInt32(GridView1.Rows[i].Cells[0].Text));
                    }
                }
                g_grupo.SelectedIndex = 0;
                actualiza();
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            
        }
        
        protected void g_grado_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
            
        }
    }
}