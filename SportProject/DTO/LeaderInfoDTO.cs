using System.Reflection.Metadata;
using SportProject.Data;

namespace SportProject.DTO
{
    public class LeaderInfoDTO
    {
        public string ImageBase { get; set; }

        public string LeaderNo { get; set; }

        public string SchoolNo { get; set; }

        public string LeaderName { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string SportNo { get; set; }

        public string TelNo { get; set; }

        public DateTime EmpDT { get; set; }

        public IEnumerable<WorkDTO> Work { get; set; }

        public IEnumerable<CertificateDTO> Certificate { get; set; }

    }

    public class LeaderInfoDetailDTO
    {
        public string ImageBase { get; set; }

        public string LeaderNo { get; set; }

        public string SchoolName { get; set; }

        public string LeaderName { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string SportName { get; set; }

        public string TelNo { get; set; }

        public string? Tel1
        {
            get { return TelNo?.Split("-")[0] ; }
        }

        public string? Tel2
        {
            get { return TelNo?.Split("-")[1]; }
        }

        public string? Tel3
        {
            get { return TelNo?.Split("-")[2]; }
        }

        public DateTime EmpDT { get; set; }

        public IEnumerable<WorkDetailDTO> Work { get; set; }

        public IEnumerable<CertificateDTO> Certificate { get; set; }
    }

}
