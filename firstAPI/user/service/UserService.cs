using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<UserOutputDto> GetUsers()
    {
        return _userRepository.GetAll().Select(UserMapper.toDto).ToList();
    }

    public void Add(UserInputDto userDto)
    {
        var user = UserMapper.toEntity(userDto);
        _userRepository.Add(user);
    }

    public bool Update(int id, UserInputDto userDto)
    {
        var existingUser = _userRepository.GetUserById(id);
        if (existingUser == null)
        {
            return false;
        }
        existingUser.UserName = userDto.UserName;
        existingUser.Email = userDto.Email;
        existingUser.Password = userDto.Password;
        _userRepository.Update(existingUser);
        return true;
    }

    public bool Delete(int id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return false;
        }
        _userRepository.Delete(user);
        return true;
    }

    public UserOutputDto? GetById(int id)
    {
        var user = _userRepository.GetUserById(id);
        if(user == null)
        {
            return null;
        }
        return UserMapper.toDto(user);
    }
}