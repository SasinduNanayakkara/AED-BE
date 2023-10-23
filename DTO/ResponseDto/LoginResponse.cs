namespace AED_BE.DTO.ResponseDto
{
    public class LoginResponse
    {
        public string accessToken { get; set; }
        public UserDto userDetails { get; set; }
    }
}
