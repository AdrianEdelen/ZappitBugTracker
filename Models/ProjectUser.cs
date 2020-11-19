namespace ZappitBugTracker.Models
{
    public class ProjectUser
    {
        public string UserId { get; set; }
        public BTUser User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
