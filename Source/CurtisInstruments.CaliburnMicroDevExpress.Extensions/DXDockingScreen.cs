namespace CurtisInstruments.CaliburnMicroDevExpress.Extensions
{
  using System;
  using System.Windows.Input;
  using Caliburn.Micro;
  using DevExpress.Xpf.Docking;

  /// <summary>
  /// Screen descendant to support binding DocumentGroup.ItemsSource to the conductor.
  /// </summary>
  public class DXDockingScreen : Screen, IMVVMDockingProperties
  {
    private string targetName;

    /// <summary>
    /// Name of the target layout panel to show the Screen. Not yet used.
    /// </summary>
    public string TargetName
    {
      get
      {
        return targetName;
      }
      set
      {
        targetName = value;
        NotifyOfPropertyChange(() => TargetName);
      }
    }

    private Action<IScreen> closeAction;

    /// <summary>
    /// An Action used to close the Screen. The conductor must set it to enable closing bound viewmodels.
    /// </summary>
    public Action<IScreen> CloseAction
    {
      get { return closeAction; }
      set
      {
        if (closeAction != value)
        {
          closeAction = value;
          closeCommand = new DXCloseCommand(RequestClose);
          NotifyOfPropertyChange(() => CloseCommand);
        }
      }
    }

    private void RequestClose(object documentPanel)
    {
      CloseAction(this);
    }

    private ICommand closeCommand;

    /// <summary>
    /// The Command the DocumentPanel.CloseCommand is bound to.
    /// </summary>
    public ICommand CloseCommand
    {
      get { return closeCommand; }
    }
  }
}