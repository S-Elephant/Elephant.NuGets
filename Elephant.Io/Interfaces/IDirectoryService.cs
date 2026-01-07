namespace Elephant.Io.Interfaces
{
	/// <summary>
	/// Directory service.
	/// Abstraction over <see cref="Directory"/> for easier unit testing and such.
	/// </summary>
	public interface IDirectoryService
	{
		/// <summary>
		/// Creates a symbolic link identified by path that points to pathToTarget.
		/// </summary>
		FileSystemInfo CreateSymbolicLink(string path, string pathToTarget);

		/// <summary>
		/// Creates all directories and subdirectories in the specified path unless they already exist.
		/// </summary>
		DirectoryInfo CreateDirectory(string path);

		/// <summary>
		/// Deletes an empty directory from a specified path.
		/// </summary>
		void Delete(string path);

		/// <summary>
		/// Deletes the specified directory and, if indicated, any subdirectories and files in the directory.
		/// </summary>
		void Delete(string path, bool recursive);

		/// <summary>
		/// Returns an enumerable collection of directory full names in a specified path.
		/// </summary>
		IEnumerable<string> EnumerateDirectories(string path);

		/// <summary>
		/// Returns an enumerable collection of directory full names that match a search pattern in a specified path.
		/// </summary>
		IEnumerable<string> EnumerateDirectories(string path, string searchPattern);

		/// <summary>
		/// Returns an enumerable collection of directory full names that match a search pattern in a specified path, and optionally searches subdirectories.
		/// </summary>
		IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption);

		/// <summary>
		/// Returns an enumerable collection of directory full names that match a search pattern in a specified path, and optionally searches subdirectories.
		/// </summary>
		IEnumerable<string> EnumerateDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions);

		/// <summary>
		/// Returns an enumerable collection of full file names in a specified path.
		/// </summary>
		IEnumerable<string> EnumerateFiles(string path);

		/// <summary>
		/// Returns an enumerable collection of full file names that match a search pattern in a specified path.
		/// </summary>
		IEnumerable<string> EnumerateFiles(string path, string searchPattern);

		/// <summary>
		/// Returns an enumerable collection of full file names that match a search pattern in a specified path, and optionally searches subdirectories.
		/// </summary>
		IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);

		/// <summary>
		/// Returns an enumerable collection of full file names that match a search pattern and enumeration options in a specified path, and optionally searches subdirectories.
		/// </summary>
		IEnumerable<string> EnumerateFiles(string path, string searchPattern, EnumerationOptions enumerationOptions);

		/// <summary>
		/// Returns an enumerable collection of file names and directory names in a specified path.
		/// </summary>
		IEnumerable<string> EnumerateFileSystemEntries(string path);

		/// <summary>
		/// Returns an enumerable collection of file names and directory names that match a search pattern in a specified path.
		/// </summary>
		IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern);

		/// <summary>
		/// Returns an enumerable collection of file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.
		/// </summary>
		IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption);

		/// <summary>
		/// Returns an enumerable collection of file names and directory names that match a search pattern and enumeration options in a specified path, and optionally searches subdirectories.
		/// </summary>
		IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions);

		/// <summary>
		/// Determines whether the given path refers to an existing directory on disk.
		/// </summary>
		bool Exists(string? path);

		/// <summary>
		/// Gets the creation date and time of a directory.
		/// </summary>
		DateTime GetCreationTime(string path);

		/// <summary>
		/// Gets the creation date and time, in Coordinated Universal Time (UTC) format, of a directory.
		/// </summary>
		DateTime GetCreationTimeUtc(string path);

		/// <summary>
		/// Gets the current working directory of the application.
		/// </summary>
		string GetCurrentDirectory();

		/// <summary>
		/// Returns the names of subdirectories (including their paths) in the specified directory.
		/// </summary>
		string[] GetDirectories(string path);

		/// <summary>
		/// Returns the names of subdirectories (including their paths) that match the specified search pattern in the specified directory.
		/// </summary>
		string[] GetDirectories(string path, string searchPattern);

		/// <summary>
		/// Returns the names of the subdirectories (including their paths) that match the specified search pattern in the specified directory, and optionally searches subdirectories.
		/// </summary>
		string[] GetDirectories(string path, string searchPattern, SearchOption searchOption);

		/// <summary>
		/// Returns the names of subdirectories (including their paths) that match the specified search pattern and enumeration options in the specified directory.
		/// </summary>
		string[] GetDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions);

		/// <summary>
		/// Returns the volume information, root information, or both for the specified path.
		/// </summary>
		string GetDirectoryRoot(string path);

		/// <summary>
		/// Returns the names of files (including their paths) in the specified directory.
		/// </summary>
		string[] GetFiles(string path);

		/// <summary>
		/// Returns the names of files (including their paths) that match the specified search pattern in the specified directory.
		/// </summary>
		string[] GetFiles(string path, string searchPattern);

		/// <summary>
		/// Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.
		/// </summary>
		string[] GetFiles(string path, string searchPattern, SearchOption searchOption);

		/// <summary>
		/// Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
		/// </summary>
		string[] GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions);

		/// <summary>
		/// Returns the names of all files and subdirectories in a specified path.
		/// </summary>
		string[] GetFileSystemEntries(string path);

		/// <summary>
		/// Returns an array of file names and directory names that match a search pattern in a specified path.
		/// </summary>
		string[] GetFileSystemEntries(string path, string searchPattern);

		/// <summary>
		/// Returns an array of all the file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.
		/// </summary>
		string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption);

		/// <summary>
		/// Returns an array of file names and directory names that match a search pattern and enumeration options in a specified path.
		/// </summary>
		string[] GetFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions);

		/// <summary>
		/// Returns the date and time the specified file or directory was last accessed.
		/// </summary>
		DateTime GetLastAccessTime(string path);

		/// <summary>
		/// Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.
		/// </summary>
		DateTime GetLastAccessTimeUtc(string path);

		/// <summary>
		/// Returns the date and time the specified file or directory was last written to.
		/// </summary>
		DateTime GetLastWriteTime(string path);

		/// <summary>
		/// Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last written to.
		/// </summary>
		DateTime GetLastWriteTimeUtc(string path);

		/// <summary>
		/// Retrieves the names of the logical drives on this computer in the form "drive letter:\" (for example, "C:\").
		/// </summary>
		string[] GetLogicalDrives();

		/// <summary>
		/// Retrieves the parent directory of the specified path, including both absolute and relative paths.
		/// </summary>
		DirectoryInfo? GetParent(string path);

		/// <summary>
		/// Moves a file or a directory and its contents to a new location.
		/// </summary>
		void Move(string sourceDirName, string destDirName);

		/// <summary>
		/// Determines whether the given path does not refer to an existing directory on disk.
		/// </summary>
		bool NotExists(string? path);

		/// <summary>
		/// Gets the target of the specified link.
		/// </summary>
		FileSystemInfo? ResolveLinkTarget(string linkPath, bool returnFinalTarget);

		/// <summary>
		/// Sets the creation date and time for the specified file or directory.
		/// </summary>
		void SetCreationTime(string path, DateTime creationTime);

		/// <summary>
		/// Sets the creation date and time, in Coordinated Universal Time (UTC) format, for the specified file or directory.
		/// </summary>
		void SetCreationTimeUtc(string path, DateTime creationTimeUtc);

		/// <summary>
		/// Sets the application's current working directory to the specified directory.
		/// </summary>
		void SetCurrentDirectory(string path);

		/// <summary>
		/// Sets the date and time the specified file or directory was last accessed.
		/// </summary>
		void SetLastAccessTime(string path, DateTime lastAccessTime);

		/// <summary>
		/// Sets the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.
		/// </summary>
		void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);

		/// <summary>
		/// Sets the date and time a directory was last written to.
		/// </summary>
		void SetLastWriteTime(string path, DateTime lastWriteTime);

		/// <summary>
		/// Sets the date and time, in Coordinated Universal Time (UTC) format, that a directory was last written to.
		/// </summary>
		void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);
	}
}
