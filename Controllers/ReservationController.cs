/**
 * @Author Perera M.R.A
 * @Created 30/9/2023
 * @Description Implement Client API Controllers
 **/
using AED_BE.Models;
using AED_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace AED_BE.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController: ControllerBase
    {
        private readonly ReservationService _reservationsService;

        public ReservationController(ReservationService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> Post(Reservation newReservation)
        {
            await _reservationsService.Create(newReservation);
            return CreatedAtAction(nameof(Get), new { id = newReservation.Id }, newReservation);
        }

        [HttpGet]
        public async Task<List<Reservation>> Get()
        {
            return await _reservationsService.GetAllReservation();
        }

        [HttpGet("{reservationId}")]
        public async Task<ActionResult<Reservation>> GetReservation(int number)
        {
            Reservation reservation = await _reservationsService.GetReservationByNumber(number);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Reservation updatedReservation)
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
        public async Task<ActionResult> Delete(string id)
        {
            Reservation reservation = await _reservationsService.GetReservation(id);
            if (reservation == null)
            {
                return NotFound();
            }
            await _reservationsService.DeleteAsync(id);
            return Ok(reservation);
        }
    }
}
