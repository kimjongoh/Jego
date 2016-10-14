using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Models {
    public class FoodRemain {
        public Food food { get; set; }
        public Remain remain { get; set; }

        public Remain todayRemain { get; set; }

    }
}
