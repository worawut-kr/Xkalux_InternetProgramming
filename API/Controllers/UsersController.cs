namespace API.Controllers;

using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("api/[controller]")]
[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return Ok(await _userRepository.GetUsersAsync());
    }

    [HttpGet("{id}")] //endpoint: /api/users/25  
    public async Task<ActionResult<AppUser?>> GetUser(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }
    [HttpGet("username/{username}")]
    public async Task<ActionResult<AppUser?>> GetUserByUserName(string username)
    {
        return await _userRepository.GetUserByUserNameAsync(username);
    }
}
