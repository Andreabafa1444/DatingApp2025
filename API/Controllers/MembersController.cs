using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;


namespace API.Controllers
{
    [Route("api/[controller]")] //local host:5001:api/members
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IReadOnlyList<AppUser>> GetMembers()
        {
            var members = context.Users.ToList();
            return members;
        }
        [HttpGet("{id}")]//localhost:5001:api/members/bob-id
        public ActionResult<AppUser> GetMember(string id)
        {
            var member = context.Users.Find(id);
            if (member == null) return NotFound();
            return member;
        }
    }
}