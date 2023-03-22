using IWshRuntimeLibrary;

namespace Elephant.Lnk
{
    /// <summary>
    /// A helper class for *.lnk files (shortcut files).
    /// </summary>
    public interface ILnkHelper
    {
        /// <summary>
        /// Returns all known shortcut (*.lnk file) info or null if something went wrong.
        /// </summary>
        LnkInfo? GetAllInfo(string? fullLnkPath);

        /// <summary>
        /// Returns the shortcut (*.lnk file) its <see cref="IWshShortcut.Arguments"/> as a string or null if something went wrong or if it has no <see cref="IWshShortcut.Arguments"/>.
        /// </summary>
        string? GetArgumentsAsString(string? fullLnkPath);

        /// <summary>
        /// Returns the shortcut (*.lnk file) its <see cref="IWshShortcut.TargetPath"/> or null if something went wrong or if it has no <see cref="IWshShortcut.TargetPath"/>.
        /// </summary>
        string? GetTarget(string? fullLnkPath);

        /// <summary>
        /// Returns the shortcut (*.lnk file) its <see cref="IWshShortcut.TargetPath"/> plus its <see cref="IWshShortcut.Arguments"/> (if any) or null if something went wrong or if it has no <see cref="IWshShortcut.TargetPath"/>.
        /// </summary>
        string? GetTargetWithArguments(string? fullLnkPath);

        /// <summary>
        /// Attempt to read the shortcut file. If something goes wrong then a null value will be returned.
        /// </summary>
        IWshShortcut? TryToReadInfo(string? fullLnkPath);
    }
}
