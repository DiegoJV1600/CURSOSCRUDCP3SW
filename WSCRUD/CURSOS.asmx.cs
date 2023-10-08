using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WSCRUD.Modelo;

namespace WSCRUD
{
    /// <summary>
    /// Descripción breve de CURSOS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CURSOS : System.Web.Services.WebService
    {

        [WebMethod(Description = "Consultar datos de los cursos")]
        public List<Curso> ConsultarDatosCurso()
        {
            using (var conexion = new CursosEntities())
            {
                var consulta = from x in conexion.Cursoes select x;

                return consulta.ToList();
            }
        }
        [WebMethod(Description = "Consultar datos de un curso por ID")]
        public List<Curso> ConsultarCursoID(int id)
        {
            using (var conexion = new CursosEntities())
            {
                var consulta = (from x in conexion.Cursoes
                                where x.ID == id
                                select x).ToList();

                return consulta;
            }
        }
        [WebMethod(Description = "Consultar datos del curso por nombre")]
        public List<Curso> ConsultarCursoNombre(string nombre_curso)
        {
            using (var conexion = new CursosEntities())
            {
                var consulta = (from x in conexion.Cursoes
                                where x.CNombre.Contains(nombre_curso)
                                select x).ToList();
                return consulta;
            }
        }
        [WebMethod(Description = "Consultar nombre del curso por ID")]
        public string ConsultarNombreCurso(int id)
        {
            using (var conexion = new CursosEntities())
            {
                var consulta = (from x in conexion.Cursoes
                                where x.ID == id
                                select x.CNombre).FirstOrDefault();

                return consulta;
            }
        }
        [WebMethod (Description = "Consultar docente del curso por ID")]
        public DatoCurso ConsultarDocenteCurso(int id)
        {
            using (var conexion = new CursosEntities())
            {
                var consulta = (from x in conexion.Cursoes
                                where x.ID == id
                                select new DatoCurso
                                {
                                    NombreDocente = x.DNombre,
                                    ApellidosDocente = x.DApellidos
                                }
                                ).FirstOrDefault();

                return consulta;
            }
        }
        [WebMethod(Description = "Insertar curso")]
        public bool InsertarCurso(int id, string nombre_curso, string nombre_docente, string apellidos_docente, int n_horas, int n_alumnos)
        {
            try
            {
                using (var conexion = new CursosEntities())
                {
                    Curso nuevo = new Curso();

                    nuevo.ID = id;
                    nuevo.CNombre = nombre_curso;
                    nuevo.DNombre = nombre_docente;
                    nuevo.DApellidos = apellidos_docente;
                    nuevo.NumHoras = n_horas;
                    nuevo.NumAlumnos = n_alumnos;

                    conexion.Cursoes.Add(nuevo);
                    conexion.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [WebMethod(Description = "Modificar curso")]
        public bool ModificarCurso(int id, string nombre_curso, string nombre_docente, string apellidos_docente, int n_horas, int n_alumnos)
        {
            try
            {
                using (var conexion = new CursosEntities())
                {
                    var consulta = (from x in conexion.Cursoes
                                    where x.ID == id
                                    select x).FirstOrDefault();

                    if (consulta != null)
                    {
                        consulta.CNombre = nombre_curso;
                        consulta.DNombre = nombre_docente;
                        consulta.DApellidos = apellidos_docente;
                        consulta.NumHoras = n_horas;
                        consulta.NumAlumnos = n_alumnos;

                        conexion.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [WebMethod(Description = "Eliminar curso")]
        public bool EliminarCurso(int id)
        {
            try
            {
                using (var conexion = new CursosEntities())
                {
                    var consulta = (from x in conexion.Cursoes
                                    where x.ID == id
                                    select x).FirstOrDefault();

                    if (consulta != null)
                    {
                        conexion.Cursoes.Remove(consulta);
                        conexion.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}