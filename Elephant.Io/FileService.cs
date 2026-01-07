using System.Runtime.CompilerServices;
using Elephant.Io.Interfaces;

namespace Elephant.Io
{
	/// <inheritdoc cref="IFileService"/>
	public class FileService : IFileService
	{
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendAllLines(string path, IEnumerable<string> contents)
		{
			File.AppendAllLines(path, contents);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendAllLines(string path, IEnumerable<string> contents, System.Text.Encoding encoding)
		{
			File.AppendAllLines(path, contents, encoding);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)
		{
			return File.AppendAllLinesAsync(path, contents, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task AppendAllLinesAsync(string path, IEnumerable<string> contents, System.Text.Encoding encoding, CancellationToken cancellationToken = default)
		{
			return File.AppendAllLinesAsync(path, contents, encoding, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendAllText(string path, string? contents)
		{
			File.AppendAllText(path, contents);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendAllText(string path, string? contents, System.Text.Encoding encoding)
		{
			File.AppendAllText(path, contents, encoding);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task AppendAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default)
		{
			return File.AppendAllTextAsync(path, contents, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task AppendAllTextAsync(string path, string? contents, System.Text.Encoding encoding, CancellationToken cancellationToken = default)
		{
			return File.AppendAllTextAsync(path, contents, encoding, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public StreamWriter AppendText(string path)
		{
			return File.AppendText(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Copy(string sourceFileName, string destFileName, bool overwrite = false)
		{
			File.Copy(sourceFileName, destFileName, overwrite);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileStream Create(string path)
		{
			return File.Create(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileStream Create(string path, int bufferSize)
		{
			return File.Create(path, bufferSize);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileStream Create(string path, int bufferSize, FileOptions options)
		{
			return File.Create(path, bufferSize, options);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public StreamWriter CreateText(string path)
		{
			return File.CreateText(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.Versioning.SupportedOSPlatform("windows")]
		public void Decrypt(string path)
		{
			File.Decrypt(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Delete(string? path)
		{
			if (!string.IsNullOrEmpty(path))
				File.Delete(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.Versioning.SupportedOSPlatform("windows")]
		public void Encrypt(string path)
		{
			File.Encrypt(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Exists(string? path)
		{
			return File.Exists(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileAttributes GetAttributes(string path)
		{
			return File.GetAttributes(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetCreationTime(string path)
		{
			return File.GetCreationTime(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetCreationTimeUtc(string path)
		{
			return File.GetCreationTimeUtc(path);
		}

		/// <inheritdoc/>
		public async IAsyncEnumerable<IEnumerable<FileInfo>> GetFilesAsync(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, bool ignoreInaccessible = true)
		{
			EnumerationOptions options = new() { IgnoreInaccessible = ignoreInaccessible };

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
			EnumerationOptions options = new() { IgnoreInaccessible = ignoreInaccessible };

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
#if NET10_0
			List<IEnumerable<FileInfo>> result = [];
			await foreach (IEnumerable<FileInfo> files in GetFilesAsync(sourceDirectories, searchPattern, searchOption, ignoreInaccessible))
				result.Add(files);
#else
			List<IEnumerable<FileInfo>> result = await GetFilesAsync(sourceDirectories, searchPattern, searchOption, ignoreInaccessible).ToListAsync();
#endif

			// result.SelectMany(x => x) joins the list of lists together into one big list.
			return result.Count == 0 ? result.SelectMany(x => x) : new List<FileInfo>();
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<FileInfo>> GetFilesAsyncAsIEnumerable(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, IEnumerable<string> extensions, bool ignoreInaccessible = true)
		{
#if NET10_0
			List<IEnumerable<FileInfo>> result = [];
			await foreach (IEnumerable<FileInfo> files in GetFilesAsync(sourceDirectories, searchPattern, searchOption, extensions, ignoreInaccessible))
				result.Add(files);
#else
			List<IEnumerable<FileInfo>> result = await GetFilesAsync(sourceDirectories, searchPattern, searchOption, extensions, ignoreInaccessible).ToListAsync();
#endif

			// result.SelectMany(x => x) joins the list of lists together into one big list.
			return result.Count == 0 ? result.SelectMany(x => x) : new List<FileInfo>();
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetLastAccessTime(string path)
		{
			return File.GetLastAccessTime(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetLastAccessTimeUtc(string path)
		{
			return File.GetLastAccessTimeUtc(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetLastWriteTime(string path)
		{
			return File.GetLastWriteTime(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DateTime GetLastWriteTimeUtc(string path)
		{
			return File.GetLastWriteTimeUtc(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Move(string sourceFileName, string destFileName)
		{
			File.Move(sourceFileName, destFileName);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Move(string sourceFileName, string destFileName, bool overwrite)
		{
			File.Move(sourceFileName, destFileName, overwrite);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NotExists(string? path)
		{
			return !Exists(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileStream Open(string path, FileMode mode)
		{
			return File.Open(path, mode);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileStream Open(string path, FileMode mode, FileAccess access)
		{
			return File.Open(path, mode, access);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return File.Open(path, mode, access, share);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileStream OpenRead(string path)
		{
			return File.OpenRead(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public StreamReader OpenText(string path)
		{
			return File.OpenText(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FileStream OpenWrite(string path)
		{
			return File.OpenWrite(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public byte[] ReadAllBytes(string path)
		{
			return File.ReadAllBytes(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default)
		{
			return File.ReadAllBytesAsync(path, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] ReadAllLines(string path)
		{
			return File.ReadAllLines(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string[] ReadAllLines(string path, System.Text.Encoding encoding)
		{
			return File.ReadAllLines(path, encoding);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = default)
		{
			return File.ReadAllLinesAsync(path, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task<string[]> ReadAllLinesAsync(string path, System.Text.Encoding encoding, CancellationToken cancellationToken = default)
		{
			return File.ReadAllLinesAsync(path, encoding, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ReadAllText(string path)
		{
			return File.ReadAllText(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ReadAllText(string path, System.Text.Encoding encoding)
		{
			return File.ReadAllText(path, encoding);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
		{
			return File.ReadAllTextAsync(path, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task<string> ReadAllTextAsync(string path, System.Text.Encoding encoding, CancellationToken cancellationToken = default)
		{
			return File.ReadAllTextAsync(path, encoding, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> ReadLines(string path)
		{
			return File.ReadLines(path);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> ReadLines(string path, System.Text.Encoding encoding)
		{
			return File.ReadLines(path, encoding);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Replace(string sourceFileName, string destinationFileName, string? destinationBackupFileName)
		{
			File.Replace(sourceFileName, destinationFileName, destinationBackupFileName);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Replace(string sourceFileName, string destinationFileName, string? destinationBackupFileName, bool ignoreMetadataErrors)
		{
			File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetAttributes(string path, FileAttributes fileAttributes)
		{
			File.SetAttributes(path, fileAttributes);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCreationTime(string path, DateTime creationTime)
		{
			File.SetCreationTime(path, creationTime);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			File.SetCreationTimeUtc(path, creationTimeUtc);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			File.SetLastAccessTime(path, lastAccessTime);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			File.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			File.SetLastWriteTime(path, lastWriteTime);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteAllBytes(string path, byte[] bytes)
		{
			File.WriteAllBytes(path, bytes);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default)
		{
			return File.WriteAllBytesAsync(path, bytes, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteAllLines(string path, IEnumerable<string> contents)
		{
			File.WriteAllLines(path, contents);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteAllLines(string path, IEnumerable<string> contents, System.Text.Encoding encoding)
		{
			File.WriteAllLines(path, contents, encoding);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteAllLines(string path, string[] contents)
		{
			File.WriteAllLines(path, contents);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteAllLines(string path, string[] contents, System.Text.Encoding encoding)
		{
			File.WriteAllLines(path, contents, encoding);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)
		{
			return File.WriteAllLinesAsync(path, contents, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task WriteAllLinesAsync(string path, IEnumerable<string> contents, System.Text.Encoding encoding, CancellationToken cancellationToken = default)
		{
			return File.WriteAllLinesAsync(path, contents, encoding, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteAllText(string path, string? contents)
		{
			File.WriteAllText(path, contents);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteAllText(string path, string? contents, System.Text.Encoding encoding)
		{
			File.WriteAllText(path, contents, encoding);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default)
		{
			return File.WriteAllTextAsync(path, contents, cancellationToken);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Task WriteAllTextAsync(string path, string? contents, System.Text.Encoding encoding, CancellationToken cancellationToken = default)
		{
			return File.WriteAllTextAsync(path, contents, encoding, cancellationToken);
		}
	}
}
