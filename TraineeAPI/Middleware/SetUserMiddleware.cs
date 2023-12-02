using TraineeAPI.Data;

namespace TraineeAPI.Middleware;

public class SetUserMiddleware
{
    private readonly RequestDelegate _next;

    public SetUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, DataContext dbContext)
    {
        // var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        // var userId = jwtUtils.ValidateToken(token);
        // if (userId != null)
        // {
        //     // attach user to context on successful jwt validation
        //     context.Items["User"] = userService.GetById(userId.Value);
        // }
        if (context.User != null)
        {
            var idClaim = context.User.Claims.FirstOrDefault((claim) => claim.Type == "id");
            if (idClaim != null)
            {
                var user = await dbContext
                    .Users
                    .FirstOrDefaultAsync((user) => user.Id == int.Parse(idClaim.Value));
                context.Items.Add("user", user);
            }    
        }
        

        await _next(context);
    }
}