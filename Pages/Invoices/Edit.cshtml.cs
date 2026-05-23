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

        public string Mode { get; set; } = "edit";

        public bool IsDetailsMode => Mode == "details";
        public bool IsEditMode => Mode == "edit";
        public bool IsDeleteMode => Mode == "delete";

        public IActionResult OnGet(int id, string mode = "edit")
        {
            var invoice = _context.Invoices.Find(id);

            if (invoice == null)
            {
                return RedirectToPage("/Invoices/Index");
            }

            if (mode != "edit" && mode != "details" && mode != "delete")
            {
                mode = "edit";
            }

            Mode = mode;
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

        public IActionResult OnPost(int id, string mode)
        {
            var invoice = _context.Invoices.Find(id);

            if (invoice == null)
            {
                return RedirectToPage("/Invoices/Index");
            }

            if (mode == "delete")
            {
                _context.Invoices.Remove(invoice);
                _context.SaveChanges();

                return RedirectToPage("/Invoices/Index");
            }

            if (!ModelState.IsValid)
            {
                Mode = mode;
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