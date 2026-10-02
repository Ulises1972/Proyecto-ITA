<%@ Page Title="" Language="C#" MasterPageFile="~/Minimal.master" AutoEventWireup="true" CodeBehind="CertificadoAlumno.aspx.cs" Inherits="TutoriasWeb.CertificadoAlumno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Encabezado -->
    <div style="margin-top:35px;  width:100%;"  >
        <asp:Image  Style="width:200px; margin-left:100px;" runat="server" ImageUrl="/PDF/Recursos/EducacionTec.jpeg"/>

        <div style="position:absolute; margin-left:950px; margin-top:-20px;">
        <label style="font-style:revert; right:500px;"><b>Instituto Tecnológico de Aguascalientes</b></label>
        <label style="font-style:revert; right:500px;"><p>Departamento de Vinculación</p></label>
        </div>   
    </div>

    <div class="text-left" style="font-style:;" >
        <h5>(NOMBRE DE JEFE)</h5>
        <br />
        <h7><b>JEDE DEL DEPARTAMENTO DE VINCULACIÓN</b></h7>
        <h7><b>PRESENTE</b></h7>
        <br />
        <h5>At'n, Ags., (NOMBRE DE ENCRGAD@ DE OFICINA)</h5>
        <br />
        <h5><i>NOMBRE DE ENCARGAD@</i></h5>
        <br />
        <h8><b>Situación Académica de los alumnos de __________________ Tutoria de la Carrera __________________</b></h8>
        <br />
        <br />
        <h8>Nombre de Tutor _________________________________________________________ Fecha de Informe: _________________________</h8>
        <br />
        <br />
        <h8>No. De alumnos asignados. __________________ No. de alumnos atendidos. __________________ Semestre: __________________ </h8>   
    </div>
    
    <div style="margin-top:30px; margin-left:225px;">
        <h8><b><i><ins>1.- Resultados especificos</ins></i></b></h8>
        <br />
        <br />
        <h8 style="margin-left:50px;">No. de alumnos canalizados a:</h8>
        <br />
        <table>
            <tbody>
                <tr>
                    <td style="width:300px;">Círculos de estudio</td>
                    <td><div style="width:100px; border-left:1px solid black; color:white;">AAAAAA</div></td>
                </tr>
                <tr>
                    <td>Atención médica</td>
                    <td><div style="width:100px; border-left:1px solid black; color:white;">AAAAAA</div></td>
                </tr>
                <tr>
                    <td>Platicas o conferencias</td>
                    <td><div style="width:100px; border-left:1px solid black; color:white;">AAAAAA</div></td>
                </tr>
                <tr>
                    <td>Atención psicológica</td>
                    <td><div style="width:100px; border-left:1px solid black; color:white;">AAAAAA</div></td>
                </tr>
                <tr>
                    <td>Servicios de apoyo externo</td>
                    <td><div style="width:100px; border-left:1px solid black; color:white;">AAAAAA</div></td>
                </tr>
            </tbody>
        </table>
    </div>


  
   
    <asp:GridView style="font-size:1px;" ID="gvAlumnos" runat="server" AutoGenerateColumns="False">
        <Columns >
            <asp:BoundField HeaderText="No. de Control" DataField="NoControl" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
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
                <asp:Label>Entrevistas</asp:Label>
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
            <td style="width:120px; text-align:center;">
                <asp:Label>Observaciones<br />o causas de<br />reprobación y<br />deserción.</asp:Label>
            </td>        </tr>
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
            <td style="width: 120px;"></td>
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

                                        <!-- Observaciones -->
                                        <td style="text-align: center; width: 130px;">
                                            <asp:Label Style="width: 120px;" ID="lblComentarios" runat="server" Text='<%# Eval("Comentarios") != null ? Eval("Comentarios").ToString() : "" %>'></asp:Label>
                                        </td>
                                       
                                           
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                                
                                 

                        </ContentTemplate>
                        <Triggers>
    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
</Triggers>

                    </asp:UpdatePanel>

                </tbody>
            </table>
       
            

            <div class="text-center" style="margin-top: 100px;">
                <h8>° Materias reprobadas. Anotar las materias en el orden de la retícula de la carrera de . __________________ para primer semestre son:</h8>
            </div>
            <br />
            <table  style="width: 1200px; margin-left: 50px; border:1px solid black;">
                <tr >
                    <th>-1</th>
                    <th>-2</th>
                    <th>-3</th>
                    <th>-4</th>
                    <th>-5</th>
                    <th>-6</th>
                    <th>-7</th>
                </tr>
      
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

                
                <table  style="width: 800px;" >
                    <tr>
                        <th style="width:155px; text-align:center; background-color:lightgray;"><b>Sesión número</b></th>
                        <th style="width:400px; background-color:lightgray;">-<b>Tema por tratar**</b></th>
                        <th style="width:155px; background-color:lightgray;">-<b></b>Fecha de realización***</th>
                    </tr>
                    <tr style="text-align:center;">
                        <td>0</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr style="text-align:center;">
                        <td>1</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr style="text-align:center;">
                        <td>2</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr style="text-align:center;">
                        <td>3</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr style="text-align:center;">
                        <td>4</td>
                        <td></td>
                        <td></td>
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

            <div style="margin-left: 185px;">
                <h8>Favor de llenar los siguientes recuadros, que apliquen.</h8>
                <br /><br />

                <table style="font-size:13px; width:85%; text-align:center;">
                    <tbody>
                        <tr>
                            <td style="width:180px;">No. de Tutorados <br /> asignados en el <br /> semestre.</td>
                            <td style="width:180px;">No. de Tutorados <br /> asignados que ya habian <br /> liberado tutoria <br /> anteriormente.</td>
                            <td style="width:180px;">No. de Tutorados <br /> que han desertado en el<br /> semestre</td>
                            <td style="width:180px;">No. de Tutorados <br /> que continúan a la <br /> fecha.</td>
                            <td style="width: 180px;">% de Deserción.</td>
                            <td colspan="6" style="width:180px;">% de Reprobación<br /> -Considerar la situación final</td>
                        </tr>
                        <tr style="height: 50px;">
                            <td colspan="1" style="border-block-end: 1px solid white;"></td>
                            <td colspan="1" style="border-block-end: 1px solid white;"></td>
                            <td colspan="1" style="border-block-end: 1px solid white;"></td>
                            <td colspan="1" style="border-block-end: 1px solid white;"></td>
                            <td colspan="1" style="border-block-end: 1px solid white;"></td>
                            <td colspan="6" style="width:180px;"></td>
                        </tr>
                        <tr style="height: 50px; text-align: center;">
                            <td colspan="1" style="border-block-start: 1px solid white;"></td>
                            <td colspan="1" style="border-block-start: 1px solid white;"></td>
                            <td colspan="1" style="border-block-start: 1px solid white;"></td>
                            <td colspan="1" style="border-block-start: 1px solid white;"></td>
                            <td colspan="1" style="border-block-start: 1px solid white;"></td>
                            <td style="width:45px;">D</td>
                            <td style="width:45px;">N</td>
                            <td style="width:45px;">I</td>
                            <td style="width:45px;">R</td>
                        </tr>
                    </tbody>
                </table><br /><br />
                

                <table style="width:85%;">
                    <tr style="text-align:center;">
                        <td><b>Observaciones</b></td>
                    </tr>
                    <tr>
                        <td style="height:80px;"></td>
                    </tr>
                </table><br /><br />

                <h8>Favor de llenar el siguiente cuadro, con los datos de los alumnos en situación especial.</h8><br />

                <table style="width:85%; text-align:center;">
                    <tr style="height:80px;">
                        <td style="width:80px;">No. de control</td>
                        <td style="width:135px;">Nombre del alumno</td>
                        <td style="width:180px; ">Situación académica actual, acciones realizadas en su apoyo y <br /> motivos de deserción.</td>
                    </tr>
                    <tr style="height:25px;">
                        <td style="width:80px;"></td>
                        <td style="width:135px;"></td>
                        <td style="width:180px;"></td>
                    </tr>
                    <tr style="height:25px;">
                        <td style="width:80px;"></td>
                        <td style="width:135px;"></td>
                        <td style="width:180px;"></td>
                    </tr>
                    <tr style="height:25px;">
                        <td style="width:80px;"></td>
                        <td style="width:135px;"></td>
                        <td style="width:180px;"></td>
                    </tr>
                </table>               
            </div>

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
