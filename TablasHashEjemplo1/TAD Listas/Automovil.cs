using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TablasHashEjemplo1.TAD_Listas
{
    public class Automovil
    {
        public string placa = "sin asignar";
        public string marca = "sin asignar";
        public int año = 1980;
        public Color color = Color.Black;
        public bool tienemultas = false;
        public Automovil sig = null;

        public Automovil()
        {
            // Constructor vacío
        }

        public void CopiarEn(out Automovil copiaauto)
        {
            // realiza una copia de valores actuales de sus campos
            // hacia una nueva instancia
            copiaauto = new Automovil();

            copiaauto.placa = this.placa;
            copiaauto.marca = this.marca;
            copiaauto.año = this.año;
            copiaauto.color = this.color;
            copiaauto.tienemultas = this.tienemultas;
        }
    }
}
