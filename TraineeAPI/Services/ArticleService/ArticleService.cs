using TraineeAPI.Data;

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
    
}