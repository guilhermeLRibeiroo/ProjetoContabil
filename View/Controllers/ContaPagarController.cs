using Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaPagarController : Controller
    {
        ContaPagarRepository repository;

        public ContaPagarController()
        {
            repository = new ContaPagarRepository();
        }

        // GET: ContaPagar
        public ActionResult Index()
        {
            ViewBag.ContasPagar = repository.ObterTodos("");
            return View();
        }

        public ActionResult Cadastrar()
        {
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            ViewBag.Categorias = categoriaRepository.ObterTodos("");

            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Clientes = clienteRepository.ObterTodos("");

            return View();
        }

        public ActionResult Store(string nome, decimal valor, int idCategoria, int idCliente, string dataVencimento, string dataPagamento)
        {
            repository.Inserir(new ContaPagar()
            {
                Nome = nome,
                IdCategoria = idCategoria,
                IdCliente = idCliente,
                Valor = valor,
                DataVencimento = Convert.ToDateTime(dataVencimento),
                DataPagamento = Convert.ToDateTime(dataPagamento)
            });
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            ViewBag.Categorias = categoriaRepository.ObterTodos("");

            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Clientes = clienteRepository.ObterTodos("");

            ViewBag.ContaPagar = repository.ObterPeloId(id);
            return View();
        }

        public ActionResult Update(int id, string nome, int idCategoria, int idCliente, string valor, string dataVencimento, string dataPagamento)
        {
            repository.Atualizar(new ContaPagar() {
                Id = id,
                Nome = nome ,
                IdCategoria = idCategoria,
                IdCliente = idCliente,
                Valor = Convert.ToDecimal(valor.Replace(".",",")),
                DataVencimento = Convert.ToDateTime(dataVencimento),
                DataPagamento = Convert.ToDateTime(dataPagamento)
            });
            return RedirectToAction("Index");
        }
    }
}