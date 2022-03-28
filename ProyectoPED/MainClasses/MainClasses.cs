using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Data;
using MySqlConnector;

namespace ProyectoPED.MainClasses
{
    public class AdminUsers
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public DateTime Birth { get; set; }
        public String DUI { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
    }

    public class ClientUsers
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public DateTime Birth { get; set; }
        public String Carnet { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
    }

    public class DesignBasic
    {
        /// <summary>
        /// Muestra la barra informativa.
        /// </summary>
        /// <param name="Severity">Importancia del mensaje.</param>
        /// <param name="Title">Titulo de la barra informativa.</param>
        /// <param name="Content">Contenido de la barra informativa.</param>
        /// <param name="IsClosable">Determina si la barra informativa puede ser cerrada. Si es <b>true</b>, se cerrara en 7.5s.</param>
        public static void InfoBarOpen(InfoBarSeverity Severity, string Title, string Content, bool IsClosable)
        {
            MainPage mainP = MainPage.mainP;
            mainP.infoBar.Severity = Severity;
            mainP.infoBar.Title = Title;
            mainP.infoBar.Content = Content + '\n';
            mainP.infoBar.IsClosable = IsClosable;
            if (IsClosable)
            {
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(7500);
                timer.Start();
                timer.Tick += (a, e) =>
                {
                    mainP.infoBar.IsOpen = false;
                    timer.Stop();
                };
            }
            mainP.infoBar.IsOpen = true;
        }

        public static void InfoBarClose()
        {
            MainPage.mainP.infoBar.IsOpen = false;
        }
    }
}
