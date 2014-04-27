using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace Zara.Web.Views.BaseClasses
{
	/// <summary>
	/// This class allows the upload of images (with the extensions provided) in the EPiServer media system. 
	/// Without this class you will not be able to upload any image.
	/// </summary>
	[ContentType(GUID = "2F0E3AE3-09AC-4F79-8CFA-907FC84301E7")]
	[MediaDescriptor(ExtensionString = "jpg,jpeg,gif,png")]
	public class ImageFile : ImageData
	{
	}
}