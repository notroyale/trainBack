using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Fitsou.Application.Contracts;
using Persistence.UsersPersistence;
using Persistence.DbPersistance;

namespace Persistence;

public static class DependencyInjection
{
    

    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUsersRepository, UsersRepository>();  
        string connection = configuration.GetConnectionString("SqliteConnection");

        SqliteConnection _sqliteConnection = new(connection);

        if (_sqliteConnection.State != ConnectionState.Open)
        {
            _sqliteConnection.Open();
        }

        services.AddDbContext<CoreContext>(options => options.UseSqlite(_sqliteConnection));

        return services;
    }

}