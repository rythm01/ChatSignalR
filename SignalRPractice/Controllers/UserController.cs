using Microsoft.AspNetCore.Mvc;
using SignalRPractice.DTO;
using SignalRPractice.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
namespace SignalRPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContacts(string id)
        {
            var data = await Task.FromResult(_userRepo.GetContacts(id).Select(a => new Contacts(a.Id, a.UserName)));

            return Ok(data);
        }
    }
}
