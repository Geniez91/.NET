using Microsoft.EntityFrameworkCore;

public class ArticleService
{
    private readonly AppDbContext _context;

    public ArticleService(AppDbContext context)
    {
        _context = context;
    }

    public List<ArticleOutputDto> GetAll()
    {
        return _context.Articles.Include(a=>a.User).Select(a => ArticleMapper.toDto(a)).ToList();
    }

    public void Add(ArticleInputDto articleInputDto)
    {
        if (articleInputDto == null)
        {
            throw new ArgumentNullException(nameof(articleInputDto));
        }
        var article = ArticleMapper.toEntity(articleInputDto);
        _context.Articles.Add(article);
        _context.SaveChanges();
    }

    public bool Update(int id, ArticleInputDto articleInputDto)
    {
        var existingArticle = _context.Articles.FirstOrDefault(a=>a.Id == id);
        if (existingArticle == null)
        {
            return false;
        }
        var article= ArticleMapper.toEntity(articleInputDto);
        existingArticle.Name = article.Name;
        existingArticle.Description = article.Description;
        existingArticle.Price = article.Price;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var existingArticle = _context.Articles.FirstOrDefault(a => a.Id == id);
        if (existingArticle == null)
        {
            return false;
        }
        _context.Articles.Remove(existingArticle);
        _context.SaveChanges();
        return true;
    }

    public ArticleOutputDto? GetArticleById(int id)
    {
        var article=_context.Articles.FirstOrDefault(a => a.Id == id);
        if(article==null)
        {
            return null;
        }
        return ArticleMapper.toDto(article);
    }
    
}