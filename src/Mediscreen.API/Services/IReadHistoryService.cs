using MediscreenAPI.Model.Entities;

namespace MediscreenAPI.Services
{
    public interface IReadHistoryService
    {
        Task<History?> GetAsync(string id);
        Task<List<History>> GetByPatIdAsync(int patId);
    }
}