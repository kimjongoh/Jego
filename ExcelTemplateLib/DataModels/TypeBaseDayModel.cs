using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTemplateLib.DataModels {
    public class TypeBaseDayModel {
        public string type { get; set; }
        public Dictionary<string, DayFoodModel> dataList { get; set; }

        public TypeBaseDayModel(string type) {
            this.type = type;
            dataList = new Dictionary<string, DayFoodModel>();
        }
    }
}
