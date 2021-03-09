using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;

namespace VendasWebMVC.Servicos {
    public class DepartamentoServico {

        private readonly VendasWebMVCContext _context;

        public DepartamentoServico(VendasWebMVCContext context) {
            _context = context;
        }

        public List<Departamento> buscarTodos() {

            return _context.Departamento.OrderBy(x => x.Nome).ToList();
        }
    }
}
