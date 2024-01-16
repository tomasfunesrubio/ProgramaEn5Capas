using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BLL;
using BE; 

namespace ControlPersonalizado
{
    public partial class UserControl1: UserControl
    {
        BLL_Cuentas bLL;
        public UserControl1()
        {
            InitializeComponent();
            textBox1.MaxLength = 8;
            bLL = new BLL_Cuentas();
            Texto = "";
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        public string Texto { get; set;  }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 8)
            {
                Regex re = new Regex(@"\d{4}[-][a-zA-Z]{3}");
                if(re.IsMatch(textBox1.Text))
                {
                    if(!(bLL.RetornarListaCuentas().Exists(x=> x.Codigo == textBox1.Text)))
                    {
                        Texto = textBox1.Text;
                    }
                    else
                    {
                        MessageBox.Show("El codigo ya existe");
                        textBox1.Text = "";
                        Texto = "";
                    }


                }
                else
                {
                    textBox1.Text = "";
                    Texto = "";
                    MessageBox.Show("El codigo no tiene el formato correcto");
                }


            }
        }
    }
}
