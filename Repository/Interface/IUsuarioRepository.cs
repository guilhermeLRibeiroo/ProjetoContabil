using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    interface IUsuarioRepository
    {
        int Inserir(Usuario categoria);

        bool Atualizar(Usuario categoria);

        bool Apagar(int id);

        Usuario ObterPeloId(int id);

        List<Usuario> ObterTodos(string busca);
    }
}
