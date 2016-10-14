using ExcelTemplateLib.DataModels;
using Jego.FSM.Collectors;
using Jego.FSM.Managers;
using Jego.FSM.Models;
using Jego.Helper;
using JegoDatabase.Entities;
using JegoDatabase.Extensions;
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

namespace Jego.Controls.MainPages.InputOutputControls {
    /// <summary>
    /// InputOutputItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputOutputItem : UserControl {
        public delegate void TotalPriceChangeDelegate();
        private TotalPriceChangeDelegate buyTotalPriceChangeListener;
        private TotalPriceChangeDelegate useTotalPriceChangeListener;

        public InputOutputItem(string type, FoodBuyUse dayModel, 
            TotalPriceChangeDelegate buyTotalPriceChangeListener, 
            TotalPriceChangeDelegate useTotalPriceChangeListener) {
            InitializeComponent();

            this.buyTotalPriceChangeListener = buyTotalPriceChangeListener;
            this.useTotalPriceChangeListener = useTotalPriceChangeListener;
            foodItem.setType(type);
            
            setBuyTrn(dayModel.buyTrn);
            setUseTrn(dayModel.useTrn);

            initFoodItem(dayModel);
            initBuyUseTrn(buyTotalPriceChangeCallback, useTotalPriceChangeCallback);
            changeFood(dayModel.food);
            sendUpdateRemain();
        }

        private void sendUpdateRemain() {
            Food food = foodItem.GetFood();
            string f_code = "";
            if (food != null) f_code = food.f_code;

            FSMInputOutputManagerHub.GetFoodInfomationChangeManager().Process(new FoodInfomationChangeModel(f_code, this));
        }
        private void buyTotalPriceChangeCallback() {
            sendUpdateRemain();
            buyTotalPriceChangeListener();
        }

        private void useTotalPriceChangeCallback() {
            sendUpdateRemain();
            useTotalPriceChangeListener();
        }

        private void initFoodItem(FoodBuyUse dayModel) {
            foodItem.setFood(dayModel.food);
            foodItem.setChangeFoodListener(changeFood);
        }

        public void setBuyTrn(BuyTrn buyTrn) {
            buyItem.setBuyTrn(buyTrn);
        }

        public void setUseTrn(UseTrn useTrn) {
            useItem.setUseTrn(useTrn);
        }


        private void changeFood(Food food) {
            buyItem.setFood(food);
            useItem.setFood(food);
        }

        private void initBuyUseTrn(TotalPriceChangeDelegate buyTotalPriceChangeListener,
            TotalPriceChangeDelegate useTotalPriceChangeListener) {
            buyItem.setTotalPriceChangeListener(buyTotalPriceChangeListener);
            useItem.setTotalPriceChangeListener(useTotalPriceChangeListener);
        }

       
        public decimal GetBuyTotalPrice() {
            return buyItem.getTotalPrice();
        }

        public decimal GetUseTotalPrice() {
            return useItem.getTotalPrice();
        }

        public void Remove() {
            InputOutputTypeContainer parent = FindParentHelper.FindParent<InputOutputTypeContainer>(this);
            parent.RemoveItem(this, foodItem.GetFood());
        }

        public void setInitFocus() {
            foodItem.setInitFocus();
        }

        private void Remove_Click(object sender, RoutedEventArgs e) {
            Remove();
        }

        private void NewFood_Click(object sender, RoutedEventArgs e) {
            InputOutputTypeContainer parent = FindParentHelper.FindParent<InputOutputTypeContainer>(this);
            parent.createNewFood();
        }

        public DayFoodModel GetTodaySetDatas() {
            Food food = foodItem.GetFood();
            if (food != null) {
                DayFoodModel todaySetModel = new DayFoodModel();
                todaySetModel.buyTrn = buyItem.getBuyTrn();
                todaySetModel.useTrn = useItem.getUseTrn();
                todaySetModel.food = food;
                return todaySetModel;
            } else {
                return null;
            }
        }
    }
}
