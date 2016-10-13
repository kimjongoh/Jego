using ExcelTemplateLib.DataModels;
using Jego.Controls.MainPages.InputOutputControls;
using Jego.FSM.Collectors;
using Jego.FSM.Interfaces;
using Jego.FSM.Interfaces.FMS.During;
using Jego.FSM.Interfaces.FMS.Output;
using Jego.FSM.Interfaces.FMS.Params;
using Jego.FSM.Models;
using Jego.SharedPreferences;
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
    public partial class RemainContents : UserControl, IFSMControl, IFSMChangeDateDuring, IFSMChangeDateOutput, IFSMFoodInfomationChangeOutput {
        private Dictionary<string, RemainListItem> remainListItems;
        private Dictionary<InputOutputItem, RemainListItem> newListItems;

        private bool isLoading = false;
        public RemainContents() {
            InitializeComponent();
            remainListItems = new Dictionary<string, RemainListItem>();
            newListItems = new Dictionary<InputOutputItem, RemainListItem>();
            RegisterCollector();
        }

        public void IFSMD_ChangeDate(DateTime date) {
            isLoading = true;
        }

        public void IFSMO_ChangeDate(IFSMChangeDateParam param) {
            remain_ListView.Items.Clear();
            remainListItems.Clear();

            Dictionary<string, List<DayFoodModel>> containerDic = new Dictionary<string, List<DayFoodModel>>();

            List<string> types = FoodTypeManager.GetFoodTypes();

            foreach (string type in types) {
                containerDic.Add(type, new List<DayFoodModel>());
            }

            List<DayFoodModel> dayModels = param.getDayModels();
            foreach (DayFoodModel dayModel in dayModels) {
                List<DayFoodModel> dayModelList;
                containerDic.TryGetValue(dayModel.food.type, out dayModelList);
                if(dayModelList != null) {
                    dayModelList.Add(dayModel);
                }
            }

            List<DayFoodModel> newDayModels = new List<DayFoodModel>();
            foreach (KeyValuePair<string, List<DayFoodModel>> entry in containerDic) {
                List <DayFoodModel> typeDayModel = entry.Value;
                foreach (DayFoodModel daymodel in typeDayModel) {
                    newDayModels.Add(daymodel);
                }
            }

            foreach (DayFoodModel dayModel in newDayModels) {
                RemainListItem remainListItem = new RemainListItem(dayModel);
                remainListItems.Add(dayModel.food.f_code, remainListItem);
                remain_ListView.Items.Add(remainListItem);
            }
            isLoading = false;
        }

        public void RegisterCollector() {
            InputOutputUICollector.registerRemainContents(this);
        }

        public void UnRegisterCollector() {
            InputOutputUICollector.unRegisterRemainContents(this);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            UnRegisterCollector();
        }

        public void IFSMOFoodInfomationChangeOutput(IFSMFoodInfomationChangeParam param){
            if (!isLoading) {
                string exf_code = param.GetExFCode();
                if (exf_code != null && !exf_code.Equals("")) {
                    if (remainListItems.ContainsKey(exf_code)) {
                        RemainListItem remainListItem = remainListItems[exf_code];
                        if (remainListItem.Tag == param.GetInputOutputItem()) {
                            remainListItem.setTodayData(null, null);
                            remainListItem.Tag = null;
                        }
                    }
                } 

                DayFoodModel dayFoodModel = param.GetInputOutputItem().GetTodaySetDatas();

                if (dayFoodModel != null) {
                    string f_code = dayFoodModel.food.f_code;
                    
                    if (remainListItems.ContainsKey(f_code)) {
                        RemainListItem remainListItem = remainListItems[f_code];
                        if (remainListItem.Tag == null || remainListItem.Tag == param.GetInputOutputItem()) {
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
                        if (f_code.Equals("")) {
                            remain_ListView.Items.Remove(remainListItem);
                            newListItems.Remove(param.GetInputOutputItem());
                        } else {
                            remainListItem.setDayFoodModel(dayFoodModel);
                            remain_ListView.SelectedItem = remainListItem;
                        }
                    } else {
                        if (!f_code.Equals("")) {
                            RemainListItem remainListItem = new RemainListItem(dayFoodModel);
                            remain_ListView.Items.Add(remainListItem);

                            newListItems.Add(param.GetInputOutputItem(), remainListItem);
                            remain_ListView.SelectedItem = remainListItem;
                        }
                    }
                }
            }
        }
    }
}
