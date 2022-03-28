using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ProyectoPED
{
    public partial class MainPage : Page
    {
        public static MainPage mainP;
        public MainPage()
        {
            this.InitializeComponent();
            mainP = this;
            mainF.CacheSize = 5;
            mainF.Navigate(typeof(Setup._1_Server));
        }
    }
}
