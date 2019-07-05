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
    public class CartaoCreditoRepository : ICartaoCreditoRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM cartoes_credito WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Atualizar(CartaoCredito cartao)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "UPDATE cartoes_credito SET id_cliente = @ID_CLIENTE, numero = @NUMERO, data_vencimento = @DATA_VENCIMENTO,cvv = @CVV WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID_CLIENTE", cartao.IdCliente);
            comando.Parameters.AddWithValue("@NUMERO", cartao.Numero);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", cartao.DataVencimento);
            comando.Parameters.AddWithValue("@CVV", cartao.CVV);
            comando.Parameters.AddWithValue("@ID", cartao.Id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(CartaoCredito cartao)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO cartoes_credito
            (id_cliente, numero, data_vencimento, cvv)
            OUTPUT INSERTED.ID 
            VALUES 
            (@ID_CLIENTE,@NUMERO,@DATA_VENCIMENTO,@CVV)";
            comando.Parameters.AddWithValue("@ID_CLIENTE", cartao.IdCliente);
            comando.Parameters.AddWithValue("@NUMERO", cartao.Numero);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", cartao.DataVencimento);
            comando.Parameters.AddWithValue("@CVV", cartao.CVV);
            int id = Convert.ToInt32(comando.ExecuteScalar());

            comando.Connection.Close();

            return id;
        }

        public CartaoCredito ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM cartoes_credito WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID",id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tabela.Rows[0];
            CartaoCredito cartao = new CartaoCredito();

            cartao.IdCliente = Convert.ToInt32(row["id_cliente"].ToString());
            cartao.Numero = row["numero"].ToString();
            cartao.DataVencimento = Convert.ToDateTime(row["data_vencimento"]);
            cartao.CVV = row["cvv"].ToString();


            return cartao;
        }

        public List<CartaoCredito> ObterTodos(string busca)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            busca = $"%{busca}%";

            comando.CommandText = @"SELECT 
clientes.id AS 'ClienteId',
clientes.nome AS 'ClienteNome',
cartoes_credito.id AS 'id',
cartoes_credito.numero AS 'numero',
cartoes_credito.data_Vencimento AS 'data_vencimento',
cartoes_credito.cvv AS 'cvv'
FROM cartoes_credito
INNER JOIN clientes ON(cartoes_credito.id_cliente = clientes.id)";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<CartaoCredito> cartaoCredito = new List<CartaoCredito>();

            foreach (DataRow row in tabela.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(row["ClienteId"]);
                cliente.Nome = row["ClienteNome"].ToString();

                CartaoCredito credito = new CartaoCredito();
                credito.Cliente = cliente;
                credito.Numero = row["numero"].ToString();
                credito.DataVencimento = Convert.ToDateTime(row["data_vencimento"]);
                credito.CVV = row["cvv"].ToString();

                credito.Id = Convert.ToInt32(row["id"].ToString());
                cartaoCredito.Add(credito);
            }

            return cartaoCredito;
        }

    }
}
