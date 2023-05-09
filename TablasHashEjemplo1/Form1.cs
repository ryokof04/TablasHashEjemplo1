using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using TablasHashEjemplo1.TAD_Listas;

namespace TablasHashEjemplo1
{
    public partial class Form1 : Form
    {
        //objeto de la clase del TAD TablaHash
        TablaHash tablahash;
        public Form1()
        {
            InitializeComponent();
            tablahash = new TablaHash();
            inicializarControles();
            ControlesNuevoNodo();
            ListarClaves();
        }
        public void inicializarControles()
        {
            //inicializa contenido de controles para info. automovil
            mtbPlaca.Clear();
            txtMarca.Clear();
            nudYear.Value = nudYear.Minimum;
            chkMultas.Checked = false;
            picColor.BackColor = Color.Black;
            mtbPlaca.Focus();
        }
        public void ListarClaves()
        {
            
        //Recibe de la Tabla Hash a un vector string con las llaves de sus nodos almacenados
 string[] p;
            lsbClaves.Items.Clear();
            if (tablahash.TotClaves > 0)
            {
                p = tablahash.VerLlaves();
                foreach (string clave in p) //recorre vector para imprimir llaves en listbox
                    lsbClaves.Items.Add(clave);
            }
            else
                lsbClaves.Items.Add("TABLA SIN NODOS");
        }
        private void ControlesNuevoNodo()
        {
            //activa/desactiva controles para permitir agregar nodos o
            //buscar a uno ya existente
            inicializarControles();
            btnCrear.Enabled = true;
            btnBuscar.Enabled = true;
            txtPlacaB.Clear();
            txtPlacaB.Enabled = true;
            btnModificar.Enabled = false;
            btnIgnorarC.Enabled = false;
            btnEliminar.Enabled = false;
            mtbPlaca.Clear();
            mtbPlaca.Enabled = true;
            mtbPlaca.Focus();
        }
        private void ControlesModificarNodo()
        {
            //activa/desactiva controles para permitir modificar
            //a un nodo existe en tabla hash o borrarlo de la tabla
            btnCrear.Enabled = false;
            btnBuscar.Enabled = false;
            mtbPlaca.Enabled = false;
            txtPlacaB.Enabled = true;
            btnModificar.Enabled = true;
            btnIgnorarC.Enabled = true;
            btnEliminar.Enabled = true;

            txtMarca.Focus();
        }
        public Automovil GenerarNodoAutomovil()
        {
            Automovil CarroTemp = new Automovil();
            CarroTemp.placa = mtbPlaca.Text;
            CarroTemp.marca = txtMarca.Text;
            CarroTemp.año = Convert.ToInt32(nudYear.Value);
            CarroTemp.color = picColor.BackColor;
            CarroTemp.tienemultas = chkMultas.Checked;
            return CarroTemp;
        }
        public void PresentarAutomovil(Automovil AutoTemp)
        {
            mtbPlaca.Text = AutoTemp.placa;
            txtMarca.Text = AutoTemp.marca;
            nudYear.Value = AutoTemp.año;
            chkMultas.Checked = AutoTemp.tienemultas;
            picColor.BackColor = AutoTemp.color;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            //intenta registrar un Vehiculo en la Tabla Hash
            //toma los datos de controles del form y crea objeto de clase Automovil

            Automovil AutoMovilTemp = GenerarNodoAutomovil();

            if (tablahash.ContieneClave(AutoMovilTemp.placa))
            {
                MessageBox.Show("La Placa" + AutoMovilTemp.placa + "ya esta registrada, debe definir otro numero de placa");
                mtbPlaca.Focus();
            }
            else
            {
                //agrega automovil a tabla hash
                //utilizando como llave su numero de placa
                tablahash.AgregarValor(AutoMovilTemp.placa, AutoMovilTemp);
                //en form, actauliza a lista de claves de nodos almacenados
                ListarClaves();

                //Limpia controlesde ingreso de datos de automovil
                ControlesNuevoNodo();
            }
        }

        private void picColor_Click(object sender, EventArgs e)
        {
            // permite al usuario elegir el color para el automovil
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Color ncolor = dlg.Color;
                picColor.BackColor = ncolor;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //recupera nodo especifico de la tabla hash, usando su llave/key
            txtPlacaB.Text.Trim();
            string placabusqueda = txtPlacaB.Text;
            if (placabusqueda.Length > 2)
            {
                //invoca a metodo de la tabla hash para recuperar un objeto Automovil
                //segun la llave enviada en su argumento
                Automovil Carro = tablahash.AutomovilDe(placabusqueda);
                //si la llave no tiene asociado un nodo, el metodo retorna un objeto null
                if (Carro == null)
                    MessageBox.Show("No existe un automovil registrado con la placa " +
                    placabusqueda);
                else
                {
                    //presenta en form a campos del objeto Automovil devuelto por tabla hash
                    PresentarAutomovil(Carro);
                    ControlesModificarNodo();
                }
            }
            else
            {
                MessageBox.Show("Primero debe ingresar un numero de placa",
                "Uso de tabla hash", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
                txtPlacaB.Clear();
                txtPlacaB.Focus();
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Intenta modificar campos del automovil recuperado de tabla hash
            DialogResult r; //opcion elegida de un cuadro dialogo MessageBox
                            //genera un objeto de clase Automovil con info. ya actualizada
                            //de controles del form
            Automovil AutoMovilTemp = GenerarNodoAutomovil();
            r = MessageBox.Show("Desea guardar cambios realizados al automovil con placa " +
             AutoMovilTemp.placa + " ? ", "Uso de Tabla Hash",
             MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                //procede a modificar nodo existente en tabla Hash
                tablahash.ModificarNodo(AutoMovilTemp.placa, AutoMovilTemp);
                //limpia los controles de edicion
                ControlesNuevoNodo();
            }


        }

        private void btnIgnorarC_Click(object sender, EventArgs e)
        {
            //limpia los controles de edicion
            ControlesNuevoNodo();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Borra a un nodo existente en tabla hash, utilizando su llave acceso
            DialogResult r;
            string placabusqueda = mtbPlaca.Text;
            r = MessageBox.Show("Confirme si desea borrar este automovil con placa " +
             placabusqueda + " ? ", "Uso de Tabla Hash",
             MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                //borra nodo de tabla Hash
                tablahash.BorrarNodo(placabusqueda);
                //actualiza lista en form con conjunto de llaves almacenadas
                ListarClaves();
                //limpia los controles de edicion
                ControlesNuevoNodo();
            }


        }
    }
}
