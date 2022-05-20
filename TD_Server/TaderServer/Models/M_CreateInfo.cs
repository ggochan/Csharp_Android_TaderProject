using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TaderServer.Models
{
    public class M_CreateInfo
    {
        public string Menu { get; set; }
        public int Menu_Count { get; set; }

        private static ObservableCollection<M_CreateInfo> Createlist;
        public static ObservableCollection<M_CreateInfo> GetCreatelist()
        {
            if (Createlist == null)
                Createlist = new ObservableCollection<M_CreateInfo>();

            return Createlist;
        }
    }
}
