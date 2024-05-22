using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.Dominio
{
    public abstract class Publicacion
    {
        private static int id { get; set; }
        public int valorDeAceptacion { get; set; }
        public string titulo { get; set; }
        public DateTime fecha { get; set; }
        public Miembro autor { get; set; }
        public string contenido { get; set; }
        public EstadoPost estado { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }

        public List<Reaccion> listaReacciones = new List<Reaccion>();

        public List<Reaccion> GetReacciones()
        {
            return this.listaReacciones;
        }

        public int GetId()
        {
            return id;
        }

        public string GetTitulo()
        {
            return titulo;
        }

        public string GetContenido()
        {
            return contenido;
        }

        public DateTime GetFecha()
        {
            return fecha;
        }

        public Miembro GetAutor()
        {
            return autor;
        }

        public void SetId()
        {
            id++;
        }

        public void SetTitulo(string titulo)
        {
            this.titulo = titulo;
        }

        public void SetFecha(DateTime _fecha)
        {
            this.fecha = _fecha;
        }

        public void SetAutor(Miembro autor)
        {
            this.autor = autor;
        }

        public void SetContenido(string contenido)
        {
            this.contenido = contenido;
        }

        public abstract int CalcularVA();

        public bool YaReacciono(Miembro miembro)
        {
            bool yaReacciono = false;
            try
            {
                foreach (Reaccion reaccion in listaReacciones)
                {
                    if (reaccion.miembroReaccion.Equals(miembro))
                    {
                        yaReacciono = true;
                    }
                }
            }
            catch (Exception)
            {
                yaReacciono = false;
            }
            return yaReacciono;
        }

        public Reaccion GetUltimaReaccion(Miembro miembro)
        {
            Reaccion reaccionTemp = new Reaccion();
            foreach (Reaccion reaccion in listaReacciones)
            {
                if (reaccion.miembroReaccion.Equals(miembro))
                {
                    reaccionTemp = reaccion;
                }
            }
            return reaccionTemp;
        }

        public int GetPosReaccion(Miembro miembro)
        {
            int pos = 0;
            foreach (Reaccion reaccion in listaReacciones)
            {
                if (reaccion.miembroReaccion.Equals(miembro))
                {
                    return pos;
                }
                pos++;
            }
            return pos;
        }

        public bool DioMeGusta(Miembro usuarioLogueado)
        {
            bool dioMeGusta = false;
            foreach (Reaccion reaccion in listaReacciones)
            {
                if (reaccion.miembroReaccion.Equals(usuarioLogueado) && reaccion.TipoReaccion == Reacciones.Like)
                {
                    dioMeGusta = true;
                }
            }
            return dioMeGusta;
        }
    }
}