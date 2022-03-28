using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ProyectoPED.MainClasses
{
    public class SettingReader
    {
        public MySql MySql { get; set; }
        public string Admin { get; }

        public SettingReader()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            MySql.Server = localSettings.Values["mysqlServer"].ToString();
            MySql.Port = localSettings.Values["mysqlPort"].ToString();
            MySql.Database = localSettings.Values["mysqlDatabase"].ToString();
            MySql.User = localSettings.Values["mysqlUsername"].ToString();
            MySql.Password = localSettings.Values["mysqlPassword"].ToString();
            Admin = localSettings.Values["appAdmin"].ToString();
        }
    }
}
