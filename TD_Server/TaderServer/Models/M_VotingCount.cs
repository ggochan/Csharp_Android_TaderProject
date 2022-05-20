using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TaderServer.Models
{
    public class M_VotingCount
    {
        public int Votingnum { get; set; }
        public string Votinguser { get; set; }

        private static ObservableCollection<M_VotingCount> countlist;
        public static ObservableCollection<M_VotingCount> GetCountlist()
        {
            if (countlist == null)
                countlist = new ObservableCollection<M_VotingCount>();

            return countlist;
        }
    }
}
