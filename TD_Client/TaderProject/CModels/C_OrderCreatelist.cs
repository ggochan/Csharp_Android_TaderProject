using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MenuList Models
namespace TaderProject.CModels
{
    class C_OrderCreatelist
    {
        public string Num { get; set; }
        public string Menu { get; set; }
        public int Menu_Count { get; set; }

        private static List<C_OrderCreatelist> list;

        public static List<C_OrderCreatelist> GetList()
        {
            if (list == null)
                list = new List<C_OrderCreatelist>();
            return list;
        }
    }
}
