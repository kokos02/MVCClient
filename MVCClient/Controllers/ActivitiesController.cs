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
    public class ActivitiesController : Controller
    {
        private readonly MVCClientContext _context;

        public ActivitiesController(MVCClientContext context)
        {
            _context = context;
        }

        // GET: Activities
        public async Task<IActionResult> Index(string searchString)
        {
            var ActivitiesList = from Activities in _context.Activities
                                    select Activities;

            ActivitiesList = ActivitiesList.Include(c => c.ActivityType);
            ActivitiesList = ActivitiesList.Include(c => c.Customer);

            if (!String.IsNullOrEmpty(searchString))
            {
                ActivitiesList = ActivitiesList.Where(s => (s.Description.Contains(searchString)) 
                || (s.Customer.Name.Contains(searchString)) || (s.ActivityType.Description.Contains(searchString)));
                return View(await ActivitiesList.ToListAsync());
            }

            var mVCClientContext = _context.Activities.Include(a => a.ActivityType).Include(a => a.Customer);
            return View(await mVCClientContext.ToListAsync());
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activities = await _context.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.ActivitiesId == id);
            if (activities == null)
            {
                return NotFound();
            }

            return View(activities);
        }

        // GET: Activities/Create
        public IActionResult Create()
        {
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityTypes>(), "ActivityTypeId", "Description");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Name");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivitiesId,Description,CustomerId,StartDate,EndDate,ActivityTypeId")] Activities activities)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(activities);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return RedirectToAction("ActionFailed", "Activities");
                }
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityTypes>(), "ActivityTypeId", "Description", activities.ActivityTypeId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Address", activities.CustomerId);
            return View(activities);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activities = await _context.Activities.FindAsync(id);
            if (activities == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityTypes>(), "ActivityTypeId", "Description", activities.ActivityTypeId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Address", activities.CustomerId);
            return View(activities);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivitiesId,Description,CustomerId,StartDate,EndDate,ActivityTypeId")] Activities activities)
        {
            if (id != activities.ActivitiesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivitiesExists(activities.ActivitiesId))
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
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityTypes>(), "ActivityTypeId", "Description", activities.ActivityTypeId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Address", activities.CustomerId);
            return View(activities);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activities = await _context.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.ActivitiesId == id);
            if (activities == null)
            {
                return NotFound();
            }

            return View(activities);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var activities = await _context.Activities.FindAsync(id);
                _context.Activities.Remove(activities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("ActionFailed", "Activities");
            }
        }

        public IActionResult ActionFailed()
        {
            return View();
        }

        private bool ActivitiesExists(int id)
        {
            return _context.Activities.Any(e => e.ActivitiesId == id);
        }
    }
}
