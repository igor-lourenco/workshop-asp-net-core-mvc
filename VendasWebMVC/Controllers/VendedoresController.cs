using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models;
using VendasWebMVC.Models.ViewModels;
using VendasWebMVC.Servicos;

namespace VendasWebMVC.Controllers {
    public class VendedoresController : Controller {

        //Declaracao de injecao de dependência da classe VendedorServico
        private readonly VendedorServico _vendedorServico;
        private readonly DepartamentoServico _departamentoServico;

        public VendedoresController(VendedorServico vendedorServico, DepartamentoServico departamentoServico) {
            _vendedorServico = vendedorServico;
            _departamentoServico = departamentoServico;
        }

        //Chama o controlador
        public IActionResult Index() {
            //Controlador acessa o model e pega os dados
            var lista = _vendedorServico.buscarTodos();
            //Controlador envia os dados para a view
            return View(lista);
        }

        public IActionResult Criar() {
            //Carrega os departamentos
            var departamentos = _departamentoServico.buscarTodos();
            //Instancia a variavel departamentos na classe VendedorViewModel
            var viewModel = new VendedorViewModel { Departamentos = departamentos };
            //Passa o objeto viewModel para a View
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Vendedor vendedor) {
            _vendedorServico.Inserir(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deletar(int? id) {
            if(id == null) 
                return NotFound();
            
            var obj = _vendedorServico.BuscarPorId(id.Value);
            if (obj == null)
                return NotFound();
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletar(int id) {
            _vendedorServico.Remover(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int? id) {
            if (id == null)
                return NotFound();

            var obj = _vendedorServico.BuscarPorId(id.Value);
            if (obj == null)
                return NotFound();
            return View(obj);
        }
    }
}