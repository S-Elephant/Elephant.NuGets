using IWshRuntimeLibrary;

namespace Elephant.Lnk
{
    /// <inheritdoc cref="ILnkHelper"/>
    public class LnkHelper : ILnkHelper
    {
        /// <inheritdoc/>
        public string? GetTarget(string? fullLnkPath)
        {
            IWshShortcut? info = TryToReadInfo(fullLnkPath);
            if (info == null)
                return null;

            return info.TargetPath == string.Empty ? null : info.TargetPath;
        }

        /// <inheritdoc/>
        public string? GetArgumentsAsString(string? fullLnkPath)
        {
            IWshShortcut? info = TryToReadInfo(fullLnkPath);
            if (info == null)
                return null;

            return info.Arguments == string.Empty ? null : info.Arguments;
        }

        /// <inheritdoc/>
        public string? GetTargetWithArguments(string? fullLnkPath)
        {
            IWshShortcut? info = TryToReadInfo(fullLnkPath);
            if (info == null)
                return null;

            string? target = info.TargetPath == string.Empty ? null : info.TargetPath;
            if (target == null || info.Arguments == null)
                return target;

            return $"{target} {info.Arguments}";
        }

        /// <inheritdoc/>
        public LnkInfo? GetAllInfo(string? fullLnkPath)
        {
            IWshShortcut? info = TryToReadInfo(fullLnkPath);
            if (info == null)
                return null;

            return new LnkInfo(
                info.FullName,
                string.IsNullOrWhiteSpace(info.FullName) ? null : Path.GetFileNameWithoutExtension(info.FullName),
                ConvertoToNullIfEmpty(info.Arguments),
                ConvertoToNullIfEmpty(info.Description),
                ConvertoToNullIfEmpty(info.Hotkey),
                ConvertoToNullIfEmpty(info.IconLocation),
                ConvertoToNullIfEmpty(info.TargetPath),
                info.WindowStyle,
                ConvertoToNullIfEmpty(info.WorkingDirectory));
        }

        /// <inheritdoc/>
        public IWshShortcut? TryToReadInfo(string? fullLnkPath)
        {
            if (string.IsNullOrWhiteSpace(fullLnkPath) || !System.IO.File.Exists(fullLnkPath))
                return null;

            WshShell shell = new();
            return (IWshShortcut)shell.CreateShortcut(fullLnkPath);
        }

        /// <inheritdoc/>
        private static string? ConvertoToNullIfEmpty(string value)
        {
            return value == string.Empty ? null : value;
        }
    }
}