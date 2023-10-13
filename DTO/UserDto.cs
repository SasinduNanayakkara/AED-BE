/**
 * @Author E.M.S.D. Ekanayake
 * @Created 10/3/2023
 * @Description Implement user Dto for access token
 **/using System;
namespace AED_BE.DTO
{
	public record UserDto( String id, String nic, String email, String role); //user dto
}

