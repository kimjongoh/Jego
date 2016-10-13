using ExcelTemplateLib.DataModels;
using Jego.FSM.Interfaces.FMS.Input;
using Jego.FSM.Models;
using JegoDatabase.Entities;
using JegoDatabase.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Managers.SubFSMs {
    public class FSMSaveInputOutputManager {
        private IFSMSaveInputOutputDate dateModule;
        private IFSMSaveInputOutputData dataModule;

        public FSMSaveInputOutputManager() {}

        public void setDateModule(IFSMSaveInputOutputDate dateModule) {
            this.dateModule = dateModule;
        }

        public void setDataModule(IFSMSaveInputOutputData dataModule) {
            this.dataModule = dataModule;
        }

        public void Process() {
            string date = GetDate();
            List<DayFoodModel> todayData = dataModule.GetTodaySetDatas();
            saveFoods(todayData);
            saveDayData(todayData, date);
        }

        private void saveFoods(List<DayFoodModel> todayDatas) {
            List<Food> foods = new List<Food>();
            foreach (DayFoodModel day in todayDatas) {
                foods.Add(day.food);
            }
            JegoManager.AddFoods(foods);
        }

        private string GetDate() {
            DateTime datetime = dateModule.FSMI_SaveInputOutputDate();
            return datetime.ToString("yyyyMMdd");
        }

        private void saveDayData(List<DayFoodModel> todayData, string date) {
            saveBuyTrns(todayData, date);
            saveUseTrns(todayData, date);
            saveRemainTrns(todayData, date);
        }

        private void saveRemainTrns(List<DayFoodModel> todayData, string date) {
            List<Remain> exRemains = JegoManager.GetAllLastRemains(date);
            Dictionary<string, Remain> exRemainDic = new Dictionary<string, Remain>();

            foreach (Remain remain in exRemains) {
                exRemainDic.Add(remain.f_code, remain);
            }

            List<Remain> remains = new List<Remain>();
            foreach (DayFoodModel dayModel in todayData) {
                Remain exRemain;
                if (exRemainDic.ContainsKey(dayModel.food.f_code)) {
                    exRemain = exRemainDic[dayModel.food.f_code];
                } else {
                    exRemain = null;
                }
                Remain remain = CalculationManager.createRemain(dayModel.food.f_code, date, exRemain, dayModel.buyTrn, dayModel.useTrn);
                remains.Add(remain);
            }
            JegoManager.AddRemains(remains);
        }

        private void saveBuyTrns(List<DayFoodModel> todayData, string date) {
            List<BuyTrn> buyTrns = new List<BuyTrn>();
            foreach (DayFoodModel dayModel in todayData) {
                if (dayModel.buyTrn != null) {
                    dayModel.buyTrn.date = date;
                    buyTrns.Add(dayModel.buyTrn);
                }
            }
            if (buyTrns != null && buyTrns.Count > 0) {
                JegoManager.AddBuyTrns(buyTrns);
            }
        }


        private void saveUseTrns(List<DayFoodModel> todayData, string date) {
            List<UseTrn> useTrns = new List<UseTrn>();
            foreach (DayFoodModel dayModel in todayData) {
                if (dayModel.useTrn != null) {
                    dayModel.useTrn.date = date;
                    useTrns.Add(dayModel.useTrn);
                }
            }

            if (useTrns != null && useTrns.Count > 0) {
                JegoManager.AddUseTrns(useTrns);
            }
        }
    }
}
