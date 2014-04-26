using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweWPF.ViewModels
{

    /// <summary>
    /// Child viewmodel
    /// </summary>
    public abstract class ChildViewModel : ViewModel
    {
        /// <summary>
        /// Method called when MainModel initialze the child
        /// </summary>
        internal void Start(MainViewModel main) {
            this.MainModel = main;
        }

        /// <summary>
        /// MainViewModel caller
        /// </summary>
        public MainViewModel MainModel { get; private set; }
    }

}
