using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InvoiceDto InvoiceDto { get; set; } = new();

        public void OnGet()
        {
            InvoiceDto.Items.Add(new InvoiceItemDto
            {
                Service = "",
                UnitPrice = 0,
                Quantity = 1
            });
        }

        public IActionResult OnPost()
        {
            InvoiceDto.Items = InvoiceDto.Items
                .Where(i => !string.IsNullOrWhiteSpace(i.Service) && i.Quantity > 0)
                .ToList();

            if (!InvoiceDto.Items.Any())
            {
                ModelState.AddModelError("", "At least one invoice item is required.");
                InvoiceDto.Items.Add(new InvoiceItemDto { Quantity = 1 });
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            decimal totalAmount = InvoiceDto.Items.Sum(i => i.UnitPrice * i.Quantity);
            var firstItem = InvoiceDto.Items.First();

            Invoice invoice = new()
            {
                Number = InvoiceDto.Number,
                Status = InvoiceDto.Status,
                IssueDate = InvoiceDto.IssueDate,
                DueDate = InvoiceDto.DueDate,

                ClientName = InvoiceDto.ClientName,
                Email = InvoiceDto.Email,
                Phone = InvoiceDto.Phone,
                Address = InvoiceDto.Address,

                // Eski tek satırlı alanlarla uyumluluk için
                Service = InvoiceDto.Items.Count == 1 ? firstItem.Service : "Multiple Services",
                UnitPrice = totalAmount,
                Quantity = 1,

                InvoiceItems = InvoiceDto.Items.Select(item => new InvoiceItem
                {
                    Service = item.Service,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                }).ToList()
            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return RedirectToPage("/Invoices/Index");
        }
    }
}