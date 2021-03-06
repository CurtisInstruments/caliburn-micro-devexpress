﻿namespace WPFSample
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.Composition;
  using System.ComponentModel.Composition.Hosting;
  using System.ComponentModel.Composition.Primitives;
  using System.Linq;
  using Caliburn.Micro;
  using System.Windows;

  public class AppBootstrapper : BootstrapperBase, IDisposable
  {
    private CompositionContainer container;

    static AppBootstrapper()
    {
      Caliburn.Micro.DevExpress.DXConventions.Install();
    }

    public AppBootstrapper()
      : base()
    {
      Initialize();
    }

    /// <summary>
    /// By default, we are configured to use MEF
    /// </summary>
    protected override void Configure()
    {
      var catalog = new AggregateCatalog(
          AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
          );

      container = new CompositionContainer(catalog);

      var batch = new CompositionBatch();

      batch.AddExportedValue<IWindowManager>(new WindowManager());
      batch.AddExportedValue<IEventAggregator>(new EventAggregator());
      batch.AddExportedValue(container);
      batch.AddExportedValue(catalog);

      container.Compose(batch);
    }

    protected override object GetInstance(Type serviceType, string key)
    {
      string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
      var exports = container.GetExportedValues<object>(contract);

      if (exports.Any())
        return exports.First();

      throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
    }

    protected override IEnumerable<object> GetAllInstances(Type serviceType)
    {
      return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
    }

    protected override void BuildUp(object instance)
    {
      container.SatisfyImportsOnce(instance);
    }

    protected override void OnStartup(object sender, StartupEventArgs e)
    {
      DisplayRootViewFor<IShell>();
    }

    public void Dispose()
    {
      if (container != null)
      {
        container.Dispose();
        container = null;
      }
    }
  }
}