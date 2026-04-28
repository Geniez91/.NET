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
    public IActionResult Create([FromBody] Article article)
    {
        if(article==null)
        {
            return BadRequest();
        }

        _articleService.Add(article);
        return Created("", article);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(int id,[FromBody] Article article)
    {
        if(article==null|| article.Id != id)
        {
            return BadRequest();
        }
        var updated = _articleService.Update(id, article);

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