using HW_Backend.Models;
using HW_Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace HW_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("Register")] // the original POST method to insert
        public bool Register([FromBody] Users user)
        {
            return Users.Register(user);
        }

        [HttpDelete("Delete/{id}")] // soft Delete
        public bool Delete(int id)
        {
            return Users.Delete(id);
        }

        [HttpPut("Update/{id}")] // user edit update
        public bool Put([FromRoute] int id, [FromBody] Users user)
        {
            return Users.Update(id, user);
        }

        [HttpPut("SetUserStatus/{userID}/{active}")] // edit user active status
        public int SetUserStatus([FromRoute] int userID, [FromRoute] bool active)
        {
            return Users.SetUserStatus(userID, active);
        }

        [HttpGet("GetAllUsers")] //get all users
        public List<Users> GetAllUsers()
        {
            return Users.GetUsers();
        }

        [HttpPost("Login")] // login auth 
        public IActionResult Login([FromBody] LoginUserDTO loginRequest) {
            return Ok(Users.Login(loginRequest));
        }

        [HttpPost("IsActive/{userID}")]
        public bool isUserActive([FromRoute] int userID)
        {
            return Users.IsActive(userID);
        }
    }
}
