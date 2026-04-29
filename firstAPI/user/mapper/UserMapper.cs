public static class UserMapper
{
    public static User toEntity(UserInputDto userInputDto)
    {
        return new User
        {
            Email = userInputDto.Email,
            UserName = userInputDto.UserName,
            Password = userInputDto.Password,
        };

    }

    public static UserOutputDto toDto(User user)
    {
        return new UserOutputDto(
            user.Id,
            user.Email,
            user.UserName,
            user.Password,
            user.Articles.Select(a => ArticleMapper.toDto(a)).ToList()
        );
    }
}