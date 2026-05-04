using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

public class ArticleServiceTest
{
    private readonly Mock<IArticleRepository> _repoMock;
    private readonly Mock<ILogger<ArticleService>> _loggerMock;
    private readonly ArticleService _service;

    public ArticleServiceTest()
    {
        _repoMock = new Mock<IArticleRepository>();
        _loggerMock = new Mock<ILogger<ArticleService>>();
        _service = new ArticleService(_repoMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Add_ShouldThrowConflictException_WhenArticleExists()
    {
        // Arrange
        _repoMock.Setup(r => r.GetArticleByName(It.IsAny<string>()))
                 .ReturnsAsync(new Article());

        var dto = new ArticleInputDto("Batman", "desc", 10, 1);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => _service.Add(dto));
    }

    [Fact]
    public async Task Add_ShouldAddArticle()
    {
     _repoMock.Setup(r => r.GetArticleByName(It.IsAny<string>()))
        .ReturnsAsync(null);
        var dto = new ArticleInputDto("Batman", "desc", 10, 1);

        // Act
        await _service.Add(dto);

        // Assert
        _repoMock.Verify(r => r.AddArticle(It.Is<Article>(a => a.Name == dto.Name)), Times.Once);
    }
}