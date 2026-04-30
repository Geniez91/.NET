public interface IUserRepository
{
    Task<List<User>>GetAll(int Page,int PageSize);
    Task Add(User user);
    Task Update(User user);
    Task Delete(User user);
    Task<User?> GetUserById(int id);
}