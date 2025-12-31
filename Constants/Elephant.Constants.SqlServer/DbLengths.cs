namespace Elephant.Constants.SqlServer
{
	/// <summary>
	/// Database length constants.
	/// </summary>
	public static class DbLengths
	{
		#region Numeric data types

		/// <summary>
		/// <see cref="decimal"/> 12,4.
		/// </summary>
		public const int DecimalPrecision = 12;

		/// <summary>
		/// <see cref="decimal"/> 12,4.
		/// </summary>
		public const int DecimalScale = 4;

		/// <summary>
		/// Precision: 15 digits (number data from -1.79E + 308 to 1.79E + 308).
		/// Is an approximate data type; therefore, not all values in the data type range can be represented exactly.
		/// Storage: 8 bytes.
		/// For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/float-and-real-transact-sql?view=sql-server-ver16.
		/// </summary>
		/// <remarks>This is the same as the float(53).</remarks>
		public const int Double = 53;

		/// <summary>
		/// Precision: 15 digits (number data from -1.79E + 308 to 1.79E + 308).
		/// Is an approximate data type; therefore, not all values in the data type range can be represented exactly.
		/// Storage: 8 bytes.
		/// For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/float-and-real-transact-sql?view=sql-server-ver16.
		/// </summary>
		public const int Float = 53;

		/// <summary>
		/// <see cref="float"/>. Precision: 7 digits .
		/// The n parameter indicates whether the field should hold 4 or 8 bytes. float (24) holds a 4-byte field and float (53) holds an 8-byte field.Default value of n is 53.
		/// Is an approximate data type; therefore, not all values in the data type range can be represented exactly.
		/// Storage: 4 bytes.
		/// For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/float-and-real-transact-sql?view=sql-server-ver16.
		/// </summary>
		public const int Float4 = 24;

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
			public const int Iso639Dash1 = 2;

			/// <summary>
			/// Language ISO 639-2.
			/// </summary>
			/// <example>"eng (T)"</example>
			public const int Iso639Dash2 = 7;
		}

		#endregion

		#region File and folder

		/// <summary>
		/// Max filename length (for both Windows and Linux).
		/// </summary>
		public const int Filename = 255;

		/// <summary>
		/// 248 character is the Windows limit, for Linux it's 4096 bytes.
		/// </summary>
		public const int FolderPath = 248;

		/// <summary>
		/// 248 character is the Windows limit, for Linux it's 4096 bytes.
		/// </summary>
		public const int FolderPathLinux = 4096;

		#endregion

		#region Other

		/// <summary>
		/// A string GUID should never be longer than 36 characters. If you need more then you should
		/// probably use <see cref="GuidHex"/> instead.
		/// </summary>
		public const int Guid = 36;

		/// <summary>
		/// A GUID should never be longer than 68 characters because the hexadecimal version is at most 68 characters.
		/// If you use the string version then you should probably use <see cref="Guid"/> instead.
		/// </summary>
		public const int GuidHex = 68;

		/// <summary>
		/// Maximum e-mail length.
		/// </summary>
		/// <remarks>For more info see: https://stackoverflow.com/questions/1199190/what-is-the-optimal-length-for-an-email-address-in-a-database.</remarks>
		public const int Email = 254;

		/// <summary>
		/// IP Address as a varbinary(16).
		/// </summary>
		/// <remarks>For more info see: https://learn.microsoft.com/en-us/sql/t-sql/data-types/binary-and-varbinary-transact-sql?view=sql-server-ver16.</remarks>
		public const int IpAddressBinary = 16;

		/// <summary>
		/// IP Address (as string).
		/// </summary>
		/// <example>ABCD:ABCD:ABCD:ABCD:ABCD:ABCD:192.168.158.100</example>
		/// <remarks>For more info see: https://stackoverflow.com/questions/1076714/max-length-for-client-ip-address.</remarks>
		public const int IpAddressString = 45;

		/// <summary>
		/// Maximum name length.
		/// </summary>
		public const int Name = 100;

		/// <summary>
		/// Maximum password length.
		/// </summary>
		public const int Password = 1024;

		/// <summary>
		/// Maximum URL length is 2048.
		/// </summary>
		public const int Url = 2048;

		#endregion
	}
}