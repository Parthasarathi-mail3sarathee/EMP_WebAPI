using System.Threading.Tasks;
using WebApplication_Shared_Services.Service;

namespace WebApplication_Services.Service
{
    public sealed class ScopeService : IScopeService
    {
        

        private readonly IOperationTransient _transientOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationScoped _scopedOperation;


        public ScopeService(

                      IOperationTransient transientOperation,
                      IOperationScoped scopedOperation,
                      IOperationSingleton singletonOperation
                      )
        {

            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
        }
        
        public Task<string>  GetTrancientScopedSingleton()
        {

            string str = "{ ServiceMethod: { Transient: " + _transientOperation.OperationId + " "
                          + ", Scoped: " + _scopedOperation.OperationId + " "
                          + ", Singleton: " + _singletonOperation.OperationId + " } ";

            return Task.FromResult(str); 
        }
    }
}
