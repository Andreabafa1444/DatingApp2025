using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // 👈 importante para ToListAsync / FindAsync
using API.Data;
using API.Entities;

namespace API.Controllers
{

    public class MembersController : BaseApiController
    {
        private readonly AppDbContext _context;

        public MembersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await _context.Users.ToListAsync();
            return members;
        }

        [HttpGet("{id}")] // localhost:5001/api/members/Bob-id
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (member == null) return NotFound();
            return member;
        }
    }
}
