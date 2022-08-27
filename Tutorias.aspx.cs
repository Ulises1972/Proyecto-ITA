using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TutoriasWeb
{
    public partial class Tutorias : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.GrupoTableAdapter tag = new dsTutoriasTableAdapters.GrupoTableAdapter();
        dsTutorias.GrupoDataTable dtg;

        dsTutoriasTableAdapters.Alumno1TableAdapter ta = new dsTutoriasTableAdapters.Alumno1TableAdapter();
        dsTutorias.Alumno1DataTable dt;

        dsTutoriasTableAdapters.AlumnoTableAdapter taa = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dta;

        dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter tagc = new dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter();
        //dsTutorias.Grupo_CompuestoDataTable dtgc;
        Metodos mt = new Metodos();
        DataTable t = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            try
            {
                if (grupo.Items.Count < 1)
                {
                    int j = Convert.ToInt32(Session["id_m"].ToString());
                    dtg = tag.GetDataByTutor(j);
                    grupo.Items.Add("Selecciona grupo");
                    for (int i = 0; i < dtg.Rows.Count; i++)
                    {
                        grupo.Items.Add(dtg[i][1].ToString());
                        grupo.Items[i + 1].Value = dtg[i][0].ToString();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            

            if (!IsPostBack)
            {
                actualiza();
            }
            
        }

        protected void actualiza()
        {
            try
            {
                if (grupo.SelectedIndex != 0)
                {
                    dt = ta.GetDataByGroup(Convert.ToInt32(grupo.SelectedValue));
                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView1.UseAccessibleHeader = true;
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;                        
                        GridView1.Visible = true;
                        GridView1.HeaderRow.Cells[6].Text += dt.Rows[0].ItemArray[6].ToString().Substring(0, 10);
                        GridView1.HeaderRow.Cells[7].Text += dt.Rows[0].ItemArray[7].ToString().Substring(0, 10);
                        GridView1.HeaderRow.Cells[8].Text += dt.Rows[0].ItemArray[8].ToString().Substring(0, 10);
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
            catch(Exception e)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + e.Message + "');", true);                
            }
            
            

            
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Editar")
                {                    
                    t = mt.GrupoComGetDataByID(e.CommandArgument.ToString());
                    ddEntr1.SelectedValue = t.Rows[0].ItemArray[15].ToString();
                    ddEntr2.SelectedValue = t.Rows[0].ItemArray[16].ToString();
                    ddEntr3.SelectedValue = t.Rows[0].ItemArray[17].ToString();

                    control.Text = t.Rows[0].ItemArray[0].ToString();
                    id.Text = e.CommandArgument.ToString();
                    coments.Text = t.Rows[0].ItemArray[3].ToString();

                    if (Metodos.toBool(t.Rows[0].ItemArray[19].ToString()))
                        CirculoEstudio.Checked = true;
                    if (Metodos.toBool(t.Rows[0].ItemArray[20].ToString()))
                        A_Medica.Checked = true;
                    if (Metodos.toBool(t.Rows[0].ItemArray[21].ToString()))
                        Platicas.Checked = true;
                    if (Metodos.toBool(t.Rows[0].ItemArray[22].ToString()))
                        A_Psicologico.Checked = true;
                    if (Metodos.toBool(t.Rows[0].ItemArray[23].ToString()))
                        A_Externo.Checked = true;                    

                                                             

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "modal", "openModal();", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
                cleanModal();
            }
            
            
        }

        protected void Btn_actualizar_Click(object sender, EventArgs e)
        {                        
            try
            {
                if(mt.GrupoComUpdateEjecucion(ddEntr1.SelectedValue, ddEntr2.SelectedValue, ddEntr3.SelectedValue, CirculoEstudio.Checked, A_Medica.Checked, Platicas.Checked, A_Psicologico.Checked, A_Externo.Checked, coments.Text, id.Text))
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error, intenta de uevo');", true);

                
                actualiza();
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            finally
            {
                cleanModal();
            }            
        }

        protected void cleanModal()
        {
            ddEntr1.SelectedIndex = 0;
            ddEntr2.SelectedIndex = 0;
            ddEntr3.SelectedIndex = 0;
            CirculoEstudio.Checked = false;
            Platicas.Checked = false;
            A_Externo.Checked = false;
            A_Medica.Checked = false;
            A_Psicologico.Checked = false;
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
            actualiza();
        }

        protected void grupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }
    }
}