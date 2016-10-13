using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelTemplateLib.DataModels {
    public class DayFoodModel {
        public Food food { get; set; }
        public BuyTrn buyTrn { get; set; }
        public UseTrn useTrn { get; set; }
        public Remain remain { get; set; }
    }
}
