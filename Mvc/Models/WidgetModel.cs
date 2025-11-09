using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WidgetSitefintiy.Mvc.Models
{
    public class WidgetModel
    {
        public Guid pageId { get; set; }

        public string pageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageProviderName { get; set; }

        public Guid ImageId { get; set; }

        public string ImageUrl { get; set; }

        public string CssClass { get; set; }

    }
}