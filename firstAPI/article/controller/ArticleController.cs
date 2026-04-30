using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly ArticleService _articleService;

    public ArticleController(ArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationDto paginationDto)
    {
        var articles = await _articleService.GetAll(paginationDto.PageNumber, paginationDto.PageSize,paginationDto.Search,paginationDto.SortBy);
        return Ok(articles);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ArticleInputDto articleInputDto)
    {
        if(articleInputDto==null)
        {
            return BadRequest();
        }

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _articleService.Add(articleInputDto);
        return Created("", articleInputDto);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody] ArticleInputDto articleInputDto)
    {
        if(articleInputDto==null)
        {
            return BadRequest();
        }

        
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated = await _articleService.Update(id, articleInputDto);

        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _articleService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var article = await _articleService.GetArticleById(id);
        if(article==null)
        {
            return NotFound();
        }
        return Ok(article);
    }
}