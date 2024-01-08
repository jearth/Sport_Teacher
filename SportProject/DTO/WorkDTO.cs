namespace SportProject.DTO
{
    public class WorkDTO
    {
        public string WorkPlace { get; set; }

        public DateTime StartDT { get; set; }

        public DateTime EndDT { get; set; }

        public string SportNo{ get; set;}
    }

    public class WorkDetailDTO
    {
        public string WorkPlace { get; set; }

        public DateTime StartDT { get; set; }

        public DateTime EndDT { get; set; }

        public string SportName { get; set; }
        // 넘버 추가
    }
}
