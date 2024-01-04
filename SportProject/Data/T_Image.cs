using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace SportProject.Data
{
    public class T_Image
    {
        [Required]
        public string LeaderNo { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
