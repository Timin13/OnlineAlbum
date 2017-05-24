using OnlineAlbum.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace OnlineAlbum.Core
{
    public static class ImageActionLinkHelper
    {
        public static IHtmlString ImageActionLink(this AjaxHelper helper, byte[] image, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", $"data:image/jpeg;base64," + Convert.ToBase64String(image));
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            var link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions).ToHtmlString();
            return MvcHtmlString.Create(link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
        }
    }
}