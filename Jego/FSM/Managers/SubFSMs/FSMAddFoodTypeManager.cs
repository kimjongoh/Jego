using Jego.FSM.Interfaces.FMS.Output;
using Jego.SharedPreferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jego.FSM.Managers.SubFSMs {
    public class FSMAddFoodTypeManager {
        private List<IFSMAddFoodTypeOutput> outputUIs;
        public FSMAddFoodTypeManager() {
            this.outputUIs = new List<IFSMAddFoodTypeOutput>();
        }

        public void addOutputUI(IFSMAddFoodTypeOutput outputUI) {
            outputUIs.Add(outputUI);
        }

        public void Process(string type) {
            bool isAdded = FoodTypeManager.AddFoodType(type);
            if (isAdded) {
                SpreadOutputUI(type);
            }
        }

        public void RefreshProcess(string type) {
            SpreadOutputUI(type);
        }

        private void SpreadOutputUI(string type) {
            foreach (IFSMAddFoodTypeOutput outputUI in outputUIs) {
                outputUI.IFSMO_AddFoodType(type);
            }
        }

        public void Refresh() {
            List<string> foodTypes = FoodTypeManager.GetFoodTypes();
            foreach (string type in foodTypes) {
                if (type != null && !type.Equals("")) {
                    RefreshProcess(type);
                }
            }
        }
    }
}
