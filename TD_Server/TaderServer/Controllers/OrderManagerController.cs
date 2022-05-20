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
    public class OrderManagerController : ControllerBase
    {
        private static readonly List<M_ComboKind> c_kindname = new List<M_ComboKind>();
        private static readonly List<M_ComboStore> c_storename = new List<M_ComboStore>();
        private static readonly List<M_ComboMenuPrice> c_menunameprice = new List<M_ComboMenuPrice>();
        private static readonly List<M_ComboOptionPrice> c_optionprice = new List<M_ComboOptionPrice>();

        DBConnection dbcon = new DBConnection();
        private DataSet kindds, storeds, menupriceds, optionpriceds, pastds; 
        private string postcheck = "t";

        public OrderManagerController() { }

        #region 종류 GET/POST
        [HttpGet("kindname")]
        public IEnumerable<M_ComboKind> GetByKind()
        {
            c_kindname.Clear();
            kindds = dbcon.SelectKindAll();
            foreach (DataRow r in kindds.Tables[0].Rows)
            {
                c_kindname.Add(new M_ComboKind
                {
                    ComboKind = r["KindName"].ToString()
                });
            }
            return c_kindname;
        }
        [HttpPost("kindname/insert")]
        public IEnumerable<string> PostKindInsert([FromBody] M_ComboKind kindname)
        {
            try
            {
                dbcon.InsertKind(kindname.ComboKind);
                postcheck = "t";
            }
            catch(Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        [HttpPost("kindname/update")]
        public IEnumerable<string> PostKindUpdate([FromBody] M_ComboKind kindname)
        {
            try
            {
                string[] splstring = kindname.ComboKind.Split('&');
                dbcon.UpdateKind(splstring[0], splstring[1]);
                postcheck = "t";
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        [HttpPost("kindname/delete")]
        public IEnumerable<string> PostKindDelete([FromBody] M_ComboKind kindname)
        {
            try
            {
                dbcon.DeleteKind(kindname.ComboKind);
                postcheck = "t";
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        #endregion

        #region 가게명 GET/POST
        [HttpGet("storename/{kindname}")]
        public IEnumerable<M_ComboStore> GetByStore(string kindname)
        {
            try
            {
                c_storename.Clear();

                storeds = dbcon.Kind_SelectStore(kindname);
                foreach (DataRow r in storeds.Tables[0].Rows)
                {
                    c_storename.Add(new M_ComboStore
                    {
                        ComboStore = r["StoreName"].ToString()
                    });
                    Console.WriteLine(r["StoreName"].ToString());
                }
                return c_storename;
            }
            catch
            { return null; }           
        }
        [HttpPost("storename/insert")]
        public IEnumerable<string> PostStoreInsert([FromBody] M_ComboStore storename)
        {
            try
            {
                string[] splstring = storename.ComboStore.Split('&');
                dbcon.InsertStore(splstring[0],splstring[1]); // 가게명, 종류
                postcheck = "t";
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        [HttpPost("storename/update")]
        public IEnumerable<string> PostStoreUpdate([FromBody] M_ComboStore storename)
        {
            try
            {
                string[] splstring = storename.ComboStore.Split('&');
                dbcon.UpdateStore(splstring[0], splstring[1], splstring[2]); // 신 , 구 , 종류
                postcheck = "t";
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        [HttpPost("storename/delete")]
        public IEnumerable<string> PostStoreDelete([FromBody] M_ComboStore storename)
        {
            try
            {
                string[] splstring = storename.ComboStore.Split('&');
                dbcon.DeleteStore(splstring[0], splstring[1]); // 가게명 종류
                postcheck = "t";
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        #endregion

        #region 메뉴 GET/POST

       [HttpGet("menu/{storename}")]
        public IEnumerable<M_ComboMenuPrice> GetByMenu(string storename)
        {
            try
            {
                c_menunameprice.Clear();
                Console.WriteLine("메뉴 :"  + storename);
                menupriceds = dbcon.Store_SelectMenu(storename);
                foreach (DataRow r in menupriceds.Tables[0].Rows)
                {
                    c_menunameprice.Add(new M_ComboMenuPrice
                    {
                        ComboMenu = r["MenuName"].ToString(),
                        ComboMenuPrice = int.Parse(r["MenuPrice"].ToString())
                    });
                    Console.WriteLine("메뉴 : "+r["MenuName"].ToString() + "\t가격 : " +r["MenuPrice"].ToString());
                }
                return c_menunameprice;
            }
            catch
            { return null; }
        }
        [HttpPost("menu/insert")]
        public IEnumerable<string> PostMenuInsert([FromBody] M_ComboMenuPrice menuname)
        {
            try
            {
                string[] splstring = menuname.ComboMenu.Split('&');
                dbcon.InsertMenu(splstring[0] ,menuname.ComboMenuPrice.ToString(),splstring[1]); // 메뉴, 가게명, 가격
                postcheck = "t";
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        [HttpPost("menu/update")]
        public IEnumerable<string> PostMenuUpdate([FromBody] M_ComboMenuPrice menuname)
        {
            try
            {
                string[] splstring = menuname.ComboMenu.Split('&');
                dbcon.UpdateMenu(splstring[0], menuname.ComboMenuPrice.ToString(), splstring[1], splstring[2]); // 신 ,가격 ,구 , 가게명
                postcheck = "t";
                Console.WriteLine(splstring[0] + menuname.ComboMenuPrice.ToString() + splstring[1] + splstring[2]);
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        [HttpPost("menu/delete")]
        public IEnumerable<string> PostMenuDelete([FromBody] M_ComboMenuPrice menuname)
        {
            try
            {
                string[] splstring = menuname.ComboMenu.Split('&');
                dbcon.DeleteMenu(splstring[0], splstring[1]); // 메뉴 종류
                postcheck = "t";
                Console.WriteLine(splstring[0] + splstring[1]);
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        #endregion

        #region 옵션 GET/POST
        [HttpGet("option/{storename}")]
        public IEnumerable<M_ComboOptionPrice> GetByOption(string storename)
        {
            try
            {
                c_optionprice.Clear();
                Console.WriteLine("옵션 :" + storename);
                optionpriceds = dbcon.Store_SelectOption(storename);
                foreach (DataRow r in optionpriceds.Tables[0].Rows)
                {
                    c_optionprice.Add(new M_ComboOptionPrice
                    {
                        ComboOption = r["OptionDes"].ToString(),
                        ComboOptionPrice = int.Parse(r["OptionPrice"].ToString())
                    });
                    Console.WriteLine("옵션 : " + r["OptionDes"].ToString() + "\t가격 : " + r["OptionPrice"].ToString());
                }
                return c_optionprice;
            }
            catch
            { return null; }
        }
        [HttpPost("option/insert")]
        public IEnumerable<string> PostOptionInsert([FromBody] M_ComboOptionPrice optiondes)
        {
            try
            {
                string[] splstring = optiondes.ComboOption.Split('&');
                dbcon.InsertOption(splstring[0],optiondes.ComboOptionPrice.ToString(), splstring[1]); // 옵션, 가격, 가게명
                postcheck = "t";
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        [HttpPost("option/update")]
        public IEnumerable<string> PostOptionUpdate([FromBody] M_ComboOptionPrice optiondes)
        {
            try
            {
                string[] splstring = optiondes.ComboOption.Split('&');
                dbcon.UpdateOption(splstring[0], optiondes.ComboOptionPrice.ToString(), splstring[1], splstring[2]); // 신 ,가격 ,구 , 가게명
                postcheck = "t";
                Console.WriteLine(splstring[0] +  optiondes.ComboOptionPrice.ToString() +  splstring[1], splstring[2]);
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        [HttpPost("option/delete")]
        public IEnumerable<string> PostOptionDelete([FromBody] M_ComboOptionPrice optiondes)
        {
            try
            {
                string[] splstring = optiondes.ComboOption.Split('&');
                dbcon.DeleteOption(splstring[0], splstring[1]); // 메뉴 종류
                postcheck = "t";
                Console.WriteLine(splstring[0] +  splstring[1]);
            }
            catch (Exception)
            {
                postcheck = "f";
            }
            yield return postcheck;
        }
        #endregion

        #region 과거 내역 불러오기
        [HttpGet("past/{objectname}")]
        public IEnumerable<M_PastOrder> GetByPastOrder(string objectname)
        {
            try
            {
                string[] str_temp = objectname.Split('&');

                M_PastOrder.GetPastlist().Clear();                
                pastds = dbcon.Select_temp(string.Format("select PastKindName, PastStoreName, PastMenuName, PastOptionDes from pastordertb Where {0} like '{1}'",str_temp[0],str_temp[1]));
                foreach (DataRow r in pastds.Tables[0].Rows)
                {
                    M_PastOrder.GetPastlist().Add(new M_PastOrder
                    {
                        PastKindName = r["PastKindName"].ToString(),
                        PastStoreName = r["PastStoreName"].ToString(),
                        PastMenuName = r["PastMenuName"].ToString(),
                        PastOptionDes = r["PastOptionDes"].ToString()
                    });
                }
                Console.WriteLine("과거 내역 불러오기");
                pastds = dbcon.Select_temp("SELECT LAST_NUMBER FROM USER_SEQUENCES WHERE SEQUENCE_NAME = 'PAST_SEQ'");
                foreach (DataRow r in pastds.Tables[0].Rows)
                {
                    if(int.Parse(r["LAST_NUMBER"].ToString())== 1000)
                    {
                        dbcon.DeleteALL_temp("Truncate Table PastOrderTB");
                    }
                }
                return M_PastOrder.GetPastlist();
            }
            catch
            { return null; }
        }
        #endregion
    }
}
