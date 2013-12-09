using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.ClearScript.V8;

namespace MadsKristensen.EditorExtensions.ScriptEngines
{
	class V8ScriptEngine : IScriptEngine
	{
		private readonly Dispatcher _dispatcher;
		private static bool? _isAvailable = null;

		public V8ScriptEngine(Dispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}

		public void Dispose()
		{
			// nothing to dispose
		}

		public bool IsAvailable()
		{
			Trace.WriteLine("Checking to see if V8 is available");
			if (V8ScriptEngine._isAvailable.HasValue)
				return V8ScriptEngine._isAvailable.Value;

			try
			{
				var engine = new Microsoft.ClearScript.V8.V8ScriptEngine();
				engine.Dispose();
				V8ScriptEngine._isAvailable = true;
			}
			catch (Exception)
			{
				V8ScriptEngine._isAvailable = false;
			}

			Trace.WriteLine(string.Format("V8 Engine is available: {0}", V8ScriptEngine._isAvailable.Value));
			return V8ScriptEngine._isAvailable.Value;
		}

		public void Compile(string source, object external)
		{
			_dispatcher.BeginInvoke(new Action(() =>
			{
				var engine = new Microsoft.ClearScript.V8.V8ScriptEngine();
				engine.AddHostObject("external", external);
				engine.Execute(source);
				engine.Dispose();
			}), DispatcherPriority.ApplicationIdle, null);
		}
	}
}
