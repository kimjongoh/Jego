using JegoDatabase.Entities;
using JegoDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JegoDatabase.Extensions;

namespace JegoDatabase.Manager {
    public class JegoManager {
        public static TodaysData GetTodayDatas(DateTime date) {
            try {
                string dateStr = date.ToString("yyyyMMdd");
                using (var context = DatabaseManager.GetContext()) {
                    List<BuyTrn> buyTrns = DatabaseManager.GetTodayBuyTrns(context, dateStr);
                    List<UseTrn> useTrns = DatabaseManager.GetTodayUseTrns(context, dateStr);
                    List<Remain> remains = DatabaseManager.GetLastRemains(context, dateStr);

                    return new TodaysData() { buyTrns = buyTrns.Clone(), useTrns = useTrns.Clone(), remains = remains.Clone() };
                }
            } catch (Exception e) {
                return new TodaysData();
            }
        }

        public static List<Remain> GetAllLastRemains(string date) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    List<Remain> remains = DatabaseManager.GetLastRemains(context, date);
                    return remains.Clone();
                }
            } catch (Exception e) {
                return new List<Remain>();
            }
        }

        public static List<Food> GetAllFoods() {
            try {
                
                using (var context = DatabaseManager.GetContext()) {
                    List<Food> foods = DatabaseManager.GetAllFoods(context);

                    return foods.Clone();
                }
            } catch (Exception e) {
                return new List<Food>();
            }
        }

        public static void AddBuyTrns(List<BuyTrn> buyTrns) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    foreach (BuyTrn buyTrn in buyTrns) {
                        DatabaseManager.PutBuyTrn(context, buyTrn);
                    }
                    DatabaseManager.SaveChanges(context);
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void AddUseTrns(List<UseTrn> useTrns) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    foreach (UseTrn useTrn in useTrns) {
                        DatabaseManager.PutUseTrn(context, useTrn);
                    }
                    DatabaseManager.SaveChanges(context);
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void AddFoods(List<Food> foods) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    foreach (Food food in foods) {
                        DatabaseManager.PutFood(context, food);
                    }
                    DatabaseManager.SaveChanges(context);
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }


        public static Remain PutRemain(Remain remain) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    Remain updateRemain = DatabaseManager.PutRemain(context, remain);
                    return updateRemain;
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }

        public static void RefreshAfterRemain(Remain updatedRiceRemain) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    DatabaseManager.RefreshAfterRemain(context, updatedRiceRemain);
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void AddRemains(List<Remain> remains) {

            try {
                using (var context = DatabaseManager.GetContext()) {
                    foreach (Remain remain in remains) {
                        Remain updateRemain = DatabaseManager.PutRemain(context, remain);
                    }
                    DatabaseManager.SaveChanges(context);
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public static List<Remain> GetNextRemains(string f_code, string date) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    List<Remain> remains = DatabaseManager.GetNextRemains(context, f_code, date);
                    return remains.Clone();
                }
            } catch (Exception e) {
                return new List<Remain>();
            }
        }

        public static Remain GetLastUseRemain(string f_code, string date) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    Remain remains = DatabaseManager.GetLastUseRemain(context, f_code, date);
                    return remains.Clone();
                }
            } catch (Exception e) {
                return null;
            }
        }

      

        public static void GetExtraAmount(Remain todayRemain, Remain exRemain) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    decimal currentExtraAmount = exRemain.extra_amount + todayRemain.use_amount;

                    List<BuyTrn> nextRemains = DatabaseManager.GetNextBuyTrn(context, exRemain.f_code, exRemain.deadline_date);
                    foreach (BuyTrn searchBuyTrn in nextRemains) {
                        if (searchBuyTrn.amount > 0) {
                            currentExtraAmount = searchBuyTrn.amount + currentExtraAmount;
                            if (currentExtraAmount > 0) {
                                todayRemain.deadline = searchBuyTrn.deadline;
                                todayRemain.deadline_date = searchBuyTrn.date;
                                todayRemain.extra_amount = currentExtraAmount;
                                return;
                            }
                        }
                    }
                    currentExtraAmount = todayRemain.buy_amount + currentExtraAmount;
                    todayRemain.deadline_date = todayRemain.date;
                    todayRemain.extra_amount = currentExtraAmount;
                }
            } catch (Exception e) {
                
            }
        }

        public static Remain GetFirstBuyRemain(string f_code) {
            try {
                using (var context = DatabaseManager.GetContext()) {
                    return DatabaseManager.GetFirstBuyRemain(context, f_code);
                }
            } catch (Exception e) {
                return null;
            }
        }
    }
}
