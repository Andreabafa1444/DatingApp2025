using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // importante para ToListAsync / FindAsync
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;



namespace API.Controllers
{
//[Authorize]
//[AllowAnonymous]  // para pruebas pequeñas 

public class MembersController(IMembersRepository membersRepository) : BaseApiController
    {


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
                    return Ok(await membersRepository.GetMembersAsync());


        }
  [HttpGet("{id}")] // se pone un parametro en la ruta api/members/bob-id
    public async Task<ActionResult<Member>> GetMember(string id)
    {
        var member = await membersRepository.GetMemberAsync(id) ; //hace select de usuario

        if (member == null) return NotFound();

        return member;
    }
        
    [HttpGet("{id}/photos")]
    public async Task<ActionResult<IReadOnlyList<Photo>>> GetPhotos(string id)
    {
        return Ok(await membersRepository.GetPhotosAsync(id));
    }
    }
}
