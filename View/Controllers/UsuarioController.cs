using Model;
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

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Store(string login, string senha, DateTime dataNascimento)
        {
            Usuario usuario = new Usuario();
            usuario.Login = login;
            usuario.Senha = senha;
            usuario.DataNascimento = dataNascimento;

            repository.Inserir(usuario);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Usuario = repository.ObterPeloId(id);
            return View();
        }

        public ActionResult Update(int id, string login, string senha, DateTime dataNascimento)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            usuario.Login = login;
            usuario.Senha = senha;
            usuario.DataNascimento = dataNascimento;

            repository.Atualizar(usuario);
            return RedirectToAction("Index");
        }
    }
}