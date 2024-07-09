using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace CRUDSqlite.Modelos
{
    public class AlumnoViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _nombreUsuario;

        private string _apaternoUsuario;

        private string _amaternoUsuario;

        private int _edadUsuario;

        private string _emailUsuario;

        private byte[] _imgUsuario;


        public AlumnoViewModel(string nombreUsuario, string apaternoUsuario, string amaternoUsuario, byte[] imgUsuario)
        {
            NombreUsuario = nombreUsuario;
            ApaternoUsuario = apaternoUsuario;
            AmaternoUsuario = amaternoUsuario;
            ImgUsuario = imgUsuario;
        }




        public string NombreUsuario
        {
            get { return _nombreUsuario; }
            set
            {
                _nombreUsuario = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NombreUsuario)));
            }
        }


        public string ApaternoUsuario
        {
            get { return _apaternoUsuario; }
            set
            {
                _apaternoUsuario = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ApaternoUsuario)));
            }
        }


        public string AmaternoUsuario
        {
            get { return _amaternoUsuario; }
            set
            {
                _amaternoUsuario = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmaternoUsuario)));
            }
        }


        public byte[] ImgUsuario
        {
            get { return _imgUsuario; }
            set
            {
                _imgUsuario = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImgUsuario)));
            }
        }



    }
}
