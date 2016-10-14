using Jego.AutoCompletes;
using Jego.AutoCompletes.Providers;
using Jego.Commands.Interfaces;
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
    /// InputOutputItemFood.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputOutputItemFood : UserControl, NewFoodInterface {
        private Food food = new Food();
        private Action<Food> foodListener;
        private FoodAutoCompleteManager nameAutoCompleteManager;
        private string type;

        public InputOutputItemFood() {
            InitializeComponent();
        }

        private void initAutoComplete() {
            Window wind = Window.GetWindow(foodName_TextBox);
            nameAutoCompleteManager = new FoodAutoCompleteManager(foodName_TextBox, changeAutoCompleteName);
            nameAutoCompleteManager.DataProvider = FoodDataProviderManager.getInstance();
            nameAutoCompleteManager.Asynchronous = false;
        }

        private void changeAutoCompleteName(Food food) {
            foodPrice_TextBox.TextChanged -= foodPrice_TextBox_TextChanged;
            foodDesc_TextBox.TextChanged -= foodDesc_TextBox_TextChanged;
            foodUnit_TextBox.TextChanged -= foodUnit_TextBox_TextChanged;
            this.food.unit_pirce = food.unit_pirce;
            this.food.desc = food.desc;
            this.food.unit = food.unit;
            foodPrice_TextBox.Text = food.unit_pirce.ToString();
            foodDesc_TextBox.Text = food.desc;
            foodUnit_TextBox.Text = food.unit;
            foodPrice_TextBox.TextChanged += foodPrice_TextBox_TextChanged;
            foodDesc_TextBox.TextChanged += foodDesc_TextBox_TextChanged;
            foodUnit_TextBox.TextChanged += foodUnit_TextBox_TextChanged;
        }
       
        public void setFood(Food food) {
            if (food != null) {
                this.food.name = food.name;
                this.food.unit_pirce = food.unit_pirce;
                this.food.desc = food.desc;
                this.food.unit = food.unit;

                foodName_TextBox.Text = this.food.name;
                foodPrice_TextBox.Text = this.food.unit_pirce.ToString();
                foodDesc_TextBox.Text = this.food.desc;
                foodUnit_TextBox.Text = this.food.unit;
            } else {
                this.food.name = "";
                this.food.unit_pirce = 0;
                this.food.desc = "";
                this.food.unit = "";

                foodName_TextBox.Text = this.food.name;
                foodPrice_TextBox.Text = this.food.unit_pirce.ToString();
                foodDesc_TextBox.Text = this.food.desc;
                foodUnit_TextBox.Text = this.food.unit;
            }
            if (foodListener != null)
                foodListener(this.food);
        }

        public Food GetFood() {
            if (!"".Equals(this.food.name)) {
                food.unit = foodUnit_TextBox.Text.Trim();
                food.type = type;
                food.CreateCode();
                return this.food;
            } else
                return null;
        }

        public void setChangeFoodListener(Action<Food> changeFood) {
            this.foodListener = changeFood;
        }

        private void foodPrice_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            foreach (char c in e.Text) {
                if (e.Text != ".") {
                    if (!char.IsDigit(c)) {
                        e.Handled = true;
                        break;
                    }
                }
            }
        }

        private void foodPrice_TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            decimal amount = 0;
            try {
                amount = decimal.Parse(foodPrice_TextBox.Text);
            } catch {
                amount = 0;
            }
            food.unit_pirce = amount;
            if (foodListener != null) {
                food.CreateCode();
                foodListener(food);
            }
        }

        private void foodName_TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            food.name = foodName_TextBox.Text.Trim();
            if (foodListener != null) {
                food.CreateCode();
                foodListener(food);
            }
        }

        private void foodDesc_TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            food.desc = foodDesc_TextBox.Text.Trim();
            if (foodListener != null) {
                food.CreateCode();
                foodListener(food);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            initAutoComplete();
        }

        public void OnNewFoodExecute() {
            InputOutputTypeContainer parent = FindParentHelper.FindParent<InputOutputTypeContainer>(this);
            parent.createNewFood();
        }

        public void setInitFocus() {
            foodName_TextBox.Loaded += foodName_TextBox_Loaded;
        }

        void foodName_TextBox_Loaded(object sender, RoutedEventArgs e) {
            foodName_TextBox.Focusable = true;
            foodName_TextBox.Focus();
            Keyboard.Focus(foodName_TextBox);
            foodName_TextBox.Loaded -= foodName_TextBox_Loaded;
        }

        private void Remove_Click(object sender, RoutedEventArgs e) {
            InputOutputItem parent = FindParentHelper.FindParent<InputOutputItem>(this);
            parent.Remove();
        }

        public void setType(string type) {
            this.type = type;
        }

        private void foodUnit_TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            food.unit = foodUnit_TextBox.Text.Trim();
            if (foodListener != null) {
                food.CreateCode();
                foodListener(food);
            }
        }
    }
}
