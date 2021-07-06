using System;

namespace App.Core.Models
{
    public class ResponseModel<T> where T : class
    {
        public DateTime Timestamp { get; set; }
        public bool IsError { get; set; }
        public string Description { get; set; }
        public T Result { get; set; }
    }
    public class BooleanResultDTO
    {
        public bool Success{ get; set; }
    }
    public class BooleanDescriptionResultDTO
    {
        public bool Success { get; set; }
        public string Description { get; set; }
    }
}
