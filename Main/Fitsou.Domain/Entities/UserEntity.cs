using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public  class UserEntity: IdentityUser
{
    public string Salt { get; init; }    

    public UserEntity(string salt)
    {
        Salt = salt;
    }
}