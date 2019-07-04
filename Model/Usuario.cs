using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Usuario
    {
        public int Id;
        public string Login;
        public string Senha;
        public DateTime DataNascimento;
        public int IdContabilidade;
        public Contabilidade contabilidade;
    }
}
