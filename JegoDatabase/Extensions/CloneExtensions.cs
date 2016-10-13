using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JegoDatabase.Extensions {
    public static class CloneExtensions {
        public static Food Clone(this Food food) {
            Food newFood = new Food();
            newFood.desc = food.desc;
            newFood.f_code = food.f_code;
            newFood.name = food.name;
            newFood.type = food.type;
            newFood.unit = food.unit;
            newFood.unit_pirce = food.unit_pirce;
            return newFood;
        }

        public static BuyTrn Clone(this BuyTrn buyTrn) {
            BuyTrn newBuyTrn = new BuyTrn();
            newBuyTrn.amount = buyTrn.amount;
            newBuyTrn.date = buyTrn.date;
            newBuyTrn.deadline = buyTrn.deadline;
            newBuyTrn.f_code = buyTrn.f_code;
            newBuyTrn.total_price = buyTrn.total_price;
            newBuyTrn.trn_idx = buyTrn.trn_idx;
            return newBuyTrn;
        }

        public static UseTrn Clone(this UseTrn useTrn) {
            UseTrn newUseTrn = new UseTrn();
            newUseTrn.amount = useTrn.amount;
            newUseTrn.date = useTrn.date;
            newUseTrn.deadline = useTrn.deadline;
            newUseTrn.f_code = useTrn.f_code;
            newUseTrn.total_price = useTrn.total_price;
            newUseTrn.trn_idx = useTrn.trn_idx;
            return newUseTrn;
        }

        public static Remain Clone(this Remain remain) {
            Remain newRemain = new Remain();
            newRemain.amount = remain.amount;
            newRemain.date = remain.date;
            newRemain.f_code = remain.f_code;
            newRemain.fh_code = remain.fh_code;
            newRemain.deadline = remain.deadline;
            return newRemain;
        }

        public static List<Food> Clone(this List<Food> foods) {
            List<Food> newFoods = new List<Food>();
            foreach (Food food in foods) {
                newFoods.Add(food.Clone());
            }
            return newFoods;
        }

        public static List<BuyTrn> Clone(this List<BuyTrn> buyTrns) {
            List<BuyTrn> newBuyTrns = new List<BuyTrn>();
            foreach (BuyTrn buyTrn in buyTrns) {
                newBuyTrns.Add(buyTrn.Clone());
            }
            return newBuyTrns;
        }

        public static List<UseTrn> Clone(this List<UseTrn> useTrns) {
            List<UseTrn> newUseTrns = new List<UseTrn>();
            foreach (UseTrn useTrn in useTrns) {
                newUseTrns.Add(useTrn.Clone());
            }
            return newUseTrns;
        }

        public static List<Remain> Clone(this List<Remain> remains) {
            List<Remain> newRemains = new List<Remain>();
            foreach (Remain remain in remains) {
                newRemains.Add(remain.Clone());
            }
            return newRemains;
        }
    }
}
