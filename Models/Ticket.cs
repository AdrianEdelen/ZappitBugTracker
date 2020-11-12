using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZappitBugTracker.Models
{
    public class Ticket
    {
        public Ticket()
        {
            Comments = new HashSet<TicketComment>();
            Attachments = new HashSet<TicketAttachment>();
            Notifications = new HashSet<Notification>();
            Histories = new HashSet<TicketHistory>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset? Updated { get; set; }

        public int ProjectId { get; set; }

        [Display(Name = "Category")]
        public int TicketTypeId { get; set; }

        [Display(Name = "Priority")]
        public int TicketPriorityId { get; set; }

        public int TicketStatusId { get; set; }

        public string OwnerUserId { get; set; }

        public string DeveloperUserId { get; set; }

        public Project Project { get; set; }

        [Display(Name = "Category")]
        public TicketType TicketType { get; set; }

        [Display(Name = "Priority")]
        public TicketPriority TicketPriority { get; set; }

        [Display(Name = "Ticket Status")]
        public TicketStatus TicketStatus { get; set; }

        [Display(Name = "Ticket Creator")]
        public BTUser OwnerUser { get; set; }
        [Display(Name = "Assigned Developer(s)")]
        public BTUser DeveloperUser { get; set; }

        public virtual ICollection<TicketComment> Comments { get; set; }

        public virtual ICollection<TicketAttachment> Attachments { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public virtual ICollection<TicketHistory> Histories { get; set; }
        
        public Dictionary<string, dynamic> GetPropertiesForCompare()
        {
            return new Dictionary<string, dynamic>()
            {
                {"Title", Title },
                {"Description", Description },
                {"TicketTypeId", TicketTypeId },
                {"TicketStatusId", TicketStatusId },
                {"TicketPriorityId", TicketPriorityId },
                {"DeveloperUserId", DeveloperUserId }
            };
        }
    }
}
