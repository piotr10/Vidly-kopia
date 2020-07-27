using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{// Ta klasa to Plain Old CLR Object (POCO) - która reprezentuje stan i zachowanie naszej aplikacji 
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        public Genre Genre { get; set; }

        [Display(Name = "Genre")] // zmiana nazwy na Genre
        [Required]
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1,20)]
        public byte NumberInStock { get; set; }

    }
}