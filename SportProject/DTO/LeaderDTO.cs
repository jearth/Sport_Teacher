using System.ComponentModel.DataAnnotations;

namespace SportProject.DTO
{
    public class LeaderDTO
    {
        public string LeaderNo { get; set; }
        public string LeaderName { get; set; }
        public string SportName { get; set; }
        public string SchoolName { get; set; }
    }

    public class LeadeDetailDTO
    {
        public string LeaderNo { get; set; }
        public string LeaderName { get; set; }
        public string SportName { get; set; }
        public string SportNo {  get; set; }
        public string SchoolName { get; set; }

        public string SchoolNo { get;set; }
    }
}
