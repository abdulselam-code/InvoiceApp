using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
            var invoice = _context.Invoices
                .Include(i => i.InvoiceItems)
                .FirstOrDefault(i => i.Id == id);

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

            InvoiceDto.ClientName = invoice.ClientName;
            InvoiceDto.Email = invoice.Email;
            InvoiceDto.Phone = invoice.Phone;
            InvoiceDto.Address = invoice.Address;

            InvoiceDto.Items = invoice.InvoiceItems
                .OrderBy(i => i.Id)
                .Select(item => new InvoiceItemDto
                {
                    Service = item.Service,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                })
                .ToList();

            if (!InvoiceDto.Items.Any())
            {
                InvoiceDto.Items.Add(new InvoiceItemDto
                {
                    Service = invoice.Service,
                    UnitPrice = invoice.UnitPrice,
                    Quantity = invoice.Quantity
                });
            }

            return Page();
        }

        public IActionResult OnPost(int id, string mode)
        {
            var invoice = _context.Invoices
                .Include(i => i.InvoiceItems)
                .FirstOrDefault(i => i.Id == id);

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

            InvoiceDto.Items = InvoiceDto.Items
                .Where(i => !string.IsNullOrWhiteSpace(i.Service) && i.Quantity > 0)
                .ToList();

            if (!InvoiceDto.Items.Any())
            {
                ModelState.AddModelError("", "At least one invoice item is required.");
                Mode = mode;
                InvoiceId = id;
                InvoiceDto.Items.Add(new InvoiceItemDto { Quantity = 1 });
                return Page();
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

            invoice.ClientName = InvoiceDto.ClientName;
            invoice.Email = InvoiceDto.Email;
            invoice.Phone = InvoiceDto.Phone;
            invoice.Address = InvoiceDto.Address;

            decimal totalAmount = InvoiceDto.Items.Sum(i => i.UnitPrice * i.Quantity);
            var firstItem = InvoiceDto.Items.First();

            invoice.Service = InvoiceDto.Items.Count == 1 ? firstItem.Service : "Multiple Services";
            invoice.UnitPrice = totalAmount;
            invoice.Quantity = 1;

            _context.InvoiceItems.RemoveRange(invoice.InvoiceItems);

            invoice.InvoiceItems = InvoiceDto.Items.Select(item => new InvoiceItem
            {
                InvoiceId = invoice.Id,
                Service = item.Service,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            }).ToList();

            _context.SaveChanges();

            return RedirectToPage("/Invoices/Index");
        }
    }
}