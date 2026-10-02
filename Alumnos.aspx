<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Alumnos.aspx.cs" Inherits="TutoriasWeb.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#btn_a").click(function () {
                $("#mdl_alumno").modal();
            });
        });

        function openModal() {
            $(document).ready(function () {
                $("#mdl_alumno").modal();
            })
        };


        function Confirmacion() {

            var seleccion = confirm("¿Seguro que quiere borrar el registro?");
            /*
            if (seleccion)
                alert("se acepto el mensaje");
            else
                alert("NO se acepto el mensaje");
                */

            //usado para que no haga postback el boton de asp.net cuando 
            //no se acepte el confirm
            return seleccion;

        }

    </script>
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div class="col-lg-12 form-inline justify-content-center">

        <div>

            <button type="button" class="btn btn-primary" id="btn_a">Agregar</button>

            <!-- <div class="row col-10 justify-content-center" style="margin-bottom:15px; margin-top:15px;">            
            <div class=" col-6 form-inline justify-content-around">
                <asp:TextBox runat="server" ID="Tb_buscar" CssClass="form-control" Width="400" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:Button runat="server" Text="Buscar" ID="Btn_buscar" OnClick="Btn_buscar_Click" CssClass="btn btn-primary" Width="100" />
            </div>                
        </div> -->
            <div class="row col-12 justify-content-start" width="350px">
<asp:GridView ID="GridView1" 
    DataKeyNames="No_Control" 
    OnRowCommand="GridView1_RowCommand"
    OnRowDeleting="GridView1_RowDeleting" 
    runat="server"
    AutoGenerateColumns="false"
    CssClass="table table-striped table-hover table-bordered table-m text-center shadow rounded table-responsive"
    HeaderStyle-BackColor="#003366"
    HeaderStyle-ForeColor="White"
    HeaderStyle-Font-Bold="true"
    HeaderStyle-Height="40px"
    ShowHeader="true">

    <HeaderStyle CssClass="rounded-top" />

  <Columns> 
    <asp:BoundField DataField="No_control" HeaderText="No. Control"> 
        <HeaderStyle Width="120px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" /> 
    </asp:BoundField> 

    <asp:BoundField DataField="A_Paterno" HeaderText="Apellido Paterno"> 
        <HeaderStyle Width="160px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" /> 
    </asp:BoundField> 

    <asp:BoundField DataField="A_Materno" HeaderText="Apellido Materno"> 
        <HeaderStyle Width="170px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" /> 
    </asp:BoundField> 

    <asp:BoundField DataField="Nombre" HeaderText="Nombre(s)"> 
        <HeaderStyle Width="200px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" /> 
    </asp:BoundField> 

    <asp:BoundField DataField="Semestre" HeaderText="Semestre"> 
        <HeaderStyle Width="100px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" /> 
    </asp:BoundField> 

    <asp:BoundField DataField="Tutoria" HeaderText="Tutoria"> 
        <HeaderStyle Width="180px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" /> 
    </asp:BoundField> 

    <asp:TemplateField> 
        <ItemTemplate> 
            <div style="position: absolute; right: -60px; margin-top: -10px;"> 
                <div> 
                    <asp:ImageButton 
                        Style="margin-right: 80px;" 
                        Width="17px" 
                        ID="btnEliminar" 
                        CommandName="Delete" 
                        runat="server" 
                        ImageUrl="https://cdn-icons-png.flaticon.com/512/1214/1214428.png" 
                        OnClientClick="return Confirmacion();" /> 
                </div> 

                <asp:TemplateField HeaderText="Editar"> 
                    <itemtemplate> 
                        <asp:ImageButton 
                            style="margin-right: 80px;" 
                            Width="17px" 
                            ID="Btn_edit" 
                            CommandName="Editar" 
                            CommandArgument='<%# Eval("No_Control") %>' 
                            runat="server" 
                            ImageUrl="https://cdn-icons-png.flaticon.com/512/1159/1159633.png" /> 
                    </itemtemplate> 
                </asp:TemplateField> 
            </div> 
        </ItemTemplate> 
    </asp:TemplateField> 

</Columns> 
</asp:GridView> 
</div> 
</div> 
</div>



        <style>
    .custom-header th {
        background-color: #003366 !important;
        color: white !important;
        border-top-left-radius: 12px;
        border-top-right-radius: 12px;
    }

    /* Opcional: mejora el borde redondeado visible */
    .table thead tr:first-child th:first-child {
        border-top-left-radius: 12px;
    }

    .table thead tr:first-child th:last-child {
        border-top-right-radius: 12px;
    }

    .rounded-start {
        border-top-left-radius: 12px;
        border-bottom-left-radius: 12px;
    }
   

    .rounded-end {
        border-top-right-radius: 12px;
        border-top-left-radius: 12px;
</style>

    <!-- The Modal Alumno-->
          <div class="modal fade" id="mdl_alumno">
            <div class="modal-dialog">
              <div class="modal-content">
      
                <div class="modal-header">
                  <h4 class="modal-title">Agregar Alumno</h4>
                  <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
        
                <div class="modal-body">
                    <div class="col-12">
                        <div class="form-group">
                            
                            <div style="margin-top:10px;">
                                <label>No de control</label>
                                    <asp:TextBox runat="server" ID="al_id" CssClass="form-control"   />
                            </div>

                            <div style="margin-top:10px;">
                                <label>Nombre</label>
                                <asp:TextBox runat="server" ID="al_nombre" CssClass="form-control text-uppercase" AutoCompleteType="Disabled"  CausesValidation="true" />
                            </div>

                            <div style="margin-top:10px;">
                                <label>Apellido Paterno</label>
                                <asp:TextBox runat="server" ID="al_aPaterno" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                            </div>

                            <div style="margin-top:10px;">
                                <label>Apellido Materno</label>
                                <asp:TextBox runat="server" ID="al_aMaterno" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                            </div>

                           
                          <div style="margin-top:10px;">
                                <label>Tutoria</label>
                                    <asp:DropDownList  ID="ddlTutoria" runat="server" CssClass="form-control" required="required">
                                        <asp:ListItem Value="0" Text="Seleccionar" Selected="True" />
                                            <asp:ListItem Value="NO REALIZADAS" Text="NO REALIZADAS" />
                                            <asp:ListItem Value="EN PROCESO" Text="EN PROCESO" />
                                            <asp:ListItem Value="LIBERADAS" Text="REALIZADAS" />
                                            </asp:DropDownList>
                            </div> 

                            <div style="margin-top:10px;">
                                <label>Semestre</label>
                                <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control" required="required">
                                     <asp:ListItem Value="0" Text="Seleccionar" Selected="True" />
                                        <asp:ListItem Text="1" />
                                        <asp:ListItem Text="2" />
                                        <asp:ListItem Text="3" />
                                        
                                     </asp:DropDownList>
                            </div>
            
                            <div style="margin-top:10px; display:none;">
                                <asp:TextBox runat="server" ID="al_tutoria" Text="0" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>

        
                <div class="modal-footer justify-content-center">
                    <asp:Button Text="Cancelar" runat="server" ID="Btn_cancel" CssClass="btn btn-primary" Width="200" OnClick="Btn_cancel_Click" Visible="false"/>
                  <asp:Button Text="Agregar" ID="Btn_addAlumno" runat="server" OnClick="Btn_addAlumno_Click" CssClass="btn btn-primary" Width="200"/>
                </div>
        
              </div>
            </div>
          </div>


    <!-- Modal para editar alumno -->
<div class="modal fade" id="mdl_editar_alumno" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Editar Alumno</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <asp:HiddenField ID="hf_NoControl" runat="server" />
        <asp:TextBox ID="txt_Nombre" runat="server" CssClass="form-control" placeholder="Nombre" />
        <asp:TextBox ID="txt_ApellidoP" runat="server" CssClass="form-control mt-2" placeholder="Apellido Paterno" />
        <asp:TextBox ID="txt_ApellidoM" runat="server" CssClass="form-control mt-2" placeholder="Apellido Materno" />
        <asp:TextBox ID="txt_Semestre" runat="server" CssClass="form-control mt-2" placeholder="Semestre" />
      </div>
      <div class="modal-footer">
        <asp:Button ID="btn_guardar_edicion" runat="server" CssClass="btn btn-success" Text="Guardar Cambios" OnClick="Btn_addAlumno_Click" />

      </div>
    </div>
  </div>
</div>


        


</asp:Content>
