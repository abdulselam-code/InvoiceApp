using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Invoice? Invoice { get; set; }

        public IActionResult OnGet(int id)
        {
            Invoice = _context.Invoices.Find(id);

            if (Invoice == null)
            {
                return RedirectToPage("/Invoices/Index");
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var invoice = _context.Invoices.Find(id);

            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                _context.SaveChanges();
            }

            return RedirectToPage("/Invoices/Index");
        }
    }
}