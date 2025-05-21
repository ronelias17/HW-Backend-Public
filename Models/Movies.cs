using HW_Backend.DAL;
using HW_Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace HW_Backend.Models
{
    public class Movies
    {
        int id, year, runtimeMinutes, numVotes, priceToRent;
        string url, genres, primaryTitle, description, primaryImage, language;
        DateTime releaseDate;
        double budget, grossWorldwide;
        bool isAdult;
        float averageRating;
        public Movies() { }

        public Movies(int id,string url, string primaryTitle, string description, string primaryImage, int year, DateTime releaseDate, string language, double budget, double grossWorldwide, string genres, bool isAdult, int runtimeMinutes, float averageRating, int numVotes, int priceToRent)
        {
            Url = url;
            PrimaryTitle = primaryTitle;
            Description = description;
            PrimaryImage = primaryImage;
            Year = year;
            ReleaseDate = releaseDate;
            Language = language;
            Budget = budget;
            GrossWorldwide = grossWorldwide;
            Genres = genres;
            IsAdult = isAdult;
            RuntimeMinutes = runtimeMinutes;
            AverageRating = averageRating;
            NumVotes = numVotes;
            Id = id;
            PriceToRent = priceToRent;
        }

        public static bool Insert(Movies movie)
        {
            try {
                MoviesDBService.Insert(movie);
                return true;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        
        public static bool Delete(int removeID)
        {
            try {
                if (MoviesDBService.Delete(removeID) == 1)
                    return true;
                else
                    return false;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool Update(int id,Movies movie)
        {
            try {
                if(MoviesDBService.Update(id,movie) == 1)
                    return true;
                else
                    return false;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static List<Dictionary<string, object>> GetRentedMovies(int userID, int offset, int itemsPerPage)
        {
            try {
                return RentedMovieDBService.GetRentedMovies(userID,offset,itemsPerPage);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static int GetRentedMoviesCount(int userID) // for pagination 
        {
            try {
                return RentedMovieDBService.GetRentedMoviesCount(userID);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public static int SendMovieToUser(RentMovieUpdateDTO request)
        {
            try {
                return RentedMovieDBService.SendMovieToUser(request);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public static bool RentMovie(RentedMovieDTO rentMovie)
        {
            try {
                if (RentedMovieDBService.Insert(rentMovie) == 2)
                    return true;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public static List<Movies> GetAllMovies(int offset, int itemsPerPage)
        {
            try {
                return MoviesDBService.GetAllMovies(offset, itemsPerPage);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static int GetAllMoviesCount() // for pagination
        {
            try {
                return MoviesDBService.GetAllMoviesCount();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public static List<Movies> GetByTitle(string title, int offset, int itemsPerPage)
        {
            try {
                return MoviesDBService.GetByTitle(title, offset, itemsPerPage);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static int GetByTitleCount(string title) // for pagination
        {
            try {
                return MoviesDBService.GetByTitleCount(title);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public static List<Movies> GetByReleaseDate(DateTime startDate, DateTime endDate, int offset, int itemsPerPage)
        {
            try
            {
                return MoviesDBService.GetByReleaseDate(startDate, endDate,offset,itemsPerPage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static int GetByReleaseDateCount(DateTime startDate, DateTime endDate)
        {
            try
            {
                return MoviesDBService.GetByReleaseDateCount(startDate, endDate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public static Dictionary<string, object> GetGenresAndLanguage() 
        {
            try {
                return MoviesDBService.GetGenresAndLanguage();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }        
        public int Id { get => id; private set => id = value; }
        public string PrimaryTitle { get => primaryTitle; set => primaryTitle = value; }
        public string Description { get => description; set => description = value; }
        public string Url { get => url; set => url = value; }
        public string PrimaryImage { get => primaryImage; set => primaryImage = value; }
        public int Year { get => year; set => year = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public string Language { get => language; set => language = value; }
        public string Genres { get => genres; set => genres = value; }
        public double Budget { get => budget; set => budget = value; }
        public double GrossWorldwide { get => grossWorldwide; set => grossWorldwide = value; }
        public bool IsAdult { get => isAdult; set => isAdult = value; }
        public int RuntimeMinutes { get => runtimeMinutes; set => runtimeMinutes = value; }
        public float AverageRating { get => averageRating; set => averageRating = value; }
        public int NumVotes { get => numVotes; set => numVotes = value; }
        public int PriceToRent { get => priceToRent; private set => priceToRent = value; }
    }
}
