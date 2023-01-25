using MediscreenAPI.Model;
using MediscreenAPI.Model.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MediscreenAPI.Services
{
    public class HistoryService
    {
        private readonly IMongoCollection<Note> _booksCollection;

        public HistoryService(IOptions<HistoryDatabaseSettings> historyDatabaseSettings)
        {
            MongoClient mongoClient = new(historyDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(historyDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<Note>(historyDatabaseSettings.Value.HistoryCollectionName);
        }

        public async Task<List<Note>> GetByPatIdAsync(int patId)
        {
            return await _booksCollection.Find(x => x.PatId == patId).ToListAsync();
        }

        public async Task<Note?> GetAsync(string id)
        {
            return await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Note newHistory)
        {
            await _booksCollection.InsertOneAsync(newHistory);
        }

        public async Task UpdateAsync(string id, Note updatedHistory)
        {
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedHistory);
        }

        public async Task RemoveAsync(string id)
        {
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
