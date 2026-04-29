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
    public IActionResult Get()
    {
        return Ok(_articleService.GetAll());
    }

    [HttpPost]
    public IActionResult Create([FromBody] ArticleInputDto articleInputDto)
    {
        if(articleInputDto==null)
        {
            return BadRequest();
        }
        _articleService.Add(articleInputDto);
        return Created("", articleInputDto);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(int id,[FromBody] ArticleInputDto articleInputDto)
    {
        if(articleInputDto==null)
        {
            return BadRequest();
        }
        var updated = _articleService.Update(id, articleInputDto);

        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _articleService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var article = _articleService.GetArticleById(id);
        if(article==null)
        {
            return NotFound();
        }
        return Ok(article);
    }
}