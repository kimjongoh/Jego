using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database.Entity;
using Database.Manager;
using System.Data.Entity;



namespace JegoUnitTestModule {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void InputFoodTest() {
            Food food = new Food(){ name="쌀", type="주식", unit="kg", desc="", unit_pirce=3000 };
            using (JegoEntities context = DatabaseManager.GetContext()) {

                DatabaseManager.PutFood(context, food);
                DatabaseManager.SaveChanges(context);

            }
            
        }
    }
}
