using KeyPass_Chernyshkov.Classes;
using KeyPass_Chernyshkov.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeyPass_Chernyshkov.Controllers
{
    [Route("/storage")]
    public class StorageController : Controller
    {
        private DatabaseManager databaseManager;

        public StorageController()
        {
            this.databaseManager = new DatabaseManager();
        }

        [Route("get")]
        [HttpGet]
        public IActionResult Get([FromHeader] string token)
        {
            try
            {
                int? idUser = JwtToken.GetUserIdFromToken(token);
                if (idUser == null)
                {
                    return StatusCode(401);
                }

                var storages = databaseManager.Storages
                    .Where(x => x.User.Id == idUser)
                    .Select(s => new StorageDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Url = s.Url,
                        Login = s.Login,
                        Password = s.Password
                    })
                    .ToList();

                return Ok(storages);
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add([FromHeader] string token, [FromBody] Storage storage)
        {
            try
            {
                int? idUser = JwtToken.GetUserIdFromToken(token);
                if (idUser == null)
                {
                    return StatusCode(401);
                }

                storage.User = databaseManager.Users.First(x => x.Id == idUser);

                databaseManager.Storages.Add(storage);
                databaseManager.SaveChanges();

                storage.User = null;
                return StatusCode(200, storage);
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }

        [Route("update")]
        [HttpPut]
        public IActionResult Update([FromHeader] string token, [FromBody] Storage storage)
        {
            try
            {
                int? idUser = JwtToken.GetUserIdFromToken(token);
                if (idUser == null)
                {
                    return StatusCode(401);
                }

                Storage? uStorage = databaseManager.Storages
                    .Where(x => x.Id == storage.Id)
                    .FirstOrDefault();

                if (uStorage == null)
                {
                    return StatusCode(404);
                }

                uStorage.Name = storage.Name;
                uStorage.Url = storage.Url;
                uStorage.Login = storage.Login;
                uStorage.Password = storage.Password;

                databaseManager.SaveChanges();

                storage.User = null;
                return StatusCode(200, storage);
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }

        [Route("delete")]
        [HttpDelete]
        public IActionResult Delete([FromHeader] string token, [FromForm] int id)
        {
            try
            {
                int? idUser = JwtToken.GetUserIdFromToken(token);
                Storage? storage = databaseManager.Storages
                    .Where(x => x.Id == id && x.User.Id == idUser)
                    .FirstOrDefault();

                if (idUser == null)
                {
                    return StatusCode(401);
                }

                if (storage == null)
                {
                    return StatusCode(404);
                }

                databaseManager.Storages.Remove(storage);
                databaseManager.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }
    }
}
