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

    <asp:label runat="server" id="lbl_name"></asp:label>
    

    <div class="col-lg-12 form-inline">
        <button type="button" class="btn btn-primary" id="btn_a">Agregar</button>
        <div class="row col-10 justify-content-center" style="margin-bottom:15px; margin-top:15px;">
            <div class=" col-6 form-inline justify-content-around">
                <asp:TextBox runat="server" ID="Tb_buscar" CssClass="form-control" Width="400"></asp:TextBox>
                <asp:Button runat="server" Text="Buscar" ID="Btn_buscar" OnClick="Btn_buscar_Click" CssClass="btn btn-primary" Width="100" />
            </div>
                
        </div>

        <div class="row col-12 justify-content-start">
            <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover "  >
                <Columns>
                    <asp:BoundField DataField="No_control" HeaderText="No. Control" />
                    <asp:BoundField DataField="A_Paterno" HeaderText="Apellido Paterno" />
                    <asp:BoundField DataField="A_Materno" HeaderText="Apellido Materno" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre(s)" />
                    <asp:BoundField DataField="Semestre" HeaderText="Semestre" />
                    <asp:BoundField DataField="Estatus" HeaderText="Status" />
                    <asp:BoundField DataField="Tutoria" HeaderText="Tutoria" />                                        
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:ImageButton Width="30px" CommandArgument='<%# Eval("No_Control") %>' CommandName="Editar" runat="server" ImageUrl="data:image/jpeg;base64,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"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton runat="server" OnClientClick="return Confirmacion()" CommandArgument='<%# Eval("No_Control") %>' CommandName="Delete" Width="25px" ImageUrl="data:image/png;base64,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"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

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
                          <div>
                              <label>Importar lista</label>
                          </div>
                              <asp:FileUpload runat="server" />
                          <div style="margin-top:10px;">
                              <label>No de control</label>
                              <asp:TextBox runat="server" ID="al_id" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" TextMode="Number" />
                          </div>
                          <div style="margin-top:10px;">
                              <label>Nombre</label>
                              <asp:TextBox runat="server" ID="al_nombre" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                          </div>
                          <div style="margin-top:10px;">
                              <label>Apellido Paterno</label>
                              <asp:TextBox runat="server" ID="al_aPaterno" CssClass="form-control text-uppercase" AutoCompleteType="Disabled"  />
                          </div>
                          <div style="margin-top:10px;">
                              <label>Apellido Materno</label>
                              <asp:TextBox runat="server" ID="al_aMaterno" CssClass="form-control text-uppercase" AutoCompleteType="Disabled"  />
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


        


</asp:Content>
