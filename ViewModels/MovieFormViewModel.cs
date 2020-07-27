using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class MovieFormViewModel
    { //View model dla rozwijanej listy Genre

        #region Sekcja skopiowana z Movi
     //Sekcja skopiowa z Movie w celu zainicjalizowania początkowej wartościo na null co W Number in Stock usunie 0 wyswietlane początkowe w textbox 
        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        public Movie Movie { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        [Required]
        public byte? NumberInStock { get; set; }
        #endregion

        //tytuł w celu ddodanie Edit Movie kiedy edytujemy dany film bądź New Movie kiedy go dodajemy
        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }
        public MovieFormViewModel() // przekazanie Id do konstruktora aby Id= 0 było wypełnione w HiddenFor 
        {//dodanie konstruktora wiąże się z tym, że tworzymy nowy film
            Id = 0;
        }
        //dodajemy konstruktor na potrzeby przekazania obiektu movie do "var viewModel = new MovieFormViewModel(movie)" w action Result w MovieContro
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}