using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Models {
    public class TodaySetModel {
        public BuyTrn buyTrn { get; set; }
        public UseTrn useTrn { get; set; }
        public Remain exRemain { get; set; }
        public Remain todayRemain { get; set; }
    }
}
