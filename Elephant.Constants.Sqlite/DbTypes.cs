namespace Elephant.Constants.Sqlite
{
    /// <summary>
    /// Contains the constant database types.
    /// See: https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/types#:~:text=SQLite%20only%20has%20four%20primitive,types%20are%20supported%20by%20Microsoft.
    /// </summary>
    public static class DbType
    {
        private static class Primitives
        {
            /// <summary>
            /// Sqlite primitive version of the Blob.
            /// </summary>
            public const string Blob = "BLOB";

            /// <summary>
            /// Sqlite primitive version of the <see cref="int"/>.
            /// </summary>
            public const string Integer = "INTEGER";

            /// <summary>
            /// Sqlite primitive version of the Real (also used for floats and doubles.
            /// </summary>
            public const string Real = "REAL";

            /// <summary>
            /// Sqlite primitive version of the <see cref="string"/>.
            /// </summary>
            public const string Text = "TEXT";
        }

        /// <summary>
        /// A <see cref="Primitives.Blob"/>.
        /// </summary>
        public const string Blob = Primitives.Blob;

        /// <summary>
        /// A <see cref="bool"/> which in Sqlite is a <see cref="Primitives.Integer"/>.
        /// </summary>
        public const string Bool = Primitives.Integer;

        /// <summary>
        /// A <see cref="byte"/> which in Sqlite is a <see cref="Primitives.Integer"/>.
        /// </summary>
        public const string Byte = Primitives.Integer;

        /// <summary>
        /// <see cref="DateTime"/> which in Sqlite is a <see cref="Primitives.Text"/>.
        /// </summary>
        public const string DateTime = Primitives.Text;

        /// <summary>
        /// <see cref="Decimal"/> 12,4 which in Sqlite is a <see cref="Primitives.Real"/>.
        /// </summary>
        public const string Decimal = Primitives.Real;

        /// <summary>
        /// <see cref="double"/> 12,4 which in Sqlite is a <see cref="Primitives.Real"/>.
        /// </summary>
        public const string Double = Primitives.Real;

        /// <summary>
        /// E-mail which in Sqlite is a <see cref="Primitives.Text"/>.
        /// </summary>
        /// <seealso href="https://stackoverflow.com/questions/1199190/what-is-the-optimal-length-for-an-email-address-in-a-database"/>
        public const string Email = Primitives.Text;

        /// <summary>
        /// Enum as <see cref="int"/> which in Sqlite is a <see cref="Primitives.Integer"/>.
        /// </summary>
        public const string EnumAsInt = Primitives.Integer;

        /// <summary>
        /// Enum as <see cref="int"/> which in Sqlite is a <see cref="Primitives.Text"/>.
        /// </summary>
        public const string EnumAsString = Primitives.Text;

        /// <summary>
        /// <see cref="float"/> which in Sqlite is a <see cref="Primitives.Real"/>.
        /// </summary>
        public const string Float = Primitives.Real;

        /// <summary>
        /// Guid which in Sqlite is a <see cref="Primitives.Text"/>.
        /// </summary>
        public const string Guid = Primitives.Text;

        /// <summary>
        /// <see cref="int"/> which in Sqlite is a <see cref="Primitives.Integer"/>.
        /// </summary>
        public const string Int = Primitives.Integer;

        /// <summary>
        /// Name which in Sqlite is a <see cref="Primitives.Text"/>.
        /// </summary>
        public const string Name = Primitives.Text;

        /// <summary>
        /// Password which in Sqlite is a <see cref="Primitives.Text"/>.
        /// </summary>
        public const string Password = Primitives.Text;

        /// <summary>
        /// A <see cref="Primitives.Real"/> which in Sqlite is a <see cref="Primitives.Real"/>.
        /// </summary>
        public const string Real = Primitives.Real;

        /// <summary>
        /// <see cref="string"/> with maximum size which in Sqlite is a <see cref="Primitives.Text"/>.
        /// </summary>
        public const string TextMax = Primitives.Text;

        /// <summary>
        /// Url which in Sqlite is a <see cref="Primitives.Text"/>.
        /// </summary>
        public const string Url = Primitives.Text;
    }
}