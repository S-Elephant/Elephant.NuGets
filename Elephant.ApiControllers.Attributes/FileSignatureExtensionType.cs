namespace Elephant.ApiControllers.Attributes
{
    /// <summary>
    /// All <see cref="FileSignatureAttribute"/> allowed file extensions.
    /// </summary>
    public enum AllowedFileExtensionType
    {
        /// <summary>
        /// Microsoft Word document.
        /// </summary>
        Doc,

        /// <summary>
        /// XML based Microsoft Word document.
        /// </summary>
        Docx,

        /// <summary>
        /// Graphical Interchange Format.
        /// </summary>
        Gif,

        /// <summary>
        /// Microsoft icon.
        /// </summary>
        Ico,

        /// <summary>
        /// Joint Photographic Experts Group. Raw or in the JFIF or Exif file format.
        /// </summary>
        Jpeg,

        /// <summary>
        /// Joint Photographic Experts Group. Raw or in the JFIF or Exif file format.
        /// </summary>
        Jpg,

        /// <summary>
        /// Rich Text Format.
        /// </summary>
        Rtf,

        /// <summary>
        /// Portable Document Format.
        /// </summary>
        Pdf,

        /// <summary>
        /// Portable Network Graphics.
        /// </summary>
        Png,

        /// <summary>
        /// Scalable Vector Graphics.
        /// </summary>
        Svg,

        /// <summary>
        /// Tag Image File Format.
        /// </summary>
        Tiff,

        /// <summary>
        /// Plain text.
        /// </summary>
        Txt,
    }
}
