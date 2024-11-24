using System.Diagnostics;
using DotNET.DAL;
using DotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductOrderDbContext _context;
        public HomeController(ILogger<HomeController> logger, ProductOrderDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Query to calculate the summary for each product
            var productSummary = _context.tblProducts.Select(product => new
            {
                ProductName = product.strProductName,
                Orders = _context.tblOrders
                    .Where(order => order.IntProductId == product.IntProductId)
                    .ToList(), // Fetch matching orders for this product
                product.numUnitPrice // Keep the unit price for client-side calculation
            }).AsEnumerable().Select(productWithOrders => new
            {
                ProductName = productWithOrders.ProductName,
                TotalQuantityOrdered = productWithOrders.Orders.Sum(o => o.numQuantity),
                TotalRevenue = productWithOrders.Orders.Sum(o => o.numQuantity * productWithOrders.numUnitPrice)
            }).ToList();

            // Pass the summary to the view
            return View(productSummary);
            //return View();
        }

        public IActionResult Products()
        {
            var products = _context.tblProducts.ToList();
            var orders=_context.tblOrders.ToList();
            var unorderedProducts = (from product in _context.tblProducts
                                     join order in _context.tblOrders
                                     on product.IntProductId equals order.IntProductId into productOrders
                                     from order in productOrders.DefaultIfEmpty()
                                     where order == null
                                     select product).ToList();

            ViewBag.Products = products;
            ViewBag.Unordered = unorderedProducts;
            return View();
        }
        public IActionResult Orders()
        {
            var orders = _context.tblOrders.ToList();
            //if (deleted.HasValue)
            //{
            //    int start = 1;
            //    foreach (var order in orders)
            //    {   
            //        if(order.intOrderId!=start)
            //            order.intOrderId = start;
            //        start++;
            //    }
            //    _context.SaveChanges();
            //}
            var topCustomers = _context.tblOrders
               .GroupBy(o => o.strCustomerName)
               .Select(group => new
               {
                   CustomerName = group.Key,
                   TotalQuantityOrdered = group.Sum(o => o.numQuantity)
               })
               .OrderByDescending(c => c.TotalQuantityOrdered)
               .Take(3)
               .ToList();

            if (!topCustomers.Any())
            {
                return NotFound("No orders found.");
            }
            ViewBag.Orders = orders;
            ViewBag.TopCustomers = topCustomers;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
