using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Models {
    public class FoodBuyUse {
        public Food food { get; set; }
        public BuyTrn buyTrn { get; set; }
        public UseTrn useTrn { get; set; }
    }
}
