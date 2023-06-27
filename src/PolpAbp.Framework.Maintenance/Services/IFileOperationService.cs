namespace PolpAbp.Framework.Maintenance.Services
{
    public interface IFileOperationService
    {
        /// <summary>
        /// Deletes the files in the given path.
        /// </summary>
        /// <param name="relativePath">Relative path, such as ~/Logs</param>
        /// <param name="lastCreatedUtc">A date time that is used for choosing the files whose last write time is earlier</param>
        /// <returns>Task</returns>
        Task DeleteFilesInDirAsync(string relativePath, DateTime lastCreatedUtc);
        /// <summary>
        /// Delete subfolders for the given targets.
        /// </summary>
        /// <param name="targets">A list of targets, in terms of the relative path, such as ~/Logs</param>
        /// <returns>Task</returns>
        Task DeleteSubDirsInDirsAsync(IEnumerable<string> targets);
    }
}
