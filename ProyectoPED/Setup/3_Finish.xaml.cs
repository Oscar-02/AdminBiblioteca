using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoPED.Setup
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class _3_Finish : Page
    {
        public MainClasses.MySql mysqlConfig = _1_Server.mySqlCnn;
        public MainClasses.AdminUsers adminConfig = _1_Server.adminUser;
        public ObservableCollection<MainClasses.AdminUsers> usersList = _2_User.saveUsers;
        public _3_Finish()
        {
            this.InitializeComponent();
            serverInfo.Text += "\n" + mysqlConfig.Server;
            databaseInfo.Text += "\n" + mysqlConfig.Database;
            portInfo.Text += "\n" + mysqlConfig.Port;
            usernamedbInfo.Text += "\n" + mysqlConfig.User;
            usernameInfo.Text += " " + adminConfig.Username;
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
