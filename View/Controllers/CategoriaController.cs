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
    }
}