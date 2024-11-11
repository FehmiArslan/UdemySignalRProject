using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace SignalRWebUI.ViewComponents.LayoutComponents
{
	public class _LayoutFooterPartialComponent:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
