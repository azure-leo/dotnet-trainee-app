using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TraineeAPI.DTO;

namespace TraineeAPI.Services.ArticleService;

public interface IArticleService
{
    Task<IEnumerable<Article>> GetAllArticles();

    Task<IEnumerable<Article>> AddArticle(ArticleDTO articleRequest, User user);

    Task<Article> UpdateArticle(int id, [FromBody] ArticleDTO articleRequest, User user);
}