using Fitsou.Application.Contracts;
using Domain.Entities;
using Persistence.DbPersistance;

namespace Persistence.UsersPersistence;

public class UsersRepository : IUsersRepository
{
    private readonly CoreContext _context;

    public UsersRepository(CoreContext context)
    {
        _context = context;
    }

    public bool Add(UserEntity user)
    {
        _context.Users.Add(user);
        return SaveChanges();
    }

    public UserEntity? GetByUsername(string userName)
    {
        return _context.Users.FirstOrDefault(user => user.UserName == userName);
    }

    public UserEntity? GetById(string id)
    {
        return _context.Users.FirstOrDefault(user => user.Id == id);
    }


    private bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}