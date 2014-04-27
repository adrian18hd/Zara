using System.Web.Routing;
using EPiServer.Commerce.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Zara.Importer;
using InitializationModule = EPiServer.Commerce.Initialization.InitializationModule;

namespace Zara.Web.Business
{
	/// <summary>
	///     This class will be executed the first time the site is loaded. Best place to put initialization code.
	/// </summary>
	[ModuleDependency(typeof (InitializationModule))]
	public class CommerceInitialization : IConfigurableModule
	{
		public void Initialize(InitializationEngine context)
		{
			CatalogRouteHelper.MapDefaultHierarchialRouter(RouteTable.Routes, false);

			ImportManager.Import();
		}

		public void Uninitialize(InitializationEngine context)
		{
		}

		public void Preload(string[] parameters)
		{
		}

		public void ConfigureContainer(ServiceConfigurationContext context)
		{
		}
	}
}