<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="Cierre.aspx.cs" Inherits="TutoriasWeb.Cierre" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">
        function openModal() {
            $(document).ready(function () {
                $("#mdl").modal();
            })
        };
        function Confirmacion() {

            var seleccion = confirm("¿Seguro que desea continuar? Una vez realizado este proceso, ya no se podran modificar los datos.");
            /*
            if (seleccion)
                alert("se acepto el mensaje");
            else
                alert("NO se acepto el mensaje");
                */

            //usado para que no haga postback el boton de asp.net cuando 
            //no se acepte el confirm
            return seleccion;

        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12 form-inline">
        <div class="row col-10 justify-content-center" style="margin-top:15px;">
            <div class=" col-6 form-inline justify-content-around">
                <asp:DropDownList ID="grupo" runat="server" CssClass="form-control" OnSelectedIndexChanged="grupo_SelectedIndexChanged" AutoPostBack="true" /> 
            </div>
        </div>

        <div class="col-12"style="margin-bottom:10px;">
            <asp:Button Text="Calcular Datos" runat="server" ID="btnCalcular" CssClass="btn btn-primary" OnClick="btnCalcular_Click" Visible="false" />
        </div>

        <div class="row col-12 justify-content-start">
            

            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover "  >
                <Columns>

                    <asp:BoundField DataField="No_control" HeaderText="No. Control" />
                    <asp:BoundField DataField="A_Paterno" HeaderText="Apellido Paterno" />
                    <asp:BoundField DataField="A_Materno" HeaderText="Apellido Materno" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre(s)" />
                    <asp:BoundField DataField="Asistencia1" HeaderText="Entrevista1 " />
                    <asp:BoundField DataField="Asistencia2" HeaderText="Entrevista2 " />
                    <asp:BoundField DataField="Asistencia3" HeaderText="Entrevista3 " />
                    <asp:BoundField DataField="A" HeaderText="A" /> <%--SI * SON ACREDITA EN CASO CONTRARIO 'NO'--%>
                    <asp:BoundField DataField="B" HeaderText="B" />  <%--SI SE TIENEN TODAS LAS CALIFICACIONES ES SI, SINO NO--%>
                    <asp:BoundField DataField="Cal1" HeaderText="Mat1" />
                    <asp:BoundField DataField="Cal2" HeaderText="Mat2"/>
                    <asp:BoundField DataField="Cal3" HeaderText="Mat3"/>
                    <asp:BoundField DataField="Cal4" HeaderText="Mat4"/>
                    <asp:BoundField DataField="Cal5" HeaderText="Mat5"/>
                    <asp:BoundField DataField="Cal6" HeaderText="Mat6"/>
                    <asp:BoundField DataField="Promedio" HeaderText="PROMEDIO" />
                    <asp:BoundField DataField="D" HeaderText="D" /> <%--NINGUNA REPROBADA Y PROM >90--%>
                    <asp:BoundField DataField="N" HeaderText="N" /> <%--NINGUNA REPROBADA Y PROM <90--%>
                    <asp:BoundField DataField="I" HeaderText="I" /> <%--1 O 2 MAT REPROB.--%>
                    <asp:BoundField DataField="R" HeaderText="R" /> <%--3 O MAS MAT. REPR--%>
                    <asp:BoundField DataField="Comentarios" HeaderText="OBSERVACIONES" /> 
                    
                    
                </Columns>
            </asp:GridView>                            
        </div>
        <div class="row col-12 justify-content-center">
            <asp:Button Text="CERRAR SEMESTRE" Visible="false" runat="server" ID="btnCerrar" OnClientClick="return Confirmacion()" CssClass="btn btn-primary" OnClick="btnCerrar_Click"/>
        </div>
        
    </div>



    
    <!-- The Modal Alumno-->
    <div class="modal fade" id="mdl">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title">Alumno</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>

                <div class="modal-body">
                    <div class="row col-12 justify-content-center">
                        <div class="form-group">
                            <asp:Label ID="control" Visible="false" runat="server" />
                            <asp:Label ID="id" Visible="false" runat="server" />

                            <asp:RadioButtonList runat="server" ID="estatus">
                                <asp:ListItem Text="APROBO" />
                                <asp:ListItem Text="REPROBO" />
                            </asp:RadioButtonList>
                            
                        </div>
                    </div>
                </div>

                <div class="modal-footer justify-content-center">
                    <asp:Button Text="Cancelar" runat="server" ID="Btn_cancel" OnClick="Btn_cancel_Click" CssClass="btn btn-primary" Width="200" />
                    <asp:Button Text="Aceptar" ID="Btn_aceptar" OnClick="Btn_aceptar_Click" runat="server" CssClass="btn btn-primary" Width="200" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>
