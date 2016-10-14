using Jego.FSM.Interfaces.FMS.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Models {
    public class FoodRemains : IFSMChangeDateRemainsParam {
        private List<FoodRemain> foodRemains;
        public FoodRemains(List<FoodRemain> foodRemains) {
            this.foodRemains = foodRemains;
        }

        public List<FoodRemain> GetFoodRemain() {
            return foodRemains;
        }
    }
}
