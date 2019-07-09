using Model;
using Repository.Database;
using Repository.Interface;
using System;
using System.Collections.Generic;
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
        }

        public Compra ObterPeloId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Compra> ObterTodos(string busca)
        {
            throw new NotImplementedException();
        }
    }
}
