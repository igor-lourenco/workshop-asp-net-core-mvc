using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;

namespace VendasWebMVC.Servicos {
    public class VendasRecordeServico {

        private readonly VendasWebMVCContext _context;

        public VendasRecordeServico(VendasWebMVCContext context) {
            _context = context;
        }

        public async Task<List<VendasRecorde>> BuscarPorDataAsync(DateTime? DataMin, DateTime? DataMax) {

            var resultado = from obj in _context.Vendas select obj;
            if (DataMin.HasValue) {
                resultado = resultado.Where(x => x.Data >= DataMin.Value);
            }
            if (DataMax.HasValue) {
                resultado = resultado.Where(x => x.Data <= DataMax.Value);
            }
            return await resultado.Include(x => x.Vendedor).Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data).ToListAsync();
        }
    }
}
