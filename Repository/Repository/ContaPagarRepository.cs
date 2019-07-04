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
    public class ContaPagarRepository : IContaPagarRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM contas_pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Atualizar(ContaPagar contaPagar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE contas_pagar SET
id_cliente = @ID_CLIENTE,
id_categoria = @ID_CATEGORIA,
nome = @NOME,
valor = @VALOR,
data_pagamento = @DATA_PAGAMENTO,
data_vencimento = @DATA_VENCIMENTO WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaPagar.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaPagar.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaPagar.DataPagamento);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", contaPagar.DataVencimento);
            comando.Parameters.AddWithValue("@ID", contaPagar.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(ContaPagar contaPagar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO contas_pagar(id_cliente, id_categoria, nome, valor, data_vencimento, data_pagamento) OUTPUT INSERTED.ID
VALUES (@ID_CLIENTE, @ID_CATEGORIA, @NOME, @VALOR, @DATA_VENCIMENTO, @DATA_PAGAMENTO)";
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaPagar.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaPagar.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaPagar.DataPagamento);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", contaPagar.DataVencimento);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            return id;
        }

        public ContaPagar ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contas_pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tabela.Rows[0];
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Id = id;
            contaPagar.IdCategoria = Convert.ToInt32(row["id_categoria"]);
            contaPagar.IdCliente = Convert.ToInt32(row["id_cliente"]);
            contaPagar.Nome = row["nome"].ToString();
            contaPagar.Valor = Convert.ToDecimal(row["valor"]);
            contaPagar.DataPagamento = Convert.ToDateTime(row["data_pagamento"].ToString());
            contaPagar.DataVencimento = Convert.ToDateTime(row["data_vencimento"].ToString());

            return contaPagar;
        }

        public List<ContaPagar> ObterTodos(string busca)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT

contas_pagar.id AS 'id', contas_pagar.id_categoria AS 'id_categoria', contas_pagar.id_cliente AS 'id_cliente',
contas_pagar.nome AS 'nome', contas_pagar.valor AS 'valor', contas_pagar.data_pagamento AS 'data_pagamento',
contas_pagar.data_vencimento AS 'data_vencimento',
clientes.nome AS 'NomeCliente', categorias.nome AS 'NomeCategoria' 
FROM contas_pagar
INNER JOIN clientes ON (contas_pagar.id_cliente = clientes.id)
INNER JOIN categorias ON (contas_pagar.id_categoria = categorias.id)";
            comando.Parameters.AddWithValue("@BUSCA", $"%{busca}%");

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            List<ContaPagar> contasPagar = new List<ContaPagar>();

            foreach (DataRow row in tabela.Rows)
            {
                Categoria categoria = new Categoria();
                categoria.Id = Convert.ToInt32(row["id_categoria"]);
                categoria.Nome = row["NomeCategoria"].ToString();

                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(row["id_cliente"]);
                cliente.Nome = row["NomeCliente"].ToString();

                ContaPagar contaPagar = new ContaPagar();

                contaPagar.Cliente = cliente;
                contaPagar.Categoria = categoria;

                contaPagar.Id = Convert.ToInt32(row["id"]);
                contaPagar.IdCategoria = Convert.ToInt32(row["id_categoria"]);
                contaPagar.IdCliente = Convert.ToInt32(row["id_cliente"]);

                contaPagar.Nome = row["nome"].ToString();
                contaPagar.Valor = Convert.ToDecimal(row["valor"]);

                contaPagar.DataPagamento = Convert.ToDateTime(row["data_pagamento"].ToString());
                contaPagar.DataVencimento = Convert.ToDateTime(row["data_vencimento"].ToString());

                contasPagar.Add(contaPagar);
            }
            return contasPagar;
        }
    }
}
