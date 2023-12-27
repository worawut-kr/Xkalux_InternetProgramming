using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
    {
        var users = await _userRepository.GetUsersAsync();
        return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
    }

    [HttpGet("{id}")] //endpoint: /api/users/25  
    public async Task<ActionResult<MemberDto?>> GetMember(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return Ok(_mapper.Map<MemberDto>(user));
    }

    [HttpGet("username/{username}")]
    public async Task<ActionResult<MemberDto?>> GetMemberByUsername(string username)
    {
        var user = await _userRepository.GetUserByUserNameAsync(username);
        return Ok(_mapper.Map<MemberDto>(user));
    }
}
