using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JegoDatabase.Test;
using ExcelTemplateLib.Templates;
using ExcelTemplateLib.Managers;
using System.Transactions;
using JegoDatabase.Entities;
using JegoDatabase.Manager;
using System.Linq;
using System.Collections.Generic;


namespace JegoTestModule {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void InputFoodTest() {
            Food rice = new Food() { name = "쌀", type = "주식", unit = "kg", desc = "", unit_pirce = 3000 };
            Food ramen = new Food() { name = "컵라면", type = "주식", unit = "EA", desc = "", unit_pirce = 790 };
            Food bread = new Food() { name = "기린빵", type = "주식", unit = "EA", desc = "", unit_pirce = 600 };

            try {
                using (var context = DatabaseManager.GetContext()) {
                    DatabaseManager.PutFood(context, rice);
                    DatabaseManager.PutFood(context, ramen);
                    DatabaseManager.PutFood(context, bread);
                    DatabaseManager.SaveChanges(context);
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        [TestMethod]
        public void InputBuyTransTest() {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    List<Food> foods = DatabaseManager.GetAllFoods(context);
                    Food rice = foods.Where(e => e.name.Equals("쌀")).First();
                    Food ramen = foods.Where(e => e.name.Equals("컵라면")).First();
                    Food bread = foods.Where(e => e.name.Equals("기린빵")).First();

                    string date = DateTime.Now.ToString("yyyyMMdd");

                    BuyTrn riceBuy = CalculationManager.createBuyTrn(rice, 100, date, "");
                    DatabaseManager.PutBuyTrn(context, riceBuy);

                    BuyTrn ramenBuy = CalculationManager.createBuyTrn(ramen, 100, date, "");
                    DatabaseManager.PutBuyTrn(context, ramenBuy);

                    BuyTrn breadBuy = CalculationManager.createBuyTrn(bread, 100, date, "");
                    DatabaseManager.PutBuyTrn(context, breadBuy);

                    Remain riceRemain = CalculationManager.createRemain(rice.f_code, date, null, riceBuy, null);
                    Remain ramenRemain = CalculationManager.createRemain(ramen.f_code, date, null, ramenBuy, null);
                    Remain breadRemain = CalculationManager.createRemain(bread.f_code, date, null, breadBuy, null);
                    
                    Remain updatedRiceRemain = DatabaseManager.PutRemain(context, riceRemain);
                    if (updatedRiceRemain != null) {
                        DatabaseManager.RefreshAfterRemain(context, updatedRiceRemain);
                    }

                    Remain updatedRamenRemain = DatabaseManager.PutRemain(context, ramenRemain);
                    if (updatedRamenRemain != null) {
                        DatabaseManager.RefreshAfterRemain(context, updatedRamenRemain);
                    }

                    Remain updatedBreadRemain = DatabaseManager.PutRemain(context, breadRemain);
                    if (updatedBreadRemain != null) {
                        DatabaseManager.RefreshAfterRemain(context, updatedBreadRemain);
                    }

                    DatabaseManager.SaveChanges(context);
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        [TestMethod]
        public void readDateData() {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    string date = DateTime.Now.ToString("yyyyMMdd");

                    List<Remain> remains = DatabaseManager.GetTodayLastRemains(context, date);
                    Assert.AreEqual(remains.Count, 3);

                    List<BuyTrn> buyTrns = DatabaseManager.GetTodayBuyTrns(context, date);
                    Assert.AreEqual(buyTrns.Count, 0);

                    List<UseTrn> useTrns = DatabaseManager.GetTodayUseTrns(context, date);
                    Assert.AreEqual(useTrns.Count, 0);

                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        [TestMethod]
        public void writeExcelTest() {
            DetailExcelTemplateCreator testTemplete = new DetailExcelTemplateCreator(1, @"C:\test1.xls", DateTime.Now);
            ExcelManager.SaveExcel(testTemplete);
        }
    }

    
}
