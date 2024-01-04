using System.ComponentModel.DataAnnotations;

namespace SportProject.Data
{
    public class T_Work
    {
        [Required]
        public string LeaderNo { get; set; }

        [Required]
        public int No { get; set; }

        [Required]
        public string WorkPlace {  get; set; }

        [Required]
        public DateTime StartDT { get; set; }

        public DateTime EndDT { get; set; }

        [Required]
        public string SportNo { get; set; }
    }
}
