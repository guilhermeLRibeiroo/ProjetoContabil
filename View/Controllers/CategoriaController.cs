using Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CategoriaController : Controller
    {
        CategoriaRepository repository;

        public CategoriaController()
        {
            repository = new CategoriaRepository();
        }

        // GET: Categoria
        public ActionResult Index()
        {
            ViewBag.Categorias = repository.ObterTodos("");
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Store(string nome)
        {
            repository.Inserir(new Categoria() { Nome = nome });
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Categoria = repository.ObterPeloId(id);
            return View();
        }

        public ActionResult Update(int id, string nome)
        {
            repository.Atualizar(new Categoria() { Id = id, Nome = nome });
            return RedirectToAction("Index");
        }
    }
}