using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Domain.Exceptions;
using Online_Library.Repository;
using Online_Library.Service.Interfaces;

namespace Online_Library.WEB.Controllers
{
    public class AuthorsController(IAuthorsService authorsService) : Controller
    {
        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await authorsService.GetAllAuthors());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var author = await authorsService.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,DateOfBirth")] Author author)
        {
            if (!ModelState.IsValid) return View(author);
            await authorsService.InsertAuthor(author);
            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var author = await authorsService.GetAuthor(id);
            
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Surname,DateOfBirth")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await authorsService.UpdateAuthor(author);
                }
                catch (AuthorNotFoundException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var author = await authorsService.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var author = await authorsService.GetAuthor(id);

            if (author is null)
            {
                return NotFound();
            }

            await authorsService.DeleteAuthor(author);
            return RedirectToAction(nameof(Index));
        }
    }
}
