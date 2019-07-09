using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    interface ICompraRepository
    {
        int Inserir(Compra compra);

        bool Atualizar(Compra compra);

        bool Apagar(int id);

        Compra ObterPeloId(int id);

        List<Compra> ObterTodos(string busca);
    }
}
