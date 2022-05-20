using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TaderServer.Models
{
    public class M_VotingAll
    {
        public string Alltitle { get; set; }
        public string Allmemo { get; set; }
        public string Allday { get; set; }
        public string Allyn { get; set; }
        public string Allcontent { get; set; }
        public string Allcount { get; set; }
        public string Alluser { get; set; }

        public int Allcountall { get; set; }

        private static ObservableCollection<M_VotingAll> voalllist;
        public static ObservableCollection<M_VotingAll> GetVoAlllist()
        {
            if (voalllist == null)
                voalllist = new ObservableCollection<M_VotingAll>();

            return voalllist;
        }
    }
}
