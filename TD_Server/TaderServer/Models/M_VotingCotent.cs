using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace TaderServer.Models
{
    public class M_VotingCotent
    {
        public int Votingnum { get; set; }
        public int Votingcontentcnt { get; set; }
        public string Votingcon { get; set; }

        private static ObservableCollection<M_VotingCotent> conlist;
        public static ObservableCollection<M_VotingCotent> GetConlist()
        {
            if (conlist == null)
                conlist = new ObservableCollection<M_VotingCotent>();

            return conlist;
        }
    }
}
