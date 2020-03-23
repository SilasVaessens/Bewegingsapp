using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bewegingsapp.Data;
using System.IO;

namespace Bewegingsapp
{
    public partial class App : Application
    {
        static DatabaseApp databaseApp;
        public static DatabaseApp Database
        {
            get
            {
                if (databaseApp == null)
                {
                    databaseApp = new DatabaseApp(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Bewegingsapp.db"));
                }
                return databaseApp;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
