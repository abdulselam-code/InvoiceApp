using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public string? Number { get; set; } = "";
        public string? Status { get; set; } = "";

        public DateOnly? IssueDate { get; set; }
        public DateOnly? DueDate { get; set; }

        // Servis ayrıntıları
        public string? Service { get; set; } = "";

        [Precision(16, 2)]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        // İstemci / müşteri ayrıntıları
        public string? ClientName { get; set; } = "";
        public string? Email { get; set; } = "";
        public string? Phone { get; set; } = "";
        public string? Address { get; set; } = "";

        public List<InvoiceItem> InvoiceItems { get; set; } = new();
    }
}