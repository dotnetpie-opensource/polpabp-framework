using System;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Impersonation
{
    public class CurrentImpersonation : ICurrentImpersonation, ITransientDependency
    {
        public virtual bool IsAvailable => UserId.HasValue;

        public virtual Guid? UserId => _currentAccessor.Current?.UserId;

        public virtual Guid? TenantId => _currentAccessor.Current?.TenantId;

        private readonly ICurrentImpersonationAccessor _currentAccessor;

        public CurrentImpersonation(ICurrentImpersonationAccessor currentAccessor)
        {
            _currentAccessor = currentAccessor;
        }

        public IDisposable Change(Guid? userId = null, Guid? tenantId = null)
        {
            return SetCurrent(userId, tenantId);
        }

        private IDisposable SetCurrent(Guid? userId, Guid? tenantId)
        {
            var parentScope = _currentAccessor.Current;
            _currentAccessor.Current = new ImpersonationInfo
            {
                TenantId = tenantId,
                UserId = userId
            };
            return new DisposeAction(() =>
            {
                _currentAccessor.Current = parentScope;
            });
        }
    }
}
