using Microsoft.EntityFrameworkCore;

public class ArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public List<ArticleOutputDto> GetAll()
    {
        return _articleRepository.GetAll().Select(ArticleMapper.toDto).ToList();
    }

    public void Add(ArticleInputDto articleInputDto)
    {
        if (articleInputDto == null)
        {
            throw new ArgumentNullException(nameof(articleInputDto));
        }
        var article = ArticleMapper.toEntity(articleInputDto);
        _articleRepository.Add(article);
    }

    public bool Update(int id, ArticleInputDto articleInputDto)
    {
        var existingArticle = _articleRepository.GetArticleById(id);
        if (existingArticle == null)
        {
            return false;
        }
        var article= ArticleMapper.toEntity(articleInputDto);
        existingArticle.Name = article.Name;
        existingArticle.Description = article.Description;
        existingArticle.Price = article.Price;
        _articleRepository.Update(existingArticle);
        return true;
    }

    public bool Delete(int id)
    {
        var existingArticle = _articleRepository.GetArticleById(id);
        if (existingArticle == null)
        {
            return false;
        }
        _articleRepository.Delete(existingArticle);
        return true;
    }

    public ArticleOutputDto? GetArticleById(int id)
    {
        var article = _articleRepository.GetArticleById(id);
        if(article==null)
        {
            return null;
        }
        return ArticleMapper.toDto(article);
    }
    
}