using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TaderServer.Models
{
    public class M_OrderList
    {
        // Y = 있다, N = 없다 
        public string Orderbool { get; set; }
        public string KindName { get; set; }
        public string StoreName { get; set; }
        public int Count { get; set; }
        public string KindName_YN { get; set; }
        public string StoreName_YN { get; set; }

        private static ObservableCollection<M_OrderList> Orderlist;
        public static ObservableCollection<M_OrderList> GetOrderlist()
        {
            if (Orderlist == null)
                Orderlist = new ObservableCollection<M_OrderList>();

            return Orderlist;
        }
    }
}
