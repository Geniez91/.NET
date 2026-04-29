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
    public IActionResult GetAllUsers()
    {
     return Ok(_userService.GetUsers());
    }

    [HttpPost]
    public IActionResult AddUser([FromBody] UserInputDto user)
    {
        if(user == null)
        {
            return BadRequest();
        }
        _userService.Add(user);
        return Created("", user);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserInputDto user)
    {
        if(user == null)
        {
            return BadRequest();
        }
        var updated = _userService.Update(id, user);
        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var deleted = _userService.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetById(id);
        if(user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}