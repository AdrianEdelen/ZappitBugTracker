using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZappitBugTracker.Models.Extensions;

namespace ZappitBugTracker.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public string FilePath { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".jpg", ".png" })]
        [MaxFileSize(2 * 1024 * 1024)]
        public IFormFile FormFile { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public byte[] FileData { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public string UserId { get; set; }
        public virtual BTUser User { get; set; }
    }
}
