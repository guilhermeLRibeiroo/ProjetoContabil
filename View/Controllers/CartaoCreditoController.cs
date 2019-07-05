using Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CartaoCreditoController : Controller
    {
        CartaoCreditoRepository repository;

        public CartaoCreditoController()
        {
            repository = new CartaoCreditoRepository();
        }

        public ActionResult Index()
        {
            ViewBag.CartoesCredito = repository.ObterTodos("");
            return View();
        }

        public ActionResult Cadastrar()
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Clientes = clienteRepository.ObterTodos("");

            return View();
        }

        public ActionResult Store(int idCliente, string numero, DateTime dataVencimento, string cvv)
        {
            CartaoCredito cartao = new CartaoCredito();
            cartao.IdCliente = idCliente;
            cartao.Numero = numero;
            cartao.DataVencimento = dataVencimento;
            cartao.CVV = cvv;

            repository.Inserir(cartao);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Clientes = clienteRepository.ObterTodos("");

            ViewBag.CartaoCredito = repository.ObterPeloId(id);
            return View();
        }

        public ActionResult Update(int id,int idCliente, string numero, DateTime dataVencimento, string cvv)
        {
            CartaoCredito cartao = new CartaoCredito();
            cartao.Id = id;
            cartao.IdCliente = idCliente;
            cartao.Numero = numero;
            cartao.DataVencimento = dataVencimento;
            cartao.CVV = cvv;

            repository.Inserir(cartao);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

    }
}