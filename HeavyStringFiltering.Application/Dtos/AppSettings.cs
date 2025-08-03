namespace HeavyStringFiltering.Application.Dtos
{
    public class AppSettings
    {
        public int SimilarityThreshold { get; set; } = 80;
        public string[] WordFilters { get; set; } = [];
        public string FilterAlgorithm { get; set; } = string.Empty;
    }
}