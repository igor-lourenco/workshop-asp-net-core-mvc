using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Models.Enums;

namespace VendasWebMVC.Models {
    public class VendasRecorde {

        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Quantia { get; set; }
        public VendaStatus Status { get; set; }
        public Vendedor Vendedor { get; set; }

        public VendasRecorde() {
        }

        public VendasRecorde(int id, DateTime data, double quantia, VendaStatus status, Vendedor vendedor) {
            Id = id;
            Data = data;
            Quantia = quantia;
            Status = status;
            Vendedor = vendedor;
        }
    }
}
