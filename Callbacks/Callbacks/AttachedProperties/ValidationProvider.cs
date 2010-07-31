using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Callbacks.AttachedProperties
{
    public static class ValidationProvider
    {
        static Brush _validBorderBrush;
        static Thickness _validBorderThickness;

        public static readonly DependencyProperty ValidateProperty =
            DependencyProperty.RegisterAttached("Validate", typeof(Predicate<string>), typeof(ValidationProvider), new UIPropertyMetadata(new PropertyChangedCallback(OnValidateChanged)));

        public static Predicate<string> GetValidate(DependencyObject obj)
        {
            return (Predicate<string>)obj.GetValue(ValidateProperty);
        }

        public static void SetValidate(DependencyObject obj, Predicate<string> value)
        {
            obj.SetValue(ValidateProperty, value);
        }

        static void OnValidateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var txtBox = (TextBox)d;
            _validBorderBrush = txtBox.BorderBrush;
            _validBorderThickness = txtBox.BorderThickness;

            if ((Predicate<string>)e.NewValue != null)
                txtBox.LostFocus += txtBox_LostFocus;
            else
                txtBox.LostFocus -= txtBox_LostFocus;
        }

        static void txtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var txtBox = (TextBox)sender;
            var validate = GetValidate(txtBox);
       
            if (validate(txtBox.Text))
            {
                txtBox.BorderBrush = _validBorderBrush;
                txtBox.BorderThickness = _validBorderThickness;
            }
            else
            {
                txtBox.BorderBrush = new SolidColorBrush(Colors.Red);
                txtBox.BorderThickness = new Thickness(3.0);
                e.Handled = true;
            }
        }
    }
}
