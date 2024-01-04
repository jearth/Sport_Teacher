using System.ComponentModel.DataAnnotations;

namespace SportProject.Data
{
    public class T_Leader
    {
        [Required]
        public string LeaderNo { get; set; }

        [Required]
        public string LeaderName { get; set; }

        public virtual T_User T_User { get; set; }

    }
}
