using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Dtos;
using Online_Library.Domain.Entities;
using Online_Library.Domain.Exceptions;
using Online_Library.Repository;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.WEB.Controllers
{
    [Authorize]
    public class BooksController(
        IBooksService booksService, 
        IAuthorsService authorsService,
        IGenresService genresService,
        IReadingListService readingListService,
        IUsersService usersService,
        IBooksInReadingListRepository booksInReadingListRepository) : Controller
    {
        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await booksService.GetAllBooksAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var book = await booksService.GetBookAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public  IActionResult Create()
        {
            var authors = authorsService.GetAllAuthors();
            var genres = genresService.GetAllGenres();
            ViewData["Authors"] = authors;
            ViewData["Genres"] = genres;
            
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookDto bookDto)
        {
            var book = new Book();
            if (ModelState.IsValid)
            {
                try
                {
                    book = await booksService.InsertBookAsync(bookDto);
                }
                catch (AuthorNotFoundException)
                {
                    return NotFound();
                }
                catch (GenreNotFoundException)
                {
                    return NotFound();
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await booksService.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            
            var authors = await authorsService.GetAllAuthorsAsync();
            var genres = await genresService.GetAllGenresAsync();
            ViewData["Authors"] = authors;
            ViewData["Genres"] = genres;

            var bookDto = new EditBookDto
            {
                Id = id,
                Title = book.Title,
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                Description = book.Description,
                AuthorId = book.Author!.Id,
                GenreId = book.Genre!.Id,
                Author = book.Author,
                Genre = book.Genre
            };
                
            return View(bookDto);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditBookDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    await booksService.UpdateBookAsync(bookDto);
                }
                catch (AuthorNotFoundException)
                {
                    return NotFound();
                }
                catch (GenreNotFoundException)
                {
                    return NotFound();
                }
                catch (BookNotFoundException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            
            
            return View(bookDto);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await booksService.GetBookAsync(id);
            if (book is null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var book = await booksService.GetBookAsync(id);
            if (book is null)
            {
                return NotFound();
            }

            await booksService.DeleteBookAsync(book);
            return RedirectToAction(nameof(Index));
        }
        
        
        public async Task<IActionResult> AddToReadingList(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var book = await booksService.GetBookAsync(id.Value);

            if (book is null)
            {
                return NotFound();
            }
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return NotFound();
            }
            
            var loggedInUser = usersService.GetUser(userId);


            BooksInReadingList booksInReadingList = new BooksInReadingList
            {
                Id = Guid.NewGuid(),
                BookId = book.Id,
                Book = book,
                ReadingList = loggedInUser.ReadingList,
                ReadingListId = loggedInUser.ReadingList.Id
            };

            return View(booksInReadingList);
        }

        [HttpPost]
        public IActionResult AddToReadingListConfirmed(BooksInReadingList model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return NotFound();
            }

            var loggedInUser = usersService.GetUser(userId);

            loggedInUser.ReadingList.BooksInReadingList ??= new List<BooksInReadingList>();
            
            // Check if the book already exists in the reading list to avoid duplicates
            if (loggedInUser.ReadingList.BooksInReadingList.Any(b => b.BookId == model.BookId))
            {
                // Optionally, return a message indicating the book is already in the list
                return RedirectToAction("Index", "ReadingList");
            }

            // Attach the book entity to the context if it's not already tracked
            var book = booksService.GetBook(model.BookId);
            if (book == null)
            {
                return NotFound(); // Handle case where the book does not exist
            }

            model.Book = book;
            model.ReadingList = loggedInUser.ReadingList;

            loggedInUser.ReadingList.BooksInReadingList.Add(model);

            try
            {
                // usersService.SaveChanges();
                if (!booksInReadingListRepository.BookExistInReadingList(model.Id))
                {
                    booksInReadingListRepository.InsertBooksInReadingList(model);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency exception, for example by reloading the entity and retrying
                ModelState.AddModelError("", "A concurrency error occurred. Please try again.");
                return RedirectToAction("Index", "ReadingList");
            }

            return RedirectToAction("Index", "ReadingList");
        }

    }
}
