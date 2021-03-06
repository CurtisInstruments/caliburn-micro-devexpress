﻿namespace CurtisInstruments.CaliburnMicroDevExpress.Extensions
{
  using System.Windows;
  using Caliburn.Micro;

  public class DXWindowManager<TWindow> : WindowManager where TWindow : Window, new()
  {
    /// <summary>
    /// Makes sure the view is a DX window or is wrapped by one.
    /// Same as the original version of WindowManager.EnsureWindow axcept that it creates a DXWindow instead of Window.
    /// </summary>
    /// <param name="model">The view model.</param>
    /// <param name="view">The view.</param>
    /// <param name="isDialog">Whethor or not the window is being shown as a dialog.</param>
    /// <returns>The window.</returns>
    protected override Window EnsureWindow(object model, object view, bool isDialog)
    {
      var window = view as Window;

      if (window == null)
      {
        window = new TWindow
        {
          Content = view,
          SizeToContent = SizeToContent.WidthAndHeight
        };

        window.SetValue(View.IsGeneratedProperty, true);

        var owner = InferOwnerOf(window);
        if (owner != null)
        {
          window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
          window.Owner = owner;
        }
        else
        {
          window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
      }
      else
      {
        var owner = InferOwnerOf(window);
        if (owner != null && isDialog)
        {
          window.Owner = owner;
        }
      }

      return window;
    }
  }
}