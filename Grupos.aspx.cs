using System;
using System.Collections.Generic;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            dtt = tta.GetData(Session["carrera"].ToString()) ;

            if(!(gr_tutor.Items.Count > 0))
            {
                gr_tutor.Items.Add("Seleccionar");
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    gr_tutor.Items.Add(dtt[i][2].ToString().Replace("  ","") + " " +dtt[i][3].ToString().Replace("  ", "") + " " + dtt[i][4].ToString().Replace("  ", ""));
                    gr_tutor.Items[i + 1].Value = dtt[i][0].ToString();
                }
            }

            dta = taa.GetDataByGrade(0, Session["carrera"].ToString());
            lb1.Text = "Usted tiene " + dta.Rows.Count + " alumnos para 1ER grado";
            dta = taa.GetDataByGrade(1, Session["carrera"].ToString());
            lb2.Text = "Usted tiene " + dta.Rows.Count + " alumnos para 2DO grado";
            dta = taa.GetDataByGrade(2, Session["carrera"].ToString());
            lb3.Text = "Usted tiene " + dta.Rows.Count + " alumnos para 3ER grado";


            if (!IsPostBack)
            {
                actualiza();
            }
            
        }

        protected void actualiza()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('antes de continuar asegurese que esten eliminados los alumnos que por alguna razon eliminaron la carrera')", true);
            dt = ta.GetData(Session["carrera"].ToString());

            if(dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
           
        }

        protected void Btn_addGroup_Click(object sender, EventArgs e)
        {
            int j = 0;
            switch (Convert.ToInt32(gr_tutoria.SelectedValue))
            {
                case 1:
                    dt = ta.GetDataByGrade1(Session["carrera"].ToString());
                    break;
                case 2:
                    dt = ta.GetDataByGrade2(Session["carrera"].ToString());
                    break;
                case 3:
                    dt = ta.GetDataByGrade3(Session["carrera"].ToString());
                    break;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt[i][1].ToString().Substring(9,1) == gr_tutoria.SelectedValue)
                {
                    j++;
                }
            }

            if (Btn_addGroup.Text == "Agregar")
            {
                ta.Insert1(Session["id"].ToString().Substring(0, 3).ToUpper() +  "2022A-"+ gr_tutoria.SelectedValue + Convert.ToChar(65 + j) , Convert.ToInt32(gr_tutor.SelectedValue), "ACTIVO");
            }
            else
            {
                ta.Update1(Session["id"].ToString().Substring(0, 3).ToUpper() + "2022A-" + gr_tutoria.SelectedValue + Convert.ToChar(65 + (j+1)), Convert.ToInt32(gr_tutor.SelectedValue), "ACTIVO", Convert.ToInt32(ID.Text));
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
            ta.UpdateStatus("ELIMINADO", Convert.ToInt32(GridView1.Rows[Convert.ToInt32(e.RowIndex)].Cells[0].Text));
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