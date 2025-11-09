using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using WidgetSitefintiy.Mvc.Models;

namespace WidgetSitefintiy.Mvc.Controllers
{
    // The ControllerToolboxItem attribute registers the widget in Sitefinity backend
    [ControllerToolboxItem(Name = "Message", Title = "Message Widget", SectionName = "Custom Message")]
    public class MessageController : Controller
    {
        public string Message { get; set; }

        // GET: Message
        public ActionResult Index()
        {
            var model = new MessageModel();
            model.Message = this.Message;
            return View(model);
        }

        public ActionResult InsertWidget()
        {
            PageManager pageManager = PageManager.GetManager();
            var nodes = pageManager.GetPageNodes().Where(x => x.UrlName == "lino-page").FirstOrDefault();
            var pageData = nodes.GetPageData();
            var page = pageManager.EditPage(pageData.Id);
            MvcControllerProxy widget = new MvcControllerProxy();
            widget.ControllerName = typeof(MessageController).FullName;
            PageTemplate pt = pageManager.GetTemplates().Where(x => x.Title == "Lino Template").FirstOrDefault();

            string placeholderId = pt.Controls.Where(c => c.Caption.Contains("grid-6+6")).FirstOrDefault().PlaceHolders[0];

            var widgetControl = pageManager.CreateControl<PageDraftControl>(widget, placeholderId);
            widgetControl.Caption = "Way cool widget";
            pageManager.SetControlDefaultPermissions(widgetControl);
            page.Controls.Add(widgetControl);
            pageManager.PublishPageDraft(page);
            pageManager.SaveChanges();

            return View("InsertWidget");
        }
    }
}