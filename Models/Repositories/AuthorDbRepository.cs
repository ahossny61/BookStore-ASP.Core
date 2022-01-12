using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class AuthorDbRepository : IBookStoreRepository<Author>
    {
        BookStoreDBContext db;
        public AuthorDbRepository(BookStoreDBContext _db)
        {
            this.db = _db;

        }
        public void Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Author author = Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            return db.Authors.SingleOrDefault(a => a.Id == id);
        }

        public IList<Author> list()
        {
            return db.Authors.ToList();
        }

        public List<Author> Search(string term)
        {
            var result = db.Authors.Where(a => a.Name.Contains(term)).ToList();
            return result;
        }

        public void Update(Author entity)
        {
            db.Authors.Update(entity);
            db.SaveChanges();
        }



    }
}