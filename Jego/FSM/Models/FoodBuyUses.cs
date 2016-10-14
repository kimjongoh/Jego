using Jego.FSM.Interfaces.FMS.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Models {
    public class FoodBuyUses : IFSMChangeDateParam {
        private List<FoodBuyUse> foodBuyUses;
        public FoodBuyUses(List<FoodBuyUse> foodBuyUses) {
            this.foodBuyUses = foodBuyUses;
        }

        public List<FoodBuyUse> GetFoodBuyUse() {
            return foodBuyUses;
        }
    }
}
