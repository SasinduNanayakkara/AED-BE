using AED_BE.Data;
using AED_BE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AED_BE.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;

        public ReservationService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _reservationCollection = mongoDb.GetCollection<Reservation>("Reservation");
        }

        public async Task Create(Reservation reservation) => await _reservationCollection.InsertOneAsync(reservation);

        public async Task<List<Reservation>> GetAllReservation() => await _reservationCollection.Find(_ => true).ToListAsync();

        public async Task<Reservation> GetReservation(string id) => await _reservationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Reservation> GetReservationByNumber(int number) => await _reservationCollection.Find(x => x.ReservationId == number).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Reservation updatedReservation) => await _reservationCollection.ReplaceOneAsync(x => x.Id == id, updatedReservation);

        public async Task DeleteAsync(string id) => await _reservationCollection.DeleteOneAsync(x => x.Id == id);
    }
}
