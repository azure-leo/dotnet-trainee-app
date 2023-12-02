using TraineeAPI.Data;

namespace TraineeAPI.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;
    public UserService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<User?> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return null;
        }

        return user;
    }

    public async Task<List<User>> AddUser(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return await _context.Users.ToListAsync();
    }

    public async Task<List<User>?> UpdateUser(int id, User request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return null; 
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Username = request.Username;
        user.Email = request.Email;

        await _context.SaveChangesAsync();
            
        return await _context.Users.ToListAsync();
    }

    public async Task<List<User>?> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return null;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
            
        return await _context.Users.ToListAsync();
    }
}