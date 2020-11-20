using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZappitBugTracker.Models.Extensions;

namespace ZappitBugTracker.Models
{
    public class Project
    {

        public Project()
        {
            Tickets = new HashSet<Ticket>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Display(Name = "Project Image")]
        public string ImagePath { get; set; }

        [DisplayName("Upload Project Logo")]
        [DataType(DataType.Upload)]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public byte[] ImageData { get; set; }

        public List<ProjectUser> ProjectUsers { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
