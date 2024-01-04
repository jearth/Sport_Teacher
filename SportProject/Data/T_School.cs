using System.ComponentModel.DataAnnotations;

namespace SportProject.Data
{
    public class T_School
    {
        [Required]
        public string SchoolNo { get; set; }

        [Required]
        public string SchoolName { get; set; }

        public virtual IEnumerable<T_User> T_User { get; set; }

    }
}
