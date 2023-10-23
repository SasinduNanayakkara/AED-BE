/**
 * @Author Perera M.R.A
 * @Created 30/9/2023
 * @Description Implement Client API Controllers
 **/
using AED_BE.DTO.RequestDto;
using AED_BE.Models;
using AED_BE.Services;
using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;

namespace AED_BE.Controllers
{
    [Route("api/reservation")] //Route
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationsService;
        private readonly ClientService _clientService;
        private readonly TrainService _trainService;

        public ReservationController(ReservationService reservationsService, ClientService clientService, TrainService trainService) //Constructor
        {
            _reservationsService = reservationsService;
            _clientService = clientService;
            _trainService = trainService;
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> Post(ReservationRequest req) //Create Reservation
        {
            Client _client  = await _clientService.GetClientAsync(req.nic);
            Trains _train = await _trainService.GetTrainsByNumber(req.trainNumber);
            List<Reservation> reservations_list_by_nic = await _reservationsService.GetAllReservationByNIC(req.nic);
            int nextReservationId = await _reservationsService.GetNextReservationID();


            if (reservations_list_by_nic.Count >= 4) {
                return Forbid();
            }

            if (_client != null && _train != null)
            {

                Reservation newReservation = new Reservation();
                newReservation.ReservationId = nextReservationId;
                newReservation.Date = DateTime.Parse(req.date).ToString("yyyy-MM-dd").ToString();
                newReservation.Client = _client;
                newReservation.Train = _train;
                await _reservationsService.Create(newReservation);
                return CreatedAtAction(nameof(Get), new { id = newReservation.Id }, newReservation);
            }
            else {
                return NotFound();
            }

        }

        [HttpGet]
        public async Task<List<Reservation>> Get() //Get all Reservations
        {
            return await _reservationsService.GetAllReservation();
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Reservation updatedReservation) //Update reservation
        {
            Reservation reservation = await _reservationsService.GetReservation(id);
            if (reservation == null)
            {
                return NotFound();
            }
            updatedReservation.Id = reservation.Id;
            await _reservationsService.UpdateAsync(id, updatedReservation);
            return Ok(reservation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id) //Delete reservation
        {
            Reservation reservation = await _reservationsService.GetReservation(id);
            if (reservation == null)
            {
                return NotFound();
            }
            await _reservationsService.DeleteAsync(id);
            return Ok(reservation);
        }

        [HttpGet("withtrain/{withTrainInfoById}")]
        public async Task<ActionResult<ReservationWithTrainInfo>> GetReservationWithTrainInfoById(int reservationId)
        {
            var reservationWithTrainInfo = await _reservationsService.GetReservationWithTrainInfoById(reservationId);

            if (reservationWithTrainInfo != null)
            {
                return Ok(reservationWithTrainInfo);
            }
            else
            {
                return NotFound(); // Reservation not found.
            }
        }

        [HttpGet("past/{nic}")]
        public async Task<List<Reservation>> GetPast(string nic)
        {
            return await _reservationsService.GetPastReservations(nic);
        }
        [HttpGet("current/{nic}")]
        public async Task<List<Reservation>> GetCurrent(string nic)
        {
            return await _reservationsService.GetCurrntReservations(nic);
        }
    }
}
