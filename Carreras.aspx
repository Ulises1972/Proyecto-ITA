<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carreras.aspx.cs" Inherits="TutoriasWeb.Carreras" %>

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

        function toggleOpciones(imgBtn) {
            // Oculta todos primero
            document.querySelectorAll('.opciones-menu').forEach(el => el.classList.add('d-none'));

            // Muestra el menú correspondiente al botón
            const menu = imgBtn.nextElementSibling;
            const rect = imgBtn.getBoundingClientRect();

            menu.style.top = (rect.bottom + window.scrollY) + 'px';
            menu.style.left = (rect.left + window.scrollX) + 'px';
            menu.classList.remove('d-none');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12 form-inline justify-content-center ">

        <div >
            <button type="button" class="btn btn-primary" id="btn_m">Agregar</button>

            <div class="row col-12 justify-content-start" >

                <asp:GridView ID="GridView1" runat="server" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand"
                    AutoGenerateColumns="false" CssClass="table table-striped table-hover table-bordered table-m text-center shadow rounded " DataKeyNames="ID"
                    HeaderStyle-BackColor="#003366"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-Height="40px"
                    ShowHeader="true">
                    <HeaderStyle CssClass="rounded-top" />

                    <Columns>

                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <HeaderStyle Width="200px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Omoclave" HeaderText="Clave">
                            <HeaderStyle Width="120px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>



                        <asp:TemplateField HeaderText="Logo">
                            <HeaderStyle Width="120px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                            <ItemTemplate>
                                <asp:Image runat="server" Width="200" ImageUrl='<%# Eval("Logo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Pertenecientes">
                            <HeaderStyle  BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                            <ItemTemplate>
                                <div class="dropdown-hover-container">
                                    <asp:ImageButton ID="btnOpciones" runat="server"
                                        ImageUrl="https://cdn.iconscout.com/icon/premium/png-512-thumb/comunidades-5-371060.png?f=webp&w=256"
                                        Width="40px"
                                        CssClass="comunidad-icon"
                                        
                                        OnClientClick="toggleOpciones(this); return false;" />

                                    <div class="dropdown-hover-menu">
                                        <asp:Button runat="server" Text="Tutores  " CommandName="Tutores" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-info btn-sm d-block my-1" />
                                        <asp:Button runat="server" Text="Materias" CommandName="Materias" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-info btn-sm d-block my-1" />
                                        <asp:Button runat="server" Text="Alumnos" CommandName="Alumnos" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-info btn-sm d-block my-1" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField>
                            <ItemTemplate>


                                <div style="position: absolute; left: 635px; margin-top: 1px;">

                                

                                    <asp:TemplateField ShowHeader="false">
                                        <itemtemplate>
                                            <asp:ImageButton style="margin-right: 80px;" Width="20px" OnClientClick="return Confirmacion();"  CommandName="Delete" runat="server" ImageUrl="https://cdn-icons-png.flaticon.com/512/1214/1214428.png" />
                                        </itemtemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ShowHeader="false">
                                        <itemtemplate>
                                            <asp:ImageButton style="margin-right: 80px;" Width="20px" CommandArgument='<%# Eval("ID") %>' CommandName="Editar" runat="server" ImageUrl="https://cdn-icons-png.flaticon.com/512/1159/1159633.png" />
                                        </itemtemplate>
                                    </asp:TemplateField>

                                    <div style="margin-left:70px;">
                                        <asp:LinkButton
                                        Style=" background: none; border: none; color: #333; padding: 1%; cursor: pointer; text-decoration: underline; white-space: nowrap;"
                                        CommandName="Grupos"
                                        CommandArgument='<%# Eval("ID") %>'
                                        ID="btnArmarGrupos"
                                        runat="server"
                                        Text="Armar Grupos"
                                        CssClass="btn btn-primary" />
                                    </div>


                                </div>
                            </ItemTemplate>



                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>




            </div>
        </div>
    </div>





    <!-- The Modal-->
    <div class="modal fade" id="mdl">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title">Agregar Carrera</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>

                <div class="modal-body">
                    <div class="col-12">
                        <div class="form-group">
                            <div>
                                <asp:Label Text="" ID="ID" Visible="false" runat="server" />
                                <label>Nombre</label>
                                <asp:TextBox ID="ca_nombre" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                            </div>
                            <div>
                                <label>Logo (URL)</label>
                                <asp:TextBox ID="ca_logo" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                            </div>
                            <div>
                                <label>Homoclave</label>
                                <asp:TextBox ID="ca_omo" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                                
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer justify-content-center">
                    <asp:Button Text="Cancelar" runat="server" ID="Btn_cancel" CssClass="btn btn-primary" Width="200" OnClick="Btn_cancel_Click" Visible="false" />
                    <asp:Button ID="Btn_addCarrera" Text="Agregar" runat="server" OnClick="Btn_addCarrera_Click" CssClass="btn btn-primary" Width="200" />
                </div>

            </div>
        </div>
    </div>


    


    <style>
        .dropdown-hover-container {
            position: relative;
            display: inline-block;
        }

        .dropdown-hover-menu {
            display: none;
            position: fixed;
            background-color: white;
            min-width: 120px;
            z-index: 999;
            border: 1px solid #ccc;
            padding: 5px;
            border-radius: 5px;
            box-shadow: 0px 2px 6px rgba(0,0,0,0.2);
        }

        .dropdown-hover-container:hover .dropdown-hover-menu {
            display: block;
        }

        .comunidad-icon {
            cursor: pointer;
        }


        .espaciado-filas tbody tr {
            margin-bottom: 15px !important;
            display: block;
        }

        .espaciado-filas td {
            vertical-align: middle !important;
        }

        .espaciado-filas {
            border-collapse: separate;
            border-spacing: 0 15px;
        }

        .espaciado-filas tbody tr {
                position: relative;
                background-color: white;
                margin-bottom: 15px;
        }

        .btn-grupos-derecha {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            white-space: nowrap;
        }

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
        }
    </style>


</asp:Content>

