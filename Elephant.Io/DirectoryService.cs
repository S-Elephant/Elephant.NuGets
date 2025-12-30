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
		public void Delete(string path, bool recursive = false)
		{
			Directory.Delete(path, recursive);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			return Directory.EnumerateFiles(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> EnumerateDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			return Directory.EnumerateDirectories(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Exists(string? path)
		{
			return Directory.Exists(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			return Directory.GetFiles(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			return Directory.GetDirectories(path, searchPattern, searchOption);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Move(string sourceDirName, string destDirName)
		{
			Directory.Move(sourceDirName, destDirName);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetCurrentDirectory()
		{
			return Directory.GetCurrentDirectory();
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NotExists(string? path)
		{
			return !Directory.Exists(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCurrentDirectory(string path)
		{
			Directory.SetCurrentDirectory(path);
		}
	}
}
