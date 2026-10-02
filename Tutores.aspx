<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  EnableEventValidation="false" CodeBehind="Tutores.aspx.cs" Inherits="TutoriasWeb.Maestros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btn_m").click(function () {
                $("#mdl_maestro").modal();
            });
        });

        function openModal() {
            $(document).ready(function () {
                $("#mdl_maestro").modal();
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

     <asp:label runat="server" id="lbl_name"  ></asp:label>


    <div class="col-lg-12 form-inline justify-content-center">

        <div cssclass="table-m"><!--AFECTA EL TAMAÑAO DELA TABLA-->

            <button type="button" class="btn btn-primary" id="btn_m">Agregar</button>

            <!--   <div class="row col-10 justify-content-center" style="margin-bottom:15px; margin-top:15px;">
            
            <div class=" col-6 form-inline justify-content-around">
                <asp:TextBox runat="server" ID="Tb_buscar" CssClass="form-control" Width="400"></asp:TextBox>
                <asp:Button runat="server" Text="Buscar" ID="Btn_buscar" OnClick="Btn_buscar_Click" CssClass="btn btn-primary" Width="100"/>
            </div>
        </div> -->


            <div class="row col-12 justify-content-start" width="350px">

                <asp:GridView ID="GridView1" OnRowDeleting="GridView1_RowDeleting"
                    OnRowCommand="GridView1_RowCommand" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover table-bordered table-m text-center  shadow rounded table-responsive   "
                    DataKeyNames="ID"
                    HeaderStyle-BackColor="#003366"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-Height="40px"
                    ShowHeader="true">

                    <HeaderStyle CssClass="rounded-top" />

                    <Columns>

                        <asp:BoundField DataField="RFC" HeaderText="RFC">
                            <HeaderStyle Width="120px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <HeaderStyle Width="200px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="A_Paterno" HeaderText="Apellido Paterno">
                            <HeaderStyle Width="200px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="A_Materno" HeaderText="Apellido Materno">
                            <HeaderStyle Width="200px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="position: absolute; right: -60px; margin-top: -10px;">
                                    <div>
                                        <asp:ImageButton
                                            Style="margin-right: 80px;"
                                            Width="17px"
                                            ID="Btn_delete"
                                            CommandName="Delete"
                                            CommandArgument='<%# Eval("ID") %>'
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
                                                CommandArgument='<%# Eval("ID") %>'
                                                runat="server"
                                                ImageUrl="https://cdn-icons-png.flaticon.com/512/1159/1159633.png" />
                                        </itemtemplate>
                                    </asp:TemplateField>
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

    

    <!-- The Modal Maestro-->
          <div class="modal fade" id="mdl_maestro">
            <div class="modal-dialog">
              <div class="modal-content">
      
                <div class="modal-header">
                  <h4 class="modal-title">Agregar</h4>
                  <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
        
                <div class="modal-body">
                  <div class="col-12">
                      <div class="form-group">
                          <div>
                              <asp:Label ID="ID" Text="" runat="server" Visible="false"/>
                              <label>RFC</label>
                              <asp:TextBox ID="tu_rfc" runat="server" CssClass="form-control text-uppercase" MaxLength="13" AutoCompleteType="Disabled" />
                          </div>
                          <div>
                              <label>Nombres</label>
                              <asp:TextBox ID="tu_nombre" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                          </div>
                          <div style="margin-top:10px;">
                              <label>Apellido Paterno</label>
                              <asp:TextBox ID="tu_aPaterno" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled"  />
                          </div>
                          <div style="margin-top:10px;">
                              <label>Apellido Materno</label>
                              <asp:TextBox ID="tu_aMaterno" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                          </div>
                      </div>
                  </div>
                </div>
        
                <div class="modal-footer justify-content-center">
                    <asp:Button Text="Cancelar" runat="server" ID="Btn_cancel" CssClass="btn btn-primary" Width="200" OnClick="Btn_cancel_Click" Visible="false"/>
                    <asp:Button ID="Btn_addTutor" Text="Agregar" runat="server" OnClick="Btn_addTutor_Click" CssClass="btn btn-primary" Width="200"/>
                </div>
        
              </div>
            </div>
          </div>

</asp:Content>
