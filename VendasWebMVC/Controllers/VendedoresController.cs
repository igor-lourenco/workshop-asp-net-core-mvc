﻿using System;
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

            //Testa se o usuario preencheu os campos corretamente
            if (!ModelState.IsValid) {
                var departamentos = _departamentoServico.buscarTodos();
                var viewModel = new VendedorViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }
                

            _vendedorServico.Inserir(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deletar(int? id) {
            if (id == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não foi fornecido" });
            
            var obj = _vendedorServico.BuscarPorId(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não existe" });
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
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não foi fornecido" });

            var obj = _vendedorServico.BuscarPorId(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não existe" });
            return View(obj);
        }

        public IActionResult Editar(int? id) {
            if (id == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não foi fornecido" });
            var obj = _vendedorServico.BuscarPorId(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Erro), new { mensagem = "ID não existe" });
            List<Departamento> departamentos = _departamentoServico.buscarTodos();
            VendedorViewModel viewModel = new VendedorViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, Vendedor vendedor) {

            //Testa se o usuario preencheu os campos corretamente
            if (!ModelState.IsValid) {
                var departamentos = _departamentoServico.buscarTodos();
                var viewModel = new VendedorViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }

            if (id != vendedor.Id)
                return RedirectToAction(nameof(Erro), new { mensagem = "IDs não correspondem" });

            try {
                _vendedorServico.Atualizar(vendedor);
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