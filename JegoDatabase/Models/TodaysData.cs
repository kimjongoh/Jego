using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JegoDatabase.Models {
    public class TodaysData {
        public List<BuyTrn> buyTrns {get; set;}
        public List<UseTrn> useTrns { get; set; }
        public List<Remain> remains { get; set; }

        public List<Remain> todayRemains { get; set; }

        public TodaysData() { }
    }
}
