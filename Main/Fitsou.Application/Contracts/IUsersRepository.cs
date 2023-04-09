using Domain.Entities;

namespace Fitsou.Application.Contracts
{
    public interface IUsersRepository
    {
        bool Add(UserEntity user);
        UserEntity? GetById(string id);
        UserEntity? GetByUsername(string userName);
    }
}