/// <summary>
/// printf management
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweNet
{

    partial class SwissEph
    {

		/// <summary>
		/// Do a printf
		/// </summary>
        protected void printf(String format, params object[] args) {
            var h = OnPrint;
            if (h != null)
                OnPrint(this, new PrintEventArgs(C.sprintf(format, args)));
        }

		/// <summary>
		/// Event raised when a call to printf is done
		/// </summary>
        public event EventHandler<PrintEventArgs> OnPrint;
    }

}
