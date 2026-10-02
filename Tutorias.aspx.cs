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

        dsTutoriasTableAdapters.AlumnoTableAdapter ta = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dt;

        dsTutoriasTableAdapters.AlumnoTableAdapter taa = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dta;

        dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter tagc = new dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter();
        //dsTutorias.Grupo_CompuestoDataTable dtgc;
        Metodos mt = new Metodos();
        DataTable t = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. Validar sesión
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                try
                {
                    // 2. Cargar grupos del tutor solo si no hay items
                    if (grupo.Items.Count == 0)
                    {
                        int idMaestro = Convert.ToInt32(Session["id_m"]);
                        DataTable dtGrupos = tag.GetDataByTutor(idMaestro);

                        // 3. Limpiar y agregar items al DropDownList
                        grupo.Items.Clear();
                        grupo.Items.Add("Selecciona grupo");

                        foreach (DataRow row in dtGrupos.Rows)
                        {
                            grupo.Items.Add(new ListItem(
                                row["Nombre"].ToString(),  // Texto visible
                                row["ID"].ToString()      // Valor oculto
                            ));
                        }
                    }

                    // 4. Actualizar otros controles
                    actualiza();
                }
                catch (Exception ex)
                {
                    // 5. Manejo de errores visible para el usuario
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        "alert", $"alert('Error: {ex.Message.Replace("'", "")}');", true);
                }
            }
        }

        protected void actualiza()
        {
            try
            {
                if (grupo.SelectedIndex != 0)
                {
                    // Obtener alumnos por ID de Grupo (a través del JOIN con No_Control)
                   

                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView1.UseAccessibleHeader = true;
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                        // Validación segura para Substring
                        if (dt.Rows[0][6] != null && dt.Rows[0][6].ToString().Length >= 10)
                            GridView1.HeaderRow.Cells[6].Text += dt.Rows[0][6].ToString().Substring(0, 10);

                        if (dt.Rows[0][7] != null && dt.Rows[0][7].ToString().Length >= 10)
                            GridView1.HeaderRow.Cells[7].Text += dt.Rows[0][7].ToString().Substring(0, 10);

                        if (dt.Rows[0][8] != null && dt.Rows[0][8].ToString().Length >= 10)
                            GridView1.HeaderRow.Cells[8].Text += dt.Rows[0][8].ToString().Substring(0, 10);
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
            catch (Exception e)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    $"alert('Error: {e.Message.Replace("'", "")}');", true);
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