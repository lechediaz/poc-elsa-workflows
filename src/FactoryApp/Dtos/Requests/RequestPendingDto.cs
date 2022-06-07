using System;
using FactoryApp.Enums;

namespace FactoryApp.Dtos
{
    public class RequestPendingDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Approver { get; set; }
        public DateTime CreatedAt { get; set; }
        public RequestStatus Status { get; set; }
        public int TotalItems { get; set; }
    }
}