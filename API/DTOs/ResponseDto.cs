namespace API.DTOs
{
    public class ResponseDto
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}