using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.Dominio
{
    public class Comentario : Publicacion
    {
        private static int contadorId = -1;
        public int id { get; }
        public EstadoAdmin estadoAdmin { get; set; }
        public Comentario()
        {
            contadorId++;
            id = contadorId;
            this.SetId();
            this.estadoAdmin = EstadoAdmin.NO_BANEADO;
        }

        public Comentario(string titulo, DateTime fecha, Miembro autor, string contenido, EstadoAdmin estadoAdminn = EstadoAdmin.NO_BANEADO)
        {
            contadorId++;
            id = contadorId;
            this.SetId();
            this.SetTitulo(titulo);
            this.SetFecha(fecha);
            this.SetAutor(autor);
            this.SetContenido(contenido);
            this.estadoAdmin = estadoAdminn;
        }

        public override int CalcularVA()
        {
            int VA = (likes * 5) + (dislikes * -2);
            return VA;
        }
    }
}