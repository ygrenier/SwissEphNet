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

namespace SweWPF.Views
{
    /// <summary>
    /// Logique d'interaction pour InputDateView.xaml
    /// </summary>
    public partial class InputDateView : UserControl
    {
        public InputDateView() {
            InitializeComponent();
            cbTimeType.SelectedIndex = 0;
        }

        private void cbTimeType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ViewModels.EphemerisDateType edt = cbTimeType.SelectedValue is ViewModels.EphemerisDateType ? (ViewModels.EphemerisDateType)cbTimeType.SelectedValue : ViewModels.EphemerisDateType.UniversalTime;
            VisualStateManager.GoToState(this, edt.ToString() + "State", true);
        }

    }
}
