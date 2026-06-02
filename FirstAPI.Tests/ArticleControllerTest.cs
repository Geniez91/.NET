using System.Net;
using Xunit;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public class ArticleControllerTest : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;
    private readonly ApiFactory _factory;

    public ArticleControllerTest(ApiFactory factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
        
    }

    [Fact]
public async Task GetArticles_ShouldReturnOk()
{
    var response = await _client.GetAsync("/api/article?PageNumber=1&PageSize=10&search=Superman&sortBy=price");

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
}

    [Fact]
    public async Task CreateArticle_ShouldReturnCreated()
    {
    using var scope = _factory.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var user = new User
    {
        UserName = "testuser",
        Email = "test@test.com",
        Password = "password"
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    var articleInputDto = new ArticleInputDto(
        $"Superman-{Guid.NewGuid()}",
        "Description",
        19.99m,
        user.Id);

    var response = await _client.PostAsJsonAsync(
        "/api/article",
        articleInputDto);

///Check du status
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

///Check que l'article a été créé en base de données
   using var verificationScope = _factory.Services.CreateScope();

   var verificationDb =
    verificationScope.ServiceProvider
        .GetRequiredService<AppDbContext>();

   var createdArticle =
    await verificationDb.Articles
        .FirstOrDefaultAsync(a => a.Name == articleInputDto.Name);
    Assert.NotNull(createdArticle);
    }

    [Fact]
public async Task CreateArticle_ShouldReturnConflict_WhenArticleAlreadyExists()
{
    using var scope = _factory.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var user = new User
    {
        UserName = "test",
        Email = "test@test.com",
        Password = "password"
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    var articleInputDto = new ArticleInputDto(
        "Superman",
        "Description",
        19.99m,
        user.Id);

    await _client.PostAsJsonAsync(
        "/api/article",
        articleInputDto);

    var response = await _client.PostAsJsonAsync(
        "/api/article",
        articleInputDto);

    Assert.Equal(
        HttpStatusCode.Conflict,
        response.StatusCode);
}

[Fact]
public async Task UpdateArticle_ShouldReturnNotFound_WhenArticleDoesNotExist()
{
    var articleInputDto = new ArticleInputDto(
        "NonExistent",
        "Description",
        19,
        1);

    var response = await _client.PatchAsJsonAsync(
        "/api/article/9999",
        articleInputDto);

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}

[Fact]
public async Task UpdateArticle_ShouldReturnNoContent()
{
    using var scope = _factory.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var user = new User
    {
        UserName = "test",
        Email = "test@test.com",
        Password = "password"
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    var article = new Article
    {
        Name = "Batman",
        Description = "Old Description",
        Price = 10,
        UserId = user.Id
    };

    db.Articles.Add(article);
    await db.SaveChangesAsync();

    var updateDto = new ArticleInputDto(
        "Batman Updated",
        "New Description",
        20,
        user.Id);

    var response = await _client.PatchAsJsonAsync(
        $"/api/article/{article.Id}",
        updateDto);

//Check du status
    Assert.Equal(HttpStatusCode.NoContent,
        response.StatusCode);

        using var verificationScope = _factory.Services.CreateScope();

var verificationDb =
    verificationScope.ServiceProvider
        .GetRequiredService<AppDbContext>();

var updatedArticle =
    await verificationDb.Articles.FindAsync(article.Id);

//Check que l'article a été mis à jour en base de données
    Assert.Equal(updateDto.Name, updatedArticle.Name);
    Assert.Equal(updateDto.Description, updatedArticle.Description);
}

[Fact]
public async Task DeleteArticle_ShouldReturnNotFound_WhenArticleDoesNotExist()
{
    var response = await _client.DeleteAsync("/api/article/9999");

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}

[Fact]
public async Task DeleteArticle_ShouldReturnNoContent()
    {
           using var scope = _factory.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var user = new User
    {
        UserName = "test",
        Email = "test@test.com",
        Password = "password"
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    var article = new Article
    {
        Name = "Batman",
        Description = "Old Description",
        Price = 10,
        UserId = user.Id
    };

    db.Articles.Add(article);
    await db.SaveChangesAsync();

    var response = await _client.DeleteAsync($"/api/article/{article.Id}");

    ///Check du status
    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

    ///Check que l'article a été supprimé en base de données
    using var verificationScope = _factory.Services.CreateScope();

    var verificationDb =
    verificationScope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    var deletedArticle =
    await verificationDb.Articles.FindAsync(article.Id);
    Assert.Null(deletedArticle);
    }


    
    [Fact]
    public async Task GetArticleById_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync("/api/article/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetArticleById_ShouldReturnOk()
    {
         using var scope = _factory.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var user = new User
    {
        UserName = "test",
        Email = "test@test.com",
        Password = "password"
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    var article = new Article
    {
        Name = "Batman",
        Description = "Old Description",
        Price = 10,
        UserId = user.Id
    };

    db.Articles.Add(article);
    await db.SaveChangesAsync();

    var response = await _client.GetAsync($"/api/article/{article.Id}");
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}