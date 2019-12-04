using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace CW1.ViewModels
{
    /*The INotifyPropertyChanged and the OnChanged method
     *sends a signal to the UI,
     * whenever the viewmodel has changed and updates the
     * UI with the new value.
      */
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChnaged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
