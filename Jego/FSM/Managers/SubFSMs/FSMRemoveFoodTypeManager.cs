using Jego.FSM.Interfaces.FMS.Output;
using Jego.SharedPreferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Jego.FSM.Managers.SubFSMs {
    public class FSMRemoveFoodTypeManager {

        private List<IFSMRemoveFoodTypeOutput> outputUIs;
        public FSMRemoveFoodTypeManager() {
            outputUIs = new List<IFSMRemoveFoodTypeOutput>();
        }

        public void addOutput(IFSMRemoveFoodTypeOutput outputUI) {
            outputUIs.Add(outputUI);
        }

        public void Process(string type) {
            removeFromPreference(type);
            SpreadUI(type);
        }

        private void removeFromPreference(string type) {
            FoodTypeManager.RemoveFoodType(type);
        }

        private void SpreadUI(string type) {
            foreach (IFSMRemoveFoodTypeOutput outputUI in outputUIs) {
                outputUI.IFSMO_RemoveFoodType(type);
            }
        }
    }
}
