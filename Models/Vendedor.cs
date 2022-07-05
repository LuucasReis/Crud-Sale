using System.Collections.Generic;
using System.Linq;

namespace AppVendas.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento  { get; set; }
        public double Salario { get; set; }
        public Department Department { get; set; }
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