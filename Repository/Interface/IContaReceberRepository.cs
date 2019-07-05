using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    interface IContaReceberRepository
    {
        bool Apagar(int id);

        bool Atualizar(ContaReceber contaReceber);

        int Inserir(ContaReceber contaReceber);

        ContaReceber ObterPeloId(int id);

        List<ContaReceber> ObterTodos(string busca);
    }
}
