using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{
    /// <summary>
    /// Base of ViewModel
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Raise a PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property changed, or empty if all properties are changed</param>
        protected virtual void RaisePropertyChanged(String propertyName) {
            var h = PropertyChanged;
            if (h != null)
                h(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Event raised when a property changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

    }

}
