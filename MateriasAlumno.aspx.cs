using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class MateriasAlumno : System.Web.UI.Page
    {
        Metodos mt = new Metodos();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                dt = mt.GrupoComGetMatByNo(Session["id"].ToString());

                if (dt.Rows.Count > 0)
                {

                    tablas.Visible = true;
                    gvMaterias.Visible = false;
                    btnAceptar.Enabled = false;
                    ddSemestre.Enabled = false;
                    gvSeleccionadas.DataSource = dt;
                    gvSeleccionadas.DataBind();
                }
                llenaMaterias();
            }
        }

        protected void ddSemestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaMaterias();
        }

        private void llenaMaterias()
        {            
            if(ddSemestre.SelectedValue != "")
            {
                tablas.Visible = true;                

                gvMaterias.DataSource = mt.MateriaGetDataByGrade(Metodos.toInt(ddSemestre.SelectedValue), Session["carrera"].ToString());
                gvMaterias.DataBind();
                gvMaterias.Visible = true;
                btnAceptar.Visible = true;
                btnAceptar.Enabled = true; 
            }
            else
            {
                btnAceptar.Visible = false;
                tablas.Visible = false;
            }

            if (mt.GrupoComConMat(Session["id"].ToString()))
            {
                dt = mt.AlumnoGetCalsBySem(Metodos.toInt(Session["id"].ToString()),0);
                gvSeleccionadas.DataSource = dt;
                gvSeleccionadas.DataBind();
                tablas.Visible = true;
                ddSemestre.Enabled = false;
                btnAceptar.Visible = true;
                btnAceptar.Enabled = false;
                gvMaterias.Visible = false;
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int[] ids = new int[6];
            for (int i = 0; i < gvSeleccionadas.Rows.Count; i++)
            {
                ids[i] = Metodos.toInt(gvSeleccionadas.Rows[i].Cells[0].Text);                
            }
            if(mt.GrupoComInsertMat(ids, Session["id"].ToString()))
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Algo Salio mal, intente de nuevo o contacte a soporte')", true);
            btnAceptar.Enabled = false;
            gvMaterias.Visible = false;
            ddSemestre.Enabled = false;
        }

        protected void gvSeleccionadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvMaterias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int[] ids = new int[(gvSeleccionadas.Rows.Count + 1)];
                if (gvSeleccionadas.Rows.Count >= 0 && gvSeleccionadas.Rows.Count <= 5)
                {                    
                    for (int i=0; i< gvSeleccionadas.Rows.Count; i++)
                    {
                        if (!ids.Contains(Metodos.toInt(gvSeleccionadas.Rows[i].Cells[0].Text)))
                        {
                            ids[i] = Metodos.toInt(gvSeleccionadas.Rows[i].Cells[0].Text);
                        }                                                                       
                    }
                    ids[(ids.Length-1)] = Metodos.toInt(e.CommandArgument.ToString());
                }
                else
                {
                    //mensaje
                    return;
                }

                gvSeleccionadas.DataSource = mt.MateriaGetDataByIds(ids);
                //dt = (DataTable) gvSeleccionadas;            
                //dt.Rows.Add(mt.MateriaGetDataById(e.CommandArgument.ToString()).Rows[0]);
                //gvSeleccionadas.DataSource = dt;
                gvSeleccionadas.DataBind();
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + ex.Message +"')", true);
            }            
        }

        protected void gvMaterias_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int r = e.NewSelectedIndex;
        }
    }
}