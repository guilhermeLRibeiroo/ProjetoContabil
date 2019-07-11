using Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CompraController : Controller
    {

        // GET: Compra

        CompraRepository repository;

        public CompraController()
        {
            repository = new CompraRepository();
        }

        public ActionResult Index()
        {
            ViewBag.Compras = repository.ObterTodos("");
            return View();
        }
        public ActionResult Cadastrar()
        {
            CartaoCreditoRepository cartaoCreditoRepository = new CartaoCreditoRepository();
            ViewBag.Cartoes = cartaoCreditoRepository.ObterTodos("");
            return View();
        }

        public ActionResult Store(int idCartaoCredito, string valor, string datacompra)
        {
            repository.Inserir(new Compra()
            {
                IdCartaoCredito = idCartaoCredito,
                Valor = Convert.ToDecimal(valor.ToString().Replace(".", ",")),
                DataCompra = Convert.ToDateTime(datacompra)
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
            CartaoCreditoRepository cartaoCreditoRepository  = new CartaoCreditoRepository();
            ViewBag.Cartoes = cartaoCreditoRepository.ObterTodos("");

            ViewBag.Compra = repository.ObterPeloId(id);
            
            return View();
        }

        public ActionResult Update(int id, int idCartaoCredito, string valor, string datacompra)
        {
            Compra contaReceber = new Compra()
            {
                Id = id,
                IdCartaoCredito = idCartaoCredito,
                Valor = Convert.ToDecimal(valor.ToString()),
                DataCompra = Convert.ToDateTime(datacompra)
            };

            repository.Atualizar(contaReceber);
            return RedirectToAction("Index");
        }
    }
}