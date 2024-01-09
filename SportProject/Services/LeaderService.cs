using System.Net;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using SportProject.Data;
using SportProject.DTO;

namespace SportProject.Services
{

    public interface ILeaderService
    {
        IEnumerable<T_Leader> GetLeaderList();
        IEnumerable<T_School> GetSchoolList();
        IEnumerable<T_Sport> GetSportList();
        IEnumerable<T_User> GetLeaderInfoList();
        T_User GetLeaderInfoListByLeaderNo(string leaderNo);
        T_User SaveUser(LeaderInfoDTO dto);
        T_User EditUser(LeaderInfoDetailDTO dto);

        HttpStatusCode RemoveUser(string[] numbers);

    }
    public class LeaderService : ILeaderService
    {
        private readonly LeaderDbContext _dbContext;

        public LeaderService(LeaderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T_Leader> GetLeaderList()
        {
            return _dbContext.T_Leader.ToList();
        }

        public IEnumerable<T_School> GetSchoolList()
        {
            return _dbContext.T_School.ToList();
        }

        public IEnumerable<T_Sport> GetSportList()
        {
            return _dbContext.T_Sport.ToList();
        }

        public IEnumerable<T_User> GetLeaderInfoList()
        {
            return _dbContext.T_User
                                  .Include(r => r.T_Leader)
                                  .Include(r => r.T_Sport)
                                  .Include(r => r.T_School)
                                  .ToList();
        }

        public T_User GetLeaderInfoListByLeaderNo(string leaderNo)
        {
            return _dbContext.T_User
                    .Where(r => r.LeaderNo == leaderNo)
                    .Include(r => r.T_Leader)
                    .Include(r => r.T_Sport)
                    .Include(r => r.T_School)
                    .Include(r => r.T_Image)
                    .Include(r => r.T_Certificates)
                    .Include(r => r.T_Work)
                    .FirstOrDefault();
        }

        public T_User SaveUser(LeaderInfoDTO dto)
        {
            var _transaction = _dbContext.Database.BeginTransaction();
            try
            {
                //1. user 테이블 저장
                var leaderInfo = new T_User()
                {
                    LeaderNo = dto.LeaderNo,
                    SchoolNo = dto.SchoolNo,
                    Birthday = dto.Birthday,
                    Gender = dto.Gender,
                    SportNo = dto.SportNo,
                    TelNo = dto.TelNo,
                    EmpDT = dto.EmpDT
                };
                var img = new T_Image()
                {
                    LeaderNo = dto.LeaderNo,
                    Image = dto.ImageBase,
                };

                var worklist = new List<T_Work>();
                foreach (var w in dto.Work)
                {
                    var work = new T_Work()
                    {
                        LeaderNo = dto.LeaderNo,
                        StartDT = w.StartDT,
                        EndDT = w.EndDT,
                        WorkPlace = w.WorkPlace,
                        SportNo = w.SportNo
                    };
                    worklist.Add(work);
                }

                var certificatelist = new List<T_Certificate>();
                foreach (var c in dto.Certificate) 
                {
                    var certificate = new T_Certificate()
                    {
                        LeaderNo = dto.LeaderNo,
                        CertificateName = c.CertificateName,
                        CertificateNumber = c.CertificateNumber,
                        CertificateDT = c.CertificateDT,
                        Origanization = c.Origanization
                    };
                    certificatelist.Add(certificate);
                }
               
                _dbContext.T_User.Add(leaderInfo);
                _dbContext.SaveChanges();
                _dbContext.T_Image.Add(img);
                _dbContext.T_Work.AddRange(worklist);
                _dbContext.T_Certificate.AddRange(certificatelist);
                _dbContext.SaveChanges();
                _transaction.Commit();
                return leaderInfo;
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception("이력 등록 실패했습니다.");
            }

        }
        public HttpStatusCode RemoveUser(string[] numbers)
        {
            var _transaction = _dbContext.Database.BeginTransaction();
            try
            {
                foreach (var number in numbers)
                {
                    // 1. 삭제할 사용자가 실제로 있는지 확인
                    var user = _dbContext.T_User.FirstOrDefault(u => u.LeaderNo == number);

                    if (user != null)
                    {
                        // 2. 사용자가 있으면 삭제
                        _dbContext.T_User.Remove(user);

                        // 3. 삭제 시 관련된 테이블도 함께 삭제 되어야 함
                        // 없을 수도 있을..때.. 조건시
                        
                        // 3-1. 근무이력
                        var relatedData = _dbContext.T_Work
                            .Where(user => user.LeaderNo == number).ToList();

                        if (relatedData.Count() > 0)
                        {
                            _dbContext.T_Work.RemoveRange(relatedData);
                        }

                        // 3-2. 자격사항
                        var certificateData = _dbContext.T_Certificate.Where(u => u.LeaderNo == number).ToList();

                        if (certificateData.Count() > 0)
                        {
                            _dbContext.T_Certificate.RemoveRange(certificateData);
                        }

                        // 3-3. 이미지
                        var imageData = _dbContext.T_Image.Where(u => u.LeaderNo == number).FirstOrDefault();
                        if (imageData != null)
                        {
                            _dbContext.T_Image.Remove(imageData);
                        }
                    }
                }
                _dbContext.SaveChanges(); // 변경사항 저장

                _transaction.Commit();
                return HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                return HttpStatusCode.BadRequest;
            }
        }

        public T_User EditUser(LeaderInfoDetailDTO dto)
        {
            var _transaction = _dbContext.Database.BeginTransaction();
            try
            {
                //1. user 테이블 수정
                var EditUserInfo = _dbContext.T_User
                                    .Include(r=> r.T_Image)
                                    .FirstOrDefault(u => u.LeaderNo == dto.LeaderNo);

                if (EditUserInfo == null)
                    throw new Exception("지도자 정보가 없습니다.");


                //추후 SchoolNo 잘 넘기고 삭제 !!!
                // dto.SchoolNo = "0004";
                //var existSchool = _dbContext.T_School.FirstOrDefault(r => r.SchoolNo == dto.SchoolNo);
                //if (existSchool == null)
                //    throw new Exception("존재하지 않는 학교입니다.");

                EditUserInfo.SchoolNo = dto.SchoolNo;

                EditUserInfo.Birthday = dto.Birthday;
                EditUserInfo.Gender = dto.Gender;
                EditUserInfo.SportNo = dto.SportNo;
                EditUserInfo.TelNo = dto.TelNo;
                EditUserInfo.EmpDT = dto.EmpDT;
                _dbContext.T_User.Update(EditUserInfo);

                var img = _dbContext.T_Image.Where(r => r.LeaderNo == dto.LeaderNo).FirstOrDefault();
                img.Image = dto.ImageBase;
                _dbContext.T_Image.Update(img);

                // Work 테이블 수정 
                //1. 기존 정보 삭제
                var works = _dbContext.T_Work.Where(r => r.LeaderNo == dto.LeaderNo).ToList();
                _dbContext.T_Work.RemoveRange(works);
                _dbContext.SaveChanges();
                //2. dto 만큼 저장
                var EditWorks = new List<T_Work>();

                foreach (var workinfo in dto.Work)
                {
                    var work = new T_Work()
                    {
                        LeaderNo = dto.LeaderNo,
                        StartDT = workinfo.StartDT,
                        EndDT = workinfo.EndDT,
                        WorkPlace = workinfo.WorkPlace,
                        SportNo = workinfo.SportNo
                    };

                    EditWorks.Add(work);
                }
                _dbContext.T_Work.AddRange(EditWorks);

                // Certificate 테이블 수정 
                //1. 기존 정보 삭제
                var cers = _dbContext.T_Certificate.Where(r => r.LeaderNo == dto.LeaderNo).ToList();
                _dbContext.T_Certificate.RemoveRange(cers);

                //2. dto 만큼 저장
                var EditCertificates = new List<T_Certificate>();

                foreach (var certificateinfo in dto.Certificate)
                {
                    var work = new T_Certificate()
                    {
                        LeaderNo = dto.LeaderNo,
                        CertificateName = certificateinfo.CertificateName,
                        CertificateNumber = certificateinfo.CertificateNumber,
                        CertificateDT = certificateinfo.CertificateDT,
                        Origanization = certificateinfo.Origanization
                    };

                    EditCertificates.Add(work);
                }
                _dbContext.T_Certificate.AddRange(EditCertificates);

                _dbContext.SaveChanges();
                _transaction.Commit();
                return EditUserInfo;

            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception("이력 수정 실패했습니다.");
            }
        }
       
    }

}

//public string GetSportNameByNo(string sportNo)
//{
//    var sport = _dbContext.T_Sport.Where(r => r.SportNo == sportNo).FirstOrDefault();
//    if (sport == null) throw new Exception("해당 종목 없음");
//    return sport.SportName;
//}
