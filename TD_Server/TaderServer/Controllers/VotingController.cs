using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaderServer.Models;

namespace TaderServer.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        DBConnection dbcon = new DBConnection();
        private DataSet titleds, contentds, allds;

        public VotingController() { }

        [HttpGet("votingbool")] // 존재여부
        public IEnumerable<string> GetByKind()
        {
            var test = M_VotingTitle.GetTitlelist().FirstOrDefault(p => p.Votingbool == "t");

            if (test == null)
            {
                yield return "f";
            }
            else
            {
                yield return "t";
            }
        }

        [HttpPost("insert/title")] // 투표 타이틀 입력
        public IEnumerable<string> VotingtitleInsert([FromBody] M_VotingTitle m_VotingTitle)
        {
            M_VotingTitle.GetTitlelist().Add(new M_VotingTitle
            {
                Votingbool = "t",
                Votingtitle = m_VotingTitle.Votingtitle,
                Votingmemo = m_VotingTitle.Votingmemo,
                Votingday = m_VotingTitle.Votingday,
                Votingyn = m_VotingTitle.Votingyn // n = 익명x , y = 익명
            });
            Console.WriteLine("타이틀 : "+M_VotingTitle.GetTitlelist()[0].Votingtitle);
            yield return "t";
        }
        [HttpGet("select/title")] // 투표  타이틀 출력
        public IEnumerable<M_VotingTitle> GetByTitle()
        {
            return M_VotingTitle.GetTitlelist();
        }
       
        [HttpPost("insert/content")] // 투표 항목 입력
        public IEnumerable<string> VotingConInsert([FromBody] M_VotingCotent m_VotingCotent)
        {
            M_VotingCotent.GetConlist().Add(new M_VotingCotent
            {
                Votingnum = m_VotingCotent.Votingnum,
                Votingcon = m_VotingCotent.Votingcon,
                Votingcontentcnt = 0
            });
            M_VotingCotent.GetConlist().OrderBy(x => x.Votingnum).ToList();


            Console.WriteLine("항목번호 : "+ m_VotingCotent.Votingnum +"||\t명 : " + m_VotingCotent.Votingcon);
            
            yield return "t";
        }
        [HttpGet("select/content")] // 투표 항목 출력
        public IEnumerable<M_VotingCotent> GetByContent()
        {
            return M_VotingCotent.GetConlist();
        }
        [HttpGet("select/{userid}")] // 투표 리스트 존재 여부 판단
        public IEnumerable<string> GetByUser(string userid)
        {
            var test = M_VotingCount.GetCountlist().FirstOrDefault(p => p.Votinguser == userid);

            if (test == null)
            {
                yield return "f";
            }
            else
            {
                string temp = "";
                foreach (M_VotingCount item in M_VotingCount.GetCountlist())
                {
                    if (item.Votinguser == userid)
                    {
                        temp = item.Votingnum.ToString();
                    }
                }
                yield return temp;
            }
        }
        [HttpGet("select/userall")] // 투표 유저 출력
        public IEnumerable<M_VotingCount> GetByUser()
        {
            
            return M_VotingCount.GetCountlist();
        }
        [HttpPost("insert/user")] // 유저의 투표 입력
        public IEnumerable<string> VotingUserInsert([FromBody] M_VotingCount m_VotingCount)
        {
            M_VotingCount.GetCountlist().Add(new M_VotingCount
            {
                Votingnum = m_VotingCount.Votingnum,
                Votinguser = m_VotingCount.Votinguser
            });
            foreach(M_VotingCotent item in M_VotingCotent.GetConlist())
            {
                if(item.Votingnum == m_VotingCount.Votingnum)
                {
                    item.Votingcontentcnt = item.Votingcontentcnt + 1;
                }
            }
            Console.WriteLine("항목번호 : " + m_VotingCount.Votingnum + "||\t유저 : " + m_VotingCount.Votinguser);
            yield return "t";
        }
       
        [HttpGet("select/user/{contentnum}")] // 유저의 투표 이름 출력
        public IEnumerable<string> GetByUserPrint(int contentnum)
        {
            string temp = "";
            foreach(M_VotingCount item in M_VotingCount.GetCountlist())
            {
                if(item.Votingnum == contentnum)
                {
                    temp = item.Votinguser;
                }
            }
            yield return temp;
        }
        [HttpPost("update/userid")] // 유저의 투표 수정
        public IEnumerable<string> VotingUserUpdate([FromBody] M_VotingCount m_VotingCount)
        {
           foreach(M_VotingCount item in M_VotingCount.GetCountlist())
            {
                if(item.Votinguser == m_VotingCount.Votinguser && m_VotingCount.Votingnum != item.Votingnum)
                {
                    int temp = item.Votingnum;
                    item.Votingnum = m_VotingCount.Votingnum;
                    foreach (M_VotingCotent item2 in M_VotingCotent.GetConlist())
                    {
                        if (item2.Votingnum == temp)
                        {
                            item2.Votingcontentcnt = item2.Votingcontentcnt - 1; // 수정 -1
                        }
                        else if (item2.Votingnum == m_VotingCount.Votingnum)
                        {
                            item2.Votingcontentcnt = item2.Votingcontentcnt + 1; //수정들어온 값 +1
                        }
                    }
                }
            }
            yield return "t";
        }
        [HttpGet("voting/dbinsert")] // 투표 내역 저장
        public IEnumerable<string> Voting_DBInsert()
        {
            try
            {
                dbcon.InsertTitleVoting(M_VotingTitle.GetTitlelist()[0].Votingtitle, M_VotingTitle.GetTitlelist()[0].Votingmemo, M_VotingTitle.GetTitlelist()[0].Votingday, M_VotingTitle.GetTitlelist()[0].Votingyn);
                titleds = dbcon.SelectTitleData(M_VotingTitle.GetTitlelist()[0].Votingtitle);
                foreach (DataRow r in titleds.Tables[0].Rows)
                {
                    foreach (M_VotingCotent item in M_VotingCotent.GetConlist())
                    {
                        dbcon.InsertContentVoting(Int32.Parse(r["VotingNum"].ToString()), item.Votingcon, item.Votingcontentcnt);
                    }
                }
                foreach(M_VotingCount item2 in M_VotingCount.GetCountlist())
                {
                    foreach(M_VotingCotent item3 in M_VotingCotent.GetConlist())
                    {
                        if(item3.Votingnum == item2.Votingnum)
                        {
                            contentds = dbcon.SelectContentData(item3.Votingcon);
                            foreach (DataRow r in contentds.Tables[0].Rows)
                            {
                                dbcon.InsertUserVoting(Int32.Parse(r["VotingConNum"].ToString()),item2.Votinguser);
                            }
                        }
                    }
                }
                M_VotingTitle.GetTitlelist().Clear();
                M_VotingCotent.GetConlist().Clear();
                M_VotingCount.GetCountlist().Clear();
                Console.WriteLine("투표함 내역 삭제");
            }
            catch (Exception)
            { 
                Console.WriteLine("투표함 저장 실패"); 
            }
            yield return "t";
        }
        [HttpGet("select/all/{userid}")] // 과거 투표 내역 출력
        public IEnumerable<M_VotingAll> Voting_GetAllData(string userid)
        {
            M_VotingAll.GetVoAlllist().Clear();

            allds = dbcon.SelectAllVoting(userid);
            try
            {
                int i = 0;
                bool first = true;
                foreach (DataRow r in allds.Tables[0].Rows)
                {
                    if (first == true)
                    {
                        M_VotingAll.GetVoAlllist().Add(new M_VotingAll
                        {
                            Alltitle = r["VotingTitle"].ToString(),
                            Allmemo = r["VotingMemo"].ToString(),
                            Allday = r["VotingDay"].ToString(),
                            Allyn = r["VotingYN"].ToString(),
                            Allcontent = r["VotingCon"].ToString(),
                            Allcount = r["VotingAllcnt"].ToString(),
                            Alluser = r["VotingUser"].ToString(),
                            Allcountall = Int32.Parse(r["VotingAllcnt"].ToString())
                        });
                        first = false;
                    }
                    else
                    {
                        if (M_VotingAll.GetVoAlllist()[i].Alltitle == r["VotingTitle"].ToString() && M_VotingAll.GetVoAlllist()[i].Allday == r["VotingDay"].ToString())
                        {
                            if(M_VotingAll.GetVoAlllist()[i].Allcontent == r["VotingCon"].ToString())
                            {
                                M_VotingAll.GetVoAlllist()[i].Alluser += ", " + r["VotingUser"].ToString();
                            }
                            else
                            {
                                M_VotingAll.GetVoAlllist()[i].Allcontent += "&%&" + r["VotingCon"].ToString();
                                M_VotingAll.GetVoAlllist()[i].Allcount += "&%&" + Int32.Parse(r["VotingAllcnt"].ToString());
                                M_VotingAll.GetVoAlllist()[i].Alluser += ", " + r["VotingUser"].ToString();
                                M_VotingAll.GetVoAlllist()[i].Allcountall += Int32.Parse(r["VotingAllcnt"].ToString());
                            }
                        }
                        else
                        {
                            M_VotingAll.GetVoAlllist().Add(new M_VotingAll
                            {
                                Alltitle = r["VotingTitle"].ToString(),
                                Allmemo = r["VotingMemo"].ToString(),
                                Allday = r["VotingDay"].ToString(),
                                Allyn = r["VotingYN"].ToString(),
                                Allcontent = r["VotingCon"].ToString(),
                                Allcount = r["VotingAllcnt"].ToString(),
                                Alluser = r["VotingUser"].ToString(),
                                Allcountall = Int32.Parse(r["VotingAllcnt"].ToString())
                            });
                            i++;
                        }
                    }
                  
                }
                i = 0;
                Console.WriteLine("과거 내역 불러오기");
               
                return M_VotingAll.GetVoAlllist();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
