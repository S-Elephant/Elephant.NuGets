namespace Elephant.Io.Interfaces
{
	/// <summary>
	/// Directory service.
	/// </summary>
	public interface IDirectoryService
	{
		/// <summary>
		/// Creates all directories and subdirectories in the specified path unless they already exist.
		/// </summary>
		DirectoryInfo CreateDirectory(string path);

		/// <summary>
		/// Deletes an empty directory, or optionally deletes contents recursively.
		/// </summary>
		void Delete(string path, bool recursive = false);

		/// <summary>
		/// Returns an enumerable collection of file names that match a search pattern in a specified path, using a <see cref="SearchOption"/>.
		/// </summary>
		IEnumerable<string> EnumerateFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);

		/// <summary>
		/// Returns an enumerable collection of directory names that match a search pattern in a specified path, using a <see cref="SearchOption"/>.
		/// </summary>
		IEnumerable<string> EnumerateDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);

		/// <summary>
		/// Tests if the given path refers to an existing DirectoryInfo on disk.
		/// </summary>
		bool Exists(string? path);

		/// <summary>
		/// Returns the names of files (as an array) that match a search pattern in a specified path, using a <see cref="SearchOption"/>.
		/// </summary>
		string[] GetFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);

		/// <summary>
		/// Returns the names of directories (as an array) that match a search pattern in a specified path, using a <see cref="SearchOption"/>.
		/// </summary>
		string[] GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);

		/// <summary>
		/// Moves a directory and its contents to a new location.
		/// </summary>
		void Move(string sourceDirName, string destDirName);

		/// <summary>
		/// Gets the current working directory of the application.
		/// </summary>
		string GetCurrentDirectory();

		/// <summary>
		/// Tests if the given path refers to a non-existing DirectoryInfo on disk.
		/// </summary>
		bool NotExists(string? path);

		/// <summary>
		/// Sets the application's current working directory.
		/// </summary>
		void SetCurrentDirectory(string path);
	}
}
