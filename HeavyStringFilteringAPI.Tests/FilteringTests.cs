using HeavyStringFiltering.Application.Services;
using HeavyStringFiltering.Infrastructure.Services;

namespace HeavyStringFilteringAPI.Tests
{
    public class FilteringTests
    {
        [Fact]
        public void Filter_Multiple_Words_Success()
        {
            // Arrange
            string[] filterWords = new string[] { "apple", "banana", "orange" };
            string combinedText = "0apple1orange2apple3banana4";
            string expectedFilteredText = "01234";

            IFilteringService filterService = new LevenshteinFilteringService();

            // Act
            string actualFilteredText = filterService.Filter(combinedText, filterWords, 15);

            // Assert
            Assert.Equal(expectedFilteredText, actualFilteredText);
        }

        [Fact]
        public void Filter_Multiple_Words_No_Success()

        {
            // Arrange
            string[] filterWords = new string[] { "cat", "dog" };
            string combinedText = "0apple1orange2apple3banana4";
            string expectedFilteredText = "0apple1orange2apple3banana4";

            IFilteringService filterService = new LevenshteinFilteringService();

            // Act
            string actualFilteredText = filterService.Filter(combinedText, filterWords, 15);

            // Assert
            Assert.Equal(expectedFilteredText, actualFilteredText);
        }

        [Fact]
        public void Filter_With_Empty_Input()
        {
            // Arrange
            string[] filterWords = new string[] { "apple", "banana", "orange" };
            string combinedText = string.Empty;
            string expectedFilteredText = string.Empty; ;

            IFilteringService filterService = new LevenshteinFilteringService();

            // Act
            string actualFilteredText = filterService.Filter(combinedText, filterWords, 15);

            // Assert
            Assert.Equal(expectedFilteredText, actualFilteredText);
        }
    }
}