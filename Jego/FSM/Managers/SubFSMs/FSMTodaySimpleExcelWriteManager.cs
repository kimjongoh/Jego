using ExcelTemplateLib.DataModels;
using ExcelTemplateLib.Managers;
using ExcelTemplateLib.Templates;
using Jego.FSM.Interfaces.FMS.Managers;
using Jego.FSM.Models;
using Jego.Helper;
using Jego.SharedPreferences;
using JegoDatabase.Entities;
using JegoDatabase.Manager;
using JegoDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Managers.SubFSMs {
    public class FSMTodaySimpleExcelWriteManager : IFSMWriteExcelManager {
        private DateTime date;

        public FSMTodaySimpleExcelWriteManager(DateTime date)
            : base() {
            this.date = date;
        }

        public override void thread_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker worker = sender as BackgroundWorker;
            createDirectoryIfNotExist();

            TodaysData todaysData = JegoManager.GetTodayDatas(date);
            List<Food> foods = JegoManager.GetAllFoods();
            Dictionary<string, Food> foodDic = new Dictionary<string,Food>();
            foreach(Food food in foods) {
                foodDic.Add(food.f_code, food);
            }

            Dictionary<string, TypeBaseDayModel> typeDayDic = new Dictionary<string,TypeBaseDayModel>();

            foreach (Remain remain in todaysData.remains) {
                Food food = foodDic[remain.f_code];
                if (typeDayDic.ContainsKey(food.type)) {
                    TypeBaseDayModel typebaseDayModel = typeDayDic[food.type];
                    if (typebaseDayModel.dataList.ContainsKey(remain.f_code)) {
                        DayFoodModel day = typebaseDayModel.dataList[remain.f_code];
                        day.remain = remain;
                    } else {
                        typebaseDayModel.dataList.Add(remain.f_code, new DayFoodModel() { food = food, remain = remain });
                    }
                } else {
                    TypeBaseDayModel typebaseDayModel = new TypeBaseDayModel(food.type);
                    typebaseDayModel.dataList.Add(remain.f_code, new DayFoodModel() { food = food, remain = remain });
                    typeDayDic.Add(food.type, typebaseDayModel);
                }
            }

            foreach (BuyTrn buyTrn in todaysData.buyTrns) {
                Food food = foodDic[buyTrn.f_code];    
                if (typeDayDic.ContainsKey(food.type)) {
                    TypeBaseDayModel typebaseDayModel = typeDayDic[food.type];
                    if(typebaseDayModel.dataList.ContainsKey(buyTrn.f_code)) {
                        DayFoodModel day = typebaseDayModel.dataList[buyTrn.f_code];
                        day.buyTrn = buyTrn;
                    } else {
                        typebaseDayModel.dataList.Add(buyTrn.f_code, new DayFoodModel() { food = food, buyTrn = buyTrn });
                    }
                } else {
                    TypeBaseDayModel typebaseDayModel = new TypeBaseDayModel(food.type);
                    typebaseDayModel.dataList.Add(buyTrn.f_code, new DayFoodModel() { food = food, buyTrn = buyTrn });
                    typeDayDic.Add(food.type, typebaseDayModel);
                }
            }

            foreach (UseTrn useTrn in todaysData.useTrns) {
                Food food = foodDic[useTrn.f_code];
                if (typeDayDic.ContainsKey(food.type)) {
                    TypeBaseDayModel typebaseDayModel = typeDayDic[food.type];
                    if (typebaseDayModel.dataList.ContainsKey(useTrn.f_code)) {
                        DayFoodModel day = typebaseDayModel.dataList[useTrn.f_code];
                        day.useTrn = useTrn;
                    } else {
                        typebaseDayModel.dataList.Add(useTrn.f_code, new DayFoodModel() { food = food, useTrn = useTrn });
                    }
                } else {
                    TypeBaseDayModel typebaseDayModel = new TypeBaseDayModel(food.type);
                    typebaseDayModel.dataList.Add(useTrn.f_code, new DayFoodModel() { food = food, useTrn = useTrn });
                    typeDayDic.Add(food.type, typebaseDayModel);
                }
            }

            List<string> foodTypes = FoodTypeManager.GetFoodTypes();
            List<TypeBaseDayModel> typeBaseDayModels = new List<TypeBaseDayModel>();
            foreach (string foodStr in foodTypes) {
                if (typeDayDic.ContainsKey(foodStr))
                    typeBaseDayModels.Add(typeDayDic[foodStr]);
            }

            ProgressTransfer progressTransfer = new ProgressTransfer(worker);
            SimpleExcelTemplateCreator testTemplete = new SimpleExcelTemplateCreator(1, getFilePath() + getFileName(), date, typeBaseDayModels, progressTransfer);
            ExcelManager.SaveExcel(testTemplete);
        }

        private string getExcelDirectory() {
            return System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\Excels\\";
        }

        private string getLastDirectoryName() {
            return "간단포멧\\";
        }

        private string getMiddleDirectoryName() {
            return "날짜별 불출대장\\";
        }

        private string getFileName() {
            return date.ToString("yyyyMMdd") + ".xls";
        }

        public override string getFilePath() {
            string path = getExcelDirectory();
            path += getMiddleDirectoryName();
            path += getLastDirectoryName();
            return path;
        }

        private void createDirectoryIfNotExist() {
            string path = getExcelDirectory();
            DirectoryHelper.CreateIfNotExist(path);
            path += getMiddleDirectoryName();
            DirectoryHelper.CreateIfNotExist(path);
            path += getLastDirectoryName();
            DirectoryHelper.CreateIfNotExist(path);
        }
    }
}
