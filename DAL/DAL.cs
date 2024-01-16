using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DAL
{
    public class DAL_Acceso
    {
        SqlConnection co;
        SqlCommand cm; 

        public DAL_Acceso() 
        {
            co = new SqlConnection("Data Source=.;Initial Catalog=\"Ultima Practica Final\";Integrated Security=True");
            cm = new SqlCommand("select * from cuentas",co);
            co.Open();
        
        }

        public SqlCommand RetornarComando()
        {
            return cm;
        }


    }
}
