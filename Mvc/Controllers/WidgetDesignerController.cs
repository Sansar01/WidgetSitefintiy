using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web;
using WidgetSitefintiy.Mvc.Models;
using Telerik.Sitefinity.Web.UI;
using System.Activities.Statements;
using System.ComponentModel;
using Telerik.Sitefinity.Personalization;

namespace WidgetSitefintiy.Mvc.Controllers
{
    // The ControllerToolboxItem attribute registers the widget in Sitefinity backend
    [ControllerToolboxItem(Name = "Widget", Title = "Widget Designer", SectionName = "Custom Widget Desginer", CssClass = "sfCardIcn sfMvcIcn")]
    public class WidgetDesignerController : Controller, ICustomWidgetVisualization, IPersonalizable
    {
        public WidgetModel obj { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ImageId { get; set; }
        public string ImageProviderName { get; set; }
        public Guid PageId { get; set; }
        private string template = "Default";
        public string CssClass { get; set; }

        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return IsEmptyValue();
            }
        }

        private bool IsEmptyValue()
        {
            if (Title == null || string.IsNullOrEmpty(Title))
            {
                return true;
            }
            return false;
        }

        public string WidgetCssClass
        {
            get
            {
                return "sfCardIcn";
            }
        }

        public string EmptyLinkText
        {
            get
            {
                return "Click edit to modify the widget settings.";
            }
        }

        public string Template
        {
            get
            {
                return this.template;
            }
            set
            {
                this.template = value;
            }
        }

        public ActionResult Index(WidgetModel models)
        {
            var model = new WidgetModel();
            model.Title = Title;
            model.Description = Description;
            model.pageUrl = GetPageUrl();
            model.CssClass = CssClass;

            if (ImageId != Guid.Empty)
            {
                var image = LibrariesManager.GetManager(this.ImageProviderName).GetImage(this.ImageId);
                if (image != null)
                {
                    model.ImageUrl = image.Url;
                }

            }

            return View("WidgetDesigner." + this.Template,model);
        }

        private string GetPageUrl()
        {

            if (PageId != Guid.Empty)
            {
                var sitemap = SitefinitySiteMap.GetCurrentProvider();

                SiteMapNode node;
                var sitefinitySiteMap = sitemap as SiteMapBase;
                if (sitefinitySiteMap != null)
                    node = sitefinitySiteMap.FindSiteMapNodeFromKey(this.PageId.ToString(), false);
                else
                    node = sitemap.FindSiteMapNodeFromKey(this.PageId.ToString());

                if (node != null)
                    return UrlPath.ResolveUrl(node.Url, true);

            }
            return string.Empty;
        }
    }
}