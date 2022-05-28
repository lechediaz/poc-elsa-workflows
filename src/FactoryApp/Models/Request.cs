using FactoryApp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactoryApp.Models
{
    public class Request
    {
        #region Fields
        public int Id { get; set; }

        [Required]
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedById { get; set; }
        public int ReceiverId { get; set; }
        public bool? Approved { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public bool? Rejected { get; set; }
        public DateTime? RejectedAt { get; set; }
        
        [MaxLength(300)]
        public string AproveLink { get; set; }

        [MaxLength(300)]
        public string RejectLink { get; set; }
        #endregion

        #region Relations
        public virtual User CreatedBy { get; set; }
        public virtual User Receiver { get; set; }

        public virtual ICollection<RequestDetail> Details { get; set; }
        #endregion
    }
}
