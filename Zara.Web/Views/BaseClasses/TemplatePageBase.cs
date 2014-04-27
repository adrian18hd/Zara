using EPiServer;
using EPiServer.Core;

namespace Zara.Web.Views.BaseClasses
{
	/// <summary>
	/// Base class for all CMS page templates
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TemplatePageBase<T> : TemplatePage<T> where T : PageData
	{
	}
}