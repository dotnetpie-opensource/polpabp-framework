using Polpware.NetStd.Framework.IO;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Maintenance.Services
{
    public class FileOperationService : IFileOperationService, ITransientDependency
    {
        private readonly INopFileProvider _fileProvider;

        public FileOperationService(INopFileProvider nopFileProvider) {
            _fileProvider = nopFileProvider;
        }

        public Task DeleteFilesInDirAsync(string relativePath, DateTime lastCreatedUtc)
        {
            var filePath = _fileProvider.MapPath(relativePath);
            var di = new DirectoryInfo(filePath);

            foreach (var file in di.GetFiles())
            {
                if (file.LastWriteTimeUtc < lastCreatedUtc)
                {
                    file.Delete();
                }
            }

            return Task.CompletedTask;
        }

        public Task DeleteSubDirsInDirsAsync(IEnumerable<string> targets)
        {
            foreach (var prefix in targets)
            {
                var filePath = _fileProvider.MapPath(prefix);
                var di = new DirectoryInfo(filePath);

                foreach (var dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            return Task.CompletedTask;
        }
    }
}
