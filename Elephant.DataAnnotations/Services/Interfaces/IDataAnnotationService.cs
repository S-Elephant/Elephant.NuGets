namespace Elephant.DataAnnotations.Services.Interfaces
{
	/// <summary>
	/// Data annotation service.
	/// </summary>
	public interface IDataAnnotationService
	{
		/// <summary>
		/// If this object or entity has any properties with the <see cref="IfEmptyMakeItNullAttribute"/>
		/// attribute then it will assign null values to them if they are empty.
		/// </summary>
		object ReplaceEmptyStringsWithNulls(ref object objectToNull);
	}
}
