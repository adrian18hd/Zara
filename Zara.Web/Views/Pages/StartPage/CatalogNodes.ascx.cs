using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Extensions;
using EPiServer.Commerce.SpecializedProperties;
using EPiServer.Core;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Catalog.Dto;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.Pages.StartPage
{
	public partial class CatalogNodes : RendererControlBase<NodeContent>
	{
		protected string GetCatalogNodeImageUrl(NodeContent content)
		{
			if (content != null)
			{
				ItemCollection<CommerceMedia> mediaItems = content.CommerceMediaCollection;
				if (mediaItems != null && mediaItems.Count > 0)
				{
					return GetMediaUrl(mediaItems.First());
				}
			}
			return string.Empty;
		}

		protected override void OnLoad(EventArgs e)
		{
			CatalogDto dto = CatalogSystem.GetCatalogDto();
			ContentReference catalogLink = Locate.ReferenceConverter()
				.GetContentLink(dto.Catalog.First().CatalogId, CatalogContentType.Catalog, 0);
			IEnumerable<NodeContent> topCatalogNodes = ContentLoader.GetChildren<NodeContent>(catalogLink).ToList();
			rptTopLevelCatalogNodes.DataSource = topCatalogNodes;
			rptTopLevelCatalogNodes.DataBind();
		}
	}
}