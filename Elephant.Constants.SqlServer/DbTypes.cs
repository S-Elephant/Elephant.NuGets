namespace Elephant.Constants.SqlServer
{
    /// <summary>
    /// Contains the constant database types.
    /// </summary>
    public static class DbType
    {
        /// <summary>
        /// A bit.
        /// </summary>
        public const string Bool = "BIT";

        /// <summary>
        /// <see cref="DateTime"/>.
        /// </summary>
        public const string DateTime = "DATETIME";

        /// <summary>
        /// <see cref="Decimal"/> 12,4.
        /// </summary>
        public const string Decimal = "DECIMAL(12,4)";

        /// <summary>
        /// <see cref="string"/> with max-length 254.
        /// </summary>
        /// <seealso href="https://stackoverflow.com/questions/1199190/what-is-the-optimal-length-for-an-email-address-in-a-database"/>
        public const string Email = "VARCHAR(254)";

        /// <summary>
        /// Enum as <see cref="int"/>.
        /// </summary>
        public const string Enum = "INT";

        /// <summary>
        /// <see cref="float"/>.
        /// </summary>
        public const string Float = "FLOAT";

        /// <summary>
        /// <see cref="string"/> with max-length 68.
        /// </summary>
        public const string Guid = "VARCHAR(68)";

        /// <summary>
        /// <see cref="int"/>.
        /// </summary>
        public const string Int = "INT";

        /// <summary>
        /// <see cref="string"/> with max-length 100.
        /// </summary>
        public const string Name = "VARCHAR(100)";

        /// <summary>
        /// <see cref="string"/> with max-length 1024.
        /// </summary>
        public const string Password = "VARCHAR(1024)";

        /// <summary>
        /// Real.
        /// </summary>
        public const string Real = "REAL";

        /// <summary>
        /// <see cref="string"/> with maximum size.
        /// </summary>
        public const string TextMax = "VARCHAR(MAX)";

        /// <summary>
        /// <see cref="string"/> with max-length 2048.
        /// </summary>
        public const string Url = "VARCHAR(2048)";
    }
}