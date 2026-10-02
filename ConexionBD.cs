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
        // 🔹 CONEXIÓN REMOTA (puedes dejarla igual o comentar si no la usas)
        SqlConnection conexionRemota = new SqlConnection(
            "data source=172.16.16.87,1433; initial catalog=Tutoria; user id=UserTutoria; password=1234"
        );

        // 🔹 CONEXIÓN LOCAL (CORREGIDA)
        SqlConnection conexionLocal = new SqlConnection(
            "Server=DESKTOP-S9VH747\\micro; Database=Tutorias; Integrated Security=True"
        );

        public bool VerificarConexionRemotamente()
        {
            bool ConexionVerificada = false;

            try
            {
                conexionRemota.Open();
                MessageBox.Show("Conexión remota verificada correctamente.");
                ConexionVerificada = true;
            }
            catch (Exception e)
            {
                conexionRemota.Close();
                MessageBox.Show("❌ Error al conectar remotamente:\n" + e.Message);
                ConexionVerificada = false;
            }

            return ConexionVerificada;
        }

        public bool VerificarConexionLocal()
        {
            bool ConexionVerificada = false;

            try
            {
                conexionLocal.Open();
                MessageBox.Show("✅ Conexión local verificada correctamente.");
                ConexionVerificada = true;
            }
            catch (Exception e)
            {
                conexionLocal.Close();
                MessageBox.Show("❌ Error al conectar localmente:\n" + e.Message);
                ConexionVerificada = false;
            }

            return ConexionVerificada;
        }

        // 🔹 Devuelve conexión remota o local según se necesite
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
            conexionLocal.Open();
        }

        public void ConexionCerrada()
        {
            conexionLocal.Close();
        }
    }
}

