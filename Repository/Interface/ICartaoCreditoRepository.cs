using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    interface ICartaoCreditoRepository
    {
        int Inserir(CartaoCredito cartao);

        bool Atualizar(CartaoCredito categoria);

        bool Apagar(int id);

        CartaoCredito ObterPeloId(int id);

        List<CartaoCredito> ObterTodos(string busca);
    }
}
