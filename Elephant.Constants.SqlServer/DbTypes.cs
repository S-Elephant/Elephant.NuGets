namespace Elephant.Constants.SqlServer
{
    /// <summary>
    /// Microsoft Sequel Server database constant types plus a few extras.
    /// </summary>
    public static class DbTypes
    {
        #region Numeric data types

        /// <summary>
        /// Integer that can be 0, 1, or NULL.
        /// Storage: 1 byte.
        /// </summary>
        public const string Bool = "BIT";

        /// <summary>
        /// Tiny integer (range from 0 to 255 (inclusive)).
        /// Storage: 1 byte.
        /// </summary>
        public const string TinyInt = "TINYINT";

        /// <summary>
        /// Small integer. Signed range is from -32768 to 32767.
        /// Unsigned range is from 0 to 65535. The size parameter specifies the maximum display width (which is 255).
        /// Storage: 2 bytes.
        /// </summary>
        public const string SmallInt = "SMALLINT";

        /// <summary>
        /// Allows whole numbers between -2,147,483,648 and 2,147,483,647.
        /// Storage: 4 bytes.
        /// </summary>
        public const string Int = "INT";

        /// <summary>
        /// Allows whole numbers between -9,223,372,036,854,775,808 and 9,223,372,036,854,775,807
        /// Storage: 8 bytes.
        /// </summary>
        public const string BigInt = "BIGINT";

        /// <summary>
        /// <see cref="Decimal"/> 12,4.
        /// </summary>
        public const string Decimal = "DECIMAL(12,4)";

        /// <summary>
        /// Monetary data from -214,748.3648 to 214,748.3647.
        /// Storage: 4 bytes.
        /// </summary>
        public const string SmallMoney = "SMALLMONEY";

        /// <summary>
        /// Monetary data from -922,337,203,685,477.5808 to 922,337,203,685,477.5807.
        /// Storage: 8 bytes.
        /// </summary>
        public const string Money = "MONEY";

        /// <summary>
        /// <see cref="float"/>. Precision: 7 digits .
        /// The n parameter indicates whether the field should hold 4 or 8 bytes. float (24) holds a 4-byte field and float (53) holds an 8-byte field.Default value of n is 53.
        /// Is an approximate data type; therefore, not all values in the data type range can be represented exactly.
        /// Storage: 4 bytes.
        /// For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/float-and-real-transact-sql?view=sql-server-ver16.
        /// </summary>
        public const string Float4 = "FLOAT(24)";

        /// <summary>
        /// Precision: 15 digits (number data from -1.79E + 308 to 1.79E + 308).
        /// Is an approximate data type; therefore, not all values in the data type range can be represented exactly.
        /// Storage: 8 bytes.
        /// For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/float-and-real-transact-sql?view=sql-server-ver16.
        /// </summary>
        public const string Float = "FLOAT(53)";

        /// <summary>
        /// The ISO synonym for real is FLOAT(24).
        /// Floating precision number data from -3.40E + 38 to 3.40E + 38.
        /// Is an approximate data type; therefore, not all values in the data type range can be represented exactly.
        /// Storage: 4 bytes.
        /// </summary>
        public const string Real = "REAL";

        #endregion

        #region Date and datetime datatypes

        /// <summary>
        /// From January 1, 1753 to December 31, 9999 with an accuracy of 3.33 milliseconds.
        /// Storage: 8 bytes.
        /// </summary>
        public const string DateTime = "DATETIME";

        /// <summary>
        /// From January 1, 0001 to December 31, 9999 with an accuracy of 100 nanoseconds.
        /// For more info for DateTime vs DateTime2 in MSSQL see: https://stackoverflow.com/a/1334193
        /// Storage: 6-8 bytes.
        /// </summary>
        public const string DateTime2 = "DATETIME2";

        /// <summary>
        /// From January 1, 1900 to through June 6, 2079.
        /// Storage: 4 bytes.
        /// </summary>
        public const string SmallDateTime = "SMALLDATETIME";

        /// <summary>
        /// Store a date only. From January 1, 0001 to December 31, 9999.
        /// Storage: 3 bytes.
        /// </summary>
        public const string Date = "DATE";

        /// <summary>
        /// Store a time only to an accuracy of 100 nanoseconds.
        /// Storage: 5 bytes, fixed, is the default with the default of 100ns fractional second precision.
        /// In Informatica, the default is 4 bytes, fixed, with the default of 1ms fractional second precision.
        /// </summary>
        public const string Time = "TIME";

        /// <summary>
        /// The same as <see cref="DateTime2"/> with the addition of a time zone offset.
        /// Storage: 8-10 bytes.
        /// </summary>
        public const string DateTimeOffset = "DATETIMEOFFSET";

        /// <summary>
        /// Stores a unique number that gets updated every time a row gets created or modified.
        /// The <see cref="Timestamp"/> value is based upon an internal clock and does not correspond
        /// to real time. Each table may have only one timestamp variable/column.
        /// Storage: 4 bytes.
        /// </summary>
        public const string Timestamp = "TIMESTAMP";

        #endregion

        #region String data types

        /// <summary>
        /// Variable length string without support for Unicode data.
        /// Storage: Variable 2 bytes + number of chars but up to 2³¹-1 bytes.
        /// Will attempt to store the data directly in the row unless it exceeds
        /// the 8k limitation and at that point it stores it in a blob.
        /// </summary>
        public const string VarCharMax = "VARCHAR(MAX)";

        /// <summary>
        /// Variable length string that supports Unicode data.
        /// Storage: Variable 4 bytes + number of chars but up to 2³¹-1 bytes.
        /// Will attempt to store the data directly in the row unless it exceeds
        /// the 8k limitation and at that point it stores it in a blob.
        /// </summary>
        public const string NVarCharMax = "NVARCHAR(MAX)";

        /// <summary>
        /// Variable length string that supports Unicode data.
        /// Storage: Variable 4 bytes + number of chars but up to 2³¹-1 bytes and is always stored in a blob.
        /// </summary>
        public const string Text = "TEXT";

        #endregion

        #region Language

        /// <summary>
        /// Language ISO's.
        /// See: https://www.loc.gov/standards/iso639-2/php/code_list.php
        /// </summary>
        public static class Language
        {
            /// <summary>
            /// Language ISO 639-1.
            /// </summary>
            /// <example>"en"</example>
            public const string Iso639Dash1 = "VARCHAR(2)";

            /// <summary>
            /// Language ISO 639-2.
            /// </summary>
            /// <example>"eng (T)"</example>
            public const string Iso639Dash2 = "VARCHAR(7)";
        }

        #endregion

        #region Other

        /// <summary>
        /// <see cref="string"/> with max-length 254.
        /// </summary>
        /// <seealso href="https://stackoverflow.com/questions/1199190/what-is-the-optimal-length-for-an-email-address-in-a-database"/>
        public const string Email = "VARCHAR(254)";

        /// <summary>
        /// <see cref="System.Enum"/> stored as an <see cref="Int"/>.
        /// </summary>
        public const string Enum = "INT";

        /// <summary>
        /// A GUID should never be longer than 68 characters because the hexadecimal version is at most 68 characters.
        /// If you use the string version then you should probably use <see cref="GuidString"/> instead.
        /// </summary>
        public const string GuidHex = "VARCHAR(68)";

        /// <summary>
        /// A string GUID should never be longer than 36 characters. If you need more then you should
        /// probably use <see cref="GuidHex"/> instead.
        /// </summary>
        public const string Guid = "VARCHAR(36)";

        /// <summary>
        /// <see cref="string"/> with max-length 100.
        /// </summary>
        public const string Name = "NVARCHAR(100)";

        /// <summary>
        /// <see cref="string"/> with max-length 1024.
        /// </summary>
        public const string Password = "NVARCHAR(1024)";

        /// <summary>
        /// <see cref="string"/> with max-length 2048.
        /// </summary>
        public const string Url = "VARCHAR(2048)";

        /// <summary>
        /// <see cref="string"/> with max-length 255.
        /// </summary>
        public const string Filename = "NVARCHAR(255)";

        #endregion
    }
}