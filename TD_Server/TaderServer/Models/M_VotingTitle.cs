using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TaderServer.Models
{
    public class M_VotingTitle
    {
        public string Votingbool { get; set; }
        public string Votingtitle { get; set; }
        public string Votingmemo { get; set; }
        public string Votingday { get; set; }
        public string Votingyn { get; set; }

        private static ObservableCollection<M_VotingTitle> titlelist;
        public static ObservableCollection<M_VotingTitle> GetTitlelist()
        {
            if (titlelist == null)
                titlelist = new ObservableCollection<M_VotingTitle>();

            return titlelist;
        }
    }
}
