using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public IMapper _mapper;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers ->aby uzyskać zasób
        //  public IHttpActionResult GetMovies()
        //  {
        //      return Ok(_context.Movies.ToList().Select(_mapper.Map<Movie, MovieDto>));
        //  }

        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies
                .Include(m => m.Genre)
                .ToList()
                .Select(_mapper.Map<Movie, MovieDto>);
        }

        // GET /api/customers/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/customers ->aby utworzyć zasób
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);

            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/customers/1 ->aby zaaktualizować klineta
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
            {
                return NotFound();
            }

            _mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1 ->aby usunąć klineta
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
