using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContaPagar
    {
        public int Id, IdCategoria, IdCliente;
        public Categoria Categoria;
        public Cliente Cliente;
        public string Nome;
        public decimal Valor;
        public DateTime DataVencimento, DataPagamento;
    }
}
