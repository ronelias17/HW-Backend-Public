using HW_Backend.Models;
using System.Data.SqlClient;

namespace HW_Backend.DAL
{
    public class MoviesDBService : DBservices
    {
        public MoviesDBService() { }
        public static int Insert(Movies movie) // inserting an movie to db
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    { "@url", movie.Url },
                    { "@primaryTitle", movie.PrimaryTitle },
                    { "@description", movie.Description },
                    { "@primaryImage", movie.PrimaryImage },
                    { "@releaseDate", movie.ReleaseDate },
                    { "@year", movie.Year },
                    { "@language", movie.Language },
                    { "@budget", movie.Budget },
                    { "@grossWorldwide", movie.GrossWorldwide },
                    { "@genres", movie.Genres },
                    { "@isAdult", movie.IsAdult ? 1 : 0 },
                    { "@runtimeMinutes", movie.RuntimeMinutes },
                    { "@averageRating", movie.AverageRating },
                    { "@numVotes", movie.NumVotes }
                };

                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertMovie", con, paramDic))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Movies> GetAllMovies(int offset, int itemsPerPage)
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object> {
                    { "@offset", offset },
                    { "@itemsPerPage" ,itemsPerPage}
                };
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_GetAllMovies", con, paramDic))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Movies> array = new List<Movies>();
                        while (reader.Read()) {
                            array.Add(CreateMovieFromReader(reader));
                        }
                        return array;
                    }
                }
            }
        }

        public static int GetAllMoviesCount() // for pagination 
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>();
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_GetAllMoviesCount", con, paramDic))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return Convert.ToInt32(reader["TotalCount"]);
                        else                       
                            return 0;
                    }
                }
            }
        }

        public static List<Movies> GetByTitle(string title, int offset, int itemsPerPage)
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>()
                {
                    { "@primaryTitle", title },
                    { "@offset", offset },
                    { "@itemsPerPage" ,itemsPerPage}
                };
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_GetMoviesByTitle", con, paramDic))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Movies> array = new List<Movies>();
                        while (reader.Read()) {
                            array.Add(CreateMovieFromReader(reader));
                        }
                        return array;
                    }
                }
            }
        }

        public static int GetByTitleCount(string title) // for pagination 
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>()
                {
                    { "@primaryTitle", title }
                };
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_GetMoviesByTitleCount", con, paramDic))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return Convert.ToInt32(reader["TotalCount"]);
                        else
                            return 0;
                    }
                }
            }
        }

        public static List<Movies> GetByReleaseDate(DateTime startDate, DateTime endDate, int offset, int itemsPerPage)
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>()
                {
                    { "@from", startDate },
                    { "@to", endDate },
                    { "@offset", offset },
                    { "@itemsPerPage" ,itemsPerPage}
                };
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_GetMoviesByDates", con, paramDic))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Movies> array = new List<Movies>();
                        while (reader.Read()) {
                            array.Add(CreateMovieFromReader(reader));
                        }
                        return array;
                    }
                }
            }
        }

        public static int GetByReleaseDateCount(DateTime startDate, DateTime endDate) // for pagination 
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>()
                {
                    { "@from", startDate },
                    { "@to", endDate }
                };
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_GetMoviesByDatesCount", con, paramDic))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return Convert.ToInt32(reader["TotalCount"]);
                        else
                            return 0;
                    }
                }
            }
        }

        public static int Delete(int id) // soft delete an movie
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    { "@id", id }
                };

                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_DeleteMovie", con, paramDic))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static int Update(int id,Movies movie) // edit movie
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    { "@id", id },
                    { "@url", movie.Url },
                    { "@primaryTitle", movie.PrimaryTitle },
                    { "@description", movie.Description },
                    { "@primaryImage", movie.PrimaryImage },
                    { "@releaseDate", movie.ReleaseDate },
                    { "@year", movie.Year },
                    { "@language", movie.Language },
                    { "@budget", movie.Budget },
                    { "@grossWorldwide", movie.GrossWorldwide },
                    { "@genres", movie.Genres },
                    { "@isAdult", movie.IsAdult ? 1 : 0 },
                    { "@runtimeMinutes", movie.RuntimeMinutes },
                    { "@averageRating", movie.AverageRating },
                    { "@numVotes", movie.NumVotes }
                };

                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_UpdateMovie", con, paramDic))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static Dictionary<string,object> GetGenresAndLanguage()
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object> {};
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_SelectGenresAndLanguage", con, paramDic))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        HashSet<string> languages = new HashSet<string>();
                        HashSet<string> genres = new HashSet<string>();
                        bool hasRows = false;

                        while (reader.Read())
                        {
                            hasRows = true;
                            languages.Add(reader["language"].ToString());
                            foreach (string genre in reader["genres"].ToString().Split(","))
                            {
                                genres.Add(genre);
                            }
                        }

                        if (!hasRows)
                            return null;
                        return new Dictionary<string, object> {
                            {"language", languages.ToArray() },
                            {"genres", genres.ToArray() }
                        };
                    }
                }
            }
        }
    }
}
