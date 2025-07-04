namespace Elephant.Texts.Tests
{
	/// <summary>
	/// <see cref="Palindrome"/> tests.
	/// </summary>
	public class PalindromeTests
	{
		/// <summary>
		/// System under test.
		/// </summary>
		private static readonly IPalindrome _palindrome = new Palindrome();

		/// <summary>
		/// <see cref="Palindrome.IsValid(string?)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(null, true)]
		[InlineData("", true)]
		[InlineData(".", true)]
		[InlineData("A man, a plan, a canal: Panama", true)]
		[InlineData("race a car", false)]
		[InlineData("No 'x' in Nixon", true)]
		[InlineData("Sit on a potato pan, Otis.", true)]
		[InlineData("Cigar? Toss it in a can. It is so tragic.", true)]
		[InlineData("Taco cat", true)]
		[InlineData("No lemon, no melon.", true)]
		[InlineData("Gert, I saw Ron avoid a radio-van, or was it Reg?", true)]
		[InlineData("No, it can, as it is, it is a war. Raw as it is, it is an action.", true)]
		[InlineData("Was it Eliot's toilet I saw?", true)]
		[InlineData("pikachu", false)]
		[InlineData("pikachuuhcakip", true)]
		[InlineData("Racecar", true)]
		[InlineData("hello", false)]
		[InlineData("a", true)]
		[InlineData("Abba", true)]
		[InlineData("Radar", true)]
		[InlineData("radar", true)]
		[InlineData("RADAR", true)]
		[InlineData("¥Radar", true)]
		[InlineData("radar.", true)]
		[InlineData("ra dar.", true)]
		public void IsValidPalindromeTests(string input, bool expected)
		{
			Assert.Equal(expected, _palindrome.IsValid(input));
		}
	}
}
