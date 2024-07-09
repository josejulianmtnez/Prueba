using System;
using System.Collections.Generic;
using System.Text;

//Importar la libreria de SQLite
using SQLite;

namespace CRUDSqlite.Modelos
{

    //Este modelo es la representación de la tabla Alumnos
    public class Alumno
    {
        [PrimaryKey, AutoIncrement]
        public int IdAlumno { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(50)]
        public string ApellidoPaterno { get; set; }    

        [MaxLength(50)]

        public string ApellidoMaterno { get; set; }

        public int Edad { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public byte[] Imagen { get; set; }    

    }
}
