using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class Seguimiento : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.AlumnoTableAdapter ta = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dt;
        dsTutoriasTableAdapters.GrupoTableAdapter tag = new dsTutoriasTableAdapters.GrupoTableAdapter();
        dsTutorias.GrupoDataTable dtg;
        Metodos mt = new Metodos();
        DataTable t = new DataTable();
        DataTable ms;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }            

            if (!IsPostBack)
            {
                try
                {
                    int j = Metodos.toInt(Session["id_m"].ToString());
                    dtg = tag.GetDataByTutor(j);
                    grupo.Items.Add("Selecciona grupo");
                    for (int i = 0; i < dtg.Rows.Count; i++)
                    {
                        grupo.Items.Add(dtg[i][1].ToString());
                        grupo.Items[i + 1].Value = dtg[i][0].ToString();
                    }
                }
                catch (Exception ex)
                {

                }
                actualiza();
            }

        }


        protected void actualiza()
        {
            try
            {
                if (grupo.SelectedIndex > 0)
                {
                    t = mt.GrupoCom_AlumnoGetDataByGroup(grupo.SelectedValue);
                    if (t.Rows.Count > 0)
                    {
                        GridView1.DataSource = t;
                        GridView1.DataBind();
                        //GridView1.HeaderRow.Cells[6].Text += Metodos.toDate(t.Rows[0]["Entrevista1"].ToString());
                        //GridView1.HeaderRow.Cells[7].Text += Metodos.toDate(t.Rows[0]["Entrevista2"].ToString());
                        //GridView1.HeaderRow.Cells[8].Text += Metodos.toDate(t.Rows[0]["Entrevista3"].ToString());

                        //ms = new DataTable();
                        //ms = mt.MateriaGetDataByGrade(grupo.SelectedItem.Text.ElementAt(9), Session["carrera"].ToString());

                        //if (ms.Rows.Count <= 5)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "modal", "alert('No estan registradas las 6 materias de la carrera, contacte al administrador.');", true);
                        //    return;
                        //}

                        //GridView1.HeaderRow.Cells[9].Text = ms.Rows[0].ItemArray[2].ToString();
                        //GridView1.HeaderRow.Cells[10].Text = ms.Rows[1].ItemArray[2].ToString();
                        //GridView1.HeaderRow.Cells[11].Text = ms.Rows[2].ItemArray[2].ToString();
                        //GridView1.HeaderRow.Cells[12].Text = ms.Rows[3].ItemArray[2].ToString();
                        //GridView1.HeaderRow.Cells[13].Text = ms.Rows[4].ItemArray[2].ToString();
                        //GridView1.HeaderRow.Cells[14].Text = ms.Rows[5].ItemArray[2].ToString();

                        GridView1.Visible = true;
                    }
                }
                else
                    GridView1.Visible = false;
            }
            catch (Exception e)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + e.Message + "');", true);
            }
        }

        protected void grupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                lbl1.Visible = false;
                lbl2.Visible = false;
                lbl3.Visible = false;
                lbl4.Visible = false;
                lbl5.Visible = false;
                lbl6.Visible = false;
                cal1.Visible = false;
                cal2.Visible = false;
                cal3.Visible = false;
                cal4.Visible = false;
                cal5.Visible = false;
                cal6.Visible = false;

                control.Text = e.CommandArgument.ToString();
                //dt = ta.GetDataByID(Convert.ToInt32(e.CommandArgument));
                t = mt.GrupoComGetCalsByNo(Metodos.toInt(control.Text));
                cal1.Text = t.Rows[0]["Cal1"].ToString();
                cal2.Text = t.Rows[0]["Cal2"].ToString();
                cal3.Text = t.Rows[0]["Cal3"].ToString();
                cal4.Text = t.Rows[0]["Cal4"].ToString();
                cal5.Text = t.Rows[0]["Cal5"].ToString();
                cal6.Text = t.Rows[0]["Cal6"].ToString();

                //ms = new DataTable();
                //ms = mt.MateriaGetDataByGrade(grupo.SelectedItem.Text.ElementAt(9), Session["carrera"].ToString());

                //if (ms.Rows.Count <= 5)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "modal", "alert('No estan registradas las 6 materias de la carrera, contacte al administrador.');", true);
                //    return;
                //}

                t = mt.AlumnoGetCalsBySem(Metodos.toInt(control.Text), 0);

                if(t.Rows.Count > 0)
                {
                    lbl1.InnerText = t.Rows[0]["Nombre_Corto"].ToString();
                    lbl1.Visible = true;
                    cal1.Visible = true;
                }
                if (t.Rows.Count > 1)
                {
                    lbl2.InnerText = t.Rows[1]["Nombre_Corto"].ToString();
                    lbl2.Visible = true;
                    cal2.Visible = true;
                }
                if (t.Rows.Count > 2)
                {
                    lbl3.InnerText = t.Rows[2]["Nombre_Corto"].ToString();
                    lbl3.Visible = true;
                    cal3.Visible = true;
                }
                if (t.Rows.Count > 3)
                {
                    lbl4.InnerText = t.Rows[3]["Nombre_Corto"].ToString();
                    lbl4.Visible = true;
                    cal4.Visible = true;
                }
                if (t.Rows.Count > 4)
                {
                    lbl5.InnerText = t.Rows[4]["Nombre_Corto"].ToString();
                    lbl5.Visible = true;
                    cal5.Visible = true;
                }
                if (t.Rows.Count > 5)
                {
                    lbl6.InnerText = t.Rows[5]["Nombre_Corto"].ToString();
                    lbl6.Visible = true;
                    cal6.Visible = true;
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "modal", "openModal();", true);
            }
        }

        protected void Btn_actualizar_Click(object sender, EventArgs e)
        {
            if(mt.GrupoComUpdateSeguimiento(cal1.Visible ? cal1.Text : "null", cal2.Visible ? cal2.Text : "null", cal3.Visible ? cal3.Text : "null", cal4.Visible ? cal4.Text : "null", cal5.Visible ? cal5.Text : "null", cal6.Visible ? cal6.Text : "null", Metodos.toInt(control.Text)))
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "modal", "alert('Error. Intenta de nuevo o contacta a soporte');", true);
            cleanModal();
            actualiza();
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {

        }

        protected void cleanModal()
        {
            cal1.Text = "";
            cal2.Text = "";
            cal3.Text = "";
            cal4.Text = "";
            cal5.Text = "";
            cal6.Text = "";            
        }
    }
}