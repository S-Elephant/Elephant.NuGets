using Elephant.Io.Interfaces;

namespace Elephant.Io
{
    /// <inheritdoc cref="IFileService"/>
    public class FileService : IFileService
    {
        /// <inheritdoc/>
        public void Copy(string sourceFileName, string destFileName, bool overwrite = false) => File.Copy(sourceFileName, destFileName, overwrite);

        /// <inheritdoc/>
        public bool Exists(string? path) => File.Exists(path);

        /// <inheritdoc/>
        public bool NotExists(string? path) => !Exists(path);

        /// <inheritdoc/>
        public void Delete(string? path)
        {
            if (!string.IsNullOrEmpty(path))
                File.Delete(path);
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<IEnumerable<FileInfo>> GetFilesAsync(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, bool ignoreInaccessible = true)
        {
            EnumerationOptions options = new () { IgnoreInaccessible = ignoreInaccessible };

            if (searchOption == SearchOption.AllDirectories)
                options.RecurseSubdirectories = true;

            IEnumerable<DirectoryInfo> directoryInfos = sourceDirectories
                .Where(directory => directory.Exists)
                .Select(directory => directory);

            foreach (DirectoryInfo directory in directoryInfos)
            {
                IEnumerable<FileInfo> files = await Task.Run(() => directory.EnumerateFiles(searchPattern, options));
                yield return files;
            }
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<IEnumerable<FileInfo>> GetFilesAsync(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, IEnumerable<string> extensions, bool ignoreInaccessible = true)
        {
            EnumerationOptions options = new () { IgnoreInaccessible = ignoreInaccessible };

            if (searchOption == SearchOption.AllDirectories)
                options.RecurseSubdirectories = true;

            IEnumerable<DirectoryInfo> directoryInfos = sourceDirectories
                .Where(directory => directory.Exists);

            extensions = extensions.Select(e => e.ToLowerInvariant());
            foreach (DirectoryInfo directory in directoryInfos)
            {
                IEnumerable<FileInfo> files = await Task.Run(() => directory.EnumerateFiles(searchPattern, options)
                    .Where(f => extensions.Contains(Path.GetExtension(f.Name).ToLowerInvariant())));
                yield return files;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<FileInfo>> GetFilesAsyncAsIEnumerable(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, bool ignoreInaccessible = true)
        {
            List<IEnumerable<FileInfo>> result = await GetFilesAsync(sourceDirectories, searchPattern, searchOption, ignoreInaccessible).ToListAsync();

            // result.SelectMany(x => x) joins the list of lists together into one big list.
            return result.Any() ? result.SelectMany(x => x) : new List<FileInfo>();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<FileInfo>> GetFilesAsyncAsIEnumerable(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, IEnumerable<string> extensions, bool ignoreInaccessible = true)
        {
            List<IEnumerable<FileInfo>> result = await GetFilesAsync(sourceDirectories, searchPattern, searchOption, extensions, ignoreInaccessible).ToListAsync();

            // result.SelectMany(x => x) joins the list of lists together into one big list.
            return result.Any() ? result.SelectMany(x => x) : new List<FileInfo>();
        }
    }
}
