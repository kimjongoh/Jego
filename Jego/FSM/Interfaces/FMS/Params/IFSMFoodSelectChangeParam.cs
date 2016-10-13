using ExcelTemplateLib.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Interfaces.FMS.Params {
    public interface IFSMFoodSelectChangeParam {
        DayFoodModel GetDayModel();
    }
}
