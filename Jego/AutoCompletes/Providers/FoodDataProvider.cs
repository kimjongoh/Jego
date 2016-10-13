using dragonz.actb.provider;
using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moda.KString;
using System.Threading.Tasks;

namespace Jego.AutoCompletes.Providers {
    public class FoodDataProvider : IAutoCompleteDataProvider {
        private IEnumerable<string> _source;
        private IEnumerable<Food> foods;
        private Dictionary<string, Food> foodDic;

        public FoodDataProvider(IEnumerable<Food> source) {
            foods = source;
            _source = GetStringResource(source);
        }

        private List<string> GetStringResource(IEnumerable<Food> source) {
            foodDic = new Dictionary<string, Food>();
            List<string> stringFoods = new List<string>();

            foreach (Food food in source) {
                string foodStr = SerializeFood(food);
                foodDic.Add(foodStr, food);
                stringFoods.Add(foodStr);
            }

            return stringFoods;
        }

        private string SerializeFood(Food food) {
            string foodStr = "";
            foodStr += food.name + "  가격:" + food.unit_pirce;
            if (food.desc != null && !food.desc.Equals("")) {
                foodStr += "  설명:" + food.desc;
            }
            return foodStr;
        }

        public Food GetFood(string foodStr) {
            if (foodDic.ContainsKey(foodStr)) return foodDic[foodStr];
            else return null;
        }

        public IEnumerable<string> GetItems(string textPattern) {
            foreach (var item in _source) {
                if (item.KContains(textPattern)) {
                    yield return item;
                }
            }
        }
    }
}
