using System.ComponentModel.DataAnnotations;

namespace SportProject.Data
{
    public class T_Certificate
    {
        //prop 사용
        [Required]
        public string LeaderNo { get; set; }

        [Required]
        public int No { get; set; }

        [Required]
        public string CertificateName { get; set; }

        [RegularExpression("^[0-9a-zA-Z]*$", ErrorMessage = "숫자와 영문자만 입력하세요.")]
        [Required]
        public string CertificateNumber { get; set; }

        [Required]
        public DateTime CertificateDT { get; set; }

        [Required]
        public string Origanization { get; set; }
    }
}
