using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetUsers();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserInputDto user)
    {
        if(user == null)
        {
            return BadRequest();
        }
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _userService.Add(user);
        return Created("", user);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserInputDto user)
    {
        if(user == null)
        {
            return BadRequest();
        }
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updated = await _userService.Update(id, user);
        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _userService.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetById(id);
        if(user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}