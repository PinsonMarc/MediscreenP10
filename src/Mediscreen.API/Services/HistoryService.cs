﻿using MediscreenAPI.Model;
using MediscreenAPI.Model.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MediscreenAPI.Services
{
    public class HistoryService
    {
        private readonly IMongoCollection<History> _booksCollection;

        public HistoryService(IOptions<HistoryDatabaseSettings> historyDatabaseSettings)
        {
            MongoClient mongoClient = new(historyDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(historyDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<History>(historyDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<History>> GetAsync()
        {
            return await _booksCollection.Find(_ => true).ToListAsync();
        }

        public async Task<History?> GetAsync(string id)
        {
            return await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(History newHistory)
        {
            await _booksCollection.InsertOneAsync(newHistory);
        }

        public async Task UpdateAsync(string id, History updatedHistory)
        {
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedHistory);
        }

        public async Task RemoveAsync(string id)
        {
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
