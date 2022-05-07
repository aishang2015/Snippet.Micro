using Refit;
using Snippet.Micro.Common.Models;

namespace Snippet.Micro.Operation.Api
{
    public interface IApiInfo
    {
        [Post("/api/apiInfo/GetApiPaths")]
        Task<CommonResult<List<string>>> GetApiPaths();
    }
}
