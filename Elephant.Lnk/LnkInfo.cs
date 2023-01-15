namespace Elephant.Lnk
{
    /// <summary>
    /// Shortcut (*.lnk) file info.
    /// </summary>
    public class LnkInfo
    {
        /// <summary>
        /// Full name.
        /// </summary>
        public string? FullName { get; set; } = null;

        /// <summary>
        /// Name.
        /// </summary>
        public string? Name { get; set; } = null;

        /// <summary>
        /// Arguments as one single <see cref="string"/>.
        /// </summary>
        public string? ArgumentsAsString { get; set; } = null;

        /// <summary>
        /// Description.
        /// </summary>
        public string? Description { get; set; } = null;

        /// <summary>
        /// Hotkey.
        /// </summary>
        public string? Hotkey { get; set; } = null;

        /// <summary>
        /// Icon location.
        /// </summary>
        public string? IconLocation { get; set; } = null;

        /// <summary>
        /// Target path.
        /// </summary>
        public string? TargetPath { get; set; } = null;

        /// <summary>
        /// Target path with arguments (if any).
        /// </summary>
        public string? TargetPathWithArguments
        {
            get
            {
                if (TargetPath == null)
                    return null;

                if (string.IsNullOrWhiteSpace(ArgumentsAsString))
                    return TargetPath;

                return $"{TargetPath} {ArgumentsAsString}";
            }
        }

        /// <summary>
        /// Window style.
        /// </summary>
        public int WindowStyle { get; set; }

        /// <summary>
        /// Working directory.
        /// </summary>
        public string? WorkingDirectory { get; set; } = null;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public LnkInfo()
        {
        }

        /// <summary>
        /// Constructor with all properties.
        /// </summary>
        public LnkInfo(string fullName, string? name, string? argumentsAsString, string? description, string? hotkey, string? iconLocation, string? targetPath, int windowStyle, string? workingDirectory)
        {
            FullName = fullName;
            Name = name;
            ArgumentsAsString = argumentsAsString;
            Description = description;
            Hotkey = hotkey;
            IconLocation = iconLocation;
            TargetPath = targetPath;
            WindowStyle = windowStyle;
            WorkingDirectory = workingDirectory;
        }
    }
}
