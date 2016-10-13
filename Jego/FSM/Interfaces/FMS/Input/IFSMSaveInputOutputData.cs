using ExcelTemplateLib.DataModels;
using Jego.FSM.Models;
using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Interfaces.FMS.Input {
    public interface IFSMSaveInputOutputData {
        List<DayFoodModel> GetTodaySetDatas();
    }
}
