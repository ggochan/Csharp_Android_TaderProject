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
    public class OrderComboController : ControllerBase
    {
        private static readonly List<M_ComboKind> c_kindname = new List<M_ComboKind>();
        private static readonly List<M_ComboKind> c_kindall = new List<M_ComboKind>();
        private static readonly List<M_ComboStore> c_storename = new List<M_ComboStore>();
        private static readonly List<M_ComboMenu> c_menuname = new List<M_ComboMenu>();
        private static readonly List<M_ComboOption> c_option = new List<M_ComboOption>();
        DBConnection dbcon = new DBConnection();
        private DataSet kindds,kindall,storeds, menuds, optionds;

        public OrderComboController() { }

        [HttpGet("allk")]
        public IEnumerable<M_ComboKind> GetByKindAll()
        {
            c_kindall.Clear();
            kindall = dbcon.SelectKindAll();
            foreach (DataRow r in kindall.Tables[0].Rows)
            {
                c_kindall.Add(new M_ComboKind
                {
                   ComboKind = r["KindName"].ToString()
                });
            }
            return c_kindall;
        }
        [HttpGet("kindname/{kindname}")]
        public IEnumerable<M_ComboKind> GetByKind(string kindname)
        {
            c_kindname.Clear();
            Console.WriteLine("메시지"+ kindname);
            kindds = dbcon.SelectKind(kindname);
            foreach (DataRow r in kindds.Tables[0].Rows)
            {
                c_kindname.Add(new M_ComboKind
                {
                    ComboKind = r["KindName"].ToString()
                });
                Console.WriteLine(r["KindName"].ToString());
            }
            return c_kindname;
        }

        [HttpGet("storename/{kindname}")]
        public IEnumerable<M_ComboStore> GetByStore(string kindname)
        {
            c_storename.Clear();

            storeds = dbcon.Kind_SelectStore(kindname);
            foreach (DataRow r in storeds.Tables[0].Rows)
            {
                c_storename.Add(new M_ComboStore
                {
                    ComboStore = r["StoreName"].ToString()
                });
            }
            return c_storename;
        }
        [HttpGet("storename/select/{storename}")] // 가게명 검색
        public IEnumerable<M_ComboStore> GetBySelectStore(string storename)
        {
            c_storename.Clear();
            string[] splstring = storename.Split('&');
            storeds = dbcon.Select_temp(string.Format("select StoreName from foodstoretb where kindID IN(select kindID from foodkindTB where Kindname like '%{0}%') and Storename like '%{1}%'", splstring[0], splstring[1]));
            foreach (DataRow r in storeds.Tables[0].Rows)
            {
                c_storename.Add(new M_ComboStore
                {
                    ComboStore = r["StoreName"].ToString()
                });
            }
            return c_storename;
        }
        [HttpGet("menu/{storename}")]
        public IEnumerable<M_ComboMenu> GetByMenu(string storename)
        {
            c_menuname.Clear();

            menuds = dbcon.Store_SelectOneMenu(storename);
            foreach (DataRow r in menuds.Tables[0].Rows)
            {
                c_menuname.Add(new M_ComboMenu
                {
                    MenuCombo = r["MenuName"].ToString()
                });
            }
            return c_menuname;
        }
        [HttpGet("menu/select/{storename}")]
        public IEnumerable<M_ComboMenu> GetBySelectMenu(string storename)
        {
            c_menuname.Clear();
            string[] splstring = storename.Split('&');
            menuds = dbcon.Select_temp(string.Format("select Menuname from foodmenutb where storeid IN(select storeid from foodstoretb where storename like '%{0}%') and menuname like '%{1}%'", splstring[0], splstring[1]));
            foreach (DataRow r in menuds.Tables[0].Rows)
            {
                c_menuname.Add(new M_ComboMenu
                {
                    MenuCombo = r["MenuName"].ToString()
                });
            }
            return c_menuname;
        }
        [HttpGet("option/{storename}")]
        public IEnumerable<M_ComboOption> GetByOption(string storename)
        {
            c_option.Clear();

            optionds = dbcon.Store_SelectOneOption(storename);
            foreach (DataRow r in optionds.Tables[0].Rows)
            {
                c_option.Add(new M_ComboOption
                {
                    OptionCombo = r["OptionDes"].ToString()
                });
            }
            return c_option;
        }
        [HttpGet("option/select/{storename}")]
        public IEnumerable<M_ComboOption> GetBySelectOption(string storename)
        {
            c_option.Clear();
            string[] splstring = storename.Split('&');
            optionds = dbcon.Select_temp(string.Format("select optiondes from foodoptiontb where storeid IN(select storeid from foodstoretb where storename like '%{0}%') and optiondes like '%{1}%'", splstring[0], splstring[1]));
            foreach (DataRow r in optionds.Tables[0].Rows)
            {
                c_option.Add(new M_ComboOption
                {
                    OptionCombo = r["OptionDes"].ToString()
                });
            }
            return c_option;
        }
    }
}
