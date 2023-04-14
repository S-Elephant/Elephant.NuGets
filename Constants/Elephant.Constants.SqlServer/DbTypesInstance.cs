using Elephant.Constants.SqlServer.Interfaces;

namespace Elephant.Constants.SqlServer;

/// <inheritdoc cref="IDbTypes"/>
public class DbTypesInstance : IDbTypes
{
	#region Numeric data types

	/// <inheritdoc/>
	public virtual string BigInt => DbTypes.BigInt;

	/// <inheritdoc/>
	public virtual string Bool => DbTypes.Bool;

	/// <inheritdoc/>
	public virtual string Decimal => DbTypes.Decimal;

	/// <inheritdoc/>
	public virtual string Float => DbTypes.Float;

	/// <inheritdoc/>
	public virtual string Float4 => DbTypes.Float4;

	/// <inheritdoc/>
	public virtual string Int => DbTypes.Int;

	/// <inheritdoc/>
	public virtual string Money => DbTypes.Money;

	/// <inheritdoc/>
	public virtual string SmallInt => DbTypes.SmallInt;

	/// <inheritdoc/>
	public virtual string SmallMoney => DbTypes.SmallMoney;

	/// <inheritdoc/>
	public virtual string TinyInt => DbTypes.TinyInt;

	/// <inheritdoc/>
	public virtual string Real => DbTypes.Real;

	#endregion

	#region Date and datetime datatypes

	/// <inheritdoc/>
	public virtual string Date => DbTypes.Date;

	/// <inheritdoc/>
	public virtual string DateTime => DbTypes.DateTime;

	/// <inheritdoc/>
	public virtual string DateTime2 => DbTypes.DateTime2;

	/// <inheritdoc/>
	public virtual string DateTimeOffset => DbTypes.DateTimeOffset;

	/// <inheritdoc/>
	public virtual string SmallDateTime => DbTypes.SmallDateTime;

	/// <inheritdoc/>
	public virtual string Time => DbTypes.Time;

	/// <inheritdoc/>
	public virtual string Timestamp => DbTypes.Timestamp;

	#endregion

	#region Spatial types

	/// <inheritdoc/>
	public virtual string Geography => DbTypes.Geography;

	/// <inheritdoc/>
	public virtual string Geometry => DbTypes.Geometry;

	#endregion

	#region String data types

	/// <inheritdoc/>
	public virtual string NText => DbTypes.NText;

	/// <inheritdoc/>
	public virtual string NVarCharMax => DbTypes.NVarCharMax;

	/// <inheritdoc/>
	public virtual string Text => DbTypes.Text;

	/// <inheritdoc/>
	public virtual string VarCharMax => DbTypes.VarCharMax;

	#endregion

	#region Language

	/// <inheritdoc/>
	public virtual string LanguageIso639Dash1 => DbTypes.Language.Iso639Dash1;

	/// <inheritdoc/>
	public virtual string LanguageIso639Dash2 => DbTypes.Language.Iso639Dash2;

	#endregion

	#region File and folder

	/// <inheritdoc/>
	public virtual string Filename => DbTypes.Filename;

	/// <inheritdoc/>
	public virtual string FolderPath => DbTypes.FolderPath;

	/// <inheritdoc/>
	public virtual string FolderPathLinux => DbTypes.FolderPathLinux;

	#endregion

	#region Other

	/// <inheritdoc/>
	public virtual string Guid => DbTypes.Guid;

	/// <inheritdoc/>
	public virtual string GuidString => DbTypes.GuidString;

	/// <inheritdoc/>
	public virtual string GuidHex => DbTypes.GuidHex;

	/// <inheritdoc/>
	public virtual string Email => DbTypes.Email;

	/// <inheritdoc/>
	public virtual string Enum => DbTypes.Enum;

	/// <inheritdoc/>
	public virtual string IpAddressBinary => DbTypes.IpAddressBinary;

	/// <inheritdoc/>
	public virtual string IpAddressString => DbTypes.IpAddressString;

	/// <inheritdoc/>
	public virtual string Mime => DbTypes.Mime;

	/// <inheritdoc/>
	public virtual string Name => DbTypes.Name;

	/// <inheritdoc/>
	public virtual string Password => DbTypes.Password;

	/// <inheritdoc/>
	public virtual string PhoneNumberInternational => DbTypes.PhoneNumberInternational;

	/// <inheritdoc/>
	public virtual string PhoneNumberNetherlands => DbTypes.PhoneNumberNetherlands;

	/// <inheritdoc/>
	public virtual string SqlVariant => DbTypes.SqlVariant;

	/// <inheritdoc/>
	public virtual string Url => DbTypes.Url;

	/// <inheritdoc/>
	public virtual string Xml => DbTypes.Xml;

	#endregion
}