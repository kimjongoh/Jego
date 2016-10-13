using JegoDatabase.Entities;
using JegoDatabase.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace JegoDatabase.Test {
    public class TestClass {
        public void test01() {
            using(var transactionScope = new TransactionScope(TransactionScopeOption.Required)) {
                Food food = new Food() { name = "쌀", type = "주식", unit = "kg", desc = "", unit_pirce = 3000 };

                try {
                    using (var context = DatabaseManager.GetContext()) {
                        DatabaseManager.PutFood(context, food);
                        DatabaseManager.SaveChanges(context);
                    }
                    transactionScope.Complete();
                } catch (Exception e) {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        
    }
}
