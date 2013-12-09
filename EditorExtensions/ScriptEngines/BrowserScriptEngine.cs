using System;
using System.Windows.Forms;
using System.Windows.Threading;

namespace MadsKristensen.EditorExtensions.ScriptEngines
{
	class BrowserScriptEngine : IScriptEngine
	{
		private readonly Dispatcher _dispatcher;
		private WebBrowser _browser = new WebBrowser();

		public BrowserScriptEngine(Dispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}

		public void Dispose()
		{
			if(_browser != null)
				_browser.Dispose();
			_browser = null;
		}

		public bool IsAvailable()
		{
			return true;
		}

		public void Compile(string source, object external)
		{
			_dispatcher.BeginInvoke(new Action(() =>
			{
				_browser.ObjectForScripting = external;
				_browser.ScriptErrorsSuppressed = true;
				_browser.DocumentText = CreateHtml(source);
			}), DispatcherPriority.ApplicationIdle, null);
		}

		private static string CreateHtml(string source)
		{
			return "<html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=9\" /><script>" + source + "</script></head><html/>";
		}
	}
}
