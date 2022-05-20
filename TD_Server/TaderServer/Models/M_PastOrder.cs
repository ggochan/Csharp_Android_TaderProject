using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TaderServer.Models
{
    public class M_PastOrder
    {
        public string PastKindName { get; set; }
        public string PastStoreName { get; set; }
        public string PastMenuName { get; set; }
        public string PastOptionDes { get; set; }

        private static ObservableCollection<M_PastOrder> pastlist;
        public static ObservableCollection<M_PastOrder> GetPastlist()
        {
            if (pastlist == null)
                pastlist = new ObservableCollection<M_PastOrder>();

            return pastlist;
        }
    }
}
