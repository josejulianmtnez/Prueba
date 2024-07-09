using CRUDSqlite.Data;
using CRUDSqlite.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUDSqlite.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detalles : ContentPage
    {
        public Detalles()
        {
            InitializeComponent();
        }

        public Detalles(string nombreUsuario, string apaternoUsuario, string amaternoUsuario, byte[] imgUsuario) : this()
        {
            // Establecer el contexto de datos con el nombre de usuario recibido
            BindingContext = new AlumnoViewModel(nombreUsuario, apaternoUsuario, amaternoUsuario, imgUsuario);
            imgControl.Source = ImageHelper.ConvertByteArrayToImage(imgUsuario);
        }
    }
}