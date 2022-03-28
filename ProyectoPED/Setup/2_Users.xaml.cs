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
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoPED.Setup
{
    public partial class _2_User : Page
    {
        public static ObservableCollection<MainClasses.AdminUsers> saveUsers = new ObservableCollection<MainClasses.AdminUsers>();
        public ObservableCollection<MainClasses.AdminUsers> users = new ObservableCollection<MainClasses.AdminUsers>();
        public _2_User()
        {
            this.InitializeComponent();
            birth.MaxDate = DateTime.Now.AddYears(-18);
            users.Add(_1_Server.adminUser);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += (a, e) =>
            {
                bool box1 = !String.IsNullOrWhiteSpace(name.Text);
                bool box2 = !String.IsNullOrWhiteSpace(lname.Text);
                bool box3 = !String.IsNullOrWhiteSpace(dui.Text) && dui.Text.Length == 10 && checker();
                bool box4 = birth.Date.HasValue;
                bool box5 = !String.IsNullOrWhiteSpace(username.Text);
                bool box6 = !String.IsNullOrWhiteSpace(password.Password) && password.Password.Length >= 8;

                if (box1 && box2 && box3 && box4 && box5 && box6) addBtn.IsEnabled = true;
                else addBtn.IsEnabled = false;

                if (usersGrid.SelectedIndex < 0) deleteBtn.IsEnabled = false;
                else deleteBtn.IsEnabled = true;
            };
            timer.Start();
        }

        private bool checker()
        {
            string toCheck = dui.Text.Substring(9);
            try
            {
                int.Parse(toCheck);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        } //PARA DUI

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool already = false;
            int numMod = 0;
            string baseUsername = String.Empty;
            if (!String.IsNullOrWhiteSpace(name.Text))
            {
                if (String.IsNullOrWhiteSpace(lname.Text))
                {
                    baseUsername = name.Text.ToLower();
                    do
                    {
                        foreach (var element in users)
                        {
                            if (element.Username == baseUsername)
                            {
                                already = true;
                                break;
                            }
                            else already = false;
                        }
                        if (already)
                        {
                            baseUsername = name.Text.ToLower() + numMod;
                            numMod++;
                        }
                    }
                    while (already);
                }
                else
                {
                    baseUsername = name.Text.ToLower() + "_" + lname.Text.ToLower();
                    do
                    {
                        foreach (var element in users)
                        {
                            if (element.Username == baseUsername) already = true;
                            else already = false;
                        }
                        if (already)
                        {
                            baseUsername = baseUsername = name.Text.ToLower() + "_" + lname.Text.ToLower() + numMod;
                            numMod++;
                        }
                    }
                    while (already);
                }
                username.Text = baseUsername;
            }
        }

        private void lname_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool already = false;
            int numMod = 0;
            string baseUsername = String.Empty;
            if (!String.IsNullOrWhiteSpace(lname.Text))
            {
                if (String.IsNullOrWhiteSpace(name.Text))
                {
                    baseUsername = lname.Text.ToLower();
                    do
                    {
                        foreach (var element in users)
                        {
                            if (element.Username == baseUsername)
                            {
                                already = true;
                                break;
                            }
                            else already = false;
                        }
                        if (already)
                        {
                            baseUsername = lname.Text.ToLower() + numMod;
                            numMod++;
                        }
                    }
                    while (already);
                }
                else
                {
                    baseUsername = name.Text.ToLower() + "_" + lname.Text.ToLower();
                    do
                    {
                        foreach (var element in users)
                        {
                            if (element.Username == baseUsername)
                            {
                                already = true;
                                break;
                            }
                            else already = false;
                        }
                        if (already)
                        {
                            baseUsername = baseUsername = name.Text.ToLower() + "_" + lname.Text.ToLower() + numMod;
                            numMod++;
                        }
                    }
                    while (already);
                }
                username.Text = baseUsername;
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            MainClasses.AdminUsers user = new MainClasses.AdminUsers()
            {
                Id = 0,
                Name = name.Text,
                LastName = lname.Text,
                DUI = dui.Text,
                Birth = birth.Date.Value.Date,
                Username = username.Text,
                Password = password.Password
            };
            bool err1 = false, err2 = false;
            foreach (var element in users)
            {
                if (user.Username == element.Username) err1 = true;
                if (user.DUI == element.DUI) err2 = true;
            }
            if (err1)
            {
                MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Error, "Error al agregar usuario", "Ya existe un usuario con el nombre de usuario colocado.", true);
                username.SelectAll();
                username.Focus(FocusState.Programmatic);
                password.Password = String.Empty;
            }
            else if (err2)
            {
                MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Error, "Error al agregar usuario", "Ya existe un usuario con el N° de DUI ingresado.", true);
                dui.SelectAll();
                dui.Focus(FocusState.Programmatic);
                password.Password = String.Empty;
            }
            else
            {
                users.Add(user);
                name.Text = String.Empty;
                lname.Text = String.Empty;
                dui.Text = String.Empty;
                birth.Date = null;

                username.Text = String.Empty;
                password.Password = String.Empty;
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = usersGrid.SelectedItem as MainClasses.AdminUsers;
            if (item.Username == _1_Server.adminUser.Username)
            {
                MainClasses.DesignBasic.InfoBarOpen(InfoBarSeverity.Error, "Error al eliminar usuario", "No se puede eliminar al usuario administrador.", true);
            }
            else users.Remove(item);
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            saveUsers = users;
            MainPage.mainP.mainF.Navigate(typeof(_3_Finish));
        }

    }
}
