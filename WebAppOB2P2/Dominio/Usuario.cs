using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.Dominio
{
    public class Usuario
    {

        private string _email;

        private string _contrasenia;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Contrasenia
        {
            get { return _contrasenia; } 
            set { _contrasenia = value;}
        }

        public string GetEmail()
        {
            return _email;
        }

        public string GetContrasenia()
        {
            return _contrasenia;
        }

        public void SetEmail(string email)
        {
            _email = email;
        }
        public void SetContrasenia(string contrasenia)
        {
            _contrasenia = contrasenia;
        }
    }
}