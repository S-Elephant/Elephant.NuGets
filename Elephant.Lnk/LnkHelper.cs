using IWshRuntimeLibrary;

namespace Elephant.Lnk
{
    /// <summary>
    /// A helper class for *.lnk files (shortcut files).
    /// </summary>
    public class LnkHelper
    {
        /// <summary>
        /// Returns the shortcut (*.lnk file) its <see cref="IWshShortcut.TargetPath"/> or null if something went wrong or if it has no <see cref="IWshShortcut.TargetPath"/>.
        /// </summary>
        public static string? GetTarget(string? fullLnkPath)
        {
            IWshShortcut? info = TryToReadInfo(fullLnkPath);
            if (info == null)
                return null;

            return info.TargetPath == string.Empty ? null : info.TargetPath;
        }

        /// <summary>
        /// Returns the shortcut (*.lnk file) its <see cref="IWshShortcut.Arguments"/> as a string or null if something went wrong or if it has no <see cref="IWshShortcut.Arguments"/>.
        /// </summary>
        public static string? GetArgumentsAsString(string? fullLnkPath)
        {
            IWshShortcut? info = TryToReadInfo(fullLnkPath);
            if (info == null)
                return null;

            return info.Arguments == string.Empty ? null : info.Arguments;
        }
        
        /// <summary>
        /// Returns the shortcut (*.lnk file) its <see cref="IWshShortcut.TargetPath"/> plus its <see cref="IWshShortcut.Arguments"/> (if any) or null if something went wrong or if it has no <see cref="IWshShortcut.TargetPath"/>.
        /// </summary>
        public static string? GetTargetWithArguments(string? fullLnkPath)
        {
            IWshShortcut? info = TryToReadInfo(fullLnkPath);
            if (info == null)
                return null;

            string? target = info.TargetPath == string.Empty ? null : info.TargetPath;
            if (target == null || info.Arguments == null)
                return target;
            
            return $"{target} {info.Arguments}";
        }
        
        /// <summary>
        /// Returns all known shortcut (*.lnk file) info or null if something went wrong.
        /// </summary>
        public static LnkInfo? GetAllInfo(string? fullLnkPath)
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
        
        /// <summary>
        /// Attempt to read the shortcut file. If something goes wrong then a null value will be returned.
        /// </summary>
        public static IWshShortcut? TryToReadInfo(string? fullLnkPath)
        {
            if (string.IsNullOrWhiteSpace(fullLnkPath) || !System.IO.File.Exists(fullLnkPath))
                return null;

            WshShell shell = new();
            return (IWshShortcut)shell.CreateShortcut(fullLnkPath);
        }

        private static string? ConvertoToNullIfEmpty(string value)
        {
            return value == string.Empty ? null : value;
        }
    }
}