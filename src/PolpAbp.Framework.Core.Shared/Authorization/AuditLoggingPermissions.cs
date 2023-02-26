using Volo.Abp.Reflection;

namespace PolpAbp.Framework.Authorization
{
    public static class AuditLoggingPermissions
    {
        public const string GroupName = "PolpAbpAuditLogging";

        public const string Default = GroupName + ".Default";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AuditLoggingPermissions));
        }
    }
}