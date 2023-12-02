using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeAPI.DTO;
using TraineeAPI.Services.ArticleService;

namespace TraineeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : Controller
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> Get()
    {
        var result = await _articleService.GetAllArticles();
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Article>> AddArticle([FromBody] ArticleDTO articleRequest)
    {
        // var idClaim = HttpContext
        //     .User
        //     .Claims
        //     .FirstOrDefault((claim) => claim.Type == "id");
        HttpContext.Items.TryGetValue("user", out var userObj);
        User user = (User) userObj;
        var result = await _articleService.AddArticle(articleRequest, user);
        
        return Ok(result);
    }
}