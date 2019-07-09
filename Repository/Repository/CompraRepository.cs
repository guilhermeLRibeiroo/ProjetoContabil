using Model;
using Repository.Database;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    class CompraRepository : ICompraRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM compras WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }

        public bool Atualizar(Compra compra)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE compras SET
id_cartao_credito = @ID_CARTAO_CREDITO,
valor = @VALOR,
data_compra = @DATA_COMPRA
WHERE id = @ID";

            comando.Parameters.AddWithValue("@ID_CARTAO_CREDITO", compra.IdCartaoCredito);
            comando.Parameters.AddWithValue("@VALOR", compra.Valor);
            comando.Parameters.AddWithValue("@DATA_COMPRA", compra.DataCompra);
            comando.Parameters.AddWithValue("@ID", compra.Id);

            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;

        }

        public int Inserir(Compra compra)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO compras(id_cartao_credito,valor,data_compra) OUTPUT INSERTED.ID VALUES(@ID_CARTAO_CREDITO,@VALOR,@DATA_COMPRA)";
            comando.Parameters.AddWithValue("@ID_CARTAO_CREDITO", compra.IdCartaoCredito);
            comando.Parameters.AddWithValue("@VALOR", compra.Valor);
            comando.Parameters.AddWithValue("@DATA_COMPRA", compra.DataCompra);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Compra ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM compras WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if(tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tabela.Rows[0];
            Compra compra = new Compra();

            compra.Id = Convert.ToInt32(row["id"].ToString());
            compra.IdCartaoCredito = Convert.ToInt32(row["id_cartao_credito"].ToString());
            compra.Valor = Convert.ToDecimal(row["valor"].ToString());
            compra.DataCompra = Convert.ToDateTime(row["Data_compra"].ToString());

            return compra;
        }

        public List<Compra> ObterTodos(string busca)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT cartoes_credito.id AS 'IdCartao',
cartoes_credito.numero AS 'NumeroCartao',
compras.id AS 'Id',
compras.valor AS 'Valor',
compras.data_compra AS 'DataCompra' 
FROM compras
INNER JOIN cartoes_credito ON ( compras.id_cartao_credito = cartao_credito.id)";

            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Compra> compras = new List<Compra>();
            foreach (DataRow row in tabela.Rows)
            {
                CartaoCredito cartaoCredito = new CartaoCredito();
                cartaoCredito.Id = Convert.ToInt32(row["IdCartao"]);
                cartaoCredito.Numero = row["NumeroCartao"].ToString();

                Compra compra = new Compra();
                compra.Valor = Convert.ToDecimal(row["valor"].ToString());
                compra.DataCompra = Convert.ToDateTime(row["data_compra"].ToString());

                compra.IdCartaoCredito= Convert.ToInt32(row["IdCartao"].ToString());

                compra.CartaoCredito = cartaoCredito;

                compra.Id = Convert.ToInt32(row["id"].ToString());

                compras.Add(compra);
            }
            return compras;

        }
    }
}
