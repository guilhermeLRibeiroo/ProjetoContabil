using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    interface IContabilidadeRepository
    {
        int Inserir(Contabilidade contabilidade);

        bool Atualizar(Contabilidade contabilidade);

        bool Apagar(int id);

        Contabilidade ObterPeloId(int id);

        List<Contabilidade> ObterTodos(string busca);
    }
}
