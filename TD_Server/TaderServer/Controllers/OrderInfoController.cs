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
    public class OrderInfoController : ControllerBase
    {
        DBConnection dbcon = new DBConnection();
        private DataSet selectuserds;

        private bool createcheck = false;
        public OrderInfoController(){}

        [HttpGet]
        public IEnumerable<M_CreateInfo> GetByAll()
        {
            return M_CreateInfo.GetCreatelist();
        }
        [HttpGet("userid/select/{userid}")] // DB 존재 여부
        public IEnumerable<string> GetCheckuserid(string userid)
        {
            string tempid = "";
            try
            {
                selectuserds = dbcon.Select_temp(string.Format("select userid from userinfortb where username = '{0}'", userid));

                foreach (DataRow r in selectuserds.Tables[0].Rows)
                {
                    tempid = r["UserId"].ToString();
                }
                if (tempid == "")
                {
                    Console.WriteLine("없는 ID");
                    dbcon.Insert_temp(string.Format("insert into UserInfortb (userId,Username) values (user_seq.nextval,'{0}')", userid));
                    tempid = userid;
                }
                else
                {
                    Console.WriteLine("이미 존재하는 ID");
                    tempid = "zopweiqushdzasdwqfngl";
                }
            }
            catch(Exception)
            { }
            yield return tempid;
        }
        [HttpGet("userid/delete/{userid}")] // DB 기존 삭제
        public IEnumerable<string> GetDeleteuserid(string userid)
        {
            try
            {
                 dbcon.DeleteALL_temp(string.Format("delete from userinfortb where username = '{0}'", userid));
                Console.WriteLine("기존 ID 삭제");
            }
            catch (Exception)
            { Console.WriteLine("기존 ID 삭제 실패"); }
            yield return "f";
        }

        [HttpGet("userid/{userid}")] // 서버 리스트 존재 여부
        public IEnumerable<string> GetByKind(string userid)
        {
            var test = M_OrderInfo.GetInfolist().FirstOrDefault(p => p.UserName == userid);

            if (test == null)
            {
                yield return "f";
            }
            else
            {
                yield return "t";
            }
        }
        [HttpGet("usermenu/{userid}")] // 주문한 유저의 메뉴
        public IEnumerable<string> GetByUserMenu(string userid)
        {
            string usermenuop = "";
            try
            {
                foreach (M_OrderInfo info in M_OrderInfo.GetInfolist())
                {
                    if (info.UserName == userid)
                    {
                        usermenuop += info.MenuName;
                        usermenuop += "&";
                        usermenuop += info.OptionDes;
                        Console.WriteLine(info.UserName + "의 주문 내역\n" + "메뉴 : " + info.MenuName + "\t\t 옵션 : " + info.OptionDes);
                        break;
                    }
                }
            }
            catch(Exception)
            {
                usermenuop = "";
                Console.WriteLine("유저 주문내역 불러오기 실패");
            }
            yield return usermenuop;
        }
        //출력 및 주문내역 처리 & 데이터 저장
        [HttpGet("reseipt")]
        public IEnumerable<string> GetReseiptData()
        {
            string reseiptstr = "";
            try
            {
                foreach (M_CreateInfo item in M_CreateInfo.GetCreatelist())
                {
                    if (item.Menu != "")
                    {
                        string optionstr = "";
                        reseiptstr += item.Menu;
                        reseiptstr += "&";
                        reseiptstr += item.Menu_Count.ToString();
                        reseiptstr += "&";
                        foreach (M_OrderInfo item2 in M_OrderInfo.GetInfolist())
                        {
                            if (item.Menu == item2.MenuName)
                            {
                                optionstr += "@";
                                optionstr += item2.OptionDes;
                            }
                        }
                        reseiptstr += optionstr;
                        reseiptstr += "&";
                    }
                }
                //데이터 저장
                foreach(M_OrderInfo item in M_OrderInfo.GetInfolist())
                {
                    dbcon.Insert_temp(string.Format(
                        "Insert Into PastOrderTB values(past_seq.nextval,'{0}','{1}','{2}','{3}','{4}')"
                        ,item.UserName,M_OrderList.GetOrderlist()[0].KindName, M_OrderList.GetOrderlist()[0].StoreName,item.MenuName,item.OptionDes));
                }

                M_CreateInfo.GetCreatelist().Clear();
                M_OrderList.GetOrderlist().Clear();
                M_OrderInfo.GetInfolist().Clear();
                Console.WriteLine("주문 내역 삭제");
            }
            catch (Exception)
            { }
            yield return reseiptstr;
        }
        [HttpGet("orderlistall")]
        public IEnumerable<string> Getorderlistalldata()
        {
            string reseiptstr = "";
            try
            {
                foreach (M_CreateInfo item in M_CreateInfo.GetCreatelist())
                {
                    if (item.Menu != "")
                    {
                        int i = 0;
                        foreach (M_OrderInfo item2 in M_OrderInfo.GetInfolist())
                        {
                            if (i==0)
                            {
                                reseiptstr += item.Menu;
                                reseiptstr += "&";
                                reseiptstr += item2.OptionDes;
                                reseiptstr += "&";
                            }
                            else if (item.Menu == item2.MenuName && item.Menu_Count != 1)
                            {
                                reseiptstr += "비어질 메뉴";
                                reseiptstr += "&";
                                reseiptstr += item2.OptionDes;
                                reseiptstr += "&";
                            }
                            i++;
                        }
                    }
                }
            }
            catch (Exception)
            { }
            yield return reseiptstr;
        }
        //메뉴, 옵션 POST
        [HttpPost]
        public IEnumerable<string> UserMenuCreate([FromBody] M_OrderInfo m_OrderInfo)
        {
            
            if(m_OrderInfo.MenuName == "")
            {
                m_OrderInfo.MenuName = "x";
            }
            if (m_OrderInfo.OptionDes == "")
            {
                m_OrderInfo.OptionDes = "x";
            }
            M_OrderInfo.GetInfolist().Add(new M_OrderInfo
            {
                UserName = m_OrderInfo.UserName,
                MenuName = m_OrderInfo.MenuName,
                OptionDes = m_OrderInfo.OptionDes,
                MenuName_YN = "N",
                OptionDes_YN = "N"
            });
            bool createbool = false;
            foreach (M_OrderInfo item in M_OrderInfo.GetInfolist())
            {
                if (createbool == true) break;
                if (item.UserName != m_OrderInfo.UserName)
                {
                    if (item.MenuName == m_OrderInfo.MenuName)
                    {
                        for (int i=0; i< M_CreateInfo.GetCreatelist().Count;i++)
                        {
                            if (item.MenuName == M_CreateInfo.GetCreatelist()[i].Menu)
                            {
                                M_CreateInfo.GetCreatelist()[i].Menu_Count = M_CreateInfo.GetCreatelist()[i].Menu_Count + 1;
                                createbool = true;                                
                            }
                            if (createbool == true) break;
                        }
                    }
                }
            }
            if (createbool == false && m_OrderInfo.MenuName != "x")
            {
                M_CreateInfo.GetCreatelist().Add(new M_CreateInfo
                {
                    //Num = OrderInfoMemory.Count,
                    Menu = m_OrderInfo.MenuName,
                    Menu_Count = 1
                });
            }
            foreach(M_CreateInfo info in M_CreateInfo.GetCreatelist())
            {
                Console.WriteLine(info.Menu + info.Menu_Count);
            }
            Console.WriteLine("유저아이디는 " + m_OrderInfo.UserName + " 메뉴 :  " + m_OrderInfo.MenuName + "  옵션 : "+ m_OrderInfo.OptionDes );
            string add = m_OrderInfo.UserName + "&" + m_OrderInfo.MenuName + "&" + m_OrderInfo.OptionDes;
            yield return add;
        }
        [HttpPost("update/usermenu")]
        public IEnumerable<string> UserMenuUpdate([FromBody] M_OrderInfo m_OrderInfo)
        {
            for(int i=0; i < M_OrderInfo.GetInfolist().Count; i++)
            {
                if (M_OrderInfo.GetInfolist()[i].UserName == m_OrderInfo.UserName)
                {
                    for (int j = 0; j < M_CreateInfo.GetCreatelist().Count; j++)
                    {
                        if (M_CreateInfo.GetCreatelist()[j].Menu == M_OrderInfo.GetInfolist()[i].MenuName)
                        {
                            M_CreateInfo.GetCreatelist()[j].Menu_Count = M_CreateInfo.GetCreatelist()[j].Menu_Count - 1;

                            if (M_CreateInfo.GetCreatelist()[j].Menu_Count == 0)
                            {
                                M_CreateInfo.GetCreatelist().RemoveAt(j);
                                break;
                            }
                            break;
                        }
                    }

                    M_OrderInfo.GetInfolist()[i].MenuName = m_OrderInfo.MenuName;
                    M_OrderInfo.GetInfolist()[i].OptionDes = m_OrderInfo.OptionDes;
                    Console.WriteLine("6" + M_OrderInfo.GetInfolist()[i].MenuName + ",  " + M_OrderInfo.GetInfolist()[i].OptionDes);

                    for (int j = 0; j < M_CreateInfo.GetCreatelist().Count; j++)
                    {
                        if (M_CreateInfo.GetCreatelist()[j].Menu == m_OrderInfo.MenuName)
                        {
                            M_CreateInfo.GetCreatelist()[j].Menu_Count = M_CreateInfo.GetCreatelist()[j].Menu_Count + 1;
                            createcheck = true;
                            break;
                        }
                    }
                    Console.WriteLine(string.Format("{0}  수정 완료\n 메뉴 : {1} \t\t 옵션 : {2}", m_OrderInfo.UserName, m_OrderInfo.MenuName, m_OrderInfo.OptionDes));
                    break;
                }
                else if (M_OrderInfo.GetInfolist()[i].UserName != m_OrderInfo.UserName) { }
            }
            if(createcheck != true && m_OrderInfo.MenuName != "")
            {
                M_CreateInfo.GetCreatelist().Add(new M_CreateInfo
                {
                    Menu = m_OrderInfo.MenuName,
                    Menu_Count = 1
                });
            }
            foreach (M_CreateInfo info in M_CreateInfo.GetCreatelist())
            {
                Console.WriteLine(info.Menu + info.Menu_Count);
            }
            string add = m_OrderInfo.UserName + "&" + m_OrderInfo.MenuName + "&" + m_OrderInfo.OptionDes;
            yield return add;
        }
    }
}
