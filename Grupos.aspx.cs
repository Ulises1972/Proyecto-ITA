using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class Grupos : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.GrupoTableAdapter ta = new dsTutoriasTableAdapters.GrupoTableAdapter();
        dsTutorias.GrupoDataTable dt;

        dsTutoriasTableAdapters.Grupo1TableAdapter ta1 = new dsTutoriasTableAdapters.Grupo1TableAdapter();
        dsTutorias.Grupo1DataTable dt1;

        dsTutoriasTableAdapters.MaestroTableAdapter tta = new dsTutoriasTableAdapters.MaestroTableAdapter();
        dsTutorias.MaestroDataTable dtt;

        dsTutoriasTableAdapters.AlumnoTableAdapter taa = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dta;
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
                dtt = tta.GetData(Session["carrera"].ToString());

                gr_tutor.Items.Add("Seleccionar");
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    gr_tutor.Items.Add(dtt[i][2].ToString().Replace("  ", "") + " " + dtt[i][3].ToString().Replace("  ", "") + " " + dtt[i][4].ToString().Replace("  ", ""));
                    gr_tutor.Items[i + 1].Value = dtt[i][0].ToString();
                }

                dta = taa.GetDataByGrade(0, Session["carrera"].ToString());
                lb1.Text = "Usted tiene " + dta.Rows.Count + " alumnos para 1ER grado";
                dta = taa.GetDataByGrade(1, Session["carrera"].ToString());
                lb2.Text = "Usted tiene " + dta.Rows.Count + " alumnos para 2DO grado";
                dta = taa.GetDataByGrade(2, Session["carrera"].ToString());
                lb3.Text = "Usted tiene " + dta.Rows.Count + " alumnos para 3ER grado";
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('antes de continuar asegurese que esten eliminados los alumnos que por alguna razon desertaron de la carrera')", true);
                
                actualiza();
            }
        }

        protected void actualiza()
        {
            t = mt.GruposSelect(Session["id"].ToString().Substring(0,3));

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

        protected void Btn_addGroup_Click(object sender, EventArgs e)
        {
            if(gr_tutor.SelectedIndex == 0 || gr_tutoria.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Ambos campos son necesarios'); ", true);
                return;
            }
            if (mt.GrupoAddUpdate(Metodos.toInt(ID.Text),Metodos.toInt(gr_tutoria.SelectedValue),Metodos.toInt(gr_tutor.SelectedValue), Session["id"].ToString().Substring(0,3).ToUpper()))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Error al guardar la informacion, contacte a soporte'); ", true);
            }
            
            cleanModal();
            actualiza();
        }

        protected void cleanModal()
        {
            //gr_nombre.Text = "";
            gr_tutor.SelectedIndex = 0;
            gr_tutoria.SelectedIndex = 0;
            Btn_cancel.Visible = false;
            Btn_addGroup.Text = "Agregar";
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ta.UpdateStatus("ELIMINADO", Convert.ToInt32(GridView1.DataKeys[Convert.ToInt32(e.RowIndex)].Value.ToString()));
            actualiza();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "Editar")
            {
                ID.Text = e.CommandArgument.ToString();
                dt = ta.GetDataByID(Convert.ToInt32(ID.Text));
                //gr_tutoria.SelectedIndex = dt[0][1].ToString();
                gr_tutor.SelectedIndex = gr_tutor.Items.IndexOf(gr_tutor.Items.FindByValue(dt[0][2].ToString()));
                gr_tutoria.SelectedIndex = Convert.ToInt32(dt[0][1].ToString().Substring(9, 1));
                Btn_cancel.Visible = true;
                Btn_addGroup.Text = "Actualizar";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
            }
        }


    }
}