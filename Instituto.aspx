<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Instituto.aspx.cs" Inherits="TutoriasWeb.Instituto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#btn_a").click(function () {
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


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >



    <div class="col-lg-12 form-inline justify-content-center">

        <div style="width: 150%; max-width: 500px;">

            <div runat="server" id="add">
                <button type="button" class="btn btn-primary" id="btn_a">Agregar</button>
            </div>

            <div class="row col-12 justify-content-start" width="350px">

                <asp:GridView styele="width:350px;" ID="GridView1" runat="server" AutoGenerateColumns="false"
                    OnRowCommand="GridView1_RowCommand"
                    CssClass="table table-striped table-hover table-bordered table-m text-center shadow rounded table-responsive "
                    HeaderStyle-BackColor="#003366"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-Height="40px"
                    ShowHeader="true">

                    <HeaderStyle CssClass="rounded-top" />


                    <Columns>

                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <HeaderStyle Width="120px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>



                        <asp:TemplateField HeaderText="Logo">
                            <HeaderStyle Width="200px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                            <ItemTemplate>
                                <br />
                                <asp:Image runat="server" Width="500px" CssClass="img-fluid rounded" ImageUrl='<%# Eval("Logo") %>' />
                                <br />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sitio Web">
                            <HeaderStyle Width="150px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                            <ItemTemplate>
                                <br />
                                <a href='<%# Eval("Sitio") %>' class="btn btn-outline-primary btn-sm" target="_blank">Visitar Sitio
                                </a>
                                <br />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Carreras">
                            <HeaderStyle BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                            <ItemTemplate>
                                <br />
                                <asp:Button runat="server" Text="Carreras" CommandName="Carreras"
                                    CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-success" />
                                <br />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="position: absolute; right: -60px; margin-top: -5px;">
                                    <div>
                                        <asp:ImageButton
                                            Style="margin-right: 80px;"
                                            Width="20px"
                                            CommandName="Eliminar"
                                            CommandArgument='<%# Eval("ID") %>'
                                            runat="server"
                                            ImageUrl="https://cdn-icons-png.flaticon.com/512/1214/1214428.png"
                                            OnClientClick="return confirm('¿Eliminar este instituto?');" />
                                    </div>

                                    <asp:TemplateField HeaderText="Editar">
                                        <itemtemplate>
                                            <asp:ImageButton
                                                style="margin-right: 80px;"
                                                Width="20px"
                                                CommandName="Editar"
                                                CommandArgument='<%# Eval("ID") %>'
                                                runat="server"
                                                ImageUrl="https://cdn-icons-png.flaticon.com/512/1159/1159633.png" />
                                        </itemtemplate>
                                    </asp:TemplateField>

                                    <div>
                                    <asp:LinkButton
                                        Style=" margin-right:-65px; background: none; border: none; color: #333; padding: 1%; cursor: pointer; text-decoration: underline;"
                                        CommandName="Administradores"
                                        CommandArgument='<%# Eval("ID") %>'
                                        ID="btnAdministrador"
                                        runat="server"
                                        Text="Coordinadores" />

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



    <!-- The Modal Maestro-->
    <div class="modal fade" id="mdl">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title">Agregar Instituto</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>

                <div class="modal-body">
                    <div class="col-12">
                        <div class="form-group">
                            <asp:Label ID="ID" Text="ID" runat="server" Visible="false" />
                            <div>
                                <label>Nombre</label>
                                <asp:TextBox ID="nombre" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                            </div>
                            <div>
                                <label>Logo(URL)</label>
                                <asp:TextBox ID="logo" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                            </div>
                            <div>
                                <label>Sitio(URL)</label>
                                <asp:TextBox ID="sitio" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer justify-content-center">
                    <asp:Button Text="Cancelar" runat="server" ID="Btn_cancel" CssClass="btn btn-primary" Width="200" OnClick="Btn_cancel_Click" Visible="false" />
                    <asp:Button ID="Btn_addInst" Text="Agregar" runat="server" OnClick="Btn_addInst_Click" CssClass="btn btn-primary" Width="200" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>
