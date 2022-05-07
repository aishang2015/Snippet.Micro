using Microsoft.AspNetCore.Mvc;
using Snippet.Micro.Common.Models;
using IOperationApi = Snippet.Micro.Operation.Api.IApiInfo;
using IRbacApi = Snippet.Micro.Rbac.Api.IApiInfo;

namespace Snippet.Micro.Operation.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        private readonly IOperationApi _operationApi;

        private readonly IRbacApi _rbacApi;

        public EnvironmentController(IOperationApi operationApi, IRbacApi rbacApi)
        {
            _operationApi = operationApi;
            _rbacApi = rbacApi;
        }

        /// <summary>
        /// 获取所有服务的接口列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult<string>), 200)]
        public async Task<CommonResult> GetApplicationApi()
        {
            var operationApis = await _operationApi.GetApiPaths();
            var rbacApis = await _rbacApi.GetApiPaths();
            var result = new
            {
                Operation = operationApis.Data,
                Rbac = rbacApis.Data,
            };

            return CommonResultExtension.SuccessResult(result);
        }


    }
}
