using Jego.FSM.Interfaces.FMS.During;
using Jego.FSM.Interfaces.FMS.Output;
using JegoDatabase.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jego.FSM.Models;
using Jego.FSM.Interfaces.FMS.Params;
using JegoDatabase.Models;
using JegoDatabase.Entities;
using ExcelTemplateLib.DataModels;

namespace Jego.FSM.Managers.SubFSMs {
    public class FSMChangeDateManager {
        private List<IFSMChangeDateDuring> duringUIs;
        private List<IFSMChangeDateOutput> outputUIs;
        private List<IFSMChangeDateRemainsOutput> remainsUIs;
        private List<FoodRemain> foodRemains;
        private List<FoodBuyUse> foodBuyUses;


        public FSMChangeDateManager() {
            this.duringUIs = new List<IFSMChangeDateDuring>();
            this.outputUIs = new List<IFSMChangeDateOutput>();
            this.remainsUIs = new List<IFSMChangeDateRemainsOutput>();
        }

        public void addDuringUI(IFSMChangeDateDuring duringUI) {
            duringUIs.Add(duringUI);
        }

        public void addOutputUI(IFSMChangeDateOutput outputUI) {
            outputUIs.Add(outputUI);
        }

        public void addRemainOutputUI(IFSMChangeDateRemainsOutput remainOutputUI) {
            remainsUIs.Add(remainOutputUI);
        }

        public void Process(DateTime date) {
            foodRemains = new List<FoodRemain>();
            foodBuyUses = new List<FoodBuyUse>();

            spreadDuringUIs(date);
            createOutputModel(date);
            spreadRemainOutputUIs();
            spreadOutputUIs();
        }

        private void spreadDuringUIs(DateTime date) {
            foreach (IFSMChangeDateDuring ui in duringUIs) {
                ui.IFSMD_ChangeDate(date);
            }
        }

        private void createOutputModel(DateTime date) {
            Dictionary<string, Food> foodDic = getFoodDic();
            TodaysData todayData = JegoManager.GetTodayDatas(date);

            createFoodBuyUses(foodDic, todayData);
            createFoodRemains(foodDic, todayData);
        }

        private void createFoodRemains(Dictionary<string, Food> foodDic, TodaysData todayData) {
            if (todayData.remains != null) {
                foreach (Remain remain in todayData.remains) {
                    if (remain != null && remain.amount != 0) {
                        FoodRemain foodRemain = new FoodRemain() { food = foodDic[remain.f_code], remain = remain };
                        foodRemains.Add(foodRemain);
                    }
                }
            }
        }

        private void createFoodBuyUses(Dictionary<string, Food> foodDic, TodaysData todayData) {
            Dictionary<string, FoodBuyUse> foodBuyUseDic = new Dictionary<string, FoodBuyUse>();

            if (todayData.buyTrns != null) {
                foreach (BuyTrn buyTrn in todayData.buyTrns) {
                    if (buyTrn != null) {
                        if (foodBuyUseDic.ContainsKey(buyTrn.f_code)) {
                            FoodBuyUse fbu = foodBuyUseDic[buyTrn.f_code];
                            fbu.buyTrn = buyTrn;
                        } else {
                            FoodBuyUse fbu = new FoodBuyUse() { food = foodDic[buyTrn.f_code], buyTrn = buyTrn };
                            foodBuyUseDic.Add(buyTrn.f_code, fbu);
                        }
                    }
                }
            }

            if (todayData.useTrns != null) {
                foreach (UseTrn useTrn in todayData.useTrns) {
                    if (useTrn != null) {
                        if (foodBuyUseDic.ContainsKey(useTrn.f_code)) {
                            FoodBuyUse fbu = foodBuyUseDic[useTrn.f_code];
                            fbu.useTrn = useTrn;
                        } else {
                            FoodBuyUse fbu = new FoodBuyUse() { food = foodDic[useTrn.f_code], useTrn = useTrn };
                            foodBuyUseDic.Add(useTrn.f_code, fbu);
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, FoodBuyUse> entry in foodBuyUseDic) {
                foodBuyUses.Add(entry.Value);
            }
        }

        private static Dictionary<string, Food> getFoodDic() {
            Dictionary<string, Food> foodDic = new Dictionary<string, Food>();
            List<Food> foods = JegoManager.GetAllFoods();
            foreach (Food food in foods) {
                foodDic.Add(food.f_code, food);
            }
            return foodDic;
        }

        private void spreadOutputUIs() {
            IFSMChangeDateParam param = new FoodBuyUses(foodBuyUses);
            foreach (IFSMChangeDateOutput outputUI in outputUIs) {
                outputUI.IFSMO_ChangeDate(param);
            }
        }

        private void spreadRemainOutputUIs() {
            IFSMChangeDateRemainsParam param = new FoodRemains(foodRemains);
            foreach (IFSMChangeDateRemainsOutput outputUI in remainsUIs) {
                outputUI.IFSMO_ChangeRemainsDate(param);
            }
        }
    }
}
