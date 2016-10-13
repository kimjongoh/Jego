using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JegoDatabase.Entities;
using JegoDatabase.Extensions;
using MoreLinq;

namespace JegoDatabase.Manager {
    public class DatabaseManager {
        public static JegoEntities GetContext() {
            return new JegoEntities();
        }

        public static void PutFood(JegoEntities context, Food food) {
            if (!food.f_code.isNotEmptyString()) {
                food.CreateCode();
            }
            AddFood(context, food);
        }

        public static void AddFood(JegoEntities context, Food food) {
            var foods = context.Food.Where(e => e.f_code.Equals(food.f_code));
             if (foods.Count() == 0) {
                 context.Food.Add(food);
             }
        }



        public static Food GetFoodByCode(JegoEntities context, string f_code) {
            var foods = context.Food.Where(e => e.f_code.Equals(f_code));
            if (foods.Count() == 1) {
                return foods.First();
            } else {
                return null;
            }
        }

        public static void SaveChanges(JegoEntities context) {
            context.SaveChanges();
        }

        public static List<Food> GetAllFoods(JegoEntities context) {
            var foods = context.Food;
            if (foods.Count() > 0) {
                return foods.ToList();
            } else {
                return new List<Food>();
            }
        }

        public static void PutBuyTrn(JegoEntities context, BuyTrn buyTrn) {
            var buyTrns = context.BuyTrn.Where(e => e.date.Equals(buyTrn.date) && e.f_code.Equals(buyTrn.f_code));
            if (buyTrns.Count() == 0) {
                AddBuyTrn(context, buyTrn);
            } else if(buyTrns.Count() == 1) {
                UpdateBuyTrn(context, buyTrns.First(), buyTrn);
            }
        }

        public static void AddBuyTrn(JegoEntities context, BuyTrn buyTrn) {
            context.BuyTrn.Add(buyTrn);
        }

        public static void UpdateBuyTrn(JegoEntities context, BuyTrn exBuyTrn, BuyTrn newBuyTrn) {
            exBuyTrn.amount = newBuyTrn.amount;
            exBuyTrn.deadline = newBuyTrn.deadline;
            exBuyTrn.total_price = newBuyTrn.total_price;
        }


        public static void PutUseTrn(JegoEntities context, UseTrn useTrn) {
            var useTrns = context.UseTrn.Where(e => e.date.Equals(useTrn.date) && e.f_code.Equals(useTrn.f_code));
            if (useTrns.Count() == 0) {
                AddUseTrn(context, useTrn);
            } else if (useTrns.Count() == 1) {
                UpdateUseTrn(context, useTrns.First(), useTrn);
            }
        }

        public static void AddUseTrn(JegoEntities context, UseTrn useTrn) {
            context.UseTrn.Add(useTrn);
        }

        public static void UpdateUseTrn(JegoEntities context, UseTrn exUseTrn, UseTrn newUseTrn) {
            exUseTrn.amount = newUseTrn.amount;
            exUseTrn.deadline = newUseTrn.deadline;
            exUseTrn.total_price = newUseTrn.total_price;
        }

        public static Remain PutRemain(JegoEntities context, Remain remain) {
            var remains = context.Remain.Where(e => e.fh_code.Equals(remain.fh_code));
            
            if (remains.Count() == 0) {
                AddRemain(context, remain);
                return remain;
            } else if (remains.Count() == 1) {
                return UpdateRemain(context, remains.First(), remain);
            }
            return null;
        }

        public static void AddRemain(JegoEntities context, Remain remain) {
            context.Remain.Add(remain);
        }

        public static Remain UpdateRemain(JegoEntities context, Remain exRemain, Remain newRemain) {
            if (exRemain.amount == newRemain.amount) {
                return null;
            } else {
                exRemain.amount = newRemain.amount;
                return exRemain;
            }
        }

        public static void RefreshAfterRemain(JegoEntities context, Remain remain) {
            var nextRemains = context.Remain.Where(e => e.f_code.Equals(remain.f_code) 
                && String.Compare(e.date, remain.date) == 1).OrderBy(e => e.date);

            Remain exRemain = remain;
            if (nextRemains.Count() > 0) {
                List<Remain> remains = nextRemains.ToList();
                foreach (Remain editRemain in remains) {
                    string updateDate = editRemain.date;
                    editRemain.amount = exRemain.amount;

                    BuyTrn buyTrn = GetBuyTrn(context, remain.f_code, updateDate);
                    if (buyTrn != null) {
                        editRemain.amount += buyTrn.amount;    
                    }

                    UseTrn useTrn = GetUseTrn(context, remain.f_code, updateDate);
                    if (useTrn != null) {
                        editRemain.amount -= useTrn.amount;
                    }
                    exRemain = editRemain;
                }
            }
        }

        public static BuyTrn GetBuyTrn(JegoEntities context, string f_code, string date) {
            var buyTrn = context.BuyTrn.Where(e => e.f_code.Equals(f_code) && e.date.Equals(date));
            if (buyTrn.Count() == 1) {
                return buyTrn.First();
            } else {
                return null;
            }
        }

        public static UseTrn GetUseTrn(JegoEntities context, string f_code, string date) {
            var useTrn = context.UseTrn.Where(e => e.f_code.Equals(f_code) && e.date.Equals(date));
            if (useTrn.Count() == 1) {
                return useTrn.First();
            } else {
                return null;
            }
        }

        public static Remain GetLastFoodRemain(JegoEntities context, Food rice, string date) {
            var remain = context.Remain.Where(e => e.f_code.Equals(rice.f_code) && String.Compare(e.date, date) <= 0).OrderByDescending(e => e.date);
            if (remain.Count() > 0) {
                return remain.First();
            } else {
                return null;
            }
        }

        public static List<Remain> GetTodayLastRemains(JegoEntities context, string date) {
            var remains = context.Remain.Where(e => String.Compare(e.date, date) <= 0)
                .OrderByDescending(o => o.date)
                .DistinctBy(p => p.f_code);
                
            if (remains.Count() > 0) {
                return remains.ToList();
            } else {
                return new List<Remain>();
            }
        }

        public static List<Remain> GetLastRemains(JegoEntities context, string date) {
            var remains = context.Remain.Where(e => String.Compare(e.date, date) < 0)
                .OrderByDescending(o => o.date)
                .DistinctBy(p => p.f_code);

            if (remains.Count() > 0) {
                return remains.ToList();
            } else {
                return new List<Remain>();
            }
        }

        public static List<BuyTrn> GetTodayBuyTrns(JegoEntities context, string date) {
            var buyTrns = context.BuyTrn.Where(e => e.date.Equals(date));

            if (buyTrns.Count() > 0) {
                return buyTrns.ToList();
            } else {
                return new List<BuyTrn>();
            }
        }

        public static List<UseTrn> GetTodayUseTrns(JegoEntities context, string date) {
            var useTrns = context.UseTrn.Where(e => e.date.Equals(date));

            if (useTrns.Count() > 0) {
                return useTrns.ToList();
            } else {
                return new List<UseTrn>();
            }
        }
    }
}
