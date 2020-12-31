using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication_Services.Service;
using WebApplication_Shared_Services.Service;

namespace WebApplication_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllHeaders")]
    public class ScopeController: ControllerBase
    {
    
        private readonly IScopeService _scopeService;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationScoped _scopedOperation;

        public ScopeController(
                   IScopeService scopeService,
                   IOperationTransient transientOperation,
                   IOperationScoped scopedOperation,
                   IOperationSingleton singletonOperation)
        {
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
            _scopeService = scopeService;
        }

        [HttpGet]
        [Route("TrancientScopedSingleton")]
        public async Task<IActionResult> GetTrancientScopedSingleton()
        {


            string str = "{ ControllerMethod: { Transient: " + _transientOperation.OperationId + " "
                          + ", Scoped: " + _scopedOperation.OperationId + " "
                          + ", Singleton: " + _singletonOperation.OperationId + " } ";

            var str1 = await _scopeService.GetTrancientScopedSingleton();

            str1 = "{ "+ str1 + "," + str + " }";
            return Ok(str1);
        }
    }
}
