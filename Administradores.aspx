<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administradores.aspx.cs" Inherits="TutoriasWeb.Administradores" %>
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

    <div class="col-lg-12 form-inline justify-content-center">

        <div style="width: 150%; max-width: 500px;">

            <button type="button" class="btn btn-primary" id="btn_m">Agregar</button>

            <div class="row col-12 justify-content-start" width="350px">

                <asp:GridView ID="GridView1" OnRowDeleting="GridView1_RowDeleting"
                    OnRowCommand="GridView1_RowCommand"
                    runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover table-bordered table-m text-center shadow rounded table-responsive "
                    DataKeyNames="ID"
                    HeaderStyle-BackColor="#003366"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-Height="40px">

                    <HeaderStyle CssClass="rounded-top" />

                    <Columns>

                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <HeaderStyle Width="120px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="A_Paterno" HeaderText="Apellido Paterno">
                            <HeaderStyle Width="120px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="A_Materno" HeaderText="Apellido Materno">
                            <HeaderStyle Width="120px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Carrera" HeaderText="Carrera">
                            <HeaderStyle Width="120px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="position: absolute; left: 460px; margin-top: -10px;">

                                    <div>
                                        <asp:ImageButton
                                            Style="margin-right: 80px;"
                                            Width="17px"
                                            CommandName="Delete"
                                            ID="Btn_delete"
                                            CommandArgument='<%# Eval("ID") %>'
                                            runat="server"
                                            ImageUrl="https://cdn-icons-png.flaticon.com/512/1214/1214428.png"
                                            OnClientClick="return confirm('¿Eliminar este instituto?');" />
                                    </div>

                                    <div>
                                     <asp:TemplateField HeaderText="Editar">
                                        <itemtemplate>
                                            <asp:ImageButton
                                                style="margin-right: 80px;"
                                                Width="17px"
                                                CommandName="Editar"
                                                CommandArgument='<%# Eval("ID") %>'
                                                runat="server"
                                                ImageUrl="https://cdn-icons-png.flaticon.com/512/1159/1159633.png" />
                                        </itemtemplate>
                                    </asp:TemplateField
                                 </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            </div>

        </div>
        <asp:HiddenField runat="server" ID="HiddenField1" Value="" />

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

    <!--Moodel Adminstradores-->
      <div class="modal fade" id="mdl">
        <div class="modal-dialog">
          <div class="modal-content">
  
            <div class="modal-header">
              <h4 class="modal-title">Agregar Tutor</h4>
              <button type="button" class="close" data-dismiss="modal">×</button>
            </div>
    
            <div class="modal-body">
              <div class="col-12">
                  <div class="form-group">
                                                                        
                      <div>
                          <label>Nombre</label>
                          <asp:TextBox ID="Nombre" runat="server" CssClass="form-control text-uppercase"  AutoCompleteType="Disabled" />
                      </div>
                      <div>
                          <label>Apellido Paterno</label>
                          <asp:TextBox ID="A_Paterno" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled"/>
                      </div>
                      <div>
                          <label>Apellido Materno</label>
                          <asp:TextBox ID="A_Materno" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                      </div>

                       

                      <div>
                          <label>Seleccionar una Carrera</label>
                          <asp:DropDownList ID="Carrera" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                              <asp:ListItem Text="-- Seleccione una Carrera --" Value="0" />
                          </asp:DropDownList>
                      </div>
                      
                  </div>
              </div>
            </div>
    
            <div class="modal-footer justify-content-center">
              <asp:Button Text="Cancelar" runat="server" ID="Btn_cancel" CssClass="btn btn-primary" Width="200" OnClick="Btn_cancel_Click" Visible="false"/>
              <asp:Button ID="Btn_addAdmin" Text="Agregar" runat="server" OnClick="Btn_addAdmin_Click" CssClass="btn btn-primary" Width="200"/>
            </div>
    
          </div>
        </div>
      </div>
</asp:Content>