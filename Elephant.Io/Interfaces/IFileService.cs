namespace Elephant.Io.Interfaces
{
    /// <summary>
    /// File service.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Copy file.
        /// </summary>
        void Copy(string sourceFileName, string destFileName, bool overwrite = false);
        
        /// <summary>
        /// Return whether or not a file exists.
        /// </summary>
        bool Exists(string? path);

        /// <summary>
        /// Return whether or not a file does NOT exist.
        /// </summary>
        bool NotExists(string? path);

        /// <summary>
        /// Delete file.
        /// </summary>
        void Delete(string? path);

        /// <summary>
        /// Get files, matching a <paramref name="searchPattern"/> from <paramref name="sourceDirectories"/> (optionally ignoring currently inaccessible files).
        /// </summary>
        /// <param name="sourceDirectories">Zero or more source directory path(s)</param>
        /// <param name="searchPattern">Search pattern. The asterisk * can be used as a wildcard.</param>
        /// <param name="searchOption">Specifies whether to search the current directory, or the current directory and all subdirectories.</param>
        /// <param name="ignoreInaccessible">If true, currently inaccessible files will not be included in the return values.</param>
        /// <returns>Matching found files.</returns>
        IAsyncEnumerable<IEnumerable<FileInfo>> GetFilesAsync(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, bool ignoreInaccessible = true);

        /// <summary>
        /// Get files, matching a <paramref name="searchPattern"/> and having at least one of the <paramref name="extensions"/> from <paramref name="sourceDirectories"/> (optionally ignoring currently inaccessible files).
        /// </summary>
        /// <param name="sourceDirectories">Zero or more source directory path(s)</param>
        /// <param name="searchPattern">Search pattern. The asterisk * can be used as a wildcard.</param>
        /// <param name="searchOption">Specifies whether to search the current directory, or the current directory and all subdirectories.</param>
        /// <param name="extensions">File extensions (case-insensitive) to match. They must start with a dot and then the extension. Example: ".png".</param>
        /// <param name="ignoreInaccessible">If true, currently inaccessible files will not be included in the return values.</param>
        /// <returns>Matching found files.</returns>
        IAsyncEnumerable<IEnumerable<FileInfo>> GetFilesAsync(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, IEnumerable<string> extensions, bool ignoreInaccessible = true);

        /// <summary>
        /// Get files, matching a <paramref name="searchPattern"/> from <paramref name="sourceDirectories"/> (optionally ignoring currently inaccessible files) as an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="sourceDirectories">Zero or more source directory path(s)</param>
        /// <param name="searchPattern">Search pattern. The asterisk * can be used as a wildcard.</param>
        /// <param name="searchOption">Specifies whether to search the current directory, or the current directory and all subdirectories.</param>
        /// <param name="ignoreInaccessible">If true, currently inaccessible files will not be included in the return values.</param>
        /// <returns>Matching found files.</returns>
        Task<IEnumerable<FileInfo>> GetFilesAsyncAsIEnumerable(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, bool ignoreInaccessible = true);

        /// <summary>
        /// Get files, matching a <paramref name="searchPattern"/> and having at least one of the <paramref name="extensions"/> from <paramref name="sourceDirectories"/> (optionally ignoring currently inaccessible files) as an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="sourceDirectories">Zero or more source directory path(s)</param>
        /// <param name="searchPattern">Search pattern. The asterisk * can be used as a wildcard.</param>
        /// <param name="searchOption">Specifies whether to search the current directory, or the current directory and all subdirectories.</param>
        /// <param name="extensions">File extensions (case-insensitive) to match. They must start with a dot and then the extension. Example: ".png".</param>
        /// <param name="ignoreInaccessible">If true, currently inaccessible files will not be included in the return values.</param>
        /// <returns>Matching found files.</returns>
        Task<IEnumerable<FileInfo>> GetFilesAsyncAsIEnumerable(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, IEnumerable<string> extensions, bool ignoreInaccessible = true);
    }
}
