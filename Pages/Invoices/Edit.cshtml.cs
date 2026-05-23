using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InvoiceDto InvoiceDto { get; set; } = new();

        public int InvoiceId { get; set; }

        public IActionResult OnGet(int id)
        {
            var invoice = _context.Invoices.Find(id);

            if (invoice == null)
            {
                return RedirectToPage("/Invoices/Index");
            }

            InvoiceId = invoice.Id;

            InvoiceDto.Number = invoice.Number;
            InvoiceDto.Status = invoice.Status;
            InvoiceDto.IssueDate = invoice.IssueDate;
            InvoiceDto.DueDate = invoice.DueDate;
            InvoiceDto.Service = invoice.Service;
            InvoiceDto.UnitPrice = invoice.UnitPrice;
            InvoiceDto.Quantity = invoice.Quantity;
            InvoiceDto.ClientName = invoice.ClientName;
            InvoiceDto.Email = invoice.Email;
            InvoiceDto.Phone = invoice.Phone;
            InvoiceDto.Address = invoice.Address;

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var invoice = _context.Invoices.Find(id);

            if (invoice == null)
            {
                return RedirectToPage("/Invoices/Index");
            }

            if (!ModelState.IsValid)
            {
                InvoiceId = id;
                return Page();
            }

            invoice.Number = InvoiceDto.Number;
            invoice.Status = InvoiceDto.Status;
            invoice.IssueDate = InvoiceDto.IssueDate;
            invoice.DueDate = InvoiceDto.DueDate;
            invoice.Service = InvoiceDto.Service;
            invoice.UnitPrice = InvoiceDto.UnitPrice;
            invoice.Quantity = InvoiceDto.Quantity;
            invoice.ClientName = InvoiceDto.ClientName;
            invoice.Email = InvoiceDto.Email;
            invoice.Phone = InvoiceDto.Phone;
            invoice.Address = InvoiceDto.Address;

            _context.SaveChanges();

            return RedirectToPage("/Invoices/Index");
        }
    }
}