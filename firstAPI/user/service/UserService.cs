public class UserService
{
     private List<User> _users = new()
    {
        new User { Id = 1, UserName = "User 1", email = "user1@example.com", password = "password1" },
        new User { Id = 2, UserName = "User 2", email = "user2@example.com", password = "password2" },
        new User { Id = 3, UserName = "User 3", email = "user3@example.com", password = "password3" }
    };
}