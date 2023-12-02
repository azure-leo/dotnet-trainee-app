namespace TraineeAPI.Services.ArticleService;

public interface IArticleService
{
    Task<IEnumerable<Article>> GetAllArticles();
}