using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace API.Controllers;
[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetUsersAsync();
        return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto?>> GetUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        var userDto = _mapper.Map<MemberDto?>(user); // Handle nullable mapping
        return userDto != null ? Ok(userDto) : NotFound();
    }

    [HttpGet("username/{username}")]
    public async Task<ActionResult<MemberDto?>> GetUserByUserName(string username)
    {
        var user = await _userRepository.GetUserByUserNameAsync(username);
        var userDto = _mapper.Map<MemberDto?>(user); // Handle nullable mapping
        return userDto != null ? Ok(userDto) : NotFound();
    }
}

