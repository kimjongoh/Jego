using Jego.FSM.Interfaces.FMS.Output;
using Jego.FSM.Interfaces.FMS.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Managers.SubFSMs {
    public class FSMFoodInfomationChangeManager {
        private List<IFSMFoodInfomationChangeOutput> outputs;
        public FSMFoodInfomationChangeManager() {
            outputs = new List<IFSMFoodInfomationChangeOutput>();
        }

        public void AddOutput(IFSMFoodInfomationChangeOutput output) {
            outputs.Add(output);
        }

        public void Process(IFSMFoodInfomationChangeParam param) {
            foreach (IFSMFoodInfomationChangeOutput output in outputs) {
                output.IFSMOFoodInfomationChangeOutput(param);
            }
        }
    }
}
