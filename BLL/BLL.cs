using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapeador;
using BE;
using System.Data;

namespace BLL
{
    public class BLL_Cuentas
    {
        Mapeador_ mp; 

        public BLL_Cuentas()
        {
            mp = new Mapeador_();
        }

        public List<Cuenta> RetornarListaCuentas(string consulta = "select * from cuentas")
        {
            return mp.RetornarListaDeCuentas(consulta);
        }

        public object ConsultaIncremental(string texto)
        {
            return mp.RetornarListaDeCuentas($"select * from cuentas where codigo like '{texto}%'");
        }

        public object OrdenarLista(string forma)
        {
            
            if(forma == "ascendente")
            {
               var consulta = from a in mp.RetornarListaDeCuentas("select * from cuentas")
                               orderby a.Codigo ascending
                               select a;
                return consulta.ToList();
            }
            else
            {
                var consulta = from a in mp.RetornarListaDeCuentas("select * from cuentas")
                                orderby a.Codigo descending
                                select a;
                return consulta.ToList();

            }

        }
        public void Agregar(Cuenta pCuenta)
        {
            mp.Agregar(pCuenta);
        }

        public void Borrar(Cuenta pCuenta)
        {
            mp.Borrar(pCuenta);
        }

        public void Modificar(Cuenta pCuenta)
        {
            mp.Modificar(pCuenta);
        }

        public void Deposito(Cuenta pCuenta)
        {
            mp.Deposito(pCuenta);
        }

        public void Extraer(Cuenta pCuenta)
        {
            mp.Extraer(pCuenta);
        }

        public void Transferencia(Cuenta pCuenta1 , Cuenta pCuenta2)
        {
            mp.Transferencia(pCuenta1, pCuenta2);
        }




    }
}
