using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class Materias : System.Web.UI.Page
    {
        DataTable dt= new DataTable();
        Metodos mt = new Metodos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                actualizar();
            }
        }

        protected void Btn_addMateria_Click(object sender, EventArgs e)
        {
            if (Btn_addMateria.Text == "Agregar")
            {
                if(mt.MateriaInsert(nombre.Text, nombre_corto.Text, semestre.SelectedValue, Session["carrera"].ToString()))
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Algo salio mal, intentalo de nuevo o contacta a soporte'); ", true);
            }
            else
            {
                mt.MateriaUpdate(nombre.Text, nombre_corto.Text, semestre.SelectedValue, ID.Text);
            }
            cleanModal();

            actualizar();
        }
        protected void actualizar()
        {
            dt = mt.MateriaSelect(Session["carrera"].ToString());

            if (dt.Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.Visible = false;
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if(mt.MateriaDelete(GridView1.DataKeys[e.RowIndex].Value.ToString()))
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Algo salio mal, intentalo de nuevo o contacta a soporte'); ", true);
            actualizar();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                try
                {
                    dt = mt.MateriaGetDataById(e.CommandArgument.ToString());
                    nombre.Text = dt.Rows[0].ItemArray[1].ToString();
                    nombre_corto.Text = dt.Rows[0].ItemArray[2].ToString();
                    semestre.Text = dt.Rows[0].ItemArray[3].ToString();
                    Btn_cancel.Visible = true;
                    Btn_addMateria.Text = "Actualizar";
                    ID.Text = e.CommandArgument.ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
                }
                catch
                {

                }                
            }
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void cleanModal()
        {
            nombre.Text = "";
            nombre_corto.Text = "";
            semestre.Text = "";
            semestre.SelectedIndex = 0;
            Btn_addMateria.Text = "Agregar";
            Btn_cancel.Visible = false;
        }
    }
}