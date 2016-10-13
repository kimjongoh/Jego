using ExcelTemplateLib.DataModels;
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
    /// RemainListItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RemainListItem : UserControl {
        
        public RemainListItem(DayFoodModel dayModel) {
            InitializeComponent();
            setDayFoodModel(dayModel);
        }

        public void setDayFoodModel(DayFoodModel dayModel) {
            if (dayModel != null) {
                if (dayModel.food != null) {
                    foodName_TextBox.Text = getString(dayModel.food.name);
                    foodPrice_TextBox.Text = dayModel.food.unit_pirce.ToString();
                    foodUnit_TextBox.Text = getString(dayModel.food.unit);
                } else {
                    foodPrice_TextBox.Text = "0";
                }
                if (dayModel.remain != null) {
                    foodExAmount_TextBox.Text = dayModel.remain.amount.ToString();
                    foodExTotalPrice_TextBox.Text = (dayModel.remain.amount * dayModel.food.unit_pirce).ToString();
                } else {
                    foodExAmount_TextBox.Text = "0";
                    foodExTotalPrice_TextBox.Text = "0";
                }

                setTodayData(dayModel.buyTrn, dayModel.useTrn);
            }
        }

        private string getString(string str) {
            if (str != null)
                return str;
            else
                return "";
        }

        public void setTodayData(BuyTrn buyTrn, UseTrn useTrn) {
            decimal currentAmount = decimal.Parse(foodExAmount_TextBox.Text);
            if (buyTrn != null) {
                currentAmount += buyTrn.amount;
            }
            if (useTrn != null) {
                currentAmount -= useTrn.amount;
            }
            foodAmount_TextBox.Text = currentAmount.ToString();

            decimal foodPrice = decimal.Parse(foodPrice_TextBox.Text);
            foodTotalPrice_TextBox.Text = (currentAmount * foodPrice).ToString();
        }
    }
}
