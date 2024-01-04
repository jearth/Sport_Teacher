using System.ComponentModel.DataAnnotations;

namespace SportProject.Data
{
    public class T_Sport
    {
        [Required]
        public string SportNo { get; set; }

        [Required]
        public string SportName { get; set; }

        public virtual IEnumerable<T_User> T_User { get; set; }

    }
}
