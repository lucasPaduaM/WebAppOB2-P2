using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.Dominio
{
    public class Administrador : Usuario
    {
        public Administrador(string email, string contrasenia)
        {
            SetEmail(email);
            SetContrasenia(contrasenia);
        }
    }
}