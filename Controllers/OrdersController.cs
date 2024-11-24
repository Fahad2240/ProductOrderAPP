using System.Drawing.Printing;
using System.Linq.Expressions;
using DotNET.DAL;
using DotNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNET.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ProductOrderDbContext _context;

        public OrdersController(ProductOrderDbContext context)
        {
            _context = context;
        }
        //public IActionResult OrderDetails(int id)
        //{
        //    var order = _context.tblOrders
        //        .Include(o => o.TblProducts) // Include related product details
        //        .FirstOrDefault(o => o.intOrderId == id);

        //    if (order == null)
        //        return NotFound("Order not found.");

        //    // Pass the order details to the view
        //    return View(order);
        //}


        // GET: Renders the CreateOrder form
        [HttpGet]
        public IActionResult CreateOrder()
        {
            // Passing product data for selection
            ViewBag.Products = _context.tblProducts.ToList();
            return View();
        }

        // POST: Handles form submission
        [HttpPost]
        public IActionResult CreateOrder(int productId, string customerName, decimal quantity)
        {
            var product = _context.tblProducts.FirstOrDefault(p => p.IntProductId == productId);
            if (product == null)
                return BadRequest("Product not found.");

            if (product.numStock < quantity)
                return BadRequest("Insufficient stock.");

            // Deduct stock and create the order
            product.numStock -= quantity;
            var newOrder = new TblOrders
            {
                IntProductId = productId,
                strCustomerName = customerName,
                numQuantity = quantity,
                dtOrderDate = DateTime.Now
            };
             var maxOrderId = _context.tblOrders.Any()
                ? _context.tblOrders.Max(o => o.intOrderId)
                : 0;
            _context.Database.ExecuteSqlInterpolated($"DBCC CHECKIDENT ('tblOrders', RESEED, {maxOrderId})");
            _context.tblOrders.Add(newOrder);
            _context.SaveChanges();


            return RedirectToAction("Orders","Home");
        }

        [HttpGet]
        public IActionResult UpdateOrderQuantity()
        {
            // Fetch all orders to populate the dropdown
            ViewBag.Orders = _context.tblOrders.Include(o => o.TblProducts).ToList();


            //var order = _context.tblOrders.FirstOrDefault();
            //if (order != null)
            //{
            //    ViewBag.CurrentQuantity = order.numQuantity;
            //    ViewBag.CurrentOrderId = order.intOrderId;  // To select the current order in the dropdown
            //}

            return View();
        }




        // API 02: Update an order's quantity
        [HttpPost]
        public IActionResult UpdateOrderQuantity(int orderId, string quantityChange)
        {
            var order = _context.tblOrders.FirstOrDefault(o => o.intOrderId == orderId);
            if (order == null)
                return NotFound("Order not found.");

            var product = _context.tblProducts.FirstOrDefault(p => p.IntProductId == order.IntProductId);
            if (product == null)
                return NotFound("Product not found.");

            decimal newQuantity=0;
            decimal change = 0;
            change = int.Parse(quantityChange);
            newQuantity = order.numQuantity + change;

            // Calculate the new quantity based on the change
            // Ensure the new quantity is valid
            if (newQuantity < 0)
            {
                return BadRequest("Quantity cannot be less than zero.");
            }

            // Ensure there’s enough stock if decreasing the quantity
            if (quantityChange[0] == '-' && product.numStock + order.numQuantity < newQuantity)
            {
                return BadRequest("Insufficient stock.");
            }

            // Update the order quantity
            order.numQuantity = newQuantity;

            // Update the product stock based on the quantity change
            product.numStock += change;

            // Save the changes
            _context.SaveChanges();

            // Redirect to the orders list or another relevant page
            return RedirectToAction("Orders","Home");
        }

        [HttpGet]
        public IActionResult DeleteOrder()
        {
            ViewBag.Orders = _context.tblOrders.Include(o => o.TblProducts).ToList();
            return View();
        }


        // API 03: Delete an order
        [HttpPost]
        public IActionResult DeleteOrder(int orderId)
        {

            var orderToDelete = _context.tblOrders.FirstOrDefault(o => o.intOrderId == orderId);
            if (orderToDelete == null)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }

            // Step 2: Adjust stock if necessary
            var product = _context.tblProducts.FirstOrDefault(p => p.IntProductId == orderToDelete.IntProductId);
            if (product != null)
            {
                product.numStock += orderToDelete.numQuantity;
            }

            // Step 3: Remove the order
            _context.tblOrders.Remove(orderToDelete);
            _context.SaveChanges();
           
            //int ExistingOrder = _context.tblOrders.Count();
            //var maxOrderId = ExistingOrder;

            //_context.Database.ExecuteSqlInterpolated($"DBCC CHECKIDENT ('tblOrders', RESEED, {maxOrderId})");
            // Step 4: Fetch all orders with IDs greater than the deleted one
            var ordersToUpdate = _context.tblOrders
                .Where(o => o.intOrderId > orderId)
                .OrderBy(o => o.intOrderId)
                .ToList();
            if (ordersToUpdate.Count()!=0)
            {
                int setId = orderId - 1;
                _context.Database.ExecuteSqlInterpolated($"DBCC CHECKIDENT ('tblOrders', RESEED, {setId})");
            }
            else
            {
                int existing = _context.tblOrders.Count();
                _context.Database.ExecuteSqlInterpolated($"DBCC CHECKIDENT ('tblOrders', RESEED, {existing})");
            }
            // Step 5: Recreate affected orders with updated IDs
            foreach (var order in ordersToUpdate)
            {
                // Detach the current order to allow re-creation
                //_context.Entry(order).State = EntityState.Detached;
                //int intOrderId = order.intOrderId - 1; // Decrement the ID
                //int ExistingOrder = _context.tblOrders.Count();
                //var maxOrderId = ExistingOrder;
                //Console.WriteLine(ExistingOrder);
              
                int IntProductId = order.IntProductId;
                string strCustomerName = order.strCustomerName;
                decimal numQuantity = order.numQuantity;
                DateTime dtOrderDate = order.dtOrderDate;
                // Create a new order with decremented ID
                var updatedOrder = new TblOrders
                {
                   IntProductId=IntProductId,
                   strCustomerName=strCustomerName,
                   numQuantity=numQuantity,
                   dtOrderDate=dtOrderDate
                };

                // Add the new order and remove the old one
                _context.tblOrders.Remove(order);
                _context.tblOrders.Add(updatedOrder);
                
            }

            _context.SaveChanges();

            // Step 6: Reseed the identity column
          
            return RedirectToAction("Orders", "Home");
        }

       

        // API 04: Get all orders with product details
        public IActionResult GetAllOrders()
        {
            var orders = _context.tblOrders
                          .Include(o => o.TblProducts)
                          .Select(o => new
                          {
                              o.intOrderId,
                              o.TblProducts.strProductName,
                              o.TblProducts.numUnitPrice,
                              o.strCustomerName,
                              o.numQuantity,
                              o.dtOrderDate
                          }).ToList();

            return View(orders); // Ensure you have a view to display this data.
        }

        // API 05: Get product order summary
        public IActionResult GetProductSummary()
        {
            var summary = _context.tblProducts
                .Select(p => new
                {
                    p.strProductName,
                    TotalQuantityOrdered = p.TblOrders.Sum(o => o.numQuantity),
                    TotalRevenue = p.TblOrders.Sum(o => o.numQuantity * p.numUnitPrice)
                }).ToList();

            return View(summary); // Ensure you have a view to display this summary.
        }


        // API 06: Retrieve all products with stock quantity below a specified threshold
        [HttpGet]
        public IActionResult GetLowStockProducts(int threshold = 100)
        {
            var lowStockProducts = _context.tblProducts
                .Where(p => p.numStock < threshold)
                .Select(p => new
                {
                    p.strProductName,
                    p.numUnitPrice,
                    p.numStock
                })
                .ToList();

            if (!lowStockProducts.Any())
            {
                return NotFound("No products found below the specified stock threshold.");
            }

            return View(lowStockProducts); // Use a view to display these products
        }


        // API 07: Get the top 3 customers by total quantity ordered
        //[HttpGet]
        //public IActionResult GetTopCustomers()
        //{


        //    return View(topCustomers); // Use a view to display top customers
        //}


        //// API 08: Find products that have not been ordered at all
        //[HttpGet]
        //public IActionResult GetUnorderedProducts()
        //{
        //    var unorderedProducts = _context.tblProducts
        //        .Where(p => !_context.tblOrders.Any(o => o.IntProductId == p.IntProductId))
        //        .Select(p => new
        //        {
        //            p.strProductName,
        //            p.numUnitPrice,
        //            p.numStock
        //        })
        //        .ToList();

        //    if (!unorderedProducts.Any())
        //    {
        //        return NotFound("All products have been ordered at least once.");
        //    }

        //    return View(unorderedProducts); // Use a view to display these products
        //}



        [HttpGet]
        public IActionResult GetOrderCount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GenerateOrderForm(int orderCount)
        {
            // Pass the order count to the next view
            ViewBag.OrderCount = orderCount;
            ViewBag.Products = _context.tblProducts.ToList(); // Fetch products for dropdowns
            return View("CreateBulkOrders");
        }

        // API 09: Implement a transactional operation for bulk order creation
        public IActionResult CreateBulkOrders()
        {
            ViewBag.Products = _context.tblOrders.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateBulkOrders(List<TblOrders> orders)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var order in orders)
                {
                    var product = _context.tblProducts.FirstOrDefault(p => p.IntProductId == order.IntProductId);
                    if (product == null)
                    {
                        string mid = (order.IntProductId).ToString();
                        ViewBag.ErrorMessage = "Product with ID "+mid+"not found";
                        return View();
                    }

                    if (product.numStock < order.numQuantity)
                    {
                        string mid = (order.IntProductId).ToString();
                        ViewBag.ErrorMessage = "Insufficient stock for product ID"+mid+"not found";
                        return View();
                    }

                    // Deduct stock and add the order
                    product.numStock -= order.numQuantity;
                    order.dtOrderDate = DateTime.Now;

                    _context.tblOrders.Add(order);
                }

                _context.SaveChanges();
                transaction.Commit();

                return RedirectToAction("Orders","Home");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.Products = _context.tblProducts.ToList();
                return View();
            }
        }



    }
}
