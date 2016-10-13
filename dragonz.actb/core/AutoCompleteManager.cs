using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using dragonz.actb.provider;
using Microsoft.Windows.Themes;

namespace dragonz.actb.core
{
    public class AutoCompleteManager : BaseAutoCompleteManger
    {
        public AutoCompleteManager() : base()
        {
            // default constructor
        }

        public AutoCompleteManager(TextBox textBox) : base(textBox)
        {
            
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
                _popup.Width = _textBox.ActualWidth + POPUP_SHADOW_DEPTH;
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

        protected override void UpdateText(string text, bool selectAll) {
            _textChangedByCode = true;
            _textBox.Text = text;
            if (selectAll) 
            {
                _textBox.SelectAll();
            }
            else
            {
                _textBox.SelectionStart = text.Length;
            }
            _textChangedByCode = false;
        }
    }
}