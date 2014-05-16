using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SweWPF.ViewModels
{

    public class RelayCommand : ICommand
    {
        Action _Execute;
        Func<bool> _CanExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null) {
            this._Execute = execute;
            this._CanExecute = canExecute;
        }

        public void RaiseCanExecuteChanged() {
            var h = CanExecuteChanged;
            if (h != null)
                h(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter) {
            return _CanExecute != null ? _CanExecute() : true;
        }

        public void Execute(object parameter) {
            if (CanExecute(parameter))
                _Execute();
        }

        public event EventHandler CanExecuteChanged;

    }

}
