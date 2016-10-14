using ExcelTemplateLib.DataModels;
using Jego.Controls.MainPages.InputOutputControls;
using Jego.FSM.Collectors;
using Jego.FSM.Interfaces;
using Jego.FSM.Interfaces.FMS.During;
using Jego.FSM.Interfaces.FMS.Input;
using Jego.FSM.Interfaces.FMS.Output;
using Jego.FSM.Interfaces.FMS.Params;
using Jego.FSM.Models;
using Jego.SharedPreferences;
using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jego.Controls.MainPages.RemainControls {
    /// <summary>
    /// RemainContents.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class RemainContents : UserControl, IFSMControl, IFSMChangeDateDuring ,IFSMChangeDateRemainsOutput, IFSMFoodInfomationChangeOutput, IFSMSaveInputOutputRemain {
        private Dictionary<string, RemainListItem> remainHaveListItem;
        private Dictionary<InputOutputItem, RemainListItem> newListItems;

        private string date;
        public RemainContents() {
            InitializeComponent();
            remainHaveListItem = new Dictionary<string, RemainListItem>();
            newListItems = new Dictionary<InputOutputItem, RemainListItem>();
            RegisterCollector();
        }
        
        public void RegisterCollector() {
            InputOutputUICollector.registerRemainContents(this);
        }

        public void UnRegisterCollector() {
            InputOutputUICollector.unRegisterRemainContents(this);
        }

        public void IFSMD_ChangeDate(DateTime date) {
            this.date = date.ToString("yyyyMMdd");
            remainHaveListItem.Clear();
            newListItems.Clear();
            remain_ListView.Items.Clear();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            UnRegisterCollector();
        }

        public void IFSMOFoodInfomationChangeOutput(IFSMFoodInfomationChangeParam param){

            string exf_code = param.GetExFCode();
            if (exf_code != null && !exf_code.Equals("") && remainHaveListItem.ContainsKey(exf_code)) {
                RemainListItem remainListItem = remainHaveListItem[exf_code];
                if (remainListItem.Tag != null && remainListItem.Tag == param.GetInputOutputItem()) {
                    remainListItem.Tag = null;
                    remainListItem.refreshTodayRemain();
                }
            } 

            DayFoodModel dayFoodModel = param.GetInputOutputItem().GetTodaySetDatas();

            if (dayFoodModel != null) {
                string f_code = dayFoodModel.food.f_code;

                if (remainHaveListItem.ContainsKey(f_code)) {
                    RemainListItem remainListItem = remainHaveListItem[f_code];
                    if (remainListItem.Tag != null && remainListItem.Tag != param.GetInputOutputItem()) {
                        if (newListItems.ContainsKey(param.GetInputOutputItem())) {
                            remain_ListView.Items.Remove(newListItems[param.GetInputOutputItem()]);
                            newListItems.Remove(param.GetInputOutputItem());
                        }
                    } else {
                        remainListItem.setTodayData(dayFoodModel.buyTrn, dayFoodModel.useTrn);
                        remainListItem.Tag = param.GetInputOutputItem();
                        remain_ListView.SelectedItem = remainListItem;

                        if (newListItems.ContainsKey(param.GetInputOutputItem())) {
                            remain_ListView.Items.Remove(newListItems[param.GetInputOutputItem()]);
                            newListItems.Remove(param.GetInputOutputItem());
                        }
                    }
                } else if (newListItems.ContainsKey(param.GetInputOutputItem())) {
                    RemainListItem remainListItem = newListItems[param.GetInputOutputItem()];

                    bool isExist = false;
                    foreach (RemainListItem listItem in remain_ListView.Items) {
                        if (remainListItem == listItem) {
                            continue;
                        }
                        if (listItem.isEqualFcode(f_code)) {
                            isExist = true;
                            break;
                        }
                    }

                    if (dayFoodModel.food.name != null && !"".Equals(dayFoodModel.food.name.Trim()) &&
                        dayFoodModel.food.unit_pirce != 0 &&
                        dayFoodModel.food.unit != null && !"".Equals(dayFoodModel.food.unit.Trim()) && !isExist) {

                        remainListItem.setFoodData(dayFoodModel.food);
                        remainListItem.setTodayData(dayFoodModel.buyTrn, dayFoodModel.useTrn);
                        remain_ListView.SelectedItem = remainListItem;
                    } else {
                        remain_ListView.Items.Remove(remainListItem);
                        newListItems.Remove(param.GetInputOutputItem());
                    }
                } else {
                    bool isExist = false;
                    foreach (RemainListItem listItem in remain_ListView.Items) {
                        if (listItem.isEqualFcode(f_code)) {
                            isExist = true;
                            break;
                        }
                    }

                    if (isExist) {

                    } else if (dayFoodModel.food.name != null && !"".Equals(dayFoodModel.food.name.Trim()) &&
                        dayFoodModel.food.unit_pirce != 0 &&
                        dayFoodModel.food.unit != null && !"".Equals(dayFoodModel.food.unit.Trim())) {

                        RemainListItem remainListItem = new RemainListItem(date);
                        remainListItem.setFoodData(dayFoodModel.food);
                        remainListItem.setTodayData(dayFoodModel.buyTrn, dayFoodModel.useTrn);
                        remain_ListView.Items.Add(remainListItem);
                        newListItems.Add(param.GetInputOutputItem(), remainListItem);
                        remain_ListView.SelectedItem = remainListItem;
                    }
                }
                
            }
        }

        public void IFSMO_ChangeRemainsDate(IFSMChangeDateRemainsParam param) {
            List<FoodRemain> foodRemains = param.GetFoodRemain();
            foreach (FoodRemain foodRemain in foodRemains) {
                if (foodRemain.food != null && foodRemain.food.f_code != null && foodRemain.remain != null) {
                    RemainListItem remainListItem = new RemainListItem(foodRemain, date);
                    remain_ListView.Items.Add(remainListItem);
                    remainHaveListItem.Add(foodRemain.food.f_code, remainListItem);
                }
            }
        }

        public List<Remain> GetTodayRemains() {
            List<Remain> newRemains = new List<Remain>();
            foreach (RemainListItem remainListItem in remain_ListView.Items) {
                if (remainListItem.isUpdated()) {
                    newRemains.Add(remainListItem.GetTodayRemains());
                }
            }
            return newRemains;
        }
    }
}
