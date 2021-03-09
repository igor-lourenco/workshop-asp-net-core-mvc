using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;

namespace VendasWebMVC.Servicos {
    public class VendedorServico {

        private readonly VendasWebMVCContext _context;

        public VendedorServico(VendasWebMVCContext context) {
            _context = context;
        }

        public List<Vendedor> buscarTodos() {

            return _context.Vendedor.ToList();
        }

        public void Inserir(Vendedor obj) {

            obj.Departamento = _context.Departamento.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
