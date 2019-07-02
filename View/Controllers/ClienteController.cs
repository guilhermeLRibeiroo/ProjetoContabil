using Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ClienteController : Controller
    {
        ClienteRepository repository;

        public ClienteController()
        {
            repository = new ClienteRepository();
        }

        // GET: Cliente
        public ActionResult Index()
        {
            ViewBag.Clientes = repository.ObterTodos("");
            return View();
        }

        public ActionResult Cadastrar()
        {
            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            ViewBag.Contabilidades = contabilidadeRepository.ObterTodos("");
            return View();
        }

        public ActionResult Store(string nome,string cpf, int idContabilidade)
        {
            repository.Inserir(new Cliente() { Nome = nome, CPF = cpf, IdContabilidade = idContabilidade });
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            ViewBag.Contabilidades = contabilidadeRepository.ObterTodos("");
            ViewBag.Cliente = repository.ObterPeloId(id);
            return View();
        }

        public ActionResult Update(int id, string nome, string cpf, int idContabilidade)
        {
            repository.Atualizar(new Cliente() { Nome = nome, CPF = cpf, IdContabilidade = idContabilidade, Id = id });
            return RedirectToAction("Index");
        }
    }
}