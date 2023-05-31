using System.Reflection;
using Elephant.DataAnnotations.Services.Interfaces;

namespace Elephant.DataAnnotations.Services
{
	/// <inheritdoc cref="IDataAnnotationService"/>
	public class DataAnnotationService : IDataAnnotationService
	{
		/// <inheritdoc/>
		public object ReplaceEmptyStringsWithNulls(ref object objectToNull)
		{
			IEnumerable<PropertyInfo> properties = objectToNull.GetType().GetProperties()
				.Where(prop => Attribute.IsDefined(prop, typeof(IfEmptyMakeItNullAttribute)));
			foreach (PropertyInfo property in properties)
			{
				object? propertyValue = property.GetValue(objectToNull);
				if (propertyValue == null)
					continue; // It's already null, therefor don't process it.

				property.SetValue(objectToNull, null);
			}

			return objectToNull;
		}
	}
}
