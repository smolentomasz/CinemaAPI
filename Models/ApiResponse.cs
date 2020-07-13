using Microsoft.AspNetCore.Mvc;

namespace Models
{
    public class ApiResponse
    {
        public string responseMessage {get;set;}

        public ApiResponse(string responseMessage, IActionResult responseResult){
            this.responseMessage = responseMessage;
        }
    }
}