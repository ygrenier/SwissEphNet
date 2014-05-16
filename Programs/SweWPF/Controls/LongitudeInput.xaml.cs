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

namespace SweWPF.Controls
{
    /// <summary>
    /// Logique d'interaction pour LongitudeInput.xaml
    /// </summary>
    public partial class LongitudeInput : UserControl
    {
        public LongitudeInput() {
            Polarities = new SweNet.LongitudePolarity[]{
                SweNet.LongitudePolarity.East,
                SweNet.LongitudePolarity.West,
            };
            InitializeComponent();
        }

        bool _Updating = false;

        private void LongitudeChanged() {
            if (_Updating) return;
            _Updating = true;
            Degrees = Longitude.Degrees;
            Minutes = Longitude.Minutes;
            Seconds = Longitude.Seconds;
            Polarity = Longitude.Polarity;
            _Updating = false;
        }

        private void ComponentChanged() {
            if (_Updating) return;
            try {
                _Updating = true;
                Longitude = new SweNet.Longitude(Degrees, Minutes, Seconds, Polarity);
                _Updating = false;
            }
            catch {
                _Updating = false;
                LongitudeChanged();
            }
        }

        public SweNet.Longitude Longitude {
            get { return (SweNet.Longitude)GetValue(LongitudeProperty); }
            set { SetValue(LongitudeProperty, value); }
        }
        public static readonly DependencyProperty LongitudeProperty =
            DependencyProperty.Register("Longitude", typeof(SweNet.Longitude), typeof(LongitudeInput), new PropertyMetadata(new SweNet.Longitude(), LongitudePropertyChanged));

        private static void LongitudePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var li = d as LongitudeInput;
            if (li != null)
                li.LongitudeChanged();
        }

        public int Degrees {
            get { return (int)GetValue(DegreesProperty); }
            set { SetValue(DegreesProperty, value); }
        }
        public static readonly DependencyProperty DegreesProperty =
            DependencyProperty.Register("Degrees", typeof(int), typeof(LongitudeInput), new PropertyMetadata(0, LongitudeComponentChanged));

        private static void LongitudeComponentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var li = d as LongitudeInput;
            if (li != null)
                li.ComponentChanged();
        }

        public int Minutes {
            get { return (int)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }
        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof(int), typeof(LongitudeInput), new PropertyMetadata(0, LongitudeComponentChanged));

        public int Seconds {
            get { return (int)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }
        }
        public static readonly DependencyProperty SecondsProperty =
            DependencyProperty.Register("Seconds", typeof(int), typeof(LongitudeInput), new PropertyMetadata(0, LongitudeComponentChanged));

        public SweNet.LongitudePolarity Polarity {
            get { return (SweNet.LongitudePolarity)GetValue(PolarityProperty); }
            set { SetValue(PolarityProperty, value); }
        }
        public static readonly DependencyProperty PolarityProperty =
            DependencyProperty.Register("Polarity", typeof(SweNet.LongitudePolarity), typeof(LongitudeInput), new PropertyMetadata(SweNet.LongitudePolarity.East, LongitudeComponentChanged));

        public SweNet.LongitudePolarity[] Polarities { get; private set; }

    }
}
