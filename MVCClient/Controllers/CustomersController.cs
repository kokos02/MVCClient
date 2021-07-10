using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCClient.Data;
using MVCClient.Models;

namespace MVCClient.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MVCClientContext _context;

        public CustomersController(MVCClientContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string searchString)
        {

            var customersList = from customers in _context.Customer
                                    select customers;

            customersList = customersList.Include(c => c.CustomerType);

            if (!String.IsNullOrEmpty(searchString))
            {
                customersList = customersList.Where(s => (s.Name.Contains(searchString)) 
                || (s.Address.Contains(searchString)) || (s.CustomerType.Description.Contains(searchString)));
            }


            //var mVCClientContext = _context.Customer.Include(c => c.CustomerType); //initial scaffolding code
            return View(await customersList.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["CustomerTypeId"] = new SelectList(_context.Set<CustomerTypes>(), "CustomerTypeId", "Description");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Name,Address,CustomerTypeId")] Customers customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return RedirectToAction("ActionFailed", "Customers");
                }
            }
            ViewData["CustomerTypeId"] = new SelectList(_context.Set<CustomerTypes>(), "CustomerTypeId", "Description", customer.CustomerTypeId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["CustomerTypeId"] = new SelectList(_context.Set<CustomerTypes>(), "CustomerTypeId", "Description", customer.CustomerTypeId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Name,Address,CustomerTypeId")] Customers customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            ViewData["CustomerTypeId"] = new SelectList(_context.Set<CustomerTypes>(), "CustomerTypeId", "Description", customer.CustomerTypeId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var customer = await _context.Customer.FindAsync(id);
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("ActionFailed", "Customers");
            }
        }

        public IActionResult ActionFailed()
        {
            return View();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
