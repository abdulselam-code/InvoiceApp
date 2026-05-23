using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public List<Invoice> invoiceList { get; set; } = new();

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public int PaidCount { get; set; }
        public int PendingCount { get; set; }
        public decimal TotalRevenue { get; set; }

        public string? SearchText { get; set; }
        public string? StatusFilter { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(int currentPage = 1, int pageSize = 10, string? searchText = null, string? statusFilter = null)
        {
            if (currentPage < 1)
            {
                currentPage = 1;
            }

            if (pageSize != 10 && pageSize != 25 && pageSize != 50)
            {
                pageSize = 10;
            }

            CurrentPage = currentPage;
            PageSize = pageSize;
            SearchText = searchText;
            StatusFilter = statusFilter;

            PaidCount = context.Invoices.Count(i => i.Status == "Paid");
            PendingCount = context.Invoices.Count(i => i.Status == "Pending");
            TotalRevenue = context.Invoices.Sum(i => i.UnitPrice * i.Quantity);

            var query = context.Invoices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(i =>
                    (i.Number != null && i.Number.Contains(searchText)) ||
                    (i.ClientName != null && i.ClientName.Contains(searchText)) ||
                    (i.Email != null && i.Email.Contains(searchText)) ||
                    (i.Service != null && i.Service.Contains(searchText))
                );
            }

            if (!string.IsNullOrWhiteSpace(statusFilter) && statusFilter != "All")
            {
                query = query.Where(i => i.Status == statusFilter);
            }

            TotalItems = query.Count();
            TotalPages = (int)Math.Ceiling(TotalItems / (double)PageSize);

            invoiceList = query
                .OrderByDescending(i => i.IssueDate)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}