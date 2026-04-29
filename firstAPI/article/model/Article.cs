public class Article
{
    public int Id {get; set; }
    public string Name {get; set; }
    public string Description {get; set; }
    public decimal Price {get; set; }

    public int UserId { get; set; } ///clé étrangère
    public User? User { get; set; } // propriété de navigation
}