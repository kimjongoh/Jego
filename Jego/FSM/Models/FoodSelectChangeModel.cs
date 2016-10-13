using ExcelTemplateLib.DataModels;
using Jego.FSM.Interfaces.FMS.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Models {
    public class FoodSelectChangeModel : IFSMFoodSelectChangeParam {
        private DayFoodModel dayModel;

        public FoodSelectChangeModel(DayFoodModel dayModel) {
            this.dayModel = dayModel;
        }

        public DayFoodModel GetDayModel() {
            return dayModel;
        }
    }
}
