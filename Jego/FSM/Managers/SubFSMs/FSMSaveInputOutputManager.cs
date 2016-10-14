using ExcelTemplateLib.DataModels;
using Jego.FSM.Interfaces.FMS.Input;
using Jego.FSM.Models;
using Jego.Helper;
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
        private IFSMSaveInputOutputRemain remainModule;

        public FSMSaveInputOutputManager() {}

        public void setDateModule(IFSMSaveInputOutputDate dateModule) {
            this.dateModule = dateModule;
        }

        public void setDataModule(IFSMSaveInputOutputData dataModule) {
            this.dataModule = dataModule;
        }

        public void setRemainModule(IFSMSaveInputOutputRemain remainModule) {
            this.remainModule = remainModule;
        }

        public void Process() {
            string date = GetDate();
            List<DayFoodModel> todayData = dataModule.GetTodaySetDatas();
            saveFoods(todayData);
            saveDayData(todayData, date);

            List<Remain> todayRemain = remainModule.GetTodayRemains();
            saveRemainTrns(todayRemain, date);
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
        }

        private void saveRemainTrns(List<Remain> todayRemain, string date) {

            foreach (Remain remain in todayRemain) {
                remain.date = date;
                remain.fh_code = remain.f_code + date;
                List<Remain> nextRemains = JegoManager.GetNextRemains(remain.f_code, remain.date);
                Remain beforeRemain = remain;
                foreach (Remain searchRemain in nextRemains) {
                    RemainDeadlineHelper.setDeadLine(searchRemain, beforeRemain);
                    searchRemain.amount = beforeRemain.amount + searchRemain.buy_amount + searchRemain.use_amount;
                }
                JegoManager.AddRemains(nextRemains);
            }
            JegoManager.AddRemains(todayRemain);
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
