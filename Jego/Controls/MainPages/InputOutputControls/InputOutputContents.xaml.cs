using Jego.FSM.Collectors;
using Jego.FSM.Interfaces;
using Jego.FSM.Interfaces.FMS.Input;
using Jego.FSM.Interfaces.FMS.Output;
using Jego.FSM.Interfaces.FMS.Params;
using Jego.FSM.Managers;
using JegoDatabase.Entities;
using JegoDatabase.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JegoDatabase.Extensions;
using Jego.FSM.Models;
using ExcelTemplateLib.DataModels;

namespace Jego.Controls.MainPages.InputOutputControls {
    /// <summary>
    /// InputOutputContents.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputOutputContents : UserControl, IFSMControl, IFSMAddFoodTypeOutput, IFSMChangeDateOutput, IFSMRemoveFoodTypeOutput, IFSMSaveInputOutputData {
        public InputOutputContents() {
            InitializeComponent();
            RegisterCollector();
        }

        public void RegisterCollector() {
            InputOutputUICollector.registerInputOutputContents(this);
        }

        public void UnRegisterCollector() {
            InputOutputUICollector.unRegisterInputOutputContents(this);
        }

        public void IFSMO_AddFoodType(string type) {
            bool isExist = false;
            foreach (InputOutputTypeContainer container in contentItem_StackPanel.Children) {
                if (container.type.Equals(type)) {
                    isExist = true;
                    break;
                }
            }
            if(!isExist)
                contentItem_StackPanel.Children.Add(new InputOutputTypeContainer(type));
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            UnRegisterCollector();
        }

        public void IFSMO_ChangeDate(IFSMChangeDateParam param) {
            Dictionary<string, InputOutputTypeContainer> containerDic = new Dictionary<string, InputOutputTypeContainer>();

            foreach (InputOutputTypeContainer container in contentItem_StackPanel.Children) {
                container.Clear();
                containerDic.Add(container.type, container);
            }

            List<FoodBuyUse> dayModels = param.GetFoodBuyUse();

            foreach (FoodBuyUse dayModel in dayModels) {
                Food food = dayModel.food;
                if (containerDic.ContainsKey(food.type)) {
                    containerDic[food.type].addDayModel(dayModel);
                } else {
                    InputOutputTypeContainer container = new InputOutputTypeContainer(food.type);
                    contentItem_StackPanel.Children.Add(container);
                    container.addDayModel(dayModel);
                    containerDic.Add(food.type, container);
                }
            }
        }

        public void IFSMO_RemoveFoodType(string type) {
            if (contentItem_StackPanel != null && contentItem_StackPanel.Children != null) {
                foreach (InputOutputTypeContainer container in contentItem_StackPanel.Children) {
                    if (container.type.Equals(type)) {
                        contentItem_StackPanel.Children.Remove(container);
                        break;
                    }
                }
            }
        }

        public List<DayFoodModel> GetTodaySetDatas() {
            List<DayFoodModel> newDayModels = new List<DayFoodModel>();
            foreach (InputOutputTypeContainer container in contentItem_StackPanel.Children) {
                List<DayFoodModel> dayModels = container.GetTodaySetDatas();
                foreach (DayFoodModel dayModel in dayModels) {
                    newDayModels.Add(dayModel);
                }
            }
            return newDayModels;
        }

    }
}
