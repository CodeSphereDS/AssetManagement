using System;
using System.Windows.Documents;
using System.Windows;

namespace CodeSphere.CSClasses.CSAdorner
{
    public class CSUserControlAdornerBehavior
    {
        public static readonly DependencyProperty ShowAdornerProperty =
        DependencyProperty.RegisterAttached("ShowAdorner", typeof(bool),
        typeof(CSUserControlAdornerBehavior), new UIPropertyMetadata(false, OnShowAdornerChanged));

        public static readonly DependencyProperty ControlProperty =
            DependencyProperty.RegisterAttached("Control", typeof(FrameworkElement),
            typeof(CSUserControlAdornerBehavior), new UIPropertyMetadata(null));

        private static readonly DependencyProperty CtrlAdornerProperty =
            DependencyProperty.RegisterAttached("CtrlAdorner", typeof(CSUserControlAdorner),
            typeof(CSUserControlAdornerBehavior), new UIPropertyMetadata(null));

        public static bool GetShowAdorner(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowAdornerProperty);
        }

        public static void SetShowAdorner(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowAdornerProperty, value);
        }


        public static FrameworkElement GetControl(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(ControlProperty);
        }

        public static void SetControl(DependencyObject obj, UIElement value)
        {
            obj.SetValue(ControlProperty, value);
        }

       private static void OnShowAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement)
            {
                if (e.NewValue != null)
                {
                    FrameworkElement adornedElement = d as FrameworkElement;
                    bool bValue = (bool)e.NewValue;
                    FrameworkElement adorningElement = GetControl(d);
 
                    CSUserControlAdorner ctrlAdorner = adornedElement.GetValue(CtrlAdornerProperty) as CSUserControlAdorner;
                    if (ctrlAdorner != null)
                        ctrlAdorner.RemoveLayer();
 
                    if (bValue && adorningElement != null)
                    {
                        if (adornedElement.IsLoaded)
                        {
                            ApplyAdornerLayer(d, adorningElement, adornedElement, ctrlAdorner);
                        }
                        else
                        {
                            adornedElement.Loaded += (sender, args) =>
                            {
                                // Its possible that the 'ShowAdorner' property may be set to
                                // false before we actually get to apply the adorner in which
                                // case we don't need to bother
                                bool showAdorner = (bool)d.GetValue(ShowAdornerProperty);
                                if (showAdorner)
                                {
                                    ApplyAdornerLayer(d, adorningElement, adornedElement, ctrlAdorner);
                                }
                            };
                        }
                    }
                }
            }
        }
 
        private static void ApplyAdornerLayer(DependencyObject d, FrameworkElement adorningElement, FrameworkElement adornedElement, CSUserControlAdorner ctrlAdorner)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(adornedElement);
            if (null != adornerLayer)
            {
                ctrlAdorner = new CSUserControlAdorner(adornedElement, adorningElement);
                ctrlAdorner.SetLayer(adornerLayer);
                d.SetValue(CtrlAdornerProperty, ctrlAdorner);
            }
        }
        }
    }


