using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public List<UserOutputDto> GetUsers()
    {
        return _context.Users.Select(u => UserMapper.toDto(u)).ToList();
    }

    public void Add(UserInputDto userDto)
    {
        var user = UserMapper.toEntity(userDto);
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public bool Update(int id, UserInputDto userDto)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return false;
        }
        existingUser.UserName = userDto.UserName;
        existingUser.Email = userDto.Email;
        existingUser.Password = userDto.Password;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return false;
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }

    public UserOutputDto? GetById(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if(user == null)
        {
            return null;
        }
        return UserMapper.toDto(user);
    }
}