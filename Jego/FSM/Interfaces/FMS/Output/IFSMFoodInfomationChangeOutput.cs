using Jego.FSM.Interfaces.FMS.Params;
using Jego.FSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Interfaces.FMS.Output {
    public interface IFSMFoodInfomationChangeOutput {
        void IFSMOFoodInfomationChangeOutput(IFSMFoodInfomationChangeParam param);
    }
}
