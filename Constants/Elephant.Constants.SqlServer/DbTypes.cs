using System.Diagnostics.CodeAnalysis;

namespace Elephant.Constants.SqlServer;

/// <summary>
/// Microsoft Sequel Server database constant types plus a few extras.
/// </summary>
public static class DbTypes
{
	#region Numeric data types

	/// <summary>
	/// Allows whole numbers between -9,223,372,036,854,775,808 and 9,223,372,036,854,775,807
	/// Storage: 8 bytes.
	/// </summary>
	public const string BigInt = "BIGINT";

	/// <summary>
	/// Integer that can be 0, 1, or NULL.
	/// Storage: 1 byte.
	/// </summary>
	public const string Bool = "BIT";

	/// <summary>
	/// <see cref="Decimal"/> 12,4.
	/// </summary>
	public static readonly string Decimal = $"DECIMAL({DbLengths.DecimalPrecision},{DbLengths.DecimalScale})";

	/// <summary>
	/// Precision: 15 digits (number data from -1.79E + 308 to 1.79E + 308).
	/// Is an approximate data type; therefore, not all values in the data type range can be represented exactly.
	/// Storage: 8 bytes.
	/// For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/float-and-real-transact-sql?view=sql-server-ver16.
	/// </summary>
	public static readonly string Float = $"FLOAT({DbLengths.Float})";

	/// <summary>
	/// <see cref="float"/>. Precision: 7 digits .
	/// The n parameter indicates whether the field should hold 4 or 8 bytes. float (24) holds a 4-byte field and float (53) holds an 8-byte field.Default value of n is 53.
	/// Is an approximate data type; therefore, not all values in the data type range can be represented exactly.
	/// Storage: 4 bytes.
	/// For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/float-and-real-transact-sql?view=sql-server-ver16.
	/// </summary>
	public static readonly string Float4 = $"FLOAT({DbLengths.Float4})";

	/// <summary>
	/// Allows whole numbers between -2,147,483,648 and 2,147,483,647.
	/// Storage: 4 bytes.
	/// </summary>
	[SuppressMessage("Microsoft.StyleCop.CSharp.OrderingRules", "SA1203:ConstantsShouldAppearBeforeFields", Justification = "Group related items for clarity.")]
	public const string Int = "INT";

	/// <summary>
	/// Monetary data from -922,337,203,685,477.5808 to 922,337,203,685,477.5807.
	/// Storage: 8 bytes.
	/// </summary>
	public const string Money = "MONEY";

	/// <summary>
	/// Small integer. Signed range is from -32768 to 32767.
	/// Unsigned range is from 0 to 65535. The size parameter specifies the maximum display width (which is 255).
	/// Storage: 2 bytes.
	/// </summary>
	public const string SmallInt = "SMALLINT";

	/// <summary>
	/// Monetary data from -214,748.3648 to 214,748.3647.
	/// Storage: 4 bytes.
	/// </summary>
	public const string SmallMoney = "SMALLMONEY";

	/// <summary>
	/// Tiny integer (range from 0 to 255 (inclusive)).
	/// Storage: 1 byte.
	/// </summary>
	public const string TinyInt = "TINYINT";

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
	/// Store a date only. From January 1, 0001 to December 31, 9999.
	/// Storage: 3 bytes.
	/// </summary>
	public const string Date = "DATE";

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
	/// The same as <see cref="DateTime2"/> with the addition of a time zone offset.
	/// Storage: 8-10 bytes.
	/// </summary>
	public const string DateTimeOffset = "DATETIMEOFFSET";

	/// <summary>
	/// From January 1, 1900 to through June 6, 2079.
	/// Storage: 4 bytes.
	/// </summary>
	public const string SmallDateTime = "SMALLDATETIME";

	/// <summary>
	/// Store a time only to an accuracy of 100 nanoseconds.
	/// Storage: 5 bytes, fixed, is the default with the default of 100ns fractional second precision.
	/// In Informatica, the default is 4 bytes, fixed, with the default of 1ms fractional second precision.
	/// </summary>
	public const string Time = "TIME";

	/// <summary>
	/// Stores a unique number that gets updated every time a row gets created or modified.
	/// The <see cref="Timestamp"/> value is based upon an internal clock and does not correspond
	/// to real time. Each table may have only one timestamp variable/column.
	/// Storage: 4 bytes.
	/// </summary>
	public const string Timestamp = "TIMESTAMP";

	#endregion

	#region Spatial types

	/// <summary>
	/// For spatial geography data.
	/// </summary>
	/// <remarks>For more info see: https://learn.microsoft.com/en-us/sql/t-sql/spatial-geography/spatial-types-geography?view=sql-server-ver16.</remarks>
	public const string Geography = "GEOGRAPHY";

	/// <summary>
	/// For spatial geometry data.
	/// </summary>
	/// <remarks>For more info see: https://learn.microsoft.com/en-us/sql/t-sql/spatial-geometry/spatial-types-geometry-transact-sql?view=sql-server-ver16.</remarks>
	public const string Geometry = "GEOMETRY";

	#endregion

	#region String data types

	/// <summary>
	/// NText. Can store up to 2GB of text data.
	/// </summary>
	/// <remarks>For more info see: https://docs.microsoft.com/en-us/sql/t-sql/data-types/ntext-text-and-image-transact-sql?view=sql-server-ver16.</remarks>
	public const string NText = "NTEXT";

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

	/// <summary>
	/// Variable length string without support for Unicode data.
	/// Storage: Variable 2 bytes + number of chars but up to 2³¹-1 bytes.
	/// Will attempt to store the data directly in the row unless it exceeds
	/// the 8k limitation and at that point it stores it in a blob.
	/// </summary>
	public const string VarCharMax = "VARCHAR(MAX)";

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
		public static readonly string Iso639Dash1 = string.Format("VARCHAR({0})", DbLengths.Language.Iso639Dash1);

		/// <summary>
		/// Language ISO 639-2.
		/// </summary>
		/// <example>"eng (T)"</example>
		public static readonly string Iso639Dash2 = string.Format("VARCHAR({0})", DbLengths.Language.Iso639Dash2);
	}

	#endregion

	#region File and folder

	/// <summary>
	/// <see cref="string"/> with max-length 255.
	/// </summary>
	public static readonly string Filename = $"NVARCHAR({DbLengths.Filename})";

	/// <summary>
	/// <see cref="string"/> with max-length 248 (248 character is the Windows limit, for Linux it's 4096 bytes).
	/// </summary>
	public static readonly string FolderPath = $"NVARCHAR({DbLengths.FolderPath})";

	/// <summary>
	/// <see cref="string"/> with max-length 248 (248 character is the Windows limit, for Linux it's 4096 bytes).
	/// </summary>
	public static readonly string FolderPathLinux = $"NVARCHAR({DbLengths.FolderPathLinux})";

	#endregion

	#region Other

	/// <summary>
	/// An actual GUID. If you need a string GUID instead then you should probably use either <see cref="GuidHex"/> or <see cref="GuidString"/> instead.
	/// This type of GUID has much better performance compared to string GUIDs.
	/// </summary>
	public static readonly string Guid = $"UNIQUEIDENTIFIER";

	/// <summary>
	/// A string GUID should never be longer than 36 characters. If you need more then you should
	/// probably use <see cref="GuidHex"/> instead.
	/// </summary>
	public static readonly string GuidString = $"VARCHAR({DbLengths.Guid})";

	/// <summary>
	/// A GUID should never be longer than 68 characters because the hexadecimal version is at most 68 characters.
	/// If you use the string version then you should probably use <see cref="GuidString"/> instead.
	/// </summary>
	public static readonly string GuidHex = $"VARCHAR({DbLengths.GuidHex})";

	/// <summary>
	/// <see cref="string"/> with max-length 254.
	/// </summary>
	/// <seealso href="https://stackoverflow.com/questions/1199190/what-is-the-optimal-length-for-an-email-address-in-a-database"/>
	public static readonly string Email = $"VARCHAR({DbLengths.Email})";

	/// <summary>
	/// <see cref="System.Enum"/> stored as an <see cref="Int"/>.
	/// </summary>
	[SuppressMessage("Microsoft.StyleCop.CSharp.OrderingRules", "SA1203:ConstantsShouldAppearBeforeFields", Justification = "Group related items for clarity.")]
	public const string Enum = "INT";

	/// <summary>
	/// IP Address as a varbinary(16).
	/// </summary>
	/// <remarks>For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/binary-and-varbinary-transact-sql?view=sql-server-ver16.</remarks>
	public static readonly string IpAddressBinary = $"VARBINARY({DbLengths.IpAddressBinary})";

	/// <summary>
	/// IP Address (as string) of 45 characters.
	/// </summary>
	/// <example>ABCD:ABCD:ABCD:ABCD:ABCD:ABCD:192.168.158.100</example>
	/// <remarks>For more info see: https://stackoverflow.com/questions/1076714/max-length-for-client-ip-address.</remarks>
	public static readonly string IpAddressString = $"VARCHAR({DbLengths.IpAddressString})";

	/// <summary>
	/// MIME type.
	/// </summary>
	/// <remarks>For more info see: https://stackoverflow.com/questions/643690/maximum-mimetype-length-when-storing-type-in-db.</remarks>
	[SuppressMessage("Microsoft.StyleCop.CSharp.OrderingRules", "SA1203:ConstantsShouldAppearBeforeFields", Justification = "Group related items for clarity.")]
	public const string Mime = "VARCHAR(255)";

	/// <summary>
	/// <see cref="string"/> with max-length 100.
	/// </summary>
	public static readonly string Name = $"NVARCHAR({DbLengths.Name})";

	/// <summary>
	/// <see cref="string"/> with max-length 1024.
	/// </summary>
	public static readonly string Password = $"NVARCHAR({DbLengths.Password})";

	/// <summary>
	/// Supports all international phone numbers.
	/// Includes space for country code.
	/// Does NOT include space for: plus, dash, parenthesis, spaces, 00 country code prefix.
	/// </summary>
	/// <remarks>For more info see: https://stackoverflow.com/questions/723587/whats-the-longest-possible-worldwide-phone-number-i-should-consider-in-sql-varc.</remarks>
	[SuppressMessage("Microsoft.StyleCop.CSharp.OrderingRules", "SA1203:ConstantsShouldAppearBeforeFields", Justification = "Group related items for clarity.")]
	public const string PhoneNumberInternational = "VARCHAR(15)";

	/// <summary>
	/// Supports all Dutch phone numbers.
	/// Includes space for country code.
	/// Does NOT include space for: plus, dash, parenthesis, spaces, 00 country code prefix.
	/// </summary>
	/// <remarks>For more info see: https://stackoverflow.com/questions/723587/whats-the-longest-possible-worldwide-phone-number-i-should-consider-in-sql-varc.</remarks>
	/// <example>06-12345678 using the country code +31, can be stored as: 31612345678</example>
	public const string PhoneNumberNetherlands = "VARCHAR(11)";

	/// <summary>
	/// SQL Variant.
	/// </summary>
	/// <remarks>For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/sql-variant-transact-sql?view=sql-server-ver16.</remarks>
	public const string SqlVariant = "SQL_VARIANT";

	/// <summary>
	/// <see cref="string"/> with max-length 2048.
	/// </summary>
	public const string Url = "VARCHAR(2048)";

	/// <summary>
	/// <see cref="string"/> with max-length 2048.
	/// </summary>
	/// <remarks>For more info see: https://learn.microsoft.com/en-us/sql/t-sql/xml/xml-transact-sql?view=sql-server-ver16.</remarks>
	public const string Xml = "XML";

	#endregion

}