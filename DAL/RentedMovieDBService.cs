using HW_Backend.Models;
using HW_Backend.Models.DTO;
using System.Data.SqlClient;

namespace HW_Backend.DAL
{
    public class RentedMovieDBService : DBservices
    {
        public RentedMovieDBService() { }

        public static int Insert(RentedMovieDTO rentedMovie) // insert a movie rented by user, and updates the movie gross and count 
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object> {
                    { "@userID", rentedMovie.UserID },
                    { "@movieID", rentedMovie.MovieID },
                    { "@rentStart", rentedMovie.RentStart },
                    { "@rentEnd", rentedMovie.RentEnd },
                    { "@totalPrice", rentedMovie.TotalPrice }
                };

                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertRentedMovie", con, paramDic))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Dictionary<string, object>> GetRentedMovies(int userID, int offset, int itemsPerPage)
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object> {
                    { "@userID", userID },
                    { "@offset", offset },
                    { "@itemsPerPage" ,itemsPerPage}
                };

                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_GetRentedMovies", con, paramDic))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dictionary<string, object>> array = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            array.Add(new Dictionary<string, object> {
                                {"id" ,Convert.ToInt32(reader["id"]) },
                                {"url" ,reader["url"].ToString() },
                                {"primaryTitle" ,reader["primaryTitle"].ToString()},
                                {"description" ,reader["description"].ToString()},
                                {"primaryImage" ,reader["primaryImage"].ToString()},
                                {"year" ,Convert.ToInt32(reader["year"])},
                                {"releaseDate" ,Convert.ToDateTime(reader["releaseDate"])},
                                {"language" ,reader["language"].ToString()},
                                {"budget" ,Convert.ToDouble(reader["budget"]) },
                                {"grossWorldwide" ,Convert.ToDouble(reader["grossWorldwide"])},
                                {"genres" ,reader["genres"].ToString()},
                                {"isAdult" ,Convert.ToBoolean(reader["isAdult"])},
                                {"runtimeMinutes" ,Convert.ToInt32(reader["runtimeMinutes"])},
                                {"averageRating" ,Convert.ToSingle(reader["averageRating"])},
                                {"numVotes" ,Convert.ToInt32(reader["numVotes"])},
                                {"priceToRent" ,Convert.ToInt32(reader["priceToRent"])},
                                {"rentStart" ,Convert.ToDateTime(reader["rentStart"])},
                                {"rentEnd" ,Convert.ToDateTime(reader["rentEnd"])},
                                {"totalPrice" ,Convert.ToDouble(reader["totalPrice"])}
                            });
                        }
                        return array;
                    }
                }
            }
        }
        public static int GetRentedMoviesCount(int userID)
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object> {
                    { "@userID", userID }
                };
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_GetRentedMoviesCount", con, paramDic))
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

        public static int SendMovieToUser(RentMovieUpdateDTO request)
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    { "@rentingUserID", request.RentingUserID },
                    { "@rentToUserID", request.RentToUserID },
                    { "@movieID", request.MovieID }
                };
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_RentMovieToUser", con, paramDic))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
