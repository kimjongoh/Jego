using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dragonz.actb.core;
using JegoDatabase.Entities;
using System.Windows.Controls;
using Jego.AutoCompletes.Providers;
using System.Windows;

namespace Jego.AutoCompletes {
    public class FoodAutoCompleteManager : BaseAutoCompleteManger {
        public delegate void FoodAutoCompleteDelegate(Food food);
        private FoodAutoCompleteDelegate foodAutoCompleteCallback;
        public FoodAutoCompleteManager()
            : base() {
        }

        public FoodAutoCompleteManager(TextBox textBox, FoodAutoCompleteDelegate foodAutoCompleteCallback)
            : base(textBox) {
            this.foodAutoCompleteCallback = foodAutoCompleteCallback;
        }

        protected override void UpdateText(string text, bool selectAll) {
            FoodDataProvider foodDataProvider = _dataProvider as FoodDataProvider;
            Food food = foodDataProvider.GetFood(text);

            if (food != null) {
                if (foodAutoCompleteCallback != null) {
                    foodAutoCompleteCallback(food);
                }
                _textBox.Text = food.name;
            } else {
                _textBox.Text = text;
            }
            _textChangedByCode = true;

            if (selectAll) {
                _textBox.SelectAll();
            } else {
                _textBox.SelectionStart = _textBox.Text.Length;
            }
            _textChangedByCode = false;
        }

        protected override void ShowPopup() {
            var popupOnTop = false;

            var p = new Point(0, _textBox.ActualHeight);
            p = _textBox.PointToScreen(p);
            var tbBottom = p.Y;

            p = new Point(0, 0);
            p = _textBox.PointToScreen(p);
            var tbTop = p.Y;

            _popup.HorizontalOffset = p.X;
            var popupTop = tbBottom;

            if (!_manualResized) {
                _popup.Width = _textBox.ActualWidth*3 + POPUP_SHADOW_DEPTH;
            }

            double popupHeight;
            if (_manualResized) {
                popupHeight = _popup.Height;
            } else {
                var visibleCount = Math.Min(16, _listBox.Items.Count + 1);
                popupHeight = visibleCount * _itemHeight + POPUP_SHADOW_DEPTH;
            }
            var screenHeight = SystemParameters.PrimaryScreenHeight;
            if (popupTop + popupHeight > screenHeight) {
                if (screenHeight - tbBottom > tbTop) {
                    popupHeight = SystemParameters.PrimaryScreenHeight - popupTop;
                } else {
                    popupOnTop = true;
                    popupTop = tbTop - popupHeight + 4;
                    if (popupTop < 0) {
                        popupTop = 0;
                        popupHeight = tbTop + 4;
                    }
                }
            }
            PopupOnTop = popupOnTop;
            _popup.Height = popupHeight;
            _popup.VerticalOffset = popupTop;

            _popup.IsOpen = true;
        }
    }
}
