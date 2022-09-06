using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TutoriasWeb
{
    public partial class Administradores : System.Web.UI.Page
    {
        dsTutoriasTableAdapters.AdministradorTableAdapter taa = new dsTutoriasTableAdapters.AdministradorTableAdapter();
        dsTutorias.AdministradorDataTable dta;
        
        dsTutoriasTableAdapters.CarreraTableAdapter tac = new dsTutoriasTableAdapters.CarreraTableAdapter();
        dsTutorias.CarreraDataTable dtc;
        Metodos mt = new Metodos();
        DataTable dt = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
            
            if(Carrera.Items.Count < 1)
            {
                dtc = tac.GetData();
                Carrera.Items.Add("Seleccionar Carrera");
                for (int i = 0; i < dtc.Rows.Count; i++)
                {
                    Carrera.Items.Add(dtc[i][1].ToString());
                    Carrera.Items[i + 1].Value = dtc[i][4].ToString();
                }
            }
            

            if (!IsPostBack)
            {
                actualizar();
            }

        }
        protected void actualizar()
        {
            dta = taa.GetData();

            if(dta.Rows.Count > 0)
            {

                GridView1.DataSource = dta;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Btn_addAdmin_Click(object sender, EventArgs e)
        {
            if(Nombre.Text.Replace(" ", "") == "" || A_Paterno.Text.Replace(" ","") == "" || A_Materno.Text.Replace(" ","") == "" || Carrera.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Todos los campos son obligatorios'); ", true);
            }
            dt = mt.AdministradoresAdd(Carrera.SelectedValue.ToUpper() + "ADMIN", Nombre.Text.ToUpper(), A_Paterno.Text.ToUpper(), A_Materno.Text.ToUpper(), Carrera.SelectedItem.Text, Metodos.toInt(hdnID.Value));           
            if(dt != null && dt.Rows[0]["result"].ToString() == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + dt.Rows[0]["msg"] + "'); ", true);
            }
            cleanModal();
            actualizar();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            hdnID.Value = GridView1.DataKeys[e.RowIndex].Value.ToString();
            mt.AdministradorDelete(Metodos.toInt(hdnID.Value));
            actualizar();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Editar")
            {
                hdnID.Value = e.CommandArgument.ToString();
                dta = taa.GetDataByID(Convert.ToInt32(hdnID.Value));
                Nombre.Text = dta[0][2].ToString();
                A_Paterno.Text = dta[0][3].ToString();
                A_Materno.Text = dta[0][4].ToString();
                Carrera.SelectedIndex = Carrera.Items.IndexOf(Carrera.Items.FindByText(dta[0][6].ToString()));                
                Btn_addAdmin.Text = "Actualizar";
                Btn_cancel.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "openModal(); ", true);
            }
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            cleanModal();
        }

        protected void cleanModal()
        {
            Nombre.Text = "";
            A_Paterno.Text = "";
            A_Materno.Text = "";
            Carrera.SelectedIndex = 0;            
            Btn_addAdmin.Text = "Agregar";
            Btn_cancel.Visible = false;
        }
    }
}