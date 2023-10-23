using System;
namespace AED_BE.DTO.RequestDto
{
	public record TrainFilterRequest(String date, String startStation, String endStation);
}

