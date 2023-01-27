using JetBrains.Annotations;

namespace Volo.Abp.SettingManagement
{
    public static class SettingManagementExtensions
    {
        public static async Task<T> GetDefaultAsync<T>([NotNull] this ISettingManager settingManager,
            [NotNull] string name, bool fallback = true, T defaultValue = default)
    where T : struct
        {
            Check.NotNull(settingManager, nameof(settingManager));
            Check.NotNull(name, nameof(name));

            var value = await settingManager.GetOrNullDefaultAsync(name, fallback);
            return value?.To<T>() ?? defaultValue;
        }

        public static async Task<T> GetGlobalAsync<T>([NotNull] this ISettingManager settingManager,
            [NotNull] string name, bool fallback = true, T defaultValue = default)
    where T : struct
        {
            Check.NotNull(settingManager, nameof(settingManager));
            Check.NotNull(name, nameof(name));

            var value = await settingManager.GetOrNullGlobalAsync(name, fallback);
            return value?.To<T>() ?? defaultValue;
        }

        public static async Task<T> GetConfigurationAsyncAsync<T>([NotNull] this ISettingManager settingManager,
            [NotNull] string name, bool fallback = true, T defaultValue = default)
    where T : struct
        {
            Check.NotNull(settingManager, nameof(settingManager));
            Check.NotNull(name, nameof(name));

            var value = await settingManager.GetOrNullConfigurationAsync(name, fallback);
            return value?.To<T>() ?? defaultValue;
        }

        public static async Task<T> GetForCurrentTenantAsync<T>([NotNull] this ISettingManager settingManager, 
            [NotNull] string name, bool fallback = true, T defaultValue = default)
    where T : struct
        {
            Check.NotNull(settingManager, nameof(settingManager));
            Check.NotNull(name, nameof(name));

            var value = await settingManager.GetOrNullForCurrentTenantAsync(name, fallback);
            return value?.To<T>() ?? defaultValue;
        }

        public static async Task<T> GetForTenantAsync<T>([NotNull] this ISettingManager settingManager, 
            [NotNull] string name, Guid tenantId, bool fallback = true, T defaultValue = default)
    where T : struct
        {
            Check.NotNull(settingManager, nameof(settingManager));
            Check.NotNull(name, nameof(name));

            var value = await settingManager.GetOrNullForTenantAsync(name, tenantId, fallback);
            return value?.To<T>() ?? defaultValue;
        }

        public static async Task<T> GetForCurrentUserAsync<T>([NotNull] this ISettingManager settingManager,
            [NotNull] string name, bool fallback = true, T defaultValue = default)
    where T : struct
        {
            Check.NotNull(settingManager, nameof(settingManager));
            Check.NotNull(name, nameof(name));

            var value = await settingManager.GetOrNullForCurrentUserAsync(name, fallback);
            return value?.To<T>() ?? defaultValue;
        }

        public static async Task<T> GetForUserAsync<T>([NotNull] this ISettingManager settingManager,
            [NotNull] string name, Guid userId, bool fallback = true, T defaultValue = default)
    where T : struct
        {
            Check.NotNull(settingManager, nameof(settingManager));
            Check.NotNull(name, nameof(name));

            var value = await settingManager.GetOrNullForUserAsync(name, userId, fallback);
            return value?.To<T>() ?? defaultValue;
        }
    }
}
