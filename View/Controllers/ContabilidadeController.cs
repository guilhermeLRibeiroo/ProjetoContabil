using Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContabilidadeController : Controller
    {
        ContabilidadeRepository repository;

        public ContabilidadeController()
        {
            repository = new ContabilidadeRepository();
        }

        // GET: Categoria
        public ActionResult Index()
        {
            ViewBag.Contabilidades = repository.ObterTodos("");
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Store(string nome)
        {
            repository.Inserir(new Contabilidade() { Nome = nome });
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Contabilidade = repository.ObterPeloId(id);
            return View();
        }

        public ActionResult Update(int id, string nome)
        {
            repository.Atualizar(new Contabilidade() { Id = id, Nome = nome });
            return RedirectToAction("Index");
        }
    }
}