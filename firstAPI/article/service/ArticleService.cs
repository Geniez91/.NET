using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<PageResult<ArticleOutputDto>> GetAll(int page, int pageSize,string? search,string? sortBy)
    {
        var (data, totalCount, totalPages) = await _articleRepository.GetAll(page,pageSize,search,sortBy);
        var listArticles= data.Select(ArticleMapper.toDto).ToList();
        return new PageResult<ArticleOutputDto>(listArticles, totalCount, page, pageSize, totalPages);
    }

    public async Task Add(ArticleInputDto articleInputDto)
    {
        if (articleInputDto == null)
        {
            throw new ArgumentNullException(nameof(articleInputDto));
        }
        var articleAltreadyExists = await _articleRepository.GetArticleByName(articleInputDto.Name);
        if(articleAltreadyExists != null)
        {
            throw new ConflictException($"Article already exists.");
        }
        var article = ArticleMapper.toEntity(articleInputDto);
        await _articleRepository.Add(article);
    }

    public async Task<bool> Update(int id, ArticleInputDto articleInputDto)
    {
        var existingArticle = await _articleRepository.GetArticleById(id);
        if (existingArticle == null)
        {
            throw new NotFoundException("Article not found.");
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
            throw new NotFoundException("Article not found.");
        }
        await _articleRepository.Delete(existingArticle);
        return true;
    }

    public async Task<ArticleOutputDto?> GetArticleById(int id)
    {
        var article = await _articleRepository.GetArticleById(id);
        if(article==null)
        {
            throw new NotFoundException("Article not found.");
        }
        return ArticleMapper.toDto(article);
    }
    
}