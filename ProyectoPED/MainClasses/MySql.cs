using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using Windows.Storage;
using Windows.Networking.BackgroundTransfer;
using Windows.Foundation;
using System.Threading.Tasks;
using MySqlConnector;

namespace ProyectoPED.MainClasses
{

    public class MySql
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Nuevo elemento base MySQL sin contraseña.
        /// </summary>
        /// <param name="server">Direccion del servidor.</param>
        /// <param name="port">Puerto de MySQL.</param>
        /// <param name="user">Usuario de MySQL.</param>
        public MySql(string server, string port, string user)
        {
            Server = server;
            Port = port;
            User = user;
            Password = String.Empty;
        }

        /// <summary>
        /// Nuevo elemento base MySQL con contraseña.
        /// </summary>
        /// <param name="server">Direccion del servidor.</param>
        /// <param name="port">Puerto de MySQL.</param>
        /// <param name="user">Usuario de MySQL.</param>
        /// <param name="password">Contraseña del usuario MySQL.</param>
        public MySql(string server, string port, string user, string password)
        {
            Server = server;
            Port = port;
            User = user;
            Password = password;
        }

        /// <summary>
        /// Conexion asincronica sencilla a MySQL sin usar base de datos. Se utiliza para pruebas de conexion.
        /// </summary>
        /// <param name="data">Elemento base MySQL con parametros ya definidos.</param>
        /// <returns></returns>
        public static async Task<bool> MySqlTestSimpleAsync(MySql data)
        {
            string cnnStr;
            if (String.IsNullOrEmpty(data.Password))
            {
                cnnStr = "Server=" + data.Server + ";Port=" + data.Port + ";Uid=" + data.User + ";";
            }
            else
            {
                cnnStr = "Server=" + data.Server + ";Port=" + data.Port + ";Uid=" + data.User + ";Pwd=" + data.Password + ";";
            }
            MySqlConnection cnn = new MySqlConnection(cnnStr);
            try
            {
                await cnn.OpenAsync();
            }
            catch
            {
                return false;
            }
            if (cnn.State == ConnectionState.Open) cnn.Close();
            return true;
        }

        /// <summary>
        /// Conexion sencilla a MySQL sin usar base de datos. Se utiliza para pruebas de conexion.
        /// </summary>
        /// <param name="data">Elemento base MySQL con parametros ya definidos.</param>
        /// <returns></returns>
        public static bool MySqlTestSimple(MySql data)
        {
            string cnnStr;
            if (String.IsNullOrEmpty(data.Password))
            {
                cnnStr = "Server=" + data.Server + ";Port=" + data.Port + ";Uid=" + data.User + ";";
            }
            else
            {
                cnnStr = "Server=" + data.Server + ";Port=" + data.Port + ";Uid=" + data.User + ";Pwd=" + data.Password + ";";
            }
            MySqlConnection cnn = new MySqlConnection(cnnStr);
            try
            {
                cnn.Open();
            }
            catch
            {
                return false;
            }
            if (cnn.State == ConnectionState.Open) cnn.Close();
            return true;
        }

        /// <summary>
        /// Conexion a MySQL usando una base de datos definida. Se utiliza para pruebas de conexion.
        /// </summary>
        /// <param name="data">Elemento base MySQL con parametros ya definidos.</param>
        /// <returns></returns>
        public static bool MySqlTestDB(MySql data)
        {
            string cnnStr;
            if (String.IsNullOrEmpty(data.Password))
            {
                cnnStr = "Server=" + data.Server + ";Port=" + data.Port + ";Database=" + data.Database + ";Uid=" + data.User + ";";
            }
            else
            {
                cnnStr = "Server=" + data.Server + ";Port=" + data.Port + ";Database=" + data.Database + ";Uid=" + data.User + ";Pwd=" + data.Password + ";";
            }
            MySqlConnection cnn = new MySqlConnection(cnnStr);
            try
            {
                cnn.Open();
            }
            catch
            {
                return false;
            }
            if (cnn.State == ConnectionState.Open) cnn.Close();
            return true;
        }

        /// <summary>
        /// Crea la base de datos que utilizara la aplicacion.
        /// </summary>
        /// <param name="exist">Declara si la base de datos existe</param>
        /// <returns></returns>
        public static bool MySqlCreateDB(bool exist)
        {
            string cnnStr;
            var data = new SettingReader().MySql;
            if (String.IsNullOrEmpty(data.Password))
            {
                cnnStr = "Server=" + data.Server + ";Port=" + data.Port + ";Uid=" + data.User + ";";
            }
            else
            {
                cnnStr = "Server=" + data.Server + ";Port=" + data.Port + ";Uid=" + data.User + ";Pwd=" + data.Password + ";";
            }
            MySqlConnection cnn = new MySqlConnection(cnnStr);
            try
            {
                cnn.Open();
            }
            catch
            {
                return false;
            }
            if (cnn.State == ConnectionState.Open)
            {
                if (exist)
                {
                    MySqlCommand cmd = new MySqlCommand("DROP DATABASE BiblioDB", cnn);
                    cmd.ExecuteNonQuery();
                }
                
            }
        }
    }
}
