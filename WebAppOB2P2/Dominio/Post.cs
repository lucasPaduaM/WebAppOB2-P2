using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.Dominio
{
    public class Post : Publicacion
    {
        private static int contadorId = -1;

        public EstadoAdmin estadoAdmin { get; set; }

        public int id { get; }

        public string imagen { get; set; }

        private List<Comentario> _listaComentarios = new List<Comentario>();

        public List<Comentario> GetComentarios()
        {
            return this._listaComentarios;
        }

        private static Post instancia;

        public static Post Instancia

        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Post();
                }
                return instancia;
            }
        }
        public Post()
        {
            this._listaComentarios = new List<Comentario>();
            this.fecha = DateTime.Now;
            contadorId++;
            this.id = contadorId;
            this.estadoAdmin = EstadoAdmin.NO_BANEADO;
        }
        public Post(DateTime fecha, Miembro autor, string titulo, string contenido, string imagen, EstadoPost estado, EstadoAdmin estadoAdmin = EstadoAdmin.NO_BANEADO)
        {
            contadorId++;
            id = contadorId;
            this.fecha = fecha;
            this.titulo = titulo;
            this.contenido = contenido;
            this.autor = autor;
            this.imagen = imagen;
            this.estado = estado;
            this.estadoAdmin = estadoAdmin;
        }

        public override int CalcularVA()
        {
            int VA = (likes * 5) + (dislikes * -2);
            if (this.estado == EstadoPost.PUBLICO)
            {
                VA += 10;
            }
            return VA;
        }
    }
}