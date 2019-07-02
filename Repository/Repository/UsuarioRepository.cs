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
    public class UsuarioRepository : IUsuarioRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM usuarios WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }

        public bool Atualizar(Usuario usuario)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "UPDATE usuarios SET login = @LOGIN,senha = @SENHA,data_nascimento = @DATA_NASCIMENTO WHERE id = @ID";
            comando.Parameters.AddWithValue("@LOGIN", usuario.Login);
            comando.Parameters.AddWithValue("@SENHA", usuario.Senha);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", usuario.DataNascimento);
            comando.Parameters.AddWithValue("@ID", usuario.Id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }

        public int Inserir(Usuario usuario)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO usuarios(login,senha,data_nascimento) OUTPUT INSERTED.ID VALUES (@LOGIN,@SENHA,@DATA_NASCIMENTO)";
            comando.Parameters.AddWithValue("@LOGIN", usuario.Login);
            comando.Parameters.AddWithValue("@SENHA", usuario.Senha);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", usuario.DataNascimento);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Usuario ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM usuarios WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tabela.Rows[0];
            Usuario usuario = new Usuario();

            usuario.Login = row["login"].ToString();
            usuario.Senha = row["senha"].ToString();
            usuario.DataNascimento = Convert.ToDateTime(row["data_nascimento"]);
            usuario.Id = Convert.ToInt32(row["id"].ToString());
            return usuario;
        }

        public List<Usuario> ObterTodos(string busca)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            busca = $"%{busca}%";
            comando.CommandText = "SELECT * FROM usuarios WHERE login LIKE @BUSCA ORDER BY id ASC";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Usuario> usuarios = new List<Usuario>();

            foreach (DataRow row in tabela.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.Login = row["login"].ToString();
                usuario.Senha = row["senha"].ToString();
                usuario.DataNascimento = Convert.ToDateTime(row["data_nascimento"]);
                usuario.Id = Convert.ToInt32(row["id"].ToString());
                usuarios.Add(usuario);
            }

            return usuarios;
        }
    }
}
