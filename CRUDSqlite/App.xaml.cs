using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CRUDSqlite.Data;
using System.IO;


namespace CRUDSqlite
{
    public partial class App : Application
    {
        static SQLiteHelper db;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static SQLiteHelper SQLiteDB
        {
            //Validar la existencia de la base de datos, si existe realiza la CREACION, sino no la realiza
            get 
            {
                if (db==null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Escuela.db3"));
                }    
                return db;
            }
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
