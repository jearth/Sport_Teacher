using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using SportProject.DTO;
using SportProject.Services;
using static System.Net.Mime.MediaTypeNames;

namespace SportProject.Controllers.Client
{
    public class HomeController : Controller
    {
        private readonly ILeaderService _leaderService;

        public HomeController(ILeaderService leaderService)
        {
            _leaderService = leaderService;
        }
        //public IActionResult Register()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // model 데이터를 테이블에 저장
        //    }
        //    else
        //    {
        //        // 에러를 보여줌
        //    }

        //    return View();
        //}

        [HttpGet]
        public IActionResult Start()
        {
            // 1. 서비스에 요청해서 필요한 정보 얻어오기
            var infos = _leaderService.GetLeaderInfoList();

            // 2. DTO로 변환하기
            var leaderDTOs = new List<LeaderDTO>();

            foreach (var li in infos)
            {
                var leaderDTO = new LeaderDTO()
                {
                    LeaderNo = li.LeaderNo,
                    LeaderName = li.T_Leader.LeaderName,
                    SportName = li.T_Sport.SportName,
                    SchoolName = li.T_School.SchoolName
                };
                leaderDTOs.Add(leaderDTO);
            }
            ViewBag.LeaderinfoList = JsonSerializer.Serialize(leaderDTOs);

            return View();
        }

        [HttpGet]
        public List<LeaderDTO> StartInfo()
        {
            // 1. 서비스에 요청해서 필요한 정보 얻어오기
            var infos = _leaderService.GetLeaderInfoList();

            // 2. DTO로 변환하기
            var leaderDTOs = new List<LeaderDTO>();

            foreach (var li in infos)
            {
                var leaderDTO = new LeaderDTO()
                {
                    LeaderNo = li.LeaderNo,
                    LeaderName = li.T_Leader.LeaderName,
                    SportName = li.T_Sport.SportName,
                    SchoolName = li.T_School.SchoolName
                };
                leaderDTOs.Add(leaderDTO);
            }
            ViewBag.LeaderinfoList = JsonSerializer.Serialize(leaderDTOs);

            return leaderDTOs;
        }


        [HttpGet]
        public IActionResult Detail([FromQuery(Name = "leaderNo")] string leaderNo)
        {
            return View();
        }

        [HttpGet]
        public LeaderInfoDetailDTO DetailInfo([FromQuery(Name = "leaderNo")] string leaderNo)
        {
            // 1.서비스에 요청해서 필요한 정보 얻어오기
            var details = _leaderService.GetLeaderInfoListByLeaderNo(leaderNo);
            var sports = _leaderService.GetSportList();

            // 2 - 1.DTO로 변환하기(User)
            var detailDTO = new LeaderInfoDetailDTO();
            detailDTO.ImageBase = details.T_Image.Image;
            detailDTO.LeaderNo = details.T_Leader.LeaderNo;
            detailDTO.LeaderName = details.T_Leader.LeaderName;
            detailDTO.Gender = details.Gender;
            detailDTO.Birthday = details.Birthday;
            detailDTO.TelNo = details.TelNo;
            detailDTO.EmpDT = details.EmpDT;
            detailDTO.SchoolName = details.T_School.SchoolName;
            detailDTO.SportName = details.T_Sport.SportName;

            // 2 - 2.DTO로 변환하기(Work)
            var detailworkDTOs = new List<WorkDetailDTO>();

            foreach (var dw in details.T_Work)
            {
                var tSports = sports.FirstOrDefault(sports => sports.SportNo == dw.SportNo);
                var detailworkDTO = new WorkDetailDTO()
                {
                    WorkPlace = dw.WorkPlace,
                    EndDT = dw.EndDT,
                    StartDT = dw.StartDT,
                    SportName = tSports?.SportName ?? ""
                };
                detailworkDTOs.Add(detailworkDTO);
            }
            detailDTO.Work = detailworkDTOs;


            // 2 - 3.DTO로 변환하기(Certificate)
            var detailcertificateDTOs = new List<CertificateDTO>();

            foreach (var dc in details.T_Certificates)
            {
                var detailcertificateDTO = new CertificateDTO()
                {
                    CertificateName = dc.CertificateName,
                    CertificateNumber = dc.CertificateNumber,
                    CertificateDT = dc.CertificateDT,
                    Origanization = dc.Origanization
                };
                detailcertificateDTOs.Add(detailcertificateDTO);
            }
            detailDTO.Certificate = detailcertificateDTOs;

            ViewBag.leaderInfolistbyleaderno = JsonSerializer.Serialize(detailDTO);

            return detailDTO;
        }

        [HttpGet]
        public IActionResult Register()
        {
            // 1. 서비스에 요청해서 필요한 정보 얻어오기
            var leaders = _leaderService.GetLeaderList();

            // 2. DTO로 변환하기
            var listDTOs = new List<LeaderDTO>();
            foreach (var ld in leaders)
            {
                var dto = new LeaderDTO()
                {
                    LeaderNo = ld.LeaderNo,
                    LeaderName = ld.LeaderName
                };
                listDTOs.Add(dto);
            }
            ViewBag.LeaderList = JsonSerializer.Serialize(listDTOs);

            // 1. 서비스에 요청해서 필요한 정보 얻어오기
            var schools = _leaderService.GetSchoolList();

            // 2. DTO로 변환하기
            var schoolDTOs = new List<SchoolDTO>();
            foreach (var sc in schools)
            {
                var dto = new SchoolDTO()
                {
                    SchoolNo = sc.SchoolNo,
                    SchoolName = sc.SchoolName
                };
                schoolDTOs.Add(dto);
            }
            ViewBag.SchoolList = JsonSerializer.Serialize(schoolDTOs);


            // 1. 서비스에 요청해서 필요한 정보 얻어오기
            var sports = _leaderService.GetSportList();

            // 2. DTO로 변환하기
            var sportDTOs = new List<SportDTO>();
            foreach (var sp in sports)
            {
                var dto = new SportDTO()
                {
                    SportName = sp.SportName,
                    SportNo = sp.SportNo
                };
                sportDTOs.Add(dto);
            }
            ViewBag.SportList = JsonSerializer.Serialize(sportDTOs);

            return View();
        }

        [HttpGet]
        public LeaderDetailDTO RegisterInfo()
        {
            // 1. 서비스에 요청해서 필요한 정보 얻어오기
            var leaders = _leaderService.GetLeaderList();
            var schools = _leaderService.GetSchoolList();
            var sports = _leaderService.GetSportList();

            // 2. DTO로 변환하기
            var leaderDetailDTO = new LeaderDetailDTO
            {
                Leaders = leaders.Select(ld => new LeaderDTO
                {
                    LeaderNo = ld.LeaderNo,
                    LeaderName = ld.LeaderName
                }).ToList(),

                Schools = schools.Select(sc => new SchoolDTO
                {
                    SchoolNo = sc.SchoolNo,
                    SchoolName = sc.SchoolName
                }).ToList(),

                Sports = sports.Select(sp => new SportDTO
                {
                    SportName = sp.SportName,
                    SportNo = sp.SportNo
                }).ToList()
            };

            return leaderDetailDTO;
        }

        [HttpDelete]
        public IActionResult Remove([FromBody] string[] numbers)
        {
            //1.호출
            var result = _leaderService.RemoveUser(numbers);

            //2.상태 확인 
            if (result == HttpStatusCode.OK)
            {
                //성공 메시지 담기
                return Ok(); // -> 200
                //return NoContent(); // -> 204
                //return Created() // -> 201
            }
            else
            {
                //실패 메시지 담기
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult RegisterInfo([FromBody] LeaderInfoDTO leaderData)
        {
            if (ModelState.IsValid)
            {
               var user =  _leaderService.SaveUser(leaderData);
               if (user != null) return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Register([FromForm] LeaderInfoDTO leader)
        {
            if (ModelState.IsValid)
            {
                _leaderService.SaveUser(leader);
            }
            return RedirectToAction("Start");
        }

        [HttpGet]
        public IActionResult Edit([FromQuery(Name = "leaderNo")] string leaderNo)
        {
            // 1.서비스에 요청해서 필요한 정보 얻어오기
            var edits = _leaderService.GetLeaderInfoListByLeaderNo(leaderNo);
            var editsports = _leaderService.GetSportList();

            // 2 - 1.DTO로 변환하기(User)
            var detailDTO = new LeaderInfoDetailDTO();
            detailDTO.ImageBase = edits.T_Image.Image;
            detailDTO.LeaderNo = edits.T_Leader.LeaderNo;
            detailDTO.LeaderName = edits.T_Leader.LeaderName;
            detailDTO.Gender = edits.Gender;
            detailDTO.Birthday = edits.Birthday;
            detailDTO.TelNo = edits.TelNo;
            detailDTO.EmpDT = edits.EmpDT;
            detailDTO.SchoolNo = edits.T_School.SchoolNo;
            detailDTO.SportNo = edits.T_Sport.SportNo;
            detailDTO.SchoolName = edits.T_School.SchoolName;
            detailDTO.SportName = edits.T_Sport.SportName;

            // 2 - 2.DTO로 변환하기(Work)
            var detailworkDTOs = new List<WorkDetailDTO>();

            foreach (var ew in edits.T_Work)
            {
                var tSports = editsports.FirstOrDefault(sports => sports.SportNo == ew.SportNo);
                var detailworkDTO = new WorkDetailDTO()
                {
                    WorkPlace = ew.WorkPlace,
                    EndDT = ew.EndDT,
                    StartDT = ew.StartDT,
                    SportName = tSports?.SportName ?? ""
                };
                detailworkDTOs.Add(detailworkDTO);
            }
            ViewBag.WorkEditList = JsonSerializer.Serialize(detailworkDTOs);

            detailDTO.Work = detailworkDTOs;


            // 2 - 3.DTO로 변환하기(Certificate)
            var detailcertificateDTOs = new List<CertificateDTO>();

            foreach (var ec in edits.T_Certificates)
            {
                var detailcertificateDTO = new CertificateDTO()
                {
                    CertificateName = ec.CertificateName,
                    CertificateNumber = ec.CertificateNumber,
                    CertificateDT = ec.CertificateDT,
                    Origanization = ec.Origanization
                };
                detailcertificateDTOs.Add(detailcertificateDTO);
            }
            ViewBag.CertificateEditList = JsonSerializer.Serialize(detailcertificateDTOs);

            detailDTO.Certificate = detailcertificateDTOs;

            //ViewBag.leaderInfolistbyleaderno = JsonSerializer.Serialize(detailDTO);
            // 1. 서비스에 요청해서 필요한 정보 얻어오기
            var schools = _leaderService.GetSchoolList();

            // 2. DTO로 변환하기
            var schoolDTOs = new List<SchoolDTO>();
            foreach (var sc in schools)
            {
                var dto = new SchoolDTO()
                {
                    SchoolNo = sc.SchoolNo,
                    SchoolName = sc.SchoolName
                };
                schoolDTOs.Add(dto);
            }
            ViewBag.SchoolList = JsonSerializer.Serialize(schoolDTOs);

            // 1. 서비스에 요청해서 필요한 정보 얻어오기
            var sports = _leaderService.GetSportList();

            // 2. DTO로 변환하기
            var sportDTOs = new List<SportDTO>();
            foreach (var sp in sports)
            {
                var dto = new SportDTO()
                {
                    SportName = sp.SportName,
                    SportNo = sp.SportNo
                };
                sportDTOs.Add(dto);
            }
            ViewBag.SportList = sportDTOs;

            return View(detailDTO);
        }

        [HttpGet]
        public LeaderEditDTO EditInfo([FromQuery(Name = "leaderNo")] string leaderNo)
        {
            // 1. 서비스에 필요한 정보 얻어오기
            var edits = _leaderService.GetLeaderInfoListByLeaderNo(leaderNo);
            var editsports = _leaderService.GetSportList();

            // 2 - 1. DTO로 변환하기(User)
            var leaderEditDTO = new LeaderEditDTO
            {
                ImageBase = edits.T_Image.Image,
                LeaderNo = edits.T_Leader.LeaderNo,
                LeaderName = edits.T_Leader.LeaderName,
                Gender = edits.Gender,
                Birthday = edits.Birthday,
                TelNo = edits.TelNo,
                EmpDT = edits.EmpDT,
                SchoolNo = edits.T_School.SchoolNo,
                SportNo = edits.T_Sport.SportNo,
                SchoolName = edits.T_School.SchoolName,
                SportName = edits.T_Sport.SportName
            };

            // 2 - 2. DTO로 변환하기(Work)
            var workDTOs = edits.T_Work.Select(ew =>
            {
                var tSports = editsports.FirstOrDefault(sports => sports.SportNo == ew.SportNo);
                return new WorkDetailDTO
                {
                    WorkPlace = ew.WorkPlace,
                    EndDT = ew.EndDT,
                    StartDT = ew.StartDT,
                    SportName = tSports?.SportName ?? "",
                    SportNo = ew.SportNo
                };
            }).ToList();

            leaderEditDTO.Work = workDTOs;

            // 2 - 3. DTO로 변환하기(Certificate)
            var certificateDTOs = edits.T_Certificates.Select(ec => new CertificateDTO
            {
                CertificateName = ec.CertificateName,
                CertificateNumber = ec.CertificateNumber,
                CertificateDT = ec.CertificateDT,
                Origanization = ec.Origanization
            }).ToList();

            leaderEditDTO.Certificate = certificateDTOs;

            // 3. 학교 정보 처리
            var schoolDTOs = _leaderService.GetSchoolList().Select(sc => new SchoolDTO
            {
                SchoolNo = sc.SchoolNo,
                SchoolName = sc.SchoolName
            }).ToList();

            leaderEditDTO.Schools = schoolDTOs;

            // 4. 스포츠 정보 처리
            var sportDTOs = _leaderService.GetSportList().Select(sp => new SportDTO
            {
                SportName = sp.SportName,
                SportNo = sp.SportNo
            }).ToList();

            leaderEditDTO.Sports = sportDTOs;

            return leaderEditDTO;
        }


        [HttpPost]
        public IActionResult Edit([FromForm] LeaderInfoDetailDTO leader)
        {
            // 1. 디테일에 있는 정보를 가져온다.
            var ResultData = _leaderService.EditUser(leader);

            return View("Detail");
        }

        [HttpPost]
        public IActionResult EditInfo([FromBody] LeaderInfoDetailDTO leaderEditInfo)
        {
            if (ModelState.IsValid)
            {            
                // 1. 디테일에 있는 정보를 가져온다.
                var user = _leaderService.EditUser(leaderEditInfo);
                if (user != null) return Ok();
            }

            return BadRequest();
        }

    }
}


//if (ResultData == null)
//{
//    // 적절한 오류 처리를 수행
//    return RedirectToAction("Detail");
//}

//_leaderService.SaveChanges();