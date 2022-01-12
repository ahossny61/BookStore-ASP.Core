using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        List<Book> books;
       public BookRepository()
        {
            books = new List<Book>()
            {
                new Book() {
                    Id = 1,
                    Title ="C# programming",
                    Descroption ="hello for My book",
                    Author=new Author(){Id=1},
                    ImgUrl="naruto.jpg",
                },
                new Book() {
                    Id = 2,
                    Title ="Java programming",
                    Descroption ="hello for My book",
                    Author=new Author(){Id=2},
                },
                new Book() {
                    Id = 3,
                    Title ="Dart programming",
                    Descroption ="hello for My book",
                    Author=new Author(){Id=2},
                },
            };  
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id)+1;
            books.Add(entity);  
        }

        public void Delete(int id)
        {
            var book=Find(id);  
            books.Remove(book);
        }

        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public Book Find(int id)
        {
             Book book=books.SingleOrDefault(b => b.Id == id);
             return book;
        }

        public IList<Book> list()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            var result=books.Where(b=>b.Title.Contains(term)).ToList();
            return result;
        }

        public void Update(Book entity)
        {
            var book = Find(entity.Id);

            book.Title = entity.Title;
            book.Descroption = entity.Descroption;
            book.Author = entity.Author;
            book.ImgUrl = entity.ImgUrl;
            
        }
    }
}
