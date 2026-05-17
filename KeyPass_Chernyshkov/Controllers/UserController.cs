using KeyPass_Chernyshkov.Classes;
using KeyPass_Chernyshkov.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeyPass_Chernyshkov.Controllers
{
    [Route("/user")]
    public class UserController : Controller
    {
        private DatabaseManager databaseManager;

        public UserController()
        {
            this.databaseManager = new DatabaseManager();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromForm] string login, [FromForm] string password)
        {
            try
            {
                User? AuthUser = databaseManager.Users
                    .Where(x => x.Login == login && x.Password == password)
                    .FirstOrDefault();

                if (AuthUser == null)
                {
                    return StatusCode(401);
                }
                else
                {
                    string Token = JwtToken.Generate(AuthUser);
                    AuthUser.LastAuth = DateTime.Now;
                    databaseManager.SaveChanges();
                    return Ok(new { token = Token });
                }
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }
    }
}
