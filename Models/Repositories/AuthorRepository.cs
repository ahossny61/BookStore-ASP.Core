using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {
        List<Author> authorList;
       public AuthorRepository()
        {
            authorList = new List<Author>()
            {
                new Author()
                {
                    Name ="Ahmed",
                    Id =1,
                },
                new Author()
                {
                    Name ="Muhamed",
                    Id =2,
                },
            };
        }
        public void Add(Author entity)
        {
            entity.Id = authorList.Max(a => a.Id)+1;
            authorList.Add(entity); 
        }

        public void Delete(int id)
        {
            Author author = Find(id);
            authorList.Remove(author);  
        }

        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public Author Find(int id)
        {
            return authorList.SingleOrDefault(a => a.Id == id);
        }

        public IList<Author> list()
        {
            return authorList;
        }

        public List<Author> Search(string term)
        {
            var result = authorList.Where(a => a.Name.Contains(term)).ToList();
            return result;
        }

        public void Update(Author entity)
        {
            Author author=Find(entity.Id);
            author.Name = entity.Name;


        }
    }
}
