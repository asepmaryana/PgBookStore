using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PgBookStore.Models;
using PgBookStore.Data;

namespace PgBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext db;
        private IWebHostEnvironment _environment;

        public BookController(ApplicationDbContext dbContext, IWebHostEnvironment environment)
        {
            db = dbContext;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var bookList = db.Books
                .Include(c => c.Category)
                .Include(ba => ba.BooksAuthors)
                .ThenInclude(a => a.Author)
                .ToList();
            
            IList<BookViewModel> items = new List<BookViewModel>();
            foreach (Book book in bookList)
            {
                BookViewModel item = new BookViewModel();
                item.ISBN = book.BookID;
                item.Title = book.Title;
                item.Photo = book.Photo;
                item.PublishDate = book.PublishDate;
                item.Price = book.Price;
                item.Quantity = book.Quantity;
                item.CategoryName = book.Category.Name;

                string authorNameList = string.Empty;
                var booksAuthorsList = book.BooksAuthors;
                foreach (BookAuthor bookAuthor in booksAuthorsList)
                {
                    var author = bookAuthor.Author;
                    authorNameList = authorNameList + author.Name + ", ";
                }
                item.AuthorNames = authorNameList.Substring(0, authorNameList.Length - 2);

                items.Add(item);
            }

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Catetories.ToList(), "CategoryID", "Name");
            ViewBag.Authors = new MultiSelectList(db.Authors.ToList(), "AuthorID", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(BookFormViewModel item)
        {
            if(ModelState.IsValid) {
                Book book = new Book();
                book.BookID = item.ISBN;
                book.CategoryID = item.CategoryID;
                book.Title = item.Title;
                book.PublishDate = item.PublishDate;
                book.Price = item.Price;
                book.Quantity = item.Quantity;

                foreach (int authorId in item.AuthorIDs)
                {
                    BookAuthor bookAuthor = new BookAuthor();
                    bookAuthor.BookID = item.ISBN;
                    bookAuthor.AuthorID = authorId;

                    db.Add(bookAuthor);
                }
                
                if (item.Photo != null) {
                    var file = item.Photo;
                    var uploads = Path.Combine(_environment.WebRootPath, "upload");
                    //uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    if (file.Length > 0) {
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create)){
                            file.CopyTo(fileStream);
                        }
                    }
                    book.Photo = file.FileName;
                }

                db.Add(book);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = new SelectList(db.Catetories.ToList(), "CategoryID", "Name");
            ViewBag.Authors = new MultiSelectList(db.Authors.ToList(), "AuthorID", "Name");

            var book = db.Books.SingleOrDefault(p => p.BookID.Equals(id));
            BookFormViewModel item = new BookFormViewModel();
            item.ISBN   = book.BookID;
            item.Title  = book.Title;
            item.PublishDate    = book.PublishDate;
            item.Price  = book.Price;
            item.Quantity   = book.Quantity;
            item.CategoryID = book.CategoryID;

            var authorList  = db.BookAuthors.Where(p => p.BookID.Equals(book.BookID)).ToList();
            List<int> authors = new List<int>();
            foreach (BookAuthor bookAuthor in authorList)
            {
                authors.Add(bookAuthor.AuthorID);
            }
            item.AuthorIDs  = authors.ToArray();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ISBN,CategoryID,Title,Photo,PublishDate,Price,Quantity,AuthorIDs")] BookFormViewModel item)
        {
            if(ModelState.IsValid) {
                db.BookAuthors.RemoveRange(db.BookAuthors.Where(p => p.BookID.Equals(item.ISBN)));
                db.SaveChanges();

                Book book = db.Books.SingleOrDefault(p => p.BookID.Equals(item.ISBN));
                book.CategoryID = item.CategoryID;
                book.Title = item.Title;
                book.PublishDate = item.PublishDate;
                book.Price = item.Price;
                book.Quantity = item.Quantity;
                
                if (item.Photo != null) {
                    var file = item.Photo;
                    var uploads = Path.Combine(_environment.WebRootPath, "upload");
                    if (file.Length > 0) {
                        using(var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create)) {
                            file.CopyTo(fileStream);
                        }
                    }
                    book.Photo = file.FileName;
                }

                db.Update(book);

                foreach (int authorId in item.AuthorIDs)
                {
                    BookAuthor bookAuthor = new BookAuthor();
                    bookAuthor.BookID = item.ISBN;
                    bookAuthor.AuthorID = authorId;

                    db.Add(bookAuthor);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid) {
                var item = db.Books.Find(id);
                db.BookAuthors.RemoveRange(db.BookAuthors.Where(p => p.BookID.Equals(item.BookID)));
                db.SaveChanges();

                db.Books.Remove(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}