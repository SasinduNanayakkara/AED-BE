
using System;
using AED_BE.Models;
namespace AED_BE.DTO.RequestDto
{
	public record ReservationRequest(String nic, int trainNumber, String date, Stations startStation, Stations endStation);
}

