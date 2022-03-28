using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoPED.Setup
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Configure : Page
    {
        public Configure()
        {
            this.InitializeComponent();
        }

        public async void AppConfigure()
        {
            if (await SettingsCreator())
            {
                pText.Text = "Intentando conexion rapida al servidor mySQL.";
                await Task.Delay(1000);
                if (await MainClasses.MySql.MySqlTestSimpleAsync(new MainClasses.SettingReader().MySql))
                {
                    pText.Text = "Intentando conexion a la base de datos del servidor mySQL.";
                    await Task.Delay(1000);
                    if (MainClasses.MySql.MySqlTestDB(new MainClasses.SettingReader().MySql))
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    //ERR
                }
            }
            else
            {
                //ERR
            }
        }

        public async Task<bool> SettingsCreator()
        {
            MainClasses.MySql configLoad = _1_Server.mySqlCnn;
            MainClasses.AdminUsers admin = _1_Server.adminUser;
            pText.Text = "Creando claves de configuracion.";
            await Task.Delay(1000);
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("mysqlPwd"))
            {
                localSettings.Values.Clear();
            }
            localSettings.Values.Add("mysqlServer", configLoad.Server);
            localSettings.Values.Add("mysqlPort", configLoad.Port);
            localSettings.Values.Add("mysqlDatabase", configLoad.Database);
            localSettings.Values.Add("mysqlUsername", configLoad.User);
            localSettings.Values.Add("mysqlPassword", configLoad.Password);
            localSettings.Values.Add("appAdmin", admin.Username);
            if (localSettings.Values.ContainsKey("appAdmin")) return true;
            else return false;
        }


    }
}
