using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace WPFSample1
{
  using CurtisInstruments.CaliburnMicroDevExpress.Extensions;

  public class InnerDocumentViewModel : DXDockingScreen
  {
    public InnerDocumentViewModel()
    {
      Products = Model.Product.GetProductList();
      TestControl = new TestControlViewModel();
      TestControl.Progress = 20;
      TestControl.Spin = 4.5M;
    }

    public BindableCollection<Model.Product> Products { get; set; }

    public Model.Product SelectedProduct { get; set; }

    public TestControlViewModel TestControl { get; set; }

    public void CheckState()
    {
    }

    public void Test()
    {
    }
  }
}