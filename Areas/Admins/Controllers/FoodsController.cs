using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Web.Data;
using OnlineRestaurant.Web.Models;

namespace OnlineRestaurant.Web.Areas.Foods.Controllers
{
    [Area("Admins")]
    [Authorize(Roles = "AppAdmin")]
    public class FoodsController : Controller
    {
       
        public async Task<IActionResult> Index1()
        {
            var applicationDbContext = _context.Foods.Include(f => f.FoodCategory);
            return View(await applicationDbContext.ToListAsync());
        }
        private readonly ApplicationDbContext _context;

        public FoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Foods/Foods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Foods.Include(f => f.FoodCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Foods/Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.FoodCategory)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Foods/Create
        public IActionResult Create()
        {
            ViewData["FoodCategoryId"] = new SelectList(_context.FoodCategories, "FoodCategoryId", "FoodCategoryName");
            return View();
        }

        // POST: Foods/Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,FoodName,Quantity,Price,IsEnabled,FoodCategoryId")] Food food)
        {
            if (ModelState.IsValid)
            {
                //Check for Duplicates
                bool isFound = _context.Foods.Any(c => c.FoodName == food.FoodName);
                if (isFound)
                {
                    ModelState.AddModelError("FoodName", "Duplicate Food Found");
                }
                else
                {
                    _context.Add(food);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["FoodCategoryId"] = new SelectList(_context.FoodCategories, "FoodCategoryId", "FoodCategoryName", food.FoodCategoryId);
            return View(food);
        }
        

        // GET: Foods/Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["FoodCategoryId"] = new SelectList(_context.FoodCategories, "FoodCategoryId", "FoodCategoryName", food.FoodCategoryId);
            return View(food);
        }

        // POST: Foods/Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodId,FoodName,Quantity,Price,IsEnabled,FoodCategoryId")] Food food)
        {
            if (id != food.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Check for Duplicates
                bool isFound = await _context.Foods
                    .AnyAsync(c => c.FoodId != food.FoodId
                        && c.FoodName == food.FoodName);
                if (isFound)
                {
                    ModelState.AddModelError("FoodName", "Duplicate Food Found !");
                }
                else
                {
                    try
                    {
                        _context.Update(food);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FoodExists(food.FoodId))
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
            }
            ViewData["FoodCategoryId"] = new SelectList(_context.FoodCategories, "FoodCategoryId", "FoodCategoryName", food.FoodCategoryId);
            return View(food);
        }

        // GET: Foods/Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.FoodCategory)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.FoodId == id);
        }
    }
}
