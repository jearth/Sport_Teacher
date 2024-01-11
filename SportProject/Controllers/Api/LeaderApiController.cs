using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportProject.Services;

namespace SportProject.Controllers.Api
{
    [Route("api/leaders")]
    [ApiController]
    public class LeaderApiController : ControllerBase
    {
        private readonly ILeaderService _leaderService;
        public LeaderApiController(ILeaderService _leaderService)
        {
            
        }
    }
}
