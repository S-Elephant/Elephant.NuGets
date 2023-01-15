using System.Diagnostics;

namespace Elephant.Common
{
    /// <summary>
    /// Helper class for starting a process.
    /// </summary>
    public static class ProcessStarter
    {
        /// <summary>
        /// Process start result interface.
        /// </summary>
        public interface IProcessStartResult
        {
            /// <summary>
            /// Error message or null if success.
            /// </summary>
            string? ErrorMessage { get; set; }

            /// <summary>
            /// Indicates if this result has an <see cref="ErrorMessage"/>.
            /// </summary>
            bool HasError { get; }

            /// <summary>
            /// The process, may be null if an error occurred or for other reasons.
            /// </summary>
            Process? Process { get; set; }
        }

        /// <summary>
        /// Process start result.
        /// </summary>
        public class ProcessStartResult : IProcessStartResult
        {
            /// <summary>
            /// <inheritdoc cref="IProcessStartResult.ErrorMessage"/>
            /// </summary>
            public string? ErrorMessage { get; set; } = null;

            /// <summary>
            /// <inheritdoc cref="HasError"/>
            /// </summary>
            public bool HasError { get => ErrorMessage != null; }

            /// <summary>
            /// <inheritdoc cref="IProcessStartResult.Process"/>
            /// </summary>
            public Process? Process { get; set; } = null;

            /// <summary>
            /// Empty constructor.
            /// </summary>
            public ProcessStartResult()
            {
            }

            /// <summary>
            /// Constructor with properties.
            /// </summary>
            public ProcessStartResult(string? errorMessage, Process? process = null)
            {
                ErrorMessage = errorMessage;
                Process = process;
            }
        }

        /// <summary>
        /// Starts the specified <paramref name="fullPath"/>, optionally with the specified <paramref name="arguments"/>.
        /// </summary>
        /// <param name="fullPath">Full path, including filename and extension. If its null then no process will be started and an error <see cref="ProcessStartResult"/> will be returned.</param>
        /// <param name="arguments">Optional start arguments.</param>
        /// <param name="workingDirectory">Optional process working directory.</param>
        /// <param name="useShellExecute">Indicates whether or not to use <see cref="ProcessStartInfo.UseShellExecute"/>.</param>
        public static IProcessStartResult StartProcess(string? fullPath, string? arguments = null, string? workingDirectory = null, bool useShellExecute = false)
        {
            if (fullPath == null)
                return new ProcessStartResult("No path."); // Error, no path.

            ProcessStartInfo info = new ()
            {
                FileName = fullPath,
                UseShellExecute = useShellExecute,
                Arguments = arguments ?? string.Empty,
            };
            if (workingDirectory != null)
                info.WorkingDirectory = workingDirectory;

            Process? process = null;
            try
            {
                process = Process.Start(info);
            }
            catch (Exception exception)
            {
                return new ProcessStartResult(exception.ToString()); // Error, see exception details.
            }

            return new ProcessStartResult(null, process); // All went well.
        }
    }
}
