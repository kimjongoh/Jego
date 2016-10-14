using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JegoDatabase.Manager {
    public class CalculationManager {

        public static BuyTrn createBuyTrn(Food food, decimal amount, string date, string deadline) {
            BuyTrn buyTrn = new BuyTrn();
            buyTrn.f_code = food.f_code;
            buyTrn.amount = amount;
            buyTrn.total_price = buyTrn.amount * food.unit_pirce;
            buyTrn.date = date;
            buyTrn.deadline = deadline;
            return buyTrn;
        }

        public static UseTrn createUseTrn(Food food, decimal amount, string date, string deadline) {
            UseTrn useTrn = new UseTrn();
            useTrn.f_code = food.f_code;
            useTrn.amount = amount;
            useTrn.total_price = useTrn.amount * food.unit_pirce;
            useTrn.date = date;
            useTrn.deadline = deadline;
            return useTrn;
        }

        
    }
}
