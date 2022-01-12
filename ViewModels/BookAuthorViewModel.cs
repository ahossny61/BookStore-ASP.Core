using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; } 

        [Required]
        [MaxLength(25)]
        [MinLength(4)]
        public string BookTitle { get; set; }

        [Required]
        [StringLength(120,MinimumLength =5)]
        public string BookDescription { get; set; }

        public int AuthorId { get; set; } 

        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; } 
        public string ImgUrl { get; set; }  


    }
}
