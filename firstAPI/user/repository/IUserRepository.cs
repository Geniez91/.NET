public interface IUserRepository
{
    List<User>GetAll();
    void Add(User user);
    void Update(User user);
    void Delete(User user);
    User? GetUserById(int id);
}