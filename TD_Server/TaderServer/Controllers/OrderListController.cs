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
    public class OrderListController: ControllerBase
    {
       DBConnection dbb = new DBConnection();

        public OrderListController() {}

        [HttpGet]
        public IEnumerable<string> GetByAll()
        {
            string temp = "";
            foreach(M_OrderList item in M_OrderList.GetOrderlist())
            {
                temp += item.KindName;
                temp += "&";
                temp += item.StoreName;
            }
            
            yield return temp;
        }

        [HttpGet("Count")] // 카운트 도달
        public IEnumerable<string> GetCountMax()
        {
            string useridcheck = "";
            try
            {
                if (M_OrderList.GetOrderlist()[0].Count > M_OrderInfo.GetInfolist().Count)
                {
                    int i = M_OrderInfo.GetInfolist().Count;
                    Console.WriteLine("현재 주문 수 : " + i);
                    Console.WriteLine("완료 주문 수 : " + M_OrderList.GetOrderlist()[0].Count);
                    useridcheck= "zopweiqushdzasdwqfngl";
                }
                else if (M_OrderList.GetOrderlist()[0].Count <= M_OrderInfo.GetInfolist().Count)
                {
                    useridcheck = M_OrderInfo.GetInfolist()[0].UserName;
                }
            }
            catch (Exception) { }
            Console.WriteLine("체크");
            yield return useridcheck;
        }
        
        [HttpGet("orderbool/{orderbool}")] // 존재여부
        public IEnumerable<string> GetByKind(string orderbool)
        {
            var test = M_OrderList.GetOrderlist().FirstOrDefault(p => p.Orderbool == orderbool);

            if (test == null)
            {
                yield return "f";
            }
            else
            {
                yield return "t";
            }
        }

        [HttpPost]
        public IEnumerable<string> Create([FromBody] M_OrderList m_list)
        {
            M_OrderList.GetOrderlist().Add(new M_OrderList
            {
                Orderbool = "t", // t = 있다, f = 없다.
                KindName = m_list.KindName,
                StoreName = m_list.StoreName,
                Count = m_list.Count,
                KindName_YN = "N", //DB에 따라 Y/N
                StoreName_YN = "N"
            });
            yield return "주문완료";
        }
    }
}
