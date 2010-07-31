using System.Windows;
using System.Windows.Documents;
using Shared.Adorners;

namespace Shared.AttachedBehaviors
{
    public static class ResizeAttachedBehavior
    {
        public static readonly DependencyProperty IsResizeableProperty;
        public static readonly DependencyProperty ThumbStyleProperty;

        static ResizeAttachedBehavior()
        {
            IsResizeableProperty = DependencyProperty.RegisterAttached(
                    "IsResizeable",
                    typeof(bool),
                    typeof(ResizeAttachedBehavior),
                    new UIPropertyMetadata((bool)false, OnIsResizeableChanged));
            
            ThumbStyleProperty = DependencyProperty.RegisterAttached(
                "ThumbStyle",
                typeof(Style),
                typeof(ResizeAttachedBehavior),
                new UIPropertyMetadata(null));
        }

        public static bool GetIsResizeable(DependencyObject d)
        {
            return (bool)d.GetValue(IsResizeableProperty);
        }

        public static Style GetThumbStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(ThumbStyleProperty);
        }

        public static void SetIsResizeable(DependencyObject d, bool value)
        {
            d.SetValue(IsResizeableProperty, value);
        }

        public static void SetThumbStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(ThumbStyleProperty, value);
        }
        static void OnIsResizeableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var resizeableElement = d as FrameworkElement;

            if ((bool)e.NewValue)
                resizeableElement.Loaded += ResizeableElement_Loaded;
            else
                resizeableElement.Loaded -= ResizeableElement_Loaded;
        }

        static void ResizeableElement_Loaded(object sender, RoutedEventArgs e)
        {
            var resizeableElement = sender as FrameworkElement;
            var adornerLayer = AdornerLayer.GetAdornerLayer(resizeableElement);
            var addedAdorner = new ResizeAdorner(resizeableElement, GetThumbStyle(resizeableElement));
            adornerLayer.Add(addedAdorner);
        }
    }
}
