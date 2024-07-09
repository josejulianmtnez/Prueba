using CRUDSqlite.Data;
using CRUDSqlite.Modelos;
using CRUDSqlite.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

using CRUDSqlite.Modelos;

namespace CRUDSqlite
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Al momento de cargar la aplicación, mostrar los datos que existen
            LlenarDatos();
        }


        

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            //Validar que los campos no esten vacios
            if (ValidarDatos())
            {
                byte[] imageBytes = ImageHelper.ConvertImageToByteArray(imgControl.Source); // imageControl es tu control de imagen

                //Objeto a guardar en la base de datos
                Alumno alumno = new Alumno
                {
                    Nombre = txtNombre.Text,
                    ApellidoPaterno = txtApellidoPaterno.Text,
                    ApellidoMaterno = txtApellidoMaterno.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Email = txtEmail.Text,
                    Imagen = imageBytes,
                };

                // Mandarlo a guardar a la tarea creada en SQLiteHelper 
                await App.SQLiteDB.SaveAlumnoAsync(alumno);
               
                await DisplayAlert("Registro", "Se guardó de manera exitosa al alumno", "Ok");
                LimpiarControles();
                //Al realizar un insert mostrar lo que se acaba de insertar en la base de datos
                LlenarDatos();


            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresa todos los datos", "Ok");
            }
        }

        public async void LlenarDatos()
        {
            //Metodo que permite llenar los datos al realizar un update o select
            var alumnoList = await App.SQLiteDB.GetAlumnosAsync();
            //Si la lista no está vacia, entonces mostrarla
            
        }
        public bool ValidarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoPaterno.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoMaterno.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }

            return respuesta;

        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            //Validar si existe un id
            if (!string.IsNullOrEmpty(txtIdAlumno.Text))
            {
                Alumno alumno = new Alumno()
                {
                    IdAlumno = Convert.ToInt32(txtIdAlumno.Text),
                    Nombre = txtNombre.Text,
                    ApellidoPaterno = txtApellidoPaterno.Text,
                    ApellidoMaterno = txtApellidoMaterno.Text,
                    Edad = Convert.ToInt32(txtEdad.Text),
                    Email = txtEmail.Text,
                };
                await App.SQLiteDB.SaveAlumnoAsync(alumno);
                await DisplayAlert("Registro", "Se actualizó de manera correcta el alumno", "Ok");

                LimpiarControles();
                txtIdAlumno.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnRegistrar.IsVisible = true;

                //visualizar los datos una vez actualizados en tiempo real
                LlenarDatos();

            }
        }

        private async void lstAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Al seleccionar un elemento de la lista recupera todas las propiedades de la lista
            var obj = (Alumno)e.SelectedItem;
            BtnRegistrar.IsVisible = false;
            txtIdAlumno.IsVisible = true;
            BtnActualizar.IsVisible = true;
            BtnEliminar.IsVisible = true;

            //Si el id es vacio no hacer nada, si contiene algo entonces hacer
            if (!string.IsNullOrEmpty(obj.IdAlumno.ToString())) 
            {
                // recueprar la informacion del alumno por la informacion del alumno proporcionada
                var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(obj.IdAlumno);

                if (alumno != null) 
                {
                    txtIdAlumno.Text = alumno.IdAlumno.ToString();
                    txtNombre.Text = alumno.Nombre;
                    txtApellidoPaterno.Text = alumno.ApellidoPaterno;
                    txtApellidoMaterno.Text = alumno.ApellidoMaterno;
                    txtEdad.Text = alumno.Edad.ToString();
                    txtEmail.Text = alumno.Email;
                    imgControl.Source = ImageHelper.ConvertByteArrayToImage(alumno.Imagen);


                }

            }

        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(Convert.ToInt32(txtIdAlumno.Text));
            if (alumno != null)
            {
                await App.SQLiteDB.DeleteAlumnoAsync(alumno);
                await DisplayAlert("Alumno", "Se eliminó de manera exitosa", "Ok");
                LimpiarControles();
                LlenarDatos();

                BtnRegistrar.IsVisible = true;
                txtIdAlumno.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;

            }
        }

        public void LimpiarControles()
        {




            txtIdAlumno.Text = "";
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtEdad.Text = "";
            txtEmail.Text = "";
            imgControl.Source = null;

        }

        private async Task SolicitarPermisos()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            
        }

        private async void BtnSeleccionarImagen_Clicked(object sender, EventArgs e)
        {
            await SolicitarPermisos();

            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Selecciona una imagen"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);

                    imgControl.Source = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo cargar la imagen: " + ex.Message, "OK");
            }
        }

        private async void verRegistro_Clicked(object sender, EventArgs e)
        {

            var alumno = await App.SQLiteDB.GetAlumnoByNombreAsync((txtNombreBuscar.Text));

            Application.Current.MainPage = new NavigationPage(new Detalles(alumno.Nombre, alumno.ApellidoPaterno, alumno.ApellidoMaterno, alumno.Imagen));
        }
    }
}
