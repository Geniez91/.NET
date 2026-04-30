using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<List<ArticleOutputDto>> GetAll()
    {
        var articles = await _articleRepository.GetAll();
        return articles.Select(ArticleMapper.toDto).ToList();
    }

    public async Task Add(ArticleInputDto articleInputDto)
    {
        if (articleInputDto == null)
        {
            throw new ArgumentNullException(nameof(articleInputDto));
        }
        var article = ArticleMapper.toEntity(articleInputDto);
        await _articleRepository.Add(article);
    }

    public async Task<bool> Update(int id, ArticleInputDto articleInputDto)
    {
        var existingArticle = await _articleRepository.GetArticleById(id);
        if (existingArticle == null)
        {
            return false;
        }
        var article= ArticleMapper.toEntity(articleInputDto);
        existingArticle.Name = article.Name;
        existingArticle.Description = article.Description;
        existingArticle.Price = article.Price;
        await _articleRepository.Update(existingArticle);
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var existingArticle = await _articleRepository.GetArticleById(id);
        if (existingArticle == null)
        {
            return false;
        }
        await _articleRepository.Delete(existingArticle);
        return true;
    }

    public async Task<ArticleOutputDto?> GetArticleById(int id)
    {
        var article = await _articleRepository.GetArticleById(id);
        if(article==null)
        {
            return null;
        }
        return ArticleMapper.toDto(article);
    }
    
}