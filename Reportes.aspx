<%@ Page Title=""  EnableEventValidation="false" Language="C#" MasterPageFile="~/Minimal.master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="TutoriasWeb.Reportes" Async="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Encabezado -->
    <div style="margin-top:35px;  width:100%;"  >
        <asp:Image  Style="width:200px; margin-left:100px;" runat="server" ImageUrl="/PDF/Recursos/SEPLogo.png"/>

        <asp:Image Style="width:200px;" runat="server" ImageUrl="/PDF/Recursos/TECNMLogo.png"/>

        <div style="position:absolute; margin-left:950px; margin-top:-20px;">
        <label style="font-style:revert; right:500px;"><b>Instituto Tecnológico de Aguascalientes</b></label>
        </div>   
    </div>

   <div class="text-center">

    <h5>Anexo 5</h5>
    <br />

    <h6><b>INSTITUTO TECNOLOGICO DE AGUASCALIENTES</b></h6>
    <h7><b>DEPARTAMENTO DE DESARROLLO ACADÉMICO</b></h7>
    <br />

    <h5>Formato de Informe Final de Tutorías</h5>
    <br />

    <h8>
        <b><i>
            Informe final semestre 
            <asp:TextBox ID="txtSemestreInforme" runat="server" Width="120px" />
            de 2026
        </i></b>
    </h8>

    <br />

    <h8>
        <b>
            Situación Académica de los alumnos de 
            <asp:TextBox ID="txtGrupo" runat="server" Width="150px" />
            Tutoría de la Carrera 
            <asp:TextBox ID="txtCarrera" runat="server" Width="200px" />
        </b>
    </h8>

    <br /><br />

    <h8>
        Nombre de Tutor 
        <asp:TextBox ID="txtNombreTutor" runat="server" Width="350px" />
        &nbsp;&nbsp;
        Fecha de Informe:
        <asp:TextBox ID="txtFechaInforme" runat="server" Width="150px" />
    </h8>

    <br /><br />

    <h8>
        No. de alumnos asignados 
        <asp:TextBox ID="txtAsignados" runat="server" Width="80px" />
        &nbsp;&nbsp;
        No. de alumnos atendidos 
        <asp:TextBox ID="txtAtendidos" runat="server" Width="80px" />
        &nbsp;&nbsp;
        Semestre:
        <asp:TextBox ID="txtSemestre" runat="server" Width="80px" />
    </h8>

</div>

    
  <div style="margin-top:30px; margin-left:225px;">
    <h8><b><i><ins>1.- Resultados específicos</ins></i></b></h8>
    <br /><br />

    <h8 style="margin-left:50px;">No. de alumnos canalizados a:</h8>
    <br />

    <table>
        <tr>
            <td style="width:300px;">Círculos de estudio</td>
            <td>
                <asp:TextBox ID="txtCirculos" runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td>Atención médica</td>
            <td>
                <asp:TextBox ID="txtMedica" runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td>Pláticas o conferencias</td>
            <td>
                <asp:TextBox ID="txtPlaticas" runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td>Atención psicológica</td>
            <td>
                <asp:TextBox ID="txtPsicologica" runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td>Servicios de apoyo externo</td>
            <td>
                <asp:TextBox ID="txtExterno" runat="server" Width="100px" />
            </td>
        </tr>
    </table>
</div>



  
   
  <asp:GridView 
    ID="gvAlumnos"
    runat="server"
    AutoGenerateColumns="False"
    CellPadding="0"
    CellSpacing="0"
    GridLines="None"
    style="font-size:1px; border-collapse:collapse; margin:0; padding:0;">

    <Columns>
       <asp:BoundField HeaderText="No. de Control" DataField="No_control" />
<asp:BoundField HeaderText="Nombre" DataField="NombreCompleto" />


        <asp:BoundField HeaderText="Entrevista1" DataField="Entrevista1" />
        <asp:BoundField HeaderText="Entrevista2" DataField="Entrevista2" />
        <asp:BoundField HeaderText="Entrevista3" DataField="Entrevista3" />

        <asp:BoundField HeaderText="A" DataField="A" />
        <asp:BoundField HeaderText="B" DataField="B" />

        <asp:BoundField HeaderText="cal1" DataField="cal1" />
        <asp:BoundField HeaderText="cal2" DataField="cal2" />
        <asp:BoundField HeaderText="cal3" DataField="cal3" />
        <asp:BoundField HeaderText="cal4" DataField="cal4" />
        <asp:BoundField HeaderText="cal5" DataField="cal5" />
        <asp:BoundField HeaderText="cal6" DataField="cal6" />

        <asp:BoundField HeaderText="Promedio" DataField="Promedio" />
        <asp:BoundField HeaderText="D" DataField="D" />
        <asp:BoundField HeaderText="N" DataField="N" />
        <asp:BoundField HeaderText="I" DataField="I" />
        <asp:BoundField HeaderText="R" DataField="R" />
    </Columns>
</asp:GridView>


    <div style="margin-top:100px;"></div>

    <h8><b><i><ins>2.- Cumplimiento a la tutoría y Desempeño académico. </ins></i></b></h8>
    <br />

    
       <Columns>
        <table class="table-editable" border="1" style="width: 100%; border-collapse: collapse;">
<thead>
    <tr>
        <td style="width:130px; background-color:lightgray; border-block-end:1px solid lightgray;">
            <asp:Label>No. De Control</asp:Label>
        </td>
        <td style="width:270px; background-color:lightgray; border-block-end:1px solid lightgray; text-align:center;">
            <asp:Label>Nombre</asp:Label>
        </td>
        <td colspan="3" style="width:20px; background-color:lightgray; text-align:center;">
            <asp:Label>Asistencias</asp:Label>
        </td>
        <td style="width:60px; text-align:center;">
            <asp:Label>A</asp:Label>
        </td>
        <td style="width:60px; text-align:center;">
            <asp:Label>B</asp:Label>
        </td>
        <td colspan="6" style="width:100px; text-align:center;">
            <asp:Label>Materia(s) Aprobadas</asp:Label>
        </td>
        <td style="width:115px; text-align:center;">
            <asp:Label>Promedio</asp:Label>
        </td>
        <td colspan="4" style="width:10px; text-align:center;">
            <asp:Label>Situación Final</asp:Label>
        </td>
              
    </tr>
    <tr>
        <td style="border-left:1px solid black; border-right:1px solid black; border-block-start:1px solid lightgray; background-color:lightgray;"></td>
        <td style="border-left:1px solid black; border-right:1px solid black; border-block-start:1px solid lightgray; background-color:lightgray;"></td>
        <td colspan="1" style="background-color:lightgray;">1ra.</td>
        <td colspan="1" style="background-color:lightgray;">2da.</td>
        <td colspan="1" style="background-color:lightgray;">3ra.</td>
        <td></td>
        <td></td>
        <td colspan="1" style="text-align:center;">1</td>
        <td colspan="1" style="text-align:center;">2</td>
        <td colspan="1" style="text-align:center;">3</td>
        <td colspan="1" style="text-align:center;">4</td>
        <td colspan="1" style="text-align:center;">5</td>
        <td colspan="1" style="text-align:center;">6</td>
        <td style="width:115px;"></td>
        <td colspan="1" style="text-align:center;">D</td>
        <td colspan="1" style="text-align:center;">N</td>
        <td colspan="1" style="text-align:center;">I</td>
        <td colspan="1" style="text-align:center;">R</td>
        <td style="width:120px; text-align:center;">
            
        </td>
    </tr>
</thead>


                <tbody>

                    <asp:ScriptManager runat="server"></asp:ScriptManager>

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <asp:Repeater ID="rptAlumnos" runat="server">


                                <ItemTemplate>
                                    <tr style="position: center;">
                                        <!-- No. Control -->
                                        <td style="text-align: center; width: 130px;">
                                            <asp:Label Style="width: 115px;" ID="lblNoControl" runat="server" Text='<%# Eval("No_control") %>'></asp:Label>
                                        </td>

                                        <!-- Nombre -->
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("NombreCompleto") %>'></asp:Label>
                                        </td>

                                        <!-- Entrevistas -->
                                        <td style="text-align: center;">
                                            <asp:RadioButton ID="rbEntrevista1" runat="server" Checked='<%# Convert.ToBoolean(Eval("Entrevista1")) %>' Enabled="false" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:RadioButton ID="rbEntrevista2" runat="server" Checked='<%# Convert.ToBoolean(Eval("Entrevista2")) %>' Enabled="false" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:RadioButton ID="rbEntrevista3" runat="server" Checked='<%# Convert.ToBoolean(Eval("Entrevista3")) %>' Enabled="false" />
                                        </td>

                                        <!-- A y B -->
                                        <td style="text-align: center;">
                                            <asp:RadioButton ID="rbA" runat="server" Checked='<%# Eval("A").ToString() == "X" %>' Enabled="false" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:RadioButton ID="rbB" runat="server" Checked='<%# Eval("B").ToString() == "X" %>' Enabled="false" />
                                        </td>

                                        <!-- Materias Aprobadas (cal1-cal6) -->
                                        <td style="text-align: center;">
                                            <asp:HiddenField ID="hfCal1" runat="server" Value='<%# Eval("cal1") %>' />
                                            <asp:CheckBox ID="rbCal1" runat="server" Checked='<%# Eval("cal1") != DBNull.Value && Convert.ToBoolean(Eval("cal1")) %>'/>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:HiddenField ID="hfCal2" runat="server" Value='<%# Eval("cal2") %>' />
                                            <asp:CheckBox ID="rbCal2" runat="server" Checked='<%# Eval("cal2") != DBNull.Value && Convert.ToBoolean(Eval("cal2")) %>'/>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:HiddenField ID="hfCal3" runat="server" Value='<%# Eval("cal3") %>' />
                                            <asp:CheckBox ID="rbCal3" runat="server" Checked='<%# Eval("cal3") != DBNull.Value && Convert.ToBoolean(Eval("cal3")) %>'/>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:HiddenField ID="hfCal4" runat="server" Value='<%# Eval("cal4") %>' />
                                            <asp:CheckBox ID="rbCal4" runat="server" Checked='<%# Eval("cal4") != DBNull.Value && Convert.ToBoolean(Eval("cal4")) %>'/>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:HiddenField ID="hfCal5" runat="server" Value='<%# Eval("cal5") %>' />
                                            <asp:CheckBox ID="rbCal5" runat="server" Checked='<%# Eval("cal5") != DBNull.Value && Convert.ToBoolean(Eval("cal5")) %>'/>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:HiddenField ID="hfCal6" runat="server" Value='<%# Eval("cal6") %>' />
                                            <asp:CheckBox ID="rbCal6" runat="server" Checked='<%# Eval("cal6") != DBNull.Value && Convert.ToBoolean(Eval("cal6")) %>'/>
                                        </td>
                                         
                                        <!-- Promedio -->
                                        <td style="text-align: center; width: 115px;">
                                            <asp:Label ID="lblPromedio" runat="server" Text='<%# Eval("Promedio", "{0:0.00}") %>'></asp:Label>
                                        </td>

                                        <!-- Situación Final -->
                                        <td style="text-align: center;">
                                            <asp:RadioButton ID="rbD" runat="server" Checked='<%# Eval("D").ToString() == "X" %>' Enabled="false" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:RadioButton ID="rbN" runat="server" Checked='<%# Eval("N").ToString() == "X" %>' Enabled="false" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:RadioButton ID="rbI" runat="server" Checked='<%# Eval("I").ToString() == "X" %>' Enabled="false" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:RadioButton ID="rbR" runat="server" Checked='<%# Eval("R").ToString() == "X" %>' Enabled="false" />
                                        </td>

                                       
                                       
                                           
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                                
                                 

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </tbody>
            </table>
            <div style="width: 15px; margin-left:1260px;" >
                <asp:ImageButton
                    CssClass="rounded-start no-print "
                    Width="17px"
                    OnClick="btnGuardarTodo_Click"
                    runat="server"
                    ImageUrl="https://cdn-icons-png.flaticon.com/512/747/747439.png" />

                <asp:ImageButton
                    CssClass="rounded-start no-print"
                    Width="17px"
                    OnClick="btnGenerarPDF_Click"
                    runat="server"
                    ImageUrl="https://cdn-icons-png.flaticon.com/512/446/446991.png" />

           
            </div>
            

         <div class="text-center" style="margin-top: 100px;">
    <h8>° Materias reprobadas. Anotar las materias en el orden de la retícula de la carrera de __________________ para primer semestre son:</h8>
</div>
<br />

<table style="width: 1200px; margin-left: 50px; border:1px solid black; border-collapse:collapse; text-align:center;">
    <tr>
        <th>-1</th>
        <th>-2</th>
        <th>-3</th>
        <th>-4</th>
        <th>-5</th>
        <th>-6</th>
        <th>-7</th>
    </tr>

    <!-- FILA: NOMBRE DE LA MATERIA -->
    <tr>
        <td><asp:TextBox ID="txtMateria1" runat="server" Width="150px" /></td>
        <td><asp:TextBox ID="txtMateria2" runat="server" Width="150px" /></td>
        <td><asp:TextBox ID="txtMateria3" runat="server" Width="150px" /></td>
        <td><asp:TextBox ID="txtMateria4" runat="server" Width="150px" /></td>
        <td><asp:TextBox ID="txtMateria5" runat="server" Width="150px" /></td>
        <td><asp:TextBox ID="txtMateria6" runat="server" Width="150px" /></td>
        <td><asp:TextBox ID="txtMateria7" runat="server" Width="150px" /></td>
    </tr>

  
</table>

      
    </table>
            <br />
            <div style="margin-left: 185px;">
                <h8>° <b><ins>A</ins></b> Cumplió cabalmente con las citas establecidas en el semestre.</h8>
                <br />
                <h8>° <b><ins>B</ins></b> No Cumplió con las citas establecidas en el semestre.</h8>
                <br />
                <br />
                <h8>* <b><ins>Situación Final</ins></b></h8>
                <br />
                <h8>•D = Estudiante Destacado: Ninguna materia reprobada y promedio mayor a 90.</h8>
                <br />
                <h8>•N = Estudiante Regular: Ninguna materia reprobada y promedio menor a 90.</h8>
                <br />
                <h8>•I  = Estudiante Irregular: De 1 a 2 materias reprobadas..</h8>
                <br />
                <h8>•R = Estudiante en Riesgo: Más de 3 o más materias reprobadas.</h8>
                <br />
                <br />
                <h8><p>NOTA: Informar a los estudiantes desde el inicio del semestre que deban entregar calificaciones finales y haber asistido a las tres entrevistas, <br /> sólo así al final de los 3 semestres de tutoría se les liberarán los créditos correspondientes.</p></h8>
                <br />
                <h8><b><ins>PROGRAMA*.</ins></b></h8>

                
            <table style="width: 800px;">
    <tr>
        <th style="width:155px; text-align:center; background-color:lightgray;">
            <b>Sesión número</b>
        </th>
        <th style="width:400px; background-color:lightgray;">
            <b>Tema por tratar</b>
        </th>
        <th style="width:155px; background-color:lightgray;">
            <b>Fecha de realización</b>
        </th>
    </tr>

    <tr style="text-align:center;">
        <td>0</td>
        <td>Presentación del docente</td>
        <td>
            <asp:TextBox ID="txtFecha0" runat="server" Width="140px" />
        </td>
    </tr>

    <tr style="text-align:center;">
        <td>1</td>
        <td>Actividad de entrevista 1</td>
        <td>
            <asp:TextBox ID="txtFecha1" runat="server" Width="140px" />
        </td>
    </tr>

    <tr style="text-align:center;">
        <td>2</td>
        <td>Actividad de entrevista 2</td>
        <td>
            <asp:TextBox ID="txtFecha2" runat="server" Width="140px" />
        </td>
    </tr>

    <tr style="text-align:center;">
        <td>3</td>
        <td>Actividad de entrevista 3</td>
        <td>
            <asp:TextBox ID="txtFecha3" runat="server" Width="140px" />
        </td>
    </tr>

    <tr style="text-align:center;">
        <td>4</td>
        <td>Cierre y conclusiones</td>
        <td>
            <asp:TextBox ID="txtFecha4" runat="server" Width="140px" />
        </td>
    </tr>
</table>



                
            </div>

            <div style="margin-left: 185px; margin-top:50px;">
                <p>* Durante el semestre, el tutor debe atender en tutoría individual a aquellos/as estudiantes que requieren seguimiento especial por una situación académica / personal que estén viviendo o que solicitan atención.</p>
                
                <p>** Los temas a tratar en tutoría se eligen de común acuerdo con las necesidades actuales del grupo de estudiantes a tutorar, se puede tomar como <br /> base el catálogo de temas que se incluye al final del documento</p>
               
                <p>*** Las sesiones 1, 2 y 3 deben ser una por mes.</p>

                <h7 style="margin-left:200px;"><b>CATÁLOGO DE TEMAS QUE SE PUEDEN TRATAR EN TUTORÍA</b></h7>
                <br />

                <p><b>Antecedentes.</b><br /> 
                    Los temas para tratar en tutoría deben estar enfocados a brindar elementos para incidir en los estudiantes tutorados/as <br /> en los siguientes ámbitos:  integración entre los alumnos y la dinámica de la Institución, seguimiento del proceso <br /> académico de los alumnos, convivencia en el aula y en la escuela y orientación hacia un proyecto de vida.
                </p>
                <br />

                  <h8><b>Sugerencia de temas.</b></h8>
                <table  style="width: 820px; font-size:16px;" >
                    <tr>
                        <th style="width:200px; text-align:center; background-color:lightgray;"><b>Temas académicos</b></th>
                        <th style="width:200px; background-color:lightgray;">-<b>Temas de formación integral</b></th>
                        <th style="width:200px; background-color:lightgray;">-<b></b>Temas sociales</th>
                    </tr>
                    <tr >
                        <td>                       
                           <ul>
                               <li>Riesgos del consumo de drogas</li>
                               <li>Prevención del acoso</li>
                               <li>Seguridad cibernética (peligros de las redes sociales)</li>
                               <li>Valoración de las tradiciones culturales</li>
                               <li>Importancia de los derechos humanos</li>
                               <li>Prevención de la discriminación</li>
                           </ul>
                        </td>
                        <td>
                            <ul>
                                <li>Plan de vida</li>
                                <li>Línea de vida</li>
                                <li>Análisis de fortalezas y dificultades</li> 
                                <li>Trabajo en equipo</li> 
                                <li>Igualdad de género</li>
                                <li>Violencia familiar</li>
                                <li>Enfermedades de transmisión sexual y su prevención</li>
                            </ul>
                        </td>
                        <td>
                            <ul>
                                <li>Riesgos del consumo de drogas</li>
                                <li>Prevención del acoso</li>
                                <li>Seguridad cibernética (peligros de las redes sociales)</li>
                                <li>Valoración de las tradiciones culturales</li>
                                <li>Importancia de los derechos humanos</li>
                                <li>Prevención de la discriminación</li>
                            </ul>
                        </td>
                    </tr>
                </table>
                <br />

                <h8>Para tratar estos temas, el tutor puede:</h8><br />
                <ul>
                    <li>Usar apoyos como videos documentales, películas o noticias.</li>
                    <li>Formar el debate en el aula.</li>
                    <li>Programar la vista de un experto en el tema.</li>
                    <li>Proponer actividades de autococimiento.</li>
                </ul> <br />

                <h8><b>Notas importantes.</b></h8>
                <p>
                    Este catálogo de temas no es único, puede sufrir cambios, de acuerdo con las necesidades actuales de los/las estudiantes <br /> 
                    o nuevas tendencias en el ámbito educativo / social / profesional. Únicamente sirve como guía para los temas a elegir <br /> 
                    para el Plan de Acción Tutorial (PAT).
                </p>
            </div> <br /> 

           <table style="font-size:13px; width:85%; text-align:center;">
    <tbody>
        <tr>
            <td style="width:180px;">
                No. de Tutorados <br /> asignados en el <br /> semestre.
            </td>
            <td style="width:180px;">
                No. de Tutorados <br /> asignados que ya habían <br /> liberado tutoría.
            </td>
            <td style="width:180px;">
                No. de Tutorados <br /> que han desertado.
            </td>
            <td style="width:180px;">
                No. de Tutorados <br /> que continúan.
            </td>
            <td style="width:180px;">
                % de Deserción.
            </td>
            <td colspan="4">
                % de Reprobación
            </td>
        </tr>

        <!-- CAMPOS -->
        <tr style="height:50px;">
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="80px" />
            </td>
            <td>
                <asp:TextBox ID="txtLiberados" runat="server" Width="80px" />
            </td>
            <td>
                <asp:TextBox ID="txtDesertados" runat="server" Width="80px" />
            </td>
            <td>
                <asp:TextBox ID="txtActivos" runat="server" Width="80px" />
            </td>
            <td>
                <asp:TextBox ID="txtPorcentaje" runat="server" Width="80px" />
            </td>

            <!-- D N I R -->
            <td>D<br />
                <asp:CheckBox ID="chkD" runat="server" />
            </td>
            <td>N<br />
                <asp:CheckBox ID="chkN" runat="server" />
            </td>
            <td>I<br />
                <asp:CheckBox ID="chkI" runat="server" />
            </td>
            <td>R<br />
                <asp:CheckBox ID="chkR" runat="server" />
            </td>
        </tr>
    </tbody>
</table>
<%--  --%>
                

               <table style="width:85%;">
    <tr style="text-align:center;">
        <td><b>Observaciones</b></td>
    </tr>
    <tr>
        <td>
            <asp:TextBox 
                ID="txtObservaciones"
                runat="server"
                TextMode="MultiLine"
                Rows="5"
                Width="100%" />
        </td>
    </tr>
</table>
<br /><br />


             <p style="margin-top:25px; margin-bottom:10px;">
    Favor de llenar el siguiente cuadro, con los datos de los alumnos en situación especial.
</p>

<table style="width:85%; text-align:center; border-collapse:collapse;">
    <tr>
        <th style="border:1px solid black;">No. de control</th>
        <th style="border:1px solid black;">Nombre del alumno</th>
        <th style="border:1px solid black;">
            Situación académica / acciones / motivos
        </th>
    </tr>

    <!-- FILA 1 -->
    <tr>
        <td style="border:1px solid black;">
            <asp:TextBox ID="txtCtrl1" runat="server" Width="120px" />
        </td>
        <td style="border:1px solid black;">
            <asp:TextBox ID="txtNom1" runat="server" Width="200px" />
        </td>
        <td style="border:1px solid black;">
            <asp:DropDownList ID="ddlSit1" runat="server" Width="260px">
                <asp:ListItem Text="-- Seleccione --" Value="" />
                <asp:ListItem Text="Baja temporal" />
                <asp:ListItem Text="Embarazo/paternidad" />
                <asp:ListItem Text="Necesidad de trabajar" />
                <asp:ListItem Text="No localizado" />
                <asp:ListItem Text="Reprobacion recurrente" />
                <asp:ListItem Text="Problemas familiares" />
                <asp:ListItem Text="Otra" />

            </asp:DropDownList>
        </td>
    </tr>

    <!-- FILA 2 -->
    <tr>
        <td style="border:1px solid black;">
            <asp:TextBox ID="txtCtrl2" runat="server" Width="120px" />
        </td>
        <td style="border:1px solid black;">
            <asp:TextBox ID="txtNom2" runat="server" Width="200px" />
        </td>
        <td style="border:1px solid black;">
            <asp:DropDownList ID="ddlSit2" runat="server" Width="260px">
                <asp:ListItem Text="-- Seleccione --" Value="" />
                <asp:ListItem Text="Baja temporal" />
 <asp:ListItem Text="Embarazo/paternidad" />
 <asp:ListItem Text="Necesidad de trabajar" />
 <asp:ListItem Text="No localizado" />
 <asp:ListItem Text="Reprobacion recurrente" />
 <asp:ListItem Text="Problemas familiares" />
 <asp:ListItem Text="Otra" />
            </asp:DropDownList>
        </td>
    </tr>

    <!-- FILA 3 -->
    <tr>
        <td style="border:1px solid black;">
            <asp:TextBox ID="txtCtrl3" runat="server" Width="120px" />
        </td>
        <td style="border:1px solid black;">
            <asp:TextBox ID="txtNom3" runat="server" Width="200px" />
        </td>
        <td style="border:1px solid black;">
            <asp:DropDownList ID="ddlSit3" runat="server" Width="260px">
                <asp:ListItem Text="-- Seleccione --" Value="" />
                <asp:ListItem Text="Baja temporal" />
 <asp:ListItem Text="Embarazo/paternidad" />
 <asp:ListItem Text="Necesidad de trabajar" />
 <asp:ListItem Text="No localizado" />
 <asp:ListItem Text="Reprobacion recurrente" />
 <asp:ListItem Text="Problemas familiares" />
 <asp:ListItem Text="Otra" />
            </asp:DropDownList>
        </td>
    </tr>
</table>

            <div style="position: absolute; margin-left:530px; margin-top:80px; text-align:center;">
                <h8 style="position: center;">_____________________________________</h8><br />
                <h8><b>Nombre y firma del tutor</b></h8>
            </div>

            <div style="margin-top:200px; width: 100%;">
                <asp:Image Style="width: 140px; margin-left: 170px;" runat="server" ImageUrl="/PDF/Recursos/ITALogo.png" />

                <div style="position: absolute; margin-left: 520px; margin-top:-55px;">
                    <asp:Image Style="width: 35px;" runat="server" ImageUrl="/PDF/Recursos/Logo1.png" />
                    <asp:Image Style="width: 35px; margin-left:10px;" runat="server" ImageUrl="/PDF/Recursos/Logo2.png" />
                    <asp:Image Style="width: 35px; margin-left:10px;" runat="server" ImageUrl="/PDF/Recursos/Logo3.png" />
                    <asp:Image Style="width: 35px; margin-left:10px;" runat="server" ImageUrl="/PDF/Recursos/Logo4.png" />
                    <asp:Image Style="width: 35px; margin-left:10px;" runat="server" ImageUrl="/PDF/Recursos/Logo5.png" />
                    <asp:Image Style="width: 35px; margin-left:10px;" runat="server" ImageUrl="/PDF/Recursos/Logo6.png" />
                    <asp:Image Style="width: 35px; margin-left:10px;" runat="server" ImageUrl="/PDF/Recursos/Logo7.png" />
                    <asp:Image Style="width: 35px; margin-left:10px;" runat="server" ImageUrl="/PDF/Recursos/Logo8.png" />
                    <asp:Image Style="width: 35px; margin-left:10px;" runat="server" ImageUrl="/PDF/Recursos/Logo9.png" />
                </div>
                <br /><br />
            </div>

        </columns>






    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('input[type="RadioButton"]').click(function () {
                if ($(this).data('clicked')) {
                    $(this).removeAttr('checked');
                    $(this).data('clicked', false);
                } else {
                    $(this).data('clicked', true);
                }
            });
        });.aspx?grupoID=' + grupoID;
            return false; // evita postback
        };


        //PARA NO DETENER EL PROGRAMAR
   

    </script>

    <style>
        .table, tr{
            border:1px solid black; 
            width:500px;
        }
        .table, tr, th, td {
            border: 1px solid black;
        }
        .table, tr, th, td {
            border: 1px solid black;
        }
        .radio-style {
            appearance: radio !important; /* Fuerza el aspecto de RadioButton */
        }
        @media print {
            .no-print {
                display: none !important;
            }
        }

    </style>


</asp:Content>