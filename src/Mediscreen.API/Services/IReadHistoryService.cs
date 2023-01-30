using MediscreenAPI.Model.Entities;

namespace MediscreenAPI.Services
{
    public interface IReadHistoryService
    {
        Task<History?> GetAsync(string id);
        Task<string[]> GetByPatIdAsync(int patId);
    }
}