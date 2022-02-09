using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WatchModule.Helpers
{
    public class AttachedProperties
    {
        #region HideExpanderArrow AttachedProperty

        [AttachedPropertyBrowsableForType(typeof(Expander))]
        public static bool GetHideExpanderArrow(DependencyObject obj)
        {
            return (bool)obj.GetValue(HideExpanderArrowProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Expander))]
        public static void SetHideExpanderArrow(DependencyObject obj, bool value)
        {
            obj.SetValue(HideExpanderArrowProperty, value);
        }

        // Using a DependencyProperty as the backing store for HideExpanderArrow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HideExpanderArrowProperty =
            DependencyProperty.RegisterAttached("HideExpanderArrow", typeof(bool), typeof(AttachedProperties), new UIPropertyMetadata(false, OnHideExpanderArrowChanged));

        private static void OnHideExpanderArrowChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Expander expander = (Expander)o;

            if (expander.IsLoaded)
            {
                UpdateExpanderArrow(expander, (bool)e.NewValue);
            }
            else
            {
                expander.Loaded += new RoutedEventHandler((x, y) => UpdateExpanderArrow(expander, (bool)e.NewValue));
            }
        }

        private static void UpdateExpanderArrow(Expander expander, bool visible)
        {
            Grid headerGrid =
                VisualTreeHelper.GetChild(
                    VisualTreeHelper.GetChild(
                            VisualTreeHelper.GetChild(
                                VisualTreeHelper.GetChild(
                                    VisualTreeHelper.GetChild(
                                        expander,
                                        0),
                                    0),
                                0),
                            0),
                        0) as Grid;

            headerGrid.Children[0].Visibility = visible ? Visibility.Collapsed : Visibility.Visible; // Hide or show the Ellipse
            headerGrid.Children[1].Visibility = visible ? Visibility.Collapsed : Visibility.Visible; // Hide or show the Arrow
            headerGrid.Children[2].SetValue(Grid.ColumnProperty, visible ? 0 : 1); // If the Arrow is not visible, then shift the Header Content to the first column.
            headerGrid.Children[2].SetValue(Grid.ColumnSpanProperty, visible ? 2 : 1); // If the Arrow is not visible, then set the Header Content to span both rows.
            headerGrid.Children[2].SetValue(ContentPresenter.MarginProperty, visible ? new Thickness(0) : new Thickness(4, 0, 0, 0)); // If the Arrow is not visible, then remove the margin from the Content.
        }

        #endregion
    }

    public class TextBoxBehavior
    {
        public static bool GetSelectAllTextOnFocus(TextBox textBox)
        {
            return (bool)textBox.GetValue(SelectAllTextOnFocusProperty);
        }

        public static void SetSelectAllTextOnFocus(TextBox textBox, bool value)
        {
            textBox.SetValue(SelectAllTextOnFocusProperty, value);
        }

        public static readonly DependencyProperty SelectAllTextOnFocusProperty =
            DependencyProperty.RegisterAttached(
                "SelectAllTextOnFocus",
                typeof(bool),
                typeof(TextBoxBehavior),
                new UIPropertyMetadata(false, OnSelectAllTextOnFocusChanged));

        private static void OnSelectAllTextOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox == null) return;

            if (e.NewValue is bool == false) return;

            if ((bool)e.NewValue)
            {
                textBox.GotFocus += SelectAll;
                textBox.PreviewMouseDown += IgnoreMouseButton;
            }
            else
            {
                textBox.GotFocus -= SelectAll;
                textBox.PreviewMouseDown -= IgnoreMouseButton;
            }
        }

        private static void SelectAll(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox == null) return;
            textBox.SelectAll();
        }

        private static void IgnoreMouseButton(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null || textBox.IsKeyboardFocusWithin) return;

            e.Handled = true;
            textBox.Focus();
        }
    }
}
