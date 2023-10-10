using AED_BE.Data;
using AED_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AED_BE.Services
{
    public class TrainService
    {
        private readonly IMongoCollection<Trains> _trainCollection;

        public TrainService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _trainCollection = mongoDb.GetCollection<Trains>("Trains");
        }

        public async Task CreateAsync(Trains newTrain) => await _trainCollection.InsertOneAsync(newTrain);

        public async Task<List<Trains>> GetAllTrains() => await _trainCollection.Find(_ => true).ToListAsync();

        public async Task<ActionResult<Trains>> GetOneTrain(string id) => await _trainCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Trains> GetTrainsByNumber(int trainNumber) => await _trainCollection.Find(x => x.TrainNo == trainNumber).FirstOrDefaultAsync();

        public async Task<List<Trains>> FilterTrains(string date) => await _trainCollection.Find(x => x.Date == date).ToListAsync();

        public async Task UpdateTrain(string id, Trains newTrain) => await _trainCollection.ReplaceOneAsync(x => x.Id == id, newTrain);

        public async Task DeleteTrain(string id) => await _trainCollection.DeleteOneAsync(x => x.Id == id);
    }
}
