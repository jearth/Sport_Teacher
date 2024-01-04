using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace SportProject.Data
{
    public class T_User
    {
        [Required]
        public string LeaderNo { get; set; }

        [Required]
        public string SchoolNo { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string SportNo { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "숫자만 입력하세요.")]
        [Required]
        public string TelNo { get; set; }

        [Required]
        public DateTime EmpDT { get; set; }

        public virtual IEnumerable<T_Work> T_Work { get; set; }
        public virtual IEnumerable<T_Certificate> T_Certificates { get; set; }
        public virtual T_Image T_Image { get; set; }
        public virtual T_Leader T_Leader { get; set; }
        public virtual T_Sport T_Sport { get; set; }
        public virtual T_School T_School { get; set; }
    }
}
