/**
 * @Author E.M.S.D. Ekanayake
 * @Created 10/1/2023
 * @Description Implement Client Request Dto
 **/
using System;
namespace AED_BE.DTO.RequestDto
{
	public record ClientLoginRequest(String nic, String password);
}

