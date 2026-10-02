<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Materias.aspx.cs" Inherits="TutoriasWeb.Materias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btn_m").click(function () {
                $("#mdl").modal();
            });
        });

        function openModal() {
            $(document).ready(function () {
                $("#mdl").modal();
            })
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!--<asp:label runat="server" id="lbl_name"  ></asp:label>-->

    <div class="col-lg-12 form-inline justify-content-center">

        <div>

            <button type="button" class="btn btn-primary" id="btn_m">Agregar</button>

            <div class="row col-12 justify-content-start" width="350px">

                <asp:GridView ID="GridView1" runat="server" OnRowDeleting="GridView1_RowDeleting"
                    OnRowCommand="GridView1_RowCommand"
                    AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover table-bordered table-m text-center  shadow rounded table-responsive"
                    DataKeyNames="ID"
                    HeaderStyle-BackColor="#003366"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-Height="40px"
                    ShowHeader="true">

                    <HeaderStyle CssClass="rounded-top" />

                    <Columns>

                        <asp:BoundField DataField="Nombre" HeaderText="Nombre de la Materia">
                            <HeaderStyle Width="250px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Nombre_Corto" HeaderText="Nombre Corto">
                            <HeaderStyle Width="200px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="MaestroNombre" HeaderText="Maestro">
                            <HeaderStyle Width="380px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Semestre" HeaderText="Semestre">
                            <HeaderStyle Width="100px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="position: absolute; right: -60px; margin-top: -10px;">
                                    <div>
                                        <asp:ImageButton
                                            Style="margin-right: 80px;"
                                            Width="17px"
                                            ID="btnEliminar"
                                            ataKeyNames="ID"
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
                                                ID="btnEditar"
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


    <!-- The Modal-->
          <div class="modal fade" id="mdl">
            <div class="modal-dialog">
              <div class="modal-content">
      
                <div class="modal-header">
                  <h4 class="modal-title">Agregar Materia</h4>
                  <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
        
                <div class="modal-body">
                  <div class="col-12">
                      <div class="form-group">
                          <div>
                              <asp:HiddenField ID="hfIDCarrera" runat="server" />

                              <asp:Label ID="lblNombreCarrera" runat="server" CssClass="h4 mb-3"></asp:Label>
                              <asp:Label Text="" ID="ID" Visible="false" runat="server" />
                              <label>Nombre</label>
                              <asp:TextBox ID="nombre" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                          </div>
                          <div>
                              <label>Nombre Corto</label>
                              <asp:TextBox ID="nombre_corto" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled"/>
                          </div>
                          <div>
                              <label>Seleccionar Maestro</label>
                              <asp:DropDownList ID="ddlMaestros" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                              <asp:ListItem Text="-- Selecciona un maestro --" Value="" />
                              </asp:DropDownList>
                          </div>
                          <div>
                              <label>Semestre</label>
                              <asp:DropDownList  ID="ddlSemestre" runat="server" CssClass="form-control"  >
                                  <asp:ListItem Text="Seleccionar" />
                                  <asp:ListItem Text="1" />
                                  <asp:ListItem Text="2" />
                                  <asp:ListItem Text="3" />
                                  <asp:ListItem Text="4" />
                                  <asp:ListItem Text="5" />
                                  <asp:ListItem Text="6" />
                                  <asp:ListItem Text="7" />
                                  <asp:ListItem Text="8" />
                                  <asp:ListItem Text="9" />
                                  <asp:ListItem Text="10" />
                                  <asp:ListItem Text="11" />
                                  <asp:ListItem Text="12" />
                                  <asp:ListItem Text="13" />
                              </asp:DropDownList>
                          </div>
                      </div>
                  </div>
                </div>
        
                <div class="modal-footer justify-content-center">
                  <asp:Button Text="Cancelar" runat="server" ID="Btn_cancel" CssClass="btn btn-primary" Width="200" OnClick="Btn_cancel_Click" Visible="false"/>
                    <asp:Button ID="Btn_addMateria" Text="Agregar" runat="server" OnClick="Btn_addMateria_Click" CssClass="btn btn-primary" Width="200"/>
                </div>        
              </div>
            </div>
          </div>

</asp:Content>