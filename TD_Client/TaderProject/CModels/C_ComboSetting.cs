using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TaderProject.CModels
{
        public class M_ComboKind
        {
            public string ComboKind { get; set; }

            private static ObservableCollection<M_ComboKind> listkind;

            public static ObservableCollection<M_ComboKind> GetKind()
            {
                if (listkind == null)
                listkind = new ObservableCollection<M_ComboKind>();
                return listkind;
            }
          
        }
    
    public class M_ComboStore
    {
        public string ComboStore { get; set; }

        private static ObservableCollection<M_ComboStore> liststore;

        public static ObservableCollection<M_ComboStore> GetStore()
        {
            if (liststore == null)
                liststore = new ObservableCollection<M_ComboStore>();
         
            return liststore;
        }
    }
        public class M_ComboMenuPrice
        {
            public string ComboMenu { get; set; }

            public int ComboMenuPrice { get; set; }

            private static ObservableCollection<M_ComboMenuPrice> listmenuprice;

            public static ObservableCollection<M_ComboMenuPrice> GetMenuprice()
            {
                if (listmenuprice == null)
                listmenuprice = new ObservableCollection<M_ComboMenuPrice>();
                return listmenuprice;
            }
        }
    public class M_ComboMenu
    {
        public string MenuCombo { get; set; }

        private static ObservableCollection<M_ComboMenu> listmenu;

        public static ObservableCollection<M_ComboMenu> GetMenu()
        {
            if (listmenu == null)
                listmenu = new ObservableCollection<M_ComboMenu>();
            return listmenu;
        }
    }
    public class M_ComboOptionPrice
    {
        public string ComboOption { get; set; }

        public int ComboOptionPrice { get; set; }

        private static ObservableCollection<M_ComboOptionPrice> listoptionprice;

        public static ObservableCollection<M_ComboOptionPrice> GetOptionprice()
        {
            if (listoptionprice == null)
                listoptionprice = new ObservableCollection<M_ComboOptionPrice>();
            return listoptionprice;
        }
    }

    public class M_ComboOption
        {
            public string OptionCombo { get; set; }

            private static ObservableCollection<M_ComboOption> listoption;

            public static ObservableCollection<M_ComboOption> GetOption()
            {
                if (listoption == null)
                listoption = new ObservableCollection<M_ComboOption>();
                return listoption;
            }
          }

}
