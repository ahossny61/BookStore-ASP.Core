using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorStoryRepository;
        private readonly IHostingEnvironment hosting;
        public BookController(IBookStoreRepository<Book> _bookRepository, IBookStoreRepository<Author> _authorStoryRepository,
            IHostingEnvironment _hosting)
        {
            this.bookRepository = _bookRepository;
            this.authorStoryRepository = _authorStoryRepository;
            this.hosting = _hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var book = bookRepository.list();
            return View(book);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel()
            {
                Authors = authorStoryRepository.list().ToList(),
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = UploadFile(model.File);

                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.BookTitle,
                        Descroption = model.BookDescription,
                        Author = authorStoryRepository.Find(model.AuthorId),
                        ImgUrl = fileName,

                    };

                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            
            ModelState.AddModelError("", "You Have to fill all the required fields!");
            return View(getAllAuthors()); 

        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var model = new BookAuthorViewModel
            {
                BookId = book.Id,
                BookTitle = book.Title,
                BookDescription = book.Descroption,
                AuthorId = authorId,
                Authors = authorStoryRepository.list().ToList(),
                ImgUrl=book.ImgUrl,
            };
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel model)
        {

            try
            {
                string fileName = CompareAndUbloadFile(model.File,model.ImgUrl);
                
                Book book = new Book
                {
                    Id = model.BookId,
                    Title = model.BookTitle,
                    Descroption = model.BookDescription,
                    Author = authorStoryRepository.Find(model.AuthorId),
                    ImgUrl = fileName,

                };
                bookRepository.Update(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> FillSelectList()
        {

            var authors = authorStoryRepository.list().ToList();
            return authors;
        }

        public ActionResult Search(string term)
        {
            var result = bookRepository.Search(term);
            return View("Index",result);
        }
        BookAuthorViewModel getAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList(),
            };
            return vmodel;
        }
        string UploadFile(IFormFile File)
        {
            string fileName = string.Empty;
            if (File != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                fileName = File.FileName;
                string fullPAth = Path.Combine(uploads, fileName);
                File.CopyTo(new FileStream(fullPAth, FileMode.Create));

            }
            return fileName;
        }
        
        string CompareAndUbloadFile(IFormFile File,string ImgUrl)
        {
            if (File != null)
            {
                string fullOldPath = string.Empty;
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fileName = File.FileName;
                string fullPAth = Path.Combine(uploads, fileName);
                //Delete the old file
                string oldFileName = ImgUrl;
                if (oldFileName != null && oldFileName != "")
                {
                    fullOldPath = Path.Combine(uploads, oldFileName);
                }
                if (fullOldPath != fullPAth)
                {
                    if (oldFileName != null && oldFileName != "")
                        System.IO.File.Delete(fullOldPath);
                    //Save the new file
                    File.CopyTo(new FileStream(fullPAth, FileMode.Create));
                }
                return File.FileName;
            }
            return ImgUrl;
        }

        
    }
}
