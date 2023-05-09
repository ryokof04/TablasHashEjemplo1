using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablasHashEjemplo1.TAD_Listas
{
    public class ListaClaves
    {
        private int totnodos = 0;
        private Clave inicio = null;

        public ListaClaves()
        {
            // Constructor vacío
        }

        public int TotClaves
        {
            get => totnodos;

        }

        public string[] VerConjuntoClaves()
        {
            string[] llaves = new string[totnodos];
            int i = 0;
            //recorre contenido de nodos de lista y los almacena en vector

            Clave aux = inicio;
            while (aux != null)
            {
                llaves[i++] = aux.valor;
                aux = aux.sig;
            }
            return llaves;

        }

        public bool Existe(string ValorClave)
        {
            //Determina si la clave recibida ya existe en el listado
            Clave aux;
            if (totnodos > 0)
            {
                aux = inicio;
                while (aux != null)
                {
                    if (aux.valor == ValorClave)
                        return true; // clave ya existe en el listado
                    aux = aux.sig;
                }
            }
            return false; // el listado esta vacio o clave aun no existe.
        }

        public bool Insertar(string ValorClave)
        {
            // intenta insertar la clave recibida
            // esta clave sera recibida unicamente si aun no existe en el listado
            Clave nue;
            if (!Existe(ValorClave))
            {
                if (inicio == null)
                    inicio = new Clave(ValorClave);
                else
                {
                    //lo agrega al inicio del contenido
                    nue = new Clave(ValorClave);
                    nue.sig = inicio;
                    inicio = nue;
                }
                totnodos++;
                return true; // fue agregado al listado
            }
            return false;
        }

        public int BuscarPosicDe(string ValorClave)
        {
            //retorna posicion del nodo con ValorClave recibido (si existe)
            int pos = 1; //asume que Clave esta en 1er nodo de la lista
            Clave aux;
            aux = inicio;
            while (aux != null)
            {
                if (aux.valor == ValorClave)
                    //finaliza funcion, retornando posicion donde se encuentra Clave
                    return pos;
                aux = aux.sig;
                pos++;
            }
            return 0; //no existe Clave en el listado
        }

        public bool BorrarClave(string ValorClave)
        {
            int pos;
            Clave ant, borrar;
            pos = BuscarPosicDe(ValorClave);
            if (pos > 0)
            {
                if (pos == 1)
                    inicio = inicio.sig;
                else
                {
                    // ubica punteros (borrar, ant) en posic a borrar y
                    //la anterior a ella, respectivamente
                    ant = inicio;
                    borrar = ant.sig;
                    while (borrar != null)
                    {
                        if (borrar.valor == ValorClave)
                        {
                            ant.sig = borrar.sig;
                            break; //finaliza el ciclo while
                        }
                        // desplaza una posicion a ambos punteros
                        ant = borrar;
                        borrar = borrar.sig;
                    }
                }
                totnodos--;
                return true; //borro clave indicada
            }
            return false; //no se pudo borrar clave (key)
        }
    }
}
