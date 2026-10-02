<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Grupos.aspx.cs" Inherits="TutoriasWeb.Grupos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function abrirModalAgregar() {
            $('#mdl_maestro').modal('show');
        }


        function Confirmacion() {
            var seleccion = confirm("¿Seguro que quiere borrar el registro?");
            return seleccion; // Prevenir el postback si se cancela
        }



        
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
    .font {
        font-size: 1px;
    }

</style>


    <div class=" col-lg-20 form-inline justify-content-center" style="height:100px;">

        <div style="width: 700%; max-width: 700px;">

            <button type="button" class="btn btn-primary" id="btn_g" onclick="abrirModalAgregar()">Agregar</button>



            <div class="row-25 col-25 justify-content-start"  >
                <asp:GridView styele="width:350px;" ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand"
                    AutoGenerateColumns="false"
                    DataKeyNames="GrupoID"
                    CssClass="table table-striped table-bordered text-center shadow rounded table-responsive table-hover"
                    HeaderStyle-BackColor="#003366"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-Height="40px"
                    ShowHeader="true">
                    <HeaderStyle CssClass="rounded-top" />

                    <Columns>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="left: 265px; margin-top: -10px;">
                                    <asp:LinkButton
                                        Style="background: none; border-color: grey; color: #333; padding: 1%; cursor: pointer; text-decoration: underline;"
                                        runat="server"
                                        Text="Reporte"
                                        CssClass="btn btn-primary"
                                        OnClientClick='<%# "return abrirModalReportes(\"" + Eval("GrupoId") + "\")" %>'
                                        ID="btnReportes" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="GrupoNombre" HeaderText="Grupo">
                            <HeaderStyle Width="250px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                        <asp:BoundField DataField="MaestroNombre" HeaderText="Maestro">
                            <HeaderStyle Width="350px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                        </asp:BoundField>

                       <asp:TemplateField HeaderText="Materia">
                           <HeaderStyle Width="300px" BackColor="#003366" ForeColor="White" CssClass="rounded-end" />
                           <ItemStyle Width="300px" CssClass="text-center" />
                           <ItemTemplate>
                               <asp:Label ID="lblMateria" runat="server" Text='<%# Eval("MateriaNombre") %>' />
                           </ItemTemplate>
                       </asp:TemplateField>







                        <asp:TemplateField>
                            <ItemTemplate>
                             <div  style="position: absolute; right: 170px; margin-top: -10px;" CssClass="table-responsiv">

                                 <div>
                                     <asp:ImageButton
                                         Style="margin-right: 80px;"
                                         CommandArgument='<%# Eval("GrupoID") %>'
                                         Width="17px"
                                         CommandName="EliminarGrupo"                                         
                                         runat="server"
                                         ImageUrl="https://cdn-icons-png.flaticon.com/512/1214/1214428.png" />
                                 </div>


                                 <itemtemplate>
                                     <asp:ImageButton CommandName="Editar"
                                         CommandArgument='<%# 
                                         Container.DataItemIndex %>'
                                         runat="server"
                                         style="margin-right: 80px;"
                                         Width="17px"
                                         ImageUrl="https://cdn-icons-png.flaticon.com/512/1159/1159633.png" />
                                 </itemtemplate>
                                 </asp:TemplateField>

                                    <asp:LinkButton
                                       Style="margin-left:50px; margin-top:-65px; background: none; border: none; color: #333; padding: 1%; cursor: pointer; text-decoration: underline;"
                                        OnClientClick='<%# "return abrirModalMaterias(\"" + Eval("GrupoID") + "\")" %>'
                                        ID="btnMateriasAlumno"
                                        runat="server"
                                        Text="Ver Alumnos"
                                        CssClass="btn btn-primary" />


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
    <div class="modal fade" id="mdl_maestro" tabindex="-1" role="dialog" aria-labelledby="mdlMaestroLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">>
        <div class="modal-content">
      
        <div class="modal-header">
            <h4 class="modal-title" id="mdlMaestroLabel">Agregar Grupo</h4>
            <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">×</button>
        </div>
        
        <div class="modal-body">
            <div class="col-12">
                <div class="form-group">

                   <!-- Cambia el Label ID para evitar conflicto -->
                <asp:Label ID="lblGrupoID" Text="" runat="server" Visible="false"/>
                    <div class=" row">
                        <asp:Label ID="lb1" Text="" runat="server" />
                        <asp:Label ID="lb2" Text="" runat="server" />
                        <asp:Label ID="lb3" Text="" runat="server" />
                    </div>

                    <div style="margin-top:10px;">
                    <label>Nombre</label>
                    <asp:TextBox id="Nombre" runat="server"  CssClass="form-control text-uppercase" AutoCompleteType="Disabled"  CausesValidation="true" />
                    </div>

                    <div>
                    <label>Seleccionar Tutor</label>
                    <asp:DropDownList ID="ddlMaestro"  runat="server" CssClass="form-control" AppendDataBoundItems="true">
                    <asp:ListItem  Value="" />
                    </asp:DropDownList>
                    </div>

                    <asp:Label ID="lblMateria" runat="server" CssClass="form-control" Visible="false">
                    </asp:Label>

                    
                    
                </div>
            </div>
        </div>
        
        <div class="modal-footer justify-content-center">
            <asp:Button Text="Cancelar" runat="server" ID="Btn_cancel" CssClass="btn btn-primary" Width="200" OnClick="Btn_cancel_Click" Visible="false"/>
            <asp:Button ID="Btn_addGroup" Text="Agregar" runat="server" OnClick="Btn_addGroup_Click" CssClass="btn btn-primary" Width="200"/>
        </div>
        
        </div>
      </div>
    </div>

    


<script type="text/javascript">
    // Función para abrir el modal de agregar (existente)
    function abrirModalAgregar() {
        $('#mdl_maestro').modal('show');
    }

    // Función de confirmación (existente)
    function Confirmacion() {
        return confirm("¿Seguro que quiere borrar el registro?");
    }

    // Nueva función para materias alumnos
    function abrirModalMaterias(grupoID) {
        $('#iframeMaterias').attr('src', 'MateriasAlumno.aspx?grupoID=' + grupoID);
        $('#mdl_materias_alumno').modal('show');
        return false; // Previene el postback
    }

    // Nueva función para reportes
    function abrirModalReportes(grupoID) {
        $('#iframeReportes').attr('src', 'Reportes.aspx?grupoID=' + grupoID);
        $('#mdl_Reportes').modal('show');
        return false; // Previene el postback
    }
</script>



<!-- The Modal Reporte-->
<div class="modal fade" id="mdl_Reportes" tabindex="-1" role="dialog" aria-labelledby="mdlReportesLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content" style="left:-400px; width: 1300px; height: 500px ; margin-top:-0px;">
            <div class="modal-header " style="height:40px; margin-top:0px; width:1300px; background-color:rgb(0,51,102); color:white;" >
                <h4 class="modal-title" id="mdlReportesLabel" style="font-size:18px; margin-top:-15px;">Reporte</h4>
                <button type="button" class="close" data-dismiss="modal" style="color:white; margin-top:-30px; " aria-label="Cerrar">×</button>
            </div>
            <div class="modal-body-center " style=" width: 1100px; height: 612px; margin-top:0px;" > 
                <iframe id="iframeReportes"  style=" width: 1300px; height: 500px; " ></iframe><!--Ventana de Grupos-->
            </div>
        </div>
    </div>
</div>

<!-- The Modal Materias Alumno-->
<div class="modal fade" id="mdl_materias_alumno" tabindex="-1" role="dialog" aria-labelledby="mdlMateriasAlumnoLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content" style="left:-395px; width: 1300px; height: 500px ; margin-top:-0px;">
            <div class="modal-header " style="height:40px; margin-top:0px; width:1300px; background-color:rgb(0,51,102); color:white;" >
                <h4 class="modal-title" id="mdlMateriasAlumnoLabel" style="font-size:18px; margin-top:-15px;">Alumnos</h4>
                <button type="button" class="close" data-dismiss="modal" style="color:white; margin-top:-30px; " aria-label="Cerrar">×</button>
            </div>
            <div class="modal-body-center " style=" width: 1100px; height: 612px; margin-top:0px;" > 
                <iframe id="iframeMaterias"  style=" width: 1300px; height: 500px; " ></iframe><!--Ventana de Grupos-->
            </div>
        </div>
    </div>
</div>

    

</asp:Content>


