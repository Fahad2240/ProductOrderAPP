using DotNET.DAL;
using Microsoft.AspNetCore.Mvc;

namespace DotNET.Controllers
{
    public class ProductOrderController : Controller
    {
        private readonly ProductOrderDbContext _context;

        public ProductOrderController(ProductOrderDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var orders=_context.tblOrders.ToList();
            return View(orders);
        }
    }
}
