using AED_BE.Models;

namespace AED_BE.DTO.RequestDto
{
    public class ReservationWithTrainInfo
    {
        public Reservation Reservation { get; set; }
        public Trains Train { get; set; }
    }
}
