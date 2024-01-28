using System;

namespace Excellerent.SharedModules.DTO
{
    public enum ResponseStatus
    {
        Error,
        Success,
        Warning,
        Info
    }
    public class ResponseDTO
    {
        public ResponseStatus ResponseStatus { get; set; }
        public string Message { get; set; }
        public Exception Ex { get; set; }
        public dynamic Data { get; set; }

        public ResponseDTO(ResponseStatus status, string message, dynamic data)
        {
            ResponseStatus = status;
            Message = message;
            Data = data;
            Ex = null;
        }

        public ResponseDTO()
        {
        }
    }
}
