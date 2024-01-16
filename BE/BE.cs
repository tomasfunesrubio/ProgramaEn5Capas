using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BE
{
    public abstract class Cuenta
    {
        public Cuenta(string pCodigo, decimal pSaldo)
        {
            Codigo = pCodigo;
            Saldo = pSaldo;
        }
        public string Codigo { get; set; }

        public decimal Saldo { get; set; }

        public decimal AcuerdoDescubierto { get; set; }

        public void Deposito(decimal pMonto)
        {
            Saldo += pMonto;
        }

        public abstract void Extraer(decimal pMonto);

        public abstract void Transferencia(decimal pMonto, Cuenta pCuenta);

    }

    public class CuentaCorriente: Cuenta
    {
        public CuentaCorriente(string pCodigo, decimal pSaldo, decimal pAcuerdo): base(pCodigo,pSaldo)
        {
            AcuerdoDescubierto = pAcuerdo;
        }

        public override void Extraer(decimal pMonto)
        {
            decimal verificar = (Saldo - pMonto) - 250;
            if(verificar >= AcuerdoDescubierto*-1)
            {
                Saldo -= pMonto;
                Saldo -= 250;
            }
            else
            {
                MessageBox.Show("No hay fondos ");
            }


        }

        public override void Transferencia(decimal pMonto, Cuenta pCuenta)
        {
            if((Saldo-pMonto) >= AcuerdoDescubierto*-1)
            {
                Saldo -= pMonto;
                pCuenta.Saldo += pMonto;
            }
            else
            {
                MessageBox.Show("No hay fondos");
            }
        }


    }



    public class CajaDeAhorro : Cuenta
    {
        public CajaDeAhorro(string pCodigo, decimal pSaldo) : base(pCodigo, pSaldo)
        {

        }

        public override void Extraer(decimal pMonto)
        {
           
            if(((Saldo -pMonto)-100)>=0)
            {
                Saldo -= pMonto; 
                Saldo-=100;
            }
            else
            {
                MessageBox.Show("No hay fondos");
            }
        }

        public override void Transferencia(decimal pMonto, Cuenta pCuenta)
        {
            if(Saldo >= pMonto )
            {
                Saldo -= pMonto;
                pCuenta.Saldo += pMonto; 
            }
            else
            {
                MessageBox.Show("No hay fondos");
            }


        }

    }

}
