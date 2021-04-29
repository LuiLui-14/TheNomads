using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Playlistofy.Data.Abstract;
using Playlistofy.Models;

namespace Playlistofy.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistRepository _arRepo;

        public ArtistsController(IArtistRepository arRepo)
        {
            _arRepo = arRepo;
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            return View(await _arRepo.GetAll().ToListAsync());
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _arRepo.FindByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Popularity,Uri")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                await _arRepo.AddAsync(artist);
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _arRepo.FindByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Popularity,Uri")] Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _arRepo.UpdateAsync(artist);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ArtistExists(artist.Id))
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
            return View(artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _arRepo.FindByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var artist = await _arRepo.FindByIdAsync(id);
            await _arRepo.DeleteAsync(artist);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ArtistExists(string id)
        {
            bool value = await _arRepo.ExistsAsync(id);
            return value;
        }
    }
}
