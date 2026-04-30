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

    public async Task<(List<User>, int TotalCount, int TotalPages)> GetAll(int page, int pageSize)
    {
        var totalCount = await _context.Users.CountAsync();
        var totalPage = (int) Math.Ceiling(totalCount/(double)pageSize);
        var data = await _context.Users.Skip((page - 1 ) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
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
}