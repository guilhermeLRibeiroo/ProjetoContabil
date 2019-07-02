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
    public class ClienteRepository : IClienteRepository
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
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM clientes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            return new Cliente() {
                Id = id,
                CPF = row["cpf"].ToString(),
                Nome = row["nome"].ToString(),
                IdContabilidade = Convert.ToInt32(row["id_contabilidade"]),
            };

        }

        public List<Cliente> ObterTodos(string busca)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT clientes.id AS 'Id', clientes.cpf AS 'CPF', clientes.nome AS 'Nome', contabilidades.Nome AS 'NomeContabilidade', contabilidades.Id AS 'IdContabilidade'
FROM clientes INNER JOIN contabilidades ON (clientes.id_contabilidade = contabilidades.id)";

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<Cliente> clientes = new List<Cliente>();
            
            foreach(DataRow row in table.Rows)
            {
                clientes.Add(new Cliente()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    CPF = row["CPF"].ToString(),
                    Nome = row["Nome"].ToString(),
                    IdContabilidade = Convert.ToInt32(row["IdContabilidade"]),
                    Contabilidade = new Contabilidade() { Id = Convert.ToInt32(row["IdContabilidade"]), Nome = row["NomeContabilidade"].ToString() }
                });
            }

            return clientes;
        }
    }
}
