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
        Metodos mt = new Metodos();
        DataTable t = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            
        }

        protected void actualiza()
        {
            g_grupo.Items.Clear();
            if (g_grado.SelectedIndex != 0)
            {
                t = mt.GruposSelect(Session["id"].ToString().Substring(0, 3), Metodos.toInt(g_grado.SelectedValue) + 1);                                

                if (t.Rows.Count > 0)
                {
                    g_grupo.Items.Add("Selecciona grupo");
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        g_grupo.Items.Add(t.Rows[i]["Nombre"].ToString());
                        g_grupo.Items[i + 1].Value = t.Rows[i]["ID"].ToString();
                    }
                }

                t = mt.AlumnosGetToAsign(Session["carrera"].ToString(), Metodos.toInt(g_grado.SelectedValue));
                if (t.Rows.Count > 0)
                {
                    GridView1.Visible = true;
                    GridView1.DataSource = t;
                    GridView1.DataBind();
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
                if(g_grupo.SelectedIndex > 0)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        CheckBox chkRow = (GridView1.Rows[i].Cells[5].FindControl("checkBox") as CheckBox);
                        if (chkRow.Checked)
                        {
                            mt.AsignarAlumnos(Convert.ToInt32(GridView1.Rows[i].Cells[0].Text), Convert.ToInt32(g_grupo.SelectedValue.ToString()));
                            //tagc.Insert1(Convert.ToInt32(g_grupo.SelectedValue.ToString()), Convert.ToInt32(GridView1.Rows[i].Cells[0].Text));
                            ta.UpdateStatus("En Curso", Convert.ToInt32(GridView1.Rows[i].Cells[0].Text));
                        }
                    }
                    g_grupo.SelectedIndex = 0;
                    actualiza();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Debe seleccionar un grupo');", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error al asignar, contacte a soporte');", true);
            }
            
        }
        
        protected void g_grado_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
            
        }
    }
}