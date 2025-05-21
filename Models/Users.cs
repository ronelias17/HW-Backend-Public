
using HW_Backend.DAL;
using HW_Backend.Models.DTO;
using Konscious.Security.Cryptography;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace HW_Backend.Models
{
    public class Users
    {
        int id;
        string name, email, password, salt;
        bool active = true;
        
        public Users() { }

        public Users(int id,string name, string email, string password, bool active, string salt)
        {
            Salt = salt;
            Name = name;
            Email = email;
            Password = password;
            Active = active;
            Id = id;
        }

        public static bool Register(Users user) 
        {
            byte[] generatedSalt = GenerateSalt(); // generated salt to add
            user.Password = HashPassword(user.Password, generatedSalt); // hash the password
            user.Salt = Convert.ToBase64String(generatedSalt); // save salt as string

            try
            {
                UsersDBService.Insert(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool Delete(int id)
        {

            try
            {
                if (UsersDBService.Delete(id) == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool Update(int id, Users user) 
        {
            if (!user.password.Equals("")) { 
                byte[] generatedSalt = GenerateSalt(); // generated NEW salt to add
                user.Password = HashPassword(user.Password, generatedSalt); // hash the password
                user.Salt = Convert.ToBase64String(generatedSalt); // save salt as string
            } else { 
                user.Salt = "";
            }        
            
            try
            {
                if (UsersDBService.Update(id, user) == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        public static List<Users> GetUsers()
        {
            try {
                return UsersDBService.GetAllUsers();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static int SetUserStatus(int userID,bool active)
        {
            try {
                return UsersDBService.SetUserStatus(userID, active);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public static bool IsActive(int userID)
        {
            try
            {
                return UsersDBService.IsActive(userID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static Dictionary<string, object> Login(LoginUserDTO loginRequest)
        {
            try { 
                Dictionary<string, object> userData = UsersDBService.GetLoginCredentials(loginRequest.Email);
                if (userData != null) {
                    string userPassword = userData["password"].ToString();
                    string userSalt = userData["salt"].ToString();
                    byte[] byteSalt = Convert.FromBase64String(userSalt); // getting the traget database user salt
                    // checking if given password was correct
                    if (userPassword == HashPassword(loginRequest.Password, byteSalt)) {
                        return new Dictionary<string, object> {
                            {"status", true },
                            {"name", userData["name"].ToString()},
                            {"id", Convert.ToInt32(userData["id"])}
                        };
                    } 
                } 
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return new Dictionary<string, object> {
                {"status", false }
            };
        }
        private static string HashPassword(string password, byte[] salt)
        {
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 4;
                argon2.MemorySize = 65536;
                argon2.Iterations = 4;

                var hashBytes = argon2.GetBytes(32);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private static byte[] GenerateSalt(int size = 16)
        {
            var salt = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public int Id { get => id; private set => id = value; }
        public string? Salt { get => salt; private set => salt = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool Active { get => active; set => active = value; }
    }
}
