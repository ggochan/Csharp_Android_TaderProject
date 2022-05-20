using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TaderServer.Models
{
    public class M_OrderInfo
    {
        public string UserName { get; set; }
        public string MenuName { get; set; }
        public string OptionDes { get; set; }
        public string MenuName_YN { get; set; }
        public string OptionDes_YN { get; set; }

        private static ObservableCollection<M_OrderInfo> Infolist;
        public static ObservableCollection<M_OrderInfo> GetInfolist()
        {
            if (Infolist == null)
                Infolist = new ObservableCollection<M_OrderInfo>();

            return Infolist;
        }
    }
}
