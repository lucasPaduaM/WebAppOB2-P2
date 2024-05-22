using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.Dominio
{
    public class Miembro : Usuario
    {
        public string nombre { get; set; }
        public EstadoAdmin estadoAdmin { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNac { get; set; }
        public bool bloqueado { get; set; }
        private List<Miembro> listaAmigos = new List<Miembro>();
        private List<Invitacion> listaPendientes = new List<Invitacion>();

        public List<Miembro> GetListaAmigos()
        {
            return listaAmigos;
        }
        public List<Invitacion> GetListaPendientes()
        {
            return listaPendientes;
        }

        public int cantidadDePublicaciones;

        public Miembro()
        {
            bloqueado = false;
            listaAmigos = new List<Miembro>();
            this.estadoAdmin = EstadoAdmin.NO_BANEADO;
        }

        public Miembro(string email, string contrasenia, string nombre, string apellido, DateTime fechaNac, EstadoAdmin estadoAdmin = EstadoAdmin.NO_BANEADO)
        {
            SetEmail(email);
            SetContrasenia(contrasenia);
            this.nombre = nombre;
            this.apellido = apellido;
            this.fechaNac = fechaNac;
            bloqueado = false;
            listaAmigos = new List<Miembro>();
            this.estadoAdmin = estadoAdmin;
        }

        public bool EsAmigoDe(Miembro usuarioLogueado)
        {
            bool esAmigo = false;
            foreach (Miembro miembro in listaAmigos)
            {
                if (miembro.Email.Equals(usuarioLogueado.Email))
                {
                    esAmigo = true;
                }
            }
            return esAmigo;
        }
    }
}
