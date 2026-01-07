using System.Runtime.CompilerServices;
using Elephant.Io.Interfaces;

namespace Elephant.Io
{
	/// <inheritdoc cref="IDirectoryService"/>
	public class DirectoryService : IDirectoryService
	{
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DirectoryInfo CreateDirectory(string path)
		{
			return Directory.CreateDirectory(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileSystemInfo CreateSymbolicLink(string path, string pathToTarget)
		{
			return Directory.CreateSymbolicLink(path, pathToTarget);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Delete(string path)
		{
			Directory.Delete(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Delete(string path, bool recursive)
		{
			Directory.Delete(path, recursive);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateDirectories(string path)
		{
			return Directory.EnumerateDirectories(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
		{
			return Directory.EnumerateDirectories(path, searchPattern);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateDirectories(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.EnumerateDirectories(path, searchPattern, enumerationOptions);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateFiles(string path)
		{
			return Directory.EnumerateFiles(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
		{
			return Directory.EnumerateFiles(path, searchPattern);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFiles(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.EnumerateFiles(path, searchPattern, enumerationOptions);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateFileSystemEntries(string path)
		{
			return Directory.EnumerateFileSystemEntries(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
		{
			return Directory.EnumerateFileSystemEntries(path, searchPattern);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFileSystemEntries(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.EnumerateFileSystemEntries(path, searchPattern, enumerationOptions);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Exists(string? path)
		{
			return Directory.Exists(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetCreationTime(string path)
		{
			return Directory.GetCreationTime(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetCreationTimeUtc(string path)
		{
			return Directory.GetCreationTimeUtc(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetCurrentDirectory()
		{
			return Directory.GetCurrentDirectory();
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetDirectories(string path)
		{
			return Directory.GetDirectories(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetDirectories(string path, string searchPattern)
		{
			return Directory.GetDirectories(path, searchPattern);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetDirectories(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.GetDirectories(path, searchPattern, enumerationOptions);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetDirectoryRoot(string path)
		{
			return Directory.GetDirectoryRoot(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetFiles(string path)
		{
			return Directory.GetFiles(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetFiles(string path, string searchPattern)
		{
			return Directory.GetFiles(path, searchPattern);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetFiles(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.GetFiles(path, searchPattern, enumerationOptions);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetFileSystemEntries(string path)
		{
			return Directory.GetFileSystemEntries(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetFileSystemEntries(string path, string searchPattern)
		{
			return Directory.GetFileSystemEntries(path, searchPattern);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, enumerationOptions);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetLastAccessTime(string path)
		{
			return Directory.GetLastAccessTime(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetLastAccessTimeUtc(string path)
		{
			return Directory.GetLastAccessTimeUtc(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetLastWriteTime(string path)
		{
			return Directory.GetLastWriteTime(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetLastWriteTimeUtc(string path)
		{
			return Directory.GetLastWriteTimeUtc(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetLogicalDrives()
		{
			return Directory.GetLogicalDrives();
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DirectoryInfo? GetParent(string path)
		{
			return Directory.GetParent(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Move(string sourceDirName, string destDirName)
		{
			Directory.Move(sourceDirName, destDirName);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NotExists(string? path)
		{
			return !Directory.Exists(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileSystemInfo? ResolveLinkTarget(string linkPath, bool returnFinalTarget)
		{
			return Directory.ResolveLinkTarget(linkPath, returnFinalTarget);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCreationTime(string path, DateTime creationTime)
		{
			Directory.SetCreationTime(path, creationTime);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			Directory.SetCreationTimeUtc(path, creationTimeUtc);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCurrentDirectory(string path)
		{
			Directory.SetCurrentDirectory(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			Directory.SetLastAccessTime(path, lastAccessTime);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			Directory.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			Directory.SetLastWriteTime(path, lastWriteTime);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			Directory.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
		}
	}
}
