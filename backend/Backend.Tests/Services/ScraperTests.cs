using Backend.Services;

namespace Backend.Tests.Services
{
    public class ScraperTests
    {

        [Theory]
        [InlineData("123", 123)]
        [InlineData("0", 0)]
        [InlineData("456", 456)]
        public void ParseInt_ValidString_ReturnsParsedInt(string input, int expected)
        {
            // Act
            int result = Scraper.ParseInt(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("-", 0)]
        [InlineData("abc", 0)]
        public void ParseInt_InvalidString_ReturnsZero(string input, int expected)
        {
            // Act
            int result = Scraper.ParseInt(input);

            // Assert
            Assert.Equal(expected, result);
        }
        [Theory]
        [InlineData("123.45", 123.45)]
        [InlineData("0", 0)]
        [InlineData("456.78", 456.78)]
        public void ParseDecimal_ValidString_ReturnsParsedDecimal(string input, decimal expected)
        {
            // Act
            decimal result = Scraper.ParseDecimal(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("-", 0)]
        [InlineData("abc", 0)]
        [InlineData("123.abc", 0)]
        public void ParseDecimal_InvalidString_ReturnsZero(string input, decimal expected)
        {
            // Act
            decimal result = Scraper.ParseDecimal(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
