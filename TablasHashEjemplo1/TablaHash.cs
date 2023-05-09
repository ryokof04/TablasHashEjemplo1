using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablasHashEjemplo1.TAD_Listas;

namespace TablasHashEjemplo1
{
    public class TablaHash
    {
        private ListaClaves claves;
        private ListaAutomoviles[] valores;
        private int valorclave;

        public TablaHash()
        {
            claves = new ListaClaves();
            valores = new ListaAutomoviles[1000];
            for (int i = 0; i < valores.Length; i++)
            {
                valores[i] = new ListaAutomoviles();
            }
        }

        public int TotClaves
        {
            get => claves.TotClaves;
        }

        public string[] VerLlaves()
        {
            return claves.VerConjuntoClaves();
        }

        private int Hashing(string llave)
        {
            //remueve la letra de la placa y convierte el resto de la llave
            //a un entero
            llave = llave.Remove(0, 1);
            int key = int.Parse(llave);
            key = key % 999;
            return key;
        }

        public bool ContieneClave(string ValorClave)
        {
            return claves.Existe(ValorClave);
        }

        public bool AgregarValor(string llave, Automovil Valor)
        {
            //Solo agregara par (key,valor) si key no existe previamente
            if (!claves.Existe(llave))
            {
                claves.Insertar(llave);
                valorclave = Hashing(llave);
                //ubica al Valor en lista correspondiente a
                //posicion [hash]
                if (valores[valorclave] == null)
                {
                    //Crea lista de (valores, claves) en posicion
                    valores[valorclave] = new ListaAutomoviles();
                }
                //agrega Valor (Nodo) al listado anterior
                valores[valorclave].Insertarl(Valor);
                return true; //clave y nodo fueron agregados a tabla hash
            }
            
            //clave y nodo no fueron agregados a tabla de dispersion
            return false;
        }

        public bool BorrarNodo(string ValorLlave)
        {
            //Intenta borrar nodo con clave recibida en parametro
            int ind;
            //Calcula el indice(hash) de posicion del vector valores.
            //Esta contiene una lista de Valores en donde eliminara al Nodo coincidente
            int key = Hashing(ValorLlave);
            if (claves.Existe(ValorLlave))
            {
                claves.BorrarClave(ValorLlave);
                ind = valores[key].BuscarAuto(ValorLlave);
                valores[key].RemoverAuto(ind);
                if (valores[key].TotNodos == 0)
                    valores[key] = null; // libera lista de nodos posicion (key)
                return true; // logro borrar nodo de la tabla de dispersion
            }
            return false; // no pudo borrar registro

        }          
            public Automovil AutomovilDe(string ValorLlave)
        {
            Automovil AutoTemp;
            int key;
            if (ContieneClave(ValorLlave))
            {
                // determina indice de posicion del vector de dispersion
                // de Valores (automoviles)
                key = Hashing(ValorLlave);
                // busca Automovil en lista de nodos del Listado de posicion
                // de vector valores[ ]
                AutoTemp = valores[key].RetornarAuto(ValorLlave);
                return AutoTemp;
            }
            return null;

        }

        public void ModificarNodo(string Clave, Automovil NuevoNodo)
        {
            //borra el nodo almacenado
            BorrarNodo(NuevoNodo.placa);
            //inserta nuevametne el nodo pero con sus datos modificados
            AgregarValor(NuevoNodo.placa, NuevoNodo);
        }
    }
}
