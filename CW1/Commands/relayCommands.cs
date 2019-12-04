using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CW1.Commands
{
    /* A RelayCommand lets the ViewModels class know
     *  that the button has been clicked or that an.
     *  event has happened
     *  
     *  RelayCommmand inherits from the ICommand interface
     */
    public class RelayCommand : ICommand
    {
        private Action _action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action action)
        {
            _action = action;
        }

        /*The CanExecute() will always return true.
         * This is to allow the Command to run.
         */
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /*The Execute() tells the private field to
         * actually run the code which we will
         * inject into the RelayCommand via the Constructor.
         */
        public void Execute(object parameter)
        {
            _action();
        }
    }
}
