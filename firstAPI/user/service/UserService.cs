using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PageResult<UserOutputDto>> GetUsers(int page,int pageSize)
    {
        var (data,totalCount,totalPages) = await _userRepository.GetAll(page, pageSize);
        var listUsers = data.Select(UserMapper.toDto).ToList();
        return new PageResult<UserOutputDto>(listUsers, totalCount, page, pageSize, totalPages);
    }

    public async Task Add(UserInputDto userDto)
    {
        var user = UserMapper.toEntity(userDto);
        await _userRepository.Add(user);
    }

    public async Task<bool> Update(int id, UserInputDto userDto)
    {
        var existingUser = await _userRepository.GetUserById(id);
        if (existingUser == null)
        {
            return false;
        }
        existingUser.UserName = userDto.UserName;
        existingUser.Email = userDto.Email;
        existingUser.Password = userDto.Password;
        await _userRepository.Update(existingUser);
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            return false;
        }
        await _userRepository.Delete(user);
        return true;
    }

    public async Task<UserOutputDto?> GetById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if(user == null)
        {
            return null;
        }
        return UserMapper.toDto(user);
    }
}