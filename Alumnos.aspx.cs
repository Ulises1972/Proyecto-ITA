using SpreadsheetLight;
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
    public partial class Home : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.AlumnoTableAdapter ta = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dt;
        Metodos mt = new Metodos();
        DataTable t = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                actualiza();
            }
        }

        protected void Btn_addAlumno_Click(object sender, EventArgs e)
        {
            if (FUalumnos.HasFile)
            {
                SLDocument sl = new SLDocument(FUalumnos.PostedFile.InputStream);
                SLWorksheetStatistics props = sl.GetWorksheetStatistics();

                int ultimaFila = props.EndRowIndex;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + sl.GetCellValueAsString("A1") + "'); ", true);

                for(int i = 2; i <= ultimaFila; i++)
                {
                    if (mt.AlumnoAdd(Metodos.toInt(sl.GetCellValueAsString("A"+i).Trim(' ')), sl.GetCellValueAsString("B"+i).ToUpper(), sl.GetCellValueAsString("C"+i).Trim(' ').ToUpper(), sl.GetCellValueAsString("D"+i).Trim(' ').ToUpper(), sl.GetCellValueAsString("E" + i).Trim(' ').ToUpper(), Metodos.toInt(sl.GetCellValueAsString("F" + i).Trim(' ').ToUpper() == "" ? "1" : sl.GetCellValueAsString("F" + i).Trim(' ').ToUpper()), Metodos.toInt(sl.GetCellValueAsString("G" + i).Trim(' ').ToUpper() == "" ? "0" : sl.GetCellValueAsString("G" + i).Trim(' ').ToUpper())))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Error al ingresar el alumno, asegurese de ingresar la informacion correcta y que no se repita el No de control'); ", true);
                        return;
                    }
                }
                cleanModal();
                actualiza();
                return;
            }

            if(al_id.Text.Replace(" ","") == "" || al_nombre.Text.Trim(' ') == "" || ( al_aPaterno.Text.Trim(' ') == "" && al_aMaterno.Text.Trim(' ') == "" ) )
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Solo un campo de apellido puede ir vacio'); ", true);
                cleanModal();
                return;
            }
            if (Btn_addAlumno.Text=="Agregar")
            {
                if (mt.AlumnoAdd(Metodos.toInt(al_id.Text.Trim(' ')), al_nombre.Text.ToUpper(), al_aPaterno.Text.Trim(' ').ToUpper(), al_aMaterno.Text.Trim(' ').ToUpper(), Session["carrera"].ToString()))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Error al ingresar el alumno, asegurese de ingresar la informacion correcta y que no se repita el No de control'); ", true);
                }
            }
            else
            {
                if (mt.AlumnoUpdate(Metodos.toInt(al_id.Text.Trim(' ')), al_nombre.Text.ToUpper(), al_aPaterno.Text.Trim(' ').ToUpper(), al_aMaterno.Text.Trim(' ').ToUpper()))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Error al actualizar la informacion, contacte a soporte'); ", true);
                }
            }
            
            cleanModal();
            actualiza();
        }

        protected void actualiza()
        {
            t = mt.AlumnosGetByCarrera(Session["carrera"].ToString(), Tb_buscar.Text.Trim(' '));

            if(t.Rows.Count > 0)
            {
                GridView1.DataSource = t;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.Visible = false;
            }
            
        }
        protected void cleanModal()
        {
            al_id.Text = "";
            al_nombre.Text = "";
            al_aPaterno.Text = "";
            al_aMaterno.Text = "";
            Btn_addAlumno.Text = "Agregar";
            al_id.Enabled = true;
            Btn_cancel.Visible = false;
            Tb_buscar.Text = "";
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ta.UpdateStatus("ELIMINADO", Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[0].Text));
            actualiza();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "Editar")
            {
                dt = ta.GetDataByNo(Convert.ToInt32(e.CommandArgument.ToString()));
                al_id.Text = e.CommandArgument.ToString();
                al_id.Enabled = false;
                al_nombre.Text = dt[0][1].ToString();
                al_aPaterno.Text = dt[0][2].ToString();
                al_aMaterno.Text = dt[0][3].ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
                Btn_addAlumno.Text = "Actualizar";
                Btn_cancel.Visible = true;
            }
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            actualiza();
        }
    }
}