using Jego.Commands.Interfaces;
using Jego.Helper;
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

namespace Jego.Controls.MainPages.InputOutputControls {
    /// <summary>
    /// InputOutputItemUse.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputOutputItemUse : UserControl, NewFoodInterface {
        private InputOutputItem.TotalPriceChangeDelegate totalPriceChangeCallback;
        private Food food;
        private decimal defaultAmount = 0;
        public InputOutputItemUse() {
            InitializeComponent();
        }

        public void setTotalPriceChangeListener(InputOutputItem.TotalPriceChangeDelegate totalPriceChangeCallback) {
            this.totalPriceChangeCallback = totalPriceChangeCallback;
        }

        public void setFood(Food food) {
            this.food = food;
            if (food != null) {
                changeTextBlockText(food.unit_pirce);
            }
        }

        private void changeTextBlockText(decimal price) {
            unitPrice_TextBlock.Text = price.ToString();
            calculateTotalPrice();
        } 

        public void setUseTrn(UseTrn useTrn) {
            if (useTrn != null) {
                amount_TextBox.Text = useTrn.amount.ToString();
                defaultAmount = useTrn.amount;
            }
        }

        public UseTrn getUseTrn() {
            decimal _amount = defaultAmount;
            try {
                _amount = decimal.Parse(amount_TextBox.Text);
                if (_amount != 0)
                    return new UseTrn() { f_code = food.f_code, amount = _amount, total_price = getTotalPrice() };
                else
                    return null;
            } catch {
                return null;
            }
        }


        private void amount_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            foreach (char c in e.Text) {
                if (e.Text != ".") {
                    if (!char.IsDigit(c)) {
                        e.Handled = true;
                        break;
                    }
                }
            }
        }

        private void amount_TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            calculateTotalPrice();
        }

        private void calculateTotalPrice() {
            decimal totalDecimal = getTotalPrice();
            totalPrice_TextBlock.Text = totalDecimal.ToString();
            if (totalPriceChangeCallback != null)
                totalPriceChangeCallback();
        }

        public decimal getTotalPrice() {
            decimal amount = 0;
            try {
                amount = decimal.Parse(amount_TextBox.Text);
            } catch {
                amount = 0;
            }

            if (food != null)
                return food.unit_pirce * amount;
            else
                return 0;
        }

        private void Remove_Click(object sender, RoutedEventArgs e) {
            InputOutputItem parent = FindParentHelper.FindParent<InputOutputItem>(this);
            parent.Remove();
        }

        public void OnNewFoodExecute() {
            InputOutputTypeContainer parent = FindParentHelper.FindParent<InputOutputTypeContainer>(this);
            parent.createNewFood();
        }
    }
}
