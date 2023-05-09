using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablasHashEjemplo1.TAD_Listas
{
    public class ListaAutomoviles
    {
        private Automovil primero = null;
        private int totnodos = 0;

        public ListaAutomoviles()
        {
            // Constructor vacío
        }

        public int TotNodos
        {
            get => totnodos;
        }

        public string[] ListaPlacas()
        {
            string[] placas = new string[totnodos];
            Automovil aux;
            int i = 0;
            aux = primero;
            while (aux != null)
            {
                placas[i] = aux.placa;
                i++;
                aux = aux.sig;
            }
            return placas;
        }

        public void Insertarl(Automovil Auto)
        {
            Automovil aux;
            Auto.CopiarEn(out aux);
            //inserta nodo al inicio del contenido de lista
            if (totnodos == 0)
                primero = aux;
            else
            {
                aux.sig = primero;
                primero = aux;
            }
            totnodos++; //incrementa conteo de nodos
        }

        public Automovil RetornarAuto(string Clave)
        {
            // ubica a nodo que coincide con Clave/Key (Num. Placa de automovil)
            // si lo encuentra, retorna una copia del mismo.
            Automovil aux;
            Automovil encontrado;
            if (totnodos > 0)
            {
                aux = primero;
                //inicia busqueda de nodo cuyo num. placa del automovil
                //coincida con la Clave usada por la tabla de dispersion
                while (aux != null)
                {
                    if (aux.placa == Clave) // coincide nodo
                    {
                        // hace copia de campos de nodo hacia a nodo a retornar (encontrado)
                        aux.CopiarEn(out encontrado);
                        //finaliza funcion, retornando nodo solicitado
                        return (encontrado);
                    }
                    aux = aux.sig; // mueve puntero a siguiente nodo de la lista.
                }
            }
            return null; // nodo solicitado no existe en la lista
        }

        public int BuscarAuto(string Clave)
        {
            int c = 1;
            Automovil aux;
            if (totnodos > 0)
            {
                aux = primero;

                //recorre todo el contenido de la lista
                while (aux != null)
                {
                    if (aux.placa == Clave)
                        return c; // retorna indice de posicion de nodo coincidente
                    c++; //actualiza conteo de indice de posicion
                    aux = aux.sig;
                }
            }
            return 0; //no encontro nodo coincidente con parametro recibido
        }

        public bool RemoverAuto(int indicenodo)
        {
            //borra nodo ubicado en posicion recibida en parametro
            Automovil aux = primero, aux2;
            if (totnodos > 0 && indicenodo <= totnodos)
            {
                if (indicenodo == 1) //primero
                    primero = primero.sig;
                else
                {
                    // ubica puntero auxiliar en nodo anterior al eliminar
                    for (int c = 1; c < indicenodo - 1; c++) aux = aux.sig;

                    aux2 = aux.sig; //ubica nodo a borrar
                    aux.sig = aux2.sig;
                    aux2 = null; //borra todo
                }
                totnodos--;
                return true; // fue eliminado de la lista

            }
            return false; //nodo en posicion no existe en lista
        }
    }
}
