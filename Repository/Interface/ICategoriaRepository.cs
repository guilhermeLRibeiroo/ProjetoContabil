using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    interface ICategoriaRepository
    {
        int Inserir(Categoria categoria);

        bool Atualizar(Categoria categoria);

        bool Apagar(int id);

        Categoria ObterPeloId(int id);

        List<Categoria> ObterTodos(string busca);
    }
}
