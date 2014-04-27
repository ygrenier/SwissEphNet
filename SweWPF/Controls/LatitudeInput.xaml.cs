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
    /// Logique d'interaction pour LatitudeInput.xaml
    /// </summary>
    public partial class LatitudeInput : UserControl
    {
        public LatitudeInput() {
            Polarities = new SweNet.LatitudePolarity[]{
                SweNet.LatitudePolarity.North,
                SweNet.LatitudePolarity.South,
            };
            InitializeComponent();
        }

        bool _Updating = false;

        private void LatitudeChanged() {
            if (_Updating) return;
            _Updating = true;
            Degrees = Latitude.Degrees;
            Minutes = Latitude.Minutes;
            Seconds = Latitude.Seconds;
            Polarity = Latitude.Polarity;
            _Updating = false;
        }

        private void ComponentChanged() {
            if (_Updating) return;
            try {
                _Updating = true;
                Latitude = new SweNet.Latitude(Degrees, Minutes, Seconds, Polarity);
                _Updating = false;
            }
            catch {
                _Updating = false;
                LatitudeChanged();
            }
        }

        public SweNet.Latitude Latitude {
            get { return (SweNet.Latitude)GetValue(LatitudeProperty); }
            set { SetValue(LatitudeProperty, value); }
        }
        public static readonly DependencyProperty LatitudeProperty =
            DependencyProperty.Register("Latitude", typeof(SweNet.Latitude), typeof(LatitudeInput), new PropertyMetadata(new SweNet.Latitude(), LatitudePropertyChanged));

        private static void LatitudePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var li = d as LatitudeInput;
            if (li != null)
                li.LatitudeChanged();
        }

        public int Degrees {
            get { return (int)GetValue(DegreesProperty); }
            set { SetValue(DegreesProperty, value); }
        }
        public static readonly DependencyProperty DegreesProperty =
            DependencyProperty.Register("Degrees", typeof(int), typeof(LatitudeInput), new PropertyMetadata(0, LatitudeComponentChanged));

        private static void LatitudeComponentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var li = d as LatitudeInput;
            if (li != null)
                li.ComponentChanged();            
        }

        public int Minutes {
            get { return (int)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }
        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof(int), typeof(LatitudeInput), new PropertyMetadata(0, LatitudeComponentChanged));

        public int Seconds {
            get { return (int)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }
        }
        public static readonly DependencyProperty SecondsProperty =
            DependencyProperty.Register("Seconds", typeof(int), typeof(LatitudeInput), new PropertyMetadata(0, LatitudeComponentChanged));

        public SweNet.LatitudePolarity Polarity {
            get { return (SweNet.LatitudePolarity)GetValue(PolarityProperty); }
            set { SetValue(PolarityProperty, value); }
        }
        public static readonly DependencyProperty PolarityProperty =
            DependencyProperty.Register("Polarity", typeof(SweNet.LatitudePolarity), typeof(LatitudeInput), new PropertyMetadata(SweNet.LatitudePolarity.North, LatitudeComponentChanged));

        public SweNet.LatitudePolarity[] Polarities { get; private set; }

    }
}
