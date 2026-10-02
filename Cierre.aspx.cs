using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class Cierre : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.AlumnoTableAdapter ta = new dsTutoriasTableAdapters.AlumnoTableAdapter();
        dsTutorias.AlumnoDataTable dt;
        dsTutoriasTableAdapters.GrupoTableAdapter tag = new dsTutoriasTableAdapters.GrupoTableAdapter();
        dsTutorias.GrupoDataTable dtg;
        dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter tagc = new dsTutoriasTableAdapters.Grupo_CompuestoTableAdapter();
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

        private void calculos()
        {            
            t = mt.GrupoCom_AlumnoGetDataByGroup(grupo.SelectedValue);
            if (t.Rows.Count > 0)
            {
                string A, B, D, N, I, R, prom="";
                int P, reprobadas, mat;
                for(int i = 0; i < t.Rows.Count; i++)
                {
                    D = "";
                    N = "";
                    I = "";
                    R = "";
                    P = 0;
                    mat = 0;
                    prom = "";
                    reprobadas = 0;
                    for(int j=0; j<6; j++)
                    {
                        if (t.Rows[i]["Cal" + (j + 1)].ToString() == "NA" || t.Rows[i]["Cal" + (j + 1)].ToString() == "-" || t.Rows[i]["Cal" + (j + 1)].ToString() == "/")
                        {
                            prom = t.Rows[i]["Cal" + (j + 1)].ToString() == "" ? "NA" : t.Rows[i]["Cal" + (j + 1)].ToString();
                            reprobadas++;
                        }
                        else if (!string.IsNullOrEmpty(t.Rows[i]["Cal" + (j + 1)].ToString()) && prom == "")
                        {
                            P += Metodos.toInt(t.Rows[i]["Cal" + (j + 1)].ToString());
                            mat++;
                        }
                    }
                    
                    A = t.Rows[i].ItemArray[15].ToString() == "A" && t.Rows[i].ItemArray[16].ToString() == "A" && t.Rows[i].ItemArray[17].ToString() == "A" ? "SI" : "NO";
                    B = reprobadas == 0 ? "SI" : "NO";
                    
                    P /= mat;

                    if (P >= 90)
                        D = "X";
                    else if (B == "SI")
                        N = "X";
                    else if (reprobadas < 3)
                        I = "X";
                    else
                        R = "X";
                    

                    if (mt.GrupoComUpdateCalculos(prom == "" ? P.ToString() : prom, A, B, D, N, I, R, t.Rows[i].ItemArray[0].ToString()))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "modal", "alert('Error al calcular datos. Contacte a Soporte');", true);
                        return;
                    }
                        
                }
            }
        }

        

        protected void actualiza()
        {
            try
            {
                if (grupo.SelectedIndex != 0)
                {
                    calculos();
                    t = mt.GrupoCom_AlumnoGetDataByGroup(grupo.SelectedValue);
                    if (t.Rows.Count > 0)
                    {
                        GridView1.DataSource = t;
                        GridView1.DataBind();
                        GridView1.UseAccessibleHeader = true;
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GridView1.HeaderRow.Cells[4].Text += Metodos.toDate(t.Rows[0]["Entrevista1"].ToString());
                        GridView1.HeaderRow.Cells[5].Text += Metodos.toDate(t.Rows[0]["Entrevista2"].ToString());
                        GridView1.HeaderRow.Cells[6].Text += Metodos.toDate(t.Rows[0]["Entrevista3"].ToString());

                        GridView1.Visible = true;
                        btnCerrar.Visible = true;
                    }
                    else
                    {
                        GridView1.Visible = false;
                        btnCerrar.Visible = false;
                    }
                        
                }
                else
                {
                    GridView1.Visible = false;
                    btnCerrar.Visible = false;
                }
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
                control.Text = GridView1.Rows[0].Cells[0].Text;
                id.Text = e.CommandArgument.ToString();                

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "modal", "openModal();", true);
            }
        }

        protected void Btn_actualizar_Click(object sender, EventArgs e)
        {

        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {

        }


        protected void Btn_aceptar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                int ID_Grupo = Convert.ToInt32(grupo.SelectedValue);
                DataTable t = mt.GetDataByGrupo(ID_Grupo);

                foreach (DataRow row in t.Rows)
                {
                    int tutoria = Convert.ToInt32(row["Tutoria"]);
                    int semestre = Convert.ToInt32(row["SemestreA"]);
                    string estatus, resultado;

                    if (row["A"].ToString() == "SI" && row["B"].ToString() == "SI")
                    {
                        estatus = tutoria < 2 ? "NO ASIGNADO" : "CONCLUIDO";
                        tutoria++;
                        resultado = "APROBADO";
                    }
                    else
                    {
                        estatus = "RESAGADO";
                        resultado = "REPROBADO";
                    }

                    // Actualizar datos
                    mt.GrupoComUpdateSemestre(semestre, resultado, row["ID"].ToString());
                    mt.AlumnoCierreUpdate(estatus, semestre + 1, tutoria, row["No_control"].ToString());
                }

                // Verificar si todos han concluido
                int concluidos = Convert.ToInt32(tagc.GetDataByConcluido(ID_Grupo));
                if (concluidos == 0) // Si ninguno está como CONCLUIDO
                {
                    bool actualizado = mt.GrupoUpdateEstatus("CONCLUIDO", ID_Grupo.ToString());
                    if (!actualizado)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(),
                            "alert", "alert('Error al actualizar el estatus del grupo');", true);
                    }
                    grupo.SelectedIndex = 0;
                }

                actualiza();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(),
                    "alert", $"alert('Error: {ex.Message}');", true);
            }
        }
    }
}