using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaderServer.Models
{
    public class M_ComboKind
    {
        public string ComboKind { get; set; }
    }
    public class M_ComboStore
    {
        public string ComboStore { get; set; }
    }
    public class M_ComboMenuPrice
    {
        public string ComboMenu { get; set; }
        public int ComboMenuPrice { get; set; }
    }
    public class M_ComboMenu
    {
        public string MenuCombo { get; set; }
    }
    public class M_ComboOptionPrice
    {
        public string ComboOption { get; set; }
        public int ComboOptionPrice { get; set; }
    }
    public class M_ComboOption
    {
        public string OptionCombo { get; set; }
    }
  
}
