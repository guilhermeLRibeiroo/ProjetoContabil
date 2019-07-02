using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class UsuarioController : Controller
    {

        UsuarioRepository repository;


        public UsuarioController()
        {
            repository = new UsuarioRepository();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            ViewBag.Usuarios = repository.ObterTodos("");
            return View();
        }
    }
}