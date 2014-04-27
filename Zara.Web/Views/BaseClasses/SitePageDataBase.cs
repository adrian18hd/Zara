using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace Zara.Web.Views.BaseClasses
{
	/// <summary>
	/// Base class for all CMS pages
	/// </summary>
	public class SitePageDataBase : PageData
	{
		[Searchable]
		[Display(
			Name = "Title",
			Description = "",
			GroupName = SystemTabNames.Content,
			Order = 1100)]
		public virtual string PageTitle { get; set; }

		[CultureSpecific]
		[Searchable]
		[Display(
			Name = "Keywords",
			Description = "",
			GroupName = SystemTabNames.Settings,
			Order = 1200)]
		public virtual string Keywords { get; set; }

		[CultureSpecific]
		[Searchable]
		[UIHint(UIHint.Textarea)]
		[Display(
			Name = "Description",
			Description = "",
			GroupName = SystemTabNames.Settings,
			Order = 1300)]
		public virtual string Description { get; set; }
	}
}