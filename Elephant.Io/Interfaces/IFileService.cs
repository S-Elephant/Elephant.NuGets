namespace Elephant.Io.Interfaces
{
	/// <summary>
	/// File service.
	/// Abstraction over <see cref="File"/> for easier unit testing and such.
	/// </summary>
	public interface IFileService
	{
		/// <summary>
		/// Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
		/// </summary>
		void AppendAllLines(string path, IEnumerable<string> contents);

		/// <summary>
		/// Appends lines to a file by using a specified encoding, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
		/// </summary>
		void AppendAllLines(string path, IEnumerable<string> contents, System.Text.Encoding encoding);

		/// <summary>
		/// Asynchronously appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
		/// </summary>
		Task AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously appends lines to a file by using a specified encoding, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
		/// </summary>
		Task AppendAllLinesAsync(string path, IEnumerable<string> contents, System.Text.Encoding encoding, CancellationToken cancellationToken = default);

		/// <summary>
		/// Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
		/// </summary>
		void AppendAllText(string path, string? contents);

		/// <summary>
		/// Appends the specified string to the file using the specified encoding, creating the file if it does not already exist.
		/// </summary>
		void AppendAllText(string path, string? contents, System.Text.Encoding encoding);

		/// <summary>
		/// Asynchronously opens a file or creates the file if it does not already exist, appends the specified string to the file, and then closes the file.
		/// </summary>
		Task AppendAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously opens a file or creates the file if it does not already exist, appends the specified string to the file using the specified encoding, and then closes the file.
		/// </summary>
		Task AppendAllTextAsync(string path, string? contents, System.Text.Encoding encoding, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a StreamWriter that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist.
		/// </summary>
		StreamWriter AppendText(string path);

		/// <summary>
		/// Copy file.
		/// </summary>
		void Copy(string sourceFileName, string destFileName, bool overwrite = false);

		/// <summary>
		/// Creates or overwrites a file in the specified path.
		/// </summary>
		FileStream Create(string path);

		/// <summary>
		/// Creates or overwrites a file in the specified path, specifying a buffer size.
		/// </summary>
		FileStream Create(string path, int bufferSize);

		/// <summary>
		/// Creates or overwrites a file in the specified path, specifying a buffer size and options that describe how to create or overwrite the file.
		/// </summary>
		FileStream Create(string path, int bufferSize, FileOptions options);

		/// <summary>
		/// Creates or opens a file for writing UTF-8 encoded text. If the file already exists, its contents are overwritten.
		/// </summary>
		StreamWriter CreateText(string path);

		/// <summary>
		/// Decrypts a file that was encrypted by the current account using the Encrypt method.
		/// </summary>
		[System.Runtime.Versioning.SupportedOSPlatform("windows")]
		void Decrypt(string path);

		/// <summary>
		/// Delete file.
		/// </summary>
		void Delete(string? path);

		/// <summary>
		/// Encrypts a file so that only the account used to encrypt the file can decrypt it.
		/// </summary>
		[System.Runtime.Versioning.SupportedOSPlatform("windows")]
		void Encrypt(string path);

		/// <summary>
		/// Return whether or not a file exists.
		/// </summary>
		bool Exists(string? path);

		/// <summary>
		/// Gets the FileAttributes of the file on the path.
		/// </summary>
		FileAttributes GetAttributes(string path);

		/// <summary>
		/// Returns the creation date and time of the specified file or directory.
		/// </summary>
		DateTime GetCreationTime(string path);

		/// <summary>
		/// Returns the creation date and time, in coordinated universal time (UTC), of the specified file or directory.
		/// </summary>
		DateTime GetCreationTimeUtc(string path);

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

		/// <summary>
		/// Returns the date and time the specified file or directory was last accessed.
		/// </summary>
		DateTime GetLastAccessTime(string path);

		/// <summary>
		/// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed.
		/// </summary>
		DateTime GetLastAccessTimeUtc(string path);

		/// <summary>
		/// Returns the date and time the specified file or directory was last written to.
		/// </summary>
		DateTime GetLastWriteTime(string path);

		/// <summary>
		/// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to.
		/// </summary>
		DateTime GetLastWriteTimeUtc(string path);

		/// <summary>
		/// Moves a specified file to a new location, providing the option to specify a new file name.
		/// </summary>
		void Move(string sourceFileName, string destFileName);

		/// <summary>
		/// Moves a specified file to a new location, providing the options to specify a new file name and to overwrite the destination file if it already exists.
		/// </summary>
		void Move(string sourceFileName, string destFileName, bool overwrite);

		/// <summary>
		/// Return whether or not a file does NOT exist.
		/// </summary>
		bool NotExists(string? path);

		/// <summary>
		/// Opens a FileStream on the specified path with read/write access with no sharing.
		/// </summary>
		FileStream Open(string path, FileMode mode);

		/// <summary>
		/// Opens a FileStream on the specified path, with the specified mode and access with no sharing.
		/// </summary>
		FileStream Open(string path, FileMode mode, FileAccess access);

		/// <summary>
		/// Opens a FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
		/// </summary>
		FileStream Open(string path, FileMode mode, FileAccess access, FileShare share);

		/// <summary>
		/// Opens an existing file for reading.
		/// </summary>
		FileStream OpenRead(string path);

		/// <summary>
		/// Opens an existing UTF-8 encoded text file for reading.
		/// </summary>
		StreamReader OpenText(string path);

		/// <summary>
		/// Opens an existing file or creates a new file for writing.
		/// </summary>
		FileStream OpenWrite(string path);

		/// <summary>
		/// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
		/// </summary>
		byte[] ReadAllBytes(string path);

		/// <summary>
		/// Asynchronously opens a binary file, reads the contents of the file into a byte array, and then closes the file.
		/// </summary>
		Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default);

		/// <summary>
		/// Opens a text file, reads all lines of the file, and then closes the file.
		/// </summary>
		string[] ReadAllLines(string path);

		/// <summary>
		/// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
		/// </summary>
		string[] ReadAllLines(string path, System.Text.Encoding encoding);

		/// <summary>
		/// Asynchronously opens a text file, reads all lines of the file, and then closes the file.
		/// </summary>
		Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously opens a text file, reads all lines of the file with the specified encoding, and then closes the file.
		/// </summary>
		Task<string[]> ReadAllLinesAsync(string path, System.Text.Encoding encoding, CancellationToken cancellationToken = default);

		/// <summary>
		/// Opens a text file, reads all the text in the file, and then closes the file.
		/// </summary>
		string ReadAllText(string path);

		/// <summary>
		/// Opens a file, reads all text in the file with the specified encoding, and then closes the file.
		/// </summary>
		string ReadAllText(string path, System.Text.Encoding encoding);

		/// <summary>
		/// Asynchronously opens a text file, reads all the text in the file, and then closes the file.
		/// </summary>
		Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously opens a text file, reads all text in the file with the specified encoding, and then closes the file.
		/// </summary>
		Task<string> ReadAllTextAsync(string path, System.Text.Encoding encoding, CancellationToken cancellationToken = default);

		/// <summary>
		/// Reads the lines of a file.
		/// </summary>
		IEnumerable<string> ReadLines(string path);

		/// <summary>
		/// Read the lines of a file that has a specified encoding.
		/// </summary>
		IEnumerable<string> ReadLines(string path, System.Text.Encoding encoding);

		/// <summary>
		/// Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file.
		/// </summary>
		void Replace(string sourceFileName, string destinationFileName, string? destinationBackupFileName);

		/// <summary>
		/// Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file and optionally ignores merge errors.
		/// </summary>
		void Replace(string sourceFileName, string destinationFileName, string? destinationBackupFileName, bool ignoreMetadataErrors);

		/// <summary>
		/// Sets the specified FileAttributes of the file on the specified path.
		/// </summary>
		void SetAttributes(string path, FileAttributes fileAttributes);

		/// <summary>
		/// Sets the date and time the file was created.
		/// </summary>
		void SetCreationTime(string path, DateTime creationTime);

		/// <summary>
		/// Sets the date and time, in coordinated universal time (UTC), that the file was created.
		/// </summary>
		void SetCreationTimeUtc(string path, DateTime creationTimeUtc);

		/// <summary>
		/// Sets the date and time the specified file was last accessed.
		/// </summary>
		void SetLastAccessTime(string path, DateTime lastAccessTime);

		/// <summary>
		/// Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.
		/// </summary>
		void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);

		/// <summary>
		/// Sets the date and time that the specified file was last written to.
		/// </summary>
		void SetLastWriteTime(string path, DateTime lastWriteTime);

		/// <summary>
		/// Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.
		/// </summary>
		void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);

		/// <summary>
		/// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		void WriteAllBytes(string path, byte[] bytes);

		/// <summary>
		/// Asynchronously creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new file, writes a collection of strings to the file, and then closes the file.
		/// </summary>
		void WriteAllLines(string path, IEnumerable<string> contents);

		/// <summary>
		/// Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.
		/// </summary>
		void WriteAllLines(string path, IEnumerable<string> contents, System.Text.Encoding encoding);

		/// <summary>
		/// Creates a new file, write the specified string array to the file, and then closes the file.
		/// </summary>
		void WriteAllLines(string path, string[] contents);

		/// <summary>
		/// Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file.
		/// </summary>
		void WriteAllLines(string path, string[] contents, System.Text.Encoding encoding);

		/// <summary>
		/// Asynchronously creates a new file, writes the specified lines to the file, and then closes the file.
		/// </summary>
		Task WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously creates a new file, writes the specified lines to the file by using the specified encoding, and then closes the file.
		/// </summary>
		Task WriteAllLinesAsync(string path, IEnumerable<string> contents, System.Text.Encoding encoding, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		void WriteAllText(string path, string? contents);

		/// <summary>
		/// Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		void WriteAllText(string path, string? contents, System.Text.Encoding encoding);

		/// <summary>
		/// Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		Task WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		Task WriteAllTextAsync(string path, string? contents, System.Text.Encoding encoding, CancellationToken cancellationToken = default);
	}
}
