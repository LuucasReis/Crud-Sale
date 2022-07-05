namespace AppVendas.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Vendedor> Vendedores {get; set;} = new List<Vendedor>();

        public Department()
        {
        }

        public Department(string nome, int id)
        {
            Nome= nome;
            Id = id;
        }

        public void AddVendedor(Vendedor vendedor)
        {
            Vendedores.Add(vendedor);
        }

        public void RemoveVendedor(Vendedor vendedor)
        {
            Vendedores.Remove(vendedor);
        }

        public double TotalVendaDepartamento(DateTime inicio, DateTime final)
        {
            return Vendedores.Sum(x=> x.VendaTotal(inicio, final));
        }
    }
}
    