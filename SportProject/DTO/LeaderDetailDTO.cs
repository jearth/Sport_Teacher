using System.Reflection.Metadata;
using SportProject.Data;

namespace SportProject.DTO
{
    public class LeaderDetailDTO
    {
        public List<LeaderDTO> Leaders { get; set; }
        public List<SchoolDTO> Schools { get; set; }
        public List<SportDTO> Sports { get; set; }

    }
}
