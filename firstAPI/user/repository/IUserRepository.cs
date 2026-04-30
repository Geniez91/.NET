public interface IUserRepository
{
    Task<List<User>>GetAll();
    Task Add(User user);
    Task Update(User user);
    Task Delete(User user);
    Task<User?> GetUserById(int id);
}