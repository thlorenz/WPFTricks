using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace Shared.Adorners
{
    public class ResizeAdorner : Adorner
    {
        FrameworkElement _adornedElement;
        Thumb _cornerThumb;
        VisualCollection _visualChildren;

        public ResizeAdorner(UIElement adornedElement, Style thumbStyle)
            : base(adornedElement)
        {
            _visualChildren = new VisualCollection(this);

            BuildResizeThumb(thumbStyle);
            _cornerThumb.DragDelta += CornerThumb_DragDelta;
           
            _adornedElement = AdornedElement as FrameworkElement;
            InitializeSize(_adornedElement);
        }

        protected override int VisualChildrenCount
        {
            get { return _visualChildren.Count; }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var adornedWidth = _adornedElement.DesiredSize.Width;
            var adornedHeight = _adornedElement.DesiredSize.Height;
            var adornerWidth = DesiredSize.Width;
            var adornerHeight = DesiredSize.Height;

            var rect = new Rect(
                adornedWidth - adornerWidth / 2,
                adornedHeight - adornerHeight / 2,
                adornerWidth,
                adornerHeight);

            _cornerThumb.Arrange(rect);

            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visualChildren[index];
        }

        void BuildResizeThumb(Style thumbStyle)
        {
            if (_cornerThumb == null)
            {
                _cornerThumb = new Thumb { Style = thumbStyle };
                _visualChildren.Add(_cornerThumb);
            }
        }

        void CornerThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;

            if (_adornedElement != null && thumb != null)
            {
               
                _adornedElement.Width = Math.Max(_adornedElement.Width + e.HorizontalChange, thumb.DesiredSize.Width);
                _adornedElement.Height = Math.Max(_adornedElement.Height + e.VerticalChange, thumb.DesiredSize.Height);
            }
        }

        void InitializeSize(FrameworkElement adornedElement)
        {
            if (adornedElement.Width.Equals(Double.NaN))
                adornedElement.Width = adornedElement.DesiredSize.Width;
            if (adornedElement.Height.Equals(Double.NaN))
                adornedElement.Height = adornedElement.DesiredSize.Height;

            FrameworkElement parent = adornedElement.Parent as FrameworkElement;
            if (parent != null)
            {
                adornedElement.MaxHeight = parent.ActualHeight;
                adornedElement.MaxWidth = parent.ActualWidth;
            }
        }
    }
}
