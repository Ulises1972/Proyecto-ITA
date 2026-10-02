<%@ Page Language="C#"  MasterPageFile="~/Minimal.master" AutoEventWireup="true" CodeBehind="MateriasAlumno.aspx.cs" Inherits="TutoriasWeb.MateriasAlumno" %>

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
        $("#mdl").modal();
    };

    function abrirModalAgregar() {
        $('#mdl_maestro').modal('show');
    }

    function validarPorcentajes() {
        let suma = 0;
        // Obtener los IDs de los controles ASP.NET
        var textBoxIds = [
            '<%= TextBox1.ClientID %>',
            '<%= TextBox3.ClientID %>',
            '<%= TextBox5.ClientID %>',
            '<%= TextBox7.ClientID %>',
            '<%= TextBox9.ClientID %>',
            '<%= TextBox11.ClientID %>',
            '<%= TextBox13.ClientID %>',
            '<%= TextBox15.ClientID %>'
        ];

        for (let i = 0; i < 8; i++) {
            const porcentaje = parseFloat(document.getElementById(textBoxIds[i]).value) || 0;
            if (porcentaje < 0 || porcentaje > 100) {
                alert("El porcentaje para U" + (i + 1) + " debe estar entre 0 y 100");
                return false;
            }
            suma += porcentaje;
        }
        if (suma > 100) {
            alert("La suma de porcentajes no puede exceder 100%");
            return false;
        }
        return true;
    }

    function configurarModal() {
        var grupoID = <%= GrupoID %>; // ¡Ahora sí accede al valor de C#!
    var tienePorcentajes = <%= Session["PorcentajesGrupo_" + GrupoID] != null %>;
    
    
    if (tienePorcentajes) {
        $("#<%= TextBox1.ClientID %>").prop("disabled", true);
            $("#<%= TextBox3.ClientID %>").prop("disabled", true);
            // ... (deshabilita los 8 campos)
        }


    }

    // Llama a la función al abrir el modal
    $(document).ready(function () {
        $("#btn_a").click(function () {
            configurarModal();
            $("#mdl").modal();
        });
    });
</script>

</asp:Content>

<asp:Content  ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >



    <div class="col-lg-12 justify-content-center">
        <div style="width:800px;" ><!--ACOMODAR TABLAR-->
        <div runat="server" id="add" >
            <button type="button" class="btn btn-primary" id="btn_a">Agregar</button>
        </div>

            <div class="row col-12 justify-content-start font" style="width:1140px; left:-20px;"  ><!--DIV QUE LIMITA TABLA-->
                  <asp:GridView ID="GridViewAlumnos"  runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover table-bordered  text-center shadow rounded font "
                    HeaderStyle-BackColor="#003366"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-Height="40px"
                    HeaderStyle-Width="1100px"
                    HeaderStyle-Font-size="10px"
                    ShowHeader="true"
                    OnRowCommand="GridViewAlumnos_RowCommand">

                    <headerstyle cssclass="rounded-top" />


                      <columns>

                          <asp:TemplateField>
                              <itemtemplate >
                                  <div style="position: absolute; left: 20px; margin-top: -10px;  border-top-left-radius: 12px;">
                                      <div>
                                          <asp:ImageButton
                                              CssClass="rounded-start "
                                              Style="margin-right: 80px;"
                                              Width="17px"
                                              CommandName="Eliminar"
                                              CommandArgument='<%# Eval("No_control") %>'
                                              runat="server"
                                              ImageUrl="https://cdn-icons-png.flaticon.com/512/1214/1214428.png"
                                              OnClientClick="return confirm('¿Eliminar este Alumno?');" />
                                      </div>

                                      <asp:TemplateField HeaderText="Editar">
                                          <itemtemplate>
                                              <asp:ImageButton
                                                  Style="margin-right: 80px;"
                                                  Width="17px"
                                                  CommandName="Editar"
                                                  CommandArgument='<%# Eval("No_control") %>'
                                                  runat="server"
                                                  ImageUrl="https://cdn-icons-png.flaticon.com/512/1159/1159633.png" />
                                          </itemtemplate>

 
                                  </div>
                            </itemtemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="No_control" HeaderText="No. Control"/>
                          <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo"/>
                          <asp:BoundField DataField="Semestre" HeaderText="Semestre" HeaderStyle-Width="10px"  />


                          <asp:TemplateField HeaderText="Mat. 1">
                              <itemtemplate >
                                   <%# Eval("u1_1") %> 
                              </itemtemplate>
                               <ItemStyle Width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="(%)">
                              <ItemTemplate>
                                 <%# Eval("u1_2") %>
                              </ItemTemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Mat. 2">
                              <itemtemplate>
                                   <%# Eval("u2_1") %> 
                              </itemtemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="(%)">
                              <ItemTemplate>
                                  <%# Eval("u2_2") %>
                              </ItemTemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Mat. 3">
                              <itemtemplate>
                                   <%# Eval("u3_1") %> 
                              </itemtemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="(%)">
                              <ItemTemplate>
                                  <%# Eval("u3_2") %>
                              </ItemTemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Mat. 4">
                              <itemtemplate>
                                   <%# Eval("u4_1") %> 
                              </itemtemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="(%)">
                              <ItemTemplate>
                                  <%# Eval("u4_2") %>
                              </ItemTemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>

                           <asp:TemplateField HeaderText="Mat. 5">
                               <itemtemplate>
                                    <%# Eval("u5_1") %>
                               </itemtemplate>
                               <ItemStyle Width="5px" />
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="(%)">
                              <ItemTemplate>
                                  <%# Eval("u5_2") %>
                              </ItemTemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Mat. 6">
                              <itemtemplate>
                                   <%# Eval("u6_1") %> 
                              </itemtemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="(%)">
                              <ItemTemplate>
                                  <%# Eval("u6_2") %>
                              </ItemTemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Mat. 7">
                              <itemtemplate>
                                   <%# Eval("u7_1") %> 
                              </itemtemplate>
                              <itemstyle width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="(%)">
                              <ItemTemplate>
                                  <%# Eval("u7_2") %>
                              </ItemTemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Mat. 8">
                              <itemtemplate>
                                   <%# Eval("u8_1") %> 
                              </itemtemplate>
                              <itemstyle width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="(%)">
                              <ItemTemplate>
                                  <%# Eval("u8_2") %>
                              </ItemTemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>

                          <asp:BoundField ControlStyle-Width="50px" DataField="Promedio" HeaderText="Promedio" DataFormatString="{0:N2}" />



                        <asp:TemplateField HeaderText="Asist.">
    <ItemTemplate>
        <asp:HiddenField ID="hfNoControl" runat="server" Value='<%# Eval("No_control") %>' />
        
        <div style="width: 63px; margin-top: -10px;">
            <asp:RadioButton 
                Style="position: absolute; right: -493px;"
                ID="RadioButton1" runat="server" Text="Entrevista 1"
                Checked='<%# (Eval("Entrevista1") != DBNull.Value) && Convert.ToBoolean(Eval("Entrevista1")) %>' />
        </div>

        <div>
            <asp:RadioButton 
                Style="position: absolute; right: -493px; margin-top: 18px;"
                ID="RadioButton2" runat="server" Text="Entrevista 2"
                Checked='<%# (Eval("Entrevista2") != DBNull.Value) && Convert.ToBoolean(Eval("Entrevista2")) %>' />
        </div>

        <div>
            <asp:RadioButton 
                Style="position: absolute; right: -493px; margin-top: 38px;"
                ID="RadioButton3" runat="server" Text="Entrevista 3"
                Checked='<%# (Eval("Entrevista3") != DBNull.Value) && Convert.ToBoolean(Eval("Entrevista3")) %>' />
        </div>

        <asp:Button 
            ID="btnGuardarEntrevistas" 
            OnClick="btnGuardarEntrevistas_Click" 
            Text="Guardar" 
            Style="position: absolute; right: -493px; margin-top: 50px; color: grey;" 
            runat="server" 
            CssClass="btn btn-link" />
    </ItemTemplate>
</asp:TemplateField>

                                 
                             
                         
                          
                    </columns>
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
        .font{
            font-size:13px;
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
        .table, th, td{
            border: 1px solid black;
            width:100px;
        }
    
    </style>



    <!-- The Modal Materia_Alumno-->
    <div class="modal fade" id="mdl">
        <div class="modal-dialog">
            <div class="modal-content">

              

                <div class="modal-body">
                    <div class="col-12">
                        <div class="form-group">
                            <asp:Label ID="ID" Text="ID" runat="server" Visible="false" />
                            
                            <div class="form-group">
                                <label>Seleccionar Alumno</label>
                                <asp:DropDownList ID="No_Control" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="" />
                                </asp:DropDownList>
                            </div>
                            <br />
                           
                            
                            <br />
                       <div class="form-group">
    <label>Calificaciones</label>
    
    <!-- Unidad 1 a 8 -->
    <div class="row">
        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 1</label>
            <div class="form-row">
                <div class="col">
                    <label>Calificación</label>
                    <asp:TextBox ID="TextBox" runat="server"  CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>Pocentaje</label>
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="% U1" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>
        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 2</label>
            <div class="form-row">
                <div class="col">
                    <label>Calificación</label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>Pocentaje</label>
                    <asp:TextBox ID="TextBox3" runat="server" placeholder="% U2" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>


        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 3</label>
            <div class="form-row">
                <div class="col">
                    <label>Calificación</label>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>Pocentaje</label>
                    <asp:TextBox ID="TextBox5" runat="server" placeholder="% U3" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 4</label>
            <div class="form-row">
                <div class="col">
                    <label>Calificación</label>
                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>Pocentaje</label>
                    <asp:TextBox ID="TextBox7" runat="server" placeholder="% U4" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 5</label>
            <div class="form-row">
                <div class="col">
                    <label>Calificación</label>
                    <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>Pocentaje</label>
                    <asp:TextBox ID="TextBox9" runat="server" placeholder="% U5" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 6</label>
            <div class="form-row">
                <div class="col">
                    <label>Calificación</label>
                    <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>Pocentaje</label>
                    <asp:TextBox ID="TextBox11" runat="server" placeholder="% U6" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 7</label>
            <div class="form-row">
                <div class="col">
                    <label>Calificación</label>
                    <asp:TextBox ID="TextBox12" runat="server"  CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>Pocentaje</label>
                    <asp:TextBox ID="TextBox13" runat="server" placeholder="% U7" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 8</label>
            <div class="form-row">
                <div class="col">
                    <label>Calificación</label>
                    <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>Pocentaje</label>
                    <asp:TextBox ID="TextBox15" runat="server" placeholder="% U8" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

    </div>
</div>
                            <div class="modal-footer justify-content-center">
                                <asp:Button Text="Cancelar" runat="server" ID="Button1" CssClass="btn btn-primary" Width="200" Visible="false" />
                                <asp:Button ID="Button2" Text="Agregar" runat="server" CssClass="btn btn-primary" Width="200" onClick="Button2_Click" OnClientClick="return validarPorcentajes();" />
                                <asp:Button ID="btnActualizar" Text="Actualizar" runat="server" CssClass="btn btn-success" Width="200" OnClick="btnActualizar_Click" OnClientClick="return validarPorcentajes();" Visible="false" /> 
                            </div>

                        </div>



                        <!-- The Modal Certificado-->
                        <div class="modal fade" id="mdl_Reportes" tabindex="-1" role="dialog" aria-labelledby="mdlReportesLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content" style="left: -400px; width: 1300px; height: 500px; margin-top: -0px;">
                                    <div class="modal-header " style="height: 40px; margin-top: 0px; width: 1300px; background-color: rgb(0,51,102); color: white;">
                                        <h4 class="modal-title" id="mdlReportesLabel" style="font-size: 18px; margin-top: -15px;">Reporte</h4>
                                        <button type="button" class="close" data-dismiss="modal" style="color: white; margin-top: -30px;" aria-label="Cerrar">×</button>
                                    </div>
                                    <div class="modal-body-center " style="width: 1100px; height: 612px; margin-top: 0px;">
                                        <iframe id="iframeReportes" style="width: 1300px; height: 500px;"></iframe>
                                        <!--Ventana de Grupos-->
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
