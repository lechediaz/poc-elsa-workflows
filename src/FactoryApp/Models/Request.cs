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
        public int AuthorId { get; set; }
        public int ApproverId { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public DateTime? InNegociationAt { get; set; }
        public DateTime? InShipmentAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        [MaxLength(300)]
        public string ApproveLink { get; set; }

        [MaxLength(300)]
        public string RejectLink { get; set; }
        #endregion

        #region Relations
        public virtual User Author { get; set; }
        public virtual User Approver { get; set; }

        public virtual ICollection<RequestDetail> Details { get; set; }
        #endregion
    }
}
