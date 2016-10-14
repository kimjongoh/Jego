using ExcelTemplateLib.DataModels;
using Jego.FSM.Models;
using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jego.FSM.Interfaces.FMS.Params {
    public interface IFSMChangeDateParam {
        List<FoodBuyUse> GetFoodBuyUse();
    }
}
