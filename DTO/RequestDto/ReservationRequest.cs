
using System;
namespace AED_BE.DTO.RequestDto
{
	public record ReservationRequest(String nic, int trainNumber, String date);
}

