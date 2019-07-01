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
    public class CategoriaRepository : ICategoriaRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Atualizar(Categoria categoria)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "UPDATE categorias SET nome = @NOME WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            comando.Parameters.AddWithValue("@ID", categoria.Id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(Categoria categoria)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO categorias OUTPUT INSERTED.ID VALUES (@NOME)";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Categoria ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM categorias";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tabela.Rows[0];
            Categoria categoria = new Categoria();

            categoria.Nome = row["nome"].ToString();
            categoria.Id = Convert.ToInt32(row["id"]);
            return categoria;
        }

        public List<Categoria> ObterTodos(string busca)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            busca = $"%{busca}%";
            comando.CommandText = "SELECT * FROM categorias WHERE nome LIKE @BUSCA ORDER BY nome ASC";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<Categoria> categorias = new List<Categoria>();
            foreach (DataRow row in tabela.Rows)
            {
                Categoria categoria = new Categoria();
                categoria.Nome = row["nome"].ToString();
                categoria.Id = Convert.ToInt32(row["id"]);
                categorias.Add(categoria);
            }
            return categorias;
        }
    }
}
