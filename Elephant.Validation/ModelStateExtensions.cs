using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Elephant.Validation
{
	/// <summary>
	/// <see cref="ModelStateDictionary"/> extensions.
	/// </summary>
	public static class ModelStateExtensions
	{
		/// <summary>
		/// Returns all the errors from the specified <paramref name="modelStateDictionary"/> as a string.
		/// </summary>
		public static string ErrorsAsString(this ModelStateDictionary modelStateDictionary)
		{
			string result = string.Empty;
			foreach (ModelStateEntry modelState in modelStateDictionary.Values)
			{
				foreach (ModelError modelError in modelState.Errors)
					result += $"{modelError.ErrorMessage}\n";
			}

			if (result.Length > 0)
				_ = result.Remove(result.Length - 1);

			return result;
		}
	}
}
