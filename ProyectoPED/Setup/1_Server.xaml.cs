using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
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
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Text.RegularExpressions;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoPED.Setup
{
    public sealed partial class _1_Server : Page
    {
        public static MainClasses.MySql mySqlCnn;
        public static MainClasses.AdminUsers adminUser;

        public _1_Server()
        {
            this.InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += (a, e) =>
            {
                if ((bool)testCheck.IsChecked && uN && pW && password.Password.Length >= 8) nextBtn.IsEnabled = true;
                else nextBtn.IsEnabled = false;

                if (sD && uNsql)
                {
                    bool cond;
                    if (!String.IsNullOrWhiteSpace(portNum.Text))
                    {
                        if (pN) cond = true;
                        else cond = false;
                    }
                    else cond = true;
                    if (cond) testBtn.IsEnabled = true;
                    else testBtn.IsEnabled = false;
                }
                else testBtn.IsEnabled = false;
            };
            timer.Start();

        }

        #region TextValidVerifier
        private bool sD, pN, uNsql, pWsql, uN, pW;
        private void serverDir_TextChanged(object sender, TextChangedEventArgs e)
        {
            sD = !String.IsNullOrWhiteSpace(serverDir.Text);
            if ((bool)testCheck.IsChecked)
            {
                testCheck.IsChecked = false;
                string wrnTitle = "Advertencia. Datos de conexion modificados.";
                string wrnContent = "Tendra que realizar una nueva prueba de conexion/n para avanzar.";
                MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Warning, wrnTitle, wrnContent, true);
            }
        }

        private void portNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((bool)testCheck.IsChecked)
            {
                testCheck.IsChecked = false;
                string wrnTitle = "Advertencia. Datos de conexion modificados.";
                string wrnContent = "Tendra que realizar una nueva prueba de conexion/n para avanzar.";
                MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Warning, wrnTitle, wrnContent, true);
            }
            if (!String.IsNullOrWhiteSpace(portNum.Text))
            {
                try
                {
                    int.Parse(portNum.Text);
                    MainClasses.DesignBasic.InfoBarClose();
                    pN = true;
                }
                catch (FormatException)
                {
                    string errTitle = "Error de formato (ERR-101)";
                    string errContent = "Solamente se admiten valores numericos en este campo.";
                    MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Error, errTitle, errContent, true);
                    pN = false;
                }
            }
            else
            {
                pN = true;
            }
        }

        private void usernameSQL_TextChanged(object sender, TextChangedEventArgs e)
        {
            uNsql = !String.IsNullOrWhiteSpace(usernameSQL.Text);
            if ((bool)testCheck.IsChecked)
            {
                testCheck.IsChecked = false;
                string wrnTitle = "Advertencia. Datos de conexion modificados.";
                string wrnContent = "Tendra que realizar una nueva prueba de conexion/n para avanzar.";
                MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Warning, wrnTitle, wrnContent, true);
            }
        }

        private void passwordSQL_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pWsql = true;
            if ((bool)testCheck.IsChecked)
            {
                testCheck.IsChecked = false;
                string wrnTitle = "Advertencia. Datos de conexion modificados.";
                string wrnContent = "Tendra que realizar una nueva prueba de conexion/n para avanzar.";
                MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Warning, wrnTitle, wrnContent, true);
            }
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            uN = !String.IsNullOrWhiteSpace(username.Text);
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pW = !String.IsNullOrWhiteSpace(password.Password);
            if (password.Password.Length < 8)
            {
                passWordCheck.Severity = InfoBarSeverity.Warning;
                passWordCheck.Message = "Contraseña invalida. Minimo 8 caracteres.";
            }
            else
            {
                passWordCheck.Severity = InfoBarSeverity.Success;
                passWordCheck.Message = "Contraseña valida.";
            }
        }
        #endregion

        #region ConnectionTest

        private async void testBtn_Click(object sender, RoutedEventArgs e)
        {
            MainClasses.MySql info;
            string port = String.IsNullOrWhiteSpace(portNum.Text) ? "3306" : portNum.Text;
            if (String.IsNullOrWhiteSpace(passwordSQL.Password))
            {
                info = new MainClasses.MySql(serverDir.Text, port, usernameSQL.Text);
            }
            else info = new MainClasses.MySql(serverDir.Text, port, usernameSQL.Text, passwordSQL.Password);
            bool ok = await MainClasses.MySql.MySqlTestSimpleAsync(info);
            if (!ok)
            {
                testCheck.IsChecked = false;
                string errTitle = "Error al contactar al servidor. (ERR-201)";
                string errContent = "No se ha logrado la conexion al servidor. Verifique si se encuentra\nfuncionando y que los datos sean correctos.";
                MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Error, errTitle, errContent, true);
            }
            else
            {
                testCheck.IsChecked = true;
                mySqlCnn = info;
                string succTitle = "Conexion finalizada correctamente.";
                string succContent = "Se ha conectado correctamente al servidor.";
                MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Success, succTitle, succContent, true);
            }
        }

        #endregion


        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            mySqlCnn.Database = "BiblioDB";
            MainClasses.AdminUsers admin = new MainClasses.AdminUsers()
            {
                Id = 0,
                Name = "Administrator",
                LastName = String.Empty,
                DUI = "00000000-0",
                Birth = DateTime.Now,
                Username = username.Text,
                Password = password.Password
            };
            adminUser = admin;

            MainPage.mainP.mainF.Navigate(typeof(_2_User));
        }
    }
}
