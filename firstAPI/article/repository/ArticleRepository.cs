using Microsoft.EntityFrameworkCore;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _context;

    public ArticleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Article article)
    {
        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Article>> GetAll()
    {
        return await _context.Articles.Include(a=>a.User).ToListAsync();
    }

    public async Task<Article?> GetArticleById(int id)
    {
      return await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Update(Article article)
    {
        _context.Articles.Update(article);
        await _context.SaveChangesAsync();
    }
}