namespace HeavyStringFiltering.Application.Services
{
    public interface IFilteringService
    {
        string Filter(string orginal, string[] filters, double threshold);
    }
}