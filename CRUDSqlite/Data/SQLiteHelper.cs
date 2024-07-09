using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using CRUDSqlite.Modelos;
using System.Threading.Tasks;


namespace CRUDSqlite.Data
{
    public class SQLiteHelper
    {

        //Creacion de la base de datos
        SQLiteAsyncConnection db;

        //Creacion de la tabla
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Alumno>().Wait();
        }

        //Insertar o Actualizar

        public Task<int> SaveAlumnoAsync(Alumno alum) 
        {
            // Verificar si el ID es diferente de 0 entonces:
            if (alum.IdAlumno != 0)
            {
                //Actualizar el registro
                return db.UpdateAsync(alum);

            }
            else
            {
                //Sino entonces insertar nuevo registro
                return db.InsertAsync(alum);
            }
        }

        //Mostrar todos los alumnos
        public Task<List<Alumno>> GetAlumnosAsync()
        {
            // Recuperar a todos los alumnos guardados en la tabla alumnos
            return db.Table<Alumno>().ToListAsync();
        }

        //Mostrar todos los alumnos por ID
        public Task<Alumno> GetAlumnoByIdAsync(int idAlumno)
        {
            return db.Table<Alumno>().Where(a => a.IdAlumno == idAlumno).FirstOrDefaultAsync(); 
        }

        //Eliminar
        public Task<int> DeleteAlumnoAsync(Alumno alumno)
        {
            return db.DeleteAsync(alumno);
        }

        public Task<Alumno> GetAlumnoByNombreAsync(string Nombre)
        {
            return db.Table<Alumno>().Where(a => a.Nombre == Nombre).FirstOrDefaultAsync();
        }








    }
}
