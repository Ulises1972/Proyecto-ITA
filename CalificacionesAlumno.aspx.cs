using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class CalificacionesAlumno : System.Web.UI.Page
    {
        Metodos mt = new Metodos();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenaGrid();
            }

        }

        protected void llenaGrid()
        {
            dt = mt.AlumnoGetCalsBySem(Metodos.toInt(Session["id"].ToString()), 0);
            if(dt.Rows.Count > 0)
            {
                gvCalificaciones.Visible = true;
                gvCalificaciones.DataSource = dt;
                gvCalificaciones.DataBind();
                Btn_Promedio.Visible = true;
            }
            else
            {
                gvCalificaciones.Visible = false;
                Btn_Promedio.Visible = false;
            }
            

        }

        protected void gvCalificaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                hdnID.Value = e.CommandArgument.ToString();
                dt = mt.AlumnoGetCalsByMat(Metodos.toInt(hdnID.Value));
                t1_1.Text = dt.Rows[0]["u1_1"].ToString();
                t1_2.Text = dt.Rows[0]["u1_2"].ToString();
                t2_1.Text = dt.Rows[0]["u2_1"].ToString();
                t2_2.Text = dt.Rows[0]["u2_2"].ToString();
                t3_1.Text = dt.Rows[0]["u3_1"].ToString();
                t3_2.Text = dt.Rows[0]["u3_2"].ToString();
                t4_1.Text = dt.Rows[0]["u4_1"].ToString();
                t4_2.Text = dt.Rows[0]["u4_2"].ToString();
                t5_1.Text = dt.Rows[0]["u5_1"].ToString();
                t5_2.Text = dt.Rows[0]["u5_2"].ToString();
                t6_1.Text = dt.Rows[0]["u6_1"].ToString();
                t6_2.Text = dt.Rows[0]["u6_2"].ToString();
                t7_1.Text = dt.Rows[0]["u7_1"].ToString();
                t7_2.Text = dt.Rows[0]["u7_2"].ToString();
                t8_1.Text = dt.Rows[0]["u8_1"].ToString();
                t8_2.Text = dt.Rows[0]["u8_2"].ToString();
                mdTitle.InnerText = dt.Rows[0]["Nombre"].ToString();
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
            }
        }

        protected void Btn_actualizar_Click(object sender, EventArgs e)
        {
            
            if(mt.AlumnoSaveCals(t1_1.Text, t1_2.Text, t2_1.Text, t2_2.Text, t3_1.Text, t3_2.Text, t4_1.Text, t4_2.Text, t5_1.Text, t5_2.Text, t6_1.Text, t6_2.Text, t7_1.Text, t7_2.Text, t8_1.Text, t8_2.Text, hdnID.Value))
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Error al guardar calificaciones, intente de nuevo o contacte a soporte.'); ", true);
            llenaGrid();
            cleanModal();
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void cleanModal()
        {
            t1_1.Text = "";
            t1_2.Text = "";
            t2_1.Text = "";
            t2_2.Text = "";
            t3_1.Text = "";
            t3_2.Text = "";
            t4_1.Text = "";
            t4_2.Text = "";
            t5_1.Text = "";
            t5_2.Text = "";
            t6_1.Text = "";
            t6_2.Text = "";
            t7_1.Text = "";
            t7_2.Text = "";
            t8_1.Text = "";
            t8_2.Text = "";
        }

        protected void Btn_Promedio_Click(object sender, EventArgs e)
        {
            if(mt.CalculaPromedios(Metodos.toInt(Session["id"].ToString())))
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Error al Calcular promedios, intente de nuevo o contacte a soporte.'); ", true);
            llenaGrid();
        }
    }
}