/**
 * @Author S.P Rupasinghe
 * @Created 10/4/2023
 * @Description Implement Employee Login request Dto
 **/
using System;
namespace AED_BE.DTO.RequestDto
{
    public record EmployeeLoginRequest(String email, String password); //employee login request dto
}

