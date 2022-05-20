using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaderProject.CModels
{
    class C_PastOrder
    {
        public string PastKindName { get; set; }
        public string PastStoreName { get; set; }
        public string PastMenuName { get; set; }
        public string PastOptionDes { get; set; }

        private static ObservableCollection<C_PastOrder> pastlist;

        public static ObservableCollection<C_PastOrder> GetPastlist()
        {
            if (pastlist == null)
                pastlist = new ObservableCollection<C_PastOrder>();
            return pastlist;
        }
    }
}
