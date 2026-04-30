public interface IArticleRepository
{
    Task<(List<Article>, int TotalCount, int TotalPages)> GetAll(int page, int pageSize);
    Task Add(Article article);
    Task Update(Article article);
    Task Delete(Article article);
    Task<Article?> GetArticleById(int id);
}