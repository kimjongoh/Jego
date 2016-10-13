using Jego.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jego.Commands {
    public class NewFoodCommand : ICommand {
        public bool CanExecute(object parameter) {
            if (parameter != null) return true;
            else return false;
        }

        public void Execute(object parameter) {
            var parentObject = parameter;
            if (parentObject is NewFoodInterface) {
                NewFoodInterface nextInputControl = parentObject as NewFoodInterface;
                nextInputControl.OnNewFoodExecute();
            }
        }


        public event EventHandler CanExecuteChanged;
    }
}
