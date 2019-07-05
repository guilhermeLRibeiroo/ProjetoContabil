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
    public class ContaReceberRepository : IContaReceberRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM contas_receber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }

        public bool Atualizar(ContaReceber contaReceber)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE contas_receber SET
id_cliente = @ID_CLIENTE,
id_categoria = @ID_CATEGORIA,
nome = @NOME,
valor = @VALOR,
data_pagamento = @DATA_PAGAMENTO 
WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaReceber.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaReceber.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaReceber.DataPagamento);
            comando.Parameters.AddWithValue("@ID", contaReceber.Id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(ContaReceber contaReceber)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO contas_receber(nome,data_pagamento,valor,id_cliente,id_categoria) OUTPUT INSERTED.ID VALUES (@NOME,@DATA_PAGAMENTO,@VALOR,@ID_CLIENTE,@ID_CATEGORIA)";
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaReceber.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaReceber.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaReceber.IdCategoria);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public ContaReceber ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contas_receber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tabela.Rows[0];
            ContaReceber contaReceber = new ContaReceber();

            contaReceber.Id = Convert.ToInt32(row["id"]);
            contaReceber.Nome = row["nome"].ToString();
            contaReceber.DataPagamento = Convert.ToDateTime(row["data_pagamento"].ToString());
            contaReceber.Valor = Convert.ToDecimal(row["valor"].ToString());
            contaReceber.IdCliente = Convert.ToInt32(row["id_cliente"].ToString());
            contaReceber.IdCategoria = Convert.ToInt32(row["id_categoria"].ToString());

            return contaReceber;
        }

        public List<ContaReceber> ObterTodos(string busca)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT clientes.id AS 'IdCliente',
clientes.nome AS 'NomeCliente',
categorias.id AS 'IdCategoria', 
categorias.nome AS 'NomeCategoria',
contas_receber.id AS 'id', 
contas_receber.nome 'nome',
contas_receber.data_pagamento AS 'data_pagamento',
contas_receber.valor 'valor' 
FROM contas_receber
INNER JOIN clientes ON (contas_receber.id_cliente = clientes.id)
INNER JOIN categorias ON (contas_receber.id_categoria = categorias.id)";

            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<ContaReceber> contasReceber = new List<ContaReceber>();

            foreach (DataRow row in tabela.Rows)
            {
                Categoria categoria = new Categoria();
                categoria.Id = Convert.ToInt32(row["IdCategoria"]);
                categoria.Nome = row["NomeCategoria"].ToString();

                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(row["IdCliente"]);
                cliente.Nome = row["NomeCliente"].ToString();

                ContaReceber contaReceber = new ContaReceber();
                contaReceber.Nome = row["nome"].ToString();
                contaReceber.DataPagamento = Convert.ToDateTime(row["data_pagamento"].ToString());
                contaReceber.Valor = Convert.ToDecimal(row["valor"].ToString());

                contaReceber.IdCategoria = Convert.ToInt32(row["IdCategoria"].ToString());
                contaReceber.IdCliente = Convert.ToInt32(row["IdCliente"].ToString());

                contaReceber.Cliente = cliente;
                contaReceber.Categoria = categoria;
                
                contaReceber.Id = Convert.ToInt32(row["id"].ToString());

                contasReceber.Add(contaReceber);
            }
            return contasReceber;
        }
    }
}
