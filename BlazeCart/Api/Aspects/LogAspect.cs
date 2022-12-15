using Castle.DynamicProxy;

namespace Api.Aspects
{
    public class LogAspect : IInterceptor
    {
        private readonly ILogger<LogAspect> _logger;
        public LogAspect(ILogger<LogAspect> logger)
        {
            _logger = logger;
        }
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
              
                _logger.LogInformation($"Method {invocation.Method.Name} " +
                    $"called with these parameters: {invocation.Arguments}" +
                    $"returned this response: {invocation.ReturnValue}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error happened in method: {invocation.Method}. Error: {ex}");
                throw;
            }
        }
    }
}
