<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="TutoriasWeb.Reportes" %>
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
    <div class="row col-10 justify-content-center" style="margin-bottom:35px; margin-top:15px;" runat="server" id="divBuscar" visible="false">
        <div class=" col-6 form-inline justify-content-around">
            <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" Width="400" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:Button runat="server" Text="btnBuscar" ID="Btn_buscar" OnClick="Btn_buscar_Click" CssClass="btn btn-primary" Width="100" />
        </div>
                
    </div>

    <div class="row col-lg-12 form-inline">
        <div class="row col-4 justify-content-around" style="margin-top:20px; margin-left:15px;">
            <%--<asp:DropDownList runat="server" ID="reportes" CssClass="form-control" OnSelectedIndexChanged="reportes_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Seleccione tipo de reporte" />
                <asp:ListItem Text="Reportes de seguimiento" Value=""/>
                <asp:ListItem Text="Reportes concluido" Value="CONCLUIDO"/>
                <asp:ListItem Text="Reportes Resagados/Reprobados" Value="RESAGADOS"/>
                <asp:ListItem Text="Reportes liberados" Value="LIBERADOS"/>
                <asp:ListItem Text="Reporte individual" />
            </asp:DropDownList>--%>
            <asp:DropDownList runat="server" ID="reportes" CssClass="form-control" OnSelectedIndexChanged="reportes_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Seleccione tipo de reporte" />
                <asp:ListItem Text="Reportes Por Alumno" Value="Alumno"/>
                <asp:ListItem Text="Reportes Por Grupo" Value="Grupo"/>
            </asp:DropDownList>
            <%--<asp:Button runat="server" CssClass="btn btn-primary" id="consultar" Text="Consultar" OnClick="consultar_Click"/>--%>
        </div>
        <div class="row col-8 justify-content-start" style="margin-top:20px;">
            <div class="row col-10 form-inline justify-content-around" runat="server" id="m_buscar" visible="false">
                <asp:TextBox runat="server" ToolTip="No Control" ID="No_Control" CssClass="form-control" Width="400"></asp:TextBox>
                <asp:Button runat="server" Text="Buscar" ID="Buscar" OnClick="Buscar_Click" CssClass="btn btn-primary" Width="100" />
                <label>(Insertar No. Control del alumno)</label>
            </div>
                
        </div>

        <div class="row col-12 justify-content-start" >
            <asp:GridView runat="server" ID="gvGrupos" Visible="false" AutoGenerateColumns="false" DataKeyNames="ID"
                CssClass="table table-bordered table-condensed table-responsive table-hover " OnRowCommand="gvGrupos_RowCommand" >
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Grupo" />
                    <asp:BoundField DataField="Tutor" HeaderText="Tutor asignado" />
                    <asp:TemplateField HeaderText="Reporte">
                        <ItemTemplate>
                            <asp:ImageButton  runat="server" CommandArgument='<%# Eval("ID")%>' Width="40px" Height="40px" ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0V_CEV_mbCNMxIyQtr7x0h34Hte4xYK1Vbg&usqp=CAU" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="row col-12 justify-content-start" >
            <asp:GridView runat="server" ID="gvAlumnos" Visible="false" AutoGenerateColumns="false" DataKeyNames="No_control"
                CssClass="table table-bordered table-condensed table-responsive table-hover " OnRowCommand="gvAlumnos_RowCommand" >
                <Columns>
                    <asp:BoundField DataField="No_control" HeaderText="No. Control" />
                    <asp:BoundField DataField="Alumno" HeaderText="Nombre Alumno" />
                    <asp:BoundField DataField="Semestre" HeaderText="Semestre" />
                    <asp:BoundField DataField="Tutoria" HeaderText="Tutorias finalizadas" />
                    <asp:TemplateField HeaderText="Reporte 1">
                        <ItemTemplate>
                            <asp:ImageButton  runat="server" Visible='<%# Convert.ToBoolean(Eval("btn1")) %>' Width="40px" Height="40px" CommandArgument='<%# Eval("No_control")%>' CommandName="r1" ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0V_CEV_mbCNMxIyQtr7x0h34Hte4xYK1Vbg&usqp=CAU" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reporte 2">
                        <ItemTemplate>
                            <asp:ImageButton  runat="server" Visible='<%# Convert.ToBoolean(Eval("btn2")) %>' Width="40px" Height="40px" CommandArgument='<%# Eval("No_control")%>' CommandName="r2" ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0V_CEV_mbCNMxIyQtr7x0h34Hte4xYK1Vbg&usqp=CAU" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reporte 3">
                        <ItemTemplate>
                            <asp:ImageButton  runat="server" Visible='<%# Convert.ToBoolean(Eval("btn3")) %>' Width="40px" Height="40px" CommandArgument='<%# Eval("No_control")%>' CommandName="r3" ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0V_CEV_mbCNMxIyQtr7x0h34Hte4xYK1Vbg&usqp=CAU" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="row col-12 justify-content-start">
            <asp:GridView ID="GridView1" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover "  >
                <Columns>
                    <asp:BoundField DataField="No_control" HeaderText="No. Control" />
                    <asp:BoundField DataField="A_Paterno" HeaderText="Apellido Paterno" />
                    <asp:BoundField DataField="A_Materno" HeaderText="Apellido Materno" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre(s)" />
                    <asp:BoundField DataField="Semestre" HeaderText="Semestre" />
                    <asp:BoundField DataField="Estatus" HeaderText="Status" />
                    <asp:BoundField DataField="Tutoria" HeaderText="Tutoria" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="row col-12 justify-content-start">
            <asp:GridView ID="GridView2" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover "  >
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Grupo" />
                    <asp:BoundField DataField="A_Paterno" HeaderText="A. Paterno" />
                    <asp:BoundField DataField="A_Materno" HeaderText="A. Materno" />
                    <asp:BoundField DataField="Nombre_Maestro" HeaderText="Nombre" />
                    <asp:BoundField DataField="Cal1" HeaderText="Cal 1" />
                    <asp:BoundField DataField="Cal2" HeaderText="Cal 2" />
                    <asp:BoundField DataField="Cal3" HeaderText="Cal 3" />
                    <asp:BoundField DataField="Cal4" HeaderText="Cal 4" />
                    <asp:BoundField DataField="Cal5" HeaderText="Cal 5" />
                    <asp:BoundField DataField="Cal6" HeaderText="Cal 6" />
                    <asp:BoundField DataField="Entrevista1" HeaderText="Entrevista 1" DataFormatString = "{0:d}"/>
                    <asp:BoundField DataField="Entrevista2" HeaderText="Entrevista 2" DataFormatString = "{0:d}"/>
                    <asp:BoundField DataField="Entrevista3" HeaderText="Entrevista 3" DataFormatString = "{0:d}"/>
                    <asp:BoundField DataField="Estado_Civil" HeaderText="Estado Civil" />
                    <asp:BoundField DataField="Dedicacion" HeaderText="Dedicación" />
                    <asp:BoundField DataField="Comentarios" HeaderText="Comentarios" />
                    <asp:BoundField DataField="Sem" HeaderText="Semestre" />
                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                </Columns> 
            </asp:GridView>
        </div>
    </div>

</asp:Content>
