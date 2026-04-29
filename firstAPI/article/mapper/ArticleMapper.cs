public static class ArticleMapper
{
    public static Article toEntity(ArticleInputDto articleInputDto)
    {
        return new Article
        {
            Name = articleInputDto.Name,
            Description = articleInputDto.Description,
            Price = articleInputDto.Price,
            UserId = articleInputDto.UserId
        };
    }

    public static ArticleOutputDto toDto(Article article)
    {
        return new ArticleOutputDto(
            article.Id,
            article.Name,
            article.Description,
            article.Price,
            article.UserId
        );
    }


}