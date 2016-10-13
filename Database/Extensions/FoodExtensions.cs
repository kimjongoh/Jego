using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.Entity;

namespace Database.Extensions {
    public static class FoodExtensions {
        public static void CreateCode(this Food food) {
            if (food.name.isNotEmptyString()) {
                string code = food.name.Trim() + food.unit_pirce.ToString();

                if(food.desc.isNotEmptyString()) {
                    code += food.desc.Trim();
                }
                food.f_code = code;
            }
        }
       
    }
}
