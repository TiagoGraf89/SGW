using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SGW.Portal
{
	// Summary:
	//     Represents support for rendering HTML controls in a strongly typed view.
	//
	// Type parameters:
	//   TModel:
	//     The type of the model.
	public static class HtmlHelperExtender
    {
        //
        // GET: /HtmlHelper/
		public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name,
										 IEnumerable<SelectListItem> items)
		{
			var output = new StringBuilder();
			output.Append(@"<div class=""checkboxList"">");

			foreach (var item in items)
			{
				output.Append(@"<div><input type=""checkbox"" name=""");
				output.Append(name);
				output.Append("\" value=\"");
				output.Append(item.Value);
				output.Append("\"");

				if (item.Selected)
					output.Append(@" checked=""checked""");

				output.Append(" />");
				output.Append("<Label>");
				output.Append(item.Text);
				output.Append("</Label></div>");
			}

			output.Append("</div>");

			return new MvcHtmlString(output.ToString());
		}

		public static string RenderPartialViewToString(Controller thisController, string viewName, object model)
		{
			// assign the model of the controller from which this method was called to the instance of the passed controller (a new instance, by the way)
			thisController.ViewData.Model = model;

			// initialize a string builder
			using (StringWriter sw = new StringWriter())
			{
				// find and load the view or partial view, pass it through the controller factory
				ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(thisController.ControllerContext, viewName);
				ViewContext viewContext = new ViewContext(thisController.ControllerContext, viewResult.View, thisController.ViewData, thisController.TempData, sw);

				// render it
				viewResult.View.Render(viewContext, sw);

				//return the razorized view/partial-view as a string
				return sw.ToString();
			}
		}
    }
}
