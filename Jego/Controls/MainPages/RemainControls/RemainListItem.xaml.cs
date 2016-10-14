using ExcelTemplateLib.DataModels;
using Jego.FSM.Models;
using Jego.Helper;
using JegoDatabase.Entities;
using JegoDatabase.Manager;
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
        private Food food;
        private Remain exRemain;
        private Remain newRemain;
        private Remain todayRemain;
        private string date;

        private decimal remainAmount = 0;

        public RemainListItem(FoodRemain foodRemain, string date) {
            InitializeComponent();
            this.date = date;
            initDatabaseItem(foodRemain);
        }

        public RemainListItem(string date) {
            InitializeComponent();
            this.date = date;
            setNewRemain(null);
        }

        private void initDatabaseItem(FoodRemain foodRemain) {
            setFood(foodRemain.food);
            setExRemain(foodRemain.remain);
            setNewRemain(foodRemain.todayRemain);
        }

        private void setFood(Food food) {
            this.food = food;
            if (food != null) {
                if (food.name != null) foodName_TextBox.Text = food.name;
                foodPrice_TextBox.Text = String.Format("{0:G}", food.unit_pirce);
                if (food.unit != null) foodUnit_TextBox.Text = food.unit;
            }
        }

        private void setExRemain(Remain remain) {
            this.exRemain = remain;
            if (exRemain != null) {
                remainAmount = exRemain.amount;
                foodExAmount_TextBox.Text = String.Format("{0:G}", exRemain.amount);
                if(food != null) 
                    foodExTotalPrice_TextBox.Text = String.Format("{0:G}", exRemain.amount * food.unit_pirce);
            }
        }

        private void setNewRemain(Remain remain) {
            if (remain != null) {
                newRemain = remain;
            } else {
                newRemain = getDefaultRemain(food);
            }

            todayRemain = new Remain();
            todayRemain.f_code = newRemain.f_code;
            todayRemain.amount = newRemain.amount;
            todayRemain.date = newRemain.date;
            todayRemain.deadline = newRemain.deadline;
            todayRemain.deadline_date = newRemain.deadline_date;
            todayRemain.fh_code = newRemain.fh_code;
            todayRemain.use_amount = newRemain.use_amount;
            todayRemain.buy_amount = newRemain.buy_amount;

            if (todayRemain != null) {
                foodAmount_TextBox.Text = String.Format("{0:G}", todayRemain.amount);
                if (food != null) {
                    foodTotalPrice_TextBox.Text = String.Format("{0:G}", todayRemain.amount * food.unit_pirce);
                }
            }
        }
        public void refreshTodayRemain() {
            todayRemain.f_code = newRemain.f_code;
            todayRemain.amount = newRemain.amount;
            todayRemain.date = newRemain.date;
            todayRemain.deadline = newRemain.deadline;
            todayRemain.deadline_date = newRemain.deadline_date;
            todayRemain.fh_code = newRemain.fh_code;
            todayRemain.use_amount = newRemain.use_amount;
            todayRemain.buy_amount = newRemain.buy_amount;

            if (todayRemain != null) {
                foodAmount_TextBox.Text = String.Format("{0:G}", todayRemain.amount);
                if (food != null) {
                    foodTotalPrice_TextBox.Text = String.Format("{0:G}", todayRemain.amount * food.unit_pirce);
                }
            }
        }

        private Remain getDefaultRemain(Food food) {
            Remain defaultRemain = new Remain();
            defaultRemain.date = date;
            if (food != null) {
                if (food.f_code != null) defaultRemain.f_code = food.f_code;
                defaultRemain.amount = remainAmount;
            }
            return defaultRemain;
        }

        public void setFoodData(Food food) {
            setFood(food);
        }

        public void setTodayData(BuyTrn buyTrn, UseTrn useTrn) {
            decimal todayRemainAmount = remainAmount;
            decimal buyAmount = 0;
            decimal useAmount = 0;
            if (buyTrn != null) {
                buyAmount += buyTrn.amount;
            } 
            if (useTrn != null) {
                useAmount -= useTrn.amount;
            }
            todayRemainAmount += (buyAmount + useAmount);

            if (todayRemain != null) {
                if (todayRemain.amount != todayRemainAmount) {
                    todayRemain.amount = todayRemainAmount;
                    todayRemain.buy_amount = buyAmount;
                    todayRemain.use_amount = useAmount;
                    todayRemain.date = date;

                    foodAmount_TextBox.Text = String.Format("{0:G}", todayRemain.amount);

                    if (exRemain == null) {
                        if(buyTrn != null) {
                            todayRemain.deadline = buyTrn.deadline;
                            todayRemain.deadline_date = todayRemain.date;
                            todayRemain.extra_amount = todayRemain.amount;
                        }
                    } else {
                        RemainDeadlineHelper.setDeadLine(todayRemain, exRemain);
                    }
                }
                if (food != null) {
                    foodTotalPrice_TextBox.Text = String.Format("{0:G}", todayRemain.amount * food.unit_pirce);
                }
            }
        }

        public bool isUpdated() {
            if (remainAmount != todayRemain.amount) return true;
            return false;
        }

        public Remain GetTodayRemains() {
            todayRemain.f_code = food.f_code;
            return todayRemain;
        }

        public bool isEqualFcode(string f_code) {
            if (food != null) {
                if (f_code.Equals(food.f_code)) return true;
            } 
            return false;
                
        }
    }
}
