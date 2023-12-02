using System.Net;
using System.Security.Claims;
using TraineeAPI.Data;
using TraineeAPI.DTO;

namespace TraineeAPI.Services.ArticleService;

public class ArticleService : IArticleService
{
    private readonly DataContext _context;
    
    public ArticleService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetAllArticles()
    {
        var articles = await _context.Articles.ToListAsync();
        return articles;
    }

    public async Task<IEnumerable<Article>> AddArticle(ArticleDTO articleRequest, Claim idClaim)
    {
        var article = new Article()
        {
            Title = articleRequest.Title,
            Content = articleRequest.Content,
            AuthorId = int.Parse(idClaim.Value),
        };
        
        await _context.Articles.AddAsync(article);
        await _context.SaveChangesAsync();
        return await _context.Articles.ToListAsync();
    }
    
}