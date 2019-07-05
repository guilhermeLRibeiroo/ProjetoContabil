using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContaReceber
    {
        public int Id;
        public int IdCliente;
        public int IdCategoria;

        public string Nome;
        public decimal Valor;

        public DateTime DataPagamento;

        public Cliente Cliente;
        public Categoria Categoria;
    }
}
