using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class BookDbRepository : IBookStoreRepository<Book>
    {
        BookStoreDBContext db;
        public BookDbRepository(BookStoreDBContext db)
        {
            this.db = db;
        }
        public void Add(Book entity)
        {
           
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }


        public Book Find(int id)
        {
            Book book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> list()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void Update(Book entity)
        {
            db.Books.Update(entity);
            db.SaveChanges();
        }
        public List<Book> Search(string term)
        {
            var result = db.Books.Include(a => a.Author).Where(b => b.Title.Contains(term)
                || b.Descroption.Contains(term)
                || b.Author.Name.Contains(term)).ToList();
            return result;
        }
    }
}

