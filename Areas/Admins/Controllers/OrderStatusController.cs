using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Web.Data;
using OnlineRestaurant.Web.Models;

namespace OnlineRestaurant.Web.Areas.Admins.Controllers
{
    [Area("Admins")]
    [Authorize(Roles = "AppAdmin")]
    public class OrderStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admins/OrderStatus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderStatus.Include(o => o.Order);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admins/OrderStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatus
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderStatusId == id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // GET: Admins/OrderStatus/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "CustomerEmail");
            return View();
        }

        // POST: Admins/OrderStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderStatusId,FoodOrderId,Status,OrderId")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                // Check for Duplicates
                bool isFound = _context.OrderStatus.Any(c => c.OrderId == orderStatus.FoodOrderId);
                bool isFound1 = _context.OrderStatus.Any(c => c.OrderId == orderStatus.FoodOrderId);
                if (isFound)
                {
                    ModelState.AddModelError("FoodOrderId", "Duplicate Status Found!");
                }
                else
                {
                    _context.Add(orderStatus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "CustomerEmail", orderStatus.OrderId);
            return View(orderStatus);
        }

        // GET: Admins/OrderStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatus.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "CustomerEmail", orderStatus.OrderId);
            return View(orderStatus);
        }

        // POST: Admins/OrderStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderStatusId,FoodOrderId,Status,OrderId")] OrderStatus orderStatus)
        {
            if (id != orderStatus.OrderStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusExists(orderStatus.OrderStatusId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "CustomerEmail", orderStatus.OrderId);
            return View(orderStatus);
        }

        // GET: Admins/OrderStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatus
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderStatusId == id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // POST: Admins/OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderStatus = await _context.OrderStatus.FindAsync(id);
            _context.OrderStatus.Remove(orderStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderStatusExists(int id)
        {
            return _context.OrderStatus.Any(e => e.OrderStatusId == id);
        }
    }
}
