using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VendasWebMVC.Models {
    public class Vendedor {

        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ser entre {2} e {1}")]
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} obrigatorio")]
        public string Email { get; set; }

        [Display(Name = "Data Aniversario")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} obrigatorio")]
        [EmailAddress(ErrorMessage = "Digite um email valido")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Salario Base")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Required(ErrorMessage = "{0} obrigatorio")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        public double SalarioBase { get; set; }

        public Departamento Departamento { get; set; }

        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }

        public ICollection<VendasRecorde> Vendas { get; set; } = new List<VendasRecorde>();

        public Vendedor() {
        }

        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento) {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void AdicionarVendas(VendasRecorde vr) {
            Vendas.Add(vr);
        }
        
        public void RemoverVendas(VendasRecorde vr) {
            Vendas.Remove(vr);
        }

        public double TotalVendas(DateTime inicio, DateTime final) {
            return Vendas.Where(vr => vr.Data >= inicio && vr.Data <= final).Sum(sr => sr.Quantia);
        }
    }
}
