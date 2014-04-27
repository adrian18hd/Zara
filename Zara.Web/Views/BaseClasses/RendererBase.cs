using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Filters;
using EPiServer.Security;
using EPiServer.Web;

namespace Zara.Web.Views.BaseClasses
{
	/// <summary>
	/// Base class for rendering catalog content
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RendererBase<T> : ContentPageBase<T> where T: CatalogContentBase
	{
		protected override void OnInit(EventArgs e)
		{
			bool hasCatalogEditAccess = PrincipalInfo.Current.Principal.IsInRole(CatalogSecurityDescriptor.CommerceAdminsRoleName);
			if (!hasCatalogEditAccess && new FilterContentForVisitor().ShouldFilter(CurrentContent))
			{
				AccessDenied();
			}
			base.OnInit(e);
		}
	}
}