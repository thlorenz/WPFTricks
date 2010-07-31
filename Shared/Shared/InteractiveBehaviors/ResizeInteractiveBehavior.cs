using System.Windows;
using System.Windows.Documents;
using System.Windows.Interactivity;
using Shared.Adorners;

namespace Shared.InteractiveBehaviors
{
    public class ResizeInteractiveBehavior : Behavior<FrameworkElement>
    {
      
        public static readonly DependencyProperty ThumbStyleProperty =
            DependencyProperty.Register("ThumbStyle", typeof(Style), typeof(ResizeInteractiveBehavior), new UIPropertyMetadata(null));

        public Style ThumbStyle
        {
            get { return (Style)GetValue(ThumbStyleProperty); }
            set { SetValue(ThumbStyleProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
         
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
            var addedAdorner = new ResizeAdorner(AssociatedObject, ThumbStyle);
            adornerLayer.Add(addedAdorner);
        }
    }
}
