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
    public class ContabilidadeRepository : IContabilidadeRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM contabilidades WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }

        public bool Atualizar(Contabilidade contabilidade)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "UPDATE contabilidades SET nome = @NOME WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", contabilidade.Nome);
            comando.Parameters.AddWithValue("@ID", contabilidade.Id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }

        public int Inserir(Contabilidade contabilidade)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO contabilidades OUTPUT INSERTED.ID VALUES (@NOME)";
            comando.Parameters.AddWithValue("@NOME", contabilidade.Nome);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Contabilidade ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contabilidades WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tabela.Rows[0];
            Contabilidade contabilidade = new Contabilidade();

            contabilidade.Nome = row["nome"].ToString();
            contabilidade.Id = Convert.ToInt32(row["id"].ToString());
            return contabilidade;
        }

        public List<Contabilidade> ObterTodos(string busca)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            busca = $"%{busca}%";
            comando.CommandText = "SELECT * FROM contabilidades WHERE nome LIKE @BUSCA ORDER BY id ASC";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Contabilidade> contabilidades = new List<Contabilidade>();

            foreach (DataRow row in tabela.Rows)
            {
                Contabilidade contabilidade = new Contabilidade();
                contabilidade.Nome = row["nome"].ToString();
                contabilidade.Id = Convert.ToInt32(row["id"].ToString());
                contabilidades.Add(contabilidade);
            }

            return contabilidades;
        }
    }
}
