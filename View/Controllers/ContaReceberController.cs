using Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaReceberController : Controller
    {
        ContaReceberRepository repository;

        public ContaReceberController()
        {
            repository = new ContaReceberRepository();
        }

        // GET: ContaReceber
        public ActionResult Index()
        {
            ViewBag.ContasReceber = repository.ObterTodos("");
            return View();
        }

        public ActionResult Cadastrar()
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Clientes = clienteRepository.ObterTodos("");
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            ViewBag.Categorias = categoriaRepository.ObterTodos("");

            return View();
        }

        public ActionResult Store(string nome, string valor, int idCliente, int idCategoria, string dataPagamento)
        {
            repository.Inserir(new ContaReceber()
            {
                Nome = nome,
                Valor = Convert.ToDecimal(valor.Replace(".",",")),
                IdCategoria = idCategoria,
                IdCliente = idCliente,
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

            ViewBag.ContaReceber = repository.ObterPeloId(id);
            return View();
            
        }

        public ActionResult Update(int id, string nome, int idCategoria, int idCliente, string valor, string dataPagamento)
        {
            ContaReceber contaReceber = new ContaReceber()
            {
                Id = id,
                Nome = nome,
                Valor = Convert.ToDecimal(valor),
                IdCategoria = idCategoria,
                IdCliente = idCliente,
                DataPagamento = Convert.ToDateTime(dataPagamento)
            };

            repository.Atualizar(contaReceber);
            return RedirectToAction("Index");
        }
        
    }
}