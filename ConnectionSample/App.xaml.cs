using ConnectionSample.Core.Models;
using ConnectionSample.Modules.ModuleName;
using ConnectionSample.Services;
using ConnectionSample.Services.Interfaces;
using ConnectionSample.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace ConnectionSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationStatusInfo, ApplicationStatusInfo>();
            containerRegistry.RegisterSingleton<IServerService, ServerService>();
            containerRegistry.RegisterSingleton<IClientService, ClientService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
