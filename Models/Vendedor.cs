using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AppVendas.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Coloque um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage ="O {0} deve ter no mínimo {2} Caracteres e no Max {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [Display(Name ="Data Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento  { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} precisa ser no mínimo {1} e no máximo {2}")]
        [Display(Name ="Salario Base")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Salario { get; set; }

        [Display(Name ="Departamento")]
        public Department Department { get; set; }

        [Display(Name ="Departamento")]
        public int DepartmentId { get; set; }
        public ICollection<RegistroVendas> Vendas { get; set; } = new List<RegistroVendas>();

        public Vendedor()
        {
        }

        public Vendedor(int id, string email, string nome, DateTime date, double salario, Department departamento)
        {
            Id= id;
            Email= email;
            Nome= nome;
            DataNascimento = date;
            Salario= salario;
            Department= departamento;
        }

        public void AddVenda(RegistroVendas venda)
        {
            Vendas.Add(venda);
        }

        public void RemoveVenda(RegistroVendas venda)
        {
            Vendas.Remove(venda);
        }

        public double VendaTotal(DateTime inicio, DateTime final)
        {
            return Vendas.Where(x => x.Data >= inicio &&  x.Data < final).Select(x=> x.Quantidade).Sum();
        }
    }
}