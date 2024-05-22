using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.Dominio
{
    public class Invitacion
    {

        private static int contadorId = -1;

        public int id { get; }

        public DateTime fechaSolicitud { get; set; }

        private Miembro solicitante { get; set; }

        private Miembro solicitado { get; set; }

        public Estados estado { get; set; }
        public Miembro Solicitante
        {
            get { return solicitante; }
            set { solicitante = value; }
        }

        public Miembro Solicitado
        {
            get { return solicitado; }
            set { solicitado = value; }
        }

        public Miembro GetSolicitante()
        {
            return this.solicitante;
        }

        public Miembro GetSolicitado()
        {
            return this.solicitado;
        }

        public Invitacion()
        {
            this.fechaSolicitud = DateTime.Now;
        }

        public Invitacion(DateTime fechaSolicitud, Miembro solicitante, Miembro solicitado, Estados estado)
        {
            contadorId++;
            id = contadorId;
            this.fechaSolicitud = fechaSolicitud;
            this.solicitante = solicitante;
            this.solicitado = solicitado;
            this.estado = estado;
        }
    }
}