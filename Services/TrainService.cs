/**
 * @Author H.M.S.Y Nanayakkara
 * @Created 10/8/2023
 * @Description Implement Train service
 **/
using AED_BE.Data;
using AED_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Globalization;

namespace AED_BE.Services
{
    public class TrainService
    {
        private readonly IMongoCollection<Trains> _trainCollection;

        public TrainService(IOptions<DatabaseSettings> settings) //constructor
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _trainCollection = mongoDb.GetCollection<Trains>("Trains");
        }

        public TrainService()
        {
        }

        public async Task CreateAsync(Trains newTrain) => await _trainCollection.InsertOneAsync(newTrain); //create train service

        public async Task<List<Trains>> GetAllTrains() => await _trainCollection.Find(_ => true).ToListAsync(); //get all train service

        public async Task<List<Trains>> GetAllTrainsByDate(String date) {

            DateTime parsedDate = DateTime.Parse(date);
            String dateName = parsedDate.DayOfWeek.ToString();
            return await _trainCollection.Find(X => X.Date.Contains(dateName)).ToListAsync();
        }//get all trains by date

        public async Task<ActionResult<Trains>> GetOneTrain(string id) => await _trainCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); //get one train service

        public async Task<Trains> GetTrainsByNumber(int trainNumber) //get one reservation by number service
        {
            Trains trains = await _trainCollection.Find(x => x.TrainNo == trainNumber).FirstOrDefaultAsync();
            return trains;
        }

        //public async Task<List<Trains>> FilterTrains(string date) => await _trainCollection.Find(x => x.Date == date).ToListAsync();

        public async Task UpdateTrain(string id, Trains newTrain) => await _trainCollection.ReplaceOneAsync(x => x.Id == id, newTrain); //update reservation service

        public async Task DeleteTrain(string id) => await _trainCollection.DeleteOneAsync(x => x.Id == id); //delete reservation service
    }
}
