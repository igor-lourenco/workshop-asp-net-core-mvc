using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using VendasWebMVC.Models;
using VendasWebMVC.Models.ViewModels;
using VendasWebMVC.Servicos;
using VendasWebMVC.Servicos.Exceptions;

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
        public async Task<IActionResult> Index() {
            //Controlador acessa o model e pega os dados
            var lista = await _vendedorServico.buscarTodosAsync();
            //Controlador envia os dados para a view
            return View(lista);
        }

        public async Task<IActionResult> Criar() {
            //Carrega os departamentos
            var departamentos = await _departamentoServico.buscarTodosAsync();
            //Instancia a variavel departamentos na classe VendedorViewModel
            var viewModel = new VendedorViewModel { Departamentos = departamentos };
            //Passa o objeto viewModel para a View
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Vendedor vendedor) {

            //Testa se o usuario preencheu os campos corretamente
            if (!ModelState.IsValid) {
                var departamentos = await _departamentoServico.buscarTodosAsync();
                var viewModel = new VendedorViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }
                

            await _vendedorServico.InserirAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Deletar(int? id) {
            if (id == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não foi fornecido" });
            
            var obj =await  _vendedorServico.BuscarPorIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não existe" });
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id) {

            try {
                await _vendedorServico.RemoverAsync(id);
                return RedirectToAction(nameof(Index));
            } catch (IntegrityException e) {
                return RedirectToAction(nameof(Erro), new { mensagem = e.Message });
            }
        }

        public async Task<IActionResult> Detalhes(int? id) {
            if (id == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não foi fornecido" });

            var obj = await _vendedorServico.BuscarPorIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não existe" });
            return View(obj);
        }

        public async Task<IActionResult> Editar(int? id) {
            if (id == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não foi fornecido" });
            var obj = await _vendedorServico.BuscarPorIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não existe" });
            List<Departamento> departamentos = await _departamentoServico.buscarTodosAsync();
            VendedorViewModel viewModel = new VendedorViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Vendedor vendedor) {

            //Testa se o usuario preencheu os campos corretamente
            if (!ModelState.IsValid) {
                var departamentos = await _departamentoServico.buscarTodosAsync();
                var viewModel = new VendedorViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }

            if (id != vendedor.Id)
                return RedirectToAction(nameof(Erro), new { mensagem = "IDs não correspondem" });

            try {
                await _vendedorServico.AtualizarAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e) {
                return RedirectToAction(nameof(Erro), new { mensagem = e.Message });

            }
        }

        public IActionResult Erro(string mensagem) {
            var viewModel = new ErrorViewModel {
                Mensagem = mensagem,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}