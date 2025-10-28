using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // importante para ToListAsync / FindAsync
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;



namespace API.Controllers
{
[Authorize]

public class MembersController(AppDbContext context) : BaseApiController
{
    [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers() // se puede
        {
            var members = await context.Users.ToListAsync();  //hace select de usuarios 
            return members;
        }
        [AllowAnonymous]
        [HttpGet("{id}")] // se pone un parametro en la ruta api/members/bob-id
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member =  await context.Users.FindAsync(id); // busca por primary key
            if (member == null) return NotFound();
            return member;
        }
    }
}
