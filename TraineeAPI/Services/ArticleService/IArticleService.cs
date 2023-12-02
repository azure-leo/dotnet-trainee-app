using System.Security.Claims;
using TraineeAPI.DTO;

namespace TraineeAPI.Services.ArticleService;

public interface IArticleService
{
    Task<IEnumerable<Article>> GetAllArticles();

    Task<IEnumerable<Article>> AddArticle(ArticleDTO articleRequest, Claim idClaim);
}