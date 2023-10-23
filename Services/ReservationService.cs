/**
 * @Author M.R.A Perera
 * @Created 10/8/2023
 * @Description Implement Reservation service
 **/using AED_BE.Data;
using AED_BE.DTO.RequestDto;
using AED_BE.DTO.ResponseDto;
using AED_BE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AED_BE.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Trains> _trainCollection;

        public ReservationService(IOptions<DatabaseSettings> settings) //constructor
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _reservationCollection = mongoDb.GetCollection<Reservation>("Reservation");
        }

        public async Task Create(Reservation reservation) => await _reservationCollection.InsertOneAsync(reservation); //create reservation service

        public async Task<List<Reservation>> GetAllReservation() => await _reservationCollection.Find(_ => true).ToListAsync(); //get all reservation serivce

        public async Task<List<Reservation>> GetAllReservationByNIC(String nic)
        {
            return await _reservationCollection.Find(X => X.Client.NIC == nic).ToListAsync();

        }//get all reservation serivce

        public async Task<int> GetNextReservationID()
        {
            List<Reservation> reservations = await _reservationCollection.Find(_ => true).ToListAsync();
            int maxid = 0;
            for (int i = 0; i < reservations.Count; i++) {
                if (maxid < reservations[i].ReservationId) {
                    maxid = reservations[i].ReservationId;
                }
            }
            return maxid + 1;

        }//get all reservation serivce

        public async Task<Reservation> GetReservation(string id) => await _reservationCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); //get one reservation service

        public async Task<Reservation> GetReservationByNumber(int number) => await _reservationCollection.Find(x => x.ReservationId == number).FirstOrDefaultAsync(); //get reservation by number service

        public async Task UpdateAsync(string id, Reservation updatedReservation) => await _reservationCollection.ReplaceOneAsync(x => x.Id == id, updatedReservation); //update reservation service

        public async Task DeleteAsync(string id) => await _reservationCollection.DeleteOneAsync(x => x.Id == id); //delete reservation service

        public async Task<ReservationWithTrainInfo> GetReservationWithTrainInfoById(int reservationId)
        {
            var reservation = await _reservationCollection.Find(r => r.ReservationId == reservationId).FirstOrDefaultAsync();

            if (reservation != null)
            {
                var train = await _trainCollection.Find(t => t.TrainNo == reservation.Train.TrainNo).FirstOrDefaultAsync();

                if (train != null)
                {
                    return new ReservationWithTrainInfo
                    {
                        Reservation = reservation,
                        Train = train
                    };
                }
            }

            return null; // Reservation not found.
        }
        public async Task<List<Reservation>> GetPastReservations(string nic)
        {
            DateTime currentDate = DateTime.Now.Date;
            return await _reservationCollection.Find(x => DateTime.Parse(x.Date) < currentDate && x.Client.NIC == nic).ToListAsync(); //get past reservations by client nic
        }

        public async Task<List<Reservation>> GetCurrntReservations(string nic)
        {
            DateTime currentDate = DateTime.Now.Date;
            return await _reservationCollection.Find(x => DateTime.Parse(x.Date) >= currentDate && x.Client.NIC == nic).ToListAsync();//get current reservations by client nic
        }
    }
}
