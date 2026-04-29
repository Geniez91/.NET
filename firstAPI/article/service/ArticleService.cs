public class ArticleService
{
    private readonly AppDbContext _context;

    public ArticleService(AppDbContext context)
    {
        _context = context;
    }

    // private List<Article> _articles = new()
    // {
    //     new Article { Id = 1, Name = "Article 1", Description = "Description of Article 1", Price = 10.99m },
    //     new Article { Id = 2, Name = "Article 2", Description = "Description of Article 2", Price = 20.99m },
    //     new Article { Id = 3, Name = "Article 3", Description = "Description of Article 3", Price = 30.99m }
    // };

    public List<Article> GetAll()
    {
        return _context.Articles.ToList();
        // return _articles;
    }

    public void Add(Article article)
    {
        // _articles.Add(article);
        _context.Articles.Add(article);
        _context.SaveChanges();
    }

    public bool Update(int id, Article article)
    {
        // var existingArticle = _articles.FirstOrDefault(a=>a.Id == id);
        // if (existingArticle == null)
        // {
        //     return false;
        // }
        // existingArticle.Name = article.Name;
        // existingArticle.Description = article.Description;
        // existingArticle.Price = article.Price;
        // return true;

        var existingArticle = _context.Articles.FirstOrDefault(a=>a.Id == id);
        if (existingArticle == null)
        {
            return false;
        }
        existingArticle.Name = article.Name;
        existingArticle.Description = article.Description;
        existingArticle.Price = article.Price;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        // var article = _articles.FirstOrDefault(a => a.Id == id);
        // if (article == null)        {
        //     return false;
        // }
        // _articles.Remove(article);
        // return true;

        var existingArticle = _context.Articles.FirstOrDefault(a => a.Id == id);
        if (existingArticle == null)
        {
            return false;
        }
        _context.Articles.Remove(existingArticle);
        _context.SaveChanges();
        return true;
    }

    public Article? GetArticleById(int id)
    {
        // var article = _articles.FirstOrDefault(a => a.Id == id);
        // if(article==null)
        // {
        //     return null;
        // }
        // return article;


        var article=_context.Articles.FirstOrDefault(a => a.Id == id);
        if(article==null)
        {
            return null;
        }
        return article;
    }
    
}