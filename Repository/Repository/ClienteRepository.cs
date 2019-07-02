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
    class ClienteRepository : IClienteRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM clientes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Atualizar(Cliente cliente)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "UPDATE clientes SET nome = @NOME, cpf = @CPF, id_contabilidade = @ID_CONTABILIDADE WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@ID_CONTABILIDADE", cliente.IdContabilidade);
            comando.Parameters.AddWithValue("@ID", cliente.Id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(Cliente cliente)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO clientes(nome, cpf, id_contabilidade) OUTPUT INSERTED.ID VALUES (@NOME, @CPF, @ID_CONTABILIDADE)";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@ID_CONTABILIDADE", cliente.IdContabilidade);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Cliente ObterPeloId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> ObterTodos(string busca)
        {
            throw new NotImplementedException();
        }
    }
}
