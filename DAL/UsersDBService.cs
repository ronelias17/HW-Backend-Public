using HW_Backend.Models;
using System;
using System.Data.SqlClient;
using System.Reflection;

namespace HW_Backend.DAL
{
    public class UsersDBService:DBservices
    {
        public UsersDBService() { }

        public static int Insert(Users user) // Registering user
        {
            using (SqlConnection con = connect("myProjDB")) 
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    { "@name", user.Name },
                    { "@email", user.Email },
                    { "@password", user.Password },
                    { "@salt", user.Salt }
                };
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertUser", con, paramDic))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static bool IsActive(int userID) // Registering user
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    {"@id", userID}
                };
                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_IsUserActive", con, paramDic))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        return Convert.ToBoolean(reader["active"]);
                    } else {
                        return false;
                    }
                }
            }
        }

        public static int Delete(int id) // soft delete user
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object> {
                    {"@id", id }
                };

                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_DeleteUser", con, paramDic))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
         }
        
        public static int Update(int id,Users user) // edit user
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    {"@id", id },
                    {"@email", user.Email},
                    {"@password", user.Password},
                    {"@name", user.Name},
                    {"@salt", user.Salt}
                };

                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_UpdateUser", con, paramDic))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static Dictionary<string, object> GetLoginCredentials(string userEmail) // select for login credentials
        {
            using (SqlConnection con = connect("myProjDB"))
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>
                {
                    {"@email", userEmail}
                };

                using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_SelectLoginCredentials", con, paramDic))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        return new Dictionary<string, object>
                        {
                            { "password", reader["password"] },
                            { "salt", reader["salt"] },
                            { "name", reader["name"] },
                            { "id", reader["id"] }
                        };
                    } else  {
                        return null;
                    }
                }
            }
        }


    public static List<Users> GetAllUsers()
    {
        using (SqlConnection con = connect("myProjDB"))
        {
            Dictionary<string, object> paramDic = new Dictionary<string, object> { };
            using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_GetAllUsers", con, paramDic))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Users> array = new List<Users>();
                    while (reader.Read())
                    {
                        array.Add(new Users(
                            Convert.ToInt32(reader["id"]),
                            reader["name"].ToString(),
                            reader["email"].ToString(),
                            "",
                            Convert.ToBoolean(reader["active"]),
                            "")
                        );
                    }
                    return array;
                }
            }
        }
    }

    public static int SetUserStatus(int userID,bool active)
    {
      using (SqlConnection con = connect("myProjDB"))
      {
        Dictionary<string, object> paramDic = new Dictionary<string, object>
        {
            { "@id", userID }, 
            { "@active", active}
        };
        using (SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_UpdateUserStatus", con, paramDic))
        {
            return cmd.ExecuteNonQuery();
        }
      }
    }
  }
}
