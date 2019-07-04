using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    interface IContaPagarRepository
    {
        int Inserir(ContaPagar contaPagar);

        bool Atualizar(ContaPagar contaPagar);

        bool Apagar(int id);

        ContaPagar ObterPeloId(int id);

        List<ContaPagar> ObterTodos(string busca);
    }
}
