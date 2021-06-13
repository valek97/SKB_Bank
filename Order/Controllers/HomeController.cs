using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Controllers
{
    public class HomeController : Controller
    {
        private readonly PurchaseContext _context;

        public HomeController(PurchaseContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var purchaseContext = _context.Purchases.Include(p => p.Client);
            return View(await purchaseContext.ToListAsync());
        }
    }
}
