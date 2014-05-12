using System;
using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.SpecializedProperties;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Mediachase.Commerce.Catalog;

namespace Zara.Web.Views.BaseClasses
{
	/// <summary>
	/// This is a base class that allows rendering of content controls. It provides access to most used interfaces and methods
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RendererControlBase<T> : ContentControlBase<T> where T : CatalogContentBase
	{
		private IPermanentLinkMapper _permanentLinkMapper;
		private UrlResolver _urlResolver;
		private IContentLoader _contentLoader;
		private ICatalogSystem _catalogSystem;
		private ILinksRepository _linksRepository;
		private ReferenceConverter _referenceConverter;

		protected IPermanentLinkMapper PermanentLinkMapper
		{
			get
			{
				return _permanentLinkMapper ?? (_permanentLinkMapper = ServiceLocator.Current.GetInstance<IPermanentLinkMapper>());
			}
		}

		protected UrlResolver UrlResolver
		{
			get { return _urlResolver ?? (_urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>()); }
		}

		protected IContentLoader ContentLoader
		{
			get { return _contentLoader ?? (_contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>()); }
		}

		protected ICatalogSystem CatalogSystem
		{
			get { return _catalogSystem ?? (_catalogSystem = ServiceLocator.Current.GetInstance<ICatalogSystem>()); }
		}

		protected ILinksRepository LinksRepository
		{
			get { return _linksRepository ?? (_linksRepository = ServiceLocator.Current.GetInstance<ILinksRepository>()); }
		}

		protected ReferenceConverter ReferenceConverter
		{
			get
			{
				return _referenceConverter ?? (_referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>());
			}
		}

		protected string GetUrl(CatalogContentBase content)
		{
			return content != null ? UrlResolver.GetUrl(content.ContentLink, content.Language.Name) : String.Empty;
		}

		protected string GetMediaUrl(CommerceMedia media)
		{
			if (media == null)
			{
				return String.Empty;
			}
			PermanentLinkMap contentLinkMap = PermanentLinkMapper.Find(new Guid(media.AssetKey));
			if (contentLinkMap == null)
			{
				return String.Empty;
			}
			return contentLinkMap.MappedUrl.ToString();
		}
	}
}