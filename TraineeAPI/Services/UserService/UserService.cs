namespace TraineeAPI.Services.UserService;

public class UserService : IUserService
{
    private static List<User> users = new List<User> {
        new User
        {
            Id = 1,
            Username = "Phony Stark",
            FirstName = "Elon",
            LastName = "Musk",
            Email = "emusk@gmail.com"
        },
        new User
        {
            Id = 2,
            Username = "CR7",
            FirstName = "Cristiano",
            LastName = "Ronaldo",
            Email = "cristiano@gmail.com"
        }
    };
    
    public List<User> GetAllUsers()
    {
        return users;
    }

    public User? GetUserById(int id)
    {
        var user = users.Find(x => x.Id == id);
        if (user is null)
        {
            return null;
        }

        return user;
    }

    public List<User> AddUser(User user)
    {
        users.Add(user);
        return users;
    }

    public List<User>? UpdateUser(int id, User request)
    {
        var user = users.Find(x => x.Id == id);
        if (user is null)
        {
            return null; 
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Username = request.Username;
        user.Email = request.Email;
            
        return users;
    }

    public List<User>? DeleteUser(int id)
    {
        var user = users.Find(x => x.Id == id);
        if (user is null)
        {
            return null;
        }

        users.Remove(user);
            
        return users;
    }
}