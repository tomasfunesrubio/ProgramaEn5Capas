using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Microsoft.VisualBasic;

namespace UltimaPracticaFinalCuentas
{
    public partial class Form1 : Form
    {
        BLL_Cuentas bllCuentas; 
        public Form1()
        {
            InitializeComponent();
            bllCuentas = new BLL_Cuentas();
            Mostrar(dataGridView1, bllCuentas.RetornarListaCuentas());
            Mostrar(dataGridView2, bllCuentas.RetornarListaCuentas());

        }

        public void Mostrar(DataGridView pDGV, object pO)
        {
            pDGV.DataSource = null;
            pDGV.DataSource = pO; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(userControl11.Texto == "") { throw new Exception("Ingrese un codigo por favor:"); }
                if(bllCuentas.RetornarListaCuentas().Exists(x0=> x0.Codigo == userControl11.Texto)) { throw new Exception("El codigo esta repetido"); }

                string codigo = userControl11.Texto;

                decimal Saldo = decimal.Parse(Interaction.InputBox("Ingrese el saldo:"));
                var x = MessageBox.Show("Es una cuenta corriente?", "", MessageBoxButtons.YesNo);

                Cuenta cuentaNueva; 
                if(x == DialogResult.Yes)
                {
                    decimal Acuerdo = decimal.Parse(Interaction.InputBox("Ingrese el acuerdo de descubierto: "));
                    cuentaNueva = new CuentaCorriente(codigo, Saldo, Acuerdo);
                }
                else
                {
                    cuentaNueva = new CajaDeAhorro(codigo, Saldo);
                }
                bllCuentas.Agregar(cuentaNueva);
                Mostrar(dataGridView1, bllCuentas.RetornarListaCuentas());
                Mostrar(dataGridView2, bllCuentas.RetornarListaCuentas());


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                Cuenta CuentaBorrar = dataGridView1.SelectedRows[0].DataBoundItem as Cuenta;
                bllCuentas.Borrar(CuentaBorrar);
                Mostrar(dataGridView1, bllCuentas.RetornarListaCuentas());
                Mostrar(dataGridView2, bllCuentas.RetornarListaCuentas());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Cuenta CuentaModificar = dataGridView1.SelectedRows[0].DataBoundItem as Cuenta;
                CuentaModificar.Saldo = decimal.Parse(Interaction.InputBox("Ingrese el saldo: "));
                if(CuentaModificar is CuentaCorriente)
                {
                    CuentaModificar.AcuerdoDescubierto = decimal.Parse(Interaction.InputBox("Ingrese el acuerdo"));
                }

                bllCuentas.Modificar(CuentaModificar);
                Mostrar(dataGridView1, bllCuentas.RetornarListaCuentas());
                Mostrar(dataGridView2, bllCuentas.RetornarListaCuentas());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>0)
            {
                Cuenta CuentaDeposito = dataGridView1.SelectedRows[0].DataBoundItem as Cuenta;
                decimal monto = decimal.Parse(Interaction.InputBox("Ingrese el monto a depositar: "));

                CuentaDeposito.Deposito(monto);
                bllCuentas.Deposito(CuentaDeposito);
                Mostrar(dataGridView1, bllCuentas.RetornarListaCuentas());
                Mostrar(dataGridView2, bllCuentas.RetornarListaCuentas());

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Cuenta CuentaDeposito = dataGridView1.SelectedRows[0].DataBoundItem as Cuenta;
                decimal monto = decimal.Parse(Interaction.InputBox("Ingrese el monto a extraer: "));

                CuentaDeposito.Extraer(monto);
                bllCuentas.Extraer(CuentaDeposito);
                Mostrar(dataGridView1, bllCuentas.RetornarListaCuentas());
                Mostrar(dataGridView2, bllCuentas.RetornarListaCuentas());

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Cuenta CuentaTrans = dataGridView1.SelectedRows[0].DataBoundItem as Cuenta;
                Cuenta CuentaDestino = dataGridView2.SelectedRows[0].DataBoundItem as Cuenta; 
                decimal monto = decimal.Parse(Interaction.InputBox("Ingrese el monto a transferir: "));

                CuentaTrans.Transferencia(monto, CuentaDestino);

                bllCuentas.Transferencia(CuentaTrans, CuentaDestino);
                Mostrar(dataGridView1, bllCuentas.RetornarListaCuentas());
                Mostrar(dataGridView2, bllCuentas.RetornarListaCuentas());

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Mostrar(dataGridView3, bllCuentas.ConsultaIncremental(textBox1.Text));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {

                Mostrar(dataGridView1, bllCuentas.OrdenarLista("ascendente"));

            }
            else
            {
                Mostrar(dataGridView1, bllCuentas.OrdenarLista("Descendente"));
            }
        }
    }
}
