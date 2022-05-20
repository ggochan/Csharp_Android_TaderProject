using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TaderServer.Models
{
    public class M_MenuOption
    {
        public string Menu { get; set; }
        public string OptionDes { get; set; }

        private static ObservableCollection<M_MenuOption> Menuoptionlist;
        public static ObservableCollection<M_MenuOption> GetMenuoptionlist()
        {
            if (Menuoptionlist == null)
                Menuoptionlist = new ObservableCollection<M_MenuOption>();

            return Menuoptionlist;
        }
    }
}
