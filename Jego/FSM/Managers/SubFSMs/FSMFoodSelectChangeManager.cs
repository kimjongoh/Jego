using ExcelTemplateLib.DataModels;
using Jego.FSM.Interfaces.FMS.Output;
using Jego.FSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Managers.SubFSMs {
    public class FSMFoodSelectChangeManager {

        private List<IFSMFoodSelectChangeOutput> outputList;

        public FSMFoodSelectChangeManager() {
            outputList = new List<IFSMFoodSelectChangeOutput>();
        }

        public void AddOutputList(IFSMFoodSelectChangeOutput output) {
            outputList.Add(output);
        }

        public void Process(DayFoodModel dayModel) {
            FoodSelectChangeModel foodChangeModel = new FoodSelectChangeModel(dayModel);
            foreach (IFSMFoodSelectChangeOutput output in outputList) {
                output.IFSMOFoodSelectChangeOutput(foodChangeModel);
            }
        }
    }
}
