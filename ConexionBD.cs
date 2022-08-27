using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace TutoriasWeb
{
    public class ConexionBD
    {
        SqlConnection conexionRemota = new SqlConnection("data source=172.16.16.87,1433 ; initial catalog= Tutoria; user id=UserTutoria; password=1234");
        SqlConnection conexionLocal = new SqlConnection("server=DESKTOP-1I96JTK\\SQLEXPRESS ; database=Tutoria ; integrated security = true");

       public bool VerificarConexionRemotamente()
        {
 
            //data source = ip del equipo; initial catalog = nombre de la BD; user = usercreado; password = password creada del user
            //configurar sql server agregar usuario y permisos, firewall y management sql para la conexion local.
            
            bool ConexionVerificada = false;

            try
            {
                conexionRemota.Open();
                MessageBox.Show("Conexión verificada.");

                ConexionVerificada = true;
                //PantallaMain frm = new PantallaMain();
                //frm.Show();

            }

            catch(Exception e)
            {
                conexionRemota.Close();
                MessageBox.Show("====== C O N E X I Ó N    F A L L I D A. ======\n" + e);
                ConexionVerificada = false;
               
            }

            return ConexionVerificada;
        }
       

        public bool VerificarConexionLocal()
        {
            //agregar nombre del servidor y agregar nombre de la bd
            SqlConnection conexion = new SqlConnection("server=DESKTOP-1I96JTK\\SQLEXPRESS ; database=Tutoria ; integrated security = true");
            bool ConexionVerificada = false;

            try
            {
                conexionLocal.Open();
                MessageBox.Show("Conexión verificada.");

                ConexionVerificada = true;
                //PantallaMain frm = new PantallaMain();
                //frm.Show();

            }

            catch (Exception e)
            {
                conexionLocal.Close();
                MessageBox.Show("====== C O N E X I Ó N    F A L L I D A. ======\n" + e);
                ConexionVerificada = false;

            }

            return ConexionVerificada;
        }

        
        public SqlConnection ConexionRemota()
        {  
            return conexionRemota;
        }
        
        

        public SqlConnection ConexionLocal()
        {
            return conexionLocal;
        }
        
        
        public void ConexionAbierta()
        {
//           conexionRemota.Open();
            conexionLocal.Open();
        }

        public void ConexionCerrada()
        {
//            conexionRemota.Close();
            conexionLocal.Close();
        }
    }
}