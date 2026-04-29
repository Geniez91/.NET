public interface IArticleRepository
{
    List<Article> GetAll();
    void Add(Article article);
    void Update(Article article);
    void Delete(Article article);
    Article? GetArticleById(int id);
}