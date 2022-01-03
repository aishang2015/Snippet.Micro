using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Snippet.Micro.RBAC.Data;
using Snippet.Micro.Redis;

namespace SnippetAdmin.Data.Cache
{
    public static class RedisCacheInitializer
    {
        public static readonly Action<RedisClient, SnippetAdminDbContext> InitialCache =
            (redisClient, dbcontext) =>
            {
                var redisDataBase = redisClient.GetDatabase();

                dbcontext.Users.Where(u => u.IsActive).ToList().ForEach(u =>
                {
                    redisDataBase.SetAdd("active_users", u.UserName);
                });

            };
    }
}
