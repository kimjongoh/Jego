using ExcelTemplateLib.DataModels;
using Jego.Controls.MainPages.InputOutputControls;
using Jego.FSM.Interfaces.FMS.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Models {
    public class FoodInfomationChangeModel : IFSMFoodInfomationChangeParam {
        private string ex_f_code { get; set; }
        private InputOutputItem intputOutputItem { get; set; }
        public FoodInfomationChangeModel(string ex_f_code, InputOutputItem dayFoodModel) {
            this.ex_f_code = ex_f_code;
            this.intputOutputItem = dayFoodModel;
        }

        public string GetExFCode() {
            return ex_f_code;
        }

        public InputOutputItem GetInputOutputItem() {
            return intputOutputItem;
        }
    }
}
