public interface IUserRepository
{
    Task<(List<User>, int TotalCount, int TotalPages)> GetAll(int page, int pageSize);
    Task Add(User user);
    Task Update(User user);
    Task Delete(User user);
    Task<User?> GetUserById(int id);
}