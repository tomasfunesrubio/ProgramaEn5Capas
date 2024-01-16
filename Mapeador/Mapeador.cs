using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace Mapeador
{
    public class Mapeador_
    {
        DAL_Acceso Acceso;
        SqlCommand cm; 

        public Mapeador_()
        {
            Acceso = new DAL_Acceso();
            cm = Acceso.RetornarComando();
        }

        public List<Cuenta> RetornarListaDeCuentas(string consulta)
        {
            List<Cuenta> listaCuentas = new List<Cuenta>();

            cm.CommandText = consulta;
            SqlDataReader DataReader = cm.ExecuteReader();

            while (DataReader.Read())
            {
                if (decimal.Parse(DataReader[2].ToString())>0)
                {
                    listaCuentas.Add(new CuentaCorriente(DataReader[0].ToString(), decimal.Parse(DataReader[1].ToString()), decimal.Parse(DataReader[2].ToString())));
                }
                else
                {
                    listaCuentas.Add(new CajaDeAhorro(DataReader[0].ToString(), decimal.Parse(DataReader[1].ToString())));
                }
            }

            DataReader.Close();
            return listaCuentas;

        }


        public void Agregar(Cuenta pCuenta)
        {
            cm.CommandText = $"insert into cuentas(Codigo, saldo, acuerdoDescubierto) values('{pCuenta.Codigo}',{pCuenta.Saldo},{(pCuenta is CuentaCorriente? pCuenta.AcuerdoDescubierto: 0)})";
            cm.ExecuteNonQuery();
        }

        public void Borrar(Cuenta pCuenta)
        {
            cm.CommandText = $"delete from cuentas where codigo = '{pCuenta.Codigo}'";
            cm.ExecuteNonQuery();
        }

        public void Modificar(Cuenta pCuenta)
        {
            if(pCuenta is CuentaCorriente)
            {
                cm.CommandText = $"update cuentas set saldo = {pCuenta.Saldo}, AcuerdoDescubierto = {pCuenta.AcuerdoDescubierto} where codigo = '{pCuenta.Codigo}'";
            }
            else
            {
                cm.CommandText = $"update cuentas set saldo = {pCuenta.Saldo} where codigo = '{pCuenta.Codigo}'";
            }
            cm.ExecuteNonQuery();

        }

        public void Deposito(Cuenta pCuenta)
        {

            cm.CommandText = $"update cuentas set Saldo = {pCuenta.Saldo} where codigo = '{pCuenta.Codigo}'";
            cm.ExecuteNonQuery();

        }

        public void Extraer(Cuenta pCuenta)
        {

            cm.CommandText = $"update cuentas set Saldo = {pCuenta.Saldo} where codigo = '{pCuenta.Codigo}'";
            cm.ExecuteNonQuery();

        }

        public void Transferencia(Cuenta pCuentaTrans, Cuenta pCuentaDeposito)
        {
            cm.CommandText = $"update cuentas set saldo = {pCuentaTrans.Saldo} where codigo = '{pCuentaTrans.Codigo}'";
            cm.ExecuteNonQuery() ;

            cm.CommandText = $"update cuentas set saldo = {pCuentaDeposito.Saldo} where codigo = '{pCuentaDeposito.Codigo}'";
            cm.ExecuteNonQuery();

        }



    }
}
