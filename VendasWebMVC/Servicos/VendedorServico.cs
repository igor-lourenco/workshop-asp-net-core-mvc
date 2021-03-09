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

            //Adiciona no banco de dados o objeto vendedor chegado como parâmetro
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Vendedor BuscarPorId(int id) {

            return _context.Vendedor.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remover(int id) {

            //Pega o objeto passando o id
            var obj = _context.Vendedor.Find(id);
            //Remove no DBSet
            _context.Vendedor.Remove(obj);
            //Framework deleta no banco de dados
            _context.SaveChanges();

        }
    }
}
