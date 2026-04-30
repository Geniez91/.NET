public interface IArticleRepository
{
    Task<List<Article>> GetAll();
    Task Add(Article article);
    Task Update(Article article);
    Task Delete(Article article);
    Task<Article?> GetArticleById(int id);
}