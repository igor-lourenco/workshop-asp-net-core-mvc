using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendasWebMVC.Models.Enums {
    public enum VendaStatus : int {

        Pendente = 0,
        Faturado = 1,
        Cancelado = 2
    }
}
