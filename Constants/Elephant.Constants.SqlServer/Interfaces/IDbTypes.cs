namespace Elephant.Constants.SqlServer.Interfaces;

/// <inheritdoc cref="DbTypes"/>
public interface IDbTypes
{
	#region Numeric data types

	/// <inheritdoc cref="DbTypes.BigInt"/>
	string BigInt { get; }

	/// <inheritdoc cref="DbTypes.Bool"/>
	string Bool { get; }

	/// <inheritdoc cref="DbTypes.Decimal"/>
	string Decimal { get; }

	/// <inheritdoc cref="DbTypes.Double"/>
	string Double { get; }

	/// <inheritdoc cref="DbTypes.Float"/>
	string Float { get; }

	/// <inheritdoc cref="DbTypes.Float4"/>
	string Float4 { get; }

	/// <inheritdoc cref="DbTypes.Int"/>
	string Int { get; }

	/// <inheritdoc cref="DbTypes.Money"/>
	string Money { get; }

	/// <inheritdoc cref="DbTypes.SmallInt"/>
	string SmallInt { get; }

	/// <inheritdoc cref="DbTypes.SmallMoney"/>
	string SmallMoney { get; }

	/// <inheritdoc cref="DbTypes.TinyInt"/>
	string TinyInt { get; }

	/// <inheritdoc cref="DbTypes.Real"/>
	string Real { get; }

	#endregion

	#region Date and datetime datatypes

	/// <inheritdoc cref="DbTypes.Date"/>
	string Date { get; }

	/// <inheritdoc cref="DbTypes.DateTime"/>
	string DateTime { get; }

	/// <inheritdoc cref="DbTypes.DateTime2"/>
	string DateTime2 { get; }

	/// <inheritdoc cref="DbTypes.DateTimeOffset"/>
	string DateTimeOffset { get; }

	/// <inheritdoc cref="DbTypes.SmallDateTime"/>
	string SmallDateTime { get; }

	/// <inheritdoc cref="DbTypes.Time"/>
	string Time { get; }

	/// <inheritdoc cref="DbTypes.Timestamp"/>
	string Timestamp { get; }

	#endregion

	#region Spatial types

	/// <inheritdoc cref="DbTypes.Geography"/>
	string Geography { get; }

	/// <inheritdoc cref="DbTypes.Geometry"/>
	string Geometry { get; }

	#endregion

	#region String data types

	/// <inheritdoc cref="DbTypes.NText"/>
	string NText { get; }

	/// <inheritdoc cref="DbTypes.NVarCharMax"/>
	string NVarCharMax { get; }

	/// <inheritdoc cref="DbTypes.Text"/>
	string Text { get; }

	/// <inheritdoc cref="DbTypes.VarCharMax"/>
	string VarCharMax { get; }

	#endregion

	#region Language

	/// <inheritdoc cref="DbTypes.Language.Iso639Dash1"/>
	string LanguageIso639Dash1 { get; }

	/// <inheritdoc cref="DbTypes.Language.Iso639Dash2"/>
	string LanguageIso639Dash2 { get; }

	#endregion

	#region File and folder

	/// <inheritdoc cref="DbTypes.Filename"/>
	string Filename { get; }

	/// <inheritdoc cref="DbTypes.FolderPath"/>
	string FolderPath { get; }

	/// <inheritdoc cref="DbTypes.FolderPathLinux"/>
	string FolderPathLinux { get; }

	#endregion

	#region Other

	/// <inheritdoc cref="DbTypes.Guid"/>
	string Guid { get; }

	/// <inheritdoc cref="DbTypes.GuidString"/>
	string GuidString { get; }

	/// <inheritdoc cref="DbTypes.GuidHex"/>
	string GuidHex { get; }

	/// <inheritdoc cref="DbTypes.Email"/>
	string Email { get; }

	/// <inheritdoc cref="DbTypes.Enum"/>
	string Enum { get; }

	/// <inheritdoc cref="DbTypes.IpAddressBinary"/>
	string IpAddressBinary { get; }

	/// <inheritdoc cref="DbTypes.IpAddressString"/>
	string IpAddressString { get; }

	/// <inheritdoc cref="DbTypes.Mime"/>
	string Mime { get; }

	/// <inheritdoc cref="DbTypes.Name"/>
	string Name { get; }

	/// <inheritdoc cref="DbTypes.Password"/>
	string Password { get; }

	/// <inheritdoc cref="DbTypes.PhoneNumberInternational"/>
	string PhoneNumberInternational { get; }

	/// <inheritdoc cref="DbTypes.PhoneNumberNetherlands"/>
	string PhoneNumberNetherlands { get; }

	/// <inheritdoc cref="DbTypes.SqlVariant"/>
	string SqlVariant { get; }

	/// <inheritdoc cref="DbTypes.Url"/>
	string Url { get; }

	/// <inheritdoc cref="DbTypes.Xml"/>
	string Xml { get; }

	#endregion
}