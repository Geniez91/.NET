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

    public async Task<(List<Article>,int TotalCount,int TotalPages)> GetAll(int page, int pageSize,string? search,string? sortBy)
    {
        var query=_context.Articles.AsNoTracking();

        //Ici on ajouter un filtre de recherche par nom d'article
        if(!string.IsNullOrWhiteSpace(search))
        {
            query=query.Where(a=>a.Name.Contains(search));
        }

        query = sortBy?.ToLower() switch
        {
            "name" => query.OrderBy(a => a.Name),
            "price" => query.OrderBy(a => (double) a.Price),
            _ => query.OrderBy(a => a.Id) // Tri par défaut
        };


        var totalCount=await query.CountAsync();
        var totalPages=(int)Math.Ceiling(totalCount/(double)pageSize);
        var data = await query.Include(a=>a.User).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (data, totalCount,totalPages);
    }

    public async Task<Article?> GetArticleById(int id)
    {
      return await _context.Articles.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Update(Article article)
    {
        _context.Articles.Update(article);
        await _context.SaveChangesAsync();
    }
}