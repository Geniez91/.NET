public class User
{
    public int Id {get;set;}
    public String UserName {get; set;}

    public String Email {get; set;}
    public String Password {get; set;}

    public List<Article> Articles { get; set; } = new (); //propriété de navigation

}