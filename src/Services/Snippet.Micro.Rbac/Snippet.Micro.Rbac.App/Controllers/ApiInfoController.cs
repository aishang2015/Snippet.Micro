using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Snippet.Micro.Common.Models;

namespace Snippet.Micro.RBAC.Controllers.RBAC
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiInfoController : ControllerBase
    {
        /// <summary>
        /// 获取程序所有API信息
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult<string>), 200)]
        public CommonResult GetApiPaths([FromServices] IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
        {
            var result = apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items
                .SelectMany(i => i.Items).Select(i => i.RelativePath);
            return CommonResultExtension.SuccessResult(result);
        }
    }
}