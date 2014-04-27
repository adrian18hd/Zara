using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Zara.Web.Business;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.Pages.StartPage.ContentTypes
{
	[ContentType(
		DisplayName = "Start Page",
		GUID = "A343BADF-B0B5-434C-955F-B5FEFC728BAF",
		Description = "Zara start page",
		GroupName = Constants.ContentGroups.Content)]
	public class StartPage : SitePageDataBase
	{
		[Display(
			Name = "Commerce Settings",
			Description = "Commerce settings for the site",
			GroupName = SystemTabNames.Settings,
			Order = 0)]
		public virtual CommerceSettingsBlock Settings { get; set; }

		[Display(
			Name = "Main block",
			Description = "",
			GroupName = SystemTabNames.Content,
			Order = 10)]
		public virtual XhtmlString MainBlock { get; set; }
	}
}