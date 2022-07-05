using AppVendas.Models.Enums;
namespace AppVendas.Models
{
    public class RegistroVendas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Quantidade { get; set; }
        public StatusVenda StatusVenda { get; set; }
        public Vendedor Vendedor { get; set; }

        public RegistroVendas()
        {
        }

        public RegistroVendas(int id, DateTime data, double quantidade, StatusVenda status, Vendedor vendedor)
        {
            Id= id;
            Data= data;
            Quantidade = quantidade;
            StatusVenda= status;
            Vendedor = vendedor;
        }
    }
}