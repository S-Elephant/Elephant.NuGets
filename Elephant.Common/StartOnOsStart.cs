using System.Runtime.Versioning;

namespace Elephant.Common
{
    /// <summary>
    /// Helper for automatically starting an application when the operating system starts. Currently only Windows is supported.
    /// </summary>
    public static class StartOnOsStart
    {
        private const string OsWindows = "windows";
        
        /// <summary>
        /// The registry sub key name for the Windows operating system.
        /// </summary>
        public static string WindowsSubKeyName { get; set; } = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// Returns whether or not the specified <see cref="applicationName"/> exists in the startup registry section as specified in <see cref="WindowsSubKeyName"/>.
        /// </summary>
        [SupportedOSPlatform(OsWindows)]
        public static bool IsRegisteredInStartup(string applicationName)
        {
            try
            {
                Microsoft.Win32.RegistryKey? registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(WindowsSubKeyName, true);
                if (registryKey == null)
                    return false;
                
                object? value = registryKey.GetValue(applicationName);
                return value != null && (bool)value;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to register the <see cref="applicationName"/> in the startup registry section as specified in <see cref="WindowsSubKeyName"/>.
        /// </summary>
        [SupportedOSPlatform(OsWindows)]
        public static bool RegisterInStartup(string applicationName, string fullPathToExecutable)
        {
            try
            {
                Microsoft.Win32.RegistryKey? registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(WindowsSubKeyName, true);
                if (registryKey == null)
                    return false;

                registryKey.SetValue(applicationName, fullPathToExecutable);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Attempts to de-register the <see cref="applicationName"/> in the startup registry section as specified in <see cref="WindowsSubKeyName"/>.
        /// </summary>
        [SupportedOSPlatform(OsWindows)]
        public static bool DeregisterFromStartup(string applicationName)
        {
            try
            {
                Microsoft.Win32.RegistryKey? registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(WindowsSubKeyName, true);
                if (registryKey == null)
                    return false;

                registryKey.DeleteValue(applicationName);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
