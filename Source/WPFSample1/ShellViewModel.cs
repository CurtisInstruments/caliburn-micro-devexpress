namespace WPFSample1
{
  using System.ComponentModel.Composition;
  using Caliburn.Micro;
  using CurtisInstruments.CaliburnMicroDevExpress.Extensions;

  [Export(typeof(IShell))]
  public class ShellViewModel : DevExpressConductorOneActive<IScreen>, IShell
  {
    private int count = 1;

    public void New()
    {
      ActivateItem(new InnerDocumentViewModel() { DisplayName = "Inner document " + count++, CloseAction = this.CloseAction });
    }
  }
}