using Microsoft.EntityFrameworkCore;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _context;

    public ArticleRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Article article)
    {
        _context.Articles.Add(article);
        _context.SaveChanges();
    }

    public void Delete(Article article)
    {
        _context.Articles.Remove(article);
        _context.SaveChanges();
    }

    public List<Article> GetAll()
    {
        return _context.Articles.Include(a=>a.User).ToList();
    }

    public Article? GetArticleById(int id)
    {
      return _context.Articles.FirstOrDefault(a => a.Id == id);
    }

    public void Update(Article article)
    {
        _context.Articles.Update(article);
        _context.SaveChanges();
    }
}