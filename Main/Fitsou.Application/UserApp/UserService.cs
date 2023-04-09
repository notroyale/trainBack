using Domain.Entities;
using Fitsou.Application.Contracts;

namespace Fitsou.Application.UserApp;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public bool Add(UserEntity user)
    {
        return _usersRepository.Add(user);
    }

    public UserEntity? GetByUsername(string userName)
    {
        return _usersRepository.GetByUsername(userName);
    }
    public UserEntity? GetById(string id)
    {
        return _usersRepository.GetById(id);
    }
}