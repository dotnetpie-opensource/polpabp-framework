using System.Threading;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Impersonation
{
    public class AsyncLocalCurrentImpersonationAccessor : ICurrentImpersonationAccessor, ISingletonDependency
    {
        public ImpersonationInfo Current
        {
            get => _currentScope.Value;
            set => _currentScope.Value = value;
        }

        private readonly AsyncLocal<ImpersonationInfo> _currentScope;

        public AsyncLocalCurrentImpersonationAccessor()
        {
            _currentScope = new AsyncLocal<ImpersonationInfo>();
        }
    }
}
