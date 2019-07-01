using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Database
{
    public class Conexao
    {
        public static SqlCommand AbrirConexao()
        {
            SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            return comando;
        }
    }
}
