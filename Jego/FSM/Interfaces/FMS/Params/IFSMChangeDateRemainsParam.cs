using Jego.FSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Interfaces.FMS.Params {
    public interface IFSMChangeDateRemainsParam {
        public List<FoodRemain> GetFoodRemain();
    }
}
