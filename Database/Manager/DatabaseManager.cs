using Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.Extensions;

namespace Database.Manager {
    public class DatabaseManager {
        public static JegoEntities GetContext() {
            return new JegoEntities();
        }

        public static void PutFood(JegoEntities context, Food food) {
            if (!food.f_code.isNotEmptyString()) {
                food.CreateCode();
                AddFood(context, food);
            }
        }

        public static void AddFood(JegoEntities context, Food food) {
            context.Food.Add(food);
        }



        public static Food GetFoodByCode(JegoEntities context, string f_code) {
            var foods = context.Food.Where(e => e.f_code.Equals(f_code));
            if (foods.Count() == 0) {
                return foods.First();
            } else {
                return null;
            }
        }

        public static void SaveChanges(JegoEntities context) {
            context.SaveChanges();
        }
    }
}
