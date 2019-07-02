using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ContabilidadeRepository : IContabilidadeRepository
    {
        public bool Apagar(int id)
        {
            throw new NotImplementedException();
        }

        public bool Atualizar(Contabilidade contabilidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Contabilidade contabilidade)
        {
            throw new NotImplementedException();
        }

        public Contabilidade ObterPeloId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Contabilidade> ObterTodos(string busca)
        {
            throw new NotImplementedException();
        }
    }
}
