using ExcelTemplateLib.DataModels;
using Jego.FSM.Interfaces.FMS.Params;
using JegoDatabase.Entities;
using JegoDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Models {
    public class DayUpdateData {
        private List<DayFoodModel> dayModels;
        private List<Remain> remains;

        public DayUpdateData(List<DayFoodModel> dayModels, List<Remain> remains) {
            this.dayModels = dayModels;
            this.remains = remains;
        }
        public List<DayFoodModel> getDayModels() {
            return dayModels;
        }

        public List<Remain> getRemains() {
            return remains;
        }
    }
}
