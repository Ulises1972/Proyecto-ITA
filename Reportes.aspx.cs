using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class Reportes : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.AlumnoTableAdapter ta = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dt;
        dsTutoriasTableAdapters.Alumno1TableAdapter taa = new dsTutoriasTableAdapters.Alumno1TableAdapter();
        dsTutorias.Alumno1DataTable dta;
        DataTable t = new DataTable();
        Metodos mt = new Metodos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                
            }

        }
        protected void actualiza()
        {
            try
            {
                //if (reportes.SelectedIndex != 4)
                //{
                //    m_buscar.Visible = false;
                //    GridView2.Visible = false;

                //    dt = ta.GetDataByEstatus(reportes.SelectedValue);
                //    if (dt.Rows.Count > 0)
                //    {
                //        GridView1.Visible = true;
                //        GridView1.DataSource = dt;
                //        GridView1.DataBind();
                //        GridView1.UseAccessibleHeader = true;
                //        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                //    }
                //    else
                //    {
                //        GridView1.Visible = false;
                //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('No existe ningun registro')", true);
                //    }
                //}
                //else
                //{
                //    m_buscar.Visible = true;
                //}
                gvAlumnos.Visible = false;
                gvGrupos.Visible = false;
                divBuscar.Visible = false;
                if (reportes.SelectedIndex != 0)
                {
                    if(reportes.SelectedValue == "Grupo")
                    {
                        t = mt.ReportesByGroup(Metodos.toInt(Session["id_m"].ToString() == null ? "-1" : Session["id_m"].ToString()), txtFiltro.Text);
                        if (t.Rows.Count > 0)
                        {
                            divBuscar.Visible = true;
                            gvGrupos.Visible = true;
                            gvGrupos.DataSource = t;
                            gvGrupos.DataBind();
                        }
                        
                    }
                    else if (reportes.SelectedValue == "Alumno")
                    {
                        t = mt.ReportesByAlumnos(Session["carrera"].ToString(), txtFiltro.Text);
                        if(t.Rows.Count > 0)
                        {
                            divBuscar.Visible = true;
                            gvAlumnos.Visible = true;
                            gvAlumnos.DataSource = t;
                            gvAlumnos.DataBind();
                        }
                    }
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + ex.Message + "')", true);
            }            
        }

        protected void consultar_Click(object sender, EventArgs e)
        {
            
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(No_Control.Text))
                //{
                //    dt = ta.GetDataByNo(Convert.ToInt32(No_Control.Text));
                //    if (dt.Rows.Count > 0)
                //    {
                //        GridView1.Visible = true;
                //        GridView1.DataSource = dt;
                //        GridView1.DataBind();
                //        GridView1.UseAccessibleHeader = true;
                //        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                //        dta = taa.GetDataByNo(Convert.ToInt32(No_Control.Text));
                //        if (dta.Rows.Count > 0)
                //        {
                //            GridView2.Visible = true;
                //            GridView2.DataSource = dta;
                //            GridView2.DataBind();
                //            GridView2.UseAccessibleHeader = true;
                //            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                //        }
                //        else
                //        {
                //            GridView2.Visible = false;
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('no existe ningun registro de tutorias con dicho numero de control.')", true);
                //        }
                //    }
                //    else
                //    {
                //        GridView1.Visible = false;
                //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('No existe ningun registro')", true);
                //    }
                //}
                actualiza();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + ex.Message + "')", true);
            }
        }

        protected void reportes_SelectedIndexChanged(object sender, EventArgs e)
        {
                actualiza();
        }

        protected void gvGrupos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int grupo = Metodos.toInt(e.CommandArgument.ToString());
            Metodos.Reporte5(grupo);
        }

        protected void gvAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int noControl = Metodos.toInt(e.CommandArgument.ToString());
            if(e.CommandName == "r1")
            {
                Metodos.reporte3(noControl, 1);
            }
            else if(e.CommandName == "r2")
            {
                Metodos.reporte3(noControl, 2);
            }
            else if(e.CommandName == "r3")
            {
                Metodos.reporte3(noControl, 3);
            }
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            actualiza();
        }
    }
}