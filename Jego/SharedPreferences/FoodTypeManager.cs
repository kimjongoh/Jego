using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jego.SharedPreferences {
    public class FoodTypeManager {
        public static List<string> GetFoodTypes() {
            string types = FoodTypes.Default.types;
            return spliteTypes(types);
        }

        public static bool AddFoodType(string type) {
            List<string> foodTypes = GetFoodTypes();
            if (!foodTypes.Contains(type)) {
                foodTypes.Add(type);
                saveTypes(foodTypes);
                return true;
            } else {
                return false;
            }
        }

        public static void saveTypes(List<string> foodTypes) {
            string typeString = "";
            for (int i = 0; i < foodTypes.Count; i++) {
                if(i != 0) {
                    typeString += ",";
                }
                typeString += foodTypes[i];
            }
            FoodTypes.Default.types = typeString;
            FoodTypes.Default.Save();
        }

        private static List<string> spliteTypes(string types) {
            string[] datas = types.Split(',');
            return datas.ToList();
        }

        public static void RemoveFoodType(string type) {
            List<string> foodTypes = GetFoodTypes();
            if (foodTypes.Contains(type)) {
                foodTypes.Remove(type);
                saveTypes(foodTypes);
            }
            
        }
    }
}
