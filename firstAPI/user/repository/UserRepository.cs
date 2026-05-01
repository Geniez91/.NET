using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<(List<User>, int TotalCount, int TotalPages)> GetAll(int page, int pageSize,string? search,string? sortBy)
    {
        var usersQuery = _context.Users.AsNoTracking();

        //Ici on ajouter un filtre de recherche par nom d'utilisateur
        if (!string.IsNullOrWhiteSpace(search))
        {
            usersQuery = usersQuery.Where(u=>u.UserName.Contains(search));
        }

        usersQuery = sortBy?.ToLower() switch
        {
            "username" => usersQuery.OrderBy(u => u.UserName),
            "username_desc" => usersQuery.OrderByDescending(u => u.UserName),

            "email" => usersQuery.OrderBy(u => u.Email),
            "email_desc"=> usersQuery.OrderByDescending(u => u.Email),
            _ => usersQuery.OrderBy(u => u.Id) // Tri par défaut
        };

        var totalCount = await usersQuery.CountAsync();
        var totalPage = (int) Math.Ceiling(totalCount/(double)pageSize);
        var data = await usersQuery.Skip((page - 1 ) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        return (data, totalCount, totalPage);
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }
}