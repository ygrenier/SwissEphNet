using SweNet;
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
    /// Logique d'interaction pour DateTimeInput.xaml
    /// </summary>
    public partial class DateTimeInput : UserControl
    {
        public DateTimeInput() {
            InitializeComponent();
            Date = new DateUT(DateTime.Now);
        }

        bool _Updating = false;

        private void DateChanged() {
            if (_Updating) return;
            _Updating = true;
            Day = Date.Day;
            Month = Date.Month;
            Year = Date.Year;
            Hours = Date.Hours;
            Minutes = Date.Minutes;
            Seconds = Date.Seconds;
            _Updating = false;
        }

        private void ElementDateChanged() {
            if (_Updating) return;
            try {
                _Updating = true;
                Date = new DateUT(Year, Month, Day, Hours, Minutes, Seconds);
                _Updating = false;
            }
            catch {
                _Updating = false;
                DateChanged();
            }
        }

        public DateUT Date {
            get { return (DateUT)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateUT), typeof(DateTimeInput), new PropertyMetadata(new DateUT(), DatePropertyChanged));

        private static void DatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var dti = d as DateTimeInput;
            if (dti != null)
                dti.DateChanged();
        }


        public int Day {
            get { return (int)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }
        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(int), typeof(DateTimeInput), new PropertyMetadata(1, ElementDatePropertyChanged));

        public int Month {
            get { return (int)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
        }
        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(int), typeof(DateTimeInput), new PropertyMetadata(1, ElementDatePropertyChanged));

        public int Year {
            get { return (int)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(int), typeof(DateTimeInput), new PropertyMetadata(1, ElementDatePropertyChanged));

        public int Hours {
            get { return (int)GetValue(HoursProperty); }
            set { SetValue(HoursProperty, value); }
        }
        public static readonly DependencyProperty HoursProperty =
            DependencyProperty.Register("Hours", typeof(int), typeof(DateTimeInput), new PropertyMetadata(1, ElementDatePropertyChanged));

        public int Minutes {
            get { return (int)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }
        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof(int), typeof(DateTimeInput), new PropertyMetadata(1, ElementDatePropertyChanged));

        public int Seconds {
            get { return (int)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }
        }
        public static readonly DependencyProperty SecondsProperty =
            DependencyProperty.Register("Seconds", typeof(int), typeof(DateTimeInput), new PropertyMetadata(1, ElementDatePropertyChanged));

        private static void ElementDatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var dti = d as DateTimeInput;
            if (dti != null)
                dti.ElementDateChanged();
        }


    }
}
