using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models;
using VendasWebMVC.Servicos;

namespace VendasWebMVC.Controllers {
    public class VendedoresController : Controller {

        //Declaracao de injecao de dependência da classe VendedorServico
        private readonly VendedorServico _vendedorServico;

        public VendedoresController(VendedorServico vendedorServico) {
            _vendedorServico = vendedorServico;
        }

        //Chama o controlador
        public IActionResult Index() {
            //Controlador acessa o model e pega os dados
            var lista = _vendedorServico.buscarTodos();
            //Controlador envia os dados para a view
            return View(lista);
        }

        public IActionResult Criar() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Vendedor vendedor) {
            _vendedorServico.Inserir(vendedor);
            return RedirectToAction(nameof(Index));
        }
    }
}