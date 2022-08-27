using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace TutoriasWeb
{
    public class Metodos
    {
        SqlConnection cnn = new SqlConnection("server=DESKTOP-1I96JTK\\SQLEXPRESS ; database=Tutoria ; integrated security = true");
        SqlCommand cmd = new SqlCommand();
        SqlTransaction tr;
        SqlDataReader dr;
        DataTable dt = new DataTable();

        public bool AlumnoCalificar(string Estatus, int Tutoria, int Semestre, int NoControl, int Promedio)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE ALUMNO SET Estatus='" + Estatus.ToUpper() + "', Tutoria='" + Tutoria + "', Semestre='" + (Semestre + 1) + 
                                "', Promedio=" + Promedio + " where No_contro=" + NoControl;
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }                

            }catch(Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool GrupoComUpdateSeguimiento(string cal1, string cal2, string cal3, string cal4, string cal5, string cal6,  int NoControl)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Grupo_Compuesto SET Cal1='" + cal1 + "', Cal2='" + cal2 + "', Cal3='" + cal3 + "', Cal4='" + cal4 + 
                                "', Cal5='" + cal5 + "', Cal6='" + cal6 + "' where No_control=" + NoControl + " and Estatus='En Curso'";
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool GrupoComUpdateSemestre(int Semestre, string Estatus, string ID)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Grupo_Compuesto SET Semestre=" + Semestre + ", Estatus='" + Estatus + "'  where ID=" + ID;
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool GrupoComUpdateCalculos(int Promedio, string A, string B, string D, string N, string I, string R, string Id)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Grupo_Compuesto SET A='" + A + "', B='" + B + "', D='" + D +
                                "', N='" + N + "', I='" + I + "', R='" + R + "', Promedio=" + Promedio + " where ID=" + Id;
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool GrupoComUpdateEjecucion(string Asistencia1, string Asistencia2, string Asistencia3, bool CirculoEstudio, bool A_Medica, bool Platicas, bool A_Psicologico, bool A_Externo, string Comentarios, string Id)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Grupo_Compuesto SET Asistencia1='" + Asistencia1 + "', Asistencia2='" + Asistencia2 + "', Asistencia3='" + 
                                Asistencia3 + "', Circulo_Estudio='" + CirculoEstudio + "', Atencion_Medica='" + A_Medica + "', Platicas='" + 
                                Platicas + "', Psicologica='" + A_Psicologico + "', Apoyo_Externo='" + A_Externo + "', Comentarios='" + Comentarios + "' where ID=" + Id;
            try
            {
                
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool GrupoUpdateEstatus(string Estatus, string Id)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Grupo SET Estatus='" + Estatus.ToUpper() + "' where ID=" + Id;
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool MateriaInsert(string Nombre, string NombreCorto, string Semetre, string Carrera)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "INSERT INTO Materia(Nombre, Nombre_Corto, Semestre, Carrera, Estatus) values('" + Nombre.ToUpper() + "','" + 
                NombreCorto.ToUpper() + "'," + Semetre + ",'" + Carrera + "', 'ACTIVO')";
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool MateriaUpdate(string Nombre, string NombreCorto, string Semestre, string ID)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Materia set Nombre='" + Nombre.ToUpper() + "', Nombre_Corto='" + NombreCorto.ToUpper() + "', semestre=" + Semestre + 
                                 " where ID=" + ID; 
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool MateriaDelete(string ID)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Materia set Estatus='ELIMINADO' where ID=" + ID;
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable MateriaSelect(string Carrera)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "Select * from Materia where Estatus = 'ACTIVO' AND Carrera='" + Carrera + "'" ;
            try
            {
                dt = new DataTable();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable MateriaGetDataById(string ID)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "Select * from Materia where ID=" + ID + " and Estatus='ACTIVO'";
            try
            {
                dt = new DataTable();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable MateriaGetDataByIds(int[] ids)
        {
            dt = new DataTable();
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "Select ID, Nombre, Nombre_Corto from Materia where ID=" + ids[0] ;
            for(int i=1; i < ids.Count() ; i++)
            {
                cmd.CommandText += " or ID=" + ids[i] ;
            }
            cmd.CommandText += " and Estatus='ACTIVO'";
            try
            {
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool AlumnoSaveCals( string u1_1, string u1_2, string u2_1, string u2_2, string u3_1, string u3_2, string u4_1, string u4_2, string u5_1, string u5_2, string u6_1, string u6_2, string u7_1, string u7_2, string u8_1, string u8_2, string IDrelacion)
        {
            dt = new DataTable();
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = " update Alumno_Materia set u1_1='" + u1_1 + "', u1_2='" + u1_2 + "', u2_1='" + u2_1 + "', u2_2='" + u2_2 + "', u3_1='" +
                u3_1 + "', u3_2='" + u3_2 + "', u4_1='" + u4_1 + "', u4_2='" + u4_2 + "', u5_1='" + u5_1 + "', u5_2='" + u5_2 + "', u6_1='" + u6_1 + 
                "', u6_2='" + u6_2 + "', u7_1='" + u7_1 + "', u7_2='" + u7_2 + "', u8_1='" + u8_1 + "', u8_2='" + u8_2 + "' where id=" + IDrelacion;
            try
            {
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();                
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool CalculaPromedios(int NoControl)
        {
            dt = AlumnoGetCalsBySem(NoControl, 0);
            if(dt.Rows.Count > 0)
            {
                int[] promedio = new int[6];
                int cont;
                int cal;
                bool[] reprobada = new bool[6];
                bool[] baja = new bool[6];
                int op;
                for(int i=0; i < dt.Rows.Count; i++)
                {
                    cont = 0;
                    promedio[i] = 0;
                    reprobada[i] = false;
                    baja[i] = false;
                    op = 1;
                    for (int j=0; j < 8; j++)
                    {
                        cal = toInt(dt.Rows[i]["u"+(j+1) +"_"+op].ToString());
                        if (cal > 0)
                        {
                            promedio[i] += cal;
                            cont++;
                            op = 1;
                        }
                        else if(dt.Rows[i]["u" + (j + 1) + "_" + op].ToString() == "NA")
                        {
                            j--;                                                        
                            if (op == 2)
                            {
                                cont = 0;
                                promedio[i] = 0;
                                op = 1;
                                j = 8;
                                reprobada[i] = true;
                            }
                            op = 2;
                        }
                        else if (dt.Rows[i]["u" + (j + 1) + "_" + op].ToString() == "-")
                        {
                            baja[i] = true;
                            j = 8;
                            op = 1;
                        }
                        else
                        {
                            op = 1;
                        }
                    }
                    if(promedio[i] > 0)
                        promedio[i] /= cont;
                }
                cmd.Connection = cnn;
                cmd.Connection.Open();
                tr = cmd.Connection.BeginTransaction();
                cmd.Transaction = tr;
                string res;
                if (baja[0])
                    res = "-";
                else
                    res = reprobada[0] ? "NA" : promedio[0].ToString();

                cmd.CommandText = "update Grupo_Compuesto set Cal1='" + res +"'";                
                for (int k =1; k < 6; k++)
                {
                    if (baja[k])
                        res = "-";
                    else
                        res = reprobada[k] ? "NA" : promedio[k].ToString();
                    cmd.CommandText += ", Cal" + (k + 1) + "='" + res + "'";
                }
                cmd.CommandText += " where No_control =" + NoControl + " and Estatus='En Curso'";

                try
                {
                    int r = cmd.ExecuteNonQuery();
                    if (r != 1)
                    {
                        tr.Rollback();
                        return true;
                    }
                    else
                    {
                        tr.Commit();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return true;
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            else
                return true;
        }
        public DataTable AlumnoGetCalsBySem(int NoControl, int Semestre)//semestre sero para indicar semestre actual
        {
            dt = new DataTable();
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = " declare @semestre int="+Semestre+", @NoControl int="+NoControl+" Select a.ID, m.Nombre, m.Nombre_Corto," +
                "a.u1_1,a.u1_2,a.u2_1,a.u2_2,a.u3_1,a.u3_2,a.u4_1,a.u4_2,a.u5_1,a.u5_2,a.u6_1,a.u6_2,a.u7_1,a.u7_2,a.u8_1,a.u8_2," +
                "a.Promedio from Alumno_Materia a left join Materia m on m.ID = a.idMateria where a.No_control =@NoControl" +
                 " and a.Semestre = (case when isnull(@semestre, 0) = 0 then (select Semestre from Alumno where No_control=@NoControl) else @semestre end)";
            try
            {
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        

        public DataTable AlumnoGetCalsByMat(int IdMateria)
        {
            dt = new DataTable();
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = " Select (select Nombre from Materia where ID=" + IdMateria + ") Nombre, a.u1_1,a.u1_2,a.u2_1,a.u2_2,a.u3_1,a.u3_2,a.u4_1,a.u4_2,a.u5_1,a.u5_2,a.u6_1,a.u6_2" +
                ",a.u7_1,a.u7_2,a.u8_1,a.u8_2 from Alumno_Materia a where a.ID=" + IdMateria;
            try
            {
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool GrupoComInsertMat(int[] Materias, string No_Control)
        {

            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "declare @Semestre int = (select Semestre from Alumno where No_control = " + No_Control + ") INSERT into Alumno_Materia (No_control, Semestre, idMateria) values (" + No_Control + ", @Semestre, " + Materias[0] + ")";
            for(int i=1; i < Materias.Length; i++)
            {
                cmd.CommandText += ", (" + No_Control + ", @Semestre, " + Materias[i] + ")";
            }
            try
            {
                int r = cmd.ExecuteNonQuery();
                if (r != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable GrupoComGetMatByNo(string No_Control)
        {

            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "exec stp_MateriasAlumno_Select " + No_Control;

            try
            {
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
             {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool GrupoComConMat(string No_Control)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "select top 1 idMateria from alumno_Materia where No_control=" + No_Control + " and Semestre=(select Semestre From Alumno where No_control=" + No_Control + " and Estatus='En Curso')";

            try
            {
                int r = toInt(cmd.ExecuteScalar().ToString());
                if (r > 0)
                {                    
                    return true;
                }
                else
                {                    
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable MateriaGetDataByGrade(int Semestre, string Carrera)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "Select * from Materia where Semestre=" + Semestre + " and Estatus='ACTIVO' and Carrera='" + Carrera + "'";
            try
            {
                dt = new DataTable();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable AlumnoGetDataByNo(string NoControl)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "Select * from Alumno where No_control=" + NoControl;
            try
            {
                dt = new DataTable();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable GrupoComGetDataByID(string ID)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "Select * from Grupo_Compuesto where ID=" + ID ;
            try
            {
                dt = new DataTable();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable GrupoComGetDataByGroup(string Grupo)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "Select * from Grupo_Compuesto where ID_Grupo=" + Grupo;
            try
            {
                dt = new DataTable();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable GrupoCom_AlumnoGetDataByGroup(string idGrupo)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "select g.ID,g.No_control,a.A_Paterno,a.A_Materno,a.Nombre,g.Cal1,g.Cal2,g.Cal3,g.Cal4,g.Cal5,g.Cal6,Comentarios, g.Entrevista1, g.Entrevista2, g.Entrevista3, g.Asistencia1, g.Asistencia2, g.Asistencia3,g.A,g.B,g.D,g.N,g.I,g.R,g.Promedio from " +
                "Grupo_Compuesto g left join Alumno a on a.No_control = g.No_control where g.Estatus = 'En Curso' and g.ID_Grupo =" + idGrupo;
            try
            {
                dt = new DataTable();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable GrupoComGetCalsByNo(int NoControl)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "select g.No_control,a.A_Paterno,a.A_Materno,a.Nombre,g.Cal1,g.Cal2,g.Cal3,g.Cal4,g.Cal5,g.Cal6  from " +
                "Grupo_Compuesto g left join Alumno a on a.No_control = g.No_control where g.Estatus = 'En Curso' and g.No_control =" + NoControl;
            try
            {
                dt = new DataTable();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        public DataTable Reporte1GetInfo(int NoControl)//semestre sero para indicar semestre actual
        {
            dt = new DataTable();
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = "declare @NoControl int =" + NoControl +" select(a.Nombre + a.A_Paterno + a.A_Materno) NombreAlumno, (m.Nombre_Maestro + m.A_Paterno + m.A_Materno)" +
                " NombreMaestro, a.Carrera, a.Semestre, gc.Entrevista1, gc.Entrevista2, gc.Entrevista3, gc.Asistencia1, gc.Asistencia2," +
                "gc.Asistencia3, gc.Comentarios from Grupo_Compuesto gc LEFT JOIN Grupo g on g.ID = gc.ID_Grupo LEFT JOIN Maestro m on  " +
                "g.ID_Maestro = m.ID LEFT JOIN Alumno a on gc.No_control=a.No_control where gc.No_control=@NoControl and " +
                "gc.Estatus='En Curso' ";
            try
            {
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static int toInt(string numero)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(numero);
                return n;
            }
            catch
            {
                return 0;
            }

        }
        public static string toDate(string fecha)
        {
            string date;
            try
            {
                date = fecha.Substring(0,10);
                return date;
            }
            catch
            {
                return "";
            }

        }

        public static bool toBool(string value)
        {
            bool n = false;
            try
            {
                n = Convert.ToBoolean(value);
                return n;
            }
            catch
            {
                return false;
            }

        }

        protected string toTextNull(string value)
        {
                if (value == "") return "null"; 
                else return value;

        }

        public bool AlumnoCierreUpdate(string Estatus, int Semestre, int Tutoria, string NoControl)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Alumno set Estatus='" + Estatus + "', Semestre=" + Semestre + ", Tutoria=" + Tutoria + "WHERE No_control =" + NoControl;
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }



        public DataTable Reporte4aGetInfo(int IdGrupo)//semestre sero para indicar semestre actual
        {
            dt = new DataTable();
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = " declare @idGrupo int = " + IdGrupo
            +"select"
            +"(select count(Psicologica) from Grupo_Compuesto where ID_Grupo = @idGrupo and Psicologica = 1) psicologica"
        	+",(select count(Atencion_Medica) from Grupo_Compuesto where ID_Grupo = @idGrupo and Atencion_Medica = 1) AtencionMedica"
        	+",(select count(Circulo_Estudio) from Grupo_Compuesto where ID_Grupo = @idGrupo and Circulo_Estudio = 1) CirculoEstudio"
        	+",(select count(Platicas) from Grupo_Compuesto where ID_Grupo = @idGrupo and Platicas = 1) Platicas"
        	+",(select count(Apoyo_Externo) from Grupo_Compuesto where ID_Grupo = @idGrupo and Apoyo_Externo = 1) ApoyoExterno";

            try
            {
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public DataTable Reporte5GetInfo(int IdGrupo)//semestre sero para indicar semestre actual
        {
            dt = new DataTable();
            cmd.Connection = cnn;
            cmd.Connection.Open();
            cmd.CommandText = " declare @idGrupo int =" + IdGrupo.ToString() +
            " select(case when substring(g.Nombre, 8, 1) = 'A' then 'ENE-JUN' else 'AGO-DIC' end) periodo ,(case when substring(g.Nombre,"
            + "10,1) = '1' then 'Primer' when substring(g.Nombre, 10,1) = '2' then 'Segundo' else 'Tercer' end + ' semestre') semestre"	        
	        + ",(select count(No_control) from Grupo_Compuesto where ID_Grupo = @idGrupo ) alumnosAsignados, ('Grupo ' + substring(g.Nombre, 10, 1) +" 
            + "'°' + SUBSTRING(g.Nombre, 8, 1)) grupo, Convert(varchar(10), CURRENT_TIMESTAMP, 103) fecha,(select count(Circulo_Estudio) from"
            + " Grupo_Compuesto where ID_Grupo = @idGrupo and Circulo_Estudio = 1) as circuloEstudio,(select count(Atencion_Medica) from Grupo_Compuesto " 
            +"where ID_Grupo = @idGrupo and Atencion_Medica = 1) as atencionMedica,(select count(Platicas) from Grupo_Compuesto where ID_Grupo = "
            +"@idGrupo and Platicas = 1) as platicas,(select count(Psicologica) from Grupo_Compuesto where ID_Grupo = @idGrupo and Psicologica = 1) as " 
            +"psicologica,(select count(Apoyo_Externo) from Grupo_Compuesto where ID_Grupo = @idGrupo and Apoyo_Externo = 1) as apoyoExterno"
	        + ",gc.No_control,(a.A_Paterno + ' ' + a.A_Materno + ' ' + a.Nombre) alumno,gc.Entrevista1,gc.Entrevista2,gc.Entrevista3,gc.Asistencia1" +
            ",gc.Asistencia2,gc.Asistencia3,gc.A,gc.B,GC.Cal1,GC.Cal2,GC.Cal3,GC.Cal4,GC.Cal5,gc.Cal6,gc.promedio,gc.D,gc.N,gc.I,gc.R,gc.Comentarios "
            + "from Grupo_Compuesto gc  left join grupo g on g.id = gc.id_grupo left join Alumno a on a.No_control = gc.No_control where gc.ID_Grupo = @idGrupo";

            try
            {
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool AlumnoPasswordUpdate(string Clave, string NoControl)
        {
            cmd.Connection = cnn;
            cmd.Connection.Open();
            tr = cmd.Connection.BeginTransaction();
            cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Alumno set Clave='" + Clave + "' WHERE No_control =" + NoControl;
            try
            {
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tr.Rollback();
                    return true;
                }
                else
                {
                    tr.Commit();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static void reporte3(int NoControl)
        {
            Metodos mt = new Metodos();
            DataTable dt, cals;
            dt = mt.Reporte1GetInfo(NoControl);
            if (dt.Rows.Count > 0)
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();

                // Create an empty page
                PdfPage page = document.AddPage();

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"img\Logo.png");
                XImage image = XImage.FromFile(path);

                // Create a font
                XFont txt = new XFont("Times New Roman", 10, XFontStyle.Regular);
                XFont title = new XFont("Times New Roman", 13, XFontStyle.Bold);
                XFont titleB = new XFont("Times New Roman", 15, XFontStyle.Bold);
                XFont titleC = new XFont("Times New Roman", 13, XFontStyle.BoldItalic);
                XFont subtitle = new XFont("Times New Roman", 13, XFontStyle.Italic);

                XStringFormat format = new XStringFormat();

                gfx.DrawImage(image, 460, 35, 105, 70);

                // Draw the text
                gfx.DrawString("ANEXO 3", subtitle, XBrushes.Black, new XRect(1, 60, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("INSTITUTO TECNOLÓGICO DE AGUSCALIENTES", title, XBrushes.Black, new XRect(1, 75, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("DEPARTAMENTO DE DESARROLLO ACADEMICO", title, XBrushes.Black, new XRect(1, 90, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("CITAS DE TUTORIA Y REGISTRO DE CALIFICACIONES", titleC, XBrushes.Black, new XRect(1, 130, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawRectangle(XPens.Gray, XBrushes.LightGray, 190, 175, 215, 25);
                gfx.DrawString("CITAS DE TUTORIA", titleB, XBrushes.Black, new XRect(1, 180, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawString("Nombre del Tutor:", title, XBrushes.Black, new XRect(50, 225, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 155, 240, 540, 240);
                gfx.DrawString(dt.Rows[0]["NombreMaestro"].ToString(), txt, XBrushes.Black, new XRect(165, 225, page.Width, page.Height), format);
                gfx.DrawString("Nombre del Alumno:", title, XBrushes.Black, new XRect(50, 257, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 170, 272, 540, 272);
                gfx.DrawString(dt.Rows[0]["NombreAlumno"].ToString(), txt, XBrushes.Black, new XRect(180, 257, page.Width, page.Height), format);
                gfx.DrawString("Carrera:", title, XBrushes.Black, new XRect(50, 290, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 100, 305, 360, 305);
                gfx.DrawString(dt.Rows[0]["Carrera"].ToString(), txt, XBrushes.Black, new XRect(110, 290, page.Width, page.Height), format);
                gfx.DrawString("Semestre Actual:", title, XBrushes.Black, new XRect(380, 290, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 480, 305, 540, 305);
                gfx.DrawString(dt.Rows[0]["Semestre"].ToString(), txt, XBrushes.Black, new XRect(490, 290, page.Width, page.Height), format);

                gfx.DrawRectangle(XPens.Transparent, XBrushes.LightGray, 50, 325, 490, 30);
                gfx.DrawLine(XPens.Black, 50, 325, 540, 325);//horizontal1
                gfx.DrawLine(XPens.Black, 50, 340, 310, 340);//horizontal2
                gfx.DrawLine(XPens.Black, 50, 325, 50, 460);//vertical1 
                gfx.DrawLine(XPens.Black, 115, 340, 115, 460);//vertical2
                gfx.DrawLine(XPens.Black, 180, 325, 180, 460);//vertical3 
                gfx.DrawLine(XPens.Black, 245, 340, 245, 460);//vertical4
                gfx.DrawLine(XPens.Black, 310, 325, 310, 460);//vertical5 
                gfx.DrawLine(XPens.Black, 420, 325, 420, 460);//vertical6
                gfx.DrawLine(XPens.Black, 540, 325, 540, 460);//vertical7
                gfx.DrawLine(XPens.Black, 50, 355, 540, 355);//horizontal3
                gfx.DrawLine(XPens.Black, 50, 390, 540, 390);//horizontal4
                gfx.DrawLine(XPens.Black, 50, 425, 540, 425);//horizontal5
                gfx.DrawLine(XPens.Black, 50, 460, 540, 460);//horizontal6
                gfx.DrawString("Programadas", title, XBrushes.Black, new XRect(70, 325, page.Width, page.Height), format);
                gfx.DrawString("Fecha", title, XBrushes.Black, new XRect(65, 340, page.Width, page.Height), format);
                gfx.DrawString("Hora", title, XBrushes.Black, new XRect(130, 340, page.Width, page.Height), format);
                gfx.DrawString("Reales", title, XBrushes.Black, new XRect(220, 325, page.Width, page.Height), format);
                gfx.DrawString("Fecha", title, XBrushes.Black, new XRect(195, 340, page.Width, page.Height), format);
                gfx.DrawString("Hora", title, XBrushes.Black, new XRect(260, 340, page.Width, page.Height), format);
                gfx.DrawString("Firma del", title, XBrushes.Black, new XRect(330, 325, page.Width, page.Height), format);
                gfx.DrawString("Tutor", title, XBrushes.Black, new XRect(340, 340, page.Width, page.Height), format);
                gfx.DrawString("Firma del", title, XBrushes.Black, new XRect(450, 325, page.Width, page.Height), format);
                gfx.DrawString("Alumno", title, XBrushes.Black, new XRect(455, 340, page.Width, page.Height), format);

                gfx.DrawString(dt.Rows[0]["Entrevista1"].ToString().Substring(0, 10), txt, XBrushes.Black, new XRect(57, 365, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista2"].ToString().Substring(0, 10), txt, XBrushes.Black, new XRect(57, 400, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista3"].ToString().Substring(0, 10), txt, XBrushes.Black, new XRect(57, 435, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista1"].ToString().Substring(0, 10), txt, XBrushes.Black, new XRect(187, 365, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista2"].ToString().Substring(0, 10), txt, XBrushes.Black, new XRect(187, 400, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista3"].ToString().Substring(0, 10), txt, XBrushes.Black, new XRect(187, 435, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista1"].ToString().Substring(11, 13), txt, XBrushes.Black, new XRect(120, 365, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista2"].ToString().Substring(11, 13), txt, XBrushes.Black, new XRect(120, 400, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista3"].ToString().Substring(11, 13), txt, XBrushes.Black, new XRect(120, 435, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista1"].ToString().Substring(11, 13), txt, XBrushes.Black, new XRect(252, 365, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista2"].ToString().Substring(11, 13), txt, XBrushes.Black, new XRect(252, 400, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista3"].ToString().Substring(11, 13), txt, XBrushes.Black, new XRect(252, 435, page.Width, page.Height), format);

                gfx.DrawRectangle(XPens.Gray, XBrushes.LightGray, 140, 495, 320, 25);
                gfx.DrawString("SEGUIMIENTO DE CALIFICACIONES", titleB, XBrushes.Black, new XRect(1, 500, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawRectangle(XPens.Gray, XBrushes.LightGray, 50, 535, 495, 55);
                gfx.DrawString("MATERIA", title, XBrushes.Black, new XRect(80, 560, page.Width, page.Height), format);
                gfx.DrawString("UNIDADES", title, XBrushes.Black, new XRect(295, 540, page.Width, page.Height), format);
                gfx.DrawString("U1", title, XBrushes.Black, new XRect(187, 560, page.Width, page.Height), format);
                gfx.DrawString("1", title, XBrushes.Black, new XRect(180, 575, page.Width, page.Height), format);
                gfx.DrawString("2", title, XBrushes.Black, new XRect(200, 575, page.Width, page.Height), format);
                gfx.DrawString("U2", title, XBrushes.Black, new XRect(227, 560, page.Width, page.Height), format);
                gfx.DrawString("1", title, XBrushes.Black, new XRect(220, 575, page.Width, page.Height), format);
                gfx.DrawString("2", title, XBrushes.Black, new XRect(240, 575, page.Width, page.Height), format);
                gfx.DrawString("U3", title, XBrushes.Black, new XRect(267, 560, page.Width, page.Height), format);
                gfx.DrawString("1", title, XBrushes.Black, new XRect(260, 575, page.Width, page.Height), format);
                gfx.DrawString("2", title, XBrushes.Black, new XRect(280, 575, page.Width, page.Height), format);
                gfx.DrawString("U4", title, XBrushes.Black, new XRect(307, 560, page.Width, page.Height), format);
                gfx.DrawString("1", title, XBrushes.Black, new XRect(300, 575, page.Width, page.Height), format);
                gfx.DrawString("2", title, XBrushes.Black, new XRect(320, 575, page.Width, page.Height), format);
                gfx.DrawString("U5", title, XBrushes.Black, new XRect(347, 560, page.Width, page.Height), format);
                gfx.DrawString("1", title, XBrushes.Black, new XRect(340, 575, page.Width, page.Height), format);
                gfx.DrawString("2", title, XBrushes.Black, new XRect(360, 575, page.Width, page.Height), format);
                gfx.DrawString("U6", title, XBrushes.Black, new XRect(387, 560, page.Width, page.Height), format);
                gfx.DrawString("1", title, XBrushes.Black, new XRect(380, 575, page.Width, page.Height), format);
                gfx.DrawString("2", title, XBrushes.Black, new XRect(400, 575, page.Width, page.Height), format);
                gfx.DrawString("U7", title, XBrushes.Black, new XRect(427, 560, page.Width, page.Height), format);
                gfx.DrawString("1", title, XBrushes.Black, new XRect(420, 575, page.Width, page.Height), format);
                gfx.DrawString("2", title, XBrushes.Black, new XRect(440, 575, page.Width, page.Height), format);
                gfx.DrawString("U8", title, XBrushes.Black, new XRect(467, 560, page.Width, page.Height), format);
                gfx.DrawString("1", title, XBrushes.Black, new XRect(460, 575, page.Width, page.Height), format);
                gfx.DrawString("2", title, XBrushes.Black, new XRect(480, 575, page.Width, page.Height), format);
                gfx.DrawString("CALIF", title, XBrushes.Black, new XRect(500, 560, page.Width, page.Height), format);
                gfx.DrawString("FINAL", title, XBrushes.Black, new XRect(500, 575, page.Width, page.Height), format);

                cals = mt.AlumnoGetCalsBySem(NoControl, 0);
                double y = 590, x = 177;

                if (cals.Rows.Count > 0)
                {
                    for (int i = 0; i < cals.Rows.Count; i++)
                    {
                        x = 177;
                        gfx.DrawString(cals.Rows[i]["Nombre"].ToString(), txt, XBrushes.Black, new XRect(60, y + 5, page.Width, page.Height), format);
                        for (int j = 3; j < 20; j++)
                        {
                            if (!string.IsNullOrEmpty(cals.Rows[i].ItemArray[j].ToString()))
                            {
                                gfx.DrawString(cals.Rows[i].ItemArray[j].ToString(), txt, XBrushes.Black, new XRect(x, y + 5, page.Width, page.Height), format);
                            }
                            x += 20;
                        }
                        y += 25;
                        gfx.DrawLine(XPens.Black, 50, y, 545, y);//horizontal5
                    }
                }


                gfx.DrawLine(XPens.Black, 50, 535, 545, 535);//horizontal1
                gfx.DrawLine(XPens.Black, 175, 560, 495, 560);//horizontal2
                gfx.DrawLine(XPens.Black, 175, 575, 495, 575);//horizontal3
                gfx.DrawLine(XPens.Black, 50, 590, 545, 590);//horizontal4
                gfx.DrawLine(XPens.Black, 50, 535, 50, y);//vertical1
                gfx.DrawLine(XPens.Black, 175, 535, 175, y);//vertical2
                gfx.DrawLine(XPens.Black, 195, 575, 195, y);//vertical3
                gfx.DrawLine(XPens.Black, 215, 560, 215, y);//vertical4
                gfx.DrawLine(XPens.Black, 235, 575, 235, y);//vertical5
                gfx.DrawLine(XPens.Black, 255, 560, 255, y);//vertical6
                gfx.DrawLine(XPens.Black, 275, 575, 275, y);//vertical7
                gfx.DrawLine(XPens.Black, 295, 560, 295, y);//vertical8
                gfx.DrawLine(XPens.Black, 315, 575, 315, y);//vertica9
                gfx.DrawLine(XPens.Black, 335, 560, 335, y);//vertical10
                gfx.DrawLine(XPens.Black, 355, 575, 355, y);//vertical11
                gfx.DrawLine(XPens.Black, 375, 560, 375, y);//vertical12
                gfx.DrawLine(XPens.Black, 395, 575, 395, y);//vertical13
                gfx.DrawLine(XPens.Black, 415, 560, 415, y);//vertical14
                gfx.DrawLine(XPens.Black, 435, 575, 435, y);//vertical15
                gfx.DrawLine(XPens.Black, 455, 560, 455, y);//vertical16
                gfx.DrawLine(XPens.Black, 475, 575, 475, y);//vertical17
                gfx.DrawLine(XPens.Black, 495, 535, 495, y);//vertical18
                gfx.DrawLine(XPens.Black, 545, 535, 545, y);//vertical19

                gfx.DrawString("OBSERVACIONES:", title, XBrushes.Black, new XRect(65, y + 20, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 185, y + 33, 545, y + 33);//horizontal1
                gfx.DrawString(dt.Rows[0]["Comentarios"].ToString(), txt, XBrushes.Black, new XRect(200, y + 20, page.Width, page.Height), format);

                gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), 100, 100, 100, 100);

                // Save the document...
                string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"PDF\") +NoControl  + ".pdf";
                document.Save(filename);
                // ...and start a viewer.
                Process.Start(filename);
            }
        }

        public static void Reporte4a(int NoControl, int idGrupo)
        {
            Metodos mt = new Metodos();
            DataTable dt, num;
            dt = mt.Reporte1GetInfo(NoControl);
            num = mt.Reporte4aGetInfo(idGrupo);

            if (dt.Rows.Count > 0)
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();

                // Create an empty page
                PdfPage page = document.AddPage();

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"img\Logo.png");
                XImage image = XImage.FromFile(path);

                // Create a font
                XFont txt = new XFont("Times New Roman", 10, XFontStyle.Regular);
                XFont title = new XFont("Times New Roman", 13, XFontStyle.Bold);
                XFont titleB = new XFont("Times New Roman", 15, XFontStyle.Bold);
                XFont titleC = new XFont("Times New Roman", 13, XFontStyle.BoldItalic);
                XFont subtitle = new XFont("Times New Roman", 13, XFontStyle.Italic);

                XStringFormat format = new XStringFormat();

                gfx.DrawImage(image, 460, 35, 105, 70);

                // Draw the text
                gfx.DrawString("ANEXO 4a", subtitle, XBrushes.Black, new XRect(1, 60, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("INSTITUTO TECNOLÓGICO DE AGUSCALIENTES", title, XBrushes.Black, new XRect(1, 75, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("DEPARTAMENTO DE DESARROLLO ACADEMICO", title, XBrushes.Black, new XRect(1, 90, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("CANALIZACION DE TUTORADOS", titleC, XBrushes.Black, new XRect(1, 130, page.Width, page.Height), XStringFormats.TopCenter);


                gfx.DrawString("Nombre del Tutor:", title, XBrushes.Black, new XRect(50, 175, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 155, 190, 540, 190);
                gfx.DrawString(dt.Rows[0]["NombreMaestro"].ToString(), txt, XBrushes.Black, new XRect(165, 175, page.Width, page.Height), format);
                gfx.DrawString("Nombre del Alumno:", title, XBrushes.Black, new XRect(50, 240, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 170, 255, 540, 255);
                gfx.DrawString(dt.Rows[0]["NombreAlumno"].ToString(), txt, XBrushes.Black, new XRect(180, 240, page.Width, page.Height), format);
                gfx.DrawString("Carrera:", title, XBrushes.Black, new XRect(50, 207, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 100, 222, 360, 222);
                gfx.DrawString(dt.Rows[0]["Carrera"].ToString(), txt, XBrushes.Black, new XRect(110, 207, page.Width, page.Height), format);
                gfx.DrawString("Semestre Actual:", title, XBrushes.Black, new XRect(380, 207, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 480, 222, 540, 222);
                gfx.DrawString(dt.Rows[0]["Semestre"].ToString(), txt, XBrushes.Black, new XRect(490, 207, page.Width, page.Height), format);

                gfx.DrawRectangle(XPens.Gray, XBrushes.LightGray, 110, 280, 380, 45);
                gfx.DrawString("Canalizacion de tutorados", titleB, XBrushes.Black, new XRect(1, 280, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawLine(XPens.Black, 50, 345, 540, 345);//horizontal1
                gfx.DrawLine(XPens.Black, 50, 385, 540, 385);//horizontal2
                gfx.DrawLine(XPens.Black, 50, 415, 540, 415);//horizontal3
                gfx.DrawLine(XPens.Black, 50, 445, 540, 445);//horizontal4
                gfx.DrawLine(XPens.Black, 50, 475, 540, 475);//horizontal5
                gfx.DrawLine(XPens.Black, 50, 505, 540, 505);//horizontal6
                gfx.DrawLine(XPens.Black, 50, 535, 540, 535);//horizontal7
                gfx.DrawLine(XPens.Black, 50, 585, 540, 585);//horizontal8
                gfx.DrawLine(XPens.Black, 50, 345, 50, 585);//vertical1 
                gfx.DrawLine(XPens.Black, 215, 345, 215, 585);//vertical2
                gfx.DrawLine(XPens.Black, 380, 345, 380, 585);//vertical3 
                gfx.DrawLine(XPens.Black, 540, 345, 540, 585);//vertical4

                gfx.DrawString("Área de Canalización", title, XBrushes.Black, new XRect(75, 360, page.Width, page.Height), format);
                gfx.DrawString("Alumno Canalizados", title, XBrushes.Black, new XRect(225, 360, page.Width, page.Height), format);
                gfx.DrawString("Observaciones", title, XBrushes.Black, new XRect(400, 360, page.Width, page.Height), format);

                gfx.DrawString("Atención Psicológica", txt, XBrushes.Black, new XRect(60, 395, page.Width, page.Height), format);
                gfx.DrawString("Atención Médica", txt, XBrushes.Black, new XRect(60, 425, page.Width, page.Height), format);
                gfx.DrawString("Circulos de estudio", txt, XBrushes.Black, new XRect(60, 455, page.Width, page.Height), format);
                gfx.DrawString("Platicas o conferencias", txt, XBrushes.Black, new XRect(60, 485, page.Width, page.Height), format);
                gfx.DrawString("servicios de apoyo externo", txt, XBrushes.Black, new XRect(60, 515, page.Width, page.Height), format);
                gfx.DrawString("Otros (especifique)", txt, XBrushes.Black, new XRect(60, 555, page.Width, page.Height), format);

                gfx.DrawString(num.Rows[0]["Psicologica"].ToString(), txt, XBrushes.Black, new XRect(293, 395, page.Width, page.Height), format);
                gfx.DrawString(num.Rows[0]["AtencionMedica"].ToString(), txt, XBrushes.Black, new XRect(293, 425, page.Width, page.Height), format);
                gfx.DrawString(num.Rows[0]["CirculoEstudio"].ToString(), txt, XBrushes.Black, new XRect(293, 455, page.Width, page.Height), format);
                gfx.DrawString(num.Rows[0]["Platicas"].ToString(), txt, XBrushes.Black, new XRect(293, 485, page.Width, page.Height), format);
                gfx.DrawString(num.Rows[0]["ApoyoExterno"].ToString(), txt, XBrushes.Black, new XRect(293, 515, page.Width, page.Height), format);

                gfx.DrawString("", txt, XBrushes.Black, new XRect(350, 395, 60, 10), format);
                gfx.DrawString("", txt, XBrushes.Black, new XRect(350, 425, 60, 10), format);
                gfx.DrawString("", txt, XBrushes.Black, new XRect(350, 455, 60, 10), format);
                gfx.DrawString("", txt, XBrushes.Black, new XRect(350, 485, 60, 10), format);
                gfx.DrawString("", txt, XBrushes.Black, new XRect(350, 515, 60, 10), format);


                // Save the document...
                string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"PDF\G_") + idGrupo + ".pdf";
                document.Save(filename);
                // ...and start a viewer.
                Process.Start(filename);
            }
        }

        public static void Reporte5(int idGrupo)
        {
            Metodos mt = new Metodos();
            DataTable dt;
            dt = mt.Reporte5GetInfo(idGrupo);

            if (dt.Rows.Count > 0)
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();

                // Create an empty page
                PdfPage page = document.AddPage();
                page.Orientation= PdfSharp.PageOrientation.Landscape;

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);
                string ita = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"img\Logo.png");
                string tecnm = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"img\tecnm.png");
                string sep = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"img\sep.png");
                XImage img_ita = XImage.FromFile(ita);
                XImage img_tecnm = XImage.FromFile(tecnm);
                XImage img_sep = XImage.FromFile(sep);

                // Create a font
                XFont txt = new XFont("Times New Roman", 12, XFontStyle.Regular);
                XFont txt1 = new XFont("Times New Roman", 10, XFontStyle.Regular);
                XFont txt2 = new XFont("Times New Roman", 9, XFontStyle.Regular);
                XFont txt3 = new XFont("Times New Roman", 8, XFontStyle.Regular);
                XFont title = new XFont("Times New Roman", 13, XFontStyle.Bold);
                XFont titleB = new XFont("Times New Roman", 15, XFontStyle.Bold);
                XFont titleC = new XFont("Times New Roman", 13, XFontStyle.BoldItalic);
                XFont subtitle = new XFont("Times New Roman", 12, XFontStyle.Bold);

                XStringFormat format = new XStringFormat();

                gfx.DrawImage(img_ita, 50, 520, 80, 55);
                gfx.DrawImage(img_sep, 50, 15, 130, 80);
                gfx.DrawImage(img_tecnm, 180, 30, 250, 50);

                gfx.DrawString("Instituto Tecnológico de Aguascalientes", txt, XBrushes.Black, new XRect(1, 60, page.Width-50, page.Height), XStringFormats.TopRight);
                // Draw the text
                gfx.DrawString("ANEXO 5", title, XBrushes.Black, new XRect(1, 80, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("INSTITUTO TECNOLÓGICO DE AGUSCALIENTES", title, XBrushes.Black, new XRect(1, 105, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("DEPARTAMENTO DE DESARROLLO ACADEMICO", title, XBrushes.Black, new XRect(1, 120, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("Formato de Informe Final de Tutorias", titleB, XBrushes.Black, new XRect(1, 160, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawString("Informe Final Semestre ", subtitle, XBrushes.Black, new XRect(1, 190, page.Width-140, page.Height), XStringFormats.TopCenter);
                gfx.DrawLine(XPens.Black, 415, 205, 495, 205);
                gfx.DrawString("ENE-JUN", subtitle, XBrushes.Black, new XRect(425, 190, page.Width, page.Height), format);
                gfx.DrawString("De " /*+ año*/, subtitle, XBrushes.Black, new XRect(500, 190, page.Width, page.Height), format);
                gfx.DrawString("2022" /*+ año*/, subtitle, XBrushes.Black, new XRect(515, 190, page.Width, page.Height), format);

                gfx.DrawString("Situacion Académica de los alumnos de ", subtitle, XBrushes.Black, new XRect(120, 210, page.Width - 140, page.Height), format);
                gfx.DrawLine(XPens.Black, 325, 225, 420, 225);
                gfx.DrawString("Primer semestre", subtitle, XBrushes.Black, new XRect(330, 210, page.Width, page.Height), format);
                gfx.DrawString("De tutorias de la carrera de  ", subtitle, XBrushes.Black, new XRect(425, 210, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 570, 225, 730, 225);
                gfx.DrawString("Ingenieria industrial", subtitle, XBrushes.Black, new XRect(570, 210, page.Width, page.Height), format);

                gfx.DrawString("Nombre De Tutor.", title, XBrushes.Black, new XRect(70, 240, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 175, 255, 430, 255);
                gfx.DrawString("Angie Johana Zamora López", subtitle, XBrushes.Black, new XRect(185, 240, page.Width, page.Height), format);
                gfx.DrawString("Fecha del informe", title, XBrushes.Black, new XRect(540, 240, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 640, 252, 755, 252);
                gfx.DrawString("15/08/2022", txt, XBrushes.Black, new XRect(650, 240, page.Width, page.Height), format);

                gfx.DrawString("No. de alumnos asignados.", subtitle, XBrushes.Black, new XRect(70, 290, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 210, 305, 260, 305);
                gfx.DrawString("38", txt, XBrushes.Black, new XRect(220, 290, page.Width, page.Height), format);
                gfx.DrawString("No. de alumnos Atendidos.", subtitle, XBrushes.Black, new XRect(350, 290, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 495, 305, 545, 305);
                gfx.DrawString("36", txt, XBrushes.Black, new XRect(510, 290, page.Width, page.Height), format);
                gfx.DrawString("Semestre.", subtitle, XBrushes.Black, new XRect(630, 290, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 685, 305, 750, 305);
                gfx.DrawString("Grupo 1°C", txt, XBrushes.Black, new XRect(690, 290, page.Width, page.Height), format);

                gfx.DrawString("1.-Resultados Especificos", title, XBrushes.Black, new XRect(70, 330, page.Width, page.Height), format);
                gfx.DrawLine(XPens.Black, 70, 345, 210, 345);

                gfx.DrawString("No de alumno canalizados a:", txt, XBrushes.Black, new XRect(120, 370, page.Width, page.Height), format);

                gfx.DrawLine(XPens.Black, 70, 390, 375, 390);//horizontal1
                gfx.DrawLine(XPens.Black, 70, 410, 375, 410);//horizontal2
                gfx.DrawLine(XPens.Black, 70, 430, 375, 430);//horizontal3
                gfx.DrawLine(XPens.Black, 70, 450, 375, 450);//horizontal4
                gfx.DrawLine(XPens.Black, 70, 470, 375, 470);//horizontal5
                gfx.DrawLine(XPens.Black, 70, 490, 375, 490);//horizontal6
                gfx.DrawLine(XPens.Black, 70, 390, 70, 490);//vertical1
                gfx.DrawLine(XPens.Black, 305, 390, 305, 490);//vertical1
                gfx.DrawLine(XPens.Black, 375, 390, 375, 490);//vertical1

                gfx.DrawString("Circulos de estudio", txt, XBrushes.Black, new XRect(80, 395, page.Width, page.Height), format);
                gfx.DrawString("Asistencia Médica", txt, XBrushes.Black, new XRect(80, 415, page.Width, page.Height), format);
                gfx.DrawString("Platicas o conferencias", txt, XBrushes.Black, new XRect(80, 435, page.Width, page.Height), format);
                gfx.DrawString("Atencion Psicológica", txt, XBrushes.Black, new XRect(80, 455, page.Width, page.Height), format);
                gfx.DrawString("Servicio de apoyo externo", txt, XBrushes.Black, new XRect(80, 475, page.Width, page.Height), format);

                page = document.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Landscape;
                gfx = XGraphics.FromPdfPage(page);

                gfx.DrawImage(img_ita, 50, 520, 80, 55);
                gfx.DrawImage(img_sep, 50, 15, 130, 80);
                gfx.DrawImage(img_tecnm, 180, 30, 250, 50);

                gfx.DrawString("Instituto Tecnológico de Aguascalientes", txt, XBrushes.Black, new XRect(1, 60, page.Width - 50, page.Height), XStringFormats.TopRight);

                gfx.DrawString("2.- Cumplimiento a la tutoría y desempeño académico", title, XBrushes.Black, new XRect(60, 90, page.Width, page.Height), format);
                //gfx.DrawLine(XPens.Black, 60, 105, 385, 105);

                double x = 170;

                gfx.DrawLine(XPens.Black, 55, 110, 795, 110);//horizontal1
                gfx.DrawLine(XPens.Black, 331, 125, 451, 125);//horizontal2
                gfx.DrawLine(XPens.Black, 481, 125, 613, 125);//horizontal3
                gfx.DrawLine(XPens.Black, 331, 140, 451, 140);//horizontal4
                gfx.DrawLine(XPens.Black, 635, 145, 695, 145);//horizontal5
                gfx.DrawLine(XPens.Black, 55, 170, 795, 170);//horizontal6
                

                gfx.DrawString("No", txt, XBrushes.Black, new XRect(57, 125, page.Width, page.Height), format);
                gfx.DrawString("No. de", txt, XBrushes.Black, new XRect(85, 120, 50, 30), format);
                gfx.DrawString("Control", txt, XBrushes.Black, new XRect(80, 140, 50, 30), format);                
                gfx.DrawString("Nombre", txt, XBrushes.Black, new XRect(210, 125, page.Width, page.Height), format);
                gfx.DrawString("Asistencias", txt, XBrushes.Black, new XRect(370, 110, page.Width, page.Height), format);
                gfx.DrawString("Fecha de entrevistas", txt, XBrushes.Black, new XRect(345, 125, page.Width, page.Height), format);
                gfx.DrawString("1ra", txt, XBrushes.Black, new XRect(345, 143, page.Width, page.Height), format);
                gfx.DrawString("2da", txt, XBrushes.Black, new XRect(385, 143, page.Width, page.Height), format);
                gfx.DrawString("3ra", txt, XBrushes.Black, new XRect(425, 143, page.Width, page.Height), format);
                gfx.DrawString("A", txt, XBrushes.Black, new XRect(455, 125, page.Width, page.Height), format);
                gfx.DrawString("B", txt, XBrushes.Black, new XRect(470, 125, page.Width, page.Height), format);
                gfx.DrawString("Materia(s) reprobadas", txt, XBrushes.Black, new XRect(500, 110, page.Width, page.Height), format);
                gfx.DrawString("1", txt, XBrushes.Black, new XRect(488, 145, page.Width, page.Height), format);
                gfx.DrawString("2", txt, XBrushes.Black, new XRect(510, 145, page.Width, page.Height), format);
                gfx.DrawString("3", txt, XBrushes.Black, new XRect(532, 145, page.Width, page.Height), format);
                gfx.DrawString("4", txt, XBrushes.Black, new XRect(554, 145, page.Width, page.Height), format);
                gfx.DrawString("5", txt, XBrushes.Black, new XRect(576, 145, page.Width, page.Height), format);
                gfx.DrawString("6", txt, XBrushes.Black, new XRect(598, 145, page.Width, page.Height), format);
                gfx.DrawString("Pro", txt, XBrushes.Black, new XRect(615, 115, 20, 30), format);
                gfx.DrawString("me", txt, XBrushes.Black, new XRect(615, 130, 20, 30), format);
                gfx.DrawString("dio", txt, XBrushes.Black, new XRect(615, 145, 20, 30), format);
                gfx.DrawString("Situacion", txt, XBrushes.Black, new XRect(640, 115, page.Width, page.Height), format);
                gfx.DrawString("Final", txt, XBrushes.Black, new XRect(650, 130, page.Width, page.Height), format);
                gfx.DrawString("D", txt, XBrushes.Black, new XRect(637, 155, page.Width, page.Height), format);
                gfx.DrawString("N", txt, XBrushes.Black, new XRect(652, 155, page.Width, page.Height), format);
                gfx.DrawString("I", txt, XBrushes.Black, new XRect(670, 155, page.Width, page.Height), format);
                gfx.DrawString("R", txt, XBrushes.Black, new XRect(685, 155, page.Width, page.Height), format);
                gfx.DrawString("Observaciones", txt, XBrushes.Black, new XRect(705, 110, page.Width, page.Height), format);
                gfx.DrawString("o causas de ", txt, XBrushes.Black, new XRect(705, 125, page.Width, page.Height), format);
                gfx.DrawString("reprobación", txt, XBrushes.Black, new XRect(705, 140, page.Width, page.Height), format);
                gfx.DrawString("o deserción", txt, XBrushes.Black, new XRect(705, 155, page.Width, page.Height), format);

                gfx.DrawLine(XPens.Black, 65, 190, 785, 190);//horizontal7

                gfx.DrawString(dt.Rows[0]["Entrevista1"].ToString().Substring(0,10), txt3, XBrushes.Black, new XRect(332, 160, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista2"].ToString().Substring(0,10), txt3, XBrushes.Black, new XRect(372, 160, page.Width, page.Height), format);
                gfx.DrawString(dt.Rows[0]["Entrevista3"].ToString().Substring(0,10), txt3, XBrushes.Black, new XRect(412, 160, page.Width, page.Height), format);

                dt =mt.Reporte5GetInfo(idGrupo);
                for (int i = 0; i < 18; i++)
                {
                    gfx.DrawString((i+1).ToString(), txt1, XBrushes.Black, new XRect(58, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["No_control"].ToString(), txt1, XBrushes.Black, new XRect(77, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["alumno"].ToString(), txt2, XBrushes.Black, new XRect(130, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Asistencia1"].ToString(), txt1, XBrushes.Black, new XRect(350, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Asistencia2"].ToString(), txt1, XBrushes.Black, new XRect(390, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Asistencia3"].ToString(), txt1, XBrushes.Black, new XRect(430, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["A"].ToString(), txt1, XBrushes.Black, new XRect(455, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["B"].ToString(), txt1, XBrushes.Black, new XRect(470, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Cal1"].ToString(), txt1, XBrushes.Black, new XRect(483, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Cal2"].ToString(), txt1, XBrushes.Black, new XRect(505, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Cal3"].ToString(), txt1, XBrushes.Black, new XRect(527, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Cal4"].ToString(), txt1, XBrushes.Black, new XRect(549, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Cal5"].ToString(), txt1, XBrushes.Black, new XRect(571, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Cal6"].ToString(), txt1, XBrushes.Black, new XRect(593, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["promedio"].ToString(), txt1, XBrushes.Black, new XRect(616, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["D"].ToString(), txt, XBrushes.Black, new XRect(638, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["N"].ToString(), txt, XBrushes.Black, new XRect(653, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["I"].ToString(), txt, XBrushes.Black, new XRect(668, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["R"].ToString(), txt, XBrushes.Black, new XRect(683, x + 5, page.Width, page.Height), format);
                    gfx.DrawString(dt.Rows[0]["Comentarios"].ToString(), txt1, XBrushes.Black, new XRect(697, x + 5, page.Width, page.Height), format);
                    x += 20;
                    gfx.DrawLine(XPens.Black, 55, x, 795, x);//horizontalDinamica
                }

                gfx.DrawLine(XPens.Black, 55, 110, 55, x);//vertical1
                gfx.DrawLine(XPens.Black, 75, 110, 75, x);//vertical2
                gfx.DrawLine(XPens.Black, 125, 110, 125, x);//vertical3
                gfx.DrawLine(XPens.Black, 331, 110, 331, x);//vertical4
                gfx.DrawLine(XPens.Black, 371, 140, 371, x);//vertical5
                gfx.DrawLine(XPens.Black, 411, 140, 411, x);//vertical6
                gfx.DrawLine(XPens.Black, 451, 110, 451, x);//vertical7
                gfx.DrawLine(XPens.Black, 466, 110, 466, x);//vertical8
                gfx.DrawLine(XPens.Black, 481, 110, 481, x);//vertical9
                gfx.DrawLine(XPens.Black, 503, 125, 503, x);//vertical10
                gfx.DrawLine(XPens.Black, 525, 125, 525, x);//vertical11
                gfx.DrawLine(XPens.Black, 547, 125, 547, x);//vertical12
                gfx.DrawLine(XPens.Black, 569, 125, 569, x);//vertical13
                gfx.DrawLine(XPens.Black, 591, 125, 591, x);//vertical14
                gfx.DrawLine(XPens.Black, 613, 110, 613, x);//vertical15
                gfx.DrawLine(XPens.Black, 635, 110, 635, x);//vertical16
                gfx.DrawLine(XPens.Black, 650, 145, 650, x);//vertical17
                gfx.DrawLine(XPens.Black, 665, 145, 665, x);//vertical18
                gfx.DrawLine(XPens.Black, 680, 145, 680, x);//vertical19
                gfx.DrawLine(XPens.Black, 695, 110, 695, x);//vertical20
                gfx.DrawLine(XPens.Black, 795, 110, 795, x);//vertical21






                // Save the document...
                string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"PDF\G_") + idGrupo + ".pdf";
                document.Save(filename);
                // ...and start a viewer.
                Process.Start(filename);
            }
        }

                /*
                public bool GrupoComCierre(string ID_Grupo)
                {
                    int promedio;
                    string 


                    try
                    {
                        cmd.Connection = cnn;
                        cmd.Connection.Open();
                        cmd.CommandText = "select * from Grupo_Compuesto where ID_Grupo=" + ID_Grupo;
                        cmd.Connection.Close();

                        dt.Load(cmd.ExecuteReader());
                        if(dt.Rows.Count > 0)
                        {
                            for(int i=0; i<dt.Rows.Count; i++)
                            {
                                if(dt.Rows[i].ItemArray[15].ToString() == "A" && dt.Rows[i].ItemArray[16].ToString() == "A" && 
                                    dt.Rows[i].ItemArray[17].ToString() == "A")
                                {

                                }

                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        return true;
                    }
                    finally
                    {
                        cmd.Connection.Close();
                    }*/


                /* 
                public bool m(string ,)
                {
                    cmd.Connection = cnn;
                    cmd.Connection.Open();
                    tr = cmd.Connection.BeginTransaction();
                    cmd.Transaction = tr;
                    cmd.CommandText = "" ;
                    try
                    {
                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            tr.Rollback();
                            return true;
                        }
                        else
                        {
                            tr.Commit();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        return true;
                    }
                    finally
                    {
                        cmd.Connection.Close();
                    }
                }
                */

            }
        }