using AppVendas.Models.Enums;
using System.ComponentModel.DataAnnotations;
namespace AppVendas.Models
{
    public class RegistroVendas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        [Display(Name ="Valor")]
        public double Quantidade { get; set; }

        [Display(Name ="Status Venda")]
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