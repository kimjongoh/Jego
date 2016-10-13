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
        public FSMChangeDateManager() {
            this.duringUIs = new List<IFSMChangeDateDuring>();
            this.outputUIs = new List<IFSMChangeDateOutput>();
        }

        public void addDuringUI(IFSMChangeDateDuring duringUI) {
            duringUIs.Add(duringUI);
        }

        public void addOutputUI(IFSMChangeDateOutput outputUI) {
            outputUIs.Add(outputUI);
        }

        public void Process(DateTime date) {
            spreadDuringUIs(date);
            IFSMChangeDateParam param = createOutputModel(date);
            spreadOutputUIs(param);
        }

        private void spreadDuringUIs(DateTime date) {
            foreach (IFSMChangeDateDuring ui in duringUIs) {
                ui.IFSMD_ChangeDate(date);
            }
        }

        private IFSMChangeDateParam createOutputModel(DateTime date) {
            List<Food> foods = JegoManager.GetAllFoods();
            Dictionary<string, DayFoodModel> dayDic = new Dictionary<string, DayFoodModel>();
            foreach (Food food in foods) {
                DayFoodModel dayModel = new DayFoodModel();
                dayModel.food = food;
                dayDic.Add(food.f_code, dayModel);
            }

            TodaysData todayData = JegoManager.GetTodayDatas(date);
            if (todayData.buyTrns != null) {
                foreach (BuyTrn buyTrn in todayData.buyTrns) {
                    if (buyTrn != null) {
                        DayFoodModel dayModel;
                        dayDic.TryGetValue(buyTrn.f_code, out dayModel);
                        if(dayModel != null)
                            dayModel.buyTrn = buyTrn;
                    }
                }
            }
            if (todayData.useTrns != null) {
                foreach (UseTrn useTrn in todayData.useTrns) {
                    if (useTrn != null) {
                        DayFoodModel dayModel;
                        dayDic.TryGetValue(useTrn.f_code, out dayModel);
                        if (dayModel != null)
                            dayModel.useTrn = useTrn;
                    }
                }
            }
            if (todayData.remains != null) {
                foreach (Remain remain in todayData.remains) {
                    if (remain != null) {
                        DayFoodModel dayModel;
                        dayDic.TryGetValue(remain.f_code, out dayModel);
                        if (dayModel != null)
                            dayModel.remain = remain;
                    }
                }
            }

            List<DayFoodModel> todayDayModels = new List<DayFoodModel>();
            foreach (KeyValuePair<string, DayFoodModel> entry in dayDic) {
                DayFoodModel dayModel = entry.Value;
                if (dayModel.buyTrn != null || dayModel.buyTrn != null) {
                    todayDayModels.Add(dayModel);
                }
            }

            return new DayUpdateData(todayDayModels, todayData.remains);
        }

        private void spreadOutputUIs(IFSMChangeDateParam param) {
            foreach (IFSMChangeDateOutput outputUI in outputUIs) {
                outputUI.IFSMO_ChangeDate(param);
            }
        }
    }
}
