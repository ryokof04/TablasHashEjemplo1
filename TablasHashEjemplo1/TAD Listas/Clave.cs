using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TablasHashEjemplo1.TAD_Listas
{
    public class Clave
    {
        public string valor;
        public Clave sig;

        public Clave(string key)
        {
            valor = key;
            sig = null;
        }
    }
}
