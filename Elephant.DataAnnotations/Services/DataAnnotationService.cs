using System.Reflection;
using Elephant.DataAnnotations.Services.Interfaces;

namespace Elephant.DataAnnotations.Services
{
	/// <inheritdoc cref="IDataAnnotationService"/>
	public class DataAnnotationService : IDataAnnotationService
	{
		/// <inheritdoc/>
		public TObject ReplaceEmptyStringsWithNulls<TObject>(ref TObject objectToNull)
			where TObject : class
		{
			bool classHasAttribute = Attribute.GetCustomAttribute(typeof(TObject), typeof(IfEmptyMakeItNullAttribute)) != null;

			// Get properties.
			IEnumerable<PropertyInfo> properties = objectToNull.GetType().GetProperties();
			if (!classHasAttribute)
			{
				// The class itself doesn't have the attribute, therefore filter the properties by only those that have that attribute.
				properties = properties.Where(prop => Attribute.IsDefined(prop, typeof(IfEmptyMakeItNullAttribute)));
			}

			foreach (PropertyInfo property in properties)
			{
				object? propertyValue = property.GetValue(objectToNull);
				if (propertyValue == null)
					continue; // It's already null, therefor don't process it.

				Type propertyType = property.PropertyType;

				if (((propertyType == typeof(string) || propertyType == typeof(char)) && propertyValue.ToString() == string.Empty))
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(int?) && (int?)propertyValue == 0)
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(float?) && (float?)propertyValue >= -float.Epsilon && (float?)propertyValue <= float.Epsilon)
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(decimal?) && (decimal?)propertyValue == 0)
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(uint?) && (uint?)propertyValue == 0)
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(byte?) && (byte?)propertyValue == 0)
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(sbyte?) && (sbyte?)propertyValue == 0)
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(short?) && (short?)propertyValue == 0)
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(ushort?) && (ushort?)propertyValue == 0)
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(long?) && (long?)propertyValue == 0)
				{
					property.SetValue(objectToNull, null);
					continue;
				}

				if (propertyType == typeof(ulong?) && (ulong?)propertyValue == 0)
				{
					property.SetValue(objectToNull, null);
				}
			}

			return objectToNull;
		}
	}
}
