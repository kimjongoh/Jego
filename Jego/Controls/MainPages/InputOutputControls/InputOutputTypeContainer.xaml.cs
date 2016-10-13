using Jego.FSM.Interfaces.FMS.Params;
using JegoDatabase.Entities;
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
    /// InputOutputTypeContainer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputOutputTypeContainer : UserControl {

        private List<string> removeFoods = new List<string>();
       
        public string type { get; set; }
        
        public InputOutputTypeContainer(string type) {
            InitializeComponent();
            init(type);
        }

        private void init(string type) {
            this.type = type;
            initTypeTextBlock();
            initTextBlockWidths();
        }

        private void initTypeTextBlock() {
            type_TextBlock.Text = type;
        }

        private void initTextBlockWidths() {
            totalTitle_textBlock.Width = (double)Application.Current.Resources["foodName_InputOutput_width"] + 10;
            totalblank_textBlock.Width = ((double)Application.Current.Resources["foodPrice_InputOutput_width"] +
                (double)Application.Current.Resources["foodDesc_InputOutput_width"] +
                (double)Application.Current.Resources["foodUnit_InputOutput_width"] + 30);

            totalBuy_textBlock.Width = ((double)Application.Current.Resources["foodAmount_InputOutput_width"] +
                (double)Application.Current.Resources["foodPrice_InputOutput_width"] +
                (double)Application.Current.Resources["foodTotalPrice_InputOutput_width"] + 28);

            totalUse_textBlock.Width = ((double)Application.Current.Resources["foodDeadLine_InputOutput_width"] +
                (double)Application.Current.Resources["foodAmount_InputOutput_width"] +
                (double)Application.Current.Resources["foodPrice_InputOutput_width"] +
                (double)Application.Current.Resources["foodTotalPrice_InputOutput_width"] + 40);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            createNewFood();
        }

        public void createNewFood() {
            InputOutputItem item = new InputOutputItem(type, new DayFoodModel(), changeBuyTotalPrice, changeUseTotalPrice);
            inputOutput_ListView.Items.Add(item);
            item.setInitFocus();
        }

       

        private void changeBuyTotalPrice() {
            decimal totalPrice = 0;
            foreach (InputOutputItem item in inputOutput_ListView.Items) {
                totalPrice += item.GetBuyTotalPrice();
            }
            totalBuy_textBlock.Text = string.Format("{0:N0}", totalPrice);
        }

        private void changeUseTotalPrice() {
            decimal totalPrice = 0;
            foreach (InputOutputItem item in inputOutput_ListView.Items) {
                totalPrice += item.GetUseTotalPrice();
            }
            totalUse_textBlock.Text = string.Format("{0:N0}", totalPrice);
        }
       

        public void Clear() {
            inputOutput_ListView.Items.Clear();
        }

        public void RemoveItem(InputOutputItem inputOutputItem, Food food) {
            if (food != null && food.name != null && !food.name.Trim().Equals("")) {
                food.CreateCode();
                if(!removeFoods.Contains(food.f_code))
                    removeFoods.Add(food.f_code);
            }
            if (inputOutput_ListView.Items.Contains(inputOutputItem))
                inputOutput_ListView.Items.Remove(inputOutputItem);
        }

        public List<DayFoodModel> GetTodaySetDatas() {
            List<DayFoodModel> dayModels = new List<DayFoodModel>();
            foreach (InputOutputItem item in inputOutput_ListView.Items) {
                DayFoodModel dayModel = item.GetTodaySetDatas();
                if (dayModel != null) {
                    dayModel.food.type = type;
                    dayModels.Add(dayModel);
                }
            }
            return dayModels;
        }

        public void addDayModel(DayFoodModel dayModel) {
            inputOutput_ListView.Items.Add(new InputOutputItem(type, dayModel, changeBuyTotalPrice, changeUseTotalPrice));
            changeBuyTotalPrice();
        }
    }
}
