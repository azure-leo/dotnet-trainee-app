using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        var articles = await _context.Articles.Include((article) => article.Author).ToListAsync();
        return articles;
    }

    public async Task<Article?> GetArticleById(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article is null)
        {
            return null;
        }

        return article;
    }

    public async Task<IEnumerable<Article>> AddArticle(ArticleDTO articleRequest, User user)
    {
        var article = new Article()
        {
            Title = articleRequest.Title,
            Content = articleRequest.Content,
            AuthorId = user.Id,
            Author = user
        };
        
        await _context.Articles.AddAsync(article);
        await _context.SaveChangesAsync();
        return await _context.Articles.ToListAsync();
    }

    public async Task<Article> UpdateArticle(int id, [FromBody] ArticleDTO articleRequest, User user)
    {
        var article = await _context.Articles.FirstOrDefaultAsync((article) =>
            article.Id == id
        );
        if (article == null)
        {
            return null;
        }

        article.Title = articleRequest.Title;
        article.Content = articleRequest.Content;
        article.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();

        return article;
    }
    
}