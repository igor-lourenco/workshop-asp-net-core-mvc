using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using VendasWebMVC.Servicos.Exceptions;

namespace VendasWebMVC.Servicos {
    public class VendedorServico {

        private readonly VendasWebMVCContext _context;

        public VendedorServico(VendasWebMVCContext context) {
            _context = context;
        }

        public async Task<List<Vendedor>> buscarTodosAsync() {

            return await _context.Vendedor.ToListAsync();
        }

        public async Task InserirAsync(Vendedor obj) {

            //Adiciona no banco de dados o objeto vendedor chegado como parâmetro
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Vendedor> BuscarPorIdAsync(int id) {

            return await _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoverAsync(int id) {

            //Pega o objeto passando o id
            var obj = await _context.Vendedor.FindAsync(id);
            //Remove no DBSet
            _context.Vendedor.Remove(obj);
            //Framework deleta no banco de dados
            await _context.SaveChangesAsync();

        }

        public async Task AtualizarAsync(Vendedor obj) {

            bool hasAny = await _context.Vendedor.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
                throw new NotFoundException("Vendedor não existe");

            try {
                _context.Update(obj);
               await  _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
