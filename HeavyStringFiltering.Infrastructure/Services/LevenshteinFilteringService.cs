using HeavyStringFiltering.Application.Services;
using System.Text;

namespace HeavyStringFiltering.Infrastructure.Services
{
    public class LevenshteinFilteringService : IFilteringService
    {
        public const string FilterAlgorithm = "Levenshtein";

        public string Filter(string originalString, string[] filters, double threshold)
        {
            foreach (var filter in filters)
            {
                double similarity = CalculateSimilarity(originalString, filter);
                if (similarity >= threshold)
                {
                    // Create a StringBuilder from the original string
                    StringBuilder sb = new StringBuilder(originalString);

                    // Perform the replacement
                    sb.Replace(filter, string.Empty);

                    // Convert the StringBuilder back to a string
                    originalString = sb.ToString();
                }
            }
            return originalString;
        }

        private double CalculateSimilarity(string source, string target)
        {
            var distance = LevenshteinDistance(source, target);
            var maxLength = Math.Max(source.Length, target.Length);
            return (1.0 - (double)distance / maxLength) * 100;
        }

        private int LevenshteinDistance(string source, string target)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(target);

            var m = source.Length;
            var n = target.Length;
            var distance = new int[m + 1, n + 1];

            for (var i = 0; i <= m; i++)
            {
                distance[i, 0] = i;
            }

            for (var j = 0; j <= n; j++)
            {
                distance[0, j] = j;
            }

            for (var i = 1; i <= m; i++)
            {
                for (var j = 1; j <= n; j++)
                {
                    var cost = target[j - 1] == source[i - 1] ? 0 : 1;

                    var deletion = distance[i - 1, j] + 1;
                    var insertion = distance[i, j - 1] + 1;
                    var substitution = distance[i - 1, j - 1] + cost;

                    distance[i, j] = Math.Min(Math.Min(deletion, insertion), substitution);
                }
            }

            return distance[m, n];
        }
    }
}