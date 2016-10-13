using ExcelTemplateLib.DataModels;
using Jego.Controls.MainPages.InputOutputControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Interfaces.FMS.Params {
    public interface IFSMFoodInfomationChangeParam {
        string GetExFCode();
        InputOutputItem GetInputOutputItem();
    }
}
