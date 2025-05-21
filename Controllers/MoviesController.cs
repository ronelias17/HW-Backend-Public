using HW_Backend.Models;
using HW_Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        [HttpPost("Insert")] // creating new movie 
        public bool Insert([FromBody] Movies newMovie)
        {
            return Movies.Insert(newMovie);
        }

        [HttpDelete("Delete/{id}")] // soft delete for movie
        public bool Delete(int id)
        {
            return Movies.Delete(id);
        }

        [HttpPut("Update/{id}")] // updating movie
        public bool Put(int id, [FromBody] Movies movie)
        {
            return Movies.Update(id, movie);
        }

        [HttpGet("GetGenresLanguage")] // sending genres and language lists for client
        public IActionResult GetGenresAndLanguage() 
        {
            return Ok(Movies.GetGenresAndLanguage());
        }

        [HttpGet("GetRentedMovies/{userID}/{offset}/{itemsPerPage}")] // sending users current rented movies  
        public List<Dictionary<string, object>> GetRentedMovies([FromRoute] int userID, [FromRoute] int offset, [FromRoute] int itemsPerPage)
        {
            return Movies.GetRentedMovies(userID, offset, itemsPerPage);
        }

        [HttpGet("GetRentedMoviesCount/{userID}")] // Count For Pagination
        public int GetRentedMoviesCount([FromRoute] int userID)
        {
            return Movies.GetRentedMoviesCount(userID);
        }

        [HttpGet("GetAllMovies/{offset}/{itemsPerPage}")] // getting all movies
        public List<Movies> GetAllMovies([FromRoute] int offset, [FromRoute] int itemsPerPage)
        {
            return Movies.GetAllMovies(offset, itemsPerPage);
        }

        [HttpGet("GetAllMoviesCount")] // Count For Pagination
        public int GetAllMoviesCount()
        {
            return Movies.GetAllMoviesCount();
        }

        [HttpGet("GetByTitle/{title}/{offset}/{itemsPerPage}")] // getting movies filtered by title
        public List<Movies> GetByTitle([FromRoute] string title ,[FromRoute] int offset, [FromRoute] int itemsPerPage)
        {
            return Movies.GetByTitle(title, offset, itemsPerPage);
        }

        [HttpGet("GetByTitleCount/{title}")] // Count For Pagination
        public int GetByTitleCount([FromRoute] string title)
        {
            return Movies.GetByTitleCount(title);
        }

        [HttpPut("SendMovieToUser")] // change rented movie owner, (send it to other user)
        public int SendMovieToUser([FromBody] RentMovieUpdateDTO request)
        {
            return Movies.SendMovieToUser(request);
        }

        [HttpPost("RentMovie")] // rent a movie to the user
        public bool RentMovie([FromBody] RentedMovieDTO rentedMovie)
        {
            return Movies.RentMovie(rentedMovie);
        }

        [HttpGet("GetByReleaseDate/from/{startDate}/until/{endDate}/{offset}/{itemsPerPage}")] // resource route search
        public List<Movies> GetByReleaseDate([FromRoute] DateTime startDate, [FromRoute] DateTime endDate, [FromRoute] int offset, [FromRoute] int itemsPerPage)
        {
            return Movies.GetByReleaseDate(startDate, endDate, offset, itemsPerPage);
        }

        [HttpGet("GetByReleaseDateCount/from/{startDate}/until/{endDate}")] // Count For Pagination
        public int GetByReleaseDateCount([FromRoute] DateTime startDate, [FromRoute] DateTime endDate)
        {
            return Movies.GetByReleaseDateCount(startDate, endDate);
        }
    }
}
