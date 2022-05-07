using Refit;
using Snippet.Micro.Common.Models;

namespace Snippet.Micro.Rbac.Api
{
    public interface IApiInfo
    {
        [Post("/api/apiInfo/GetApiPaths")]
        Task<CommonResult<List<string>>> GetApiPaths();
    }
}
