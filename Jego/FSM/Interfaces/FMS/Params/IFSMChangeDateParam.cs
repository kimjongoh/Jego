using ExcelTemplateLib.DataModels;
using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jego.FSM.Interfaces.FMS.Params {
    public interface IFSMChangeDateParam {
        List<DayFoodModel> getDayModels();
        List<Remain> getRemains();
    }
}
