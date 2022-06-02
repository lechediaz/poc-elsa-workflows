using System;
using FactoryApp.Enums;

namespace FactoryApp.Dtos
{
    public class UserRequestDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string Approver { get; set; }
        public DateTime CreatedAt { get; set; }
        public RequestStatus Status { get; set; }
        public int TotalItems { get; set; }
    }
}