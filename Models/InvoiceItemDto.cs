using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Models
{
    public class InvoiceItemDto
    {
        public string? Service { get; set; } = "";

        [Precision(16, 2)]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}