using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace TutoriasWeb
{
    public partial class Planeacion : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.GrupoTableAdapter tag = new dsTutoriasTableAdapters.GrupoTableAdapter();
        dsTutorias.GrupoDataTable dtg;

        dsTutoriasTableAdapters.AlumnoTableAdapter ta = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dt;

        dsTutoriasTableAdapters.AlumnoTableAdapter taa = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dta;

        dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter tagc = new dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter();
        //dsTutorias.Grupo_CompuestoDataTable dtgc;
        SqlConnection cnn = new SqlConnection("server=DESKTOP-1I96JTK\\SQLEXPRESS ; database=Tutoria ; integrated security = true");
        SqlCommand cmd = new SqlCommand();
        
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
                catch (Exception ex)
                {

                }
            }

        }


        protected void actualiza()
        {
            try
            {
                if (grupo.SelectedIndex != 0)
                {
                    
                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView1.UseAccessibleHeader = true;
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GridView1.Visible = true;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + e.Message + "');", true);
            }
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Editar")
                {
                                    
                    entr1.Text = string.IsNullOrEmpty(GridView1.Rows[0].Cells[6].Text.Replace("&nbsp;", "")) ? "" : GridView1.Rows[0].Cells[6].Text.Substring(6, 4) + "-" + GridView1.Rows[0].Cells[6].Text.Substring(3, 2) + "-" + GridView1.Rows[0].Cells[6].Text.Substring(0, 2);
                    entr2.Text = string.IsNullOrEmpty(GridView1.Rows[0].Cells[7].Text.Replace("&nbsp;", "")) ? "" : GridView1.Rows[0].Cells[7].Text.Substring(6, 4) + "-" + GridView1.Rows[0].Cells[7].Text.Substring(3, 2) + "-" + GridView1.Rows[0].Cells[7].Text.Substring(0, 2);
                    entr3.Text = string.IsNullOrEmpty(GridView1.Rows[0].Cells[8].Text.Replace("&nbsp;", "")) ? "" : GridView1.Rows[0].Cells[8].Text.Substring(6, 4) + "-" + GridView1.Rows[0].Cells[8].Text.Substring(3, 2) + "-" + GridView1.Rows[0].Cells[8].Text.Substring(0, 2);
                    control.Text = GridView1.Rows[0].Cells[0].Text;
                    id.Text = e.CommandArgument.ToString();

                    //string r = entr1.Text.Substring(8, 2) + "/" + entr1.Text.Substring(5, 2) + "/" + entr1.Text.Substring(0, 4);
                    //string f = DateTime.Now.ToString().Substring(0,10);
                    //Btn_asistencia.Visible = r == f;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "modal", "openModal();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
                cleanModal();
            }


        }

        protected void Btn_actualizar_Click(object sender, EventArgs e)
        {
            string e1, e2, e3;
            SqlTransaction tr;
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            try
            {
                cmd.Transaction = tr;

                if (GridView1.Rows.Count > 0)
                {
                    GridView1.Rows[0].Cells[6].BackColor = Color.Red;
                }
                e1 = entr1.Text == "" ? "NULL" : "'" + entr1.Text + "'";
                e2 = entr2.Text == "" ? "NULL" : "'" + entr2.Text + "'";
                e3 = entr3.Text == "" ? "NULL" : "'" + entr3.Text + "'";
                cmd.CommandText = "UPDATE Grupo_Compuesto set Entrevista1=" + e1 + ", Entrevista2=" + e2 +
                                        ", Entrevista3=" + e3 + " where ID_Grupo=" + grupo.SelectedValue ;
                //cmd.Connection = cnn;
                //cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                tr.Commit();
                cmd.Connection.Close();                

                if (Convert.ToInt32(tagc.GetDataByConcluido(Convert.ToInt32(grupo.SelectedValue))) < 1)
                {
                    tag.UpdateStatus("Concluido", Convert.ToInt32(grupo.SelectedValue));
                    grupo.SelectedIndex = 0;
                }
                actualiza();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
                try
                {
                    tr.Rollback();
                }
                catch (Exception ex2)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex2.Message + "');", true);
                }
            }
            finally
            {
                cmd.Connection.Close();
                cleanModal();
            }
        }

        protected void cleanModal()
        {
            entr1.Text = "";
            entr2.Text = "";
            entr3.Text = "";
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

        protected void Btn_asistencia_Click(object sender, EventArgs e)
        {

        }
    }
}