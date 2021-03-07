using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Models;
using VendasWebMVC.Models.Enums;

namespace VendasWebMVC.Data {
    public class PovoarServico {

        private VendasWebMVCContext _context;

        public PovoarServico(VendasWebMVCContext context) {
            _context = context;
        }

        public void Povoar() {
            if (_context.Departamento.Any() || _context.Vendedor.Any() || _context.Vendas.Any()) {
                //O banco de dados já foi povoado
                return;
            }

            Departamento d1 = new Departamento(1, "Computadores");
            Departamento d2 = new Departamento(2, "Eletronicos");
            Departamento d3 = new Departamento(3, "Fashion");
            Departamento d4 = new Departamento(4, "Livros");

            Vendedor s1 = new Vendedor(1, "Igor", "igor@gmail.com", new DateTime(1996, 04, 21), 1000.0, d1);
            Vendedor s2 = new Vendedor(2, "Maria Green", "maria@gmail.com", new DateTime(1979, 12, 31), 3500.0, d2);
            Vendedor s3 = new Vendedor(3, "Alex Grey", "alex@gmail.com", new DateTime(1988, 1, 15), 2200.0, d1);
            Vendedor s4 = new Vendedor(4, "Martha Red", "martha@gmail.com", new DateTime(1993, 11, 30), 3000.0, d4);
            Vendedor s5 = new Vendedor(5, "Donald Blue", "donald@gmail.com", new DateTime(2000, 1, 9), 4000.0, d3);
            Vendedor s6 = new Vendedor(6, "Alex Pink", "bob@gmail.com", new DateTime(1997, 3, 4), 3000.0, d2);

            VendasRecorde r1 = new VendasRecorde(1, new DateTime(2018, 09, 25), 11000.0, VendaStatus.Faturado, s1);
            VendasRecorde r2 = new VendasRecorde(2, new DateTime(2018, 09, 4), 7000.0, VendaStatus.Faturado, s5);
            VendasRecorde r3 = new VendasRecorde(3, new DateTime(2018, 09, 13), 4000.0, VendaStatus.Cancelado, s4);
            VendasRecorde r4 = new VendasRecorde(4, new DateTime(2018, 09, 1), 8000.0, VendaStatus.Faturado, s1);
            VendasRecorde r5 = new VendasRecorde(5, new DateTime(2018, 09, 21), 3000.0, VendaStatus.Faturado, s3);
            VendasRecorde r6 = new VendasRecorde(6, new DateTime(2018, 09, 15), 2000.0, VendaStatus.Faturado, s1);
            VendasRecorde r7 = new VendasRecorde(7, new DateTime(2018, 09, 28), 13000.0, VendaStatus.Faturado, s2);
            VendasRecorde r8 = new VendasRecorde(8, new DateTime(2018, 09, 11), 4000.0, VendaStatus.Faturado, s4);
            VendasRecorde r9 = new VendasRecorde(9, new DateTime(2018, 09, 14), 11000.0, VendaStatus.Pendente, s6);
            VendasRecorde r10 = new VendasRecorde(10, new DateTime(2018, 09, 7), 9000.0, VendaStatus.Faturado, s6);
            VendasRecorde r11 = new VendasRecorde(11, new DateTime(2018, 09, 13), 6000.0, VendaStatus.Faturado, s2);
            VendasRecorde r12 = new VendasRecorde(12, new DateTime(2018, 09, 25), 7000.0, VendaStatus.Pendente, s3);
            VendasRecorde r13 = new VendasRecorde(13, new DateTime(2018, 09, 29), 10000.0, VendaStatus.Faturado, s4);
            VendasRecorde r14 = new VendasRecorde(14, new DateTime(2018, 09, 4), 3000.0, VendaStatus.Faturado, s5);
            VendasRecorde r15 = new VendasRecorde(15, new DateTime(2018, 09, 12), 4000.0, VendaStatus.Faturado, s1);
            VendasRecorde r16 = new VendasRecorde(16, new DateTime(2018, 10, 5), 2000.0, VendaStatus.Faturado, s4);
            VendasRecorde r17 = new VendasRecorde(17, new DateTime(2018, 10, 1), 12000.0, VendaStatus.Faturado, s1);
            VendasRecorde r18 = new VendasRecorde(18, new DateTime(2018, 10, 24), 6000.0, VendaStatus.Faturado, s3);
            VendasRecorde r19 = new VendasRecorde(19, new DateTime(2018, 10, 22), 8000.0, VendaStatus.Faturado, s5);
            VendasRecorde r20 = new VendasRecorde(20, new DateTime(2018, 10, 15), 8000.0, VendaStatus.Faturado, s6);
            VendasRecorde r21 = new VendasRecorde(21, new DateTime(2018, 10, 17), 9000.0, VendaStatus.Faturado, s2);
            VendasRecorde r22 = new VendasRecorde(22, new DateTime(2018, 10, 24), 4000.0, VendaStatus.Faturado, s4);
            VendasRecorde r23 = new VendasRecorde(23, new DateTime(2018, 10, 19), 11000.0, VendaStatus.Cancelado, s2);
            VendasRecorde r24 = new VendasRecorde(24, new DateTime(2018, 10, 12), 8000.0, VendaStatus.Faturado, s5);
            VendasRecorde r25 = new VendasRecorde(25, new DateTime(2018, 10, 31), 7000.0, VendaStatus.Faturado, s3);
            VendasRecorde r26 = new VendasRecorde(26, new DateTime(2018, 10, 6), 5000.0, VendaStatus.Faturado, s4);
            VendasRecorde r27 = new VendasRecorde(27, new DateTime(2018, 10, 13), 9000.0, VendaStatus.Pendente, s1);
            VendasRecorde r28 = new VendasRecorde(28, new DateTime(2018, 10, 7), 4000.0, VendaStatus.Faturado, s3);
            VendasRecorde r29 = new VendasRecorde(29, new DateTime(2018, 10, 23), 12000.0, VendaStatus.Faturado, s5);
            VendasRecorde r30 = new VendasRecorde(30, new DateTime(2018, 10, 12), 5000.0, VendaStatus.Faturado, s2);

            _context.Departamento.AddRange(d1, d2, d3, d4);

            _context.Vendedor.AddRange(s1, s2, s3, s4, s5, s6);

            _context.Vendas.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10,
                r11, r12, r13, r14, r15, r16, r17, r18, r19, r20,
                r21, r22, r23, r24, r25, r26, r27, r28, r29, r30);

            _context.SaveChanges();
        }
    }
}
