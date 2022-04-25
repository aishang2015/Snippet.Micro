using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Snippet.Micro.EntityFrameworkCore
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加dbcontext
        /// </summary>
        /// <exception cref="InvalidDatabaseOptionException"></exception>
        public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services,
                IConfiguration configuration, string optionKey = "DatabaseOption") where TDbContext : DbContext
        {
            var databaseOption = configuration.GetSection(optionKey).Get<DatabaseOption>();
            if (databaseOption != null)
            {
                return services.AddDatabase<TDbContext>(databaseOption);
            }
            throw new InvalidDatabaseOptionException();
        }

        /// <summary>
        /// 添加dbcontext
        /// </summary>
        /// <exception cref="InvalidDatabaseOptionException"></exception>
        public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services,
                DatabaseOption databaseOption) where TDbContext : DbContext
        {
            if (databaseOption != null)
            {
                services.AddDbContext<TDbContext>(option =>
                {
                    option = databaseOption.Type switch
                    {
                        "MySQL" => option.UseMySql(databaseOption.Connection,
                            string.IsNullOrEmpty(databaseOption.Version) ?
                                new MySqlServerVersion(new Version(databaseOption.Version)) : MySqlServerVersion.LatestSupportedServerVersion,
                        builder =>
                        {
                            builder.UseRelationalNulls();
                        }),
                        "SQLite" => option.UseSqlite(databaseOption.Connection, builder =>
                        {
                            builder.UseRelationalNulls();
                        }),
                        "SQLServer" => option.UseSqlServer(databaseOption.Connection, builder =>
                        {
                            builder.UseRelationalNulls();
                        }),
                        "PostgreSQL" => option.UseNpgsql(databaseOption.Connection, builder =>
                        {
                            builder.UseRelationalNulls();
                        }),
                        "Oracle" => option.UseOracle(databaseOption.Connection, builder =>
                        {
                            builder.UseRelationalNulls();
                            builder.UseOracleSQLCompatibility(databaseOption.Version);
                        }),
                        "InMemory" => option.UseInMemoryDatabase(databaseOption.Connection),
                        _ => option
                    };
                });

                return services;
            }
            throw new InvalidDatabaseOptionException();
        }

        /// <summary>
        /// 添加identity dbcontext
        /// </summary>
        /// <exception cref="InvalidDatabaseOptionException"></exception>
        public static IServiceCollection AddIdentityDatabase<TDbContext, TUser, TRole, TKey>(this IServiceCollection services,
                IConfiguration configuration, string optionKey = "DatabaseOption", Action<IdentityOptions> setupAction = null)
            where TDbContext : IdentityDbContext<TUser, TRole, TKey>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
        {
            var databaseOption = configuration.GetSection(optionKey).Get<DatabaseOption>();
            if (databaseOption != null)
            {
                return services.AddIdentityDatabase<TDbContext, TUser, TRole, TKey>(databaseOption);
            }
            throw new InvalidDatabaseOptionException();
        }

        /// <summary>
        /// 添加identity dbcontext
        /// </summary>
        /// <exception cref="InvalidDatabaseOptionException"></exception>
        public static IServiceCollection AddIdentityDatabase<TDbContext, TUser, TRole, TKey>(this IServiceCollection services,
                DatabaseOption databaseOption, Action<IdentityOptions> setupAction = null)
            where TDbContext : IdentityDbContext<TUser, TRole, TKey>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
        {
            if (databaseOption != null)
            {
                if (setupAction == null)
                {
                    setupAction = (options) =>
                    {
                        // 密码强度设置
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequiredLength = 4;
                    };
                }

                services
                    .AddDatabase<TDbContext>(databaseOption)
                    .AddIdentity<TUser, TRole>(setupAction)
                    .AddEntityFrameworkStores<TDbContext>()
                    .AddDefaultTokenProviders();
                return services;
            }
            throw new InvalidDatabaseOptionException();
        }
    }
}
