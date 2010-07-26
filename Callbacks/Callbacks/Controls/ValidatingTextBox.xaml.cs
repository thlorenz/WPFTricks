using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Callbacks.Controls
{
    /// <summary>
    /// Interaction logic for ValidatingTextBox.xaml
    /// </summary>
    public partial class ValidatingTextBox : TextBox
    {
        string _lastValidText;
        Brush _validBorderBrush;
        Thickness _validBorderThickness;
        public static readonly DependencyProperty ValidateProperty =
            DependencyProperty.Register("Validate", typeof(Predicate<string>), typeof(ValidatingTextBox), new UIPropertyMetadata(null));

        public ValidatingTextBox()
        {
            InitializeComponent();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            _lastValidText = Text;
            _validBorderBrush = Background;
            _validBorderThickness = BorderThickness;
        }

        public Predicate<string> Validate
        {
            get { return (Predicate<string>)GetValue(ValidateProperty); }
            set { SetValue(ValidateProperty, value); }
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (Validate(Text))
            {
                BorderBrush = _validBorderBrush;
                BorderThickness = _validBorderThickness;
                base.OnLostFocus(e);
            }
            else
            {
                BorderBrush = new SolidColorBrush(Colors.Red);
                BorderThickness = new Thickness(3.0);
                CaretIndex = 0;
            }
        }
    }
}
