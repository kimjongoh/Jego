using JegoDatabase.Entities;
using JegoDatabase.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.AutoCompletes.Providers {
    public class FoodDataProviderManager {
        private static FoodDataProvider provider;
        public static FoodDataProvider getInstance() {
            if (provider == null) {
                List<Food> foodList = JegoManager.GetAllFoods();
                provider = new FoodDataProvider(foodList);
            }
            return provider;
        }
    }
}
